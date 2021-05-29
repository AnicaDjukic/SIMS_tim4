using Bolnica.Commands;
using Bolnica.DTO;
using Bolnica.Model.Pregledi;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.ViewModel
{
    public class KomentarLekaLekarViewModel : ViewModel
    {
        #region POLJA
        private string komentar;

        public string Komentar
        {
            get { return komentar; }
            set
            {
                komentar = value;
                OnPropertyChanged();
            }
        }

        private PrikazLek prikazLeka = new PrikazLek();

        private List<Lek> listaLekova = new List<Lek>();

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
            inject.KomentarLekaLekarController.Potvrdi(new KomentarLekaLekarDTO(Komentar,prikazLeka,listaLekova));
            ZatvoriAkcija();
        }

        public bool CanExecute_PotvrdiKomanda(object obj)
        {
            return true;
        }
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
        #endregion
        public KomentarLekaLekarViewModel(PrikazLek izabraniLek)
        {
            InicirajPolja(izabraniLek);
            NapraviKomande();
        }
        #region POMOCNE FUNKCIJE
        public void InicirajPolja(PrikazLek izabraniLek)
        {
            Komentar = "";
            Inject = new Injector();
            listaLekova = inject.KomentarLekaLekarController.DobijLek();
            for (int i = 0; i < listaLekova.Count; i++)
            {
                if (listaLekova[i].Status.Equals(StatusLeka.odbijen) || listaLekova[i].Obrisan)
                {
                    listaLekova.RemoveAt(i);
                    i--;
                }
            }
            prikazLeka = izabraniLek;
        }
        public void NapraviKomande()
        {
            ZatvoriKomanda = new RelayCommand(Executed_ZatvoriKomanda, CanExecute_ZatvoriKomanda);
            PotvrdiKomanda = new RelayCommand(Executed_PotvrdiKomanda, CanExecute_PotvrdiKomanda);
        }
        #endregion
    }
}
