using Bolnica.DTO;
using Bolnica.Model.Pregledi;
using Bolnica.Model.Prostorije;
using Bolnica.Services;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Bolnica.Controller
{
    public class HospitalizacijaLekarController
    {
        private HospitalizacijaLekarService service = new HospitalizacijaLekarService();


        public bool Potvrdi(HospitalizacijaLekarDTO hospitalizacijaDTO)
        {
            return service.Potvrdi(hospitalizacijaDTO);
        }
        public List<BolnickaSoba> DobijBolnickeSobe()
        {
            return service.DobijBolnickeSobe();
        }
        public List<Hospitalizacija> DobijSveHospitalizacije()
        {
            return service.DobijSveHospitalizacije();
        }
    }
}
