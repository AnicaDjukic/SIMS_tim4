using Bolnica.Model.Prostorije;
using Bolnica.Repository.Prostorije;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Services.Prostorije
{
    public class ServiceBuducaZaliha
    {
        private RepositoryBuducaZaliha repository;

        public ServiceBuducaZaliha()
        {
            repository = new FileRepositoryBuducaZaliha();
        }
        public List<BuducaZaliha> DobaviBuduceZaliheOpreme(string sifraOpreme)
        {
            List<BuducaZaliha> buduceZalihe = new List<BuducaZaliha>();
            if (repository.GetAll() != null)
            {
                foreach (BuducaZaliha bz in repository.GetAll())
                {
                    if (bz.Oprema.Sifra == sifraOpreme && bz.Datum <= DateTime.Now.Date)
                    {
                        buduceZalihe.Add(bz);
                        repository.Delete(bz);
                    }
                }               
            }

            return buduceZalihe;
        }

        public void ObrisiBuduceZaliheOpreme(string sifraOpreme, DateTime datum)
        {
            foreach (BuducaZaliha bz in repository.GetAll())
            {
                if (bz.Oprema.Sifra == sifraOpreme && bz.Datum == datum)
                    repository.Delete(bz);
            }
        }

        public void SacuvajBuduceZalihe(List<BuducaZaliha> buduceZalihe)
        {
            repository.Save(buduceZalihe);
        }

        public List<BuducaZaliha> DobaviBuduceZaliheIsteklogDatuma()
        {
            List<BuducaZaliha> buduceZalihe = new List<BuducaZaliha>();
            foreach (BuducaZaliha bz in repository.GetAll())
            {
                if (bz.Datum <= DateTime.Now.Date)
                {
                    buduceZalihe.Add(bz);
                }
            }

            return buduceZalihe;
        }

        internal void ObrisiBuduceZalihe(List<BuducaZaliha> buduceZalihe)
        {
            foreach(BuducaZaliha bz in buduceZalihe)
            {
                repository.Delete(bz);
            }
        }

        public List<BuducaZaliha> DobaviSveBuduceZalihe()
        {
            return repository.GetAll();
        }

        public void ObrisiBuducuZalihu(BuducaZaliha bz)
        {
            repository.Delete(bz);
        }

        public void ObrisiBuduceZaliheProstorije(string brojProstorije)
        {
            foreach (BuducaZaliha bz in repository.GetAll())
            {
                if (bz.Prostorija.BrojProstorije == brojProstorije)
                {
                    foreach (BuducaZaliha bzo in DobaviBuduceZaliheOpreme(bz.Oprema.Sifra))
                    {
                        if (bzo.Prostorija.BrojProstorije == "magacin")
                        {
                            repository.Delete(bzo);
                            bzo.Kolicina += bz.Kolicina;
                            repository.Save(bzo);
                        }
                    }
                    repository.Delete(bz);
                }
            }
        }
    }
}
