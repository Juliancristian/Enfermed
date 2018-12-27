using System;
using System.Linq;
using System.Collections.Generic;
using Android.OS;
using Android.App;
using Android.Text;
using Android.Views;
using Android.Speech;
using Android.Widget;
using Android.Content;
using Enfermed.Models;
using Enfermed.Services;
using Enfermed.Adapters;

namespace Enfermed.Fragments
{
    public class GuiaFragment : Fragment
    {
        private readonly int VOICE = 10;
        private ListView _guiaListView;
        private ImageButton _btnMic;
        private EditText _edtSearch;
        private List<Guia> _guiaList;
        private GuiaService _guiaService;

        public GuiaFragment()
        {
            // Instanciamos
            _guiaList = new List<Guia>();
            _guiaService = new GuiaService();
        }

        // Cuando la actividad ha sido creado, este metodo se ejecutara cuando la actividad que contiene este fragmento sea creado
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            // Verifica si hay guia
            if (!_guiaList.Any())
            {
                _guiaService = new GuiaService();
                _guiaList = _guiaService.getListGuia();
            }

            ConfigurarVistas();
            ConfigurarEventos();

            // Adaptador
            _guiaListView.Adapter = new GuiaListAdapter(Activity, _guiaList);
        }
        protected void ConfigurarVistas()
        {
            _btnMic = View.FindViewById<ImageButton>(Resource.Id.btnMic); // Mic
            _edtSearch = View.FindViewById<EditText>(Resource.Id.edtSearch); // Search

            _guiaService = new GuiaService(); // Instanciamos
            _edtSearch.TextChanged += InputSearch_TextChanged; // Edittext Search

            _btnMic.Click += StartSearchVoice; // Click Mic

            // Traemos el ListView de nuestro Fragmento
            _guiaListView = View.FindViewById<ListView>(Resource.Id.guiaListView);
        }

        private void InputSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Si no esta vacio el campo Search           
            if (_edtSearch.Text != string.Empty)
            {
                var searchText = _edtSearch.Text;
                _guiaList = _guiaService.searchGuiaByName(searchText + "%"); // Consulta en la base de datos

                // Adaptador
                _guiaListView.Adapter = new GuiaListAdapter(Activity, _guiaList);
            }
            // Caso contrario, devuelve la lista 
            else
            {
                _guiaService = new GuiaService(); // Instanciamos
                _guiaList = _guiaService.getListGuia(); // Devuelve la lista guia

                // Adaptador
                _guiaListView.Adapter = new GuiaListAdapter(Activity, _guiaList);
            }
        }

        private void StartSearchVoice(object sender, EventArgs e)
        {
            Intent intent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
            intent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);

            // Mensaje en el diálogo modal
            intent.PutExtra(RecognizerIntent.ExtraPrompt, "Hable ahora!");

            intent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1500);
            intent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1500);
            intent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
            intent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);
            intent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default);
            try
            {
                StartActivityForResult(intent, VOICE);
            }
            catch (ActivityNotFoundException)
            {
            }
        }

        public override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == VOICE)
            {
                if (resultCode == Result.Ok)
                {
                    var matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
                    if (matches.Count != 0)
                    {
                        string textInput = _edtSearch.Text + matches[0];

                        // limite de salida para 500 caracteres
                        if (textInput.Length > 500)
                            textInput = textInput.Substring(0, 500);
                            _edtSearch.Text = textInput;
                    }
                    else
                    {
                        _edtSearch.Text = "No se reconoció ningún discurso.";
                    }
                }
            }

            base.OnActivityResult(requestCode, resultCode, data);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Seleccionamos el layout de nuestro Fragmento
            return inflater.Inflate(Resource.Layout.guiaFragment, container, false);
        }

        protected void ConfigurarEventos()
        {
            // Evento Click de ListView
            _guiaListView.ItemClick += GuiaListView_ItemClick;
        }

        // Metodo se ejecuta al hacer click a un elemento del listView
        private void GuiaListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            // Intent nos permite la comunicacion entre Activitys o Fragments
            Intent otroActivity = new Intent(Activity, typeof(GuiaDetail));

            // Obtiene el elemento del Id clickeado y convirtiendolo en un entero
            var id = (int)e.Id;

            // KEY_ID hace referencia a una fila en GuiaDetail
            otroActivity.PutExtra(GuiaDetail.KEY_ID, id);
            StartActivity(otroActivity);
        }
    }
}