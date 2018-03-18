using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using encuesta.Dominio.Enum;

namespace encuesta.Vistas
{
    public partial class SalesmenList : ContentPage
    {
        private string _searchedText;
        public string SearchedText
        {
            get { return _searchedText; }
            set { _searchedText = value; OnPropertyChanged(); }
        }

        protected Database DB { get; set; }
        private ObservableCollection<Salesman> _salesmenCollection;

        protected ObservableCollection<Salesman> SalesmenCollection
        {
            get
            {
                return _salesmenCollection;
            }
            set
            {
                _salesmenCollection = value;
                OnPropertyChanged();
            }
        }

        protected class Salesman
        {
            protected Database DB = new Database("Encuesta");

            public Salesman(User _user)
            {
                User = _user;
                Name = _user.Name;
                Zone = DB.GetItems<Zone>().Where(x => x.ID == _user.ZoneID).FirstOrDefault().Name;
            }

            public User User { get; set; }
            public string Name { get; set; }
            public string Zone { get; set; }
        }

        public SalesmenList()
        {
            InitializeComponent();


            DB = new Database("Encuesta");


            SalesmenCollection = new ObservableCollection<Salesman>();
            GetDefaultSalesmen();
        }


        async void BtnSalesman_OnClickItem(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            // Click on Customer will lead to his surveys.
            var _selectedSalesman = ((Salesman)e.SelectedItem).User;

            await Navigation.PushAsync(new Vistas.CustomersList(_selectedSalesman));

            ((ListView)sender).SelectedItem = null;
        }


        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            SalesmenListView.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                SalesmenCollection.Clear();
                GetDefaultSalesmen();
            }
            else
            {
                SalesmenCollection.Clear();

                // Check if there are any customers for this salesman.
                var UserIsSupervisor = App.User.ClassID == UserClass.SUPERVISOR;

                // If there are any salesmen for this supervisor, show them. If not, just show the current user as the salesman. 
                var _supervisorSalesmen = DB.GetItems<Salesmen>().Where(x => x.SupervisorID.Equals(App.User.ID));
                var _salesmenList = new List<User>();
                if (UserIsSupervisor)
                {
                    foreach (var item in _supervisorSalesmen)
                    {
                        _salesmenList.Add(DB.GetItems<User>().Where(x => x.ID == item.SalesmanID && x.Name.ToLower().Contains(e.NewTextValue.ToLower())).FirstOrDefault());
                    }
                }
                else
                {
                    foreach (var item in DB.GetItems<User>().Where(x => x.ID == App.User.ID && x.Name.ToLower().Contains(e.NewTextValue.ToLower())))
                    {
                        _salesmenList.Add(item);
                    }
                }



                foreach (var item in _salesmenList)
                {
                    if (item != null)
                        SalesmenCollection.Add(new Salesman(item));
                }

                SalesmenListView.ItemsSource = SalesmenCollection;
            }

            SalesmenListView.EndRefresh();
        }

        protected void GetDefaultSalesmen()
        {
            // Check if there are any customers for this salesman.
            var UserIsSupervisor = App.User.ClassID == UserClass.SUPERVISOR;

            // If there are any salesmen for this supervisor, show them. If not, just show the current user as the salesman. 
            var _supervisorSalesmen = DB.GetItems<Salesmen>().Where(x => x.SupervisorID.Equals(App.User.ID));
            var _salesmenList = new List<User>();
            if (UserIsSupervisor)
            {
                foreach (var item in _supervisorSalesmen)
                {
                    _salesmenList.Add(DB.GetItems<User>().Where(x => x.ID == item.SalesmanID).FirstOrDefault());
                }
            }
            else
            {
                foreach (var item in DB.GetItems<User>().Where(c => c.ID == App.User.ID))
                {
                    _salesmenList.Add(item);
                }
            }


            foreach (var item in _salesmenList)
            {
                SalesmenCollection.Add(new Salesman(item));
            }

            SalesmenListView.ItemsSource = SalesmenCollection;

        }
    }
}