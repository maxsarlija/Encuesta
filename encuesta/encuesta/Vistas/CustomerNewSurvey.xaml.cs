
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace encuesta.Vistas
{
    public partial class CustomerNewSurvey : ContentPage
    {
        private List<Customer> _customers;


        public CustomerNewSurvey()
        {
            InitializeComponent();

            var database = new Database("Encuesta");

            
        }
        

    }
}