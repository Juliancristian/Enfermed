using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using Android.Content;
using Enfermed.Models;

namespace Enfermed.Adapters
{
    class PacienteListAdapter : BaseAdapter<Paciente>
    {
        private Activity context;
        private List<Paciente> listPaciente;

        // PacienteListAdapter pasa la Activity y el Listado de Paciente
        public PacienteListAdapter(Activity _context, List<Paciente> _listPaciente)
        {
            this.context = _context;
            this.listPaciente = _listPaciente;
        }

        // Retorna la cantidad que tenemos en la lista
        public override int Count
        {
            get { return listPaciente.Count; }
        }

        // Obtenemos el elemento en la posicion a traves del Id
        public override long GetItemId(int position)
        {
            return this[position].Id;
        }

        // Indexamos para traer el elemento en la posicion que le indiquemos
        public override Paciente this[int position] => listPaciente[position];


        // GetView para mostrar cada una de las filas de la lista
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.pacienteListRow, parent, false);
            }
            Paciente item = this[position];

            // Mostrar en pantalla
            convertView.FindViewById<TextView>(Resource.Id.txtView_NroHistoria).Text = "Historia N° " + item.NroHistoria.ToString(); //ToString: convierte de entero a string
            convertView.FindViewById<TextView>(Resource.Id.txtView_NombreApellido).Text = item.Nombre + "  " + item.Apellido;

            var _iconMenu = convertView.FindViewById<ImageView>(Resource.Id.row_click);

            // PopupMenu
            _iconMenu.Click += (s, arg) =>
            {
                PopupMenu popup = new PopupMenu(context, _iconMenu);
                popup.Inflate(Resource.Menu.popupMenu);
                popup.Show();
                popup.MenuItemClick += (sender, args) =>
                {
                    if (args.Item.ItemId == Resource.Id.edit)
                    {
                        var otroActivity = new Intent(context, typeof(PacienteEdit));
                        otroActivity.PutExtra("KEY_ID", item.Id); // Pasamos el ID
                        context.StartActivity(otroActivity);
                    }
                    if (args.Item.ItemId == Resource.Id.remove)
                    {
                        var otroActivity2 = new Intent(context, typeof(PacienteBorrar));
                        otroActivity2.PutExtra("KEY_ID", item.Id); // Pasamos el ID
                        context.StartActivity(otroActivity2);
                    }
                };
            };

            return convertView;
        }
    }
}