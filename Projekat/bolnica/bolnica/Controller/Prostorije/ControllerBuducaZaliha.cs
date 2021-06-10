using Bolnica.Model.Prostorije;
using Bolnica.Services.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Controller.Prostorije
{
    public class ControllerBuducaZaliha
    {
        private ServiceBuducaZaliha service;

        public ControllerBuducaZaliha()
        {
            service = new ServiceBuducaZaliha();
        }

        internal void SacuvajBuduceZalihe(List<BuducaZaliha> buduceZalihe)
        {
            service.SacuvajBuduceZalihe(buduceZalihe);
        }

        public void ObrisiBuduceZaliheOpremeZaDatum(string sifraOpreme, DateTime datum)
        {
            service.ObrisiBuduceZaliheOpremeZaDatum(sifraOpreme, datum);
        }

        public List<BuducaZaliha> DobaviBuduceZaliheOpreme(string sifra)
        {

            return service.DobaviBuduceZaliheOpreme(sifra);
        }

        public List<BuducaZaliha> DobaviBuduceZaliheIsteklogDatuma()
        {
            List<BuducaZaliha> buduceZalihe = new List<BuducaZaliha>();
            foreach (BuducaZaliha bz in service.DobaviSveBuduceZalihe())
            {
                if (bz.Datum <= DateTime.Now.Date)
                {
                    buduceZalihe.Add(bz);
                }
            }

            return buduceZalihe;
        }

        public void ObrisiBuduceZalihe(List<BuducaZaliha> buduceZalihe)
        {
            service.ObrisiBuduceZalihe(buduceZalihe);
        }
    }
}
