using Bolnica.Commands;
using Bolnica.Model.Pregledi;
using Model.Korisnici;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Bolnica.ViewModel
{
    public class IstorijaPregledaPacijentViewModel : ViewModel
    {
        private Pacijent trenutniPacijent = new Pacijent();
        public static ObservableCollection<PrikazPregleda> PrikazZavrsenihPregleda
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

        private PrikazPregleda selektovaniRed;
        public PrikazPregleda SelektovaniRed
        {
            get { return selektovaniRed; }
            set
            {
                selektovaniRed = value;
                OnPropertyChanged();
            }
        }

        #region Anamneza
        private RelayCommand anamnezaKomanda;
        public RelayCommand AnamnezaKomanda
        {
            get { return anamnezaKomanda; }
            set
            {
                anamnezaKomanda = value;
            }
        }
        public void Executed_AnamnezaKomanda(object obj)
        {
            inject.IstorijaPregledaController.Anamneza(SelektovaniRed);
        }

        public bool CanExecute_AnamnezaKomanda(object obj)
        {
            return true;
        }
        #endregion

        #region OceniLekara
        private RelayCommand oceniLekaraKomanda;
        public RelayCommand OceniLekaraKomanda
        {
            get { return oceniLekaraKomanda; }
            set
            {
                oceniLekaraKomanda = value;
            }
        }
        public void Executed_OceniLekaraKomanda(object obj)
        {
            inject.IstorijaPregledaController.Oceni_Lekara(SelektovaniRed);
        }

        public bool CanExecute_OceniLekaraKomanda(object obj)
        {
            return true;
        }
        #endregion

        #region OceniBolnicu
        private RelayCommand oceniBolnicuKomanda;
        public RelayCommand OceniBolnicuKomanda
        {
            get { return oceniBolnicuKomanda; }
            set
            {
                oceniBolnicuKomanda = value;
            }
        }
        public void Executed_OceniBolnicuKomanda(object obj)
        {
            inject.IstorijaPregledaController.Oceni_Bolnicu(trenutniPacijent);
        }

        public bool CanExecute_OceniBolnicuKomanda(object obj)
        {
            return true;
        }
        #endregion

        #region IstorijaOcena
        private RelayCommand istorijaOcenaKomanda;
        public RelayCommand IstorijaOcenaKomanda
        {
            get { return istorijaOcenaKomanda; }
            set
            {
                istorijaOcenaKomanda = value;
            }
        }
        public void Executed_IstorijaOcenaKomanda(object obj)
        {
            inject.IstorijaPregledaController.Istorija_Ocena_I_Komentara(trenutniPacijent);
        }

        public bool CanExecute_IstorijaOcenaKomanda(object obj)
        {
            return true;
        }
        #endregion

        public IstorijaPregledaPacijentViewModel(Pacijent pacijent)
        {
            Inject = new Injector();
            trenutniPacijent = pacijent;

            PrikazZavrsenihPregleda = new ObservableCollection<PrikazPregleda>();

            List<Lekar> lekari = inject.RepositoryController.DobijLekare();
            List<Prostorija> prostorije = inject.RepositoryController.DobijProstorije();
            List<Pregled> pregledi = inject.RepositoryController.DobijPreglede();
            foreach (Pregled p in pregledi)
            {
                if (p.Pacijent.Jmbg.Equals(pacijent.Jmbg))
                {
                    if (!pacijent.Obrisan && !pacijent.Guest && p.Zavrsen)
                    {
                        PrikazPregleda prikaz = new PrikazPregleda
                        {
                            Datum = p.Datum,
                            Trajanje = p.Trajanje,
                            Zavrsen = p.Zavrsen,
                            Hitan = p.Hitan,
                            Id = p.Id,
                            Anamneza = p.Anamneza,
                            Pacijent = pacijent
                        };

                        foreach (Lekar l in lekari)
                        {
                            if (p.Lekar.Jmbg.Equals(l.Jmbg))
                            {
                                prikaz.Lekar = l;
                            }
                        }

                        foreach (Prostorija pro in prostorije)
                        {
                            if (p.Prostorija.BrojProstorije.Equals(pro.BrojProstorije))
                            {
                                prikaz.Prostorija = pro;
                            }
                        }

                        PrikazZavrsenihPregleda.Add(prikaz);
                    }
                }
            }
            List<Operacija> operacije = inject.RepositoryController.DobijOperacije();
            foreach (Operacija o in operacije)
            {
                if (o.Pacijent.Jmbg.Equals(pacijent.Jmbg))
                {
                    if (!pacijent.Obrisan && !pacijent.Guest && o.Zavrsen)
                    {
                        PrikazOperacije prikaz = new PrikazOperacije
                        {
                            Datum = o.Datum,
                            Trajanje = o.Trajanje,
                            Zavrsen = o.Zavrsen,
                            Hitan = o.Hitan,
                            Id = o.Id,
                            Anamneza = o.Anamneza,
                            TipOperacije = o.TipOperacije,
                            Pacijent = pacijent
                        };

                        foreach (Lekar l in lekari)
                        {
                            if (o.Lekar.Jmbg.Equals(l.Jmbg))
                            {
                                prikaz.Lekar = l;
                            }
                        }

                        foreach (Prostorija p in prostorije)
                        {
                            if (o.Prostorija.BrojProstorije.Equals(p.BrojProstorije))
                            {
                                prikaz.Prostorija = p;
                            }
                        }

                        PrikazZavrsenihPregleda.Add(prikaz);
                    }
                }
            }

            PostaviKomande();
        }

        private void PostaviKomande()
        {
            AnamnezaKomanda = new RelayCommand(Executed_AnamnezaKomanda, CanExecute_AnamnezaKomanda);
            OceniLekaraKomanda = new RelayCommand(Executed_OceniLekaraKomanda, CanExecute_OceniLekaraKomanda);
            OceniBolnicuKomanda = new RelayCommand(Executed_OceniBolnicuKomanda, CanExecute_OceniBolnicuKomanda);
            IstorijaOcenaKomanda = new RelayCommand(Executed_IstorijaOcenaKomanda, CanExecute_IstorijaOcenaKomanda);
        }
    }
}
