using SQLiteNetExtensions.Attributes;

namespace Enfermed.Models
{
    public class PacienteRotacion
    {
        [ForeignKey(typeof(Paciente))]
        public int PacienteId { get; set; }

        [ForeignKey(typeof(Rotacion))]
        public int RotacionId { get; set; }
    }
}