using System.Collections.Generic;
using System.Linq;
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
    [Activity(Label = "Borrar Paciente", ParentActivity = typeof(PacienteList), Theme = "@style/MyTheme")]
    public class PacienteBorrar : AppCompatActivity
    {
        internal static string KEY_ID = "KEY_ID"; // Id Paciente
        private TextView _txtNroHistoria, _txtNombre, _txtApellido, _txtEdad, _txtGenero, _txtNroHabitacion, _txtNroCama;
        private List<Medicamento> _listaMedicamentos;
        private List<Rotacion> _listaRotaciones;
        private Button _btnRemove;
        private Paciente _paciente;
        private PacienteService _pacienteService;
        private MedicamentoService _medicamentoService;
        private RotacionService _rotacionService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PacienteBorrar);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            // Una marca atrás en el icono en ActionBar
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            _txtNroHistoria = FindViewById<TextView>(Resource.Id.txtNroHistoria);
            _txtNombre = FindViewById<TextView>(Resource.Id.txtNombre);
            _txtApellido = FindViewById<TextView>(Resource.Id.txtApellido);
            _txtEdad = FindViewById<TextView>(Resource.Id.txtEdad);
            _txtGenero = FindViewById<TextView>(Resource.Id.txtGenero);
            _txtNroHabitacion = FindViewById<TextView>(Resource.Id.txtNroHabit);
            _txtNroCama = FindViewById<TextView>(Resource.Id.txtNroCama);
            _btnRemove = FindViewById<Button>(Resource.Id.btnRemove);

            // Recibimos el Id Paciente
            var id = Intent.Extras.GetInt(KEY_ID);
            ViewPacienteDetail(id);

            // Instanciamos 
            _listaMedicamentos = new List<Medicamento>();
            _medicamentoService = new MedicamentoService();

            // Click Eliminar
            _btnRemove.Click += delegate
            {
                RemovePaciente(id);
            };
        }

        private void ViewPacienteDetail(int id)
        {
            // Instanciamos
            _paciente = new Paciente();
            _pacienteService = new PacienteService();

            // Consultamos
            _paciente = _pacienteService.getPacienteById(id); // Devuelve un paciente por Id

            // Mostramos los datos
            _txtNroHistoria.Text = _paciente.NroHistoria.ToString(); //ToString: convierte de entero a string
            _txtNombre.Text = _paciente.Nombre;
            _txtApellido.Text = _paciente.Apellido;
            _txtEdad.Text = _paciente.Edad.ToString();
            if (_paciente.Masculino == true) { _txtGenero.Text = "Masculino"; }
            if (_paciente.Femenino == true) { _txtGenero.Text = "Femenino"; }
            _txtNroHabitacion.Text = _paciente.NroHabitacion.ToString();
            _txtNroCama.Text = _paciente.NroCama;
        }

        private void RemovePaciente(int id)
        {
            // Instanciamos 
            _listaMedicamentos = new List<Medicamento>();
            _listaRotaciones = new List<Rotacion>();
            _medicamentoService = new MedicamentoService();
            _rotacionService = new RotacionService();

            // Consultamos
            _listaMedicamentos = _medicamentoService.getMedicamentosPacienteById(id); // Devuelve lista medicamentos por Id paciente
            _listaRotaciones = _rotacionService.getRotacionesPacienteById(id); // Devuelve lista rotaciones por Id paciente

            // Si no hay registros de medicamentos y rotaciones. Se elimina el paciente
            if (!_listaMedicamentos.Any() && !_listaRotaciones.Any())
            {
                // Eliminar el registro en la base de datos
                _pacienteService.deletePaciente(id);

                // Mensaje
                Toast.MakeText(this, "Se ha eliminado el paciente", ToastLength.Short).Show();

                // Acción redireccionar a otra activity
                StartActivity(new Intent(this, typeof(PacienteList)));
                Finish();
            }
            else
            {
                // Mensaje
                Toast.MakeText(this, "No se puede eliminar el paciente", ToastLength.Short).Show();
            }
        }
    }
}