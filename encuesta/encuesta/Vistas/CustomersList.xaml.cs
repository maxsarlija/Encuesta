using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.ComponentModel;
using System;

namespace encuesta.Vistas
{
    public partial class CustomersList : ContentPage, INotifyPropertyChanged
    {
        private string _searchedText;
        public string SearchedText
        {
            get { return _searchedText; }
            set { _searchedText = value; OnPropertyChanged(); }
        }

        protected User CurrentSalesman { get; set; }
        protected Database DB { get; set; }
        private ObservableCollection<Customer> _customerCollection;

        protected ObservableCollection<Customer> CustomerCollection
        {
            get
            {
                return _customerCollection;
            }
            set
            {
                _customerCollection = value;
                OnPropertyChanged();
            }
        }


        public CustomersList(User _salesman)
        {
            InitializeComponent();

            DB = new Database("Encuesta");
            CurrentSalesman = _salesman;

            CustomerCollection = new ObservableCollection<Customer>();
            GetDefaultCustomers();
        }


        async void BtnCliente_OnClickItem(object sender, SelectedItemChangedEventArgs e)
        {
            // Click on Customer will lead to his surveys.
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            await Navigation.PushAsync(new Vistas.CustomerSurveys((Customer)e.SelectedItem));

            ((ListView)sender).SelectedItem = null;
        }


        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            CustomersListView.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                CustomerCollection.Clear();
                GetDefaultCustomers();
            }
            else
            {
                CustomerCollection.Clear();
                // Check if there are any customers for this salesman.
                var SalesmanHasCustomers = DB.GetItems<Customer>().Where(c => c.SalesmanID == CurrentSalesman.ID).Count() > 0;

                // If there are any customer for the salesmen, show them. If not, just show the ones for the zone of the User. 
                var _customersList = SalesmanHasCustomers ? DB.GetItems<Customer>().Where(c => c.SalesmanID == CurrentSalesman.ID &&
                                                                    (c.Name.ToLower().Contains(e.NewTextValue.ToLower()) ||
                                                                     c.Address.ToLower().Contains(e.NewTextValue.ToLower())))
                                                          : DB.GetItems<Customer>().Where(c => c.ZoneID == App.User.ZoneID &&
                                                                    (c.Name.ToLower().Contains(e.NewTextValue.ToLower()) ||
                                                                     c.Address.ToLower().Contains(e.NewTextValue.ToLower())));

                foreach (var item in _customersList)
                {
                    CustomerCollection.Add(item);
                }

                CustomersListView.ItemsSource = CustomerCollection;
            }

            CustomersListView.EndRefresh();
        }

        protected void GetDefaultCustomers()
        {

            // Check if there are any customers for this salesman.
            var SalesmanHasCustomers = DB.GetItems<Customer>().Where(c => c.SalesmanID == CurrentSalesman.ID).Count() > 0;

            // If there are any customer for the salesmen, show them. If not, just show the ones for the zone of the User. 
            var _customersList = SalesmanHasCustomers ? DB.GetItems<Customer>().Where(c => c.SalesmanID == CurrentSalesman.ID)
                                                      : DB.GetItems<Customer>().Where(c => c.ZoneID == App.User.ZoneID);

            foreach (var item in _customersList)
            {
                CustomerCollection.Add(item);
            }

            CustomersListView.ItemsSource = CustomerCollection;
        }


    }
}