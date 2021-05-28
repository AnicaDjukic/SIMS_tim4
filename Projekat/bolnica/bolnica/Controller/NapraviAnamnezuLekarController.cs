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
    public class NapraviAnamnezuLekarController
    {
        private NapraviAnamnezuLekarService service = new NapraviAnamnezuLekarService();
        public void DodajLek(NapraviAnamnezuLekarServiceDTO anamnezaDTO)
        {
            service.DodajLek(anamnezaDTO);
        }
        public void Potvrdi(NapraviAnamnezuLekarServiceDTO anamnezaDTO)
        {
            service.Potvrdi(anamnezaDTO);
        }
        public void ObrisiRecept(NapraviAnamnezuLekarServiceDTO anamnezaDTO)
        {
            service.ObrisiRecept(anamnezaDTO);
        }
        public void ZakaziPregled(NapraviAnamnezuLekarServiceDTO anamnezaDTO)
        {
            service.ZakaziPregled(anamnezaDTO);
        }
        public void VidiDetaljeOReceptu(NapraviAnamnezuLekarServiceDTO anamnezaDTO)
        {
            service.VidiDetaljeOReceptu(anamnezaDTO);
        }
        public void PredjiNaScrollBar(NapraviAnamnezuLekarServiceDTO anamnezaDTO)
        {
            service.PredjiNaScrollBar(anamnezaDTO);
        }
        public void ZaustaviStrelice(NapraviAnamnezuLekarServiceDTO anamnezaDTO)
        {
            service.ZaustaviStrelice(anamnezaDTO);
        }


    }
}
