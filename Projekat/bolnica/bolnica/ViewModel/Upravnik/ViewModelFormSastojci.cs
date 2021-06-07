using Bolnica.Commands;
using Bolnica.DTO;
using Bolnica.Forms.Upravnik;
using Bolnica.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Bolnica.ViewModel.Upravnik
{
    public class ViewModelFormSastojci : ViewModel
    {
        #region Polja
        public ObservableCollection<SastojakDTO> SastojciZaPrikaz
        {
            get;
            set;
        }

        public ObservableCollection<SastojakDTO> SastojciLeka
        {
            get;
            set;
        }

        public Action ZatvoriAkcija { get; set; }

        public SastojakDTO IzabraniSastojak { get; set; }

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
        public ViewModelFormSastojci(ObservableCollection<SastojakDTO> sastojci)
        {
            SastojciLeka = sastojci;
            Inicijalizacija();
            PostaviKomande();
        }

        
        private void Inicijalizacija()
        {
            Inject = new Injector();
            PrikaziSastojke();
            
        }

        private void PrikaziSastojke()
        {
            SastojciZaPrikaz = new ObservableCollection<SastojakDTO>();
            List<SastojakDTO> sastojciZaIzbaciti = NadjiSastojkeLeka();
            IzbaciSastojkeIzPrikaza(sastojciZaIzbaciti);
        }

        private List<SastojakDTO> NadjiSastojkeLeka()
        {
            List<SastojakDTO> sastojciLeka = new List<SastojakDTO>();
            foreach (SastojakDTO s in Inject.ControllerSastojak.DobaviSveSastojke())
            {
                foreach (SastojakDTO sastojakLeka in SastojciLeka)
                {
                    if (s.Id == sastojakLeka.Id)
                        sastojciLeka.Add(s);
                }
                SastojciZaPrikaz.Add(s);
            }
            return sastojciLeka;
        }

        private void IzbaciSastojkeIzPrikaza(List<SastojakDTO> sastojciZaIzbaciti)
        {
            for (int i = 0; i < SastojciZaPrikaz.Count; i++)
            {
                foreach (SastojakDTO sastojak in sastojciZaIzbaciti)
                {
                    if (SastojciZaPrikaz[i].Id == sastojak.Id)
                        SastojciZaPrikaz.Remove(SastojciZaPrikaz[i]);
                }
            }
        }

        private void PostaviKomande()
        {
            PotvrdiKomanda = new RelayCommand(Executed_PotvrdiKomanda, CanExecute_PotvrdiKomanda);
            OdustaniKomanda = new RelayCommand(Executed_OdustaniKomanda, CanExecute_OdustaniKomanda);
            DodajNoviKomanda = new RelayCommand(Executed_DodajNoviKomanda, CanExecute_DodajNoviKomanda);
            ObrisiKomanda = new RelayCommand(Executed_ObrisiKomanda, CanExecute_ObrisiKomanda);
        }
        #endregion

        #region Komande

        #region PotvrdiKomanda
        public RelayCommand potvrdiKomanda;
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
            SastojciLeka.Add(IzabraniSastojak);
            ZatvoriAkcija();
        }

        public bool CanExecute_PotvrdiKomanda(object obj)
        {
            return IzabraniSastojak != null; ;
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

        #region DodajNoviKomanda

        public RelayCommand dodajNoviKomanda;
        public RelayCommand DodajNoviKomanda
        {
            get { return dodajNoviKomanda; }
            set
            {
                dodajNoviKomanda = value;
            }
        }

        public void Executed_DodajNoviKomanda(object obj)
        {
            ViewModelCreateFormSastojak vm = new ViewModelCreateFormSastojak(SastojciZaPrikaz);
            CreateFormSastojak createFormSastojak = new CreateFormSastojak(vm);
        }

        public bool CanExecute_DodajNoviKomanda(object obj)
        {
            return true;
        }
        #endregion

        #region ObrisiKomanda

        public RelayCommand obrisiKomanda;
        public RelayCommand ObrisiKomanda
        {
            get { return obrisiKomanda; }
            set
            {
                obrisiKomanda = value;
            }
        }

        public void Executed_ObrisiKomanda(object obj)
        {
            if (UpitZaBrisanjeLeka() == MessageBoxResult.Yes)
            {
                Inject.ControllerSastojak.ObrisiSastojak(IzabraniSastojak.Id);
                SastojciZaPrikaz.Remove(IzabraniSastojak);
            }
        }

        private MessageBoxResult UpitZaBrisanjeLeka()
        {
            string sMessageBoxText = LocalizedStrings.Instance["Da li ste sigurni da želite da obrišete sastojak"] + " \"" + IzabraniSastojak.Naziv + "?";
            string sCaption = LocalizedStrings.Instance["Brisanje sastojka"];

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            return rsltMessageBox;
        }

        public bool CanExecute_ObrisiKomanda(object obj)
        {
            return IzabraniSastojak != null;
        }
        #endregion

        #endregion
    }
}
