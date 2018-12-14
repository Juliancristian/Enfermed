using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using Enfermed.Models;

namespace Enfermed.Adapters
{
    public class GuiaListAdapter : BaseAdapter<Guia>
    {
        private Activity context;
        private List<Guia> listGuia;

        // GuiaListAdapter pasa la activity y el listado de la guia
        public GuiaListAdapter(Activity _context, List<Guia> _listGuia)
        {
            this.context = _context;
            this.listGuia = _listGuia;
        }

        // Retorna la cantidad que tenemos en la lista
        public override int Count
        {
            get { return listGuia.Count; }
        }

        // Obtenemos el elemento en la posicion a traves del Id
        public override long GetItemId(int position)
        {
            return this[position].Id;
        }

        // Indexamos para traer el elemento en la posicion que le indiquemos
        public override Guia this[int position] => listGuia[position];


        // GetView para mostrar cada una de las filas de la lista
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.guiaListRow, parent, false);
            }

            Guia item = this[position];

            convertView.FindViewById<TextView>(Resource.Id.guiaTitle).Text = item.Title;
            return convertView;
        }
    }
}