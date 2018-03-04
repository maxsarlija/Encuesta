using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace encuesta.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenu : ContentPage
    {
        private Database _database;
        public MainMenu()
        {
            InitializeComponent();

            _database = new Database("Encuesta");
            
        }


        async void BtnNewSurvey_OnClick(object sender, System.EventArgs e)
        {

            await Navigation.PushAsync(new Vistas.NewSurvey_SelectSalesman());

        }

        async void BtnViewSurveys_OnClick(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Vistas.SalesmenList());
        }

        async void BtnTasks_OnClick(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Vistas.Tasks());
        }

        async void BtnSync_OnClick(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Vistas.SynchronizationMenu());
        }
    }
}