using bolnica.Forms;
using Bolnica.DTO;
using Bolnica.Model.Pregledi;
using Bolnica.Services.Pregledi;
using Model.Pregledi;
using System;
using System.Collections.Generic;

namespace Bolnica.Controller.Pregledi
{
    public class ControllerLek
    {
        private ServiceLek service;

        public ControllerLek()
        {
            service = new ServiceLek();
        }

        public bool LekPostoji(int id)
        {
            return service.LekPostoji(id);
        }

        public bool LekIspravan(int id)
        {
            return (!LekPostoji(id) || !FormUpravnik.clickedDodaj);
        }

        public void ObrisiLek(int id)
        {
            service.ObrisiLek(id);
        }

        public Lek NapraviLek(LekDTO lekDto, List<SastojakDTO> sastojci)
        {
            Lek lek = new Lek(lekDto.Id, lekDto.Naziv, lekDto.Proizvodjac, lekDto.KolicinaUMg, lekDto.Status, lekDto.Zalihe);
            foreach(SastojakDTO s in sastojci)
            {
                Sastojak sastojak = new Sastojak { Id = s.Id, Naziv = s.Naziv };
                lek.Sastojak.Add(sastojak);
            }
            return lek;
        }

        public List<LekDTO> DobaviSveLekoveDTO()
        {
            List<LekDTO> lekovi = new List<LekDTO>();
            foreach(Lek l in service.DobaviSveLekove())
            {
                lekovi.Add(new LekDTO(l));
            }
            return lekovi;
        }

        public List<Lek> DobaviSveLekove()
        {
            return service.DobaviSveLekove();
        }

        public void SacuvajLek(Lek lek)
        {
            service.SacuvajLek(lek);
        }

        public Lek DobaviLek(int id)
        {
            return service.DobaviLek(id);
        }

        public List<Sastojak> DobaviSastojkeLeka(Lek lek)
        {
            return service.DobaviSastojkeLeka(lek);
        }

        public List<Lek> DobaviSveZameneLeka(Lek lek)
        {
            return service.DobaviSveZameneLeka(lek);
        }
    }
}
