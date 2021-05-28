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
    }
}
