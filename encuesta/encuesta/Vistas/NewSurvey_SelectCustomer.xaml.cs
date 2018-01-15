using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace encuesta.Vistas
{
    public partial class NewSurvey_SelectCustomer : ContentPage
    {
        private List<Customer> _customers;


        public NewSurvey_SelectCustomer()
        {
            InitializeComponent();

            var database = new Database("Encuesta");


            _customers = database.Query<Customer>("SELECT * FROM Customer");
            CustomersListView.ItemsSource = _customers;
        }


        async void BtnCliente_OnClickItem(object sender, SelectedItemChangedEventArgs e)
        {
            // Click on Customer will lead to his surveys.
            // await Navigation.PushAsync(new Vistas.CustomerProfile((Customer)e.SelectedItem));

            await Navigation.PushAsync(new Vistas.NewSurvey_SelectSurvey((Customer)e.SelectedItem));
        }


    }
}