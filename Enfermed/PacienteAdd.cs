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
    [Activity(Label = "Agregar Paciente", ParentActivity = typeof(PacienteList), Theme = "@style/MyTheme")]
    public class PacienteAdd : AppCompatActivity
    {
        private EditText _edtNroHistoria, _edtNombre, _edtApellido, _edtEdad, _edtNroHabitacion, _edtNroCama;
        private RadioButton _radioM, _radioF;
        private Button _btnAdd;
        private Paciente _paciente;
        private PacienteService _pacienteService;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PacienteAdd);

            Toolbar toolBar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolBar);

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
            _btnAdd = FindViewById<Button>(Resource.Id.btnAdd); // Botón

            // Click Guardar
            _btnAdd.Click += savePaciente;
        }


        private void savePaciente(object sender, EventArgs e)
        {
            // Instanciamos
            _paciente = new Paciente();

            // Cargamos los datos                
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

                    // Insertar el registro en la base de datos
                    _pacienteService.addPaciente(_paciente);

                    // Mensaje
                    Toast.MakeText(this, "Se agregó: " + _paciente.Nombre, ToastLength.Short).Show();

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