using System.Collections.Generic;
using System.Linq;
using SQLite;

namespace Enfermed.Models
{
    internal class GuiaRepository
    {
        private string _dbPath;
        public GuiaRepository()
        {
        }

        //  Constructor con parametro de Ruta
        public GuiaRepository(string dbPath)
        {
            // Path
            _dbPath = dbPath;

            // Crea la base de datos si no existe, y crea una nueva conexion
            using (var _db = new SQLiteConnection(_dbPath))
            {
                _db.CreateTable<Guia>();
            }
        }

        // Agregar Lista Guia 
        public void addListGuia(List<Guia> listaGuia)
        {
            using (var _db = new SQLiteConnection(_dbPath))
            {
                _db.InsertAll(listaGuia);
            }
        }

        // Devuelve Guia por Id
        public Guia getGuiaById(int Id)
        {
            using (var _db = new SQLiteConnection(_dbPath))
            {
                return _db.Get<Guia>(Id);
            }
        }

        // Devuelve Lista de Guia
        public List<Guia> getListGuia()
        {
            using (var _db = new SQLiteConnection(_dbPath))
            {
                return _db.Table<Guia>().ToList();
            }
        }

        // Buscar Guia por Nombre
        public List<Guia> searchGuiaByName(string nombre)
        {
            using (var _db = new SQLiteConnection(_dbPath))
            {
                return _db.Query<Guia>("SELECT * FROM Guia WHERE Title like?", nombre).ToList();
            }
        }
    }
}