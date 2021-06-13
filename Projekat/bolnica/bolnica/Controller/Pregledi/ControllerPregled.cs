using Bolnica.Service.Upravnik;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Controller.Pregledi
{
    public class ControllerPregled
    {
        private ServicePregled service;
        public ControllerPregled()
        {
            service = new ServicePregled();
        }

        public List<Pregled> DobaviSvePregledeProstorije(string brojProstorije)
        {
            List<Pregled> preglediProstorije = new List<Pregled>();
            foreach (Pregled p in service.DobaviSvePreglede())
            {
                if (p.Prostorija.BrojProstorije == brojProstorije)
                {
                    preglediProstorije.Add(p);
                }
            }

            return preglediProstorije;
        }
    }
}
