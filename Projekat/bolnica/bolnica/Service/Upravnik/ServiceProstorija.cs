using Bolnica.Model.Prostorije;
using Bolnica.Repository.Prostorije;
using Bolnica.Service.Upravnik;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Services.Prostorije
{
    public class ServiceProstorija
    {
        private RepositoryProstorija repository;
        private ServiceBolnickaSoba serviceBolnickaSoba;
        private ServicePregled servicePregled;
        private ServiceOperacija serviceOperacija;
        public ServiceProstorija()
        {
            repository = new FileRepositoryProstorija();
            serviceBolnickaSoba = new ServiceBolnickaSoba();
            servicePregled = new ServicePregled();
            serviceOperacija = new ServiceOperacija();
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

        public double DobaviKvadraturu(string brojProstorije)
        {
            return repository.GetById(brojProstorije).Kvadratura;
        }

        public bool ProstorijaPostoji(string brojProstorije)
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
            if (!postoji)
                postoji = serviceBolnickaSoba.BolnickaSobaPostoji(brojProstorije);

            return postoji;
        }

        public List<Operacija> DobaviOperacijeProstorije(string brojProstorije)
        {
            List<Operacija> operacijeProstorije = new List<Operacija>();
            foreach (Operacija o in serviceOperacija.DobaviSveOperacije())
            {
                if (o.Prostorija.BrojProstorije == brojProstorije)
                    operacijeProstorije.Add(o);
            }
            return operacijeProstorije;
        }

        public List<Pregled> DobaviPregledeProstorije(string brojProstorije)
        {
            List<Pregled> preglediProstorije = new List<Pregled>();
            foreach(Pregled p in servicePregled.DobaviSvePreglede())
            {
                if(p.Prostorija.BrojProstorije == brojProstorije)
                    preglediProstorije.Add(p);
            }
            return preglediProstorije;
        }

        public Prostorija DobaviProstoriju(string brojProstorije)
        {
            return repository.GetById(brojProstorije);
        }

        public void ObrisiProstoriju(string brojProstorije)
        {
            repository.DeleteById(brojProstorije);
        }

        public void SacuvajProstoriju(Prostorija prostorija)
        {
            repository.Save(prostorija);
        }

        public void IzmeniProstoriju(Prostorija prostorija)
        {
            repository.DeleteById(prostorija.BrojProstorije);
            repository.Save(prostorija);
        }
    }
}
