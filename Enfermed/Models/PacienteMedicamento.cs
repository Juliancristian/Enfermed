using SQLiteNetExtensions.Attributes;

namespace Enfermed.Models
{
    public class PacienteMedicamento
    {
        [ForeignKey(typeof(Paciente))]
        public int PacienteId { get; set; }

        [ForeignKey(typeof(Medicamento))]
        public int MedicamentoId { get; set; }
    }
}