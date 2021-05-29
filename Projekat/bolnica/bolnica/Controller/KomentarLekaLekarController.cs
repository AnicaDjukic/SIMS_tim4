using Bolnica.DTO;
using Bolnica.Forms;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.Services;
using Bolnica.ViewModel;
using Model.Korisnici;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Controller
{
    public class KomentarLekaLekarController
    {
        private KomentarLekaLekarService service = new KomentarLekaLekarService();

        public List<Lek> DobijLek()
        {
            return service.DobijLek();
        }
        public void Potvrdi(KomentarLekaLekarServiceDTO komentarDTO)
        {
            service.Potvrdi(komentarDTO);
        }
        public int IzracunajId(List<Obavestenje> svaObavestenja)
        {
            return IzracunajId(svaObavestenja);
        }
    }
}
