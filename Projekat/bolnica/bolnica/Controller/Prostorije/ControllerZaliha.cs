using Bolnica.Model.Prostorije;
using Bolnica.Services.Prostorije;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Bolnica.Controller.Prostorije
{
    public class ControllerZaliha
    {
        private ServiceZaliha service;

        public ControllerZaliha()
        {
            service = new ServiceZaliha();
        }

        public void ObrisiZaliheOpreme(string sifra)
        {
            service.ObrisiZaliheOpreme(sifra);
        }

        public void PrebaciBuduceZaliheUZalihe(List<BuducaZaliha> buduceZalihe)
        {
            service.PrebaciBuduceZaliheUZalihe(buduceZalihe);
        }

        public List<Zaliha> DobaviZaliheOpreme(string sifra)
        {
            return service.DobaviZaliheOpreme(sifra);
        }

        public bool ValidnaKolicina(int kolicinaZaPremestanje, Oprema opremaZaSkladistenje)
        {
            return service.ValidnaKolicina(kolicinaZaPremestanje, opremaZaSkladistenje);
        }

        public int DobaviRezervisanuKolicinu(string sifra)
        {
            return service.IzracunajRezervisanuKolicinu(sifra);
        }

        public void SacuvajZalihe(ObservableCollection<Zaliha> zalihe)
        {
            foreach (Zaliha z in zalihe)
                service.SacuvajZalihu(z);
        }

        public List<Zaliha> NapraviZaliheOdBuducihZaliha(List<BuducaZaliha> buduceZalihe)
        {
            return service.NapraviZaliheOdBuducihZaliha(buduceZalihe);
        }

        public void SacuvajZalihe(List<Zaliha> zalihe)
        {
            service.SacuvajZalihe(zalihe);
        }

        public List<Zaliha> DobaviZalihe()
        {
            return service.DobaviSveZalihe();
        }

        public void ObrisiZalihu(Zaliha z)
        {
            service.ObrisiZalihu(z);
        }

        public List<Zaliha> DobaviZaliheProstorije(string brojProstorije)
        {
            return service.DobaviZaliheProstorije(brojProstorije);
        }
    }
}
