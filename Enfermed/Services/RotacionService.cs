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

        // Metodo Agregar Rotación de Paciente

        public void addRotacion(Paciente pacient, Rotacion rotacion)
        {
            _pacienteRepository.addRotacion(pacient, rotacion);
        }

        // Metodo Actualizar Rotación de Paciente
        public void updateRotacion(Rotacion rotacion)
        {
            _pacienteRepository.updateRotacion(rotacion);
        }

        // Metodo Eliminar Rotación de Paciente
        public void deleteRotacion(int Id)
        {
            _pacienteRepository.deleteRotacion(Id);
        }

        // Metodo Devuelve Una Lista de Rotaciones del Paciente por Id
        public List<Rotacion> getRotacionesPacienteById(int Id)
        {
            return _pacienteRepository.getRotacionesPacienteById(Id);
        }

        // Devuelve Una Rotación de Paciente por Id
        public Rotacion getRotacionPacienteById(int Id)
        {
            return _pacienteRepository.getRotacionPacienteById(Id);
        }

        // Devuelve Una Lista de Rotación
        public List<Rotacion> getListRotacion()
        {
            return _pacienteRepository.getListRotacion();
        }

        // Selecciona Una Rotación por Fecha y Hora Actual
        public Rotacion selectRotacion()
        {
            return _pacienteRepository.selectRotacion();
        }

        // Devuelve Una Lista de Rotación por Fecha y Hora Actual
        public List<Rotacion> getListRotacionNow()
        {
            return _pacienteRepository.getListRotacionNow();
        }

        // Devuelve Un Paciente de Rotación por Id
        public Paciente getPacienteByIdRotacion(int Id)
        {
            return _pacienteRepository.getPacienteByIdRotacion(Id);
        }
    }
}