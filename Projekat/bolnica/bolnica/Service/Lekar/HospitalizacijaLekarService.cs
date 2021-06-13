using Bolnica.DTO;
using Bolnica.Model.Pregledi;
using Bolnica.Model.Prostorije;
using Bolnica.Repository.Prostorije;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Bolnica.Services
{
    public class HospitalizacijaLekarService
    {
        FileRepositoryHospitalizacija skladisteHospitalizacija = new FileRepositoryHospitalizacija();
        FileRepositoryProstorija skladisteProstorija = new FileRepositoryProstorija();
        FileRepositoryBolnickaSoba skladisteBolnickihSoba = new FileRepositoryBolnickaSoba();
        List<Hospitalizacija> sveHospitalizacije;
        List<BolnickaSoba> sveBolnickeSobe;


        public bool Potvrdi(HospitalizacijaLekarDTO hospitalizacijaDTO)
        {
            InicijalizujPodatke();
            BolnickaSoba izabranaBolnickaSoba = DobijBolnickuSobu(hospitalizacijaDTO);
            Hospitalizacija hospitalizacija = new Hospitalizacija(hospitalizacijaDTO.datumPocetka, hospitalizacijaDTO.datumZavrsetka, IzracunajId(), hospitalizacijaDTO.izabraniPacijent, izabranaBolnickaSoba);
            if (hospitalizacijaDTO.datumPocetka > hospitalizacijaDTO.datumZavrsetka)
            {
                MessageBox.Show("Datum pocetka je veci od datuma zavrsetka");
                return false;
            }
            else
            {
                SacuvajIliIzmeni(hospitalizacijaDTO, hospitalizacija);
                return true;
            }
        }

        private void InicijalizujPodatke()
        {
            sveHospitalizacije = new List<Hospitalizacija>();
            sveBolnickeSobe = new List<BolnickaSoba>();
            sveHospitalizacije = skladisteHospitalizacija.GetAll();
            sveBolnickeSobe = skladisteBolnickihSoba.GetAll();
        }

        private void SacuvajIliIzmeni(HospitalizacijaLekarDTO hospitalizacijaDTO, Hospitalizacija hospitalizacija)
        {
            for (int i = 0; i < sveHospitalizacije.Count; i++)
            {
                if (sveHospitalizacije[i].Pacijent.Jmbg.Equals(hospitalizacijaDTO.izabraniPacijent.Jmbg) && sveHospitalizacije[i].DatumPocetka <= DateTime.Now && sveHospitalizacije[i].DatumZavrsetka > DateTime.Now)
                {
                    hospitalizacija.Id = sveHospitalizacije[i].Id;
                    skladisteHospitalizacija.Delete(hospitalizacija);
                    skladisteHospitalizacija.Save(hospitalizacija);
                    return;
                }
            }
            skladisteHospitalizacija.Save(hospitalizacija);
        }
        private BolnickaSoba DobijBolnickuSobu(HospitalizacijaLekarDTO hospitalizacijaDTO)
        {
            for (int i = 0; i < sveBolnickeSobe.Count; i++)
            {
                if (sveBolnickeSobe[i].BrojProstorije.Equals(hospitalizacijaDTO.brojBolnickeSobe))
                {
                    return sveBolnickeSobe[i];
                }
            }
            return null;
        }
        private int IzracunajId()
        {
            int max = 0;
            for (int i = 0; i < sveHospitalizacije.Count; i++)
            {
                if (max < sveHospitalizacije[i].Id)
                {
                    max = sveHospitalizacije[i].Id;
                }
            }
            max = max + 1;
            return max;
        }
        public List<BolnickaSoba> DobijBolnickeSobe()
        {
            return skladisteBolnickihSoba.GetAll();
        }
        public List<Hospitalizacija> DobijSveHospitalizacije()
        {
            return skladisteHospitalizacija.GetAll();
        }

    }
}
