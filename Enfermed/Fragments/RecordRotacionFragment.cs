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
    public class RecordRotacionFragment : SupportFragment
    {
        private TextView _txtMensaje;
        private ListView _listView;
        private Rotacion _rotacion;
        private List<Rotacion> _listRotacion;
        private RotacionService _rotacionService;

        public RecordRotacionFragment()
        {
            // Instanciamos
            _listRotacion = new List<Rotacion>();
            _rotacionService = new RotacionService();
        }

        // Cuando la actividad ha sido creada, este metodo se ejecutara cuando la actividad que contiene este fragmento sea creado
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            ConfigurarVistas();

            _rotacionService = new RotacionService(); // Instanciamos
            _listRotacion = _rotacionService.getListRotacion(); // Devuelve lista

            // Si hay registros, mostrar lista
            if (_listRotacion.Count > 0)
            {
                // Adaptador
                _listView.Adapter = new RecordRotacionAdapter(Activity, _listRotacion);

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
            // Item Rotacion
            _rotacion = _listRotacion[e.Position];

            if (_rotacion.confirmar == true)
            {
                Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(Activity);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Rotación");
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
                alert.SetTitle("Rotación");
                alert.SetIcon(Resource.Drawable.logo);
                alert.SetButton("Confirmar", (c, ev) =>
                {
                    //_rotacion = _listRotacion[e.Position];
                    _rotacion.confirmar = true; // Confirmar Rotación
                    _rotacionService.updateRotacion(_rotacion); // Actualiza el registro en la base de datos

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
            return inflater.Inflate(Resource.Layout.recordRotacionFragment, container, false);
        }
    }
}