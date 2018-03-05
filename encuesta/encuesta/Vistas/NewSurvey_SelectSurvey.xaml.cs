using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using encuesta.Dominio.Enum;

namespace encuesta.Vistas
{
    public partial class NewSurvey_SelectSurvey : ContentPage
    {
        public Customer SelectedCustomer { get; set; }
        public User SelectedSalesman { get; set; }
        protected Database DB { get; set; }


        public NewSurvey_SelectSurvey(Customer _customer, User _salesman)
        {
            InitializeComponent();

            SelectedCustomer = _customer;
            SelectedSalesman = _salesman;

            Title = "Encuesta - " + SelectedCustomer.Name;

            DB = new Database("Encuesta");
            // Check if the matinal plan has been done already.
            var matinalPlanIsDone = DB.Query<CustomerAnswer>("SELECT CA.* " +
                                                            "FROM CustomerAnswer CA " +
                                                            "LEFT OUTER JOIN Survey S ON S.ID = CA.SurveyID " +
                                                            "WHERE S.PlanGold = 2").Count() > 0;
            IEnumerable<Survey> _surveys;
            // If it has been done, only offer the other surveys to be completed.
            if (matinalPlanIsDone)
            {
                _surveys = DB.GetItems<Survey>().Where(x => SelectedCustomer.PlanGoldBool ? x.PlanGold != Plan.PLAN_MATINAL : 
                                                                                            x.PlanGold == Plan.PLAN);
            }
            else
            {
                // Check if the customer is Plan Gold.
                if(SelectedCustomer.PlanGoldBool)
                {
                    _surveys = DB.GetItems<Survey>();

                } else
                {
                    _surveys = DB.GetItems<Survey>().Where(x => x.PlanGold != Plan.PLAN_GOLD);
                }

            }

            SurveysListView.ItemsSource = _surveys;
        }


        async void BtnSurvey_OnClickItem(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            Survey _selectedSurvey = (Survey)e.SelectedItem;
            DB.SaveItem(new CustomerAnswer(SelectedCustomer.ID, _selectedSurvey.ID, SelectedSalesman.Username));

            // Create the list of questions, and the CustomerAnswer row.
            var _customerAnswer = DB.Query<CustomerAnswer>("SELECT * FROM CustomerAnswer WHERE CustomerID = ? ORDER BY ID DESC LIMIT 1", SelectedCustomer.ID).FirstOrDefault();
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

            await Navigation.PushAsync(new Vistas.NewSurvey_Questions(_customerAnswer, _selectedSurvey, SelectedCustomer));

            ((ListView)sender).SelectedItem = null;
        }


    }
}