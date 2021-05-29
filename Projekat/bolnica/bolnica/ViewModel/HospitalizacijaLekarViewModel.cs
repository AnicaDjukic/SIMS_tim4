using Bolnica.Commands;
using Bolnica.Controller;
using Bolnica.DTO;
using Bolnica.Model.Pregledi;
using Bolnica.Model.Prostorije;
using Model.Korisnici;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.ViewModel
{
    public class HospitalizacijaLekarViewModel : ViewModel
    {
        #region POLJA
        public DateTime datumPocetka { get; set; }
        public DateTime datumZavrsetka { get; set; }
        public string brojBolnickeSobe { get; set; }
        public Pacijent izabraniPacijent { get; set; }

        private List<string> slobodneBolniceSobe;
        private List<BolnickaSoba> sveBolnickeSobe;
        private List<Hospitalizacija> sveHospitalizacije;
        public List<string> ItemSourceBrojBolnickeSobe { get; set; }
        private bool itemSourceBrojBolnickeSobeComboOtvori;
        public bool ItemSourceBrojBolnickeSobeComboOtvori { 
            get { return itemSourceBrojBolnickeSobeComboOtvori; }
            set
            {
                itemSourceBrojBolnickeSobeComboOtvori = value;
                OnPropertyChanged();
            }
            }
        private Injector inject;
        public Injector Inject
        {
            get { return inject; }
            set
            {
                inject = value;
            }
        }
        #endregion

        #region KOMANDE
        public Action ZatvoriAkcija { get; set; }

        private RelayCommand zatvoriKomanda;
        public RelayCommand ZatvoriKomanda
        {
            get { return zatvoriKomanda; }
            set
            {
                zatvoriKomanda = value;

            }
        }

        public void Executed_ZatvoriKomanda(object obj)
        {
            ZatvoriAkcija();
        }

        public bool CanExecute_ZatvoriKomanda(object obj)
        {
            return true;
        }

         private RelayCommand potvrdiKomanda;
        public RelayCommand PotvrdiKomanda
        {
            get { return potvrdiKomanda; }
            set
            {
                potvrdiKomanda = value;

            }
        }

        public void Executed_PotvrdiKomanda(object obj)
        {
            if(inject.HospitalizacijaLekarController.Potvrdi(new HospitalizacijaLekarDTO(datumPocetka, datumZavrsetka, brojBolnickeSobe, izabraniPacijent)))
            {
                ZatvoriAkcija();
            }
           
        }

        public bool CanExecute_PotvrdiKomanda(object obj)
        {
            return true;
        }

        private RelayCommand otvoriComboNaEnterKomanda;
        public RelayCommand OtvoriComboNaEnterKomanda
        {
            get { return otvoriComboNaEnterKomanda; }
            set
            {
                otvoriComboNaEnterKomanda = value;

            }
        }

        public void Executed_OtvoriComboNaEnterKomanda(object obj)
        {
            ItemSourceBrojBolnickeSobeComboOtvori = true;
        }

        public bool CanExecute_OtvoriComboNaEnterKomanda(object obj)
        {
            return true;
        }

        #endregion
        public HospitalizacijaLekarViewModel(Pacijent izabraniPacijent)
        {
            InicijalizujPodatke(izabraniPacijent);
            PripremiBolnickeSobe();
            IzfiltrirajBolnickeSobe();
            PodesiPodatke();
            NapraviKomande();   
        }
        public void InicijalizujPodatke(Pacijent izabraniPacijent)
        {
            Inject = new Injector();
            this.izabraniPacijent = izabraniPacijent;
            ItemSourceBrojBolnickeSobeComboOtvori = false;
            datumPocetka = DateTime.Now;
            datumZavrsetka = DateTime.Now;
            brojBolnickeSobe = "";
            slobodneBolniceSobe = new List<string>();
            ItemSourceBrojBolnickeSobe = new List<string>();
            sveBolnickeSobe = inject.HospitalizacijaLekarController.DobijBolnickeSobe();
            sveHospitalizacije = inject.HospitalizacijaLekarController.DobijSveHospitalizacije();
        }
        #region POMOCNE FUNKCIJE
        public void PripremiBolnickeSobe()
        {
            for (int i = 0; i < sveBolnickeSobe.Count; i++)
            {
                for (int h = 0; h < sveHospitalizacije.Count; h++)
                {
                    if (sveBolnickeSobe[i].BrojProstorije.Equals(sveHospitalizacije[h].Prostorija.BrojProstorije) && sveHospitalizacije[h].DatumPocetka < DateTime.Now && sveHospitalizacije[h].DatumZavrsetka > DateTime.Now)
                    {
                        sveBolnickeSobe[i].UkBrojKreveta = sveBolnickeSobe[i].UkBrojKreveta - 1;
                    }
                }

            }
        }
        public void IzfiltrirajBolnickeSobe()
        {
            for (int i = 0; i < sveBolnickeSobe.Count; i++)
            {
                if (sveBolnickeSobe[i].UkBrojKreveta > 0)
                {
                    slobodneBolniceSobe.Add(sveBolnickeSobe[i].BrojProstorije);
                }
            }
            ItemSourceBrojBolnickeSobe = slobodneBolniceSobe;
        }
        public void PodesiPodatke()
        {
            for (int i = 0; i < sveHospitalizacije.Count; i++)
            {
                if (sveHospitalizacije[i].Pacijent.Jmbg.Equals(izabraniPacijent.Jmbg) && sveHospitalizacije[i].DatumPocetka <= DateTime.Now && sveHospitalizacije[i].DatumZavrsetka > DateTime.Now)
                {
                    brojBolnickeSobe = sveHospitalizacije[i].Prostorija.BrojProstorije;
                    datumPocetka = sveHospitalizacije[i].DatumPocetka;
                    datumZavrsetka = sveHospitalizacije[i].DatumZavrsetka;
                }
            }
        }

        public void NapraviKomande()
        {
            ZatvoriKomanda = new RelayCommand(Executed_ZatvoriKomanda, CanExecute_ZatvoriKomanda);
            OtvoriComboNaEnterKomanda = new RelayCommand(Executed_OtvoriComboNaEnterKomanda, CanExecute_OtvoriComboNaEnterKomanda);
            PotvrdiKomanda = new RelayCommand(Executed_PotvrdiKomanda, CanExecute_PotvrdiKomanda);
        }
        #endregion
    }

}
