using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using Android.Content;
using Enfermed.Models;

namespace Enfermed.Adapters
{
    class RotacionListAdapter : BaseAdapter<Rotacion>
    {
        private Activity context;
        private List<Rotacion> listaRotacion;

        // RotacionListAdapter pasa la Activity y el Listado de Rotacion
        public RotacionListAdapter(Activity _context, List<Rotacion> _listaRotacion)
        {
            this.context = _context;
            this.listaRotacion = _listaRotacion;
        }

        // Retorna la cantidad que tenemos en la lista
        public override int Count
        {
            get { return listaRotacion.Count; }
        }

        // Obtenemos el elemento en la posicion a traves del Id
        public override long GetItemId(int position)
        {
            return this[position].Id;
        }

        // Indexamos para traer el elemento en la posicion que le indiquemos
        public override Rotacion this[int position] => listaRotacion[position];


        // GetView para mostrar cada una de las filas de la lista
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.rotacionListRow, parent, false);
            }

            Rotacion item = this[position];

            // Mostrar en pantalla
            if (item.lateralIzq == true) { convertView.FindViewById<TextView>(Resource.Id.txtPosicion).Text = "Lateral Izquierdo"; convertView.FindViewById<TextView>(Resource.Id.txtFechaHorario).Text = item.fecha + " " + item.hora; }
            if (item.lateralDer == true) { convertView.FindViewById<TextView>(Resource.Id.txtPosicion).Text = "Lateral Derecho"; convertView.FindViewById<TextView>(Resource.Id.txtFechaHorario).Text = item.fecha + " " + item.hora; }
            if (item.supina == true) { convertView.FindViewById<TextView>(Resource.Id.txtPosicion).Text = "Decúbito supino"; convertView.FindViewById<TextView>(Resource.Id.txtFechaHorario).Text = item.fecha + " " + item.hora; }
            if (item.prono == true) { convertView.FindViewById<TextView>(Resource.Id.txtPosicion).Text = "Decúbito prono"; convertView.FindViewById<TextView>(Resource.Id.txtFechaHorario).Text = item.fecha + " " + item.hora; }

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
                        var otroActivity = new Intent(context, typeof(RotacionEdit));
                        otroActivity.PutExtra("KEY_ID", item.Id); // Pasamos el ID
                        context.StartActivity(otroActivity);
                    }
                    if (args.Item.ItemId == Resource.Id.remove)
                    {
                        var otroActivity2 = new Intent(context, typeof(RotacionBorrar));
                        otroActivity2.PutExtra("KEY_ID", item.Id); // Pasamos el ID
                        context.StartActivity(otroActivity2);
                    }
                };
            };

            return convertView;
        }
    }
}