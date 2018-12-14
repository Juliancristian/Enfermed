using Enfermed.Models;
using System.Collections.Generic;

namespace Enfermed.Services
{
    public class PacienteService
    {
        private PacienteRepository _pacienteRepository;

        public PacienteService()
        {
            _pacienteRepository = new PacienteRepository(ValuesService.GetDbPath());
        }

        // Metodo Agregar Paciente
        public void addPaciente(Paciente pacient)
        {
            _pacienteRepository.addPaciente(pacient);
        }

        // Metodo Actualizar Paciente
        public void updatePaciente(Paciente pacient)
        {
            _pacienteRepository.updatePaciente(pacient);
        }

        // Metodo Eliminar Paciente
        public void deletePaciente(int Id)
        {
            _pacienteRepository.deletePaciente(Id);
        }

        // Metodo Devuelve Paciente Por Id
        public Paciente getPacienteById(int Id)
        {
            return _pacienteRepository.getPacienteById(Id);
        }

        // Metodo Devuelve Lista de Pacientes
        public List<Paciente> getListPacientes()
        {
            return _pacienteRepository.getListPacientes();
        }
    }
}