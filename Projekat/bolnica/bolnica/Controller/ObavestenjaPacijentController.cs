using Bolnica.Service;
using Model.Korisnici;

namespace Bolnica.Controller
{
    public class ObavestenjaPacijentController
    {
        private ObavestenjaPacijentService service = new ObavestenjaPacijentService();

        public void PrikaziObavestenjaBolnice(Pacijent pacijent)
        {
            service.PrikaziObavestenjaBolnice(pacijent);
        }

        public void PrikaziObavestenjaOLekovima(Pacijent pacijent)
        {
            service.PrikaziObavestenjaOLekovima(pacijent);
        }

    }
}
