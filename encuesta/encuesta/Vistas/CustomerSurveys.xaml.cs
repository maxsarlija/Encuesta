using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using encuesta.Dominio.Enum;
using System;
using Plugin.Share;
using Plugin.Share.Abstractions;
using System.Collections.ObjectModel;

namespace encuesta.Vistas
{
    public partial class CustomerSurveys : ContentPage
    {

        protected Database DB { get; set; }
        public Customer SelectedCustomer { get; set; }
        private ObservableCollection<CustomerSurvey> _surveyCollection;

        protected ObservableCollection<CustomerSurvey> SurveyCollection
        {
            get
            {
                return _surveyCollection;
            }
            set
            {
                _surveyCollection = value;
                OnPropertyChanged();
            }
        }

        public CustomerSurveys(Customer _customer)
        {
            InitializeComponent();

            DB = new Database("Encuesta");

            SelectedCustomer = _customer;
            Title = "Encuestas - " + SelectedCustomer.Name;
            SurveyCollection = new ObservableCollection<CustomerSurvey>();

            GetDefaultSurveys(SelectedCustomer);
        }

        protected class CustomerSurvey
        {
            public CustomerSurvey(CustomerAnswer _customerAnswer)
            {
                var db = new Database("Encuesta");
                CustomerAnswer = _customerAnswer;
                Survey = db.Query<Survey>("SELECT * FROM Survey WHERE ID = ?", CustomerAnswer.SurveyID).FirstOrDefault();
                Customer = db.Query<Customer>("SELECT * FROM Customer WHERE ID = ?", CustomerAnswer.CustomerID).FirstOrDefault();

                switch (CustomerAnswer.Status)
                {
                    case SurveyStatus.COMPLETED:
                        StatusColor = Survey != null ? Color.Green : Color.Red;
                        break;
                    case SurveyStatus.PENDING:
                        StatusColor = Survey != null ? Color.Orange : Color.Red;
                        break;
                    default:
                        StatusColor = Color.Red;
                        break;
                }

                Date = _customerAnswer.DateCompleted == DateTime.MinValue ? String.Empty : _customerAnswer.DateCompleted.ToString();
                Status = Survey != null ? _customerAnswer.Status : "N/A";
                SurveyName = Survey != null ? Survey.Name : "No disponible";
            }
            public CustomerAnswer CustomerAnswer { get; set; }
            public Survey Survey { get; set; }
            public Customer Customer { get; set; }
            public Color StatusColor { get; set; }
            public string Date { get; set; }
            public string Status { get; set; }
            public string SurveyName { get; set; }

        }

        async void BtnSurvey_OnClickItem(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            CustomerSurvey _selectedSurvey = (CustomerSurvey)e.SelectedItem;

            var action = await DisplayActionSheet("Encuesta - Acciones", "Cancelar", null, "Ver Encuesta", "Compartir", "Eliminar");

            switch (action)
            {
                case "Ver Encuesta":
                    ViewSurvey(_selectedSurvey);
                    break;
                case "Compartir":
                    ShareSurvey(_selectedSurvey);
                    break;
                case "Eliminar":
                    DeleteSurvey(_selectedSurvey);
                    break;
                default:
                    break;
            }


            ((ListView)sender).SelectedItem = null;
        }

        // Share results of a survey.
        protected async void ShareSurvey(CustomerSurvey _selectedSurvey)
        {
            if (!CrossShare.IsSupported)
            {
                await DisplayAlert("Error", "Este dispositivo no soporta la función 'Compartir Encuesta'.", "OK");
                return;
            }

            if (_selectedSurvey.Survey == null)
            {
                await DisplayAlert("Error", "Esta encuesta no se encuestra disponible en el sistema.", "OK");
                return;
            } else
            {
                var message = "";
                var _answers = DB.Query<Answer>("SELECT * FROM Answer WHERE CustomerAnswerID = ? ", _selectedSurvey.CustomerAnswer.ID);

                // Get all CustomerAnswers and populate list with survey and customer data.
                foreach (var item in _answers)
                {
                    var question = DB.Query<Question>("SELECT * FROM Question WHERE ID = ?", item.QuestionID).FirstOrDefault().ShortDescription;
                    message += "Pregunta: " + question;
                    message += System.Environment.NewLine;
                    message += "Respuesta: " + item.Option;
                    message += System.Environment.NewLine;
                    message += System.Environment.NewLine;
                }


                await CrossShare.Current.Share(new ShareMessage
                {
                    Title = _selectedSurvey.Survey.Name,
                    Text = message
                });
            }
                
        }

        // Navigates to view the survey, or alerts customer that survey is broken.
        protected async void ViewSurvey(CustomerSurvey _selectedSurvey)
        {
            if (_selectedSurvey.Survey != null)
            {
                await Navigation.PushAsync(new Vistas.CustomerSurveyAnswers(_selectedSurvey.CustomerAnswer, _selectedSurvey.Survey, _selectedSurvey.Customer));
            }
            else
            {
                await DisplayAlert("Error", "Esta encuesta ya no se encuentra disponible en el sistema.", "OK");
            }
        }

        // Function to delete surveys.
        // We eliminate both its answers and the CustomerAnswer header.
        protected async void DeleteSurvey(CustomerSurvey _selectedSurvey)
        {
            var actionSheet = await DisplayActionSheet("¿Realmente desea eliminar esta encuesta?", null, null, "Eliminar", "Cancelar");
            if (actionSheet == "Eliminar")
            {
                var answers = DB.GetItems<Answer>().Where(x => x.CustomerAnswerID == _selectedSurvey.CustomerAnswer.ID);
                foreach (var item in answers)
                {
                    DB.DeleteItem<Answer>(item.ID);
                }

                DB.DeleteItem<CustomerAnswer>(_selectedSurvey.CustomerAnswer.ID);
                await DisplayAlert("Alerta", "Encuesta eliminada exitosamente", "OK");
                CustomerAnswers.BeginRefresh();
                SurveyCollection.Clear();
                GetDefaultSurveys(SelectedCustomer);
                CustomerAnswers.EndRefresh();
            }
        }

        protected void GetDefaultSurveys(Customer _customer)
        {
            // Initialize new items source list.
            var _customerAnswers = DB.Query<CustomerAnswer>("SELECT * FROM CustomerAnswer WHERE CustomerID = ?", _customer.ID);

            // Get all CustomerAnswers and populate list with survey and customer data.
            foreach (var item in _customerAnswers)
            {
                SurveyCollection.Add(new CustomerSurvey(item));
            }

            // Assign CustomerSurveys as ListView ItemsSource.
            CustomerAnswers.ItemsSource = _surveyCollection;
        }

    }
}