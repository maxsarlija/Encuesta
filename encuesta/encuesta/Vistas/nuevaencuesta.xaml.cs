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
    public partial class nuevaencuesta : ContentPage
    {
        private Database _database;
        public nuevaencuesta()
        {
            InitializeComponent();

            _database = new Database("Encuesta");
            
        }


        async void BtnNewSurvey_OnClick(object sender, System.EventArgs e)
        {

            await Navigation.PushAsync(new Vistas.NewSurvey_SelectCustomer());

        }

        async void BtnViewSurveys_OnClick(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Vistas.CustomersList());
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