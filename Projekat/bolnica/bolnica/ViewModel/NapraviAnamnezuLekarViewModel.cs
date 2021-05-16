using Bolnica.Commands;
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
    public class NapraviAnamnezuLekarViewModel : ViewModel
    {
        public string simptomi { get; set; }
        public string dijagnoza { get; set; }

        public List<PrikazRecepta> recepti { get; set; }
        private int DaLiPostojiAnamneza = 0;
        private List<Lek> sviLekovi = new List<Lek>();
        private FileStoragePregledi skladistePregleda = new FileStoragePregledi();
        private FileStorageLek skladisteLekova = new FileStorageLek();
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
        private FileStorageAnamneza skladisteAnamneza = new FileStorageAnamneza();
        private FileStorageLekar skladisteLekara = new FileStorageLekar();
        private int idAnamneze;
        private int DaLiJePregled = 0;
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
        public Action CloseAction { get; set; }

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

        private RelayCommand obrisiReceptCommand;
        public RelayCommand ObrisiReceptCommand
        {
            get { return obrisiReceptCommand; }
            set
            {
                obrisiReceptCommand = value;

            }
        }

        public void Executed_ObrisiReceptCommand(object obj)
        {
            inject.NapraviAnamnezuLekarService.ObrisiRecept(dataGridLekovi);
        }

        public bool CanExecute_ObrisiReceptCommand(object obj)
        {
            return true;
        }

        private RelayCommand zakaziPregledCommand;
        public RelayCommand ZakaziPregledCommand
        {
            get { return zakaziPregledCommand; }
            set
            {
                zakaziPregledCommand = value;

            }
        }

        public void Executed_ZakaziPregledCommand(object obj)
        {
            inject.NapraviAnamnezuLekarService.ZakaziPregled(ulogovaniLekar, trenutniPacijent);
        }

        public bool CanExecute_ZakaziPregledCommand(object obj)
        {
            return true;
        }

        private RelayCommand vidiReceptCommand;
        public RelayCommand VidiReceptCommand
        {
            get { return vidiReceptCommand; }
            set
            {
                vidiReceptCommand = value;

            }
        }

        public void Executed_VidiReceptCommand(object obj)
        {
            inject.NapraviAnamnezuLekarService.VidiDetaljeOReceptu(dataGridLekovi, trenutniPacijent);
        }

        public bool CanExecute_VidiReceptCommand(object obj)
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
            inject.NapraviAnamnezuLekarService.DodajLek(trenutniPacijent);
        }

        public bool CanExecute_DodajReceptCommand(object obj)
        {
            return true;
        }

        private RelayCommand potvrdiCommand;
        public RelayCommand PotvrdiCommand
        {
            get { return potvrdiCommand; }
            set
            {
                potvrdiCommand = value;

            }
        }

        public void Executed_PotvrdiCommand(object obj)
        {
            inject.NapraviAnamnezuLekarService.Potvrdi(DaLiPostojiAnamneza, DaLiJePregled, idAnamneze, simptomi, dijagnoza, stariPregled, trenutniPregled, staraOperacija, trenutnaOperacija, sveAnamneze);
            CloseAction();
        }

        public bool CanExecute_PotvrdiCommand(object obj)
        {
            return true;
        }

        private RelayCommand predjiNaScrollBarCommand;
        public RelayCommand PredjiNaScrollBarCommand
        {
            get { return predjiNaScrollBarCommand; }
            set
            {
                predjiNaScrollBarCommand = value;

            }
        }

        public void Executed_PredjiNaScrollBarCommand(object obj)
        {
            inject.NapraviAnamnezuLekarService.PredjiNaScrollBar(izbrisiButton);
        }

        public bool CanExecute_PredjiNaScrollBarCommand(object obj)
        {
            return true;
        }

        private RelayCommand zaustaviStrelicaCommand;
        public RelayCommand ZaustaviStrelicaCommand
        {
            get { return zaustaviStrelicaCommand; }
            set
            {
                zaustaviStrelicaCommand = value;

            }
        }

        public void Executed_ZaustaviStrelicaCommand(object obj)
        {
            inject.NapraviAnamnezuLekarService.ZaustaviStrelice(ScrollBar);
        }

        public bool CanExecute_ZaustaviStrelicaCommand(object obj)
        {
            return true;
        }

        public NapraviAnamnezuLekarViewModel(PrikazPregleda izabraniPregled, Lekar ulogovaniLekar)
        {
            DaLiJePregled = 1;
            Inject = new Injector();
            FiltirajLekove();
            InicirajPodatkeZaPregled(izabraniPregled, ulogovaniLekar);
            PopuniIliKreirajAnamnezuPregleda(izabraniPregled);
            ZakaziPregledCommand = new RelayCommand(Executed_ZakaziPregledCommand, CanExecute_ZakaziPregledCommand);
            ObrisiReceptCommand = new RelayCommand(Executed_ObrisiReceptCommand, CanExecute_ObrisiReceptCommand);
            VidiReceptCommand = new RelayCommand(Executed_VidiReceptCommand, CanExecute_VidiReceptCommand);
            DodajReceptCommand = new RelayCommand(Executed_DodajReceptCommand, CanExecute_DodajReceptCommand);
            ZatvoriCommand = new RelayCommand(Executed_ZatvoriCommand, CanExecute_ZatvoriCommand);
            PotvrdiCommand = new RelayCommand(Executed_PotvrdiCommand, CanExecute_PotvrdiCommand);
            PredjiNaScrollBarCommand = new RelayCommand(Executed_PredjiNaScrollBarCommand, CanExecute_PredjiNaScrollBarCommand);
            ZaustaviStrelicaCommand = new RelayCommand(Executed_ZaustaviStrelicaCommand, CanExecute_ZaustaviStrelicaCommand);
        }

        public NapraviAnamnezuLekarViewModel(PrikazOperacije izabranaOperacija, Lekar ulogovaniLekar)
        {
            InicirajPodatkeZaOperaciju(izabranaOperacija, ulogovaniLekar);
            Inject = new Injector();
            FiltirajLekove();
            PopuniIliKreirajAnamnezuOperacije(izabranaOperacija);
            ZakaziPregledCommand = new RelayCommand(Executed_ZakaziPregledCommand, CanExecute_ZakaziPregledCommand);
            ObrisiReceptCommand = new RelayCommand(Executed_ObrisiReceptCommand, CanExecute_ObrisiReceptCommand);
            VidiReceptCommand = new RelayCommand(Executed_VidiReceptCommand, CanExecute_VidiReceptCommand);
            DodajReceptCommand = new RelayCommand(Executed_DodajReceptCommand, CanExecute_DodajReceptCommand);
            ZatvoriCommand = new RelayCommand(Executed_ZatvoriCommand, CanExecute_ZatvoriCommand);
            PotvrdiCommand = new RelayCommand(Executed_PotvrdiCommand, CanExecute_PotvrdiCommand);
            PredjiNaScrollBarCommand = new RelayCommand(Executed_PredjiNaScrollBarCommand, CanExecute_PredjiNaScrollBarCommand);
            ZaustaviStrelicaCommand = new RelayCommand(Executed_ZaustaviStrelicaCommand, CanExecute_ZaustaviStrelicaCommand);
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
                noviPrikazRecepta = new PrikazRecepta();
                noviPrikazRecepta.DatumIzdavanja = sveAnamneze[i].Recept[r].DatumIzdavanja;
                noviPrikazRecepta.Id = sveAnamneze[i].Recept[r].Id;
                noviPrikazRecepta.Kolicina = sveAnamneze[i].Recept[r].Kolicina;
                noviPrikazRecepta.Trajanje = sveAnamneze[i].Recept[r].Trajanje;
                noviPrikazRecepta.VremeUzimanja = sveAnamneze[i].Recept[r].VremeUzimanja;
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

        private void InicirajPodatkeZaPregled(PrikazPregleda izabraniPregled, Lekar ulogovaniLekar)
        {
            DatumProsao = true;
            trenutniPacijent = izabraniPregled.Pacijent;
            sveAnamneze = skladisteAnamneza.GetAll();
            Recepti = new ObservableCollection<PrikazRecepta>();
            simptomi = "";
            dijagnoza = "";
            trenutniPregled = izabraniPregled;
            sviLekari = skladisteLekara.GetAll();
            stariPregled = izabraniPregled;
            this.ulogovaniLekar = ulogovaniLekar;
        }
        private void InicirajPodatkeZaOperaciju(PrikazOperacije izabranaOperacija, Lekar ulogovaniLekar)
        {
            DatumProsao = true;
            trenutniPacijent = izabranaOperacija.Pacijent;
            trenutnaOperacija = izabranaOperacija;
            staraOperacija = izabranaOperacija;
            sveAnamneze = skladisteAnamneza.GetAll();
            simptomi = "";
            dijagnoza = "";
            sviLekovi = skladisteLekova.GetAll();
            sviLekari = skladisteLekara.GetAll();
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
                    DaLiPostojiAnamneza = 1;
                    break;
                }
            }

            if (DaLiPostojiAnamneza == 0)
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
                    DaLiPostojiAnamneza = 1;
                    break;
                }
            }
            if (DaLiPostojiAnamneza == 0)
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

    }
}
