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
    public class RotacionFragment : Fragment
    {
        internal static string KEY_ID = "KEY_ID"; // Id Paciente
        private ListView _rotacionListView;
        private List<Rotacion> _rotacionList;
        private RotacionService _rotacionService;

        public RotacionFragment()
        {
            // Instanciamos
            _rotacionList = new List<Rotacion>();
            _rotacionService = new RotacionService();

        }

        // Cuando la actividad ha sido creada, este metodo se ejecutara cuando la actividad que contiene este fragmento sea creado
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            // Verificar si hay rotacion
            if (!_rotacionList.Any())
            {
                // Recibimos el Id paciente
                var idPaciente = this.Activity.Intent.Extras.GetInt(KEY_ID);

                // Consultamos la lista rotación en el paciente
                _rotacionService = new RotacionService();
                _rotacionList = _rotacionService.getRotacionesPacienteById(idPaciente);
            }

            ConfigurarVistas();
            ConfigurarEventos();

            // Adaptador
            _rotacionListView.Adapter = new RotacionListAdapter(Activity, _rotacionList);
        }
        protected void ConfigurarVistas()
        {
            // Traemos el ListView de nuestro Fragmento
            _rotacionListView = View.FindViewById<ListView>(Resource.Id.rotacionListView);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Seleccionamos el layout de nuestro Fragmento
            return inflater.Inflate(Resource.Layout.rotacionFragment, container, false);
        }

        protected void ConfigurarEventos()
        {
            // Evento Click de ListView
            _rotacionListView.ItemClick += RotacionListView_ItemClick;
        }

        // Metodo se ejecuta al hacer click a un elemento del listView
        private void RotacionListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            // Obtiene el elemento del Id clickeado y convirtiendolo en un entero
            var idRotacion = (int)e.Id;

            // Acción redireccionar a otra activity
            Intent otroActivity = new Intent(Activity, typeof(RotacionDetail));
            otroActivity.PutExtra(RotacionDetail.KEY_ID, idRotacion);
            StartActivity(otroActivity);
        }
    }
}