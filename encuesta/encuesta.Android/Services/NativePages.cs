using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using encuesta.Droid.Services;
using encuesta.Interfaces;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(NativePages))]
namespace encuesta.Droid.Services
{
    public class NativePages : INativePages
    {


        public void StartActivityInAndroid()
        {
            
        }

    }

}
