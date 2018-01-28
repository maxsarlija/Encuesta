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
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            // Click on Customer will lead to his surveys.
            var _selectedCustomer = (Customer) e.SelectedItem;

            await Navigation.PushAsync(new Vistas.NewSurvey_SelectSurvey(_selectedCustomer));

            ((ListView)sender).SelectedItem = null;
        }

        

    }
}