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

        private FileStorageLek sviLekovi = new FileStorageLek();

        private Injector inject;
        public Injector Inject
        {
            get { return inject; }
            set
            {
                inject = value;
            }
        }
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
            inject.KomentarLekaService.Potvrdi(new KomentarLekaLekarServiceDTO(Komentar,prikazLeka,listaLekova));
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

        public KomentarLekaLekarViewModel(PrikazLek izabraniLek)
        {
            InicirajPolja(izabraniLek);
            NapraviKomande();
        }
        public void InicirajPolja(PrikazLek izabraniLek)
        {
            Komentar = "";
            Inject = new Injector();
            listaLekova = sviLekovi.GetAll();
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
    }
}
