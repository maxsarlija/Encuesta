using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace encuesta.Vistas
{
    public partial class CustomerNewSurvey : ContentPage
    {
        public Customer SelectedCustomer { get; set; }
        protected Database DB { get; set; }


        public CustomerNewSurvey(Customer _customer)
        {
            InitializeComponent();

            SelectedCustomer = _customer;

            DB = new Database("Encuesta");
            var _surveys = DB.GetItems<Survey>();
            
            SurveysListView.ItemsSource = _surveys;
        }
        

        async void BtnSurvey_OnClickItem(object sender, SelectedItemChangedEventArgs e)
        {
            Survey _selectedSurvey = (Survey)e.SelectedItem;
            DB.SaveItem(new CustomerAnswer(SelectedCustomer.ID, _selectedSurvey.ID));

            var _customerAnswer = DB.Query<CustomerAnswer>("SELECT * FROM CustomerAnswer ORDER BY ID DESC LIMIT 1").FirstOrDefault();
            var _surveyQuestions = DB.Query<SurveyQuestion>("SELECT * FROM SurveyQuestion ORDER BY QuestionNumber");


            foreach (var sq in _surveyQuestions)
            {
                DB.SaveItem(new Answer(_customerAnswer.ID, sq.ID));
            }
            
            // PASAR CURRENT INDEX A LA VISTA
            int currentIndex = 0;

            await Navigation.PushAsync(new Vistas.EncuestaView(SelectedCustomer, _selectedSurvey, _customerAnswer, currentIndex));
        }


    }
}