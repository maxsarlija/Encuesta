
using Xamarin.Forms;

namespace encuesta
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
            InitializeComponent();
            var database = new Database("Encuesta"); // Creates (if does not exist) a database named Encuesta
            var script = new InitialScript(database);
        }
   
           async void testBTN(object sender, System.EventArgs e) {

            await Navigation.PushAsync(new Vistas.nuevaencuesta());
            
        }
    }
}
