using Bolnica.Model.Korisnici;
using Bolnica.Repository.Korisnici;
using Model.Korisnici;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Services.Korisnici
{
    public class ServiceObavestenje
    {
        private RepositoryObavestenje repository;

        public ServiceObavestenje()
        {
            repository = new FileRepositoryObavestenje();
        }
        public void SortirajObavestenja(List<Obavestenje> obavestenjaZaPrikaz)
        {
            Obavestenje temp = new Obavestenje();
            for (int j = 0; j <= obavestenjaZaPrikaz.Count - 2; j++)
            {
                for (int i = 0; i <= obavestenjaZaPrikaz.Count - 2; i++)
                {
                    if (obavestenjaZaPrikaz[i].Datum < obavestenjaZaPrikaz[i + 1].Datum)
                    {
                        temp = obavestenjaZaPrikaz[i + 1];
                        obavestenjaZaPrikaz[i + 1] = obavestenjaZaPrikaz[i];
                        obavestenjaZaPrikaz[i] = temp;
                    }
                }
            }
        }

        public List<Obavestenje> NadjiObavestenjaKorisnika(string korisnickoIme)
        {
            
            List<Obavestenje> obavestenjaZaPrikaz = new List<Obavestenje>();
            foreach (Obavestenje o in repository.GetAll())
            {
                foreach (Korisnik k in o.Korisnici)
                {
                    if (k.KorisnickoIme == korisnickoIme)
                    {
                        o.Sadrzaj = o.Sadrzaj.Split(",")[0] + "...";
                        obavestenjaZaPrikaz.Add(o);
                        break;
                    }
                }
            }
            return obavestenjaZaPrikaz;
        }
    }
}
