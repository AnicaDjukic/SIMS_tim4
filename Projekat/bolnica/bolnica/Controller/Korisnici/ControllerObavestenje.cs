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
        private ServiceObavestenje service = new ServiceObavestenje();

        public void PosaljiObavestenjeZaLek(LekDTO lekZaPrikaz)
        {
            Obavestenje obavestenje = NapraviObavestenjeZaLek(lekZaPrikaz);
            service.SacuvajObavestenje(obavestenje);
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
            foreach (Obavestenje o in service.DobaviSvaObavestenja())
            {
                if (maxId < o.Id)
                    maxId = o.Id;
            }
            return maxId;
        }

        public Obavestenje DobaviObavestenje(int id)
        {
            return service.DobaviObavestenje(id);
        }

        public void SortirajObavestenja(List<Obavestenje> obavestenja)
        {
            Obavestenje temp = new Obavestenje();
            for (int j = 0; j <= obavestenja.Count - 2; j++)
            {
                for (int i = 0; i <= obavestenja.Count - 2; i++)
                {
                    if (obavestenja[i].Datum < obavestenja[i + 1].Datum)
                    {
                        temp = obavestenja[i + 1];
                        obavestenja[i + 1] = obavestenja[i];
                        obavestenja[i] = temp;
                    }
                }
            }
        }

        public List<Obavestenje> NadjiObavestenjaKorisnika(string korisnickoIme)
        {
            List<Obavestenje> obavestenja = new List<Obavestenje>();
            foreach(Obavestenje o in service.DobaviObavestenjaKorisnika(korisnickoIme))
            {
                o.Sadrzaj = o.Sadrzaj.Split(",")[0] + "...";
                obavestenja.Add(o);
            }

            return obavestenja;
        }
    }
}
