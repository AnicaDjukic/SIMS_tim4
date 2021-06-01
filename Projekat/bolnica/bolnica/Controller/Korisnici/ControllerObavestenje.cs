using Bolnica.DTO;
using Bolnica.Model.Korisnici;
using Bolnica.Services.Korisnici;
using Model.Korisnici;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Controller.Korisnici
{
    public class ControllerObavestenje
    {
        private ServiceObavestenje serviceObavestenje = new ServiceObavestenje();

        public void PosaljiObavestenjeZaLek(LekDTO lekZaPrikaz)
        {
            Obavestenje obavestenje = NapraviObavestenjeZaLek(lekZaPrikaz);
            serviceObavestenje.SacuvajObavestenje(obavestenje);
        }

        private Obavestenje NapraviObavestenjeZaLek(LekDTO lek)
        {
            int maxId = NadjiMaxId();
            Obavestenje obavestenje = new Obavestenje { Id = maxId + 1, Naslov = "Lek za validaciju", Datum = DateTime.Now.Date, Sadrzaj = "Za lek \"" + lek.Naziv + "\" je potrebno izvršiti validaciju" };
            Korisnik lekar = new Korisnik();
            lekar.KorisnickoIme = "mico";
            obavestenje.Korisnici.Add(lekar);
            return obavestenje;
        }

        private int NadjiMaxId()
        {
            int maxId = 0;
            foreach (Obavestenje o in serviceObavestenje.DobaviSvaObavestenja())
            {
                if (maxId < o.Id)
                    maxId = o.Id;
            }
            return maxId;
        }

    }
}
