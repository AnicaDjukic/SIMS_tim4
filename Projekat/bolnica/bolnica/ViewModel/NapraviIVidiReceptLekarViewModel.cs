using Bolnica.Commands;
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

        private FileStorageLek storageLekovi = new FileStorageLek();

        private Injector inject;

        public Injector Inject
        {
            get { return inject; }
            set
            {
                inject = value;
            }
        }

        public Action CloseAction { get; set; }

        private RelayCommand proizvodjacComboOpenCommand;
        public RelayCommand ProizvodjacComboOpenCommand
        {
            get { return proizvodjacComboOpenCommand; }
            set
            {
                proizvodjacComboOpenCommand = value;

            }
        }

        public void Executed_ProizvodjacComboOpenCommand(object obj)
        {
            ItemSourceProizvodjacComboOpen = true;

        }

        public bool CanExecute_ProizvodjacComboOpenCommand(object obj)
        {
            return true;
        }
        private RelayCommand proizvodjacComboOpenTabCommand;
        public RelayCommand ProizvodjacComboOpenTabCommand
        {
            get { return proizvodjacComboOpenTabCommand; }
            set
            {
                proizvodjacComboOpenTabCommand = value;

            }
        }

        public void Executed_ProizvodjacComboOpenTabCommand(object obj)
        {

            ItemSourceNazivLeka = inject.ReceptService.OtvoriIFiltirajNaTabProizvodjac(proizvodjacLeka, sviLekovi);
            ItemSourceProizvodjacComboOpen = false;
            TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
            (Keyboard.FocusedElement as FrameworkElement).MoveFocus(request);

        }
        public bool CanExecute_ProizvodjacComboOpenTabCommand(object obj)
        {
            return true;
        }

        private RelayCommand lekComboOpenCommand;
        public RelayCommand LekComboOpenCommand
        {
            get { return lekComboOpenCommand; }
            set
            {
                lekComboOpenCommand = value;

            }
        }
        public void Executed_LekComboOpenCommand(object obj)
        {
            ItemSourceNazivLekaComboOpen = true;

        }

        public bool CanExecute_LekComboOpenCommand(object obj)
        {
            return true;
        }


        private RelayCommand lekComboOpenTabCommand;
        public RelayCommand LekComboOpenTabCommand
        {
            get { return lekComboOpenTabCommand; }
            set
            {
                lekComboOpenTabCommand = value;

            }
        }

        public void Executed_LekComboOpenTabCommand(object obj)
        {

            ItemSourceDozaLeka = inject.ReceptService.OtvoriIFiltirajNaTabLek(nazivLeka, sviLekovi,proizvodjacLeka);
            ItemSourceNazivLekaComboOpen = false;
            TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
            (Keyboard.FocusedElement as FrameworkElement).MoveFocus(request);

        }
        public bool CanExecute_LekComboOpenTabCommand(object obj)
        {
            return true;
        }

       

        private RelayCommand dozaComboOpenCommand;
        public RelayCommand DozaComboOpenCommand
        {
            get { return dozaComboOpenCommand; }
            set
            {
                dozaComboOpenCommand = value;

            }
        }

        public void Executed_DozaComboOpenCommand(object obj)
        {
            ItemSourceDozaComboOpen = true;

        }

        public bool CanExecute_DozaComboOpenCommand(object obj)
        {
            return true;
        }
        

        private RelayCommand vremeUzimanjaComboOpenCommand;
        public RelayCommand VremeUzimanjaComboOpenCommand
        {
            get { return vremeUzimanjaComboOpenCommand; }
            set
            {
                vremeUzimanjaComboOpenCommand = value;

            }
        }

        public void Executed_VremeUzimanjaComboOpenCommand(object obj)
        {
            ItemSourceVremeUzimanjaComboOpen = true;

        }

        public bool CanExecute_VremeUzimanjaComboOpenCommand(object obj)
        {
            return true;
        }

        private RelayCommand brojKutijaComboOpenCommand;
        public RelayCommand BrojKutijaComboOpenCommand
        {
            get { return brojKutijaComboOpenCommand; }
            set
            {
                brojKutijaComboOpenCommand = value;

            }
        }

        public void Executed_BrojKutijaComboOpenCommand(object obj)
        {
            ItemSourceBrojKutijaComboOpen = true;

        }

        public bool CanExecute_BrojKutijaComboOpenCommand(object obj)
        {
            return true;
        }

        private RelayCommand dodajReceptCommand;
        public RelayCommand DodajReceptCommand
        {
            get { return dodajReceptCommand; }
            set
            {
                dodajReceptCommand = value;

            }
        }

        public void Executed_DodajReceptCommand(object obj)
        {
            if (!inject.ReceptService.PacijentAlergicanNaLek(trenutniPacijent,proizvodjacLeka,nazivLeka,dozaLeka,sviLekovi))
            {
                inject.ReceptService.Potvrdi(nazivLeka,dozaLeka,sviLekovi,datumIzdavanjaRecepta.ToString(),brojKutijaLeka,vremeUzimanjaLeka,datumPrekidaRecepta.ToString());
                CloseAction();
            }

        }

        public bool CanExecute_DodajReceptCommand(object obj)
        {
            return true;
        }

        private RelayCommand zatvoriCommand;
        public RelayCommand ZatvoriCommand
        {
            get { return zatvoriCommand; }
            set
            {
                zatvoriCommand = value;

            }
        }

        public void Executed_ZatvoriCommand(object obj)
        {
            
                CloseAction();
            

        }

        public bool CanExecute_ZatvoriCommand(object obj)
        {
            return true;
        }
        public NapraviIVidiReceptLekarViewModel(Pacijent trenutniPacijent)
        {
            ItemSourceProizvodjacLeka = new List<string>();
            ItemSourceVremeUzimanjaLeka = new List<TimeSpan>();
            ItemSourceBrojKutijaLeka = new List<int>();
            ItemSourceDozaLeka = new List<int>();
            ItemSourceNazivLeka = new List<string>();
            Inject = new Injector();

            FiltrirajLekove();
            this.trenutniPacijent = trenutniPacijent;
            PopuniComboBoxoveIDatePickere();
            DodajReceptCommand = new RelayCommand(Executed_DodajReceptCommand, CanExecute_DodajReceptCommand);
            LekComboOpenCommand = new RelayCommand(Executed_LekComboOpenCommand, CanExecute_LekComboOpenCommand);
            LekComboOpenTabCommand = new RelayCommand(Executed_LekComboOpenTabCommand, CanExecute_LekComboOpenTabCommand);
            ProizvodjacComboOpenCommand = new RelayCommand(Executed_ProizvodjacComboOpenCommand, CanExecute_ProizvodjacComboOpenCommand);
            ProizvodjacComboOpenTabCommand = new RelayCommand(Executed_ProizvodjacComboOpenTabCommand, CanExecute_ProizvodjacComboOpenTabCommand);
            DozaComboOpenCommand = new RelayCommand(Executed_DozaComboOpenCommand, CanExecute_DozaComboOpenCommand);
            VremeUzimanjaComboOpenCommand = new RelayCommand(Executed_VremeUzimanjaComboOpenCommand, CanExecute_VremeUzimanjaComboOpenCommand);
            BrojKutijaComboOpenCommand = new RelayCommand(Executed_BrojKutijaComboOpenCommand, CanExecute_BrojKutijaComboOpenCommand);
            ZatvoriCommand = new RelayCommand(Executed_ZatvoriCommand, CanExecute_ZatvoriCommand);
        }

        public NapraviIVidiReceptLekarViewModel(Pacijent trenutniPacijent, Recept r)
        {
            ItemSourceProizvodjacLeka = new List<string>();
            ItemSourceVremeUzimanjaLeka = new List<TimeSpan>();
            ItemSourceBrojKutijaLeka = new List<int>();
            ItemSourceDozaLeka = new List<int>();
            ItemSourceNazivLeka = new List<string>();
            Inject = new Injector();

            FiltrirajLekove();
            this.trenutniPacijent = trenutniPacijent;
            PopuniComboBoxoveIDatePickere();
            DodajReceptCommand = new RelayCommand(Executed_DodajReceptCommand, CanExecute_DodajReceptCommand);
            LekComboOpenCommand = new RelayCommand(Executed_LekComboOpenCommand, CanExecute_LekComboOpenCommand);
            LekComboOpenTabCommand = new RelayCommand(Executed_LekComboOpenTabCommand, CanExecute_LekComboOpenTabCommand);
            ProizvodjacComboOpenCommand = new RelayCommand(Executed_ProizvodjacComboOpenCommand, CanExecute_ProizvodjacComboOpenCommand);
            ProizvodjacComboOpenTabCommand = new RelayCommand(Executed_ProizvodjacComboOpenTabCommand, CanExecute_ProizvodjacComboOpenTabCommand);
            DozaComboOpenCommand = new RelayCommand(Executed_DozaComboOpenCommand, CanExecute_DozaComboOpenCommand);
            VremeUzimanjaComboOpenCommand = new RelayCommand(Executed_VremeUzimanjaComboOpenCommand, CanExecute_VremeUzimanjaComboOpenCommand);
            BrojKutijaComboOpenCommand = new RelayCommand(Executed_BrojKutijaComboOpenCommand, CanExecute_BrojKutijaComboOpenCommand);
            ZatvoriCommand = new RelayCommand(Executed_ZatvoriCommand, CanExecute_ZatvoriCommand);
            datumIzdavanjaRecepta = r.DatumIzdavanja;

            for (int i = 0; i < sviLekovi.Count; i++)
            {
                if (sviLekovi[i].Id.Equals(r.Lek.Id))
                {
                    nazivLeka = sviLekovi[i].Naziv;
                    dozaLeka = sviLekovi[i].KolicinaUMg.ToString();
                    proizvodjacLeka = sviLekovi[i].Proizvodjac;
                }
            }
            brojKutijaLeka = r.Kolicina.ToString();
            vremeUzimanjaLeka = r.VremeUzimanja.ToString();
            datumPrekidaRecepta = r.Trajanje;
        }

        public void FiltrirajLekove()
        {
            sviLekovi = storageLekovi.GetAll();
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
                if (LekVecDodat(i) == 0)
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
                if (LekVecDodat(i) == 0)
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
                if (LekVecDodat(i) == 0)
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

        public int LekVecDodat(int i)
        {
            int lekVecDodat = 0;
            for (int p = 0; p < FormNapraviAnamnezuLekar.Recepti.Count; p++)
            {
                if (FormNapraviAnamnezuLekar.Recepti[p].lek.Id.Equals(sviLekovi[i].Id))
                {
                    lekVecDodat = 1;
                }
            }
            return lekVecDodat;
        }




    }
}
