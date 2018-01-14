using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using encuesta.Dominio.Enum;
using System;

namespace encuesta.Vistas
{
    public partial class EncuestaView : ContentPage
    {
        public Customer SelectedCustomer { get; set; }
        public Survey SelectedSurvey { get; set; }
        public int CurrentQuestionNumber { get; set; }
        protected List<SurveyQuestion> SurveyQuestions { get; set; }
        public Question CurrentQuestion { get; set; }
        public CustomerAnswer CurrentCustomerAnswer { get; set; }

        public Boolean QuestionWithoutAnswer { get; set; }
        public Boolean ShowAnswerContainer { get; set; }


        protected Database DB { get; set; }

        public EncuestaView(Customer _customer, Survey _survey, CustomerAnswer _customerAnswer, int _currentIndex)
        {
            InitializeComponent();

            DB = new Database("Encuesta");

            SelectedCustomer = _customer;
            SelectedSurvey = _survey;
            SurveyQuestions = DB.Query<SurveyQuestion>("SELECT * FROM SurveyQuestion WHERE SurveyID = ? ORDER BY QuestionNumber", SelectedSurvey.ID); ;
            CurrentQuestionNumber = _currentIndex;
            CurrentCustomerAnswer = _customerAnswer;

            CurrentQuestion = DB.Query<Question>("SELECT * FROM Question WHERE ID = ?", SurveyQuestions.Where(x => x.QuestionNumber == CurrentQuestionNumber).FirstOrDefault().ID).FirstOrDefault();

            Title = SelectedCustomer.Name + " - " + SelectedSurvey.Name;
            SurveyTitle.Text = DB.Query<Moment>("SELECT * FROM Moment").Where(x => x.Category == CurrentQuestion.Moment).FirstOrDefault().Description;
            SurveyQuestion.Text = CurrentQuestion.Details;

            QuestionWithoutAnswer = _customerAnswer.Status == SurveyStatus.PENDING ? true : false;
            ShowAnswerContainer = !QuestionWithoutAnswer;

            BtnYes.IsEnabled = QuestionWithoutAnswer;
            BtnNo.IsEnabled = QuestionWithoutAnswer;
            QuestionWasAnsweredContainer.IsVisible = ShowAnswerContainer;

            if(!QuestionWithoutAnswer)
            {
                var _answer = DB.Query<Answer>("SELECT * FROM Answer WHERE CustomerAnswerID = ? AND QuestionID = ?", CurrentCustomerAnswer.ID, CurrentQuestion.ID).FirstOrDefault();
                QuestionWasAnswered.Text = "PREGUNTA RESPONDIDA. RESPUESTA: " + _answer.Option;
            }
        }



        async void BtnYes_OnClick(object sender, System.EventArgs e)
        {
            // Selecciono respuesta correspondiente para este cliente y survey, puntaje total de la pregunta.
            var test = DB.Query<Answer>("SELECT * FROM Answer WHERE CustomerAnswerID = ?", CurrentCustomerAnswer.ID);
            var _answer = DB.Query<Answer>("SELECT * FROM Answer WHERE CustomerAnswerID = ? AND QuestionID = ?", CurrentCustomerAnswer.ID, CurrentQuestion.ID).FirstOrDefault();
            _answer.Option = AnswerOptions.YES;
            _answer.Score = CurrentQuestion.Score;
            // Update answer and proceed.
            DB.SaveItem(_answer);
            await PushNextView();
        }

        async void BtnNo_OnClick(object sender, System.EventArgs e)
        {
            // Selecciono respuesta correspondiente para este cliente y survey, puntaje 0.
            var _answer = DB.Query<Answer>("SELECT * FROM Answer WHERE CustomerAnswerID = ?", CurrentCustomerAnswer.ID).FirstOrDefault();
            _answer.Option = AnswerOptions.NO;
            _answer.Score = 0;
            // Update answer and proceed.
            DB.SaveItem(_answer);
            await PushNextView();
        }

        async void BtnNext_OnClick(object sender, System.EventArgs e)
        {
            await PushNextView();
        }

        protected async Task PushNextView()
        {
            // Compare the question number of the last number with the current index.
            // If it's the same, that means it's the end of the survey.
            var lastSurveyQuestion = DB.Query<SurveyQuestion>("SELECT * FROM SurveyQuestion WHERE SurveyID = ? ORDER BY QuestionNumber DESC", SelectedSurvey.ID).FirstOrDefault();

            if (lastSurveyQuestion.QuestionNumber == CurrentQuestionNumber)
            {
                CurrentCustomerAnswer.DateCompleted = DateTime.Now;
                CurrentCustomerAnswer.Status = SurveyStatus.COMPLETED;
                DB.SaveItem(CurrentCustomerAnswer);
                await Navigation.PushAsync(new Vistas.CustomerProfile(SelectedCustomer));
            }
            else
            // If it's different, we go to the next question.
            {
                CurrentQuestionNumber++;
                await Navigation.PushAsync(new Vistas.EncuestaView(SelectedCustomer, SelectedSurvey, CurrentCustomerAnswer, CurrentQuestionNumber));
            }
        }


    }
}