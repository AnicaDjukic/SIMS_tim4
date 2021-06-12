using Bolnica.Service;
using Model.Korisnici;

namespace Bolnica.Controller
{
    public class PodsetnikController
    {
        private PodsetnikService service = new PodsetnikService();

        public void ProveriObavestenja(Pacijent trenutniPacijent)
        {
            service.ProveriObavestenja(trenutniPacijent);
        }

        public void BlokirajPacijenta(Pacijent trenutniPacijent)
        {
            service.BlokirajPacijenta(trenutniPacijent);
        }

        public int DobijBrojAktivnosti(Pacijent trenutniPacijent)
        {
            return service.DobijBrojAktivnosti(trenutniPacijent);
        }
    }
}
