using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using encuesta.Dominio.Enum;

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

        public NewSurvey_SelectCustomer(User _salesman)
        {
            InitializeComponent();

            DB = new Database("Encuesta");
            CurrentSalesman = _salesman;

            CustomerCollection = new ObservableCollection<Customer>();
            GetDefaultCustomers();
        }


        async void BtnCliente_OnClickItem(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            // Click on Customer will lead to the available surveys.
            var _selectedCustomer = (Customer)e.SelectedItem;

            // Check if the matinal plan survey has been done.
            var matinalPlanIsDone = DB.Query<CustomerAnswer>("SELECT CA.* " +
                                                            "FROM CustomerAnswer CA " +
                                                            "LEFT OUTER JOIN Survey S ON S.ID = CA.SurveyID " +
                                                            "WHERE S.PlanGold = 2").Count() > 0;

            if (matinalPlanIsDone)
            {
                await Navigation.PushAsync(new Vistas.NewSurvey_SelectSurvey(_selectedCustomer, CurrentSalesman));
            }
            else
            {
                // Select the survey for matinal plan.
                Survey _selectedSurvey = DB.GetItems<Survey>().Where(x => x.PlanGold == Plan.PLAN_MATINAL).FirstOrDefault();
                DB.SaveItem(new CustomerAnswer(_selectedCustomer.ID, _selectedSurvey.ID, App.UserName));

                // Create the list of questions, and the CustomerAnswer row.
                var _customerAnswer = DB.Query<CustomerAnswer>("SELECT * FROM CustomerAnswer WHERE CustomerID = ? ORDER BY ID DESC LIMIT 1", _selectedCustomer.ID).FirstOrDefault();
                string sql = "SELECT Q.* " +
                            "FROM  SubGroupQuestion Q " +
                            "LEFT OUTER JOIN SubGroup SG ON Q.SubGroupID = SG.ID " +
                            "LEFT OUTER JOIN SurveyGroup S ON SG.GroupID = S.GroupID " +
                            "WHERE S.SurveyID = ? " +
                            "ORDER BY Q.QuestionOrder;";

                var _surveyQuestions = DB.Query<SubGroupQuestion>(sql, _selectedSurvey.ID);



                foreach (var sq in _surveyQuestions)
                {
                    var _answer = new Answer(_customerAnswer.ID, sq.QuestionID);
                    DB.SaveItem(_answer);
                }

                await Navigation.PushAsync(new Vistas.NewSurvey_Questions(_customerAnswer, _selectedSurvey, _selectedCustomer));
            }


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