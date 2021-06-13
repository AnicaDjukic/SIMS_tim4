using Bolnica.Service;
using Model.Korisnici;

namespace Bolnica.Controller
{
    public class OcenaController
    {
        private OcenaService service = new OcenaService();

        public void SacuvajOcenu(Ocena novaOcena)
        {
            service.SacuvajOcenu(novaOcena);
        }

        public int IzracunajIdOcene()
        {
            return service.IzracunajIdOcene();
        }

        public void DobijOcenePacijenta(Pacijent trenutniPacijent)
        {
            service.DobijOcenePacijenta(trenutniPacijent);
        }
    }
}
