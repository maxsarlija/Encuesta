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
        public nuevaencuesta()
        {
            InitializeComponent();

            var database = new Database("Encuesta");

            var res = database.Query<Customer>("SELECT * FROM Customer WHERE Name = 'Restaurant'");
            Address.Text = res[0].Name;
        }
	}
}