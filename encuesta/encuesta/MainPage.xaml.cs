
using Xamarin.Forms;

namespace encuesta
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
            InitializeComponent();
            var database = new Database("Encuesta"); // Creates (if does not exist) a database named People
            database.CreateTable<Usuario>(); // Creates (if does not exist) a table of type Person
        }
	}
}
