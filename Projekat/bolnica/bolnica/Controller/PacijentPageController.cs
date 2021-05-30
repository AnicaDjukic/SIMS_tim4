using Bolnica.Model.Pregledi;
using Bolnica.Service;
using Model.Korisnici;

namespace Bolnica.Controller
{
    public class PacijentPageController
    {
        private PacijentPageService service = new PacijentPageService();

        public void ZakaziPregled(Pacijent trenutniPacijent)
        {
            service.ZakaziPregled(trenutniPacijent);
        }

        public void OtkaziPregled(PrikazPregleda prikazPregleda)
        {
            service.OtkaziPregled(prikazPregleda);
        }

        public void IzmeniPregled(PrikazPregleda prikazPregleda)
        {
            service.IzmeniPregled(prikazPregleda);
        }

        public void IstorijaPregleda(Pacijent trenutniPacijent)
        {
            service.IstorijaPregleda(trenutniPacijent);
        }

        public void ObavestenjaPacijent(Pacijent trenutniPacijent)
        {
            service.ObavestenjaPacijent(trenutniPacijent);
        }
    }
}
