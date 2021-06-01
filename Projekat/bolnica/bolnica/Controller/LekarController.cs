using Bolnica.DTO;
using Bolnica.Forms;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.Services;
using Bolnica.ViewModel;
using Model.Korisnici;
using Model.Pacijenti;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Bolnica.Controller
{
    public class LekarController
    {
        private LekarService service = new LekarService();

        public List<Pregled> DobijPreglede()
        {
            return service.DobijPreglede();
        }
        public List<Pacijent> DobijPacijente()
        {
            return service.DobijPacijente();
        }
        public List<Prostorija> DobijProstorije()
        {
            return service.DobijProstorije();
        }
        public List<Lekar> DobijLekare()
        {
            return service.DobijLekare();
        }
        public List<Operacija> DobijOperacije()
        {
            return service.DobijOperacije();
        }
        public List<Lek> DobijLekove()
        {
            return service.DobijLekove();
        }
        public void ZakaziPregled(LekarServiceDTO lekarServiceDTO)
        {
            service.ZakaziPregled(lekarServiceDTO);
        }
        public void OtkaziPregled(LekarServiceDTO lekarServiceDTO)
        {
            service.OtkaziPregled(lekarServiceDTO);
        }
        public void IzmeniPregled(LekarServiceDTO lekarServiceDTO)
        {
            service.IzmeniPregled(lekarServiceDTO);
        }
        public void InformacijeOPacijentu(LekarServiceDTO lekarServiceDTO)
        {
            service.InformacijeOPacijentu(lekarServiceDTO);
        }

        public void DemoKomanda()
        {
            service.DemoKomanda();
        }
        public void SkociNaEnter(LekarServiceDTO lekarServiceDTO)
        {
            service.SkociNaEnter(lekarServiceDTO);
        }
        public void SkociNaLevo(LekarServiceDTO lekarServiceDTO)
        {
            service.SkociNaLevo(lekarServiceDTO);
        }
        public void SkociNaTab(LekarServiceDTO lekarServiceDTO)
        {
            service.SkociNaTab(lekarServiceDTO);
        }
        public void SkociNaEnterIstorija(LekarServiceDTO lekarServiceDTO)
        {
            service.SkociNaEnterIstorija(lekarServiceDTO);
        }
        public void SkociNaLevoIstorija(LekarServiceDTO lekarServiceDTO)
        {
            service.SkociNaLevoIstorija(lekarServiceDTO);
        }
        public void SkociNaTabIstorija(LekarServiceDTO lekarServiceDTO)
        {
            service.SkociNaTabIstorija(lekarServiceDTO);
        }
        public void Anamneza(LekarServiceDTO lekarServiceDTO)
        {
            service.Anamneza(lekarServiceDTO);
        }
        public void AnamnezaIstorija(LekarServiceDTO lekarServiceDTO)
        {
            service.AnamnezaIstorija(lekarServiceDTO);
        }
        public void IzmeniLek(LekarServiceDTO lekarServiceDTO)
        {
            service.IzmeniLek(lekarServiceDTO);
        }
        public void SkociNaEnterLek(LekarServiceDTO lekarServiceDTO)
        {
            service.SkociNaEnterLek(lekarServiceDTO);
        }
        public void SkociNaLevoLek(LekarServiceDTO lekarServiceDTO)
        {
            service.SkociNaLevoLek(lekarServiceDTO);
        }
        public void SkociNaTabLek(LekarServiceDTO lekarServiceDTO)
        {
            service.SkociNaTabLek(lekarServiceDTO);
        }
        public void OdobriLek(LekarServiceDTO lekarServiceDTO)
        {
            service.OdobriLek(lekarServiceDTO);
        }
        public void VratiNaIzmenu(LekarServiceDTO lekarServiceDTO)
        {
            service.VratiNaIzmenu(lekarServiceDTO);
        }

    }
}
