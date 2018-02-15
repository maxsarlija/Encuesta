using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace encuesta.Vistas
{
    public partial class Tasks : ContentPage
    {
        protected Database DB { get; set; }


        public Tasks()
        {
            InitializeComponent();

            DB = new Database("Encuesta");
            var _tasks = DB.GetItems<Task>();

            TasksListView.ItemsSource = _tasks;
        }
        


    }
}