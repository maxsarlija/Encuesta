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
	public partial class inicio : ContentPage
	{
		public inicio ()
		{
			InitializeComponent ();
		}

        async void newEncuesta(object sender, System.EventArgs e)
        {

            await Navigation.PushAsync(new Vistas.CustomersList());

        }
    }
}