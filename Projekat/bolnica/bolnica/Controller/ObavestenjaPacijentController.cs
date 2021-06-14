using Bolnica.Service;
using Model.Korisnici;

namespace Bolnica.Controller
{
    public class ObavestenjaPacijentController
    {
        private ObavestenjaPacijentService service = new ObavestenjaPacijentService();

        public void DobijObavestenjaBolnice(Pacijent pacijent)
        {
            service.DobijObavestenjaBolnice(pacijent);
        }

        public void DobijObavestenjaOLekovima(Pacijent pacijent)
        {
            service.DobijObavestenjaOLekovima(pacijent);
        }

    }
}
