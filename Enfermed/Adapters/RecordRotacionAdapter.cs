using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using Enfermed.Models;
using Enfermed.Services;

namespace Enfermed.Adapters
{
    class RecordRotacionAdapter : BaseAdapter<Rotacion>
    {
        private Activity context;
        private List<Rotacion> _listRotacion;
        private RotacionService _rotacionService;
        private Paciente _paciente;

        // RecordatorioRListAdapter pasa la activity y el listado de Rotacion
        public RecordRotacionAdapter(Activity context, List<Rotacion> _listRotacion)
        {
            this.context = context;
            this._listRotacion = _listRotacion;
        }


        // Retorna la cantidad que tenemos en la lista
        public override int Count
        {
            get { return _listRotacion.Count; }
        }

        // Obtenemos el elemento en la posicion a traves del Id
        public override long GetItemId(int position)
        {
            return this[position].Id;
        }

        // Indexamos para traer el elemento en la posicion que le indiquemos
        public override Rotacion this[int position] => _listRotacion[position];


        // GetView para mostrar cada una de las filas de la lista
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.recordRotacionListRow, parent, false);
            }

            Rotacion item = this[position];

            _paciente = new Paciente(); // Instanciamos Paciente
            _rotacionService = new RotacionService(); // Instanciamos PacienteService
            _paciente = _rotacionService.getPacienteByIdRotacion(item.Id); // Devuelve un paciente

            DateTime nowDate = DateTime.Now.Date; // Fecha actual
            DateTime selectedDT = Convert.ToDateTime(item.fecha); // Fecha registrado
            if (item.confirmar == true)
            {
                // Si la fecha registrada es mayor a la actual
                if (selectedDT > nowDate)
                {
                    convertView.FindViewById<TextView>(Resource.Id.txtFechaHorario).Text = item.fecha + " " + item.hora; // Mostrar Fecha/Hora
                }
                else
                {
                    convertView.FindViewById<TextView>(Resource.Id.txtFechaHorario).Text = item.hora; // Mostrar Hora
                }

                // Mostrar
                if (item.lateralIzq == true) { convertView.FindViewById<TextView>(Resource.Id.txtPosicion).Text = "Lateral Izquierdo"; }
                if (item.lateralDer == true) { convertView.FindViewById<TextView>(Resource.Id.txtPosicion).Text = "Lateral Derecho"; }
                if (item.supina == true) { convertView.FindViewById<TextView>(Resource.Id.txtPosicion).Text = "Decúbito supino"; }
                if (item.prono == true) { convertView.FindViewById<TextView>(Resource.Id.txtPosicion).Text = "Decúbito prono"; }

                convertView.FindViewById<TextView>(Resource.Id.txtHabiCama).Text = "Habitación N°" + _paciente.NroHabitacion.ToString() + " - " + "Cama: " + _paciente.NroCama;
                convertView.FindViewById<TextView>(Resource.Id.txtConfirmado).Text = "Confirmado";
                convertView.FindViewById<TextView>(Resource.Id.txtNomApe).Text = _paciente.Nombre + " " + _paciente.Apellido;
            }
            else
            {
                // Si la fecha registrada es mayor a la actual
                if (selectedDT > nowDate)
                {
                    convertView.FindViewById<TextView>(Resource.Id.txtFechaHorario).Text = item.fecha + " " + item.hora; // Mostrar Fecha/Hora
                }
                else
                {
                    convertView.FindViewById<TextView>(Resource.Id.txtFechaHorario).Text = item.hora; // Mostrar Hora
                }

                // Mostrar
                if (item.lateralIzq == true) { convertView.FindViewById<TextView>(Resource.Id.txtPosicion).Text = "Lateral Izquierdo"; }
                if (item.lateralDer == true) { convertView.FindViewById<TextView>(Resource.Id.txtPosicion).Text = "Lateral Derecho"; }
                if (item.supina == true) { convertView.FindViewById<TextView>(Resource.Id.txtPosicion).Text = "Decúbito supino"; }
                if (item.prono == true) { convertView.FindViewById<TextView>(Resource.Id.txtPosicion).Text = "Decúbito prono"; }

                convertView.FindViewById<TextView>(Resource.Id.txtHabiCama).Text = "Habitación N°" + _paciente.NroHabitacion.ToString() + " - " + "Cama: " + _paciente.NroCama;
                convertView.FindViewById<TextView>(Resource.Id.txtNomApe).Text = _paciente.Nombre + " " + _paciente.Apellido;
                convertView.FindViewById<TextView>(Resource.Id.txtNoConfirmado).Text = "Pendiente";
            }

            return convertView;
        }
    }
}