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
    [Activity(Label = "Editar Paciente", ParentActivity = typeof(PacienteList), Theme = "@style/MyTheme")]
    public class PacienteEdit : AppCompatActivity
    {
        internal static string KEY_ID = "KEY_ID"; // Id Paciente
        private EditText _edtNroHistoria, _edtNombre, _edtApellido, _edtEdad, _edtNroHabitacion, _edtNroCama;
        private RadioButton _radioM, _radioF;
        private Button _btnEdit;
        private PacienteService _pacienteService;
        private Paciente _paciente;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PacienteEdit);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            // Una marca atrás en el icono en ActionBar
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            _edtNroHistoria = FindViewById<EditText>(Resource.Id.editNroHistoria); // Numero historia
            _edtNombre = FindViewById<EditText>(Resource.Id.editNombre); // Nombre
            _edtApellido = FindViewById<EditText>(Resource.Id.editApellido); // Apellido
            _edtEdad = FindViewById<EditText>(Resource.Id.editEdad); // Edad
            _radioM = FindViewById<RadioButton>(Resource.Id.radioM); // Masculino
            _radioF = FindViewById<RadioButton>(Resource.Id.radioF); // Femenino
            _edtNroHabitacion = FindViewById<EditText>(Resource.Id.editNroHabit); // Numero habitacion
            _edtNroCama = FindViewById<EditText>(Resource.Id.editNroCama); // Numero cama
            _btnEdit = FindViewById<Button>(Resource.Id.btnEdit); // Botón

            // Recibimos el Id Paciente
            var id = Intent.Extras.GetInt(KEY_ID);

            // Instanciamos
            _paciente = new Paciente();
            _pacienteService = new PacienteService();

            // Consultamos
            _paciente = _pacienteService.getPacienteById(id); // Devuelve un paciente por Id

            // Mostramos los datos en EditText
            _edtNroHistoria.Text = _paciente.NroHistoria.ToString(); //ToString: convierte de entero a string
            _edtNombre.Text = _paciente.Nombre;
            _edtApellido.Text = _paciente.Apellido;
            _edtEdad.Text = _paciente.Edad.ToString();
            if (_paciente.Masculino == true) { _radioM.Checked = true; }
            if (_paciente.Femenino == true) { _radioF.Checked = true; }
            _edtNroHabitacion.Text = _paciente.NroHabitacion.ToString();
            _edtNroCama.Text = _paciente.NroCama;

            // Click Actualizar
            _btnEdit.Click += updatePaciente;
        }

        private void updatePaciente(object sender, EventArgs e)
        {
            // Recibimos el Id
            var id = Intent.Extras.GetInt(KEY_ID);

            // Instanciamos
            _paciente = new Paciente();

            // Cargamos los datos           
            _paciente.Id = id;
            _paciente.Nombre = _edtNombre.Text;
            _paciente.Apellido = _edtApellido.Text;
            _paciente.Masculino = _radioM.Checked == true ? true : false;
            _paciente.Femenino = _radioF.Checked == true ? true : false;
            _paciente.NroCama = _edtNroCama.Text;

            // Validacion
            if (Validate())
            {
                try
                {
                    //Instanciamos 
                    _pacienteService = new PacienteService();

                    // Actualizar el registro en la base de datos
                    _pacienteService.updatePaciente(_paciente);

                    // Mensaje
                    Toast.MakeText(this, "Se ha actualizado el paciente", ToastLength.Short).Show();

                    // Acción redireccionar a otra activity
                    StartActivity(new Intent(this, typeof(PacienteList)));
                    Finish();
                }
                catch (Java.Lang.Exception ex)
                {
                    Toast.MakeText(this, "Error en la base de datos: " + ex.Message, ToastLength.Long).Show();
                }
            }
        }

        private bool Validate()
        {
            // Si los campos enteros estan vacios
            if (_edtNroHistoria.Text == string.Empty || _edtEdad.Text == string.Empty || _edtNroHabitacion.Text == string.Empty)
            {
                _paciente.NroHistoria = 0;
                _paciente.Edad = 0;
                _paciente.NroHabitacion = 0;
            }
            else
            {
                _paciente.NroHistoria = int.Parse(_edtNroHistoria.Text);
                _paciente.Edad = int.Parse(_edtEdad.Text);
                _paciente.NroHabitacion = int.Parse(_edtNroHabitacion.Text);
            }

            // Si todos los campos estan vacios
            if (_paciente.NroHistoria == 0 || _paciente.Nombre == string.Empty || _paciente.Apellido == string.Empty || _paciente.Edad == 0 || _paciente.NroHabitacion == 0 || _paciente.NroCama == string.Empty)
            {
                Toast.MakeText(this, "Todos los campos son obligatorios", ToastLength.Short).Show();
                return false;
            }

            return true;
        }
    }
}