
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


        async void BtnCliente_OnClickItem(object sender, SelectedItemChangedEventArgs e)
        {
            // Click on Customer will lead to his surveys.
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            await Navigation.PushAsync(new Vistas.CustomerSurveys((Customer) e.SelectedItem));

            ((ListView)sender).SelectedItem = null;
        }



    }
}