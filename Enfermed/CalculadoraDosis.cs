using System;
using Android.OS;
using Android.App;
using Android.Widget;

// importamos libreria AppCompatActivity
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
namespace Enfermed
{
    [Activity(Label = "Calculadora Dosis", ParentActivity = typeof(Calculadora), Theme = "@style/MyTheme")]
    public class CalculadoraDosis : AppCompatActivity
    {
        private EditText _n1, _n2, _n3;
        private Button _btnCalcular;
        private TextView _result;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CalculadoraDosis);

            Toolbar toolBar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolBar);

            // Una marca atrás en el icono en ActionBar
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            _n1 = FindViewById<EditText>(Resource.Id.edtConc); // Concentracion
            _n2 = FindViewById<EditText>(Resource.Id.edtDilu); // Dilucion
            _n3 = FindViewById<EditText>(Resource.Id.edtPres); // Presentacion
            _btnCalcular = FindViewById<Button>(Resource.Id.btnCalcular); // Botón
            _result = FindViewById<TextView>(Resource.Id.textResult); // Resultado

            _btnCalcular.Click += btnClickCalcular;
        }

        // Click en calcular
        private void btnClickCalcular(object sender, EventArgs e)
        {
            // Validar los campos
            if (_n1.Text == string.Empty || _n2.Text == string.Empty || _n3.Text == string.Empty)
            {
                Toast.MakeText(this, "Todos los campos son obligatorios", ToastLength.Short).Show();
            }
            else
            {
                // Casteo los enteros
                var num1 = int.Parse(_n1.Text);
                var num2 = int.Parse(_n2.Text);
                var num3 = int.Parse(_n3.Text);

                // Operacion de calculo de dosis
                _result.Text = ((num1 * num2) / num3).ToString(); // Convierto el resultado en una cadena
            }
        }
    }
}