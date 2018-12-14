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
    public class PacienteFragment : Fragment
    {
        private ListView _pacienteListView;
        private List<Paciente> _pacienteList;
        private PacienteService _pacienteService;

        public PacienteFragment()
        {
            // Instanciamos
            _pacienteList = new List<Paciente>();
            _pacienteService = new PacienteService();

        }

        // Cuando la actividad ha sido creado, este metodo se ejecutara cuando la actividad que contiene este fragmento sea creado
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            // Verifica si hay pacientes
            if (!_pacienteList.Any())
            {
                _pacienteService = new PacienteService();
                _pacienteList = _pacienteService.getListPacientes();
            }

            ConfigurarVistas();
            ConfigurarEventos();

            // Adaptador
            _pacienteListView.Adapter = new PacienteListAdapter(Activity, _pacienteList);
        }
        protected void ConfigurarVistas()
        {
            // Traemos el ListView de nuestro Fragmento
            _pacienteListView = View.FindViewById<ListView>(Resource.Id.pacienteListView);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Seleccionamos el layout de nuestro Fragmento
            return inflater.Inflate(Resource.Layout.pacienteFragment, container, false);
        }

        protected void ConfigurarEventos()
        {
            // Evento Click de ListView
            _pacienteListView.ItemClick += PacienteListView_ItemClick;
        }

        // Metodo se ejecuta al hacer click a un elemento del listView
        private void PacienteListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            // Intent nos permite la comunicacion entre Activitys
            Intent otroActivity = new Intent(Activity, typeof(PacienteDetail));

            // Obtiene el elemento del Id clickeado y convirtiendolo en un entero
            var id = (int)e.Id;

            // KEY_ID hace referencia a una fila en PacienteListActivity
            otroActivity.PutExtra(PacienteDetail.KEY_ID, id);
            StartActivity(otroActivity);
        }
    }
}