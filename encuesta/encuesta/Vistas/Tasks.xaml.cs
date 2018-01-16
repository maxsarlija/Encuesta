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

            var _tasks = new List<Task>();
            _tasks.Add(new Task("Tarea 1", Color.Beige));
            _tasks.Add(new Task("Tarea 2", Color.DarkRed));
            _tasks.Add(new Task("Tarea 3", Color.CornflowerBlue));
            _tasks.Add(new Task("Tarea 4", Color.GreenYellow));
            _tasks.Add(new Task("Tarea 5", Color.PaleVioletRed));
            _tasks.Add(new Task("Tarea 6", Color.DarkCyan));
            TasksListView.ItemsSource = _tasks;
        }
        

        protected class Task
        {
            public Color Color { get; set; }
            public string Description { get; set; }

            public Task(string _description, Color _color)
            {
                Color = _color;
                Description = _description;
            }

        }
        


    }
}