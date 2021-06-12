using Bolnica.Service;
using Model.Korisnici;

namespace Bolnica.Controller
{
    public class TerapijaPacijentController
    {
        private TerapijaPacijentService service = new TerapijaPacijentService();

        public void DobijTrenutnuSedmicu()
        {
            service.DobijTrenutnuSedmicu();
        }

        public void InicijalizujSedmicnuTerapiju()
        {
            service.InicijalizujSedmicnuTerapiju();
        }

        public void DobijTerapijuPacijenta(Pacijent trenutniPacijent)
        {
            service.DobijTerapijuPacijenta(trenutniPacijent);
        }
    }
}
