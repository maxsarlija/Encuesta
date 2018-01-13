
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
            await Navigation.PushAsync(new Vistas.CustomerProfile((Customer) e.SelectedItem));

        }

        // Button start Surveys.
        async void BtnStartSurvey_OnClick(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Vistas.CustomersList());
        }

        // Button view Surveys.
        async void BtnViewSurveys_OnClick(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Vistas.CustomersList());
        }



    }
}