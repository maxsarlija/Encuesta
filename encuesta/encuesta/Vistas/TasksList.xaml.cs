using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace encuesta.Vistas
{
    public partial class TasksList : ContentPage
    {
        protected Database DB { get; set; }
        private ObservableCollection<Tasks> _tasksCollection;

        protected ObservableCollection<Tasks> TasksCollection
        {
            get
            {
                return _tasksCollection;
            }
            set
            {
                _tasksCollection = value;
                OnPropertyChanged();
            }
        }

        public TasksList()
        {
            InitializeComponent();

            DB = new Database("Encuesta");
            var _tasks = DB.GetItems<encuesta.Tasks>();

            TasksCollection = new ObservableCollection<Tasks>();

            foreach (var item in _tasks)
            {
                TasksCollection.Add(item);
            }
            
            TasksListView.ItemsSource = TasksCollection;
        }

        async void BtnTask_OnClickItem(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            // Click on Customer will lead to the available surveys.
            var _selectedTask = (encuesta.Tasks)e.SelectedItem;

            await Navigation.PushAsync(new ViewTask(_selectedTask));

            ((ListView)sender).SelectedItem = null;
        }


    }
}