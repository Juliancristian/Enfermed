using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Enfermed.Models
{
    [Table("Paciente")]
    public class Paciente
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int NroHistoria { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public bool Masculino { get; set; }
        public bool Femenino { get; set; }
        public int NroHabitacion { get; set; }
        public string NroCama { get; set; }

        [ManyToMany(typeof(PacienteMedicamento), CascadeOperations = CascadeOperation.All)]
        public List<Medicamento> Medicamentos { get; set; }

        [ManyToMany(typeof(PacienteRotacion), CascadeOperations = CascadeOperation.All)]
        public List<Rotacion> Rotaciones { get; set; }
    }
}