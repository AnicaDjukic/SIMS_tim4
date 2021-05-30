using Bolnica.Commands;
using Bolnica.DTO;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Model.Korisnici;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;

namespace Bolnica.ViewModel
{
    public class AnamnezaLekarViewModel : ViewModel
    {
        #region POLJA
        public string simptomi { get; set; }
        public string dijagnoza { get; set; }

        public List<PrikazRecepta> recepti { get; set; }
        private bool DaLiPostojiAnamneza = false;
        private List<Lek> sviLekovi = new List<Lek>();
        private List<Pregled> sviPregledi = new List<Pregled>();
        private List<Operacija> sveOperacije = new List<Operacija>();
        private List<Lekar> sviLekari = new List<Lekar>();
        private Lekar ulogovaniLekar = new Lekar();
        private Pacijent trenutniPacijent = new Pacijent();
        private PrikazPregleda trenutniPregled = new PrikazPregleda();
        private PrikazOperacije trenutnaOperacija = new PrikazOperacije();
        private PrikazPregleda stariPregled = new PrikazPregleda();
        private PrikazOperacije staraOperacija = new PrikazOperacije();
        private List<Anamneza> sveAnamneze = new List<Anamneza>();
        private int idAnamneze;
        private bool DaLiJePregled = false;
        public DataGrid dataGridLekovi;
        public Button izbrisiButton;
        public ScrollViewer ScrollBar;

        public static ObservableCollection<PrikazRecepta> Recepti
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
        public Action ZatvoriAction { get; set; }

        private bool datumProsao;
        public bool DatumProsao
        {
            get { return datumProsao; }
            set
            {
                datumProsao = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region KOMANDE
        private RelayCommand obrisiReceptKomanda;
        public RelayCommand ObrisiReceptKomanda
        {
            get { return obrisiReceptKomanda; }
            set
            {
                obrisiReceptKomanda = value;

            }
        }

        public void Executed_ObrisiReceptKomanda(object obj)
        {
            inject.AnamnezaLekarController.ObrisiRecept(new AnamnezaLekarDTO(dataGridLekovi));
        }

        public bool CanExecute_ObrisiReceptKomanda(object obj)
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
            inject.AnamnezaLekarController.ZakaziPregled(new AnamnezaLekarDTO(ulogovaniLekar, trenutniPacijent));

        }

        public bool CanExecute_ZakaziPregledKomanda(object obj)
        {
            return true;
        }

        private RelayCommand vidiReceptKomanda;
        public RelayCommand VidiReceptKomanda
        {
            get { return vidiReceptKomanda; }
            set
            {
                vidiReceptKomanda = value;

            }
        }

        public void Executed_VidiReceptKomanda(object obj)
        {
            inject.AnamnezaLekarController.VidiDetaljeOReceptu(new AnamnezaLekarDTO(dataGridLekovi, trenutniPacijent));
        }

        public bool CanExecute_VidiReceptKomanda(object obj)
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
            ZatvoriAction();
        }

        public bool CanExecute_ZatvoriKomanda(object obj)
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
            inject.AnamnezaLekarController.DodajLek(new AnamnezaLekarDTO(trenutniPacijent));
        }

        public bool CanExecute_DodajReceptKomanda(object obj)
        {
            return true;
        }

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
            inject.AnamnezaLekarController.Potvrdi(new AnamnezaLekarDTO(DaLiPostojiAnamneza, DaLiJePregled, idAnamneze, simptomi, dijagnoza, stariPregled, trenutniPregled, staraOperacija, trenutnaOperacija, sveAnamneze));
            ZatvoriAction();
        }

        public bool CanExecute_PotvrdiKomanda(object obj)
        {
            return true;
        }

        private RelayCommand predjiNaScrollBarKomanda;
        public RelayCommand PredjiNaScrollBarKomanda
        {
            get { return predjiNaScrollBarKomanda; }
            set
            {
                predjiNaScrollBarKomanda = value;

            }
        }

        public void Executed_PredjiNaScrollBarKomanda(object obj)
        {
            inject.AnamnezaLekarController.PredjiNaScrollBar(new AnamnezaLekarDTO(izbrisiButton));
        }

        public bool CanExecute_PredjiNaScrollBarKomanda(object obj)
        {
            return true;
        }

        private RelayCommand zaustaviStreliceKomanda;
        public RelayCommand ZaustaviStreliceKomanda
        {
            get { return zaustaviStreliceKomanda; }
            set
            {
                zaustaviStreliceKomanda = value;

            }
        }

        public void Executed_ZaustaviStreliceKomanda(object obj)
        {
            inject.AnamnezaLekarController.ZaustaviStrelice(new AnamnezaLekarDTO(ScrollBar));
        }

        public bool CanExecute_ZaustaviStreliceKomanda(object obj)
        {
            return true;
        }
#endregion
        public AnamnezaLekarViewModel(PrikazPregleda izabraniPregled, Lekar ulogovaniLekar)
        {
            Inject = new Injector();
            DaLiJePregled = true;
            FiltirajLekove();
            InicirajPodatkeZaPregled(izabraniPregled, ulogovaniLekar);
            PopuniIliKreirajAnamnezuPregleda(izabraniPregled);
            NapraviKomande();
        }

        public AnamnezaLekarViewModel(PrikazOperacije izabranaOperacija, Lekar ulogovaniLekar)
        {
            Inject = new Injector();
            InicirajPodatkeZaOperaciju(izabranaOperacija, ulogovaniLekar);
            FiltirajLekove();
            PopuniIliKreirajAnamnezuOperacije(izabranaOperacija);
            NapraviKomande();
        }
        #region POMOCNE FUNKCIJE
        public void NapraviKomande()
        {
            ZakaziPregledKomanda = new RelayCommand(Executed_ZakaziPregledKomanda, CanExecute_ZakaziPregledKomanda);
            ObrisiReceptKomanda = new RelayCommand(Executed_ObrisiReceptKomanda, CanExecute_ObrisiReceptKomanda);
            VidiReceptKomanda = new RelayCommand(Executed_VidiReceptKomanda, CanExecute_VidiReceptKomanda);
            DodajReceptKomanda = new RelayCommand(Executed_DodajReceptKomanda, CanExecute_DodajReceptKomanda);
            ZatvoriKomanda = new RelayCommand(Executed_ZatvoriKomanda, CanExecute_ZatvoriKomanda);
            PotvrdiKomanda = new RelayCommand(Executed_PotvrdiKomanda, CanExecute_PotvrdiKomanda);
            PredjiNaScrollBarKomanda = new RelayCommand(Executed_PredjiNaScrollBarKomanda, CanExecute_PredjiNaScrollBarKomanda);
            ZaustaviStreliceKomanda = new RelayCommand(Executed_ZaustaviStreliceKomanda, CanExecute_ZaustaviStreliceKomanda);
        }
       
        
        private void PopuniAnamnezu(PrikazPregleda izabraniPregled, int i)
        {
            idAnamneze = izabraniPregled.Anamneza.Id;
            simptomi = sveAnamneze[i].Simptomi;
            dijagnoza = sveAnamneze[i].Dijagnoza;
            popuniRecepte(i);
        }
        private void PopuniAnamnezu(PrikazOperacije izabranaOperacija, int i)
        {
            idAnamneze = izabranaOperacija.Anamneza.Id;
            simptomi = sveAnamneze[i].Simptomi;
            dijagnoza = sveAnamneze[i].Dijagnoza;
            popuniRecepte(i);
        }

        private void popuniRecepte(int i)
        {
            PrikazRecepta noviPrikazRecepta = new PrikazRecepta();
            for (int r = 0; r < sveAnamneze[i].Recept.Count; r++)
            {
                noviPrikazRecepta = new PrikazRecepta(sveAnamneze[i].Recept[r].DatumIzdavanja, sveAnamneze[i].Recept[r].Kolicina, sveAnamneze[i].Recept[r].VremeUzimanja, sveAnamneze[i].Recept[r].Trajanje);
                noviPrikazRecepta.Id = sveAnamneze[i].Recept[r].Id;
                for (int le = 0; le < sviLekovi.Count; le++)
                {
                    if (sveAnamneze[i].Recept[r].Lek.Id.Equals(sviLekovi[le].Id))
                    {
                        noviPrikazRecepta.lek = sviLekovi[le];
                        break;
                    }
                }
                Recepti.Add(noviPrikazRecepta);

            }
        }

        
        private void FiltirajLekove()
        {
            sviLekovi = inject.AnamnezaLekarController.DobijLekove();

            for (int i = 0; i < sviLekovi.Count; i++)
            {
                if (sviLekovi[i].Status.Equals(StatusLeka.odbijen) || sviLekovi[i].Obrisan)
                {
                    sviLekovi.RemoveAt(i);
                    i--;
                }
            }
        }

        private void InicirajPodatkeZaPregled(PrikazPregleda izabraniPregled, Lekar ulogovaniLekar)
        {
            DatumProsao = true;
            trenutniPacijent = izabraniPregled.Pacijent;
            sveAnamneze = inject.AnamnezaLekarController.DobijAnamneze();
            Recepti = new ObservableCollection<PrikazRecepta>();
            simptomi = "";
            dijagnoza = "";
            trenutniPregled = izabraniPregled;
            sviLekari = inject.AnamnezaLekarController.DobijLekare();
            stariPregled = izabraniPregled;
            this.ulogovaniLekar = ulogovaniLekar;
        }
        private void InicirajPodatkeZaOperaciju(PrikazOperacije izabranaOperacija, Lekar ulogovaniLekar)
        {
            DatumProsao = true;
            trenutniPacijent = izabranaOperacija.Pacijent;
            trenutnaOperacija = izabranaOperacija;
            staraOperacija = izabranaOperacija;
            sveAnamneze = inject.AnamnezaLekarController.DobijAnamneze();
            simptomi = "";
            dijagnoza = "";
            sviLekovi = inject.AnamnezaLekarController.DobijLekove();
            sviLekari = inject.AnamnezaLekarController.DobijLekare();
            this.ulogovaniLekar = ulogovaniLekar;
            Recepti = new ObservableCollection<PrikazRecepta>();
        }
        private void PopuniIliKreirajAnamnezuPregleda(PrikazPregleda izabraniPregled)
        {
            for (int i = 0; i < sveAnamneze.Count; i++)
            {
                if (izabraniPregled.Anamneza.Id == sveAnamneze[i].Id)
                {
                    ProveriValidnostDatumaIzdavanja(izabraniPregled);
                    PopuniAnamnezu(izabraniPregled, i);
                    DaLiPostojiAnamneza = true;
                    break;
                }
            }

            if (!DaLiPostojiAnamneza)
            {
             
                Recepti = new ObservableCollection<PrikazRecepta>();
            }
        }

        private void PopuniIliKreirajAnamnezuOperacije(PrikazOperacije izabranaOperacija)
        {
            for (int i = 0; i < sveAnamneze.Count; i++)
            {
                if (izabranaOperacija.Anamneza.Id == sveAnamneze[i].Id)
                {
                    ProveriValidnostDatumaIzdavanja(izabranaOperacija);
                    PopuniAnamnezu(izabranaOperacija, i);
                    DaLiPostojiAnamneza = true;
                    break;
                }
            }
            if (!DaLiPostojiAnamneza )
            {
               
                Recepti = new ObservableCollection<PrikazRecepta>();
            }
        }

        private void ProveriValidnostDatumaIzdavanja(PrikazPregleda izabraniPregled)
        {
            if ((izabraniPregled.Datum > DateTime.Now) || (izabraniPregled.Datum.AddDays(7) < DateTime.Now))
            {
                DatumProsao = false;
            }
        }

        private void ProveriValidnostDatumaIzdavanja(PrikazOperacije izabranaOperacija)
        {
            if ((izabranaOperacija.Datum > DateTime.Now) || (izabranaOperacija.Datum.AddDays(7) < DateTime.Now))
            {
                DatumProsao = false;
            }

        }

        #endregion
    }
}
