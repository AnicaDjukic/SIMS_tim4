using Bolnica.Service;
using Model.Korisnici;

namespace Bolnica.Controller
{
    public class OcenaController
    {
        private OcenaService service = new OcenaService();
        private OcenaIdService idService = new OcenaIdService();

        public void SacuvajOcenu(Ocena novaOcena)
        {
            service.SacuvajOcenu(novaOcena);
        }

        public int IzracunajIdOcene()
        {
            return idService.IzracunajIdOcene();
        }

        public void DobijOcenePacijenta(Pacijent trenutniPacijent)
        {
            service.DobijOcenePacijenta(trenutniPacijent);
        }
    }
}
