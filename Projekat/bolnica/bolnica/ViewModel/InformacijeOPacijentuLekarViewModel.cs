using Bolnica.Commands;
using Bolnica.Model.Pregledi;
using Model.Korisnici;
using Model.Pacijenti;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Bolnica.ViewModel
{
    public class InformacijeOPacijentuLekarViewModel : ViewModel
    {
        #region POLJA

        public static ObservableCollection<Hospitalizacija> Hospitalizacije
        {
            get;
            set;
        }
        private bool skociNaHospitalizuj;
        public bool SkociNaHospitalizuj
        {
            get { return skociNaHospitalizuj; }
            set
            {
                skociNaHospitalizuj = value;
                OnPropertyChanged();
            }
        }
        private bool skociNaTab;
        public bool SkociNaTab
        {
            get { return skociNaTab; }
            set
            {
                skociNaTab = value;
                OnPropertyChanged();
            }
        }
        public bool gost { get; set; }
        private Pacijent pacijentIzabrani;
        public Visibility vidljiv { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string pol { get; set; }
        public string jmbg { get; set; }

        public string datumR { get; set; }
        public string adresaS { get; set; }
        public string brojTel { get; set; }
        public string emailAdresa { get; set; }
        public List<string> alergeni { get; set; }
        public string korisnickoIme { get; set; }

        public string zanimanje { get; set; }
        public BracniStatus bracniStatus { get; set; }
        public bool osiguranje { get; set; }

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

        private RelayCommand skociNaLevoKomanda;
        public RelayCommand SkociNaLevoKomanda
        {
            get { return skociNaLevoKomanda; }
            set
            {
                skociNaLevoKomanda = value;

            }
        }

        public void Executed_SkociNaLevoKomanda(object obj)
        {
           SkociNaTab = true;
        }

        public bool CanExecute_SkociNaLevoKomanda(object obj)
        {
            return true;
        }

        private RelayCommand skociNaEnterKomanda;
        public RelayCommand SkociNaEnterKomanda
        {
            get { return skociNaEnterKomanda; }
            set
            {
                skociNaEnterKomanda = value;

            }
        }

        public void Executed_SkociNaEnterKomanda(object obj)
        {
            SkociNaHospitalizuj = true;
        }

        public bool CanExecute_SkociNaEnterKomanda(object obj)
        {
            return true;
        }

       


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

        private RelayCommand hospitalizujKomanda;
        public RelayCommand HospitalizujKomanda
        {
            get { return hospitalizujKomanda; }
            set
            {
                hospitalizujKomanda = value;

            }
        }

        public void Executed_HospitalizujKomanda(object obj)
        {
            inject.InformacijeOPacijentuLekarController.HospitalizacijaPacijenta(pacijentIzabrani,1);

        }

        public bool CanExecute_HospitalizujKomanda(object obj)
        {
            return true;
        }
        private RelayCommand izmeniKomanda;
        public RelayCommand IzmeniKomanda
        {
            get { return izmeniKomanda; }
            set
            {
                izmeniKomanda = value;

            }
        }

        public void Executed_IzmeniKomanda(object obj)
        {
            inject.InformacijeOPacijentuLekarController.HospitalizacijaPacijenta(pacijentIzabrani,2);

        }

        public bool CanExecute_IzmeniKomanda(object obj)
        {
            return true;
        }
        #endregion
        public InformacijeOPacijentuLekarViewModel(Pacijent izabraniPacijent)
        {
            pacijentIzabrani = izabraniPacijent;
            PostaviZajednickaPolja(izabraniPacijent);
            PostaviAlergene(izabraniPacijent);
            UpravljajZdravstvenimKartonom(izabraniPacijent);
            ZatvoriKomanda = new RelayCommand(Executed_ZatvoriKomanda, CanExecute_ZatvoriKomanda);
            HospitalizujKomanda = new RelayCommand(Executed_HospitalizujKomanda, CanExecute_HospitalizujKomanda);
            SkociNaEnterKomanda = new RelayCommand(Executed_SkociNaEnterKomanda, CanExecute_SkociNaEnterKomanda);
            SkociNaLevoKomanda = new RelayCommand(Executed_SkociNaLevoKomanda, CanExecute_SkociNaLevoKomanda);
            IzmeniKomanda = new RelayCommand(Executed_IzmeniKomanda, CanExecute_IzmeniKomanda);

        }
        #region POMOCNE FUNKCIJE
        public void UpravljajZdravstvenimKartonom(Pacijent izabraniPacijent)
        {
            if (izabraniPacijent.Guest)
            {
                vidljiv = Visibility.Hidden;
            }
            else
            {
                vidljiv = Visibility.Visible;
                korisnickoIme = izabraniPacijent.KorisnickoIme;

                zanimanje = izabraniPacijent.ZdravstveniKarton.Zanimanje;
                bracniStatus = izabraniPacijent.ZdravstveniKarton.BracniStatus;
                osiguranje = izabraniPacijent.ZdravstveniKarton.Osiguranje;

            }
        }
        public void PostaviZajednickaPolja(Pacijent izabraniPacijent)
        {
            SkociNaHospitalizuj = false;
            SkociNaTab = false;
            Hospitalizacije = new ObservableCollection<Hospitalizacija>();
            Inject = new Injector();
            gost = izabraniPacijent.Guest;
            ime = izabraniPacijent.Ime;
            prezime = izabraniPacijent.Prezime;
            PostaviHospitalizacije();
            if (izabraniPacijent.Pol.Equals(Pol.muski))
            {
                pol = "Muski";
            }
            else
            {
                pol = "Zenski";
            }

            jmbg = izabraniPacijent.Jmbg;
            datumR = izabraniPacijent.DatumRodjenja.ToString();
            adresaS = izabraniPacijent.AdresaStanovanja;
            brojTel = izabraniPacijent.BrojTelefona;
            emailAdresa = izabraniPacijent.Email;
        }
        public void PostaviAlergene(Pacijent izabraniPacijent)
        {
            List<Sastojak>? alergeniPacijenta = izabraniPacijent.Alergeni;
            List<String> alergeniZaPrikaz = new List<string>();

            if (alergeniPacijenta != null)
            {
                for (int i = 0; i < izabraniPacijent.Alergeni.Count; i++)
                {
                    foreach (Sastojak s in inject.InformacijeOPacijentuLekarController.DobijSastojke())
                    {
                        if (izabraniPacijent.Alergeni[i].Id.Equals(s.Id))
                        {
                            alergeniZaPrikaz.Add(s.Naziv);
                        }
                    }
                }
            }

            alergeni = alergeniZaPrikaz;
        }

        public void PostaviHospitalizacije()
        {
            List<Hospitalizacija> sveHospitalizacije = inject.InformacijeOPacijentuLekarController.DobijHospitalizacije();
            for (int i = 0; i < sveHospitalizacije.Count; i++)
            {
                if (sveHospitalizacije[i].Pacijent.Jmbg.Equals(pacijentIzabrani.Jmbg))
                {
                    Hospitalizacije.Add(sveHospitalizacije[i]);
                }
            }
          
        }
        #endregion





    }
}
