using Bolnica.Service.Sekretar;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Controller.Sekretar
{
    public class OperacijaController
    {
        private OperacijaService service = new OperacijaService(); 

        public List<PrikazOperacije> GetAllOperacije() 
        {
            return service.GetAllOperacije();
        }

        public void DeleteOperacija(PrikazOperacije operacijaDTO) 
        {
            service.DeleteOperacija(operacijaDTO);
        }
    }
}
