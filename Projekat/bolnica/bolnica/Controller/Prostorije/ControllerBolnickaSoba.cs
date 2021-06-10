using Bolnica.Model.Prostorije;
using Bolnica.Services.Prostorije;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Controller.Prostorije
{
    public class ControllerBolnickaSoba
    {
        private ServiceBolnickaSoba service;

        public ControllerBolnickaSoba()
        {
            service = new ServiceBolnickaSoba();
        }
        public void SacuvajBolnickuSobu(BolnickaSoba bolnickaSoba)
        {
            service.SacuvajBolnickuSobu(bolnickaSoba);
        }

        public void ObrisiBolnickuSobu(string brojProstorije)
        {
            service.ObrisiBolnickuSobu(brojProstorije);
        }

        public List<BolnickaSoba> DobaviSveBolnickeSobe()
        {
            return service.DobaviSveBolnickeSobe();
        }

        public List<BolnickaSoba> DobaviBolnickeSobeBezOpreme(List<Zaliha> zaliheOpreme)
        {
            return service.DobaviBolnickeSobeBezOpreme(zaliheOpreme);
        }
    }
}
