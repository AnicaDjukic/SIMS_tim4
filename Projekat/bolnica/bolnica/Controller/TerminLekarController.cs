using Bolnica.DTO;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.Model.Prostorije;
using Bolnica.Services;
using Bolnica.ViewModel;
using Model.Korisnici;
using Model.Pacijenti;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;

namespace Bolnica.Controller
{
    public class TerminLekarController
    {

        private TerminLekarService service = new TerminLekarService();

        public List<Lekar> DobijLekare()
        {
            return service.DobijLekare();
        }
        public List<Pacijent> DobijPacijente()
        {
            return service.DobijPacijente();
        }
        public List<Pregled> DobijPreglede()
        {
            return service.DobijPreglede();
        }
        public List<Operacija> DobijOperacije()
        {
            return service.DobijOperacije();
        }
        public List<Prostorija> DobijProstorije()
        {
            return service.DobijProstorije();
        }
        public void PotvrdiIzmenu(TerminLekarDTO terminDTO)
        {
            service.PotvrdiIzmenu(terminDTO);
        }
        public void Potvrdi(TerminLekarDTO terminDTO)
        {
            service.Potvrdi(terminDTO);
        }
        public bool PostojiLekar(TerminLekarDTO terminDTO)
        {
            return service.PostojiLekar(terminDTO);
        }
        public bool PacijentSlobodanUToVreme(TerminLekarDTO terminDTO)
        {
            return service.PacijentSlobodanUToVreme(terminDTO);
        }
        public bool PacijentSlobodanUToVremeIzmeni(TerminLekarDTO terminDTO)
        {
            return service.PacijentSlobodanUToVremeIzmeni(terminDTO);
        }
        public bool PostojiProstorija(TerminLekarDTO terminDTO)
        {
            return service.PostojiProstorija(terminDTO);
        }
        public bool ProstorijaSlobodna(TerminLekarDTO terminDTO)
        {
            return service.ProstorijaSlobodna(terminDTO);
        }
        public List<TimeSpan> LekarFiltriranje(TerminLekarDTO terminDTO)
        {
            return service.LekarFiltriranje(terminDTO);
        }
        public List<string> DatumFiltriranje(TerminLekarDTO terminDTO)
        {
            return service.DatumFiltriranje(terminDTO);
        }
        public string LekarComboNaTab(TerminLekarDTO terminDTO)
        {
            return service.LekarComboNaTab(terminDTO);
        }
        public List<string> SpecijalizacijaComboNaTab(TerminLekarDTO terminDTO)
        {
            return service.SpecijalizacijaComboNaTab(terminDTO);
        }
        public bool LekarSlobodanUToVreme(TerminLekarDTO terminDTO)
        {
            return service.LekarSlobodanUToVreme(terminDTO);
        }
        public bool LekarSlobodanUToVremeIzmeni(TerminLekarDTO terminDTO)
        {
            return service.LekarSlobodanUToVremeIzmeni(terminDTO);
        }
    }
}
