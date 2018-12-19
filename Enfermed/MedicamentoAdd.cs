using System;
using Android.OS;
using Android.App;
using Android.Widget;
using Android.Content;
using Enfermed.Models;
using Enfermed.Services;
using Enfermed.Broadcast;

// importamos libreria AppCompatActivity
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Enfermed
{
    [Activity(Label = "Agregar Medicamento", ParentActivity = typeof(PacienteList), Theme = "@style/MyTheme")]
    public class MedicamentoAdd : AppCompatActivity
    {
        internal static string KEY_ID = "KEY_ID"; // Id Paciente
        internal static int _idPending;
        private EditText _edtFarmaco, _edtDosis, _edtFecha, _edtHora;
        private RadioButton _radio1, _radio2, _radio3, _radio4, _radio5;
        private Button _btnAdd;
        private Paciente _paciente;
        private Medicamento _medicamento;
        private PacienteService _pacienteService;
        private MedicamentoService _medicamentoService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MedicamentoAdd);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            // Una marca atrás en el icono en ActionBar
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            _edtFarmaco = FindViewById<EditText>(Resource.Id.editFarmaco); // Farmaco
            _edtDosis = FindViewById<EditText>(Resource.Id.editDosis); // Dosis
            _radio1 = FindViewById<RadioButton>(Resource.Id.radio1); // Oral
            _radio2 = FindViewById<RadioButton>(Resource.Id.radio2); // Subcutanea
            _radio3 = FindViewById<RadioButton>(Resource.Id.radio3); // Intramuscular
            _radio4 = FindViewById<RadioButton>(Resource.Id.radio4); // Intravenoso
            _radio5 = FindViewById<RadioButton>(Resource.Id.radio5); // Inhalatoria
            _edtFecha = FindViewById<EditText>(Resource.Id.edtFecha); // Fecha
            _edtHora = FindViewById<EditText>(Resource.Id.edtHora); // Hora
            _btnAdd = FindViewById<Button>(Resource.Id.btnAdd); // Botón

            // Instanciamos
            _medicamento = new Medicamento();

            // Click Fecha
            _edtFecha.Click += delegate
            {
                DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
                {
                    _edtFecha.Text = time.ToShortDateString(); // Mostrar la fecha seleccionada en el edittext
                    _medicamento.fecha = _edtFecha.Text; // Cargamos la fecha
                });
                frag.Show(FragmentManager, DatePickerFragment.TAG);
            };

            // Click Hora
            _edtHora.Click += delegate
            {
                TimePickerFragment frag = TimePickerFragment.NewInstance(delegate (DateTime time)
                {
                    _edtHora.Text = time.ToShortTimeString(); // Mostrar la hora seleccionada en el edittext
                    _medicamento.hora = _edtHora.Text; // Cargamos la hora
                });

                frag.Show(FragmentManager, TimePickerFragment.TAG);
            };

            // Recibimos el Id paciente
            var id = Intent.Extras.GetInt(KEY_ID);

            // Consultamos
            _paciente = new Paciente();
            _pacienteService = new PacienteService();
            _paciente = _pacienteService.getPacienteById(id); // Devuelve un paciente por Id

            // Click Alertar
            _btnAdd.Click += saveAlertMedicamento;
        }

        private void saveAlertMedicamento(object sender, EventArgs e)
        {
            // Cargamos los datos          
            _medicamento.farmaco = _edtFarmaco.Text;
            _medicamento.viaOral = _radio1.Checked == true ? true : false;
            _medicamento.viaSubcutanea = _radio2.Checked == true ? true : false;
            _medicamento.viaIntramuscular = _radio3.Checked == true ? true : false;
            _medicamento.viaIntravenoso = _radio4.Checked == true ? true : false;
            _medicamento.viaInhalatoria = _radio5.Checked == true ? true : false;
            _medicamento.confirmar = false; // Por defecto es false

            // Validacion
            if (Validate())
            {
                DateTime now = DateTime.Now; // Fecha y hora actual
                DateTime selectedDT = Convert.ToDateTime(_medicamento.fecha + " " + _medicamento.hora); // Fecha y hora seleccionado

                // No debe aplicarse en la fecha pasada. Debe ser mayor a la fecha y hora seleccionada
                if (selectedDT > now)
                {
                    _medicamentoService = new MedicamentoService(); // Instanciamos
                    _medicamentoService.addMedicamento(_paciente, _medicamento); // Insertar el registro en la base de datos
                    AlarmMedicamento(_medicamento);

                    // Mensaje
                    Toast.MakeText(this, "Se agregó: " + _medicamento.farmaco, ToastLength.Short).Show();

                    // Acción redireccionar a otra activity
                    Intent myIntent = new Intent(this, typeof(MedicamentoList));
                    myIntent.PutExtra("KEY_ID", _paciente.Id); // Pasamos el Id Paciente
                    StartActivity(myIntent);
                }
                else
                {
                    Toast.MakeText(this, "Esta es una selección no válida de fecha y hora", ToastLength.Short).Show();
                }
            }
        }

        private bool Validate()
        {
            // Si el campo entero esta vacio
            if (_edtDosis.Text == string.Empty)
            {
                _medicamento.dosis = 0;
            }
            else
            {
                _medicamento.dosis = int.Parse(_edtDosis.Text);
            }

            // Si todos los campos estan vacios
            if (_medicamento.fecha == string.Empty || _medicamento.hora == string.Empty || _medicamento.farmaco == string.Empty || _medicamento.dosis == 0)
            {
                Toast.MakeText(this, "Todos los campos son obligatorios", ToastLength.Short).Show();
                return false;
            }

            return true;
        }

        private void AlarmMedicamento(Medicamento _medicamento)
        {
            Intent myIntent = new Intent(this, typeof(MedicamentoNotification));

            var dateString = Convert.ToString(_medicamento.fecha + " " + _medicamento.hora); // Fecha y hora seleccionado en string

            DateTimeOffset dateOffsetValue = DateTimeOffset.Parse(dateString);
            var millisec = dateOffsetValue.ToUnixTimeMilliseconds();

            _idPending = (int)millisec; // Identificar

            // Inicializar al gerente de alarma
            AlarmManager alarmManager = (AlarmManager)GetSystemService(AlarmService);

            // Pasar el contexto, su codigo de solicitud privado, objeto de intencion y bandera
            var pendingIntent = PendingIntent.GetBroadcast(this, _idPending, myIntent, 0);
            alarmManager.Set(AlarmType.RtcWakeup, millisec, pendingIntent);
        }
    }
}