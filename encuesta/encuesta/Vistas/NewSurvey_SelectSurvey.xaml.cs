using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace encuesta.Vistas
{
    public partial class NewSurvey_SelectSurvey : ContentPage
    {
        public Customer SelectedCustomer { get; set; }
        protected Database DB { get; set; }


        public NewSurvey_SelectSurvey(Customer _customer)
        {
            InitializeComponent();

            SelectedCustomer = _customer;

            Title = "Encuesta - " + SelectedCustomer.Name;

            DB = new Database("Encuesta");
            var _surveys = DB.GetItems<Survey>();
            
            SurveysListView.ItemsSource = _surveys;
        }
        

        async void BtnSurvey_OnClickItem(object sender, SelectedItemChangedEventArgs e)
        {
            Survey _selectedSurvey = (Survey)e.SelectedItem;
            DB.SaveItem(new CustomerAnswer(SelectedCustomer.ID, _selectedSurvey.ID));

            var _customerAnswer = DB.Query<CustomerAnswer>("SELECT * FROM CustomerAnswer WHERE CustomerID = ? ORDER BY ID DESC LIMIT 1", SelectedCustomer.ID).FirstOrDefault();
            var _surveyQuestions = DB.Query<SurveyQuestion>("SELECT * FROM SurveyQuestion WHERE SurveyID = ? ORDER BY QuestionNumber", _selectedSurvey.ID);


            foreach (var sq in _surveyQuestions)
            {
                var _answer = new Answer(_customerAnswer.ID, sq.QuestionID);
                DB.SaveItem(_answer);
            }
            
            await Navigation.PushAsync(new Vistas.NewSurvey_Questions(_customerAnswer, _selectedSurvey, SelectedCustomer));

        }


    }
}