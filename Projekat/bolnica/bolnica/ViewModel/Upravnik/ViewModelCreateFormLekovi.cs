using bolnica.Forms;
using Bolnica.Commands;
using Bolnica.DTO;
using Bolnica.Forms.Upravnik;
using Bolnica.Model.Pregledi;
using Bolnica.Services.Pregledi;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Bolnica.ViewModel.Upravnik
{
    public class ViewModelCreateFormLekovi : ViewModel
    {
        #region Polja
        private LekDTO lekZaPrikaz;
        public LekDTO LekZaPrikaz
        {
            get
            {
                return lekZaPrikaz;
            }
            set
            {
                if (value != null)
                {
                    lekZaPrikaz = value;
                    OnPropertyChanged();
                }
            }
        }

        public SastojakDTO SastojakZaUklanjanje { get; set; }

        public Action ZatvoriAkcija { get; set; }

        #region VidljivaLabelaZalihe
        private Visibility vidljivaLabelaZalihe;
        public Visibility VidljivaLabelaZalihe
        {
            get
            {
                return vidljivaLabelaZalihe;
            }
            set
            {
                vidljivaLabelaZalihe = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region VidljivTxtZalihe
        private Visibility vidljivTxtZalihe;
        public Visibility VidljivTxtZalihe
        {
            get
            {
                return vidljivTxtZalihe;
            }
            set
            {
                vidljivTxtZalihe = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region DozvoljenUnosZaliha
        private bool dozvoljenUnosZaliha;
        public bool DozvoljenUnosZaliha
        {
            get
            {
                return dozvoljenUnosZaliha;
            }
            set
            {
                dozvoljenUnosZaliha = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region DozvoljenUnosSvegaOsimZaliha
        public bool dozvoljenUnosSvegaOsimZaliha;
        public bool DozvoljenUnosSvegaOsimZaliha
        {
            get
            {
                return dozvoljenUnosSvegaOsimZaliha;
            }
            set
            {
                dozvoljenUnosSvegaOsimZaliha = value;
                OnPropertyChanged();
            }
        }
        #endregion
        public static ObservableCollection<SastojakDTO> Sastojci
        {
            get;
            set;
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

        #region Konstruktor i inicijalizacija
        public ViewModelCreateFormLekovi(int idLeka)
        {
            if (FormUpravnik.clickedDodaj)
            {
                vidljivaLabelaZalihe = Visibility.Hidden;
                vidljivTxtZalihe = Visibility.Hidden;
            }
            Inicijalizacija(idLeka);
            PostaviKomande();
        }

        private void Inicijalizacija(int idLeka)
        {
            Inject = new Injector();
            Lek lek = Inject.ControllerLek.DobaviLek(idLeka);
            lekZaPrikaz = new LekDTO(lek);
            InicijalizujPoljaZaUnos();
            InicijalizujSastojke();
        }

        private void InicijalizujPoljaZaUnos()
        {
            dozvoljenUnosSvegaOsimZaliha = true;
            dozvoljenUnosZaliha = true;
            if (LekZaPrikaz.Status != StatusLeka.odobren)
                dozvoljenUnosZaliha = false;
            else
            {
                if(!FormUpravnik.clickedDodaj)
                    dozvoljenUnosSvegaOsimZaliha = false;
            }    
        }

        private void InicijalizujSastojke()
        {
            Sastojci = new ObservableCollection<SastojakDTO>();
            foreach (SastojakDTO s in Inject.ControllerSastojak.DobaviSastojkeLeka(lekZaPrikaz.Id))
                Sastojci.Add(s);
        }

        private void PostaviKomande()
        {
            ValidacijaLekaKomanda = new RelayCommand(Executed_ValidacijaLekaKomanda, CanExecute_ValidacijaLekaKomanda);
            DodajKomanda = new RelayCommand(Executed_DodajKomanda, CanExecute_DodajKomanda);
            UkloniKomanda = new RelayCommand(Executed_UkloniKomanda, CanExecute_UkloniKomanda);
            OdustaniKomanda = new RelayCommand(Executed_OdustaniKomanda, CanExecute_OdustaniKomanda);
        }
        #endregion

        #region Komande

        #region ValidacijaLekaKomanda

        private RelayCommand validacijaLekaKomanda;
        public RelayCommand ValidacijaLekaKomanda
        {
            get { return validacijaLekaKomanda; }
            set
            {
                validacijaLekaKomanda = value;
            }
        }

        public void Executed_ValidacijaLekaKomanda(object obj)
        {
            if (Inject.ControllerLek.LekIspravan(lekZaPrikaz.Id))
            {
                if (!FormUpravnik.clickedDodaj)
                    UkloniLekIzSistema();
                    
                if (lekZaPrikaz.Status != StatusLeka.odobren)
                {
                    lekZaPrikaz.Status = StatusLeka.cekaValidaciju;
                    PosaljiObavestenje();
                }

                DodajLekUSistem();
                ZatvoriAkcija();
            }
            else
            {
                MessageBox.Show("Lek koji ima id: " + lekZaPrikaz.Id + " već postoji!");
            }
        }

        private void UkloniLekIzSistema()
        {
            Inject.ControllerLek.ObrisiLek(lekZaPrikaz.Id);
            UkloniIzPrikaza(lekZaPrikaz);
        }

        private void PosaljiObavestenje()
        {
            if (lekZaPrikaz.Status != StatusLeka.odobren)
            {
                Inject.ControllerObavestenje.PosaljiObavestenjeZaLek(lekZaPrikaz);
            }
        }

        private void DodajLekUSistem()
        {
            List<SastojakDTO> sastojci = new List<SastojakDTO>();
            foreach (SastojakDTO s in Sastojci)
            {
                sastojci.Add(s);
            }
            Inject.ControllerLek.SacuvajLek(Inject.ControllerLek.NapraviLek(lekZaPrikaz, sastojci));
            DodajUPrikaz(lekZaPrikaz);
        }

        private void DodajUPrikaz(LekDTO lekZaPrikaz)
        {
            Lek lek = Inject.ControllerLek.DobaviLek(lekZaPrikaz.Id); //ispraviti kad dodje dto na glavni prozor
            FormUpravnik.Lekovi.Add(lek);
        }

        private void UkloniIzPrikaza(LekDTO lek)
        {
            foreach (Lek l in FormUpravnik.Lekovi)
            {
                if (l.Id == lek.Id)
                {
                    FormUpravnik.Lekovi.Remove(l);  //ispraviti kad dodje dto na glavni prozor
                    break;
                }
            }
        }

        public bool CanExecute_ValidacijaLekaKomanda(object obj)
        {
            return lekZaPrikaz.Id != 0 && lekZaPrikaz.Naziv != null && lekZaPrikaz.Proizvodjac != null && lekZaPrikaz.KolicinaUMg != 0;
        }
        #endregion

        #region OdustaniKomanda

        public RelayCommand odustaniKomanda;
        public RelayCommand OdustaniKomanda
        {
            get { return odustaniKomanda; }
            set
            {
                odustaniKomanda = value;
            }
        }

        public void Executed_OdustaniKomanda(object obj)
        {
            ZatvoriAkcija();
        }

        public bool CanExecute_OdustaniKomanda(object obj)
        {
            return true;
        }
        #endregion

        #region DodajKomanda
        public RelayCommand dodajKomanda;
        public RelayCommand DodajKomanda
        {
            get { return dodajKomanda; }
            set
            {
                dodajKomanda = value;
            }
        }

        public void Executed_DodajKomanda(object obj)
        {
            ViewModelFormSastojci vmSastojci = new ViewModelFormSastojci(Sastojci);
            FormSastojci formSastojci = new FormSastojci(vmSastojci);
        }

        public bool CanExecute_DodajKomanda(object obj)
        {
            return true;
        }
        #endregion

        #region UkloniKomanda
        public RelayCommand ukloniKomanda;
        public RelayCommand UkloniKomanda
        {
            get { return ukloniKomanda; }
            set
            {
                ukloniKomanda = value;
            }
        }

        public void Executed_UkloniKomanda(object obj)
        {
            Sastojci.Remove(SastojakZaUklanjanje);
        }

        public bool CanExecute_UkloniKomanda(object obj)
        {
            return SastojakZaUklanjanje != null;
        }
        #endregion

        #endregion
    }
}
