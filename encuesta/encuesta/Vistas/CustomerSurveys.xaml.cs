using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using encuesta.Dominio.Enum;
using System;

namespace encuesta.Vistas
{
    public partial class CustomerSurveys : ContentPage
    {

        protected Database DB { get; set; }
        public Customer SelectedCustomer { get; set; }


        public CustomerSurveys(Customer _customer)
        {
            InitializeComponent();

            DB = new Database("Encuesta");
            
            SelectedCustomer = _customer;
            Title = "Encuestas - " + SelectedCustomer.Name;


            // Initialize new items source list.
            var _customerSurveys = new List<CustomerSurvey>();
            var _customerAnswers = DB.Query<CustomerAnswer>("SELECT * FROM CustomerAnswer WHERE CustomerID = ?", _customer.ID);

            // Get all CustomerAnswers and populate list with survey and customer data.
            foreach (var item in _customerAnswers)
            {
                _customerSurveys.Add(new CustomerSurvey(item));
            }

            // Assign CustomerSurveys as ListView ItemsSource.
            CustomerAnswers.ItemsSource = _customerSurveys;
        }

        protected class CustomerSurvey
        {
            public CustomerSurvey(CustomerAnswer _customerAnswer) 
            {
                var db = new Database("Encuesta");
                CustomerAnswer = _customerAnswer;
                Survey = db.Query<Survey>("SELECT * FROM Survey WHERE ID = ?", CustomerAnswer.SurveyID).FirstOrDefault();
                Customer = db.Query<Customer>("SELECT * FROM Customer WHERE ID = ?", CustomerAnswer.CustomerID).FirstOrDefault();
                StatusColor = CustomerAnswer.Status == SurveyStatus.COMPLETED ? Color.Green : Color.Orange;
                Date = _customerAnswer.DateCompleted == DateTime.MinValue ? String.Empty : _customerAnswer.DateCompleted.ToString();
            }
            public CustomerAnswer CustomerAnswer { get; set; }
            public Survey Survey { get; set; }
            public Customer Customer { get; set; }
            public Color StatusColor { get; set; }
            public string Date { get; set; }
            
        }

        async void BtnSurvey_OnClickItem(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            CustomerSurvey _selectedSurvey = (CustomerSurvey)e.SelectedItem;

            await Navigation.PushAsync(new Vistas.CustomerSurveyAnswers(_selectedSurvey.CustomerAnswer, _selectedSurvey.Survey, _selectedSurvey.Customer));

            ((ListView)sender).SelectedItem = null;
        }

    }
}