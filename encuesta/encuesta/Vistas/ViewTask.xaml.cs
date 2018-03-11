using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Plugin.Connectivity;
using System.Diagnostics;
using Plugin.LocalNotifications;

namespace encuesta.Vistas
{
    public partial class ViewTask : ContentPage
    {
        private encuesta.Tasks _currentTask;
        private TimeSpan _timeSpan;

        public bool TaskIsFinished { get; set; } = false;

        public TimeSpan Alarm 
        {
            get { return _timeSpan; } 
            set { _timeSpan = value; } 
        }

        public ViewTask(encuesta.Tasks _task)
        {
            _currentTask = _task;
            Alarm = _currentTask.Time.TimeOfDay;
            TaskIsFinished = _currentTask.Status == encuesta.Dominio.Enum.TaskStatus.COMPLETED;

            InitializeComponent();

            btnFinish.IsEnabled = !TaskIsFinished;
            btnSave.IsEnabled = !TaskIsFinished;

            TaskName.Text = _currentTask.Name;
            TaskDate.Text = _currentTask.Date.ToLongDateString();
            TaskDetails.Text = _currentTask.Details;
            TaskStatus.TextColor = _currentTask.Status.Equals(Dominio.Enum.TaskStatus.PENDING) ? Color.Orange : Color.Green;
            TaskStatus.Text = _currentTask.Status;


            mTimePicker.BindingContext = this; // the object containing the MyTimeSpanProperty property
            mTimePicker.SetBinding(TimePicker.TimeProperty, "Alarm", BindingMode.TwoWay, null, null);
            Alarm = DateTime.Now.TimeOfDay;
        }

        async void BtnFinishTask_OnClick(object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new ViewTask_Finish(_currentTask));
        }


        async void BtnSaveTask_OnClick(object sender, System.EventArgs e)
        {
            DateTime time = _currentTask.Date.Date;
            time = time + Alarm;

            var DB = new Database("Encuesta");
            
            CrossLocalNotifications.Current.Show(_currentTask.Name, _currentTask.Details, _currentTask.ID, time);
            
            var task = _currentTask;
            task.Time = time;
            DB.ExecuteQuery("UPDATE Task SET Time = '" + task.Time + "' WHERE ID = " + task.ID);

            await DisplayAlert(_currentTask.Name, "Tarea guardada exitosamente.", "OK");
            OnBackButtonPressed();
        }



    }
}