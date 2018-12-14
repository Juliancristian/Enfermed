using System.Collections.Generic;
using Enfermed.Data;
using Enfermed.Models;

namespace Enfermed.Services
{
    public class GuiaService
    {
        private GuiaRepository _guiaRepository;
        private GuiaData _guiaData;

        public GuiaService()
        {
            _guiaRepository = new GuiaRepository(ValuesService.GetDbPath());
            _guiaData = new GuiaData();
        }

        // Metodo Cargar Datos Guia
        public void loadDataGuia()
        {
            _guiaRepository.addListGuia(_guiaData.getListGuiaData());
        }

        // Metodo Devuelve Lista Guia
        public List<Guia> getListGuia()
        {
            return _guiaRepository.getListGuia();
        }

        // Metodo Devuelve Guia por Id
        public Guia getGuiaById(int Id)
        {
            return _guiaRepository.getGuiaById(Id);
        }

        // Metodo Buscar Guia por Nombre
        public List<Guia> searchGuiaByName(string nombre)
        {
            return _guiaRepository.searchGuiaByName(nombre);
        }
    }
}