using Bolnica.Model.Pregledi;
using Bolnica.Service.Sekretar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Controller.Sekretar
{
    public class PregledController
    {
        private PregledService service = new PregledService();

        public List<PrikazPregleda> GetAllPregledi()
        {
            return service.GetAllPregledi();
        }

        public void DeletePregled(PrikazPregleda pregledDTO) 
        {
            service.DeletePregled(pregledDTO);
        }

        public PrikazPregleda GetPregledById(int id)
        {
            return service.GetPregledById(id);
        }
    }
}
