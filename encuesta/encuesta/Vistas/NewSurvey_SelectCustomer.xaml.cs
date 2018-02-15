using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace encuesta.Vistas
{
    public partial class NewSurvey_SelectCustomer : ContentPage
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

        public NewSurvey_SelectCustomer()
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
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            // Click on Customer will lead to his surveys.
            var _selectedCustomer = (Customer) e.SelectedItem;

            await Navigation.PushAsync(new Vistas.NewSurvey_SelectSurvey(_selectedCustomer));

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