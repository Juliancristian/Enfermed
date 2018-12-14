using Android.OS;
using Android.App;
using Android.Views;
using Android.Widget;
using Android.Content;
using Enfermed.Models;
using Enfermed.Services;

// importamos libreria AppCompatActivity
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Enfermed
{
    [Activity(Label = "Paciente", ParentActivity = typeof(PacienteList), Theme = "@style/MyTheme")]
    public class PacienteDetail : AppCompatActivity
    {
        internal static string KEY_ID = "KEY_ID"; // Id Paciente
        private TextView _txtNroHistoria, _txtNombre, _txtApellido, _txtEdad, _txtGenero, _txtNroHabitacion, _txtNroCama;
        private BottomNavigationView _navigation;
        private Paciente _paciente;
        private PacienteService _pacienteService;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PacienteDetail);

            Toolbar toolbarTop = FindViewById<Toolbar>(Resource.Id.toolbarTop);
            SetSupportActionBar(toolbarTop);

            // Una marca atrás en el icono en ActionBar
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            _txtNroHistoria = FindViewById<TextView>(Resource.Id.txtNroHistoria);
            _txtNombre = FindViewById<TextView>(Resource.Id.txtNombre);
            _txtApellido = FindViewById<TextView>(Resource.Id.txtApellido);
            _txtEdad = FindViewById<TextView>(Resource.Id.txtEdad);
            _txtGenero = FindViewById<TextView>(Resource.Id.txtGenero);
            _txtNroHabitacion = FindViewById<TextView>(Resource.Id.txtNroHabit);
            _txtNroCama = FindViewById<TextView>(Resource.Id.txtNroCama);

            // Bottom Navigation View (Medicamento | Rotacion)
            _navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            _navigation.InflateMenu(Resource.Menu.bottomNavigation);

            // Deshabilitar Checked Default
            IMenuItem menuItem = _navigation.Menu.GetItem(1);
            menuItem.SetCheckable(false);
            _navigation.Menu.GetItem(1).SetChecked(false);


            // Items Medicamento | Rotacion
            _navigation.NavigationItemSelected += Navigation_NavigationItemSelected;

            // Recibimos el Id Paciente
            var idPaciente = Intent.Extras.GetInt(KEY_ID);
            ViewPacienteDetail(idPaciente);
        }

        private void ViewPacienteDetail(int idPaciente)
        {
            // Instanciamos
            _paciente = new Paciente();
            _pacienteService = new PacienteService();

            // Consultamos
            _paciente = _pacienteService.getPacienteById(idPaciente); // Devuelve un paciente por Id

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

        private void Navigation_NavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            // Recibimos el Id Paciente
            var idPaciente = Intent.Extras.GetInt(KEY_ID);

            switch (e.Item.ItemId)
            {
                // Medicamentos
                case Resource.Id.icon_medicamento:
                    Intent intentMed = new Intent(this, typeof(MedicamentoList));
                    intentMed.PutExtra("KEY_ID", idPaciente); // Pasamos el Id Paciente
                    StartActivity(intentMed);
                    break;

                // Rotacion
                case Resource.Id.icon_rotacion:
                    Intent intentRot = new Intent(this, typeof(RotacionList));
                    intentRot.PutExtra("KEY_ID", idPaciente); // Pasamos el Id Paciente
                    StartActivity(intentRot);
                    break;
            }
        }
    }
}