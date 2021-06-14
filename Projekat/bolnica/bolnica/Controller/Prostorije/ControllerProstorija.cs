using Bolnica.Model.Prostorije;
using Bolnica.Services.Prostorije;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;

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

        public Prostorija DobaviProstoriju(string brojProstorije)
        {
            return service.DobaviProstoriju(brojProstorije);
        }

        public List<Prostorija> DobaviProstorijeNaIstomSpratu(int sprat)
        {
            List<Prostorija> prostorijeNaSpratu = new List<Prostorija>();
            foreach (Prostorija p in service.DobaviSveProstorije())
            {
                if (p.Sprat == sprat && !p.Obrisana)
                    prostorijeNaSpratu.Add(p);
            }
            return prostorijeNaSpratu;
        }

        public bool ZauzetaOdDatuma(string brojProstorije, DateTime datum)
        {
            if (service.DobaviProstoriju(brojProstorije).TipProstorije != TipProstorije.operacionaSala)
            {
                if (ProstorijaImaPregledePosleDatuma(brojProstorije, datum))
                    return true;
            }
            else
            {
                if (ProstorijaImaOperacijePosleDatuma(brojProstorije, datum))
                    return true;
            }
            return false;
        }

        private bool ProstorijaImaPregledePosleDatuma(string brojProstorije, DateTime datum)
        {
            foreach (Pregled p in service.DobaviPregledeProstorije(brojProstorije))
            {
                if (p.Datum >= datum)
                    return true;
            }
            return false;
        }

        private bool ProstorijaImaOperacijePosleDatuma(string brojProstorije, DateTime datum)
        {
            foreach (Operacija o in service.DobaviOperacijeProstorije(brojProstorije))
            {
                if (o.Datum >= datum)
                    return true;
            }

            return false;
        }

        public double DobaviKvadraturu(string brojProstorije)
        {
            return service.DobaviKvadraturu(brojProstorije);
        }

        public void IzmeniProstoriju(Prostorija prostorija)
        {
            service.IzmeniProstoriju(prostorija);
        }
    }
}
