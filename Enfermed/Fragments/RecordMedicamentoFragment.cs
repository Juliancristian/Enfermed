using System;
using System.Collections.Generic;
using Android.OS;
using Android.Views;
using Android.Widget;
using Enfermed.Models;
using Enfermed.Services;
using Enfermed.Adapters;
using SupportFragment = Android.Support.V4.App.Fragment;
using AlertDialog = Android.App.AlertDialog;

namespace Enfermed.Fragments
{
    public class RecordMedicamentoFragment : SupportFragment
    {
        private TextView _txtMensaje;
        private ListView _listView;
        private List<Medicamento> _listMedicamentos;
        private Medicamento _medicamento;
        private MedicamentoService _medicamentoService;

        public RecordMedicamentoFragment()
        {
            // Instanciamos
            _listMedicamentos = new List<Medicamento>();
            _medicamentoService = new MedicamentoService();
        }

        // Cuando la actividad ha sido creada, este metodo se ejecutara cuando la actividad que contiene este fragmento sea creado
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            ConfigurarVistas();

            _medicamentoService = new MedicamentoService(); // Instanciamos
            _listMedicamentos = _medicamentoService.getListMedicamentos(); // Devuelve lista

            // Si hay registros, mostrar lista
            if (_listMedicamentos.Count > 0)
            {
                // Adaptador
                _listView.Adapter = new RecordMedicamentoAdapter(Activity, _listMedicamentos);

                // Lista Items Click
                _listView.ItemClick += List_ItemClick;
            }
            else
            {
                _listView.Visibility = ViewStates.Gone; // ListView Invisible
                _txtMensaje.Visibility = View.Visibility; // TextView Visible
                _txtMensaje.Text = "No hay registros";
            }

        }
        protected void ConfigurarVistas()
        {
            // Traemos el ListView de nuestro Fragmento
            _listView = View.FindViewById<ListView>(Resource.Id.listReminder);
            _txtMensaje = View.FindViewById<TextView>(Resource.Id.txtMensaje);
            _txtMensaje.Visibility = ViewStates.Gone; // TextView Invisible
        }

        private void List_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            // Item Medicamento
            _medicamento = _listMedicamentos[e.Position];

            if (_medicamento.confirmar == true)
            {
                Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(Activity);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Medicamento");
                alert.SetMessage("Ya ha sido confirmado!");
                alert.SetButton("Cerrar", delegate
                {
                    alert.Dispose();
                });

                alert.Show();
            }
            else
            {
                AlertDialog.Builder dialog = new AlertDialog.Builder(Activity);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Medicamento");
                alert.SetIcon(Resource.Drawable.logo);
                alert.SetButton("Confirmar", (c, ev) =>
                {
                    //_medicamento = _listMedicamentos[e.Position];
                    _medicamento.confirmar = true; // Confirmar Medicamento
                    _medicamentoService.updateMedicamento(_medicamento); // Actualiza el registro en la base de datos

                    // Para refresh
                    Activity.Finish(); StartActivity(Activity.Intent);

                    GC.Collect();
                });

                alert.SetButton2("Omitir", (c, ev) => { });
                alert.Show();
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Seleccionamos el layout de nuestro Fragmento
            return inflater.Inflate(Resource.Layout.recordMedicamentoFragment, container, false);
        }
    }
}