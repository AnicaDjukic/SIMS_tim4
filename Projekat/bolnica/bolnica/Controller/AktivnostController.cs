using Bolnica.Service;
using Model.Korisnici;

namespace Bolnica.Controller
{
    public class AktivnostController
    {
        private AktivnostService service = new AktivnostService();

        public int DobijBrojAktivnosti(Pacijent trenutniPacijent)
        {
            return service.DobijBrojAktivnosti(trenutniPacijent);
        }
    }
}
