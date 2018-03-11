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

namespace encuesta.Vistas
{
    public partial class ViewTask_Finish : ContentPage
    {
        private encuesta.Tasks _currentTask;
        private Database DB { get; set; }

        public encuesta.Tasks CurrentTask { get; set; }

        public ViewTask_Finish(encuesta.Tasks _task)
        {
            InitializeComponent();
            CurrentTask = _task;

            DB = new Database("Encuesta");
            
            TaskName.Text = CurrentTask.Name;
            TaskEditor.Text = CurrentTask.Notes;
        }

        async void BtnFinishTask_OnClick(object sender, System.EventArgs e)
        {
            CurrentTask.Notes = TaskEditor.Text;
            CurrentTask.Status = encuesta.Dominio.Enum.TaskStatus.COMPLETED;
            CurrentTask.DateCompleted = DateTime.Now.ToLongDateString();

            DB.UpdateItem<encuesta.Tasks>(CurrentTask);

            await DisplayAlert("Tarea finalizada", "La tarea ha sido finalizada exitosamente.", "OK");
            OnBackButtonPressed();
            OnBackButtonPressed();
            OnBackButtonPressed();
        }
        


    }
}