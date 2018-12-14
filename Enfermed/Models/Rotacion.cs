using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Enfermed.Models
{
    [Table("Rotacion")]
    public class Rotacion
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public bool comun { get; set; }
        public bool aire { get; set; }
        public bool lateralIzq { get; set; }
        public bool lateralDer { get; set; }
        public bool supina { get; set; }
        public bool prono { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public bool confirmar { get; set; }

        [ManyToMany(typeof(PacienteRotacion), CascadeOperations = CascadeOperation.All)]
        public List<Paciente> Pacientes { get; set; }
    }
}