using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Plugin.Connectivity;

namespace encuesta.Vistas
{
    public partial class SynchronizationInitial : ContentPage
    {
        protected Database DB { get; set; }
        protected ObservableCollection<SynchroInfo> SynchroInfoList { get; set; }

        private const double progressPercentage = 1 / 7;
        private double Progress { get; set; }

        public SynchronizationInitial()
        {
            InitializeComponent();
            DB = new Database("Encuesta");
            SynchroInfoList = new ObservableCollection<SynchroInfo>();
            SyncListView.ItemsSource = SynchroInfoList;
            
            Task.Run(async () => await Sync());
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


        // Synchronize all the tables that need to be pulled from server.
        protected async Task Sync()
        {
            if (await CheckConnection())
            {
                // Get data from server.
                // Update UI.
                // Insert new data on local DB.
                var users = await GetUsersAsync();
                UpdateSynchro(new SynchroInfo("Usuarios", users != null));
                if (users != null)
                {
                    DB.DeleteAll<User>();
                    foreach (var item in users) { DB.InsertItemWithID(item); }
                }

                var customers = await GetCustomersAsync();
                UpdateSynchro(new SynchroInfo("Clientes", customers != null));
                if (customers != null)
                {
                    DB.DeleteAll<Customer>();
                    foreach (var item in customers) { DB.InsertItemWithID(item); }
                }

                var surveys = await GetSurveysAsync();
                UpdateSynchro(new SynchroInfo("Encuestas", surveys != null));
                if (surveys != null)
                {
                    DB.DeleteAll<Survey>();
                    foreach (var item in surveys) { DB.InsertItemWithID(item); }
                }

                var surveyQuestions = await GetSurveyQuestionsAsync();
                UpdateSynchro(new SynchroInfo("Preguntas de Encuestas", surveyQuestions != null));
                if (surveyQuestions != null)
                {
                    DB.DeleteAll<SurveyQuestion>();
                    foreach (var item in surveyQuestions) { DB.InsertItemWithID(item); }
                }

                var sq = DB.GetItems<SurveyQuestion>();

                var moments = await GetMoments();
                UpdateSynchro(new SynchroInfo("Categorías", moments != null));
                if (moments != null)
                {
                    DB.DeleteAll<Moment>();
                    foreach (var item in moments) { DB.InsertItemWithID(item); }
                }

                var questions = await GetQuestionAsync();
                UpdateSynchro(new SynchroInfo("Preguntas", questions != null));
                if (questions != null)
                {
                    DB.DeleteAll<Question>();
                    foreach (var item in questions) { DB.InsertItemWithID(item); }
                }

                var sq2 = DB.GetItems<Question>();
                var questionOptions = await GetQuestionOptionsAsync();
                UpdateSynchro(new SynchroInfo("Opciones de preguntas", questionOptions != null));
                if (questionOptions != null)
                {
                    DB.DeleteAll<QuestionOption>();
                    foreach (var item in questionOptions) { DB.InsertItemWithID(item); }
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert("Sincronización", SynchroInfoList.Any(x => !x.Success) ?
                        "Uno o más elementos han fallado." : "La sincronización ha finalizado con éxito.", "OK");
                });
            }
            
            return;
        }
        

        protected void InsertNewData<T>(List<T> list)
        {
            // Erase all elements from table.

            // Insert the new ones.
            
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
        public async Task<List<User>> GetUsersAsync()
        {
            var httpClient = new HttpClient();

            try
            {
                var uri = new Uri("http://s-tmkt.com/dev/encuesta/app/GetUsers.php");
                var jsonResponse = httpClient.GetAsync(uri).Result;
                var jsonString = jsonResponse.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<User>>(jsonString.Result);

                return list;
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", e.Message, "OK");
            }

            return null;
        }


        public async Task<List<Customer>> GetCustomersAsync()
        {
            var httpClient = new HttpClient();

            try
            {
                var uri = new Uri("http://s-tmkt.com/dev/encuesta/app/GetCustomers.php");
                var jsonResponse = httpClient.GetAsync(uri).Result;
                var jsonString = jsonResponse.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<Customer>>(jsonString.Result);
                
                return list;
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", e.Message, "OK");
            }

            return null;
        }


        public async Task<List<Survey>> GetSurveysAsync()
        {
            var httpClient = new HttpClient();

            try
            {
                var uri = new Uri("http://s-tmkt.com/dev/encuesta/app/GetSurveys.php");
                var jsonResponse = httpClient.GetAsync(uri).Result;
                var jsonString = jsonResponse.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<Survey>>(jsonString.Result);

                return list;
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", e.Message, "OK");
            }

            return null;
        }


        public async Task<List<Moment>> GetMoments()
        {
            var httpClient = new HttpClient();

            try
            {
                var uri = new Uri("http://s-tmkt.com/dev/encuesta/app/GetMoments.php");
                var jsonResponse = httpClient.GetAsync(uri).Result;
                var jsonString = jsonResponse.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<Moment>>(jsonString.Result);

                return list;
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", e.Message, "OK");
            }

            return null;
        }

        public async Task<List<Question>> GetQuestionAsync()
        {
            var httpClient = new HttpClient();

            try
            {
                var uri = new Uri("http://s-tmkt.com/dev/encuesta/app/GetQuestions.php");
                var jsonResponse = httpClient.GetAsync(uri).Result;
                var jsonString = jsonResponse.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<Question>>(jsonString.Result);

                return list;
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", e.Message, "OK");
            }

            return null;
        }


        public async Task<List<QuestionOption>> GetQuestionOptionsAsync()
        {
            var httpClient = new HttpClient();

            try
            {
                var uri = new Uri("http://s-tmkt.com/dev/encuesta/app/GetQuestionOptions.php");
                var jsonResponse = httpClient.GetAsync(uri).Result;
                var jsonString = jsonResponse.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<QuestionOption>>(jsonString.Result);

                return list;
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", e.Message, "OK");
            }

            return null;
        }


        public async Task<List<SurveyQuestion>> GetSurveyQuestionsAsync()
        {
            var httpClient = new HttpClient();

            try
            {
                var uri = new Uri("http://s-tmkt.com/dev/encuesta/app/GetSurveyQuestions.php");
                var jsonResponse = httpClient.GetAsync(uri).Result;
                var jsonString = jsonResponse.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<SurveyQuestion>>(jsonString.Result);

                return list;
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", e.Message, "OK");
            }

            return null;
        }

        #endregion
    }
}