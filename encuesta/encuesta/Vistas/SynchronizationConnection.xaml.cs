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
    public partial class SynchronizationConnection : ContentPage
    {

        public SynchronizationConnection()
        {
            InitializeComponent();
        }

        async void BtnTestConnection_OnClick(object sender, System.EventArgs e)
        {
            await CheckConnection();
        }

        protected async System.Threading.Tasks.Task CheckConnection()
        {
            txtTestingConnection.IsVisible = true;
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {

                    var canReach = await CrossConnectivity.Current.IsRemoteReachable("http://s-tmkt.com/");
                    if (canReach)
                    {
                        txtConnection.TextColor = Color.Green;
                        txtConnection.Text = "Dispositivo conectado.";
                    }
                    else
                    {
                        txtConnection.TextColor = Color.Orange;
                        txtConnection.Text = "Error al contactar servidor.";
                    }
                }
                else
                {
                    txtConnection.TextColor = Color.Red;
                    txtConnection.Text = "El dispositivo no está conectado a internet.";
                }

                txtConnection.IsVisible = true;
                txtTestingConnection.IsVisible = false;

            } catch(Exception e)
            {
                Debug.Print(e.Message);
            }          

        }


    }
}