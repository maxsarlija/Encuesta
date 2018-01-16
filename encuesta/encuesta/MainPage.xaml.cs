using System;
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
            var script = new InitialScript(DB);

            LoginButton.GestureRecognizers.Add(new TapGestureRecognizer());

        }

        void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            if (EntryUsername.Text == null || EntryPassword.Text == null)
            {
                DisplayAlert("Error", "Ingresar usuario y contraseña.", "OK");
            }
            else
            {
                var query = "SELECT * FROM User WHERE Username = " + EntryUsername.Text + " AND Password = " + EntryPassword.Text;
                var _userValidation = DB.Query<User>(query).FirstOrDefault();

                if (_userValidation == null)
                {
                    DisplayAlert("Error", "El usuario y/o contraseña son inválidos.", "OK");
                }
                else
                {
                    App.Current.MainPage = new NavigationPage(new encuesta.Vistas.nuevaencuesta());
                }
            }
            
        }
        
    }
}
