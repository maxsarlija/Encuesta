using encuesta.Vistas;
using Plugin.Connectivity;
using System;
using System.IO;
using System.Linq;
using Xamarin.Forms;

namespace encuesta
{
    public partial class MainPage : ContentPage
    {

        protected Database DB { get; set; }

        public MainPage()
        {
            InitializeComponent();
            DB = new Database("Encuesta"); // Creates (if does not exist) a database named Encuesta
            DB.CreateTable<User>();

            if (DB.Query<User>("SELECT * FROM User").FirstOrDefault() == null)
            {
                DB.InsertItemWithID(new User(1, "1", "1"));
                new InitialScript(DB);
            }
            EntryUsername.Completed += EntryUsername_Completed;
            EntryPassword.Completed += EntryPassword_Completed;

            LoginButton.GestureRecognizers.Add(new TapGestureRecognizer());

        }

        private void EntryPassword_Completed(object sender, EventArgs e)
        {
            OnTapGestureRecognizerTapped(sender, e);
        }

        private void EntryUsername_Completed(object sender, EventArgs e)
        {
            EntryPassword.Focus();
        }

        async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            if (EntryUsername.Text == null || EntryPassword.Text == null)
            {
                await DisplayAlert("Error", "Ingresar usuario y contraseña.", "OK");
            }
            else
            {
                var query = "SELECT * FROM User WHERE Username = '" + EntryUsername.Text + "' AND Password = '" + EntryPassword.Text +"'";
                var _userValidation = DB.Query<User>(query).FirstOrDefault();

                if (_userValidation == null)
                {
                    await DisplayAlert("Error", "El usuario y/o contraseña son inválidos.", "OK");
                }
                else
                {

                    if (CrossConnectivity.Current.IsConnected)
                    {
                        await Navigation.PushAsync(new Vistas.SynchronizationInitial());
                    }
                    else
                    {
                        await DisplayAlert("Alerta", "Su dispositivo no se encuentra conectado a internet. Conéctese para sincronizar datos.", "OK");
                    }

                }
            }
            
        }
        
    }
}
