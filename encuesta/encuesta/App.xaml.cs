using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace encuesta
{
	public partial class App : Application
	{
        private static ISettings AppSettings => CrossSettings.Current;

        public static string UserName
        {
            get => AppSettings.GetValueOrDefault(nameof(UserName), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(UserName), value);
        }


        public App ()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
