using Bolnica.Model.Pregledi;
using Bolnica.Service;
using Model.Korisnici;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Controller
{
    public class IstorijaPregledaController
    {
        private IstorijaPregledaService service = new IstorijaPregledaService();
        public void Anamneza(PrikazPregleda SelektovaniRed)
        {
            service.Anamneza(SelektovaniRed);
        }
        public void Oceni_Lekara(PrikazPregleda SelektovaniRed)
        {
            service.Oceni_Lekara(SelektovaniRed);
        }
        public void Oceni_Bolnicu(Pacijent pacijent)
        {
            service.Oceni_Bolnicu(pacijent);
        }
        public void Istorija_Ocena_I_Komentara(Pacijent pacijent)
        {
            service.Istorija_Ocena_I_Komentara(pacijent);
        }
    }
}
