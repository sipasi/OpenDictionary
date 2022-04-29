using Android.Content;

using OpenDictionary.Views.Controls;

using ProjectWord.Droid.Renderers.Controls;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ButtonLowercase), typeof(ButtonLowercaseRenderer))]
namespace ProjectWord.Droid.Renderers.Controls
{
    public class ButtonLowercaseRenderer : ButtonRenderer
    {
        public ButtonLowercaseRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            var button = Control;

            button.SetAllCaps(false);
        }
    }
}