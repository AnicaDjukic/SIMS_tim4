using Bolnica.Commands;
using Bolnica.DTO;
using Bolnica.Forms;
using Bolnica.Model.Pregledi;
using Model.Korisnici;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Bolnica.ViewModel
{
   public class ReceptLekarViewModel : ViewModel
    {
        #region POLJA
        private List<string> itemSourceNazivLeka;
        public List<string> ItemSourceNazivLeka
        {
            get { return itemSourceNazivLeka; }
            set
            {
                itemSourceNazivLeka = value;
                OnPropertyChanged();
            }
        }
        private List<int> itemSourceDozaLeka;
        public List<int> ItemSourceDozaLeka
        {
            get { return itemSourceDozaLeka; }
            set
            {
                itemSourceDozaLeka = value;
                OnPropertyChanged();
            }
        }
        private List<int> itemSourceBrojKutijaLeka;
        public List<int> ItemSourceBrojKutijaLeka
        {
            get { return itemSourceBrojKutijaLeka; }
            set
            {
                itemSourceBrojKutijaLeka = value;
                OnPropertyChanged();
            }
        }
        private List<int> itemSourceVremeUzimanjaLeka;
        public List<int> ItemSourceVremeUzimanjaLeka
        {
            get { return itemSourceVremeUzimanjaLeka; }
            set
            {
                itemSourceVremeUzimanjaLeka = value;
                OnPropertyChanged();
            }
        }

        private List<string> itemSourceProizvodjacLeka;
        public List<string> ItemSourceProizvodjacLeka
        {
            get { return itemSourceProizvodjacLeka; }
            set
            {
                itemSourceProizvodjacLeka = value;
                OnPropertyChanged();
            }
        }

        private bool itemSourceProizvodjacComboOpen;
        public bool ItemSourceProizvodjacComboOpen
        {
            get { return itemSourceProizvodjacComboOpen; }
            set
            {
                itemSourceProizvodjacComboOpen = value;
                OnPropertyChanged();
            }
        }
        private bool itemSourceDozaComboOpen;
        public bool ItemSourceDozaComboOpen
        {
            get { return itemSourceDozaComboOpen; }
            set
            {
                itemSourceDozaComboOpen = value;
                OnPropertyChanged();
            }
        }
        private bool itemSourceBrojKutijaComboOpen;
        public bool ItemSourceBrojKutijaComboOpen
        {
            get { return itemSourceBrojKutijaComboOpen; }
            set
            {
                itemSourceBrojKutijaComboOpen = value;
                OnPropertyChanged();
            }
        }
        private bool itemSourceVremeUzimanjaComboOpen;
        public bool ItemSourceVremeUzimanjaComboOpen
        {
            get { return itemSourceVremeUzimanjaComboOpen; }
            set
            {
                itemSourceVremeUzimanjaComboOpen = value;
                OnPropertyChanged();
            }
        }

        private bool itemSourceNazivLekaComboOpen;
        public bool ItemSourceNazivLekaComboOpen
        {
            get { return itemSourceNazivLekaComboOpen; }
            set
            {
                itemSourceNazivLekaComboOpen = value;
                OnPropertyChanged();
            }
        }
        private DateTime datumIzadavanjaRecepta;

        private string nazivLeka;
        private string dozaLeka;
        private string brojKutijaLeka;
        private string vremeUzimanjaLeka;
        private string proizvodjacLeka;
        private DateTime datumPrekidaRecepta;
        public DateTime DatumIzdavanjaRecepta {
            get { return datumIzadavanjaRecepta; }
            set {
                datumIzadavanjaRecepta = value;
                OnPropertyChanged();
            } }
        public string NazivLeka
        {
            get { return nazivLeka; }
            set
            {
                nazivLeka = value;
                OnPropertyChanged();
            }
        }
        public string DozaLeka
        {
            get { return dozaLeka; }
            set
            {
                dozaLeka = value;
                OnPropertyChanged();
            }
        }
        public string BrojKutijaLeka
        {
            get { return brojKutijaLeka; }
            set
            {
                brojKutijaLeka = value;
                OnPropertyChanged();
            }
        }
        public string VremeUzimanjaLeka
        {
            get { return vremeUzimanjaLeka; }
            set
            {
                vremeUzimanjaLeka = value;
                OnPropertyChanged();
            }
        }
        public string ProizvodjacLeka
        {
            get { return proizvodjacLeka; }
            set
            {
                proizvodjacLeka = value;
                OnPropertyChanged();
            }
        }
        public DateTime DatumPrekidaRecepta
        {
            get { return datumPrekidaRecepta; }
            set
            {
                datumPrekidaRecepta = value;
                OnPropertyChanged();
            }
        }

        private Pacijent trenutniPacijent = new Pacijent();

        private List<Lek> sviLekovi;

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

        #endregion
        #region KOMANDE
        private RelayCommand demoOtkaziKomanda;
        public RelayCommand DemoOtkaziKomanda
        {
            get { return demoOtkaziKomanda; }
            set
            {
                demoOtkaziKomanda = value;

            }
        }

        public void Executed_DemoOtkaziKomanda(object obj)
        {
            LekarViewModel.prekidaj = true;
        }

        public bool CanExecute_DemoOtkaziKomanda(object obj)
        {
            return true;
        }

        private RelayCommand proizvodjacComboOtvoriKomanda;
        public RelayCommand ProizvodjacComboOtvoriKomanda
        {
            get { return proizvodjacComboOtvoriKomanda; }
            set
            {
                proizvodjacComboOtvoriKomanda = value;

            }
        }

        public void Executed_ProizvodjacComboOtvoriKomanda(object obj)
        {
            ItemSourceProizvodjacComboOpen = true;

        }

        public bool CanExecute_ProizvodjacComboOtvoriKomanda(object obj)
        {
            return true;
        }
        private RelayCommand proizvodjacComboTabKomanda;
        public RelayCommand ProizvodjacComboTabKomanda
        {
            get { return proizvodjacComboTabKomanda; }
            set
            {
                proizvodjacComboTabKomanda = value;

            }
        }

        public void Executed_ProizvodjacComboTabKomanda(object obj)
        {

            ItemSourceNazivLeka = inject.ReceptLekarController.OtvoriIFiltirajNaTabProizvodjac(new ReceptLekarDTO(ProizvodjacLeka, sviLekovi));
            ItemSourceProizvodjacComboOpen = false;
            TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
            (Keyboard.FocusedElement as FrameworkElement).MoveFocus(request);

        }
        public bool CanExecute_ProizvodjacComboTabKomanda(object obj)
        {
            return true;
        }

        private RelayCommand lekComboOtvoriKomanda;
        public RelayCommand LekComboOtvoriKomanda
        {
            get { return lekComboOtvoriKomanda; }
            set
            {
                lekComboOtvoriKomanda = value;

            }
        }
        public void Executed_LekComboOtvoriKomanda(object obj)
        {
            ItemSourceNazivLekaComboOpen = true;

        }

        public bool CanExecute_LekComboOtvoriKomanda(object obj)
        {
            return true;
        }


        private RelayCommand lekComboTabKomanda;
        public RelayCommand LekComboTabKomanda
        {
            get { return lekComboTabKomanda; }
            set
            {
                lekComboTabKomanda = value;

            }
        }

        public void Executed_LekComboTabKomanda(object obj)
        {

            ItemSourceDozaLeka = inject.ReceptLekarController.OtvoriIFiltirajNaTabLek(new ReceptLekarDTO(NazivLeka, sviLekovi,ProizvodjacLeka));
            ItemSourceNazivLekaComboOpen = false;
            TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
            (Keyboard.FocusedElement as FrameworkElement).MoveFocus(request);

        }
        public bool CanExecute_LekComboTabKomanda(object obj)
        {
            return true;
        }

       

        private RelayCommand dozaComboOtvoriKomanda;
        public RelayCommand DozaComboOtvoriKomanda
        {
            get { return dozaComboOtvoriKomanda; }
            set
            {
                dozaComboOtvoriKomanda = value;

            }
        }

        public void Executed_DozaComboOtvoriKomanda(object obj)
        {
            ItemSourceDozaComboOpen = true;

        }

        public bool CanExecute_DozaComboOtvoriKomanda(object obj)
        {
            return true;
        }
        

        private RelayCommand vremeUzimanjaComboOtvoriKomanda;
        public RelayCommand VremeUzimanjaComboOtvoriKomanda
        {
            get { return vremeUzimanjaComboOtvoriKomanda; }
            set
            {
                vremeUzimanjaComboOtvoriKomanda = value;

            }
        }

        public void Executed_VremeUzimanjaComboOtvoriKomanda(object obj)
        {
            ItemSourceVremeUzimanjaComboOpen = true;

        }

        public bool CanExecute_VremeUzimanjaComboOtvoriKomanda(object obj)
        {
            return true;
        }

        private RelayCommand brojKutijaComboOtvoriKomanda;
        public RelayCommand BrojKutijaComboOtvoriKomanda
        {
            get { return brojKutijaComboOtvoriKomanda; }
            set
            {
                brojKutijaComboOtvoriKomanda = value;

            }
        }

        public void Executed_BrojKutijaComboOtvoriKomanda(object obj)
        {
            ItemSourceBrojKutijaComboOpen = true;

        }

        public bool CanExecute_BrojKutijaComboOtvoriKomanda(object obj)
        {
            return true;
        }

        private RelayCommand dodajReceptKomanda;
        public RelayCommand DodajReceptKomanda
        {
            get { return dodajReceptKomanda; }
            set
            {
                dodajReceptKomanda = value;

            }
        }

        public void Executed_DodajReceptKomanda(object obj)
        {
            if (!inject.ReceptLekarController.PacijentAlergicanNaLek(new ReceptLekarDTO(trenutniPacijent,ProizvodjacLeka,NazivLeka,DozaLeka,sviLekovi)))
            {
                inject.ReceptLekarController.Potvrdi(new ReceptLekarDTO(NazivLeka,DozaLeka,sviLekovi,DatumIzdavanjaRecepta.ToString(),BrojKutijaLeka,VremeUzimanjaLeka,DatumPrekidaRecepta.ToString()));
                ZatvoriAkcija();
            }

        }

        public bool CanExecute_DodajReceptKomanda(object obj)
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
        public ReceptLekarViewModel(Pacijent trenutniPacijent)
        {
            InicijalizujPolja();
            FiltrirajLekove();
            this.trenutniPacijent = trenutniPacijent;
            PopuniComboBoxoveIDatePickere();
            NapraviKomande();
            
        }

        public ReceptLekarViewModel(Pacijent trenutniPacijent, Recept postojeci)
        {

            InicijalizujPolja();
            FiltrirajLekove();
            this.trenutniPacijent = trenutniPacijent;
            PopuniComboBoxoveIDatePickere();
            PrikaziInformacije(postojeci);
            NapraviKomande();
            
        }
        #region POMOCNE FUNKCIJE
        public void InicijalizujPolja()
        {
            ItemSourceProizvodjacLeka = new List<string>();
            ItemSourceVremeUzimanjaLeka = new List<int>();
            ItemSourceBrojKutijaLeka = new List<int>();
            ItemSourceDozaLeka = new List<int>();
            ItemSourceNazivLeka = new List<string>();
            Inject = new Injector();
        }

        public void PrikaziInformacije(Recept postojeci)
        {
            DatumIzdavanjaRecepta = postojeci.DatumIzdavanja;

            for (int i = 0; i < sviLekovi.Count; i++)
            {
                if (sviLekovi[i].Id.Equals(postojeci.Lek.Id))
                {
                    NazivLeka = sviLekovi[i].Naziv;
                    DozaLeka = sviLekovi[i].KolicinaUMg.ToString();
                    ProizvodjacLeka = sviLekovi[i].Proizvodjac;
                }
            }
            BrojKutijaLeka = postojeci.Kolicina.ToString();
            VremeUzimanjaLeka = postojeci.VremeUzimanja.ToString();
            DatumPrekidaRecepta = postojeci.Trajanje;
        }

        public void NapraviKomande()
        {
            DodajReceptKomanda = new RelayCommand(Executed_DodajReceptKomanda, CanExecute_DodajReceptKomanda);
            LekComboOtvoriKomanda = new RelayCommand(Executed_LekComboOtvoriKomanda, CanExecute_LekComboOtvoriKomanda);
            LekComboTabKomanda = new RelayCommand(Executed_LekComboTabKomanda, CanExecute_LekComboTabKomanda);
            ProizvodjacComboOtvoriKomanda = new RelayCommand(Executed_ProizvodjacComboOtvoriKomanda, CanExecute_ProizvodjacComboOtvoriKomanda);
            ProizvodjacComboTabKomanda = new RelayCommand(Executed_ProizvodjacComboTabKomanda, CanExecute_ProizvodjacComboTabKomanda);
            DozaComboOtvoriKomanda = new RelayCommand(Executed_DozaComboOtvoriKomanda, CanExecute_DozaComboOtvoriKomanda);
            VremeUzimanjaComboOtvoriKomanda = new RelayCommand(Executed_VremeUzimanjaComboOtvoriKomanda, CanExecute_VremeUzimanjaComboOtvoriKomanda);
            BrojKutijaComboOtvoriKomanda = new RelayCommand(Executed_BrojKutijaComboOtvoriKomanda, CanExecute_BrojKutijaComboOtvoriKomanda);
            ZatvoriKomanda = new RelayCommand(Executed_ZatvoriKomanda, CanExecute_ZatvoriKomanda);
            DemoOtkaziKomanda = new RelayCommand(Executed_DemoOtkaziKomanda, CanExecute_DemoOtkaziKomanda);
        }
        public void FiltrirajLekove()
        {
            sviLekovi = inject.ReceptLekarController.DobijLekove();
            for (int i = 0; i < sviLekovi.Count; i++)
            {
                if (sviLekovi[i].Status.Equals(StatusLeka.odbijen) || sviLekovi[i].Obrisan)
                {
                    sviLekovi.RemoveAt(i);
                    i--;
                }
            }
        }
        public void PopuniComboBoxoveIDatePickere()
        {
            PopuniComboBoxLek();
            PopuniComboBoxDoza();
            PopuniComboBoxProizvodjac();
            PopuniComboBoxVremeUzimanja();
            PopuniComboBoxBrojKutija();
            DatumIzdavanjaRecepta = DateTime.Now;
            DatumPrekidaRecepta = DateTime.Now;
        }
       
        public void PopuniComboBoxLek()
        {
            List<string> naziviLekova = new List<string>();
            for (int i = 0; i < sviLekovi.Count; i++)
            {
                if (!LekVecDodat(i))
                {
                    if (!naziviLekova.Contains(sviLekovi[i].Naziv))
                    {
                        naziviLekova.Add(sviLekovi[i].Naziv);
                    }
                }
            }
            ItemSourceNazivLeka = naziviLekova;
        }
        public void PopuniComboBoxDoza()
        {
            List<int> dozeLekova = new List<int>();
            for (int i = 0; i < sviLekovi.Count; i++)
            {
                if (!LekVecDodat(i) )
                {
                    if (!dozeLekova.Contains(sviLekovi[i].KolicinaUMg))
                    {
                        dozeLekova.Add(sviLekovi[i].KolicinaUMg);
                    }
                }
            }
            ItemSourceDozaLeka = dozeLekova;
        }

        public void PopuniComboBoxProizvodjac()
        {
            List<string> proizvodjaciLekova = new List<string>();
            for (int i = 0; i < sviLekovi.Count; i++)
            {
                if (!LekVecDodat(i) )
                {
                    if (!proizvodjaciLekova.Contains(sviLekovi[i].Proizvodjac))
                    {
                        proizvodjaciLekova.Add(sviLekovi[i].Proizvodjac);
                    }
                }
            }
            ItemSourceProizvodjacLeka = proizvodjaciLekova;
        }


        public void PopuniComboBoxBrojKutija()
        {
            List<int> brojKutijaLekova = new List<int>();
            for (int i = 1; i < 10; i++)
            {
                brojKutijaLekova.Add(i);
            }
            ItemSourceBrojKutijaLeka = brojKutijaLekova;
        }

        public void PopuniComboBoxVremeUzimanja()
        {
            List<int> vremeUzimanjaLekova = new List<int>();
            for (int vre = 1; vre <= 48; vre++)
            {
               
                    vremeUzimanjaLekova.Add(vre);


            }
            ItemSourceVremeUzimanjaLeka = vremeUzimanjaLekova;
        }

        public bool LekVecDodat(int i)
        {
            bool lekVecDodat = false;
            for (int p = 0; p < AnamnezaLekarViewModel.Recepti.Count; p++)
            {
                if (AnamnezaLekarViewModel.Recepti[p].lek.Id.Equals(sviLekovi[i].Id))
                {
                    lekVecDodat = true;
                }
            }
            return lekVecDodat;
        }


        #endregion

    }
}
