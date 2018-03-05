using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using encuesta.Dominio.Enum;
using System;
using System.IO;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace encuesta.Vistas
{
    public partial class ViewPhoto : ContentPage
    {
        
        public ViewPhoto(MediaFile file)
        {
            InitializeComponent();

            if (file == null)
                return;

            image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }
        

    }
}