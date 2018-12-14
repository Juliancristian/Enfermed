using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Enfermed.Models
{
    [Table("Medicamento")]
    public class Medicamento
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string farmaco { get; set; }
        public int dosis { get; set; }
        public bool viaOral { get; set; }
        public bool viaSubcutanea { get; set; }
        public bool viaIntramuscular { get; set; }
        public bool viaIntravenoso { get; set; }
        public bool viaInhalatoria { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public bool confirmar { get; set; }

        [ManyToMany(typeof(PacienteMedicamento), CascadeOperations = CascadeOperation.All)]
        public List<Paciente> Pacientes { get; set; }

    }
}