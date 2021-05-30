using bolnica.Forms;
using Bolnica.DTO;
using Bolnica.Model.Pregledi;
using Bolnica.Services.Pregledi;
using Model.Pregledi;
using System;

namespace Bolnica.Controller.Pregledi
{
    class ControllerLek
    {
        ServiceLek serviceLek = new ServiceLek();
        public Lek RegistrujLek(LekDTO lekDto)
        {
            Lek lek = new Lek();
            if (!FormUpravnik.clickedDodaj)
            {
                serviceLek.ObrisiLek(lek);
                
            }
            lek.Id = lekDto.Id;
            lek.Naziv = lekDto.Naziv;
            lek.KolicinaUMg = lekDto.KolicinaUMg;
            lek.Proizvodjac = lekDto.Proizvodjac;
            if (FormUpravnik.clickedDodaj || lekDto.Status == StatusLeka.odbijen)
            {
                lek.Status = StatusLeka.cekaValidaciju;
                lek.Zalihe = 0;
            }
            else
            {
                lek.Zalihe = lekDto.Zalihe;
            }
            serviceLek.SacuvajLek(lek);
            return lek;
        }

        public bool LekPostoji(int id)
        {
            return serviceLek.LekPostoji(id);
        }
    }
}
