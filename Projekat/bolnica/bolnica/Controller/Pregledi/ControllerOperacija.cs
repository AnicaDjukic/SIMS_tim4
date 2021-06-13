using Bolnica.Service.Upravnik;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Controller.Pregledi
{
    public class ControllerOperacija
    {
        private ServiceOperacija service;

        public ControllerOperacija()
        {
            service = new ServiceOperacija();
        }

        public List<Operacija> DobaviSveOperacijeProstorije(string brojProstorije)
        {
            List<Operacija> operacijeProstorije = new List<Operacija>();
            foreach (Operacija o in service.DobaviSveOperacije())
            {
                if (o.Prostorija.BrojProstorije == brojProstorije)
                {
                    operacijeProstorije.Add(o);
                }
            }

            return operacijeProstorije;
        }
    }
}
