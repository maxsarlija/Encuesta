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
using encuesta.Dominio.Enum;

namespace encuesta.Vistas
{
    public partial class SynchronizationMenu : ContentPage
    {
        public Database DB;

        public SynchronizationMenu()
        {
            InitializeComponent();
            DB = new Database("Encuesta");
        }


        async void BtnConnection_OnClick(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Vistas.SynchronizationConnection());
        }

        


        // Sincronizar encuestas finalizadas al servidor.
        async void BtnSyncSurveys_OnClick(object sender, System.EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {

                var customerAnswers = DB.Query<CustomerAnswer>("SELECT * FROM CustomerAnswer").Where(x => x.Status.Equals(SurveyStatus.COMPLETED)).Count();

                if(customerAnswers > 0)
                {
                    await Navigation.PushAsync(new Vistas.SynchronizationSurveys());
                } 
                else
                {
                    await DisplayAlert("Alerta", "No hay encuestas finalizadas para sincronizar.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Alerta", "Su dispositivo no se encuentra conectado a internet.", "OK");
            }            
        }

    }
}