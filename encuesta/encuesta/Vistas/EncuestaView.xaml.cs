
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace encuesta.Vistas
{
    public partial class EncuestaView : ContentPage
    {
        
        public EncuestaView()
        {
            InitializeComponent();

            var database = new Database("Encuesta");
        }
        
    }
}