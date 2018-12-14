using System;
using System.Collections.Generic;
using Android.OS;
using Android.App;
using Android.Views;
using Android.Widget;
using Android.Content;
using Enfermed.Models;
using Enfermed.Services;
using Enfermed.Adapters;

// importamos libreria AppCompatActivity
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using AlertDialog = Android.App.AlertDialog;
using Android.Support.Design.Widget;

namespace Enfermed
{
    [Activity(Label = "Medicamentos", ParentActivity = typeof(Panel), Theme = "@style/MyTheme")]
    public class Recordatorio : AppCompatActivity
    {
        private TextView _txtMensaje;
        private ListView _listView;
        private Medicamento _medicamento;
        private List<Medicamento> _listMedicamentos;
        private MedicamentoService _medicamentoService;
        private RecordMedicamentoAdapter adapter;
        private BottomNavigationView _navigation;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Recordatorio);

            Toolbar toolbarTop = FindViewById<Toolbar>(Resource.Id.toolbarTop);
            SetSupportActionBar(toolbarTop);

            // Una marca atrás en el icono en ActionBar
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            _txtMensaje = FindViewById<TextView>(Resource.Id.txtMensaje);
            _listView = (ListView)FindViewById(Resource.Id.listReminder);
            _txtMensaje.Visibility = Android.Views.ViewStates.Invisible;

            // Bottom Navigation View (Medicamento | Rotacion)
            _navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            _navigation.InflateMenu(Resource.Menu.bottomNavigation);

            // Deshabilitar Checked Default

            IMenuItem menuItem = _navigation.Menu.GetItem(1);
            menuItem.SetCheckable(false);
            _navigation.Menu.GetItem(1).SetChecked(false);

            // Items Medicamento | Rotacion
            _navigation.NavigationItemSelected += Navigation_NavigationItemSelected;

            // Mostrar lista o mensaje
            BindData();
        }

        private void BindData()
        {
            _medicamentoService = new MedicamentoService(); // Instanciamos
            _listMedicamentos = _medicamentoService.getListMedicamentos(); // Devuelve lista

            // Si hay registros, mostrar lista
            if (_listMedicamentos.Count > 0)
            {
                adapter = new RecordMedicamentoAdapter(this, _listMedicamentos);
                _listView.Adapter = adapter;

                // LISTA ITEMS CLICK
                _listView.ItemClick += List_ItemClick;
            }
            else
            {
                _listView.Visibility = Android.Views.ViewStates.Invisible;
                _txtMensaje.Visibility = Android.Views.ViewStates.Visible;
                _txtMensaje.Text = "No hay registros";
            }
        }

        private void List_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            AlertDialog.Builder dialog = new AlertDialog.Builder(this);
            AlertDialog alert = dialog.Create();
            alert.SetTitle("Confirmar");
            //alert.SetMessage("¿Estás seguro?");
            alert.SetIcon(Resource.Drawable.logo);
            alert.SetButton("Si", (c, ev) =>
            {
                _medicamento = _listMedicamentos[e.Position];
                _medicamento.confirmar = true; // Confirmar Medicamento
                _medicamentoService.updateMedicamento(_medicamento); // Actualiza el registro en la base de datos

                // Para actualizar una actividad desde dentro de sí mismo
                Finish(); StartActivity(Intent);

                GC.Collect();
            });

            alert.SetButton2("no", (c, ev) => { });
            alert.Show();
        }

        private void Navigation_NavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            switch (e.Item.ItemId)
            {
                // Medicamentos
                case Resource.Id.icon_medicamento:

                    // Acción redireccionar a otra activity
                    StartActivity(new Intent(this, typeof(Recordatorio)));
                    break;

                // Rotacion
                case Resource.Id.icon_rotacion:

                    // Acción redireccionar a otra activity
                    StartActivity(new Intent(this, typeof(RecordatorioRotacion)));
                    break;
            }
        }
    }
}