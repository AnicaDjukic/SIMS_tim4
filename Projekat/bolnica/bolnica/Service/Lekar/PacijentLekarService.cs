using Bolnica.DTO;
using Bolnica.Forms;
using Bolnica.Model.Pregledi;
using Bolnica.Model.Prostorije;
using Bolnica.Repository.Pregledi;
using Bolnica.ViewModel;
using Model.Korisnici;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Bolnica.Services
{
    public class PacijentLekarService
    {
        FileRepositorySastojak skladisteSastojaka = new FileRepositorySastojak();
        FileRepositoryHospitalizacija skladisteHospitalizacija = new FileRepositoryHospitalizacija();
        public List<Sastojak> DobijSastojke()
        {
            return skladisteSastojaka.GetAll();
        }
        public List<Hospitalizacija> DobijHospitalizacije()
        {
            return skladisteHospitalizacija.GetAll();
        }

        public void HospitalizacijaPacijenta(Pacijent pacijent,int akcija)
        {
            List<Hospitalizacija> sveHospitalizacije = skladisteHospitalizacija.GetAll();
            if (akcija == 1) {
                for (int i = 0; i < sveHospitalizacije.Count; i++)
                {
                    if (sveHospitalizacije[i].Pacijent.Jmbg.Equals(pacijent.Jmbg) && sveHospitalizacije[i].DatumPocetka <= DateTime.Now && sveHospitalizacije[i].DatumZavrsetka > DateTime.Now)
                    {
                        MessageBox.Show("Pacijent je vec hospitalizovan");
                        return;
                    }
                } 
            }
            else
            {
                bool hospitalizovan = false;
                for (int i = 0; i < sveHospitalizacije.Count; i++)
                {
                    if (sveHospitalizacije[i].Pacijent.Jmbg.Equals(pacijent.Jmbg) && sveHospitalizacije[i].DatumPocetka <= DateTime.Now && sveHospitalizacije[i].DatumZavrsetka > DateTime.Now)
                    {
                        hospitalizovan = true;
                    }
                }
                if (!hospitalizovan)
                {
                    MessageBox.Show("Pacijent nije trenutno hospitalizovan");
                    return;
                }
            }
                       
             HospitalizacijaLekarViewModel vm = new HospitalizacijaLekarViewModel(pacijent);
             FormHospitalizujLekar ff = new FormHospitalizujLekar(vm);
        }

        
    }
}
