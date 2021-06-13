using Bolnica.Model.Prostorije;
using Bolnica.Repository.Prostorije;
using Bolnica.Services.Prostorije;
using Model.Prostorije;
using System.Collections.Generic;

namespace Bolnica.Controller.Prostorije
{
    public class ControllerBolnickaSoba
    {
        private ServiceBolnickaSoba service;

        public ControllerBolnickaSoba()
        {
            service = new ServiceBolnickaSoba(new FileRepositoryBolnickaSoba());
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

        public BolnickaSoba DobaviBolnickuSobu(string brojBolnickeSobe)
        {
            return service.DobaviBolnickuSobu(brojBolnickeSobe);
        }

        public bool PostojeZauzetiKreveti(string brojProstorije)
        {
            BolnickaSoba bolnickaSoba = service.DobaviBolnickuSobu(brojProstorije);
            return bolnickaSoba.BrojSlobodnihKreveta != bolnickaSoba.UkBrojKreveta;
        }

        public List<BolnickaSoba> DobaviBolnickeSobeNaIstomSpratu(int sprat)
        {
            List<BolnickaSoba> bolnickeSobeNaIstomSpratu = new List<BolnickaSoba>();
            foreach (BolnickaSoba b in service.DobaviSveBolnickeSobe())
            {
                if (b.Sprat == sprat && !b.Obrisana)
                    bolnickeSobeNaIstomSpratu.Add(b);
            }
            return bolnickeSobeNaIstomSpratu;
        }
    }
}
