using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace Enfermed.Models
{
    internal class PacienteRepository
    {
        private string _dbPath;

        //  Constructor con parametro de Ruta
        public PacienteRepository(string dbPath)
        {
            // Path
            _dbPath = dbPath;

            // Crea la base de datos si no existe, y crea una nueva conexion
            using (var _db = new SQLiteConnection(_dbPath))
            {
                _db.CreateTable<Paciente>();
                _db.CreateTable<Medicamento>();
                _db.CreateTable<PacienteMedicamento>();
                _db.CreateTable<Rotacion>();
                _db.CreateTable<PacienteRotacion>();
            }
        }

        // Agregar Paciente 
        public void addPaciente(Paciente paciente)
        {
            using (var _db = new SQLiteConnection(_dbPath))
            {
                _db.Insert(paciente);
            }
        }

        // Editar Paciente
        public void updatePaciente(Paciente paciente)
        {
            using (var _db = new SQLiteConnection(_dbPath))
            {
                _db.Query<Paciente>("UPDATE Paciente set NroHistoria=?, Nombre=?, Apellido=?, Edad=?, Masculino=?, Femenino=?, NroHabitacion=?, NroCama=? Where Id=?", paciente.NroHistoria, paciente.Nombre, paciente.Apellido, paciente.Edad, paciente.Masculino, paciente.Femenino, paciente.NroHabitacion, paciente.NroCama, paciente.Id);
            }
        }

        // Eliminar Paciente  
        public void deletePaciente(int Id)
        {
            using (var _db = new SQLiteConnection(_dbPath))
            {                                         
                _db.Delete<Paciente>(Id);                
            }
        }

        // Devuelve Paciente por Id
        public Paciente getPacienteById(int Id)
        {
            using (var _db = new SQLiteConnection(_dbPath))
            {
                return _db.Get<Paciente>(Id);
            }
        }

        // Devuelve Lista de Pacientes
        public List<Paciente> getListPacientes()
        {
            using (var _db = new SQLiteConnection(_dbPath))
            {
                return _db.Table<Paciente>().ToList();
            }
        }

        // //////////////////////////////////////////////////////////////////////// //
        //                              MEDICAMENTO                                //
        // ////////////////////////////////////////////////////////////////////// //

        // Agregar Medicamento de Paciente
        public void addMedicamento(Paciente paciente, Medicamento medicamento)
        {
            using (var _db = new SQLiteConnection(_dbPath))
            {
                var listaMedicamentos = _db.GetWithChildren<Paciente>(paciente.Id).Medicamentos; // Medicamentos Actuales
                var listaRotaciones = _db.GetWithChildren<Paciente>(paciente.Id).Rotaciones; // Rotaciones Actuales

                paciente.Medicamentos = new List<Medicamento>();
                paciente.Rotaciones = new List<Rotacion>();
                paciente.Medicamentos.AddRange(listaMedicamentos); // Agrego Lista de Medicamentos Actuales
                paciente.Rotaciones.AddRange(listaRotaciones); // Agrego Lista de Rotaciones Actuales
                paciente.Medicamentos.Add(medicamento); // Agrego Nuevo Medicamento

                _db.InsertOrReplaceWithChildren(paciente); // Inserta registros a la Base de Datos 
                //_db.UpdateWithChildren(paciente); //NO FUNCIONA METODO UPDATEWITHCHILDREN PROBLEMA EN EL PAQUETE
            }
        }

        // Editar Medicamento de Paciente
        public void updateMedicamento(Medicamento medicamento)
        {
            using (var _db = new SQLiteConnection(_dbPath))
            {
                _db.Query<Medicamento>("UPDATE Medicamento set farmaco=?, dosis=?, viaOral=?, viaSubcutanea=?, viaIntramuscular=?, viaIntravenoso=?, viaInhalatoria=?, fecha=?, hora=?, confirmar=? Where Id=?", medicamento.farmaco, medicamento.dosis, medicamento.viaOral, medicamento.viaSubcutanea, medicamento.viaIntramuscular, medicamento.viaIntravenoso, medicamento.viaInhalatoria, medicamento.fecha, medicamento.hora, medicamento.confirmar, medicamento.Id);
            }
        }

        // Eliminar Medicamento de Paciente
        public void deleteMedicamento(int Id)
        {
            using (var _db = new SQLiteConnection(_dbPath))
            {
                _db.Delete<Medicamento>(Id);
            }
        }

        // Devuelve Una Lista de Medicamentos de Paciente por Id
        public List<Medicamento> getMedicamentosPacienteById(int Id)
        {
            using (var _db = new SQLiteConnection(_dbPath))
            {
                return _db.GetWithChildren<Paciente>(Id).Medicamentos;
            }
        }

        // Devuelve Un Medicamento de Paciente por Id
        public Medicamento getMedicamentoPacienteById(int Id)
        {
            using (var _db = new SQLiteConnection(_dbPath))
            {
                return _db.Table<Medicamento>().Where(m => m.Id == Id).FirstOrDefault();
            }
        }

        // Devuelve Una Lista de Medicamentos
        public List<Medicamento> getListMedicamentos()
        {
            using (var _db = new SQLiteConnection(_dbPath))
            {
                return _db.Table<Medicamento>().ToList();
            }
        }

        // Selecciona Un Medicamento por Fecha y Hora Actual
        public Medicamento selectMedicamento()
        {
            using (var _db = new SQLiteConnection(_dbPath))
            {
                // DateTime Now
                var dateActual = DateTime.Now.ToShortDateString();
                var timeActual = DateTime.Now.ToShortTimeString();
                return _db.Table<Medicamento>().Where(x => x.fecha == dateActual && x.hora == timeActual).FirstOrDefault();
            }
        }

        // Devuelve Una Lista de Medicamentos por Fecha y Hora Actual
        public List<Medicamento> getListMedicamentosNow()
        {
            using (var _db = new SQLiteConnection(_dbPath))
            {
                // DateTime Now
                var dateActual = DateTime.Now.ToShortDateString();
                var timeActual = DateTime.Now.ToShortTimeString();
                return _db.Table<Medicamento>().Where(x => x.fecha == dateActual && x.hora == timeActual && x.confirmar == false).ToList();
            }
        }

        // Devuelve Un Paciente de Medicamento por Id
        public Paciente getPacienteByIdMedicamento(int Id)
        {
            using (var _db = new SQLiteConnection(_dbPath))
            {
                var idPaciente = _db.Table<PacienteMedicamento>().Where(x => x.MedicamentoId == Id).FirstOrDefault().PacienteId;
                return _db.Get<Paciente>(idPaciente);
            }
        }

        // //////////////////////////////////////////////////////////////////////// //
        //                              ROTACIÓN                                   //
        // ////////////////////////////////////////////////////////////////////// //

        // Agregar Rotación de Paciente
        public void addRotacion(Paciente paciente, Rotacion rotacion)
        {
            using (var _db = new SQLiteConnection(_dbPath))
            {
                var listaMedicamentos = _db.GetWithChildren<Paciente>(paciente.Id).Medicamentos; // Medicamentos Actuales
                var listaRotaciones = _db.GetWithChildren<Paciente>(paciente.Id).Rotaciones; // Rotaciones Actuales

                paciente.Medicamentos = new List<Medicamento>();
                paciente.Rotaciones = new List<Rotacion>();
                paciente.Medicamentos.AddRange(listaMedicamentos); // Agrego Lista de Medicamentos Actuales
                paciente.Rotaciones.AddRange(listaRotaciones); // Agrego Lista de Rotaciones Actuales
                paciente.Rotaciones.Add(rotacion); // Agrego Nueva Rotacion

                _db.InsertOrReplaceWithChildren(paciente); // Inserta registros a la Base de Datos
            }
        }

        // Editar Rotación de Paciente
        public void updateRotacion(Rotacion rotacion)
        {
            using (var _db = new SQLiteConnection(_dbPath))
            {
                _db.Query<Rotacion>("UPDATE Rotacion set comun=?, aire=?, lateralIzq=?, lateralDer=?, supina=?, prono=?, fecha=?, hora=?, confirmar=? Where Id=?", rotacion.comun, rotacion.aire, rotacion.lateralIzq, rotacion.lateralDer, rotacion.supina, rotacion.prono, rotacion.fecha, rotacion.hora, rotacion.confirmar, rotacion.Id);
            }
        }

        // Eliminar Rotación de Paciente
        public void deleteRotacion(int Id)
        {
            using (var _db = new SQLiteConnection(_dbPath))
            {
                _db.Delete<Rotacion>(Id);
            }
        }

        // Devuelve Una Lista de Rotaciones de Paciente por Id
        public List<Rotacion> getRotacionesPacienteById(int Id)
        {
            using (var _db = new SQLiteConnection(_dbPath))
            {
                return _db.GetWithChildren<Paciente>(Id).Rotaciones;
            }
        }

        // Devuelve Una Rotación de Paciente por Id
        public Rotacion getRotacionPacienteById(int Id)
        {
            using (var _db = new SQLiteConnection(_dbPath))
            {
                return _db.Table<Rotacion>().Where(m => m.Id == Id).FirstOrDefault();
            }
        }

        // Devuelve Una Lista de Rotación
        public List<Rotacion> getListRotacion()
        {
            using (var _db = new SQLiteConnection(_dbPath))
            {
                return _db.Table<Rotacion>().ToList();
            }
        }

        // Selecciona Una Rotación por Fecha y Hora Actual
        public Rotacion selectRotacion()
        {
            using (var _db = new SQLiteConnection(_dbPath))
            {
                // DateTime Now
                var dateActual = DateTime.Now.ToShortDateString();
                var timeActual = DateTime.Now.ToShortTimeString();
                return _db.Table<Rotacion>().Where(x => x.fecha == dateActual && x.hora == timeActual).FirstOrDefault();
            }
        }

        // Devuelve Una Lista de Rotación por Fecha y Hora Actual
        public List<Rotacion> getListRotacionNow()
        {
            using (var _db = new SQLiteConnection(_dbPath))
            {
                // DateTime Now
                var dateActual = DateTime.Now.ToShortDateString();
                var timeActual = DateTime.Now.ToShortTimeString();
                return _db.Table<Rotacion>().Where(x => x.fecha == dateActual && x.hora == timeActual && x.confirmar == false).ToList();
            }
        }

        // Devuelve Un Paciente de Rotación por Id
        public Paciente getPacienteByIdRotacion(int Id)
        {
            using (var _db = new SQLiteConnection(_dbPath))
            {
                var idPaciente = _db.Table<PacienteRotacion>().Where(x => x.RotacionId == Id).FirstOrDefault().PacienteId;
                return _db.Get<Paciente>(idPaciente);
            }
        }
    }
}