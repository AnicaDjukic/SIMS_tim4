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
        ServiceLek serviceLek = new ServiceLek();

        public bool LekPostoji(int id)
        {
            return serviceLek.LekPostoji(id);
        }

        public bool LekIspravan(int id)
        {
            return (!LekPostoji(id) || !FormUpravnik.clickedDodaj);
        }

        public void ObrisiLek(int id)
        {
            serviceLek.ObrisiLek(id);
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

        public List<LekDTO> DobaviSveLekove()
        {
            List<LekDTO> lekovi = new List<LekDTO>();
            foreach(Lek l in  serviceLek.DobaviSveLekove())
            {
                lekovi.Add(new LekDTO(l));
            }
            return lekovi;
        }

        public void SacuvajLek(Lek lek)
        {
            serviceLek.SacuvajLek(lek);
        }

        internal Lek DobaviLek(int id)
        {
            return serviceLek.DobaviLek(id);
        }
    }
}
