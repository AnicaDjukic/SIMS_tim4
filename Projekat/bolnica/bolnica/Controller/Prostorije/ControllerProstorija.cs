using Bolnica.Model.Prostorije;
using Bolnica.Services.Prostorije;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Controller.Prostorije
{
    public class ControllerProstorija
    {
        private ServiceProstorija service;

        public ControllerProstorija()
        {
            service = new ServiceProstorija();
        }

        public bool ProstorijaPostoji(string brojProstorije)
        {
            return service.ProstorijaPostoji(brojProstorije);
        }

        public void ObrisiProstoriju(string brojProstorije)
        {
            service.ObrisiProstoriju(brojProstorije);
        }

        public void SacuvajProstoriju(Prostorija prostorija)
        {
            service.SacuvajProstoriju(prostorija);
        }

        public List<Prostorija> DobaviSveProstorije()
        {
            return service.DobaviSveProstorije();
        }

        public List<Prostorija> DobaviProstorijeBezOpreme(List<Zaliha> zaliheOpreme)
        {
            return service.DobaviProstorijeBezOpreme(zaliheOpreme);
        }

        public Prostorija NapraviProstoriju(string brojProstorije)
        {
            return new Prostorija { BrojProstorije = brojProstorije };
        }

        public List<Prostorija> DobaviProstorijeNaIstomSpratu(string brojProstorije)
        {
            List<Prostorija> prostorijeNaSpratu = new List<Prostorija>();
            Prostorija prostorija = service.DobaviProstoriju(brojProstorije);
            foreach (Prostorija p in service.DobaviSveProstorije())
            {
                if (p.Sprat == prostorija.Sprat)
                    prostorijeNaSpratu.Add(p);
            }
            return prostorijeNaSpratu;
        }
    }
}
