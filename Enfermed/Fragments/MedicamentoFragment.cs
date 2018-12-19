using System.Collections.Generic;
using System.Linq;
using Android.OS;
using Android.App;
using Android.Views;
using Android.Widget;
using Android.Content;
using Enfermed.Models;
using Enfermed.Services;
using Enfermed.Adapters;

namespace Enfermed.Fragments
{
    public class MedicamentoFragment : Fragment
    {
        internal static string KEY_ID = "KEY_ID"; // Id Paciente      
        private ListView _medicamentoListView;
        private Paciente _paciente;
        private List<Medicamento> _medicamentoList;
        private MedicamentoService _medicamentoService;

        public MedicamentoFragment()
        {
            // Instanciamos
            _medicamentoList = new List<Medicamento>();
            _medicamentoService = new MedicamentoService();
        }

        // Cuando la actividad ha sido creada, este metodo se ejecutara cuando la actividad que contiene este fragmento sea creado
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            // Verificar si hay medicamentos
            if (!_medicamentoList.Any())
            {
                // Recibimos el Id paciente
                var idPaciente = this.Activity.Intent.Extras.GetInt(KEY_ID);

                // Consultamos la lista medicamentos en el paciente
                _medicamentoService = new MedicamentoService();
                _medicamentoList = _medicamentoService.getMedicamentosPacienteById(idPaciente);

            }
            ConfigurarVistas();
            ConfigurarEventos();

            // Adaptador
            _medicamentoListView.Adapter = new MedicamentoListAdapter(Activity, _medicamentoList);
        }
        protected void ConfigurarVistas()
        {
            // Traemos el ListView de nuestro Fragmento
            _medicamentoListView = View.FindViewById<ListView>(Resource.Id.medicamentoListView);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Seleccionamos el layout de nuestro Fragmento
            return inflater.Inflate(Resource.Layout.medicamentoFragment, container, false);
        }

        protected void ConfigurarEventos()
        {
            // Evento Click de ListView
            _medicamentoListView.ItemClick += MedicamentoListView_ItemClick;
        }

        // Metodo se ejecuta al hacer click a un elemento del listView
        private void MedicamentoListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            // Obtiene el elemento del Id clickeado y convirtiendolo en un entero
            var idMedicamento = (int)e.Id;

            // Acción redireccionar a otra activity
            Intent otroActivity = new Intent(Activity, typeof(MedicamentoDetail));
            otroActivity.PutExtra(MedicamentoDetail.KEY_ID, idMedicamento); // Pasamos el Id Medicamento
            StartActivity(otroActivity);
        }
    }
}