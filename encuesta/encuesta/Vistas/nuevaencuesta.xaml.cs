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


        async void BtnClientes_OnClick(object sender, System.EventArgs e)
        {

            await Navigation.PushAsync(new Vistas.CustomersList());

        }

        void BtnBorrarDatos_OnClick(object sender, System.EventArgs e)
        {
            _database.DeleteAll<User>();
            _database.DeleteAll<Customer>();
            _database.DeleteAll<Question>();
            _database.DeleteAll<When>();
        }
    }
}