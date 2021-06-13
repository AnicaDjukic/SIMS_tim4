using Bolnica.DTO;
using Bolnica.Forms;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.Services;
using Bolnica.ViewModel;
using Model.Korisnici;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bolnica.Controller
{
    public class AnamnezaLekarController
    {
        private AnamnezaLekarService service = new AnamnezaLekarService();

        public List<Pregled> DobijPreglede()
        {
            return service.DobijPreglede();
        }
        public List<Operacija> DobijOperacije()
        {
            return service.DobijOperacije();
        }
        public List<Lek> DobijLekove()
        {
            return service.DobijLekove();
        }
        public List<Anamneza> DobijAnamneze()
        {
            return service.DobijAnamneze();
        }
        public List<Lekar> DobijLekare()
        {
            return service.DobijLekare();
        }
        public void DodajLek(AnamnezaLekarDTO anamnezaDTO)
        {
            service.DodajLek(anamnezaDTO);
        }
        public void Potvrdi(AnamnezaLekarDTO anamnezaDTO)
        {
            service.Potvrdi(anamnezaDTO);
        }
        public void ObrisiRecept(AnamnezaLekarDTO anamnezaDTO)
        {
            service.ObrisiRecept(anamnezaDTO);
        }
        public void ZakaziPregled(AnamnezaLekarDTO anamnezaDTO)
        {
            service.ZakaziPregled(anamnezaDTO);
        }
        public void VidiDetaljeOReceptu(AnamnezaLekarDTO anamnezaDTO)
        {
            service.VidiDetaljeOReceptu(anamnezaDTO);
        }
        public void ZaustaviStrelice(AnamnezaLekarDTO anamnezaDTO)
        {
            service.ZaustaviStrelice(anamnezaDTO);
        }


    }
}
