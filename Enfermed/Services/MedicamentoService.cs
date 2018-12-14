using System.Collections.Generic;
using Enfermed.Models;

namespace Enfermed.Services
{
    public class MedicamentoService
    {
        private PacienteRepository _pacienteRepository;

        public MedicamentoService()
        {
            _pacienteRepository = new PacienteRepository(ValuesService.GetDbPath());
        }

        // Metodo Agregar Medicamento de Paciente
        public void addMedicamento(Paciente pacient, Medicamento medicamento)
        {
            _pacienteRepository.addMedicamento(pacient, medicamento);
        }

        // Metodo Actualizar Medicamento de Paciente
        public void updateMedicamento(Medicamento medicamento)
        {
            _pacienteRepository.updateMedicamento(medicamento);
        }

        // Metodo Eliminar Medicamento de Paciente
        public void deleteMedicamento(int Id)
        {
            _pacienteRepository.deleteMedicamento(Id);
        }

        // Metodo Devuelve Una Lista de Medicamentos del Paciente por Id
        public List<Medicamento> getMedicamentosPacienteById(int Id)
        {
            return _pacienteRepository.getMedicamentosPacienteById(Id);
        }

        // Devuelve Un Medicamento de Paciente por Id
        public Medicamento getMedicamentoPacienteById(int Id)
        {
            return _pacienteRepository.getMedicamentoPacienteById(Id);
        }

        // Devuelve Una Lista de Medicamentos
        public List<Medicamento> getListMedicamentos()
        {
            return _pacienteRepository.getListMedicamentos();
        }

        // Selecciona Un Medicamento por Fecha y Hora Actual
        public Medicamento selectMedicamento()
        {
            return _pacienteRepository.selectMedicamento();
        }


        // Devuelve Una Lista de Medicamentos por Fecha y Hora Actual
        public List<Medicamento> getListMedicamentosNow()
        {
            return _pacienteRepository.getListMedicamentosNow();
        }

        // Devuelve Un Paciente de Medicamento por Id
        public Paciente getPacienteByIdMedicamento(int Id)
        {
            return _pacienteRepository.getPacienteByIdMedicamento(Id);
        }
    }
}