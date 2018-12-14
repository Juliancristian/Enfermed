using Android.OS;
using Android.App;
using Android.Widget;
using Android.Content;


// importamos libreria AppCompatActivity
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Enfermed.Models;
using Enfermed.Services;

namespace Enfermed
{
    [Activity(Label = "Antibiótico", ParentActivity = typeof(GuiaList), Theme = "@style/MyTheme")]
    public class GuiaDetail : AppCompatActivity
    {
        internal static string KEY_ID = "KEY_ID"; // Id Guia
        private TextView _guiaTitle, _guiaBody;
        private Guia _guia;
        private GuiaService _guiaService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.GuiaDetail);

            Toolbar toolBar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolBar);

            // Una marca atrás en el icono en ActionBar
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            _guiaTitle = FindViewById<TextView>(Resource.Id.guiaTitle);
            _guiaBody = FindViewById<TextView>(Resource.Id.guiaBody);

            // Recibimos el ID Guia
            var id = Intent.Extras.GetInt(KEY_ID);
            ViewGuiaDetail(id);
        }

        private void ViewGuiaDetail(int id)
        {
            // Consultamos
            _guiaService = new GuiaService();
            _guia = _guiaService.getGuiaById(id); // Devuelve una guia por Id

            // Mostramos los datos
            _guiaTitle.Text = _guia.Title;
            _guiaBody.Text = _guia.Body;
        }
    }
}