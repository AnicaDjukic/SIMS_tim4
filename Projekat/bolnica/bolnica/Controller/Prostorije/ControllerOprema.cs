using Bolnica.Model.Prostorije;
using Bolnica.Services.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Controller.Prostorije
{
    public class ControllerOprema
    {
        private ServiceOprema service;
        public ControllerOprema()
        {
            service = new ServiceOprema();
        }

        public bool OpremaPostoji(string sifra)
        {
            return service.OpremaPostoji(sifra);
        }

        public void ObrisiOpremu(string sifra)
        {
            service.ObrisiOpremu(sifra);
        }

        public void SacuvajOpremu(Oprema oprema)
        {
            service.SacuvajOpremu(oprema);
        }

        public Oprema DobaviOpremu(string sifraOpreme)
        {
            return service.DobaviOpremu(sifraOpreme);
        }

        public List<Oprema> DobaviSvuOpremu()
        {
            return service.DobaviSvuOpremu();
        }
    }
}
