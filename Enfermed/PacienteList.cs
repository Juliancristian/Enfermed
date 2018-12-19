using Android.OS;
using Android.App;
using Android.Views;
using Android.Content;
using Enfermed.Fragments;

// importamos libreria AppCompatActivity
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Enfermed
{
    [Activity(Label = "Lista Pacientes", ParentActivity = typeof(Panel), Theme = "@style/MyTheme")]
    public class PacienteList : AppCompatActivity
    {
        private FloatingActionButton _btnAdd;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PacienteList);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            // Una marca atrás en el icono en ActionBar
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            // Si Bundle esta vacio, mostrar contenedor de la lista
            if (savedInstanceState == null)
            {
                // Iniciamos una transaccion y lo guarda en una variable
                var transaction = FragmentManager.BeginTransaction();

                // Agregar en pantalla el fragmento
                transaction.Add(Resource.Id.pacienteListFragmentContainer, new PacienteFragment());

                // para aplicar la transacción a la actividad, se debe llamar a commit().
                transaction.Commit();
            }

            // Boton Flotante
            _btnAdd = FindViewById<FloatingActionButton>(Resource.Id.btnAdd);
            _btnAdd.Click += (sender, args) =>
            {
                // Acción redireccionar a otra activity
                StartActivity(new Intent(this, typeof(PacienteAdd)));
            };
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.popupMenuCode, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            // Si selecciona icon codebarras
            if (item.ItemId == Resource.Id.codebarras)
            {
                // Acción redireccionar a otra activity
                StartActivity(new Intent(this, typeof(ScanCodeBarras)));
            }
            else
            {
                // Acción redireccionar a otra activity
                StartActivity(new Intent(this, typeof(ScanCodeQR)));
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}