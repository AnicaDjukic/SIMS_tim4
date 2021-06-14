using bolnica;
using Bolnica.Commands;
using Bolnica.DTO;
using Bolnica.Forms;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.Repository.Pregledi;
using Model.Korisnici;
using Model.Pacijenti;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Bolnica.ViewModel
{
    public class LekarViewModel : ViewModel
    {
        #region POLJA
        public static List<Pregled> listaPregleda = new List<Pregled>();
        public static List<Operacija> listaOperacija = new List<Operacija>();
        private List<Pacijent> listaPacijenata = new List<Pacijent>();
        private List<Prostorija> listaProstorija = new List<Prostorija>();
        private List<Lekar> listaLekara = new List<Lekar>();
        private List<Lek> lekovi = new List<Lek>();
        public static DataGrid podaciLista = new DataGrid();
        public static DataGrid podaciListaIstorija = new DataGrid();
        public static ObservableCollection<PrikazLek> lekoviPrikaz = new ObservableCollection<PrikazLek>();
        public static ObservableCollection<Ocena> ocenePrikaz = new ObservableCollection<Ocena>();
        public Lekar lekarTrenutni { get; set; }
        private PrikazPregleda prikazPregleda = new PrikazPregleda();
        private PrikazOperacije prikazOperacije = new PrikazOperacije();
        private DataGrid preglediTabela;
        private DataGrid istorijaPregledaTabela;
        private DataGrid lekoviTabela;
        private DataGrid oceneTabela;
        private Button zakaziDugme;
        private TabItem preglediTab;
        private TabItem istorijaTab;
        private TabItem lekTab;
        private Button anamnezaIstorijaDugme;
        private Button odobriDugme;
        private DispatcherTimer _activeTimer;
        public static bool prekidaj = false;
        public string prosecnaOcena { get; set; }

    

        private Injector inject;
        public Injector Inject
        {
            get { return inject; }
            set
            {
                inject = value;
            }
        }

        private bool demoFokus;
        public bool DemoFokus
        {
            get { return demoFokus; }
            set
            {
                demoFokus = value;
                OnPropertyChanged();
            }
        }
        public Action ZatvoriAkcija { get; set; }

        #endregion
        #region KOMANDE

        private RelayCommand oceniKomanda;
        public RelayCommand OceniKomanda
        {
            get { return oceniKomanda; }
            set
            {
                oceniKomanda = value;

            }
        }

        public void Executed_OceniKomanda(object obj)
        {
            FormOceniLekar form = new FormOceniLekar();
            form.Show();
        }

        public bool CanExecute_OceniKomanda(object obj)
        {
            return true;
        }

        private RelayCommand odjavaKomanda;
        public RelayCommand OdjavaKomanda
        {
            get { return odjavaKomanda; }
            set
            {
                odjavaKomanda = value;

            }
        }

        public void Executed_OdjavaKomanda(object obj)
        {
            if (MessageBox.Show("Da li ste sigurni da želite da se odjavite.", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {

            }
            else
            {
                MainWindow form = new MainWindow();
                form.Show();
                listaPregleda = new List<Pregled>();
                listaOperacija = new List<Operacija>();
                podaciLista = new DataGrid();
                podaciListaIstorija = new DataGrid();
                lekoviPrikaz = new ObservableCollection<PrikazLek>();
                ocenePrikaz = new ObservableCollection<Ocena>();
                prekidaj = false;
                InformacijeOPacijentuLekarViewModel.Hospitalizacije = new ObservableCollection<Hospitalizacija>();
                InformacijeOPacijentuLekarViewModel.Pregledi = new ObservableCollection<PrikazPregleda>();
                AnamnezaLekarViewModel.Recepti = new ObservableCollection<PrikazRecepta>();
        
                ZatvoriAkcija();
            }
        }

        public bool CanExecute_OdjavaKomanda(object obj)
        {
            return true;
        }

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
            prekidaj = true;
        }

        public bool CanExecute_DemoOtkaziKomanda(object obj)
        {
            return true;
        }

        private RelayCommand zakaziPregledKomanda;
        public RelayCommand ZakaziPregledKomanda
        {
            get { return zakaziPregledKomanda; }
            set
            {
                zakaziPregledKomanda = value;

            }
        }

        public void Executed_ZakaziPregledKomanda(object obj)
        {
            inject.LekarController.ZakaziPregled(new LekarServiceDTO(lekarTrenutni));
        }

        public bool CanExecute_ZakaziPregledKomanda(object obj)
        {
            return true;
        }
        private RelayCommand otkaziPregledKomanda;
        public RelayCommand OtkaziPregledKomanda
        {
            get { return otkaziPregledKomanda; }
            set
            {
                otkaziPregledKomanda = value;

            }
        }

        public void Executed_OtkaziPregledKomanda(object obj)
        {
            inject.LekarController.OtkaziPregled(new LekarServiceDTO(preglediTabela, listaOperacija, listaPregleda, lekarTrenutni, prikazPregleda, prikazOperacije));
        }

        public bool CanExecute_OtkaziPregledKomanda(object obj)
        {
            return true;
        }
        private RelayCommand izmeniPregledKomanda;
        public RelayCommand IzmeniPregledKomanda
        {
            get { return izmeniPregledKomanda; }
            set
            {
                izmeniPregledKomanda = value;

            }
        }

        public void Executed_IzmeniPregledKomanda(object obj)
        {
            inject.LekarController.IzmeniPregled(new LekarServiceDTO(preglediTabela, listaOperacija, listaPregleda, lekarTrenutni, prikazPregleda, prikazOperacije));
        }

        public bool CanExecute_IzmeniPregledKomanda(object obj)
        {
            return true;
        }
        private RelayCommand informacijeOPacijentuKomanda;
        public RelayCommand InformacijeOPacijentuKomanda
        {
            get { return informacijeOPacijentuKomanda; }
            set
            {
                informacijeOPacijentuKomanda = value;

            }
        }

        public void Executed_InformacijeOPacijentuKomanda(object obj)
        {
            inject.LekarController.InformacijeOPacijentu(new LekarServiceDTO(preglediTabela, listaOperacija, listaPregleda, lekarTrenutni, prikazPregleda, prikazOperacije));
        }

        public bool CanExecute_InformacijeOPacijentuKomanda(object obj)
        {
            return true;
        }

        private RelayCommand informacijeOPacijentuIstorijaKomanda;
        public RelayCommand InformacijeOPacijentuIstorijaKomanda
        {
            get { return informacijeOPacijentuIstorijaKomanda; }
            set
            {
                informacijeOPacijentuIstorijaKomanda = value;

            }
        }

        public void Executed_InformacijeOPacijentuIstorijaKomanda(object obj)
        {
            inject.LekarController.InformacijeOPacijentu(new LekarServiceDTO(istorijaPregledaTabela, listaOperacija, listaPregleda, lekarTrenutni, prikazPregleda, prikazOperacije));
        }

        public bool CanExecute_InformacijeOPacijentuIstorijaKomanda(object obj)
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
            inject.LekarController.SkociNaEnter(new LekarServiceDTO(zakaziDugme));
        }

        public bool CanExecute_SkociNaEnterKomanda(object obj)
        {
            return true;
        }


        private RelayCommand demoFokusKomanda;
        public RelayCommand DemoFokusKomanda
        {
            get { return demoFokusKomanda; }
            set
            {
                demoFokusKomanda = value;

            }
        }

        public void Executed_DemoFokusKomanda(object obj)
        {
            DemoFokus = true;
            DemoFokus = false;
        }

        public bool CanExecute_DemoFokusKomanda(object obj)
        {
            return true;
        }

        private RelayCommand demoKomanda;
        public RelayCommand DemoKomanda
        {
            get { return demoKomanda; }
            set
            {
                demoKomanda = value;

            }
        }

        public void Executed_DemoKomanda(object obj)
        {
            if (MessageBox.Show("Da li ste sigurni da želite da pokrenete demo mod. Izlazak iz demo moda možete izvrišiti u bilo kome trenutku pritiskom tastera Ctrl+J", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {

            }
            else
            {
                DemoKomandaIzView();
            }
        }

        public bool CanExecute_DemoKomanda(object obj)
        {
            return true;
        }

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
            inject.LekarController.SkociNaLevo(new LekarServiceDTO(preglediTab));
        }

        public bool CanExecute_SkociNaLevoKomanda(object obj)
        {
            return true;
        }
        private RelayCommand skociNaTabKomanda;
        public RelayCommand SkociNaTabKomanda
        {
            get { return skociNaTabKomanda; }
            set
            {
                skociNaTabKomanda = value;

            }
        }

        public void Executed_SkociNaTabKomanda(object obj)
        {
            inject.LekarController.SkociNaTab(new LekarServiceDTO(preglediTabela));
        }

        public bool CanExecute_SkociNaTabKomanda(object obj)
        {
            return true;
        }
        private RelayCommand skociNaEnterIstorijaKomanda;
        public RelayCommand SkociNaEnterIstorijaKomanda
        {
            get { return skociNaEnterIstorijaKomanda; }
            set
            {
                skociNaEnterIstorijaKomanda = value;

            }
        }

        public void Executed_SkociNaEnterIstorijaKomanda(object obj)
        {
            inject.LekarController.SkociNaEnterIstorija(new LekarServiceDTO(anamnezaIstorijaDugme));
        }

        public bool CanExecute_SkociNaEnterIstorijaKomanda(object obj)
        {
            return true;
        }
        private RelayCommand skociNaLevoIstorijaKomanda;
        public RelayCommand SkociNaLevoIstorijaKomanda
        {
            get { return skociNaLevoIstorijaKomanda; }
            set
            {
                skociNaLevoIstorijaKomanda = value;

            }
        }

        public void Executed_SkociNaLevoIstorijaKomanda(object obj)
        {
            inject.LekarController.SkociNaLevoIstorija(new LekarServiceDTO(istorijaTab));
        }

        public bool CanExecute_SkociNaLevoIstorijaKomanda(object obj)
        {
            return true;
        }
        private RelayCommand skociNaTabIstorijaKomanda;
        public RelayCommand SkociNaTabIstorijaKomanda
        {
            get { return skociNaTabIstorijaKomanda; }
            set
            {
                skociNaTabIstorijaKomanda = value;

            }
        }

       

        public void Executed_SkociNaTabIstorijaKomanda(object obj)
        {
            inject.LekarController.SkociNaTabIstorija(new LekarServiceDTO(istorijaPregledaTabela));
        }

        public bool CanExecute_SkociNaTabIstorijaKomanda(object obj)
        {
            return true;
        }

        private RelayCommand istorijaDugmeSkociNaInformacijeKomanda;
        public RelayCommand IstorijaDugmeSkociNaInformacijeKomanda
        {
            get { return istorijaDugmeSkociNaInformacijeKomanda; }
            set { istorijaDugmeSkociNaInformacijeKomanda = value; }
        }

        public void Executed_IstorijaDugmeSkociNaInformacijeKomanda(object obj)
        {
            TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Right);
            (Keyboard.FocusedElement as FrameworkElement).MoveFocus(request);
        }

        public bool CanExecute_IstorijaDugmeSkociNaInformacijeKomanda(object obj)
        {
            return true;
        }

        private RelayCommand informacijeDugmeSkociNaIstorijaTabKomanda;
        public RelayCommand InformacijeDugmeSkociNaIstorijaTabKomanda
        {
            get { return informacijeDugmeSkociNaIstorijaTabKomanda; }
            set { informacijeDugmeSkociNaIstorijaTabKomanda = value; }
        }

        public void Executed_InformacijeDugmeSkociNaIstorijaTabKomanda(object obj)
        {
            istorijaTab.Focus();
        }

        public bool CanExecute_InformacijeDugmeSkociNaIstorijaTabKomanda(object obj)
        {
            return true;
        }

        private RelayCommand vratiDugmeSkociNaLekTabKomanda;
        public RelayCommand VratiDugmeSkociNaLekTabKomanda
        {
            get { return vratiDugmeSkociNaLekTabKomanda; }
            set { vratiDugmeSkociNaLekTabKomanda = value; }
        }

        public void Executed_VratiDugmeSkociNaLekTabKomanda(object obj)
        {
            lekTab.Focus();
        }

        public bool CanExecute_VratiDugmeSkociNaLekTabKomanda(object obj)
        {
            return true;
        }

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
            inject.LekarController.Anamneza(new LekarServiceDTO(preglediTabela, listaOperacija, listaPregleda, lekarTrenutni, prikazPregleda, prikazOperacije));
            
        }

        public bool CanExecute_AnamnezaKomanda(object obj)
        {
            return true;
        }

        private RelayCommand anamnezaIstorijaKomanda;
        public RelayCommand AnamnezaIstorijaKomanda
        {
            get { return anamnezaIstorijaKomanda; }
            set
            {
                anamnezaIstorijaKomanda = value;

            }
        }

        public void Executed_AnamnezaIstorijaKomanda(object obj)
        {
            inject.LekarController.AnamnezaIstorija(new LekarServiceDTO(istorijaPregledaTabela, listaOperacija, listaPregleda, lekarTrenutni, prikazPregleda, prikazOperacije));
           
        }

        public bool CanExecute_AnamnezaIstorijaKomanda(object obj)
        {
            return true;
        }

        

        private RelayCommand izmeniLekKomanda;
        public RelayCommand IzmeniLekKomanda
        {
            get { return izmeniLekKomanda; }
            set
            {
                izmeniLekKomanda = value;

            }
        }

        public void Executed_IzmeniLekKomanda(object obj)
        {
            inject.LekarController.IzmeniLek(new LekarServiceDTO(lekoviTabela, lekovi));
        }

        public bool CanExecute_IzmeniLekKomanda(object obj)
        {
            return true;
        }

        private RelayCommand skociNaEnterLekKomanda;
        public RelayCommand SkociNaEnterLekKomanda
        {
            get { return skociNaEnterLekKomanda; }
            set
            {
                skociNaEnterLekKomanda = value;

            }
        }

        public void Executed_SkociNaEnterLekKomanda(object obj)
        {
            inject.LekarController.SkociNaEnterLek(new LekarServiceDTO(odobriDugme));
        }

        public bool CanExecute_SkociNaEnterLekKomanda(object obj)
        {
            return true;
        }

        private RelayCommand skociNaLevoLekKomanda;
        public RelayCommand SkociNaLevoLekKomanda
        {
            get { return skociNaLevoLekKomanda; }
            set
            {
                skociNaLevoLekKomanda = value;

            }
        }

        public void Executed_SkociNaLevoLekKomanda(object obj)
        {
            inject.LekarController.SkociNaLevoLek(new LekarServiceDTO(lekTab));
        }

        public bool CanExecute_SkociNaLevoLekKomanda(object obj)
        {
            return true;
        }

        private RelayCommand skociNaTabLekKomanda;
        public RelayCommand SkociNaTabLekKomanda
        {
            get { return skociNaTabLekKomanda; }
            set
            {
                skociNaTabLekKomanda = value;

            }
        }

        public void Executed_SkociNaTabLekKomanda(object obj)
        {
            inject.LekarController.SkociNaTabLek(new LekarServiceDTO(lekoviTabela));
        }

        public bool CanExecute_SkociNaTabLekKomanda(object obj)
        {
            return true;
        }

        private RelayCommand odobriLekKomanda;
        public RelayCommand OdobriLekKomanda
        {
            get { return odobriLekKomanda; }
            set
            {
                odobriLekKomanda = value;

            }
        }

        public void Executed_OdobriLekKomanda(object obj)
        {
            inject.LekarController.OdobriLek(new LekarServiceDTO(lekoviTabela,lekovi));
        }

        public bool CanExecute_OdobriLekKomanda(object obj)
        {
            return true;
        }

        private RelayCommand vratiNaIzmenuKomanda;
        public RelayCommand VratiNaIzmenuKomanda
        {
            get { return vratiNaIzmenuKomanda; }
            set
            {
                vratiNaIzmenuKomanda = value;

            }
        }

        public void Executed_VratiNaIzmenuKomanda(object obj)
        {
            inject.LekarController.VratiNaIzmenu(new LekarServiceDTO(lekoviTabela));
        }

        public bool CanExecute_VratiNaIzmenuKomanda(object obj)
        {
            return true;
        }
        #endregion

        public LekarViewModel(Lekar lekar)
        {
            lekarTrenutni = new Lekar();
            
            DemoFokus = false;
            Inject = new Injector();
            IzfiltrirajLekove();
            InicijalizujPolja(lekar);
            DobijPregledeLekara();
            DobijOperacijeLekara();
            NapraviKomande();

        }
        #region POMOCNE FUNKCIJE
        public void Popuni(DataGrid lekarGridd, DataGrid lekarGridIstorijaa, DataGrid dataGridLekovii, DataGrid dataGridOcena)
        {
            preglediTabela = lekarGridd;
            istorijaPregledaTabela = lekarGridIstorijaa;
            lekoviTabela = dataGridLekovii;
            oceneTabela = dataGridOcena;
            SortirajPodatke();
            FiltrirajPregledeZaPrikaz();
            FiltrirajOperacijeZaPrikaz();
            NapraviListuLekovaZaPrikaz();
            PodesiPrikazPregledaIOperacija();
            PrikaziLekove();
            PrikaziOcene();
        }

        public void PodesiParametre(Button Zakazi,TabItem PreglediTab,TabItem IstorijaTab,TabItem LekTab,Button AnamenzaIstorijaDugme,Button Odobri)
        {
            zakaziDugme = Zakazi;
            preglediTab = PreglediTab;
            istorijaTab = IstorijaTab;
            lekTab = LekTab;
            anamnezaIstorijaDugme = AnamenzaIstorijaDugme;
            odobriDugme = Odobri;
           
        }
        public static void RefreshPodaciListu()
        {
            podaciLista.Items.Refresh();
        }
        public static void RefreshPodaciListuIstorija()
        {
            podaciListaIstorija.Items.Refresh();
        }
        public void RefreshPreglediTabelu()
        {
            preglediTabela.Items.Refresh();
        }
        public void IzfiltrirajLekove()
        {
            lekovi = inject.LekarController.DobijLekove();

            for (int i = 0; i < lekovi.Count; i++)
            {
                if (lekovi[i].Status.Equals(StatusLeka.odbijen) || lekovi[i].Obrisan)
                {
                    lekovi.RemoveAt(i);
                    i--;
                }
            }
        }
        public void InicijalizujPolja(Lekar lekar)
        {
            lekarTrenutni = lekar;
            listaLekara = inject.LekarController.DobijLekare();
            listaPregleda = inject.LekarController.DobijPreglede();
            listaOperacija = inject.LekarController.DobijOperacije();
            listaPacijenata = inject.LekarController.DobijPacijente();
            listaProstorija = inject.LekarController.DobijProstorije();
        }
        public void DobijPregledeLekara()
        {
            for (int i = 0; i < listaPregleda.Count; i++)
            {
                if (!listaPregleda[i].Lekar.Jmbg.Equals(lekarTrenutni.Jmbg))
                {
                    listaPregleda.RemoveAt(i);
                    i = i - 1;
                }
            }
        }
        public void DobijOperacijeLekara()
        {
            for (int i = 0; i < listaOperacija.Count; i++)
            {
                if (!listaOperacija[i].Lekar.Jmbg.Equals(lekarTrenutni.Jmbg))
                {
                    listaOperacija.RemoveAt(i);
                    i = i - 1;
                }
            }
        }
        public void NapraviKomande()
        {
     
            ZakaziPregledKomanda = new RelayCommand(Executed_ZakaziPregledKomanda, CanExecute_ZakaziPregledKomanda);
            OtkaziPregledKomanda = new RelayCommand(Executed_OtkaziPregledKomanda, CanExecute_OtkaziPregledKomanda);
            IzmeniPregledKomanda = new RelayCommand(Executed_IzmeniPregledKomanda, CanExecute_IzmeniPregledKomanda);
            InformacijeOPacijentuKomanda = new RelayCommand(Executed_InformacijeOPacijentuKomanda, CanExecute_InformacijeOPacijentuKomanda);
            InformacijeOPacijentuIstorijaKomanda = new RelayCommand(Executed_InformacijeOPacijentuIstorijaKomanda, CanExecute_InformacijeOPacijentuIstorijaKomanda);
            SkociNaEnterKomanda = new RelayCommand(Executed_SkociNaEnterKomanda, CanExecute_SkociNaEnterKomanda);
            SkociNaLevoKomanda = new RelayCommand(Executed_SkociNaLevoKomanda, CanExecute_SkociNaLevoKomanda);
            SkociNaTabKomanda = new RelayCommand(Executed_SkociNaTabKomanda, CanExecute_SkociNaTabKomanda);
            SkociNaEnterIstorijaKomanda = new RelayCommand(Executed_SkociNaEnterIstorijaKomanda, CanExecute_SkociNaEnterIstorijaKomanda);
            SkociNaLevoIstorijaKomanda = new RelayCommand(Executed_SkociNaLevoIstorijaKomanda, CanExecute_SkociNaLevoIstorijaKomanda);
            SkociNaTabIstorijaKomanda = new RelayCommand(Executed_SkociNaTabIstorijaKomanda, CanExecute_SkociNaTabIstorijaKomanda);
            AnamnezaKomanda = new RelayCommand(Executed_AnamnezaKomanda, CanExecute_AnamnezaKomanda);
            AnamnezaIstorijaKomanda = new RelayCommand(Executed_AnamnezaIstorijaKomanda, CanExecute_AnamnezaIstorijaKomanda);
            IzmeniLekKomanda = new RelayCommand(Executed_IzmeniLekKomanda, CanExecute_IzmeniLekKomanda);
            SkociNaEnterLekKomanda = new RelayCommand(Executed_SkociNaEnterLekKomanda, CanExecute_SkociNaEnterLekKomanda);
            SkociNaLevoLekKomanda = new RelayCommand(Executed_SkociNaLevoLekKomanda, CanExecute_SkociNaLevoLekKomanda);
            SkociNaTabLekKomanda = new RelayCommand(Executed_SkociNaTabLekKomanda, CanExecute_SkociNaTabLekKomanda);
            OdobriLekKomanda = new RelayCommand(Executed_OdobriLekKomanda, CanExecute_OdobriLekKomanda);
            VratiNaIzmenuKomanda = new RelayCommand(Executed_VratiNaIzmenuKomanda, CanExecute_VratiNaIzmenuKomanda);
            IstorijaDugmeSkociNaInformacijeKomanda = new RelayCommand(Executed_IstorijaDugmeSkociNaInformacijeKomanda, CanExecute_IstorijaDugmeSkociNaInformacijeKomanda);
            InformacijeDugmeSkociNaIstorijaTabKomanda = new RelayCommand(Executed_InformacijeDugmeSkociNaIstorijaTabKomanda, CanExecute_InformacijeDugmeSkociNaIstorijaTabKomanda);
            VratiDugmeSkociNaLekTabKomanda = new RelayCommand(Executed_VratiDugmeSkociNaLekTabKomanda, CanExecute_VratiDugmeSkociNaLekTabKomanda);
            DemoFokusKomanda = new RelayCommand(Executed_DemoFokusKomanda, CanExecute_DemoFokusKomanda);
            DemoKomanda = new RelayCommand(Executed_DemoKomanda, CanExecute_DemoKomanda);
            DemoOtkaziKomanda = new RelayCommand(Executed_DemoOtkaziKomanda, CanExecute_DemoOtkaziKomanda);
            OdjavaKomanda = new RelayCommand(Executed_OdjavaKomanda, CanExecute_OdjavaKomanda);
            OceniKomanda = new RelayCommand(Executed_OceniKomanda, CanExecute_OceniKomanda);
        }

        public void SortirajPodatke()
        {
            SortirajPodaciLista();
            SortirajPodaciListaIstorija();
        }
        public void SortirajPodaciListaIstorija()
        {
            podaciListaIstorija.Items.SortDescriptions.Clear();
            podaciListaIstorija.Items.SortDescriptions.Add(new SortDescription("Datum", ListSortDirection.Descending));
        }


        public void SortirajPodaciLista()
        {
            podaciLista.Items.SortDescriptions.Clear();
            podaciLista.Items.SortDescriptions.Add(new SortDescription("Datum", ListSortDirection.Ascending));
        }
        public void FiltrirajPregledeZaPrikaz()
        {
            for (int i = 0; i < listaPregleda.Count; i++)
            {
                if (listaPregleda[i].Zavrsen.Equals(false))
                {
                    prikazPregleda = new PrikazPregleda(listaPregleda[i].Id, listaPregleda[i].Datum, listaPregleda[i].Trajanje, listaPregleda[i].Zavrsen,
                    listaPregleda[i].Hitan, listaPregleda[i].Anamneza, i, new LekarServiceDTO(listaLekara, listaProstorija, listaPacijenata, listaPregleda));
                    podaciLista.Items.Add(prikazPregleda);
                }

                else
                {
                    prikazPregleda = new PrikazPregleda(listaPregleda[i].Id, listaPregleda[i].Datum, listaPregleda[i].Trajanje, listaPregleda[i].Zavrsen,
                    listaPregleda[i].Hitan, listaPregleda[i].Anamneza, i, new LekarServiceDTO(listaLekara, listaProstorija, listaPacijenata, listaPregleda));
                    podaciListaIstorija.Items.Add(prikazPregleda);
                }
            }
        }

        public void FiltrirajOperacijeZaPrikaz()
        {
            for (int i = 0; i < listaOperacija.Count; i++)
            {
                if (listaOperacija[i].Zavrsen.Equals(false))
                {
                    prikazOperacije = new PrikazOperacije(listaOperacija[i].Id, listaOperacija[i].Datum, listaOperacija[i].Trajanje, listaOperacija[i].Zavrsen,
                    listaOperacija[i].Hitan, listaOperacija[i].Anamneza, listaOperacija[i].TipOperacije, i, new LekarServiceDTO(listaLekara,listaProstorija,listaPacijenata,listaOperacija));

                    podaciLista.Items.Add(prikazOperacije);
                }
                else
                {
                    prikazOperacije = new PrikazOperacije(listaOperacija[i].Id, listaOperacija[i].Datum, listaOperacija[i].Trajanje, listaOperacija[i].Zavrsen,
                    listaOperacija[i].Hitan, listaOperacija[i].Anamneza, listaOperacija[i].TipOperacije, i, new LekarServiceDTO(listaLekara, listaProstorija, listaPacijenata, listaOperacija));
                    podaciListaIstorija.Items.Add(prikazOperacije);
                }
            }
        }
        public void PodesiPrikazPregledaIOperacija()
        {

            RefreshPodaciListu();
            RefreshPodaciListuIstorija();

            preglediTabela.ItemsSource = podaciLista.Items;
            istorijaPregledaTabela.ItemsSource = podaciListaIstorija.Items;
        }

        public void NapraviListuLekovaZaPrikaz()
        {
            for (int i = 0; i < lekovi.Count; i++)
            {
                if (!lekovi[i].Status.Equals(StatusLeka.odbijen))
                {
                    PrikazLek p = new PrikazLek(lekovi[i].Id, lekovi[i].Naziv, lekovi[i].KolicinaUMg, lekovi[i].Status, lekovi[i].Zalihe, lekovi[i].Proizvodjac, i, new LekarServiceDTO(lekovi));
                    lekoviPrikaz.Add(p);
                }
            }
        }
        public void PrikaziLekove()
        {
            lekoviTabela.ItemsSource = lekoviPrikaz;
            
        }
        public void PrikaziOcene()
        {
            List<Ocena> ocene = new List<Ocena>();
            List<Pacijent> pacijenti = new List<Pacijent>();
            FileRepositoryOcena skalidsteOcena = new FileRepositoryOcena();
            FileRepositoryPacijent skladistePacijenata = new FileRepositoryPacijent();
            ocene = skalidsteOcena.GetAll();
            pacijenti = skladistePacijenata.GetAll();
            double sveOcene = 0;
            double brojOcena = 0;
            for (int i = 0; i < ocene.Count; i++)
            {
                if (lekarTrenutni.Jmbg.Equals(ocene[i].Lekar.Jmbg))
                {
                    for (int m = 0; m < pacijenti.Count; m++)
                    {
                        if (ocene[i].Pacijent.Jmbg.Equals(pacijenti[m].Jmbg))
                        {
                            ocene[i].Pacijent = pacijenti[m];
                            break;
                        }
                    }
                    sveOcene += ocene[i].BrojOcene;
                    brojOcena += 1;
                    ocenePrikaz.Add(ocene[i]);
                }
            }
            prosecnaOcena = "Ocena: "+(sveOcene / brojOcena).ToString("0.0");
            oceneTabela.ItemsSource = ocenePrikaz;

        }






        public void DemoKomandaIzView()
        {
            
            
            TerminLekarViewModel vm = new TerminLekarViewModel(inject.LekarController.DobijLekare()[0]);
            FormNapraviTerminLekar form = new FormNapraviTerminLekar(vm);

            prekidaj = false;

           

         
            int i = 0;
            int predjiDalje1 = 0;
            _activeTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(0.5)
            };
            _activeTimer.Tick += delegate (object sender, EventArgs e) {
                if (!prekidaj)
                {
                    if (predjiDalje1 == 0)
                    {
                        ZaTermin(ref i, ref vm, ref predjiDalje1);
                    }
                    else if (predjiDalje1 == 1)
                    {
                        _activeTimer.Stop();
                        DemoKomandaIzView3();
                    }
                }
                else
                {
                    _activeTimer.Stop();
                    vm.CloseAction();
                }
            };
            _activeTimer.Start();
          
          

        }
        public void ZaTermin(ref int i, ref TerminLekarViewModel vm, ref int predjiDalje1)
        {
            string specijali = "Opsta";
            string leka = "Pap Vatroslav 0303965123456";
            string pac = "Savić Stefan 2512002149573";
            string pro = "103";
            string vreme = "00:30:00";
            if (i < 5)
                {
                    if (i == 0)
                    {
                        vm.ItemSourceSpecijalizacijaComboOpen = true;
                    }
                    vm.Specijalizacija += specijali[i];
                    i++;
                }
                else if (i >= 5 && i < 32)
                {
                    if (i == 5)
                    {
                        vm.ItemSourceLekarComboOpen = true;
                    }
                    vm.LekarPodaci += leka[i - 5];
                    i++;
                }
                else if (i >= 32 && i < 58)
                {
                    if (i == 32)
                    {
                        vm.ItemSourcePacijentComboOpen = true;
                    }
                    vm.PacijentPodaci += pac[i - 32];
                    i++;
                }
                else if (i >= 58 && i < 61)
                {
                    if (i == 58)
                    {
                        vm.ItemSourceProstorijaComboOpen = true;
                    }
                    vm.BrojProstorije += pro[i - 58];
                    i++;
                }
                else if (i >= 61 && i < 69)
                {
                    if (i == 61)
                    {
                        vm.ItemSourceVremeComboOpen = true;
                    }
                    vm.VremePregleda += vreme[i - 61];

                    if (i == 68)
                    {
                        vm.ItemSourceVremeComboOpen = false;
                    }
                    i++;
                }
                else if (i >=69 && i<75)
                {
                i++;
                 }
            else if (i >= 75)
            {
                vm.CloseAction();
                predjiDalje1 = 1;
            }
            

        }


        public void DemoKomandaIzView2()
        {


            LekLekarViewModel vm = new LekLekarViewModel(inject.LekarController.DobijLekove()[0]);
            FormIzmeniLekLekar form = new FormIzmeniLekLekar(vm);
            int i = 0;
            int predjiDalje1 = 0;
            vm.Proizvodjac = "";
            vm.Lek = "";
            vm.Doza = "";
            _activeTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(0.5)
            };
            _activeTimer.Tick += delegate (object sender, EventArgs e) {
                if (!prekidaj)
                {
                    if (predjiDalje1 == 0)
                    {
                        ZaLek(ref i, ref vm, ref predjiDalje1);
                    }
                    else if (predjiDalje1 == 1)
                    {
                        _activeTimer.Stop();
                        DemoKomandaIzView();
                    }
                }
                else
                {
                    _activeTimer.Stop();
                    vm.ZatvoriAkcija();
                }
            };
            _activeTimer.Start();
        }

        public void ZaLek(ref int i, ref LekLekarViewModel vm, ref int predjiDalje1)
        {
            string proi = "Sinofarm";
            string leka = "Paracetamol ";
            string doza = "500";
            if (i < 8)
            {
                if (i == 0)
                {
                    vm.ItemSourceProizvodjacComboOpen = true;
                }
                vm.Proizvodjac += proi[i];
                i++;
            }
            else if (i >= 8 && i < 20)
            {
                if (i == 8)
                {
                    vm.ItemSourceNazivLekaComboOpen = true;
                }
                vm.Lek += leka[i - 8];
                i++;
            }
            else if (i >= 20 && i < 23)
            {
                if (i == 20)
                {
                    vm.ItemSourceDozaComboOpen = true;
                }
                vm.Doza += doza[i - 20];
                i++;
            }
            
            else if (i >= 23 && i < 30)
            {   if (i == 23)
                {
                    vm.ItemSourceDozaComboOpen = false;
                }
                i++;
            }
            else if (i >= 30)
            {
                vm.ZatvoriAkcija();
                predjiDalje1 = 1;
            }


        }

        public void DemoKomandaIzView3()
        {
            List<Pregled> pregledi = inject.LekarController.DobijPreglede();
            PrikazPregleda p = new PrikazPregleda();
            for (int i = 0; i < pregledi.Count; i++)
            {
                if(!pregledi[i].Zavrsen && pregledi[i].Datum < DateTime.Now)
                {
                    p = new PrikazPregleda(pregledi[i].Id, pregledi[i].Datum, pregledi[i].Trajanje, pregledi[i].Zavrsen, pregledi[i].Hitan, pregledi[i].Anamneza);
                    break;
                }
            }
            
            AnamnezaLekarViewModel vm = new AnamnezaLekarViewModel(p, inject.LekarController.DobijLekare()[0]);
            FormNapraviAnamnezuLekar form = new FormNapraviAnamnezuLekar(vm);
            int L = 0;
            int predjiDalje1 = 0;
            _activeTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(0.5)
            };
            _activeTimer.Tick += delegate (object sender, EventArgs e) {
                if (!prekidaj)
                {
                    if (predjiDalje1 == 0)
                    {
                        ZaAnamnezu(ref L, ref vm, ref predjiDalje1);
                    }
                    else if (predjiDalje1 == 1)
                    {
                        _activeTimer.Stop();
                        DemoKomandaIzView4(vm);
                    }
                }
                else
                {
                    _activeTimer.Stop();
                    vm.ZatvoriAction();
                }
            };
            _activeTimer.Start();
        }

        public void ZaAnamnezu(ref int i, ref AnamnezaLekarViewModel vm, ref int predjiDalje1)
        {
            string sim = "Kasalj, temparatura, bol u stomaku";
            string dijagnoza = "Potrebno je odraditi jos testova";
            if (i < 34)
            {
                vm.Simptomi += sim[i];
                i++;
            }
            else if (i >= 34 && i < 66)
            {

                vm.Dijagnoza += dijagnoza[i - 34];
                i++;
            }
            else if (i >= 66 && i < 70)
            {
                i++;
            }
            else if (i >= 70)
            {

                predjiDalje1 = 1;
            }


        }

        public void DemoKomandaIzView4(AnamnezaLekarViewModel view)
        {
            List<Pregled> pregledi = inject.LekarController.DobijPreglede();
            PrikazPregleda p = new PrikazPregleda();
            for (int i = 0; i < pregledi.Count; i++)
            {
                if (!pregledi[i].Zavrsen && pregledi[i].Datum < DateTime.Now)
                {
                    p = new PrikazPregleda(pregledi[i].Id, pregledi[i].Datum, pregledi[i].Trajanje, pregledi[i].Zavrsen, pregledi[i].Hitan, pregledi[i].Anamneza);
                    break;
                }
            }
            
            ReceptLekarViewModel vm = new ReceptLekarViewModel(p.Pacijent);
            FormNapraviReceptLekar form = new FormNapraviReceptLekar(vm);
            int L = 0;
            int predjiDalje1 = 0;
            _activeTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(0.5)
            };
            _activeTimer.Tick += delegate (object sender, EventArgs e) {
                if (!prekidaj)
                {
                    if (predjiDalje1 == 0)
                    {
                        ZaRecept(ref L, ref vm, ref predjiDalje1);
                    }
                    else if (predjiDalje1 == 1)
                    {
                        _activeTimer.Stop();
                        view.ZatvoriAction();
                        DemoKomandaIzView2();
                    }
                }
                else
                {
                    _activeTimer.Stop();
                    vm.ZatvoriAkcija();
                    view.ZatvoriAction();
                }
            };
            _activeTimer.Start();
        }

        public void ZaRecept(ref int i, ref ReceptLekarViewModel vm, ref int predjiDalje1)
        {
            string proizvodjac = "Galenika";
            string naziv = "Panadol";
            string doza = "200";
            string brojKutija = "1";
            string vremeUzimanja = "1";
            DateTime datumPrekida = DateTime.Parse("13.6.2021");

            if (i < 8)
            {
                if (i == 0)
                {
                    vm.ItemSourceProizvodjacComboOpen = true;
                }
                vm.ProizvodjacLeka += proizvodjac[i];
                i++;
            }
            else if (i >= 8 && i < 15)
            {
                if (i ==8)
                {
                    vm.ItemSourceNazivLekaComboOpen = true;
                }
                vm.NazivLeka += naziv[i - 8];
                i++;
            }
            else if (i >= 15 && i < 18)
            {
                if (i == 15)
                {
                    vm.ItemSourceDozaComboOpen = true;
                }
                vm.DozaLeka += doza[i - 15];
                i++;
            }
            else if (i >= 18 && i < 19)
            {
                if (i == 18)
                {
                    vm.ItemSourceBrojKutijaComboOpen = true;
                }
                vm.BrojKutijaLeka += brojKutija[i - 18];
                i++;
            }
            else if (i >= 19 && i < 20)
            {
                if (i == 19)
                {
                    vm.ItemSourceVremeUzimanjaComboOpen = true;
                }
                vm.VremeUzimanjaLeka += vremeUzimanja[i - 19];
                i++;
            }
            else if (i>=20 && i < 21)
            {
                vm.ItemSourceVremeUzimanjaComboOpen = false;
                vm.DatumPrekidaRecepta = datumPrekida;
                i++;
            }
            else if (i >= 21 && i < 26)
            {
                i++;
            }
            else if (i >= 26)
            {
                vm.ZatvoriAkcija();
                predjiDalje1 = 1;
            }
        }

        #endregion
    }
}
