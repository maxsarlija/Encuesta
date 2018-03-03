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
        

        public SynchronizationInitial()
        {
            InitializeComponent();
            DB = new Database("Encuesta");
            var script = new InitialScript(DB);
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
                else
                {
                    await DisplayAlert("Alerta", "Error al contactar servidor.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Alerta", "Su dispositivo no se encuentra conectado a internet.", "OK");
            }

            return false;
        }


        // Synchronize all the tables that need to be pulled from server.
        protected async System.Threading.Tasks.Task Sync()
        {
            if (await CheckConnection())
            {
                // Get data from server.
                // Update UI.
                // Insert new data on local DB.

                // Users.
                var users = await GetUsersAsync();
                UpdateSynchro(new SynchroInfo("Usuarios", users != null));
                if (users != null)
                {
                    DB.DeleteAll<User>();
                    foreach (var item in users) { DB.InsertItemWithID(item); }
                }

                // Customers.
                var customers = await GetCustomersAsync();
                UpdateSynchro(new SynchroInfo("Clientes", customers != null));
                if (customers != null)
                {
                    DB.DeleteAll<Customer>();
                    foreach (var item in customers) { DB.InsertItemWithID(item); }
                }
                
                // User Class.
                var classes = await GetClassesAsync();
                UpdateSynchro(new SynchroInfo("Clases de Usuario", classes != null));
                if (classes != null)
                {
                    DB.DeleteAll<Class>();
                    foreach (var item in classes) { DB.InsertItemWithID(item); }
                }

                // Salesmen - Supervisors table.
                var salesmen = await GetSalesmenAsync();
                UpdateSynchro(new SynchroInfo("Supervisor - Vendedor", salesmen != null));
                if (salesmen != null)
                {
                    DB.DeleteAll<Salesmen>();
                    foreach (var item in salesmen) { DB.SaveItem(item); }
                }

                // Surveys.
                var surveys = await GetSurveysAsync();
                UpdateSynchro(new SynchroInfo("Encuestas", surveys != null));
                if (surveys != null)
                {
                    DB.DeleteAll<Survey>();
                    foreach (var item in surveys) { DB.InsertItemWithID(item); }
                }

                // Groups.
                var groups = await GetGroupsAsync();
                UpdateSynchro(new SynchroInfo("Grupos", groups != null));
                if (groups != null)
                {
                    DB.DeleteAll<Group>();
                    foreach (var item in groups) { DB.InsertItemWithID(item); }
                }

                // SubGroups - Groups table.
                var subSurveyGroups = await GetSurveyGroupsAsync();
                UpdateSynchro(new SynchroInfo("Grupos de Encuesta", subSurveyGroups != null));
                if (subSurveyGroups != null)
                {
                    DB.DeleteAll<SurveyGroup>();
                    foreach (var item in subSurveyGroups) { DB.SaveItem(item); }
                }
                
                // SubGroups.
                var subGroups = await GetSubGroupsAsync();
                UpdateSynchro(new SynchroInfo("SubGrupos", subGroups != null));
                if (subGroups != null)
                {
                    DB.DeleteAll<SubGroup>();
                    foreach (var item in subGroups) { DB.InsertItemWithID(item); }
                }

                // SubGroup Questions.
                var subGroupQuestions = await GetSubGroupQuestionsAsync();
                UpdateSynchro(new SynchroInfo("Preguntas de SubGrupos", subGroupQuestions != null));
                if (subGroupQuestions != null)
                {
                    DB.DeleteAll<SubGroupQuestion>();
                    foreach (var item in subGroupQuestions) { DB.SaveItem(item); }
                }
                
                // Moments - Categories.
                var moments = await GetMoments();
                UpdateSynchro(new SynchroInfo("Categorías", moments != null));
                if (moments != null)
                {
                    DB.DeleteAll<Moment>();
                    foreach (var item in moments) { DB.InsertItemWithID(item); }
                }

                // Questions.
                var questions = await GetQuestionAsync();
                UpdateSynchro(new SynchroInfo("Preguntas", questions != null));
                if (questions != null)
                {
                    DB.DeleteAll<Question>();
                    foreach (var item in questions) { DB.InsertItemWithID(item); }
                }

                // QuestionOptions - NOT IN USE.
                var questionOptions = await GetQuestionOptionsAsync();
                UpdateSynchro(new SynchroInfo("Opciones de preguntas", questionOptions != null));
                if (questionOptions != null)
                {
                    DB.DeleteAll<QuestionOption>();
                    foreach (var item in questionOptions) { DB.InsertItemWithID(item); }
                }

                // Zones.
                var zones = await GetZonesAsync();
                UpdateSynchro(new SynchroInfo("Zonas", zones != null));
                if (zones != null)
                {
                    DB.DeleteAll<Zone>();
                    foreach (var item in zones) { DB.InsertItemWithID(item); }
                }

                // Tasks.
                var tasks = await GetTasksAsync();
                UpdateSynchro(new SynchroInfo("Tareas", tasks != null));
                if (tasks != null)
                {
                    DB.DeleteAll<Task>();
                    foreach (var item in tasks) { DB.InsertItemWithID(item); }
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert("Sincronización", SynchroInfoList.Any(x => !x.Success) ?
                        "Uno o más elementos han fallado." : "La sincronización ha finalizado con éxito.", "OK");

                    App.Current.MainPage = new NavigationPage(new encuesta.Vistas.nuevaencuesta());
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

        public async Task<List<Class>> GetClassesAsync()
        {
            var httpClient = new HttpClient();

            try
            {
                var uri = new Uri("http://s-tmkt.com/dev/encuesta/app/GetClasses.php");
                var jsonResponse = httpClient.GetAsync(uri).Result;
                var jsonString = jsonResponse.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<Class>>(jsonString.Result);

                return list;
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", e.Message, "OK");
            }

            return null;
        }


        public async Task<List<Salesmen>> GetSalesmenAsync()
        {
            var httpClient = new HttpClient();

            try
            {
                var uri = new Uri("http://s-tmkt.com/dev/encuesta/app/GetClasses.php");
                var jsonResponse = httpClient.GetAsync(uri).Result;
                var jsonString = jsonResponse.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<Salesmen>>(jsonString.Result);

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


        public async Task<List<SubGroupQuestion>> GetSubGroupQuestionsAsync()
        {
            var httpClient = new HttpClient();

            try
            {
                var uri = new Uri("http://s-tmkt.com/dev/encuesta/app/GetSubGroupQuestions.php");
                var jsonResponse = httpClient.GetAsync(uri).Result;
                var jsonString = jsonResponse.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<SubGroupQuestion>>(jsonString.Result);

                return list;
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", e.Message, "OK");
            }

            return null;
        }

        public async Task<List<Zone>> GetZonesAsync()
        {
            var httpClient = new HttpClient();

            try
            {
                var uri = new Uri("http://s-tmkt.com/dev/encuesta/app/GetZones.php");
                var jsonResponse = httpClient.GetAsync(uri).Result;
                var jsonString = jsonResponse.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<Zone>>(jsonString.Result);

                return list;
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", e.Message, "OK");
            }

            return null;
        }

        public async Task<List<Task>> GetTasksAsync()
        {
            var httpClient = new HttpClient();

            try
            {
                var uri = new Uri("http://s-tmkt.com/dev/encuesta/app/GetTasks.php");
                var jsonResponse = httpClient.GetAsync(uri).Result;
                var jsonString = jsonResponse.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<Task>>(jsonString.Result);

                return list;
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", e.Message, "OK");
            }

            return null;
        }


        public async Task<List<Group>> GetGroupsAsync()
        {
            var httpClient = new HttpClient();

            try
            {
                var uri = new Uri("http://s-tmkt.com/dev/encuesta/app/GetGroups.php");
                var jsonResponse = httpClient.GetAsync(uri).Result;
                var jsonString = jsonResponse.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<Group>>(jsonString.Result);

                return list;
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", e.Message, "OK");
            }

            return null;
        }

        public async Task<List<SubGroup>> GetSubGroupsAsync()
        {
            var httpClient = new HttpClient();

            try
            {
                var uri = new Uri("http://s-tmkt.com/dev/encuesta/app/GetSubGroups.php");
                var jsonResponse = httpClient.GetAsync(uri).Result;
                var jsonString = jsonResponse.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<SubGroup>>(jsonString.Result);

                return list;
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", e.Message, "OK");
            }

            return null;
        }

        public async Task<List<SurveyGroup>> GetSurveyGroupsAsync()
        {
            var httpClient = new HttpClient();

            try
            {
                var uri = new Uri("http://s-tmkt.com/dev/encuesta/app/GetSurveyGroups.php");
                var jsonResponse = httpClient.GetAsync(uri).Result;
                var jsonString = jsonResponse.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<SurveyGroup>>(jsonString.Result);

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