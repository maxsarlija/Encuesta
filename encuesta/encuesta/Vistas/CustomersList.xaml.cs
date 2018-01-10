
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace encuesta.Vistas
{
    public partial class CustomersList : ContentPage
    {
        private List<Customer> _customers;


        public CustomersList()
        {
            InitializeComponent();

            var database = new Database("Encuesta");

            
            _customers = database.Query<Customer>("SELECT * FROM Customer");
            CustomersListView.ItemsSource = _customers;
        }
        
    }
}