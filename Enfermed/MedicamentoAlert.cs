using System;
using System.Linq;
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
    [Activity(Label = "Recordar Medicaci�n", Theme = "@style/MyTheme")]
    public class MedicamentoAlert : AppCompatActivity
    {
        private ListView _listView;
        private Medicamento _medicamento;
        private List<Medicamento> _listMedicamentos;
        private MedicamentoService _medicamentoService;

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

            // Si hay registros
            if (_listMedicamentos.Any())
            {
                _listView.Adapter = new RecordMedicamentoAdapter(this, _listMedicamentos);

                // Lista items click
                _listView.ItemClick += List_ItemClick;
            }
            else
            {
                this.FinishAffinity();
                Finish();
                Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            }
        }

        private void List_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            AlertDialog.Builder dialog = new AlertDialog.Builder(this);
            AlertDialog alert = dialog.Create();
            alert.SetTitle("Medicamento");
            alert.SetIcon(Resource.Drawable.logo);
            alert.SetButton("Confirmar", (c, ev) =>
            {
                _medicamento = _listMedicamentos[e.Position];
                _medicamento.confirmar = true; // Confirmar Medicamento
                _medicamentoService.updateMedicamento(_medicamento); // Actualiza el registro en la base de datos

                // Para actualizar una actividad desde dentro de s� mismo
                Finish(); StartActivity(Intent);

                GC.Collect();
            });

            alert.SetButton2("Omitir", (c, ev) => { });
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
                AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Salir");
                alert.SetMessage("�Est�s seguro?");
                alert.SetIcon(Resource.Drawable.logo);
                alert.SetButton("Si", (c, ev) =>
                {
                    this.FinishAffinity();
                    Finish();
                    Android.OS.Process.KillProcess(Android.OS.Process.MyPid());

                    GC.Collect();
                });

                alert.SetButton2("no", (c, ev) => { });
                alert.Show();
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}