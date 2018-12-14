using Android.OS;
using Android.App;
using Android.Widget;
using Enfermed.Models;
using Enfermed.Services;

// importamos libreria AppCompatActivity
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Enfermed
{
    [Activity(Label = "Rotación", ParentActivity = typeof(PacienteList), Theme = "@style/MyTheme")]
    public class RotacionDetail : AppCompatActivity
    {
        internal static string KEY_ID = "KEY_ID"; // KEY_ID de Paciente
        private TextView _txtTipoColchon, _txtPosicion, _txtFecha, _txtHora;
        private Rotacion _rotacion;
        private RotacionService _rotacionService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RotacionDetail);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            // Una marca atrás en el icono en ActionBar
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            _txtTipoColchon = FindViewById<TextView>(Resource.Id.txtTipoColchon);
            _txtPosicion = FindViewById<TextView>(Resource.Id.txtPosicion);
            _txtFecha = FindViewById<TextView>(Resource.Id.txtFecha);
            _txtHora = FindViewById<TextView>(Resource.Id.txtHora);

            // Recibimos el ID de MedicamentoListActivity
            var id = Intent.Extras.GetInt(KEY_ID);
            ViewRotacionDetail(id);
        }

        private void ViewRotacionDetail(int id)
        {
            // Instanciamos
            _rotacion = new Rotacion();
            _rotacionService = new RotacionService();

            // Consultamos
            _rotacion = _rotacionService.getRotacionPacienteById(id); // Devuelve una rotación de un paciente por Id

            // Mostramos los datos
            if (_rotacion.comun == true) { _txtTipoColchon.Text = "Común"; }
            if (_rotacion.aire == true) { _txtTipoColchon.Text = "Aíre"; }
            if (_rotacion.lateralIzq == true) { _txtPosicion.Text = "Lateral Izquierdo"; }
            if (_rotacion.lateralDer == true) { _txtPosicion.Text = "Lateral Derecho"; }
            if (_rotacion.supina == true) { _txtPosicion.Text = "Supino"; }
            if (_rotacion.prono == true) { _txtPosicion.Text = "Prono"; }
            _txtFecha.Text = _rotacion.fecha;
            _txtHora.Text = _rotacion.hora;
        }
    }
}