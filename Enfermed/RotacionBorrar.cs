using System;
using Android.OS;
using Android.App;
using Android.Widget;
using Android.Content;
using Enfermed.Models;
using Enfermed.Services;

// importamos libreria AppCompatActivity
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Enfermed
{
    [Activity(Label = "Borrar Rotación", ParentActivity = typeof(PacienteList), Theme = "@style/MyTheme")]
    public class RotacionBorrar : AppCompatActivity
    {
        internal static string KEY_ID = "KEY_ID"; // Id Rotación
        private TextView _txtTipoColchon, _txtPosicion, _txtFecha, _txtHora;
        private Button _btnRemove;
        private Rotacion _rotacion;
        private Paciente _paciente;
        private RotacionService _rotacionService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RotacionBorrar);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            // Una marca atrás en el icono en ActionBar
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            _txtTipoColchon = FindViewById<TextView>(Resource.Id.txtTipoColchon);
            _txtPosicion = FindViewById<TextView>(Resource.Id.txtPosicion);
            _txtFecha = FindViewById<TextView>(Resource.Id.txtFecha);
            _txtHora = FindViewById<TextView>(Resource.Id.txtHora);
            _btnRemove = FindViewById<Button>(Resource.Id.btnRemove);

            // Recibimos el Id Rotación
            var id = Intent.Extras.GetInt(KEY_ID);
            ViewRotacionDetail(id);

            // Click Eliminar
            _btnRemove.Click += delegate
            {
                RemoveMedicamento(id);
            };
        }

        private void ViewRotacionDetail(int id)
        {
            // Instanciamos
            _rotacion = new Rotacion();
            _rotacionService = new RotacionService();

            // Consultamos la lista rotación de paciente por Id
            _rotacion = _rotacionService.getRotacionPacienteById(id);

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

        private void RemoveMedicamento(int id)
        {
            try
            {
                // Eliminar el registro en la base de datos
                _rotacionService.deleteRotacion(id);

                // Mensaje
                Toast.MakeText(this, "Se ha eliminado la rotación", ToastLength.Short).Show();

                _paciente = new Paciente(); // Instanciamos
                _paciente = _rotacionService.getPacienteByIdRotacion(_rotacion.Id); // Devuelve paciente por id rotación

                // Acción redireccionar a otra activity
                Intent otroActivity = new Intent(this, typeof(RotacionList));
                otroActivity.PutExtra("KEY_ID", _paciente.Id); // Pasamos el Id Paciente
                StartActivity(otroActivity);
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "Error en la Base de Datos: " + ex.Message, ToastLength.Long).Show();
            }
        }
    }
}