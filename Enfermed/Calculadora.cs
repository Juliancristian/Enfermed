using Android.OS;
using Android.App;
using Android.Content;

// importamos libreria AppCompatActivity
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Enfermed
{
    [Activity(Label = "Calculadora", ParentActivity = typeof(Panel), Theme = "@style/MyTheme")]
    public class Calculadora : AppCompatActivity
    {
        private CardView _btnGoteo, _btnDosis;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Calculadora);

            Toolbar toolBar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolBar);

            // Una marca atrás en el icono en ActionBar
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            // Calculadora Goteo
            _btnGoteo = FindViewById<CardView>(Resource.Id.bntGoteo);
            _btnGoteo.Click += (sender, args) =>
            {
                // Acción redireccionar a otra activity
                StartActivity(new Intent(this, typeof(CalculadoraGoteo)));
            };

            // Calculadora Dosis
            _btnDosis = FindViewById<CardView>(Resource.Id.btnDosis);
            _btnDosis.Click += (sender, args) =>
            {
                // Acción redireccionar a otra activity
                StartActivity(new Intent(this, typeof(CalculadoraDosis)));
            };
        }
    }
}