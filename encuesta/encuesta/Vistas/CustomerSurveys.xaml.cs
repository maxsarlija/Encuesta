
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace encuesta.Vistas
{
    public partial class CustomerSurveys : ContentPage
    {
        private List<Survey> _customers;


        public CustomerSurveys()
        {
            InitializeComponent();

            var database = new Database("Encuesta");

            
        }
        
    }
}