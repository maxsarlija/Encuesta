using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Text;
using encuesta.Dominio.Enum;
using Plugin.Connectivity;

namespace encuesta.Vistas
{
    public partial class SynchronizationSurveys : ContentPage
    {
        protected Database DB { get; set; }
        protected ObservableCollection<SynchroInfo> SynchroInfoList { get; set; }


        public SynchronizationSurveys()
        {
            InitializeComponent();

            DB = new Database("Encuesta");

            SynchroInfoList = new ObservableCollection<SynchroInfo>();
            SyncListView.ItemsSource = SynchroInfoList;

            System.Threading.Tasks.Task.Run(async () => await Sync());
        }


        // Check connection first, before synchronizing.
        protected async Task<bool> CheckConnection()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                if (await CrossConnectivity.Current.IsRemoteReachable("http://s-tmkt.com/"))
                {
                    return true;
                }
                else { await DisplayAlert("Alerta", "Error al contactar servidor.", "OK"); }
            }
            else { await DisplayAlert("Alerta", "Su dispositivo no se encuentra conectado a internet.", "OK"); }

            return false;
        }

        protected async System.Threading.Tasks.Task Sync()
        {
            if (await CheckConnection())
            {
                // Get all customer answers that have been completed fully.
                var customerAnswers = DB.Query<CustomerAnswer>("SELECT * FROM CustomerAnswer").Where(x => x.Status.Equals(SurveyStatus.COMPLETED));
                var tasks = DB.Query<encuesta.Tasks>("SELECT * FROM Task").Where(x => x.Status.Equals(Dominio.Enum.TaskStatus.COMPLETED));

                // Sync all the surveys that have been completed.
                if (customerAnswers != null)
                {
                    foreach (var item in customerAnswers)
                    {
                        if(DB.GetItems<Survey>().Where(x => x.ID == item.SurveyID).FirstOrDefault() != null)
                            await PostCustomerAnswerAsync(item);
                    }
                }

                // Sync all the tasks that have been completed.
                if (tasks != null)
                {
                    foreach (var item in tasks)
                    {
                        var result = await PostTasksAsync(item);
                    }
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert("Sincronización", SynchroInfoList.Any(x => !x.Success) ?
                        "Uno o más elementos han fallado." : "La sincronización ha finalizado con éxito.", "OK");
                });
            }

            return;
        }


        protected void UpdateSynchro(SynchroInfo _synchroInfo)
        {
            SynchroInfoList.Add(_synchroInfo);
        }

        public class SynchroInfo
        {
            public SynchroInfo(string _name, bool _success)
            {
                Name = _name;
                Success = _success;
                SuccessDetails = _success ? "OK" : "Error";
                SuccessColor = _success ? Color.Green : Color.Red;
            }

            public string Name { get; set; }
            public bool Success { get; set; }
            public string SuccessDetails { get; set; }
            public Color SuccessColor { get; set; }
        }



        #region Repositories

        // Post del CustomerAnswer primero.
        // Levantamos todas las respuestas de esa instancia, y las sincronizamos con la base de datos.
        public async Task<bool> PostCustomerAnswerAsync(CustomerAnswer _customerAnswer)
        {
            var result = false;
            var httpClient = new HttpClient();

            try
            {
                var uriCustomerAnswer = new Uri("http://s-tmkt.com/dev/encuesta/app/PostCustomerAnswer.php");
                var httpContent = new StringContent(JsonConvert.SerializeObject(_customerAnswer), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(uriCustomerAnswer, httpContent);

                if (response.Content != null)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;

                    result = await PostAnswersAsync(_customerAnswer, Convert.ToInt32(jsonString));
                }
                

                UpdateSynchro(new SynchroInfo("Encuesta transmitida exitosamente.", result));

                return result;
            }
            catch (Exception e)
            {
                UpdateSynchro(new SynchroInfo("Error transmitiendo encuesta.", result));
                await DisplayAlert("Error", e.Message, "OK");
            }

            return false;
        }


        // Mandar todas las respuestas de la instancia de encuesta.
        // Enviamos nueva ID de CustomerAnswer devuelta por el servidor.
        public async Task<bool> PostAnswersAsync(CustomerAnswer _customerAnswer, int _newID)
        {
            var httpClient = new HttpClient();

            try
            {

                var app_answers = DB.Query<Answer>("SELECT * FROM Answer").Where(x => x.CustomerAnswerID == _customerAnswer.ID);
                var web_answers = new List<Answer>();
                foreach (var item in app_answers)
                {
                    web_answers.Add(new Answer(_newID, _newID, item.QuestionID, item.Option, item.Score));
                }

                var uriAnswers = new Uri("http://s-tmkt.com/dev/encuesta/app/PostAnswers.php");
                var httpContent = new StringContent(JsonConvert.SerializeObject(web_answers), Encoding.UTF8, "application/json");


                var response = await httpClient.PostAsync(uriAnswers, httpContent);

                if (response.Content != null)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                }

                // After synchronizing all the answers, delete them from the local DB.
                foreach (var item in app_answers)
                {
                    DB.DeleteItem<Answer>(item.ID);
                }

                // Delete CustomerAnswer as well.
                DB.DeleteItem<CustomerAnswer>(_customerAnswer.ID);

                
                return true;
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", e.Message, "OK");
            }

            return false;
        }

        public async Task<bool> PostTasksAsync(encuesta.Tasks _task)
        {
            var result = false;
            var httpClient = new HttpClient();

            try
            {
                var uriTask = new Uri("http://s-tmkt.com/dev/encuesta/app/PostTasks.php");
                var httpContent = new StringContent(JsonConvert.SerializeObject(_task), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(uriTask, httpContent);

                if (response.Content != null)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                }

                result = true;

                UpdateSynchro(new SynchroInfo("Tarea transmitida exitosamente.", result));
                DB.DeleteItem<encuesta.Tasks>(_task.ID);

                return result;
            }
            catch (Exception e)
            {
                UpdateSynchro(new SynchroInfo("Error transmitiendo tarea.", result));
                await DisplayAlert("Error", e.Message, "OK");
            }

            return false;
        }
        #endregion
    }
}