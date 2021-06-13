using Bolnica.DTO;
using Bolnica.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Controller
{
    public class ZazaziPregledPacijentController
    {
        private ZakaziPregledPacijentService service = new ZakaziPregledPacijentService();

        public void Potvrdi(ZakazaniPregledDTO pregledDTO)
        {
            service.Potvrdi(pregledDTO);
        }

        public void Otkazi(ZakazaniPregledDTO pregledDTO)
        {
            service.Otkazi(pregledDTO);
        }

        public void NasiPredlozi(ZakazaniPregledDTO pregledDTO)
        {
            service.NasiPredlozi(pregledDTO);
        }
    }
}
