﻿using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using encuesta.Dominio.Enum;
using System;

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

            Title = SelectedCustomer.Name + " - " + SelectedSurvey.Name;

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
                StatusColor = (Answer.Option == null || Answer.Option.Equals(string.Empty) || Answer.Option == AnswerOptions.PENDING)
                                    ? Color.Orange : (Answer.Option == AnswerOptions.YES ? Color.Green : Color.Red);
            }

            public Answer Answer { get; set; }
            public Question Question { get; set; }
            public Color StatusColor { get; set; }

        }

        // *********************************************
        // USAR ESTO PARA VER PREGUNTAS INDIVIDUALMENTE
        // *********************************************

        async void BtnAnswer_OnClickItem(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            CustomerSurveyAnswer _selectedSurveyAnswer = (CustomerSurveyAnswer)e.SelectedItem;

            if (_selectedSurveyAnswer.Answer.Option != AnswerOptions.PENDING)
                await DisplayAlert("Alerta", "Esta pregunta ha sido respondida.", "OK");
            else
            {
                var _answer = _selectedSurveyAnswer.Answer;
                var action = await DisplayActionSheet(_selectedSurveyAnswer.Question.Details, "Cancelar", null, AnswerOptions.NO, AnswerOptions.YES);
                switch (action)
                {
                    case AnswerOptions.NO:
                        SaveAnswer(_selectedSurveyAnswer.Answer, AnswerOptions.NO);
                        break;
                    case AnswerOptions.YES:
                        SaveAnswer(_selectedSurveyAnswer.Answer, AnswerOptions.YES);
                        break;
                    default:
                        break;
                }
            }

            ((ListView)sender).SelectedItem = null;
        }

        protected async void SaveAnswer(Answer _answer, string _option)
        {
            _answer.Option = _option;
            DB.SaveItem(_answer);

            var _customerSurveyAnswers = new List<CustomerSurveyAnswer>();
            var _answers = DB.Query<Answer>("SELECT * FROM Answer WHERE CustomerAnswerID = ? ", SelectedCustomerAnswer.ID);
            CustomerAnswers.ItemsSource = null;

            int i = 0;

            foreach (var item in _answers)
            {
                if (item.Option != AnswerOptions.PENDING)
                    i++;

                _customerSurveyAnswers.Add(new CustomerSurveyAnswer(item));
            }

            CustomerAnswers.ItemsSource = _customerSurveyAnswers;

            // If all the questions have answer, then we go to the main menu.
            if (_answers.Count == i)
            {

                SelectedCustomerAnswer.DateCompleted = DateTime.Now;
                SelectedCustomerAnswer.Status = SurveyStatus.COMPLETED;
                DB.SaveItem(SelectedCustomerAnswer);

                await DisplayAlert("Encuesta", "La encuesta ha finalizado.", "OK");
                await Navigation.PopToRootAsync();
            }

        }

    }
}