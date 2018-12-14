using Android.OS;
using Android.App;
using Android.Widget;
using Enfermed.Models;
using Enfermed.Services;

// importamos libreria AppCompatActivity
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Enfermed
{
    [Activity(Label = "Medicamento", ParentActivity = typeof(PacienteList), Theme = "@style/MyTheme")]
    public class MedicamentoDetail : AppCompatActivity
    {
        internal static string KEY_ID = "KEY_ID"; // Id Paciente
        private TextView _txtFarmaco, _txtDosis, _txtViaAdmin, _txtFecha, _txtHora;
        private Medicamento _medicamento;
        private MedicamentoService _medicamentoService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MedicamentoDetail);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            // Una marca atrás en el icono en ActionBar
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            _txtFarmaco = FindViewById<TextView>(Resource.Id.txtFarmaco);
            _txtDosis = FindViewById<TextView>(Resource.Id.txtDosis);
            _txtViaAdmin = FindViewById<TextView>(Resource.Id.txtViaAdmin);
            _txtFecha = FindViewById<TextView>(Resource.Id.txtFecha);
            _txtHora = FindViewById<TextView>(Resource.Id.txtHora);

            // Recibimos el Id Paciente
            var id = Intent.Extras.GetInt(KEY_ID);
            ViewMedicamentoDetail(id);
        }

        private void ViewMedicamentoDetail(int id)
        {
            // Instanciamos
            _medicamento = new Medicamento();
            _medicamentoService = new MedicamentoService();

            // Consultamos
            _medicamento = _medicamentoService.getMedicamentoPacienteById(id); // Devuelve un medicamento de un paciente por Id

            // Mostramos los datos
            _txtFarmaco.Text = _medicamento.farmaco;
            _txtDosis.Text = _medicamento.dosis.ToString(); //ToString: convierte de entero a string
            if (_medicamento.viaOral == true) { _txtViaAdmin.Text = "Oral"; }
            if (_medicamento.viaSubcutanea == true) { _txtViaAdmin.Text = "Subcutánea"; }
            if (_medicamento.viaIntramuscular == true) { _txtViaAdmin.Text = "Intramuscular"; }
            if (_medicamento.viaIntravenoso == true) { _txtViaAdmin.Text = "Intravenoso"; }
            if (_medicamento.viaInhalatoria == true) { _txtViaAdmin.Text = "Inhalatoria"; }
            _txtFecha.Text = _medicamento.fecha;
            _txtHora.Text = _medicamento.hora;
        }
    }
}