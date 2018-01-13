using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using encuesta.Dominio.Enum;

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
            }
            public CustomerAnswer CustomerAnswer { get; set; }
            public Survey Survey { get; set; }
            public Customer Customer { get; set; }
            public Color StatusColor { get; set; }
            
        }

    }
}