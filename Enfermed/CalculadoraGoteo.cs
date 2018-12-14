using System;
using Android.OS;
using Android.App;
using Android.Widget;

// importamos libreria AppCompatActivity
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Enfermed
{
    [Activity(Label = "Calculadora Goteo", ParentActivity = typeof(Calculadora), Theme = "@style/MyTheme")]
    public class CalculadoraGoteo : AppCompatActivity
    {
        private EditText _n1, _n2;
        private Button _btnCalcular;
        private TextView _result;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CalculadoraGoteo);

            Toolbar toolBar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolBar);

            // Una marca atrás en el icono en ActionBar
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            _n1 = FindViewById<EditText>(Resource.Id.edtVol); // Volumen
            _n2 = FindViewById<EditText>(Resource.Id.edtTiempo); // Tiempo
            _btnCalcular = FindViewById<Button>(Resource.Id.btnCalcular); // Botón
            _result = FindViewById<TextView>(Resource.Id.textResult); // Resultado

            _btnCalcular.Click += btnClickCalcular;
        }

        // Click en calcular
        private void btnClickCalcular(object sender, EventArgs e)
        {
            // Validar los campos
            if (_n1.Text == string.Empty || _n2.Text == string.Empty)
            {
                Toast.MakeText(this, "Todos los campos son obligatorios", ToastLength.Short).Show();
            }
            else
            {
                // Casteo los enteros
                var num1 = int.Parse(_n1.Text);
                var num2 = int.Parse(_n2.Text);

                // Operacion de calculo de goteo
                _result.Text = (num1 / (3 * num2)).ToString(); // Convierto el resultado en una cadena
            }
        }
    }
}