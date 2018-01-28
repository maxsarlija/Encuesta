using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace encuesta.Vistas
{
    public partial class Synchronization : ContentPage
    {
        protected Database DB { get; set; }

        private HttpClient mClient;
        private Uri mUri;

        public Synchronization()
        {
            InitializeComponent();

            var _users = new List<User>();

            mClient = new HttpClient();

            SyncListView.ItemsSource = GetUsersAsync();
            var users = GetUsersAsync();
            var customers = GetCustomersAsync();
            var surveys = GetSurveysAsync();
            var surveyQuestions = GetSurveyQuestionsAsync();
            var moments = GetMoments();
            var questions = GetQuestionAsync();
            var questionOptions = GetQuestionOptionsAsync();
            
            //mClient.
        }

        public List<User> GetUsersAsync()
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
                DisplayAlert("Error", e.Message, "OK");
            }

            return null;
        }


        public List<Customer> GetCustomersAsync()
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
                DisplayAlert("Error", e.Message, "OK");
            }

            return null;
        }


        public List<Survey> GetSurveysAsync()
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
                DisplayAlert("Error", e.Message, "OK");
            }

            return null;
        }


        public List<Moment> GetMoments()
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
                DisplayAlert("Error", e.Message, "OK");
            }

            return null;
        }

        public List<Question> GetQuestionAsync()
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
                DisplayAlert("Error", e.Message, "OK");
            }

            return null;
        }


        public List<QuestionOption> GetQuestionOptionsAsync()
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
                DisplayAlert("Error", e.Message, "OK");
            }

            return null;
        }


        public List<SurveyQuestion> GetSurveyQuestionsAsync()
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
                DisplayAlert("Error", e.Message, "OK");
            }

            return null;
        }
    }
}