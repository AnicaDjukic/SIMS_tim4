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
   public class NapraviIVidiReceptLekarViewModel : ViewModel
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
        private List<TimeSpan> itemSourceVremeUzimanjaLeka;
        public List<TimeSpan> ItemSourceVremeUzimanjaLeka
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
        public DateTime datumIzdavanjaRecepta { get; set; }
        public string nazivLeka { get; set; }
        public string dozaLeka { get; set; }
        public string brojKutijaLeka { get; set; }
        public string vremeUzimanjaLeka { get; set; }
        public string proizvodjacLeka { get; set; }
        public DateTime datumPrekidaRecepta { get; set; }

        private Pacijent trenutniPacijent = new Pacijent();

        private List<Lek> sviLekovi;

        private FileStorageLek skladisteLekova = new FileStorageLek();

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

            ItemSourceNazivLeka = inject.ReceptController.OtvoriIFiltirajNaTabProizvodjac(new NapraviIVidiReceptLekarServiceDTO(proizvodjacLeka, sviLekovi));
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

            ItemSourceDozaLeka = inject.ReceptController.OtvoriIFiltirajNaTabLek(new NapraviIVidiReceptLekarServiceDTO(nazivLeka, sviLekovi,proizvodjacLeka));
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
            if (!inject.ReceptController.PacijentAlergicanNaLek(new NapraviIVidiReceptLekarServiceDTO(trenutniPacijent,proizvodjacLeka,nazivLeka,dozaLeka,sviLekovi)))
            {
                inject.ReceptController.Potvrdi(new NapraviIVidiReceptLekarServiceDTO(nazivLeka,dozaLeka,sviLekovi,datumIzdavanjaRecepta.ToString(),brojKutijaLeka,vremeUzimanjaLeka,datumPrekidaRecepta.ToString()));
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
        public NapraviIVidiReceptLekarViewModel(Pacijent trenutniPacijent)
        {
            InicijalizujPolja();
            FiltrirajLekove();
            this.trenutniPacijent = trenutniPacijent;
            PopuniComboBoxoveIDatePickere();
            NapraviKomande();
            
        }

        public NapraviIVidiReceptLekarViewModel(Pacijent trenutniPacijent, Recept postojeci)
        {

            InicijalizujPolja();
            FiltrirajLekove();
            this.trenutniPacijent = trenutniPacijent;
            PopuniComboBoxoveIDatePickere();
            PrikaziInformacije(postojeci);
            NapraviKomande();
            
        }

        public void InicijalizujPolja()
        {
            ItemSourceProizvodjacLeka = new List<string>();
            ItemSourceVremeUzimanjaLeka = new List<TimeSpan>();
            ItemSourceBrojKutijaLeka = new List<int>();
            ItemSourceDozaLeka = new List<int>();
            ItemSourceNazivLeka = new List<string>();
            Inject = new Injector();
        }

        public void PrikaziInformacije(Recept postojeci)
        {
            datumIzdavanjaRecepta = postojeci.DatumIzdavanja;

            for (int i = 0; i < sviLekovi.Count; i++)
            {
                if (sviLekovi[i].Id.Equals(postojeci.Lek.Id))
                {
                    nazivLeka = sviLekovi[i].Naziv;
                    dozaLeka = sviLekovi[i].KolicinaUMg.ToString();
                    proizvodjacLeka = sviLekovi[i].Proizvodjac;
                }
            }
            brojKutijaLeka = postojeci.Kolicina.ToString();
            vremeUzimanjaLeka = postojeci.VremeUzimanja.ToString();
            datumPrekidaRecepta = postojeci.Trajanje;
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
        }
        public void FiltrirajLekove()
        {
            sviLekovi = skladisteLekova.GetAll();
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
            datumIzdavanjaRecepta = DateTime.Now;
            datumPrekidaRecepta = DateTime.Now;
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
            List<TimeSpan> vremeUzimanjaLekova = new List<TimeSpan>();
            for (int vre = 0; vre < 24; vre++)
            {
                for (int min = 0; min < 59;)
                {
                    TimeSpan ts = new TimeSpan(vre, min, 0);
                    min = min + 15;
                    vremeUzimanjaLekova.Add(ts);
                }

            }
            ItemSourceVremeUzimanjaLeka = vremeUzimanjaLekova;
        }

        public bool LekVecDodat(int i)
        {
            bool lekVecDodat = false;
            for (int p = 0; p < NapraviAnamnezuLekarViewModel.Recepti.Count; p++)
            {
                if (NapraviAnamnezuLekarViewModel.Recepti[p].lek.Id.Equals(sviLekovi[i].Id))
                {
                    lekVecDodat = true;
                }
            }
            return lekVecDodat;
        }




    }
}
