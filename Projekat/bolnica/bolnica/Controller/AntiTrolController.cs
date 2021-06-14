using Bolnica.Model.Pregledi;
using Bolnica.Service;
using Model.Korisnici;
using System.Collections.Generic;

namespace Bolnica.Controller
{
    public class AntiTrolController
    {
        private AntiTrolService service = new AntiTrolService();

        public List<AntiTrol> DobaviSveAntiTrolove()
        {
            return service.DobaviSveAntiTrolove();
        }

        public void SacuvajAntiTrol(AntiTrol noviAntiTrol)
        {
            service.SacuvajAntiTrol(noviAntiTrol);
        }

        public AntiTrol KreirajAntiTrol(PrikazPregleda prikazPregleda)
        {
            return service.KreirajAntiTrol(prikazPregleda);
        }
    }
}
