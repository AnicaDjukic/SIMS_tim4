using Bolnica.Model.Prostorije;
using Bolnica.Repository.Prostorije;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Services.Prostorije
{
    public class ServiceProstorija
    {
        private RepositoryProstorija repository;

        public ServiceProstorija()
        {
            repository = new FileRepositoryProstorija();
        }

        public List<Prostorija> DobaviSveProstorije()
        {
            return repository.GetAll();
        }

        public List<Prostorija> DobaviProstorijeBezOpreme(List<Zaliha> zaliheOpreme)
        {
            List<Prostorija> prostorije = repository.GetAll();
            List<Prostorija> korisceneProstorije = new List<Prostorija>();
            List<Prostorija> slobodneProstorije = new List<Prostorija>();
            foreach (Zaliha z in zaliheOpreme)
            {
                foreach (Prostorija p in prostorije)
                {
                    if (z.Prostorija.BrojProstorije != p.BrojProstorije && !p.Obrisana)
                    {
                        slobodneProstorije.Remove(p);
                        slobodneProstorije.Add(p);
                    }
                    else
                    {
                        slobodneProstorije.Remove(p);
                        korisceneProstorije.Add(p);
                    }
                }
                foreach (Prostorija k in korisceneProstorije)
                {
                    prostorije.Remove(k);
                }
            }

            return slobodneProstorije;
        }

        public Prostorija NapraviProstoriju(string brojProstorije)
        {
            return new Prostorija { BrojProstorije = brojProstorije };
        }
    }
}
