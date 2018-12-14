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
    [Activity(Label = "Borrar Medicamento", ParentActivity = typeof(PacienteList), Theme = "@style/MyTheme")]
    public class MedicamentoBorrar : AppCompatActivity
    {
        internal static string KEY_ID = "KEY_ID"; // Id Medicamento
        private TextView _txtFarmaco, _txtDosis, _txtViaAdmin, _txtFecha, _txtHora;
        private Button _btnRemove;
        private Medicamento _medicamento;
        private Paciente _paciente;
        private MedicamentoService _medicamentoService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MedicamentoBorrar);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            // Una marca atrás en el icono en ActionBar
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            _txtFarmaco = FindViewById<TextView>(Resource.Id.txtFarmaco);
            _txtDosis = FindViewById<TextView>(Resource.Id.txtDosis);
            _txtViaAdmin = FindViewById<TextView>(Resource.Id.txtViaAdmin);
            _txtFecha = FindViewById<TextView>(Resource.Id.txtFecha);
            _txtHora = FindViewById<TextView>(Resource.Id.txtHora);
            _btnRemove = FindViewById<Button>(Resource.Id.btnRemove);

            // Recibimos el Id Medicamento
            var id = Intent.Extras.GetInt(KEY_ID);
            ViewMedicamentoDetail(id);

            // Click Eliminar
            _btnRemove.Click += delegate
            {
                RemoveMedicamento(id);
            };
        }

        private void ViewMedicamentoDetail(int id)
        {
            // Instanciamos
            _medicamento = new Medicamento();
            _medicamentoService = new MedicamentoService();

            // Consultamos la lista medicamentos de un paciente por Id
            _medicamento = _medicamentoService.getMedicamentoPacienteById(id);

            // Mostramos los datos
            _txtFarmaco.Text = _medicamento.farmaco;
            _txtDosis.Text = _medicamento.dosis.ToString(); //ToString: convierte de entero a string
            if (_medicamento.viaOral == true) { _txtViaAdmin.Text = "Oral"; }
            if (_medicamento.viaSubcutanea == true) { _txtViaAdmin.Text = "Subcutánea"; }
            if (_medicamento.viaIntramuscular == true) { _txtViaAdmin.Text = "Intramuscular"; }
            if (_medicamento.viaIntravenoso == true) { _txtViaAdmin.Text = "Intravenoso"; }
            if (_medicamento.viaInhalatoria == true) { _txtViaAdmin.Text = "Inhalatoria"; }
            _txtFecha.Text = _medicamento.fecha;
            _txtHora.Text = _medicamento.hora;
        }

        private void RemoveMedicamento(int id)
        {
            try
            {
                // Eliminar el registro en la base de datos
                _medicamentoService.deleteMedicamento(id);

                // Mensaje
                Toast.MakeText(this, "Se ha eliminado el medicamento", ToastLength.Short).Show();

                _paciente = new Paciente(); // Instanciamos
                _paciente = _medicamentoService.getPacienteByIdMedicamento(_medicamento.Id); // Devuelve paciente por id medicamento

                // Acción redireccionar a otra activity
                Intent otroActivity = new Intent(this, typeof(MedicamentoList));
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