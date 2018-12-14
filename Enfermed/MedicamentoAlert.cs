using System;
using System.Collections.Generic;
using Android.OS;
using Android.App;
using Android.Views;
using Android.Widget;
using Enfermed.Models;
using Enfermed.Services;
using Enfermed.Adapters;

// importamos libreria AppCompatActivity
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using AlertDialog = Android.App.AlertDialog;

namespace Enfermed
{
    [Activity(Label = "Recordar Medicación", Theme = "@style/MyTheme")]
    public class MedicamentoAlert : AppCompatActivity
    {
        private ListView _listView;
        private Medicamento _medicamento;
        private List<Medicamento> _listMedicamentos;
        private MedicamentoService _medicamentoService;
        private RecordMedicamentoAdapter adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MedicamentoAlert);

            Toolbar toolBar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolBar);

            _listView = (ListView)FindViewById(Resource.Id.listReminder);

            // Mostrar lista
            BindData();
        }

        private void BindData()
        {
            _medicamentoService = new MedicamentoService(); // Instanciamos
            _listMedicamentos = _medicamentoService.getListMedicamentosNow(); // Devuelve lista

            // Si hay registros mostrar lista
            if (_listMedicamentos.Count > 0)
            {
                adapter = new RecordMedicamentoAdapter(this, _listMedicamentos);
                _listView.Adapter = adapter;

                // Lista items click
                _listView.ItemClick += List_ItemClick;
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

        // PopupMenu Exit
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.popupExit, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            // Si selecciona icon exit
            if (item.ItemId == Resource.Id.exit)
            {
                this.FinishAffinity();
                Finish();
                Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}