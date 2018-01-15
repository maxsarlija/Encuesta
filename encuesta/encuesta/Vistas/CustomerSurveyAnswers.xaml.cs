using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using encuesta.Dominio.Enum;

namespace encuesta.Vistas
{
    public partial class CustomerSurveyAnswers : ContentPage
    {

        protected Database DB { get; set; }
        public Customer SelectedCustomer { get; set; }
        public Survey SelectedSurvey { get; set; }
        public CustomerAnswer SelectedCustomerAnswer { get; set; }


        public CustomerSurveyAnswers(CustomerAnswer _customerAnswer, Survey _survey, Customer _customer)
        {
            InitializeComponent();

            DB = new Database("Encuesta");

            SelectedCustomerAnswer = _customerAnswer;
            SelectedCustomer = _customer;
            SelectedSurvey = _survey;


            // Initialize new items source list.
            var _customerSurveyAnswers = new List<CustomerSurveyAnswer>();
            var _answers = DB.Query<Answer>("SELECT * FROM Answer WHERE CustomerAnswerID = ? ", SelectedCustomerAnswer.ID);

            // Get all CustomerAnswers and populate list with survey and customer data.
            foreach (var item in _answers)
            {
                _customerSurveyAnswers.Add(new CustomerSurveyAnswer(item));
            }

            // Assign CustomerSurveys as ListView ItemsSource.
            CustomerAnswers.ItemsSource = _customerSurveyAnswers;
        }

        protected class CustomerSurveyAnswer
        {
            public CustomerSurveyAnswer(Answer _answer)
            {
                var db = new Database("Encuesta");
                Answer = _answer;
                Question = db.Query<Question>("SELECT * FROM Question WHERE ID = ?", Answer.QuestionID).FirstOrDefault();
                StatusColor = Answer.Option == AnswerOptions.YES ? Color.Green : Color.Red;
            }

            public Answer Answer { get; set; }
            public Question Question { get; set; }
            public Color StatusColor { get; set; }

        }

        // *********************************************
        // USAR ESTO PARA VER PREGUNTAS INDIVIDUALMENTE
        // *********************************************

        /* async void BtnAnswer_OnClickItem(object sender, SelectedItemChangedEventArgs e)
        {
            CustomerSurveyAnswer _selectedSurveyAnswer = (CustomerSurveyAnswer)e.SelectedItem;
            

            // TODO: REVISAR MECANISMO PARA MOSTRAR ENCUESTA
            // PASAR CURRENT INDEX A LA VISTA
            int currentIndex = 0;

            await Navigation.PushAsync(new Vistas.EncuestaView(SelectedCustomer, SelectedSurvey, SelectedCustomerAnswer, currentIndex));
        }*/

    }
}