using Bolnica.Model.Prostorije;
using Bolnica.Services.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Controller.Prostorije
{
    public class ControllerRenoviranje
    {
        private ServiceRenoviranje service;

        public ControllerRenoviranje()
        {
            service = new ServiceRenoviranje();
        }

        internal List<Renoviranje> DobaviRenoviranjaProstorije(string brojProstorije)
        {
            return service.DobaviRenoviranjaProstorije(brojProstorije);
        }

        internal void SacuvajRenoviranje(Renoviranje novoRenoviranje)
        {
            service.SacuvajRenoviranje(novoRenoviranje);
        }

        public List<Renoviranje> DobaviSvaRenoviranja()
        {
            return service.DobaviSvaRenoviranja();
        }

        public void IzmeniRenoviranje(Renoviranje renoviranje)
        {
            service.Izmeni(renoviranje);
        }

        internal void PodeliISpojiProstorije()
        {
            service.PodeliISpojiProstorije();
        }
    }
}
