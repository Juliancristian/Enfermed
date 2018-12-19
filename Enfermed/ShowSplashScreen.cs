using Android.OS;
using Android.App;
using Android.Support.V7.App;
using GR.Net.Maroulis.Library;
using Android.Graphics;
using Android.Views;

namespace Enfermed
{
    [Activity(Label = "Enfermed", MainLauncher = true, Icon = "@drawable/logo", Theme = "@style/MyTheme")]
    public class ShowSplashScreen : AppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Show Splash Screen: DESCARGAR NUGET "EasySplashScreen v.1"
            var config = new EasySplashScreen(this)
                .WithFullScreen()
                .WithTargetActivity(Java.Lang.Class.FromType(typeof(Panel)))
                .WithSplashTimeOut(3000) // 3 segundos
                .WithBackgroundColor(Color.White) // Fondo de Pantalla      
                .WithLogo(Resource.Drawable.logo_small)
                .WithFooterText("2018 © Copyright | Enfermed");

            // Set Color
            config.FooterTextView.SetTextColor(Color.ParseColor("#00b00f"));

            // Create View
            View view = config.Create();
            SetContentView(view);
        }
    }
}