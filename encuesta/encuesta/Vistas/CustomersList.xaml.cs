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


        public CustomersList()
        {
            InitializeComponent();

            DB = new Database("Encuesta");


            CustomerCollection = new ObservableCollection<Customer>();

            foreach (var item in DB.GetItems<Customer>())
            {
                CustomerCollection.Add(item);
            }

            CustomersListView.ItemsSource = CustomerCollection;
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
                CustomersListView.ItemsSource = CustomerCollection;
            else
            {
                CustomerCollection.Clear();
                foreach (var item in DB.GetItems<Customer>().Where(c => c.Name.ToLower().Contains(e.NewTextValue.ToLower()) || 
                                                                        c.Address.ToLower().Contains(e.NewTextValue.ToLower())))
                {
                    CustomerCollection.Add(item);
                }

                CustomersListView.ItemsSource = CustomerCollection;
            }

            CustomersListView.EndRefresh();
        }


    }
}