using System.Collections.Generic;
using Enfermed.Models;

namespace Enfermed.Services
{
    public class RotacionService
    {
        private PacienteRepository _pacienteRepository;

        public RotacionService()
        {
            _pacienteRepository = new PacienteRepository(ValuesService.GetDbPath());
        }

        // Metodo Agregar Rotaci�n de Paciente

        public void addRotacion(Paciente pacient, Rotacion rotacion)
        {
            _pacienteRepository.addRotacion(pacient, rotacion);
        }

        // Metodo Actualizar Rotaci�n de Paciente
        public void updateRotacion(Rotacion rotacion)
        {
            _pacienteRepository.updateRotacion(rotacion);
        }

        // Metodo Eliminar Rotaci�n de Paciente
        public void deleteRotacion(int Id)
        {
            _pacienteRepository.deleteRotacion(Id);
        }

        // Metodo Devuelve Una Lista de Rotaciones del Paciente por Id
        public List<Rotacion> getRotacionesPacienteById(int Id)
        {
            return _pacienteRepository.getRotacionesPacienteById(Id);
        }

        // Devuelve Una Rotaci�n de Paciente por Id
        public Rotacion getRotacionPacienteById(int Id)
        {
            return _pacienteRepository.getRotacionPacienteById(Id);
        }

        // Devuelve Una Lista de Rotaci�n
        public List<Rotacion> getListRotacion()
        {
            return _pacienteRepository.getListRotacion();
        }

        // Selecciona Una Rotaci�n por Fecha y Hora Actual
        public Rotacion selectRotacion()
        {
            return _pacienteRepository.selectRotacion();
        }

        // Devuelve Una Lista de Rotaci�n por Fecha y Hora Actual
        public List<Rotacion> getListRotacionNow()
        {
            return _pacienteRepository.getListRotacionNow();
        }

        // Devuelve Un Paciente de Rotaci�n por Id
        public Paciente getPacienteByIdRotacion(int Id)
        {
            return _pacienteRepository.getPacienteByIdRotacion(Id);
        }
    }
}