using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using Android.Content;
using Enfermed.Models;

namespace Enfermed.Adapters
{
    class MedicamentoListAdapter : BaseAdapter<Medicamento>
    {
        private Activity context;
        private List<Medicamento> listaMedicamentos;

        // MedicamentoListAdapter pasa la Activity y el Listado de Medicamentos
        public MedicamentoListAdapter(Activity _context, List<Medicamento> _listaMedicamentos)
        {
            this.context = _context;
            this.listaMedicamentos = _listaMedicamentos;
        }

        // Retorna la cantidad que tenemos en la lista
        public override int Count
        {
            get { return listaMedicamentos.Count; }
        }

        // Obtenemos el elemento en la posicion a traves del Id
        public override long GetItemId(int position)
        {
            return this[position].Id;
        }

        // Indexamos para traer el elemento en la posicion que le indiquemos
        public override Medicamento this[int position] => listaMedicamentos[position];


        // GetView para mostrar cada una de las filas de la lista
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.medicamentoListRow, parent, false);
            }

            Medicamento item = this[position];

            // Mostrar en pantalla
            convertView.FindViewById<TextView>(Resource.Id.txtFarmacoDosis).Text = item.farmaco + " " + item.dosis.ToString() + "mg.";
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
                        var otroActivity = new Intent(context, typeof(MedicamentoEdit));
                        otroActivity.PutExtra("KEY_ID", item.Id); // Pasamos el ID
                        context.StartActivity(otroActivity);
                    }
                    if (args.Item.ItemId == Resource.Id.remove)
                    {
                        var otroActivity2 = new Intent(context, typeof(MedicamentoBorrar));
                        otroActivity2.PutExtra("KEY_ID", item.Id); // Pasamos el ID
                        context.StartActivity(otroActivity2);
                    }
                };
            };

            return convertView;
        }
    }
}