using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using Enfermed.Models;
using Enfermed.Services;
using Android.Graphics;

namespace Enfermed.Adapters
{
    class RecordMedicamentoAdapter : BaseAdapter<Medicamento>
    {
        private Activity context;
        private List<Medicamento> _listMedicamentos;
        private MedicamentoService _medicamentoService;
        private Paciente _paciente;

        // RecordMedicamentoAdapter pasa la activity y el listado de medicamentos
        public RecordMedicamentoAdapter(Activity context, List<Medicamento> _listMedicamentos)
        {
            this.context = context;
            this._listMedicamentos = _listMedicamentos;
        }


        // Retorna la cantidad que tenemos en la lista
        public override int Count
        {
            get { return _listMedicamentos.Count; }
        }

        // Obtenemos el elemento en la posicion a traves del Id
        public override long GetItemId(int position)
        {
            return this[position].Id;
        }

        // Indexamos para traer el elemento en la posicion que le indiquemos
        public override Medicamento this[int position] => _listMedicamentos[position];


        // GetView para mostrar cada una de las filas de la lista
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.recordMedicamentoListRow, parent, false);
            }

            Medicamento item = this[position];

            _paciente = new Paciente(); // Instanciamos Paciente
            _medicamentoService = new MedicamentoService(); // Instanciamos PacienteService
            _paciente = _medicamentoService.getPacienteByIdMedicamento(item.Id); // Devuelve un paciente

            DateTime nowDate = DateTime.Now.Date; // Fecha actual
            DateTime selectedDT = Convert.ToDateTime(item.fecha); // Fecha registrado
            if(item.confirmar == true)
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
                convertView.FindViewById<TextView>(Resource.Id.txtFarmacoDosis).Text = item.farmaco + "  " + item.dosis.ToString() + "mg."; //ToString: convierte de entero a string            
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
                convertView.FindViewById<TextView>(Resource.Id.txtFarmacoDosis).Text = item.farmaco + "  " + item.dosis.ToString() + "mg."; //ToString: convierte de entero a string            
                convertView.FindViewById<TextView>(Resource.Id.txtHabiCama).Text = "Habitación N°" + _paciente.NroHabitacion.ToString() + " - " + "Cama: " + _paciente.NroCama;               
                convertView.FindViewById<TextView>(Resource.Id.txtNomApe).Text = _paciente.Nombre + " " + _paciente.Apellido;
                convertView.FindViewById<TextView>(Resource.Id.txtNoConfirmado).Text = "Pendiente";
            }

            return convertView;
        }
    }
}