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
    [Activity(Label = "Editar Rotación", ParentActivity = typeof(PacienteList), Theme = "@style/MyTheme")]
    public class RotacionEdit : AppCompatActivity
    {
        internal static string KEY_ID = "KEY_ID"; // Id Rotación
        internal static int _idPending;
        private EditText _edtFecha, _edtHora;
        private RadioButton _radioComun, _radioAire, _radio1, _radio2, _radio3, _radio4;
        private Button _btnEdit;
        private Rotacion _rotacion;
        private Paciente _paciente;
        private RotacionService _rotacionService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RotacionEdit);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            // Una marca atrás en el icono en ActionBar
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            _radioComun = FindViewById<RadioButton>(Resource.Id.radioComun); // Común
            _radioAire = FindViewById<RadioButton>(Resource.Id.radioAire); // Aíre
            _radio1 = FindViewById<RadioButton>(Resource.Id.radio1); // Lateral Izquierdo
            _radio2 = FindViewById<RadioButton>(Resource.Id.radio2); // Lateral Derecho
            _radio3 = FindViewById<RadioButton>(Resource.Id.radio3); // Supino
            _radio4 = FindViewById<RadioButton>(Resource.Id.radio4); // Prono
            _edtFecha = FindViewById<EditText>(Resource.Id.edtFecha); // Fecha
            _edtHora = FindViewById<EditText>(Resource.Id.edtHora); // Hora
            _btnEdit = FindViewById<Button>(Resource.Id.btnEdit); // Botón

            // Instanciamos
            _rotacion = new Rotacion();

            // Click Fecha
            _edtFecha.Click += delegate
            {
                DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
                {
                    _edtFecha.Text = time.ToShortDateString(); // Mostrar la fecha seleccionada en el edittext
                    _rotacion.fecha = _edtFecha.Text; // Cargamos la fecha
                });
                frag.Show(FragmentManager, DatePickerFragment.TAG);
            };

            // Click Hora
            _edtHora.Click += delegate
            {
                TimePickerFragment frag = TimePickerFragment.NewInstance(delegate (DateTime time)
                {
                    _edtHora.Text = time.ToShortTimeString(); // Mostrar la hora seleccionada en el edittext
                    _rotacion.hora = _edtHora.Text; // Cargamos la hora
                });

                frag.Show(FragmentManager, TimePickerFragment.TAG);
            };

            // Recibimos el Id Rotación
            var id = Intent.Extras.GetInt(KEY_ID);

            _rotacion = new Rotacion();
            _rotacionService = new RotacionService();

            // Consultamos la lista rotación de paciente pot Id
            _rotacion = _rotacionService.getRotacionPacienteById(id);

            // Mostramos los datos
            if (_rotacion.comun == true) { _radioComun.Checked = true; }
            if (_rotacion.aire == true) { _radioAire.Checked = true; }
            if (_rotacion.lateralIzq == true) { _radio1.Checked = true; }
            if (_rotacion.lateralDer == true) { _radio2.Checked = true; }
            if (_rotacion.supina == true) { _radio3.Checked = true; }
            if (_rotacion.prono == true) { _radio4.Checked = true; }
            _edtFecha.Text = _rotacion.fecha;
            _edtHora.Text = _rotacion.hora;

            // Click Alertar
            _btnEdit.Click += updateAlertRotacion;
        }

        private void updateAlertRotacion(object sender, EventArgs e)
        {
            // Recibimos el Id Rotación
            var id = Intent.Extras.GetInt(KEY_ID);

            // Cargamos los datos  
            _rotacion.Id = id;
            _rotacion.comun = _radioComun.Checked == true ? true : false;
            _rotacion.aire = _radioAire.Checked == true ? true : false;
            _rotacion.lateralIzq = _radio1.Checked == true ? true : false;
            _rotacion.lateralDer = _radio2.Checked == true ? true : false;
            _rotacion.supina = _radio3.Checked == true ? true : false;
            _rotacion.prono = _radio4.Checked == true ? true : false;
            _rotacion.confirmar = false; // Por defecto es false

            // Validacion
            if (Validate())
            {
                DateTime now = DateTime.Now; // Fecha y hora actual
                DateTime selectedDT = Convert.ToDateTime(_rotacion.fecha + " " + _rotacion.hora); // Fecha y hora seleccionado

                // No debe aplicarse en la fecha pasada. Debe ser mayor a la fecha y hora seleccionada
                if (selectedDT > now)
                {
                    _rotacionService = new RotacionService(); // Instanciamos
                    _rotacionService.updateRotacion(_rotacion); // Actualizar el registro en la base de datos

                    _paciente = new Paciente(); // Instanciamos
                    _paciente = _rotacionService.getPacienteByIdRotacion(_rotacion.Id); // Devuelve paciente por id rotación

                    AlarmRotacion(_rotacion);

                    // Mensaje
                    Toast.MakeText(this, "Se ha actualizado la rotación", ToastLength.Short).Show();

                    // Acción redireccionar a otra activity
                    Intent otroActivity = new Intent(this, typeof(RotacionList));
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

            // Si todos los campos estan vacios
            if (_rotacion.fecha == string.Empty || _rotacion.hora == string.Empty)
            {
                Toast.MakeText(this, "Todos los campos son obligatorios", ToastLength.Short).Show();
                return false;
            }

            return true;
        }

        private void AlarmRotacion(Rotacion _rotacion)
        {
            Intent myIntent = new Intent(this, typeof(RotacionNotification));
            //myIntent.PutExtra("reminder", JsonConvert.SerializeObject(_medicamento));

            var dateString = Convert.ToString(_rotacion.fecha + " " + _rotacion.hora); // Fecha y hora seleccionado en string

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