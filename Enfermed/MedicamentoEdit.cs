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
    [Activity(Label = "Editar Medicamento", ParentActivity = typeof(PacienteList), Theme = "@style/MyTheme")]
    public class MedicamentoEdit : AppCompatActivity
    {
        internal static string KEY_ID = "KEY_ID"; // Id Medicamento
        internal static int _idPending;
        private EditText _edtFarmaco, _edtDosis, _edtFecha, _edtHora;
        private RadioButton _radio1, _radio2, _radio3, _radio4, _radio5;
        private Button _btnEdit;
        private Medicamento _medicamento;
        private Paciente _paciente;
        private MedicamentoService _medicamentoService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MedicamentoEdit);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            // Una marca atrás en el icono en ActionBar
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            _edtFarmaco = FindViewById<EditText>(Resource.Id.edtFarmaco); // Farmaco
            _edtDosis = FindViewById<EditText>(Resource.Id.edtDosis); // Dosis
            _radio1 = FindViewById<RadioButton>(Resource.Id.radio1); // Oral
            _radio2 = FindViewById<RadioButton>(Resource.Id.radio2); // Subcutanea
            _radio3 = FindViewById<RadioButton>(Resource.Id.radio3); // Intramuscular
            _radio4 = FindViewById<RadioButton>(Resource.Id.radio4); // Intravenoso
            _radio5 = FindViewById<RadioButton>(Resource.Id.radio5); // Inhalatoria
            _edtFecha = FindViewById<EditText>(Resource.Id.edtFecha); // Fecha
            _edtHora = FindViewById<EditText>(Resource.Id.edtHora); // Hora
            _btnEdit = FindViewById<Button>(Resource.Id.btnEdit); // Botón

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


            // Recibimos el Id Medicamento
            var id = Intent.Extras.GetInt(KEY_ID);

            // Instanciamos
            _medicamento = new Medicamento();
            _medicamentoService = new MedicamentoService();

            // Consultamos la lista medicamentos de un paciente por Id
            _medicamento = _medicamentoService.getMedicamentoPacienteById(id);

            // Mostramos los datos
            _edtFarmaco.Text = _medicamento.farmaco;
            _edtDosis.Text = _medicamento.dosis.ToString(); //ToString: convierte de entero a string
            if (_medicamento.viaOral == true) { _radio1.Checked = true; }
            if (_medicamento.viaSubcutanea == true) { _radio2.Checked = true; }
            if (_medicamento.viaIntramuscular == true) { _radio3.Checked = true; }
            if (_medicamento.viaIntravenoso == true) { _radio4.Checked = true; }
            if (_medicamento.viaInhalatoria == true) { _radio5.Checked = true; }
            _edtFecha.Text = _medicamento.fecha;
            _edtHora.Text = _medicamento.hora;

            // Click Actualizar
            _btnEdit.Click += updateAlertMedicamento;
        }

        private void updateAlertMedicamento(object sender, EventArgs e)
        {
            // Recibimos el Id Paciente
            var id = Intent.Extras.GetInt(KEY_ID);

            // Cargamos los datos  
            _medicamento.Id = id;
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
                    _medicamentoService.updateMedicamento(_medicamento); // Actualizar el registro en la base de datos

                    _paciente = new Paciente(); // Instanciamos
                    _paciente = _medicamentoService.getPacienteByIdMedicamento(_medicamento.Id); // Devuelve paciente por id medicamento

                    AlarmMedicamento(_medicamento);

                    // Mensaje
                    Toast.MakeText(this, "Se ha actualizado el medicamento", ToastLength.Short).Show();

                    // Acción redireccionar a otra activity
                    Intent otroActivity = new Intent(this, typeof(MedicamentoList));
                    otroActivity.PutExtra("KEY_ID", _paciente.Id); // Pasamos el Id Paciente
                    StartActivity(otroActivity);
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