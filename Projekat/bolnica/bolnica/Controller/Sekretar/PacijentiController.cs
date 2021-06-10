using Bolnica.DTO;
using Bolnica.Forms;
using Bolnica.Sekretar;
using Bolnica.Services;
using Model.Korisnici;
using Model.Pacijenti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Bolnica.Controller
{
    public class PacijentiController
    {
        private PacijentiService service = new PacijentiService();

        public PacijentDTO GetPacijentByID(string jmbg)
        {
            return service.GetPacijentByID(jmbg);
        }

        public List<PacijentDTO> GetAllPacijente()
        {
            return service.GetAllPacijente();
        }

        public List<PacijentDTO> GetRedovnePacijente() 
        {
            return service.GetRedovnePacijente();
        }

        public List<PacijentDTO> GetGostPacijente()
        {
            return service.GetGostPacijente();
        }

        public List<PacijentDTO> GetObrisanePacijente()
        {
            return service.GetObrisanePacijente();
        }

        public void BlokirajPacijenta(PacijentDTO pacijentDTO) 
        {
            service.BlokirajPacijenta(pacijentDTO);
        }

        public void OdblokirajPacijenta(PacijentDTO pacijentDTO) 
        {
            service.OdblokirajPacijenta(pacijentDTO);
        }

        public void DodajIliIzmeniRedovnogPacijenta(PacijentDTO pacijent) 
        {
            service.DodajIliIzmeniRedovnogPacijenta(pacijent);
        }
    }
}
