using Bolnica.Commands;
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
using System.Windows.Controls;

namespace Bolnica.ViewModel
{
    public class LekarViewModel : ViewModel
    {
        public static List<Pregled> listaPregleda = new List<Pregled>();
        public static List<Operacija> listaOperacija = new List<Operacija>();
        public static DataGrid dataList = new DataGrid();
        public static DataGrid dataListIstorija = new DataGrid();
        public static ObservableCollection<PrikazLek> lekoviPrikaz = new ObservableCollection<PrikazLek>();
        public static List<Lekar> listaLekara = new List<Lekar>();
        private Lekar lekarTrenutni = new Lekar();
        private Lekar lekarPomocni = new Lekar();
        private Lekar ll3 = new Lekar();
        private Lekar ll4 = new Lekar();
        private FileRepositoryPregled sviPregledi = new FileRepositoryPregled();
        private FileRepositoryPacijent sviPacijenti = new FileRepositoryPacijent();
        private FileStorageProstorija sveProstorije = new FileStorageProstorija();
        private FileRepositoryLekar sviLekari = new FileRepositoryLekar();
        private FileStorageLek sviLekovi = new FileStorageLek();
        private List<Pacijent> listaPacijenata = new List<Pacijent>();
        private List<Prostorija> listaProstorija = new List<Prostorija>();
        private PrikazPregleda prikazPregleda = new PrikazPregleda();
        private PrikazOperacije prikazOperacije = new PrikazOperacije();
        private List<Lek> lekovi = new List<Lek>();
        public DataGrid lekarGrid;
        public DataGrid lekarGridIstorija;
        public DataGrid dataGridLekovi;
        public Button Zakazi;
        public TabItem PreglediTab;
        public TabItem IstorijaTab;
        public TabItem LekTab;
        public Button AnamnezaIstorijaDugme;
        public Button OdobriButton;


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
            inject.LekarService.ZakaziPregled(lekarTrenutni);
            inject.LekarService.CollorLekarGrid(lekarGrid);
        }

        public bool CanExecute_ZakaziPregledCommand(object obj)
        {
            return true;
        }
        private RelayCommand otkaziPregledCommand;
        public RelayCommand OtkaziPregledCommand
        {
            get { return otkaziPregledCommand; }
            set
            {
                otkaziPregledCommand = value;

            }
        }

        public void Executed_OtkaziPregledCommand(object obj)
        {
            inject.LekarService.OtkaziPregled(lekarGrid,prikazPregleda,listaPregleda,prikazOperacije,listaOperacija);
            inject.LekarService.CollorLekarGrid(lekarGrid);
        }

        public bool CanExecute_OtkaziPregledCommand(object obj)
        {
            return true;
        }
        private RelayCommand izmeniPregledCommand;
        public RelayCommand IzmeniPregledCommand
        {
            get { return izmeniPregledCommand; }
            set
            {
                izmeniPregledCommand = value;

            }
        }

        public void Executed_IzmeniPregledCommand(object obj)
        {
            inject.LekarService.IzmeniPregled(lekarGrid, prikazPregleda, listaPregleda, prikazOperacije, listaOperacija,lekarTrenutni);
            inject.LekarService.CollorLekarGrid(lekarGrid);
        }

        public bool CanExecute_IzmeniPregledCommand(object obj)
        {
            return true;
        }
        private RelayCommand informacijeOPacijentuCommand;
        public RelayCommand InformacijeOPacijentuCommand
        {
            get { return informacijeOPacijentuCommand; }
            set
            {
                informacijeOPacijentuCommand = value;

            }
        }

        public void Executed_InformacijeOPacijentuCommand(object obj)
        {
            inject.LekarService.InformacijeOPacijentu(lekarGrid, prikazPregleda, listaPregleda, prikazOperacije, listaOperacija);
        }

        public bool CanExecute_InformacijeOPacijentuCommand(object obj)
        {
            return true;
        }
        private RelayCommand jumpOnButtonEnterCommand;
        public RelayCommand JumpOnButtonEnterCommand
        {
            get { return jumpOnButtonEnterCommand; }
            set
            {
                jumpOnButtonEnterCommand = value;

            }
        }

        public void Executed_JumpOnButtonEnterCommand(object obj)
        {
            inject.LekarService.JumpOnButtonEnter(Zakazi);
        }

        public bool CanExecute_JumpOnButtonEnterCommand(object obj)
        {
            return true;
        }
        private RelayCommand jumpOnButtonLeftCommand;
        public RelayCommand JumpOnButtonLeftCommand
        {
            get { return jumpOnButtonLeftCommand; }
            set
            {
                jumpOnButtonLeftCommand = value;

            }
        }

        public void Executed_JumpOnButtonLeftCommand(object obj)
        {
            inject.LekarService.JumpOnButtonLeft(PreglediTab);
        }

        public bool CanExecute_JumpOnButtonLeftCommand(object obj)
        {
            return true;
        }
        private RelayCommand jumpOnButtonTabCommand;
        public RelayCommand JumpOnButtonTabCommand
        {
            get { return jumpOnButtonTabCommand; }
            set
            {
                jumpOnButtonTabCommand = value;

            }
        }

        public void Executed_JumpOnButtonTabCommand(object obj)
        {
            inject.LekarService.JumpOnButtonTab(lekarGrid);
        }

        public bool CanExecute_JumpOnButtonTabCommand(object obj)
        {
            return true;
        }
        private RelayCommand jumpOnButtonIstorijaEnterCommand;
        public RelayCommand JumpOnButtonIstorijaEnterCommand
        {
            get { return jumpOnButtonIstorijaEnterCommand; }
            set
            {
                jumpOnButtonIstorijaEnterCommand = value;

            }
        }

        public void Executed_JumpOnButtonIstorijaEnterCommand(object obj)
        {
            inject.LekarService.JumpOnButtonIstorijaEnter(AnamnezaIstorijaDugme);
        }

        public bool CanExecute_JumpOnButtonIstorijaEnterCommand(object obj)
        {
            return true;
        }
        private RelayCommand jumpOnButtonIstorijaLeftCommand;
        public RelayCommand JumpOnButtonIstorijaLeftCommand
        {
            get { return jumpOnButtonIstorijaLeftCommand; }
            set
            {
                jumpOnButtonIstorijaLeftCommand = value;

            }
        }

        public void Executed_JumpOnButtonIstorijaLeftCommand(object obj)
        {
            inject.LekarService.JumpOnButtonIstorijaLeft(IstorijaTab);
        }

        public bool CanExecute_JumpOnButtonIstorijaLeftCommand(object obj)
        {
            return true;
        }
        private RelayCommand jumpOnButtonIstorijaTabCommand;
        public RelayCommand JumpOnButtonIstorijaTabCommand
        {
            get { return jumpOnButtonIstorijaTabCommand; }
            set
            {
                jumpOnButtonIstorijaTabCommand = value;

            }
        }

        public void Executed_JumpOnButtonIstorijaTabCommand(object obj)
        {
            inject.LekarService.JumpOnButtonIstorijaTab(lekarGridIstorija);
        }

        public bool CanExecute_JumpOnButtonIstorijaTabCommand(object obj)
        {
            return true;
        }
        private RelayCommand collorLekarGridCommand;
        public RelayCommand CollorLekarGridCommand
        {
            get { return collorLekarGridCommand; }
            set
            {
                collorLekarGridCommand = value;

            }
        }

        public void Executed_CollorLekarGridCommand(object obj)
        {
           // inject.LekarService.CollorLekarGrid(lekarGrid);
        }

        public bool CanExecute_CollorLekarGridCommand(object obj)
        {
            return true;
        }
        private RelayCommand collorLekarGridIstorijaCommand;
        public RelayCommand CollorLekarGridIstorijaCommand
        {
            get { return collorLekarGridIstorijaCommand; }
            set
            {
                collorLekarGridIstorijaCommand = value;

            }
        }

        public void Executed_CollorLekarGridIstorijaCommand(object obj)
        {
          //  inject.LekarService.CollorLekarGridIstorija(lekarGridIstorija);
        }

        public bool CanExecute_CollorLekarGridIstorijaCommand(object obj)
        {
            return true;
        }
        private RelayCommand focusTabCommand;
        public RelayCommand FocusTabCommand
        {
            get { return focusTabCommand; }
            set
            {
                focusTabCommand = value;

            }
        }

        public void Executed_FocusTabCommand(object obj)
        {
            inject.LekarService.focusTab(PreglediTab);
        }

        public bool CanExecute_FocusTabCommand(object obj)
        {
            return true;

        }

        private RelayCommand anamnezaCommand;
        public RelayCommand AnamnezaCommand
        {
            get { return anamnezaCommand; }
            set
            {
                anamnezaCommand = value;

            }
        }

        public void Executed_AnamnezaCommand(object obj)
        {
            inject.LekarService.Anamneza(lekarGrid, prikazPregleda, listaPregleda, prikazOperacije, listaOperacija, lekarTrenutni);
            
        }

        public bool CanExecute_AnamnezaCommand(object obj)
        {
            return true;
        }

        private RelayCommand anamnezaIstorijaCommand;
        public RelayCommand AnamnezaIstorijaCommand
        {
            get { return anamnezaIstorijaCommand; }
            set
            {
                anamnezaIstorijaCommand = value;

            }
        }

        public void Executed_AnamnezaIstorijaCommand(object obj)
        {
            inject.LekarService.AnamnezaIstorija(lekarGrid, prikazPregleda, listaPregleda, prikazOperacije, listaOperacija, lekarTrenutni);
           
        }

        public bool CanExecute_AnamnezaIstorijaCommand(object obj)
        {
            return true;
        }

        private RelayCommand izmeniLekCommand;
        public RelayCommand IzmeniLekCommand
        {
            get { return izmeniLekCommand; }
            set
            {
                izmeniLekCommand = value;

            }
        }

        public void Executed_IzmeniLekCommand(object obj)
        {
            inject.LekarService.IzmeniLek(dataGridLekovi, lekovi);
        }

        public bool CanExecute_IzmeniLekCommand(object obj)
        {
            return true;
        }

        private RelayCommand jumpOnButtonLekEnterCommand;
        public RelayCommand JumpOnButtonLekEnterCommand
        {
            get { return jumpOnButtonLekEnterCommand; }
            set
            {
                jumpOnButtonLekEnterCommand = value;

            }
        }

        public void Executed_JumpOnButtonLekEnterCommand(object obj)
        {
            inject.LekarService.JumpOnButtonLekEnter(OdobriButton);
        }

        public bool CanExecute_JumpOnButtonLekEnterCommand(object obj)
        {
            return true;
        }

        private RelayCommand jumpOnButtonLekLeftCommand;
        public RelayCommand JumpOnButtonLekLeftCommand
        {
            get { return jumpOnButtonLekLeftCommand; }
            set
            {
                jumpOnButtonLekLeftCommand = value;

            }
        }

        public void Executed_JumpOnButtonLekLeftCommand(object obj)
        {
            inject.LekarService.JumpOnButtonLekLeft(LekTab);
        }

        public bool CanExecute_JumpOnButtonLekLeftCommand(object obj)
        {
            return true;
        }

        private RelayCommand jumpOnButtonLekTabCommand;
        public RelayCommand JumpOnButtonLekTabCommand
        {
            get { return jumpOnButtonLekTabCommand; }
            set
            {
                jumpOnButtonLekTabCommand = value;

            }
        }

        public void Executed_JumpOnButtonLekTabCommand(object obj)
        {
            inject.LekarService.JumpOnButtonLekTab(dataGridLekovi);
        }

        public bool CanExecute_JumpOnButtonLekTabCommand(object obj)
        {
            return true;
        }

        private RelayCommand odobriLekCommand;
        public RelayCommand OdobriLekCommand
        {
            get { return odobriLekCommand; }
            set
            {
                odobriLekCommand = value;

            }
        }

        public void Executed_OdobriLekCommand(object obj)
        {
            inject.LekarService.OdobriLek(dataGridLekovi, lekovi, lekoviPrikaz);
        }

        public bool CanExecute_OdobriLekCommand(object obj)
        {
            return true;
        }

        private RelayCommand vratiNaIzmenuCommand;
        public RelayCommand VratiNaIzmenuCommand
        {
            get { return vratiNaIzmenuCommand; }
            set
            {
                vratiNaIzmenuCommand = value;

            }
        }

        public void Executed_VratiNaIzmenuCommand(object obj)
        {
            inject.LekarService.VratiNaIzmenu(dataGridLekovi);
        }

        public bool CanExecute_VratiNaIzmenuCommand(object obj)
        {
            return true;
        }

        



        public LekarViewModel(Lekar ln)
        {
            Inject = new Injector();
           
            
            

            lekovi = sviLekovi.GetAll();

            for (int i = 0; i < lekovi.Count; i++)
            {
                if (lekovi[i].Status.Equals(StatusLeka.odbijen) || lekovi[i].Obrisan)
                {
                    lekovi.RemoveAt(i);
                    i--;
                }
            }

            lekarTrenutni = ln;
            listaLekara = sviLekari.GetAll();



            listaPregleda = sviPregledi.GetAll();
            listaOperacija = sviPregledi.GetAllOperacije();
            listaPacijenata = sviPacijenti.GetAll();
            listaProstorija = sveProstorije.GetAllProstorije();


            for (int l = 0; l < listaPregleda.Count; l++)
            {
                if (!listaPregleda[l].Lekar.Jmbg.Equals(lekarTrenutni.Jmbg))
                {
                    listaPregleda.RemoveAt(l);
                    l = l - 1;
                }
            }
            for (int l = 0; l < listaOperacija.Count; l++)
            {
                if (!listaOperacija[l].Lekar.Jmbg.Equals(lekarTrenutni.Jmbg))
                {
                    listaOperacija.RemoveAt(l);
                    l = l - 1;
                }
            }
           

            
           
            ZakaziPregledCommand = new RelayCommand(Executed_ZakaziPregledCommand, CanExecute_ZakaziPregledCommand);
            OtkaziPregledCommand = new RelayCommand(Executed_OtkaziPregledCommand, CanExecute_OtkaziPregledCommand);
            IzmeniPregledCommand = new RelayCommand(Executed_IzmeniPregledCommand, CanExecute_IzmeniPregledCommand);
            InformacijeOPacijentuCommand = new RelayCommand(Executed_InformacijeOPacijentuCommand, CanExecute_InformacijeOPacijentuCommand);
            JumpOnButtonEnterCommand = new RelayCommand(Executed_JumpOnButtonEnterCommand, CanExecute_JumpOnButtonEnterCommand);
            JumpOnButtonLeftCommand = new RelayCommand(Executed_JumpOnButtonLeftCommand, CanExecute_JumpOnButtonLeftCommand);
            JumpOnButtonTabCommand = new RelayCommand(Executed_JumpOnButtonTabCommand, CanExecute_JumpOnButtonTabCommand );
            JumpOnButtonIstorijaEnterCommand = new RelayCommand(Executed_JumpOnButtonIstorijaEnterCommand, CanExecute_JumpOnButtonIstorijaEnterCommand);
            JumpOnButtonIstorijaLeftCommand = new RelayCommand(Executed_JumpOnButtonIstorijaLeftCommand, CanExecute_JumpOnButtonIstorijaLeftCommand);
            JumpOnButtonIstorijaTabCommand = new RelayCommand(Executed_JumpOnButtonIstorijaTabCommand, CanExecute_JumpOnButtonIstorijaTabCommand);
            CollorLekarGridCommand = new RelayCommand(Executed_CollorLekarGridCommand, CanExecute_CollorLekarGridCommand);
            CollorLekarGridIstorijaCommand = new RelayCommand(Executed_CollorLekarGridIstorijaCommand, CanExecute_CollorLekarGridIstorijaCommand);
            FocusTabCommand = new RelayCommand(Executed_FocusTabCommand, CanExecute_FocusTabCommand);
            AnamnezaCommand = new RelayCommand(Executed_AnamnezaCommand, CanExecute_AnamnezaCommand);
            AnamnezaIstorijaCommand = new RelayCommand(Executed_AnamnezaIstorijaCommand, CanExecute_AnamnezaIstorijaCommand);
            IzmeniLekCommand = new RelayCommand(Executed_IzmeniLekCommand, CanExecute_IzmeniLekCommand);
            JumpOnButtonLekEnterCommand = new RelayCommand(Executed_JumpOnButtonLekEnterCommand, CanExecute_JumpOnButtonLekEnterCommand);
            JumpOnButtonLekLeftCommand = new RelayCommand(Executed_JumpOnButtonLekLeftCommand, CanExecute_JumpOnButtonLekLeftCommand);
            JumpOnButtonLekTabCommand = new RelayCommand(Executed_JumpOnButtonLekTabCommand, CanExecute_JumpOnButtonLekTabCommand);
            OdobriLekCommand = new RelayCommand(Executed_OdobriLekCommand, CanExecute_OdobriLekCommand);
            VratiNaIzmenuCommand = new RelayCommand(Executed_VratiNaIzmenuCommand, CanExecute_VratiNaIzmenuCommand);
            
            



        }
        public void Popuni(DataGrid lekarGridd, DataGrid lekarGridIstorijaa, DataGrid dataGridLekovii)
        {
            lekarGrid = lekarGridd;
            lekarGridIstorija = lekarGridIstorijaa;
            dataGridLekovi = dataGridLekovii;
            dataListIstorija.Items.SortDescriptions.Clear();
            dataListIstorija.Items.SortDescriptions.Add(new SortDescription("Datum", ListSortDirection.Descending));
            dataList.Items.SortDescriptions.Clear();
            dataList.Items.SortDescriptions.Add(new SortDescription("Datum", ListSortDirection.Ascending));
            for (int i = 0; i < listaPregleda.Count; i++)
            {
                if (listaPregleda[i].Zavrsen.Equals(false))
                {
                    prikazPregleda = new PrikazPregleda();
                    prikazPregleda.Id = listaPregleda[i].Id;
                    prikazPregleda.Trajanje = listaPregleda[i].Trajanje;
                    prikazPregleda.Zavrsen = listaPregleda[i].Zavrsen;
                    prikazPregleda.Datum = listaPregleda[i].Datum;
                    prikazPregleda.Anamneza = listaPregleda[i].Anamneza;
                    prikazPregleda.Hitan = listaPregleda[i].Hitan;
                    for (int p = 0; p < listaPacijenata.Count; p++)
                    {
                        if (listaPregleda[i].Pacijent.Jmbg.Equals(listaPacijenata[p].Jmbg) && listaPacijenata[p].Obrisan == false)
                        {
                            prikazPregleda.Pacijent = listaPacijenata[p];
                            break;
                        }


                    }
                    for (int p = 0; p < listaProstorija.Count; p++)
                    {
                        if (listaPregleda[i].Prostorija.BrojProstorije.Equals(listaProstorija[p].BrojProstorije) && listaProstorija[p].Obrisana == false)
                        {
                            prikazPregleda.Prostorija = listaProstorija[p];
                            break;
                        }
                    }
                    for (int p = 0; p < listaLekara.Count; p++)
                    {
                        if (listaPregleda[i].Lekar.Jmbg.Equals(listaLekara[p].Jmbg))
                        {
                            prikazPregleda.Lekar = listaLekara[p];
                        }
                    }
                    dataList.Items.Add(prikazPregleda);
                }
                else
                {
                    prikazPregleda = new PrikazPregleda();
                    prikazPregleda.Id = listaPregleda[i].Id;
                    prikazPregleda.Trajanje = listaPregleda[i].Trajanje;
                    prikazPregleda.Zavrsen = listaPregleda[i].Zavrsen;
                    prikazPregleda.Datum = listaPregleda[i].Datum;
                    prikazPregleda.Anamneza = listaPregleda[i].Anamneza;
                    prikazPregleda.Hitan = listaPregleda[i].Hitan;
                    for (int p = 0; p < listaPacijenata.Count; p++)
                    {
                        if (listaPregleda[i].Pacijent.Jmbg.Equals(listaPacijenata[p].Jmbg) && listaPacijenata[p].Obrisan == false)
                        {
                            prikazPregleda.Pacijent = listaPacijenata[p];
                            break;
                        }


                    }
                    for (int p = 0; p < listaProstorija.Count; p++)
                    {
                        if (listaPregleda[i].Prostorija.BrojProstorije.Equals(listaProstorija[p].BrojProstorije) && listaProstorija[p].Obrisana == false)
                        {
                            prikazPregleda.Prostorija = listaProstorija[p];
                            break;
                        }
                    }
                    for (int p = 0; p < listaLekara.Count; p++)
                    {
                        if (listaPregleda[i].Lekar.Jmbg.Equals(listaLekara[p].Jmbg))
                        {
                            prikazPregleda.Lekar = listaLekara[p];
                            break;
                        }
                    }
                    dataListIstorija.Items.Add(prikazPregleda);
                }
            }
            for (int i = 0; i < listaOperacija.Count; i++)
            {
                if (listaOperacija[i].Zavrsen.Equals(false))
                {
                    prikazOperacije = new PrikazOperacije();
                    prikazOperacije.Id = listaOperacija[i].Id;
                    prikazOperacije.Trajanje = listaOperacija[i].Trajanje;
                    prikazOperacije.Zavrsen = listaOperacija[i].Zavrsen;
                    prikazOperacije.Datum = listaOperacija[i].Datum;
                    prikazOperacije.Anamneza = listaOperacija[i].Anamneza;
                    prikazOperacije.TipOperacije = listaOperacija[i].TipOperacije;
                    prikazOperacije.Hitan = listaOperacija[i].Hitan;
                    for (int p = 0; p < listaPacijenata.Count; p++)
                    {
                        if (listaOperacija[i].Pacijent.Jmbg.Equals(listaPacijenata[p].Jmbg) && listaPacijenata[p].Obrisan == false)
                        {
                            prikazOperacije.Pacijent = listaPacijenata[p];
                            break;
                        }


                    }
                    for (int p = 0; p < listaProstorija.Count; p++)
                    {
                        if (listaOperacija[i].Prostorija.BrojProstorije.Equals(listaProstorija[p].BrojProstorije) && listaProstorija[p].Obrisana == false)
                        {
                            prikazOperacije.Prostorija = listaProstorija[p];
                            break;
                        }
                    }
                    for (int p = 0; p < listaLekara.Count; p++)
                    {
                        if (listaOperacija[i].Lekar.Jmbg.Equals(listaLekara[p].Jmbg))
                        {
                            prikazOperacije.Lekar = listaLekara[p];
                            break;
                        }
                    }
                    dataList.Items.Add(prikazOperacije);
                }
                else
                {
                    prikazOperacije = new PrikazOperacije();
                    prikazOperacije.Id = listaOperacija[i].Id;
                    prikazOperacije.Trajanje = listaOperacija[i].Trajanje;
                    prikazOperacije.Zavrsen = listaOperacija[i].Zavrsen;
                    prikazOperacije.Datum = listaOperacija[i].Datum;
                    prikazOperacije.Anamneza = listaOperacija[i].Anamneza;
                    prikazOperacije.TipOperacije = listaOperacija[i].TipOperacije;
                    prikazOperacije.Hitan = listaOperacija[i].Hitan;
                    for (int p = 0; p < listaPacijenata.Count; p++)
                    {
                        if (listaOperacija[i].Pacijent.Jmbg.Equals(listaPacijenata[p].Jmbg) && listaPacijenata[p].Obrisan == false)
                        {
                            prikazOperacije.Pacijent = listaPacijenata[p];
                            break;
                        }


                    }
                    for (int p = 0; p < listaProstorija.Count; p++)
                    {
                        if (listaOperacija[i].Prostorija.BrojProstorije.Equals(listaProstorija[p].BrojProstorije) && listaProstorija[p].Obrisana == false)
                        {
                            prikazOperacije.Prostorija = listaProstorija[p];
                            break;
                        }
                    }
                    for (int p = 0; p < listaLekara.Count; p++)
                    {
                        if (listaOperacija[i].Lekar.Jmbg.Equals(listaLekara[p].Jmbg))
                        {
                            prikazOperacije.Lekar = listaLekara[p];
                            break;
                        }
                    }
                    dataListIstorija.Items.Add(prikazOperacije);
                }
            }
            data();
            dataIstorija();

            lekarGrid.ItemsSource = dataList.Items;
            lekarGridIstorija.ItemsSource = dataListIstorija.Items;
            for (int i = 0; i < lekovi.Count; i++)
            {
                if (!lekovi[i].Status.Equals(StatusLeka.odbijen))
                {
                    PrikazLek p = new PrikazLek();
                    p.Id = lekovi[i].Id;
                    p.KolicinaUMg = lekovi[i].KolicinaUMg;
                    p.Naziv = lekovi[i].Naziv;
                    p.Status = lekovi[i].Status;
                    p.Proizvodjac = lekovi[i].Proizvodjac;
                    string l = "";
                    FileRepositorySastojak storageSastojak = new FileRepositorySastojak();
                    for (int m = 0; m < lekovi[i].Sastojak.Count; m++)
                    {
                        foreach (Sastojak s in storageSastojak.GetAll())
                        {
                            if (m == 0)
                            {
                                if (lekovi[i].Sastojak[m].Id == s.Id)
                                    l = l + " " + s.Naziv;
                            }
                            else
                            {
                                if (lekovi[i].Sastojak[m].Id == s.Id)
                                    l = l + ", " + s.Naziv;
                            }
                        }
                    }
                    string h = "";
                    for (int m = 0; m < lekovi[i].IdZamena.Count; m++)
                    {
                        Lek novi = new Lek();
                        for (int mo = 0; mo < lekovi.Count; mo++)
                        {
                            if (lekovi[i].IdZamena[m].Equals(lekovi[mo].Id))
                            {
                                novi = lekovi[mo];
                                break;
                            }
                        }
                        if (m == 0)
                        {
                            h = h + " " + novi.Naziv;
                        }
                        else
                        {
                            h = h + ", " + novi.Naziv;
                        }
                    }
                    p.Sastojak = l;
                    p.Zamena = h;
                    lekoviPrikaz.Add(p);
                }
            }
            dataGridLekovi.ItemsSource = lekoviPrikaz;
           
           
        }

        public void PodesiParametre(Button Zakazi,TabItem PreglediTab,TabItem IstorijaTab,TabItem LekTab,Button AnamenzaIstorijaDugme,Button Odobri)
        {
            this.Zakazi = Zakazi;
            this.PreglediTab = PreglediTab;
            this.IstorijaTab = IstorijaTab;
            this.LekTab = LekTab;
            this.AnamnezaIstorijaDugme = AnamenzaIstorijaDugme;
            this.OdobriButton = Odobri;
           
        }
        public static void data()
        {
            dataList.Items.Refresh();
        }
        public static void dataIstorija()
        {
            dataListIstorija.Items.Refresh();
        }
        public void Refresh()
        {
            lekarGrid.Items.Refresh();
        }
    }
}
