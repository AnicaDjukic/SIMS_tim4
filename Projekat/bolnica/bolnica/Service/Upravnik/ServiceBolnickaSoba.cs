using Bolnica.Model.Prostorije;
using Bolnica.Repository.Prostorije;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Services.Prostorije
{
    public class ServiceBolnickaSoba
    {
        private RepositoryBolnickaSoba repository;

        public ServiceBolnickaSoba()
        {
            repository = new FileRepositoryBolnickaSoba();
        }

        public List<BolnickaSoba> DobaviSveBolnickeSobe()
        {
            return repository.GetAll();
        }

        public List<BolnickaSoba> DobaviBolnickeSobeBezOpreme(List<Zaliha> zaliheOpreme)
        {
            List<BolnickaSoba> bolnickeSobe = repository.GetAll();
            List<BolnickaSoba> korisceneBolnicke = new List<BolnickaSoba>();
            List<BolnickaSoba> slobodneBolnickeSobe = new List<BolnickaSoba>();
            foreach (Zaliha z in zaliheOpreme)
            {
                foreach (BolnickaSoba b in bolnickeSobe)
                {
                    if (z.Prostorija.BrojProstorije != b.BrojProstorije && !b.Obrisana)
                    {
                        slobodneBolnickeSobe.Remove(b);
                        slobodneBolnickeSobe.Add(b);
                    }
                    else
                    {
                        slobodneBolnickeSobe.Remove(b);
                        korisceneBolnicke.Add(b);
                    }
                }
                foreach (BolnickaSoba k in korisceneBolnicke)
                {
                    bolnickeSobe.Remove(k);
                }
            }

            return slobodneBolnickeSobe;
        }

        public BolnickaSoba DobaviBolnickuSobu(string brojBolnickeSobe)
        {
            return repository.GetById(brojBolnickeSobe);
        }

        public bool BolnickaSobaPostoji(string brojProstorije)
        {
            bool postoji = false;
            foreach (Prostorija p in repository.GetAll())
            {
                if (p.BrojProstorije == brojProstorije && !p.Obrisana)
                {
                    postoji = true;
                    break;
                }
            }

            return postoji;
        }

        internal void ObrisiBolnickuSobu(string brojProstorije)
        {
            repository.DeleteById(brojProstorije);
        }

        internal void SacuvajBolnickuSobu(BolnickaSoba bolnickaSoba)
        {
            repository.Save(bolnickaSoba);
        }
    }
}
