using System.Collections.Generic;
using Android.OS;
using Android.App;
using Enfermed.Models;
using Enfermed.Services;
using Enfermed.Fragments;

// importamos libreria AppCompatActivity
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Enfermed
{
    [Activity(Label = "Guía Parenteral", ParentActivity = typeof(Panel), Theme = "@style/MyTheme")]
    public class GuiaList : AppCompatActivity
    {
        private GuiaService _guiaService;
        private List<Guia> _listGuia;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.GuiaList);

            Toolbar toolBar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolBar);

            // Una marca atrás en el icono en ActionBar
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            //Instanciamos
            _listGuia = new List<Guia>();
            _guiaService = new GuiaService();

            // Devuelve una lista guia
            _listGuia = _guiaService.getListGuia();

            // Precargar la guia
            LoadGuia();

            // Si Bundle esta vacio, mostrar contenedor de la lista
            if (savedInstanceState == null)
            {
                // Iniciamos una transaccion y lo guarda en una variable
                var transaction = FragmentManager.BeginTransaction();

                // Agregar en pantalla el fragmento
                transaction.Add(Resource.Id.guiaListFragmentContainer, new GuiaFragment());

                // para aplicar la transacción a la actividad, se debe llamar a commit().
                transaction.Commit();
            }
        }

        private void LoadGuia()
        {
            // Si no hay registros
            if (_listGuia.Count == 0)
            {
                _guiaService.loadDataGuia();
            }
        }
    }
}