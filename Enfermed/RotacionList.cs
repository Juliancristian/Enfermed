using Android.OS;
using Android.App;
using Android.Content;
using Enfermed.Fragments;

// importamos libreria AppCompatActivity
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Enfermed
{
    [Activity(Label = "Lista Rotaci�n", ParentActivity = typeof(PacienteList), Theme = "@style/MyTheme")]
    public class RotacionList : AppCompatActivity
    {
        internal static string KEY_ID = "KEY_ID"; // Id Paciente
        private FloatingActionButton _btnAdd;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RotacionList);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            // Una marca atr�s en el icono en ActionBar
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            // Recibimos el Id Paciente
            var idPaciente = Intent.Extras.GetInt(KEY_ID);

            // Pasamos la variable al fragmento
            Intent i = new Intent(this, typeof(RotacionFragment));
            i.PutExtra(RotacionFragment.KEY_ID, idPaciente);

            // Si Bundle esta vacio, mostrar contenedor de la lista
            if (savedInstanceState == null)
            {
                // Iniciamos una transaccion y lo guarda en una variable
                var transaction = FragmentManager.BeginTransaction();

                // Agregar en pantalla el fragmento
                transaction.Add(Resource.Id.rotacionListFragmentContainer, new RotacionFragment());

                // para aplicar la transacci�n a la actividad, se debe llamar a commit().
                transaction.Commit();
            }

            // Boton Flotante
            _btnAdd = FindViewById<FloatingActionButton>(Resource.Id.btnAdd);
            _btnAdd.Click += (sender, args) =>
            {
                // Acci�n redireccionar a otra activity
                Intent otroActivity = new Intent(this, typeof(RotacionAdd));
                otroActivity.PutExtra(RotacionAdd.KEY_ID, idPaciente); // KEY_ID hace referencia a RotacionAddActivity
                StartActivity(otroActivity);
            };
        }
    }
}