using Bolnica.Commands;
using Bolnica.DTO;
using Bolnica.Localization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace Bolnica.ViewModel.Upravnik
{
    public class ViewModelCreateFormSastojak : ViewModel
    {
        private ObservableCollection<SastojakDTO> SviSastojci
        {
            get;
            set;
        }
        public string Naziv { get; set; }
        public Action ZatvoriAkcija { get; set; }

        private Injector inject;
        public Injector Inject
        {
            get { return inject; }
            set
            {
                inject = value;
            }
        }

        public ViewModelCreateFormSastojak(ObservableCollection<SastojakDTO> sastojciZaPrikaz)
        {
            inject = new Injector();
            SviSastojci = sastojciZaPrikaz;
            PostaviKomande();
        }

        private void PostaviKomande()
        {
            PotvrdiKomanda = new RelayCommand(Executed_PotvrdiKomanda, CanExecute_PotvrdiKomanda);
            OdustaniKomanda = new RelayCommand(Executed_OdustaniKomanda, CanExecute_OdustaniKomanda);
        }
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
            if (!Inject.ControllerSastojak.SastojakPostoji(Naziv))
            {
                DodajNoviSastojak();
                ZatvoriAkcija();
            }
            else
            {
                MessageBox.Show(LocalizedStrings.Instance["Sastojak već postoji!"]);
            }
        }

        private void DodajNoviSastojak()
        {
            SastojakDTO noviSastojak = new SastojakDTO(Inject.ControllerSastojak.DobaviNoviId(), Naziv);
            Inject.ControllerSastojak.SacuvajSastojak(noviSastojak);
            SviSastojci.Add(noviSastojak);
        }

        public bool CanExecute_PotvrdiKomanda(object obj)
        {
            return Naziv != null;
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
        #endregion
    }
}
