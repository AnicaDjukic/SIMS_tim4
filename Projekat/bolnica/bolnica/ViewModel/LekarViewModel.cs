using Bolnica.Commands;
using Bolnica.DTO;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Model.Korisnici;
using Model.Pacijenti;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        private Lekar lekarTrenutni = new Lekar();
        private PrikazPregleda prikazPregleda = new PrikazPregleda();
        private PrikazOperacije prikazOperacije = new PrikazOperacije();
        private DataGrid preglediTabela;
        private DataGrid istorijaPregledaTabela;
        private DataGrid lekoviTabela;
        private Button zakaziDugme;
        private TabItem preglediTab;
        private TabItem istorijaTab;
        private TabItem lekTab;
        private Button anamnezaIstorijaDugme;
        private Button odobriDugme;


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
            Inject = new Injector();
            IzfiltrirajLekove();
            InicijalizujPolja(lekar);
            DobijPregledeLekara();
            DobijOperacijeLekara();
            NapraviKomande();

        }
        #region POMOCNE FUNKCIJE
        public void Popuni(DataGrid lekarGridd, DataGrid lekarGridIstorijaa, DataGrid dataGridLekovii)
        {
            preglediTabela = lekarGridd;
            istorijaPregledaTabela = lekarGridIstorijaa;
            lekoviTabela = dataGridLekovii;
            SortirajPodatke();
            FiltrirajPregledeZaPrikaz();
            FiltrirajOperacijeZaPrikaz();
            NapraviListuLekovaZaPrikaz();
            PodesiPrikazPregledaIOperacija();
            PrikaziLekove();
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
        #endregion
    }
}
