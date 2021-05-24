using Bolnica.DTO;
using Bolnica.Model.Pregledi;
using Bolnica.Model.Prostorije;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Services
{
    public class HospitalizujLekarService
    {
        FileStorageHospitalizacija skladisteHospitalizacija = new FileStorageHospitalizacija();
        FileStorageProstorija skladisteProstorija = new FileStorageProstorija();
        List<Hospitalizacija> sveHospitalizacije;
        List<BolnickaSoba> sveBolnickeSobe;


        public void Potvrdi(HospitalizacijaDTO hospitalizacijaDTO)
        {
            InicijalizujPodatke();
            BolnickaSoba izabranaBolnickaSoba = DobijBolnickuSobu(hospitalizacijaDTO);
            Hospitalizacija hospitalizacija = new Hospitalizacija(hospitalizacijaDTO.datumPocetka, hospitalizacijaDTO.datumZavrsetka, IzracunajId(), hospitalizacijaDTO.izabraniPacijent, izabranaBolnickaSoba);
            SacuvajIliIzmeni(hospitalizacijaDTO, hospitalizacija);
        }
        
        public void InicijalizujPodatke()
        {
            sveHospitalizacije = new List<Hospitalizacija>();
            sveBolnickeSobe = new List<BolnickaSoba>();
            sveHospitalizacije = skladisteHospitalizacija.GetAll();
            sveBolnickeSobe = skladisteProstorija.GetAllBolnickeSobe();
        }

        public void SacuvajIliIzmeni(HospitalizacijaDTO hospitalizacijaDTO, Hospitalizacija hospitalizacija)
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
        public BolnickaSoba DobijBolnickuSobu(HospitalizacijaDTO hospitalizacijaDTO)
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
        public int IzracunajId()
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
    }
}
