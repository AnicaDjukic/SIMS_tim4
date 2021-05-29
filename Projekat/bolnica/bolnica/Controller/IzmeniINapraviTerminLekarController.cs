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
    public class IzmeniINapraviTerminLekarController
    {

        private IzmeniINapraviTerminLekarService service = new IzmeniINapraviTerminLekarService();

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
        public void PotvrdiIzmenu(IzmeniINapraviTerminLekarServiceDTO terminDTO)
        {
            service.PotvrdiIzmenu(terminDTO);
        }
        public void Potvrdi(IzmeniINapraviTerminLekarServiceDTO terminDTO)
        {
            service.Potvrdi(terminDTO);
        }
        public bool PostojiLekar(IzmeniINapraviTerminLekarServiceDTO terminDTO)
        {
            return service.PostojiLekar(terminDTO);
        }
        public bool PacijentSlobodanUToVreme(IzmeniINapraviTerminLekarServiceDTO terminDTO)
        {
            return service.PacijentSlobodanUToVreme(terminDTO);
        }
        public bool PacijentSlobodanUToVremeIzmeni(IzmeniINapraviTerminLekarServiceDTO terminDTO)
        {
            return service.PacijentSlobodanUToVremeIzmeni(terminDTO);
        }
        public bool PostojiProstorija(IzmeniINapraviTerminLekarServiceDTO terminDTO)
        {
            return service.PostojiProstorija(terminDTO);
        }
        public bool ProstorijaSlobodna(IzmeniINapraviTerminLekarServiceDTO terminDTO)
        {
            return service.ProstorijaSlobodna(terminDTO);
        }
        public List<TimeSpan> LekarFiltriranje(IzmeniINapraviTerminLekarServiceDTO terminDTO)
        {
            return service.LekarFiltriranje(terminDTO);
        }
        public List<string> DatumFiltriranje(IzmeniINapraviTerminLekarServiceDTO terminDTO)
        {
            return service.DatumFiltriranje(terminDTO);
        }
        public string LekarComboNaTab(IzmeniINapraviTerminLekarServiceDTO terminDTO)
        {
            return service.LekarComboNaTab(terminDTO);
        }
        public List<string> SpecijalizacijaComboNaTab(IzmeniINapraviTerminLekarServiceDTO terminDTO)
        {
            return service.SpecijalizacijaComboNaTab(terminDTO);
        }
        public bool LekarSlobodanUToVreme(IzmeniINapraviTerminLekarServiceDTO terminDTO)
        {
            return service.LekarSlobodanUToVreme(terminDTO);
        }
        public bool LekarSlobodanUToVremeIzmeni(IzmeniINapraviTerminLekarServiceDTO terminDTO)
        {
            return service.LekarSlobodanUToVremeIzmeni(terminDTO);
        }
    }
}
