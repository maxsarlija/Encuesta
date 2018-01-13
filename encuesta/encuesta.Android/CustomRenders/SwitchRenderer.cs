using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.ExportRenderer(typeof(Xamarin.Forms.Switch),
typeof(encuesta.Droid.CustomRenders.SwitchRenderer))]

namespace encuesta.Droid.CustomRenders
{
    public class SwitchRenderer : Xamarin.Forms.Switch
    {
        private Color greyColor = new Color(215, 218, 220);
        private Color greenColor = new Color(32, 156, 68);

       /* protected override void Dispose(bool disposing)
        {
            this.Control.CheckedChange -= this.OnCheckedChange;
            base.Dispose(disposing);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Switch> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null)
            {
                if (this.Control.Checked)
                {
                    this.Control.ThumbDrawable.SetColorFilter(greenColor, PorterDuff.Mode.SrcAtop);
                }
                else
                {
                    this.Control.ThumbDrawable.SetColorFilter(greyColor, PorterDuff.Mode.SrcAtop);
                }

                this.Control.CheckedChange += this.OnCheckedChange;
            }
        }

        private void OnCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (this.Control.Checked)
            {
                this.Control.ThumbDrawable.SetColorFilter(greenColor, PorterDuff.Mode.SrcAtop);
            }
            else
            {
                this.Control.ThumbDrawable.SetColorFilter(greyColor, PorterDuff.Mode.SrcAtop);
            }
        }*/
    }
}