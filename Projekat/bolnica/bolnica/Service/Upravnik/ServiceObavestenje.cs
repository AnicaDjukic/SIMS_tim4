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
        private IRepositoryObavestenje repository;

        public ServiceObavestenje()
        {
            repository = new FileRepositoryObavestenje();
        }
        public Obavestenje DobaviObavestenje(int id)
        {
            Obavestenje obavestenje = new Obavestenje();
            foreach (Obavestenje o in repository.GetAll())
            {
                if (o.Id == id)
                {
                    obavestenje = o;
                    break;
                }
            }

            return obavestenje;
        }

        public List<Obavestenje> DobaviObavestenjaKorisnika(string korisnickoIme)
        {
            
            List<Obavestenje> obavestenja = new List<Obavestenje>();
            foreach (Obavestenje o in repository.GetAll())
            {
                foreach (Korisnik k in o.Korisnici)
                {
                    if (k.KorisnickoIme == korisnickoIme)
                    {
                        obavestenja.Add(o);
                        break;
                    }
                }
            }
            return obavestenja;
        }

        internal List<Obavestenje> DobaviSvaObavestenja()
        {
            return repository.GetAll();
        }

        internal void SacuvajObavestenje(Obavestenje obavestenje)
        {
            repository.Save(obavestenje);
        }
    }
}
