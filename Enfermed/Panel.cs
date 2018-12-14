using Android.OS;
using Android.App;
using Android.Views;
using Android.Content;

// importamos libreria AppCompatActivity
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;

namespace Enfermed
{
    [Activity(Label = "Enfermed", Theme = "@style/MyTheme")]
    public class Panel : AppCompatActivity
    {
        private CardView _btnRecordatorio, _btnCalculadora, _btnGuia, _btnPacientes;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Panel);

            // Traemos la barra toobar
            var toolBar = FindViewById<SupportToolbar>(Resource.Id.toolbar);

            // Toolbar ahora asumira las características predeterminadas de la barra de acciones
            SetSupportActionBar(toolBar);

            // Traemos el boton para la siguiente Activity: RECORDATORIO
            _btnRecordatorio = FindViewById<CardView>(Resource.Id.btnRecordatorio);
            _btnRecordatorio.Click += (sender, args) =>
            {
                /* 
                Iniciando actividades con Intents.
                Se puede usar las intents para varias tareas, pero en este caso el intent inicia otra activity.           
                */
                var otroActivity = new Intent(this, typeof(Recordatorio));
                StartActivity(otroActivity); // Este metodo es utilizado para empezar una nueva actividad.
            };

            // Traemos el boton para la siguiente Activity: CALCULADORA
            _btnCalculadora = FindViewById<CardView>(Resource.Id.btnCalculadora);
            _btnCalculadora.Click += (sender, args) =>
            {
                var otroActivity = new Intent(this, typeof(Calculadora));
                StartActivity(otroActivity);
            };

            // Traemos el boton para la siguiente Activity: GUIA
            _btnGuia = FindViewById<CardView>(Resource.Id.btnGuia);
            _btnGuia.Click += (sender, args) =>
            {
                var otroActivity = new Intent(this, typeof(GuiaList));
                StartActivity(otroActivity);
            };

            // Traemos el boton para la siguiente Activity: PACIENTES
            _btnPacientes = FindViewById<CardView>(Resource.Id.btnPacientes);
            _btnPacientes.Click += (sender, args) =>
            {
                var otroActivity = new Intent(this, typeof(PacienteList));
                StartActivity(otroActivity);
            };
        }

        // PopupMenu Exit
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.popupExit, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            // Si selecciona icon exit
            if (item.ItemId == Resource.Id.exit)
            {
                this.FinishAffinity();
                Finish();
                Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}