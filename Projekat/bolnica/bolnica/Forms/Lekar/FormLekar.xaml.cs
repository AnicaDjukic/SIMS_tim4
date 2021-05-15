using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.ViewModel;
using Model.Korisnici;
using Model.Pacijenti;
using Model.Pregledi;
using Model.Prostorije;



namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormLekar.xaml
    /// </summary>

    public partial class FormLekar : Window
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
        private FileStoragePregledi sviPregledi = new FileStoragePregledi();
        private FileStoragePacijenti sviPacijenti = new FileStoragePacijenti();
        private FileStorageProstorija sveProstorije = new FileStorageProstorija();
        private FileStorageLekar sviLekari = new FileStorageLekar();
        private FileStorageLek sviLekovi = new FileStorageLek();
        private List<Pacijent> listaPacijenata = new List<Pacijent>();
        private List<Prostorija> listaProstorija = new List<Prostorija>();
        private PrikazPregleda prikazPregleda = new PrikazPregleda();
        private PrikazOperacije prikazOperacije = new PrikazOperacije();
        private List<Lek> lekovi = new List<Lek>();

        public FormLekar(Lekar ln)
        {
            InitializeComponent();
            //WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //Owner = Application.Current.MainWindow;
            //this.WindowState = WindowState.Maximized;
            Application.Current.MainWindow = this;

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



            listaPregleda = sviPregledi.GetAllPregledi();
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
            dataListIstorija.AddingNewItem += dataListAddingNewItemEventArgs;
            dataList.AddingNewItem += dataListAddingNewItemEventArgs;

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
                    FileStorageSastojak storageSastojak = new FileStorageSastojak();
                    for (int m = 0; m < lekovi[i].Sastojak.Count; m++)
                    {
                        foreach (Sastojak s in storageSastojak.GetAll()) {
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

        private void ZakaziPregled(object sender, RoutedEventArgs e)
        {
            ZakaziPregled();
        }

        public void ZakaziPregled()
        {
            IzmeniINapraviTerminLekarViewModel vm = new IzmeniINapraviTerminLekarViewModel(lekarTrenutni);
            FormNapraviTerminLekar ff = new FormNapraviTerminLekar(vm);
        }

        private void OtkaziPregled(object sender, RoutedEventArgs e)
        {

            OtkaziPregled();


        }

        public void OtkaziPregled()
        {
            if (lekarGrid.SelectedCells.Count > 0)
            {
                var objekat = lekarGrid.SelectedValue;
                if (objekat.GetType().Equals(prikazPregleda.GetType()))
                {
                    PrikazPregleda pri = objekat as PrikazPregleda;
                    for (int i = 0; i < listaPregleda.Count; i++)
                    {
                        if (pri.Id.Equals(listaPregleda[i].Id))
                        {

                            sviPregledi.Delete(listaPregleda[i]);
                            listaPregleda.RemoveAt(i);
                            break;
                        }
                    }
                }
                else if (objekat.GetType().Equals(prikazOperacije.GetType()))
                {
                    PrikazPregleda pri = objekat as PrikazOperacije;
                    for (int i = 0; i < listaOperacija.Count; i++)
                    {
                        if (pri.Id.Equals(listaOperacija[i].Id))
                        {
                            sviPregledi.Delete(listaOperacija[i]);
                            listaOperacija.RemoveAt(i);
                            break;
                        }
                    }
                }
                int index = lekarGrid.SelectedIndex;
                dataList.Items.RemoveAt(index);
                data();
            }

        }

        private void IzmeniPregled(object sender, RoutedEventArgs e)
        {
            IzmeniPregled();

        }

        public void IzmeniPregled()
        {
            if (lekarGrid.SelectedCells.Count > 0)
            {

                var objekat = lekarGrid.SelectedValue;
                PrikazPregleda p1 = new PrikazPregleda();
                PrikazOperacije op = new PrikazOperacije();
                p1.Pacijent = new Pacijent();
                op.Pacijent = new Pacijent();

                if (objekat.GetType().Equals(prikazPregleda.GetType()))
                {
                    PrikazPregleda pri = objekat as PrikazPregleda;
                    for (int i = 0; i < listaPregleda.Count; i++)
                    {
                        if (pri.Id.Equals(listaPregleda[i].Id))
                        {

                            p1 = lekarGrid.SelectedItem as PrikazPregleda;
                            IzmeniINapraviTerminLekarViewModel vm = new IzmeniINapraviTerminLekarViewModel(p1,lekarTrenutni);
                            FormIzmeniTerminLekar ff = new FormIzmeniTerminLekar(vm);
                            break;
                        }
                    }
                }
                else if (objekat.GetType().Equals(prikazOperacije.GetType()))
                {
                    if (!lekarTrenutni.Specijalizacija.OblastMedicine.Equals("Opsta"))
                    {
                        PrikazOperacije pri = objekat as PrikazOperacije;
                        for (int i = 0; i < listaOperacija.Count; i++)
                        {
                            if (pri.Id.Equals(listaOperacija[i].Id))
                            {

                                op = lekarGrid.SelectedItem as PrikazOperacije;
                                IzmeniINapraviTerminLekarViewModel vm = new IzmeniINapraviTerminLekarViewModel(op,lekarTrenutni);
                                FormIzmeniTerminLekar ff = new FormIzmeniTerminLekar(vm);
                                break;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nemate ovlastenje da menjate operacije");
                    }
                }


            }

        }

        public void Refresh()
        {
            lekarGrid.Items.Refresh();
        }

        public static void data()
        {
            dataList.Items.Refresh();
        }
        public static void dataIstorija()
        {
            dataListIstorija.Items.Refresh();
        }
        private void dataListAddingNewItemEventArgs(object sender, AddingNewItemEventArgs e)
        {
            lekarGrid.Items.Refresh();
            lekarGridIstorija.Items.Refresh();
        }

        private void InformacijeOPacijentu(object sender, RoutedEventArgs e)
        {
            InformacijeOPacijentu();

        }

        public void InformacijeOPacijentu()
        {
            if (lekarGrid.SelectedCells.Count > 0)
            {

                var objekat = lekarGrid.SelectedValue;
                PrikazPregleda p1 = new PrikazPregleda();
                PrikazOperacije op = new PrikazOperacije();

                if (objekat.GetType().Equals(prikazPregleda.GetType()))
                {
                    PrikazPregleda pri = objekat as PrikazPregleda;
                    for (int i = 0; i < listaPregleda.Count; i++)
                    {
                        if (pri.Id.Equals(listaPregleda[i].Id))
                        {

                            p1 = lekarGrid.SelectedItem as PrikazPregleda;
                            FormPrikazInformacijaOPacijentuLekar forma = new FormPrikazInformacijaOPacijentuLekar(p1.Pacijent);
                            forma.Show();

                            break;
                        }
                    }
                }
                else if (objekat.GetType().Equals(prikazOperacije.GetType()))
                {
                    PrikazOperacije pri = objekat as PrikazOperacije;
                    for (int i = 0; i < listaOperacija.Count; i++)
                    {
                        if (pri.Id.Equals(listaOperacija[i].Id))
                        {
                            op = lekarGrid.SelectedItem as PrikazOperacije;
                            FormPrikazInformacijaOPacijentuLekar forma = new FormPrikazInformacijaOPacijentuLekar(op.Pacijent);
                            forma.Show();
                            break;
                        }
                    }
                }

            }
        }

        private void JumpOnButton(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                Zakazi.Focus();

            }

            if (e.Key == Key.Left)
            {
                e.Handled = true;
                PreglediTab.Focus();
            }



            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                var row = lekarGrid.SelectedIndex;
                if (row < lekarGrid.Items.Count - 1)
                {
                    row = row + 1;
                    lekarGrid.SelectedIndex = row;


                }
                else
                {
                    row = 0;
                    lekarGrid.SelectedIndex = row;
                }
            }


        }

        private void JumpOnButtonIstorija(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                AnamnezaIstorijaDugme.Focus();
            }

            if (e.Key == Key.Left)
            {
                e.Handled = true;
                IstorijaTab.Focus();
            }



            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                var row = lekarGridIstorija.SelectedIndex;
                if (row < lekarGridIstorija.Items.Count - 1)
                {
                    row = row + 1;
                    lekarGridIstorija.SelectedIndex = row;


                }
                else
                {
                    row = 0;
                    lekarGridIstorija.SelectedIndex = row;
                }
            }


        }

        public void CollorLekarGrid()
        {
            DateTime trenutni = new DateTime();
            int dozvola = 0;
            PrikazPregleda preg = new PrikazPregleda();
            PrikazOperacije oper = new PrikazOperacije();

            for (int i = 0; i < lekarGrid.Items.Count; i++)
            {

                var row = (DataGridRow)lekarGrid.ItemContainerGenerator.ContainerFromIndex(i);
                if (row != null)
                {


                    var Objekat = row.Item;
                    if (Objekat.GetType().Equals(preg.GetType()))
                    {
                        preg = Objekat as PrikazPregleda;
                        if (trenutni.Date != preg.Datum.Date)
                        {
                            trenutni = preg.Datum;
                            dozvola++;
                            if (dozvola == 2)
                            {
                                dozvola = 0;
                            }
                        }
                    }
                    else if (Objekat.GetType().Equals(oper.GetType()))
                    {
                        oper = Objekat as PrikazOperacije;
                        if (trenutni.Date != oper.Datum.Date)
                        {
                            trenutni = oper.Datum;
                            dozvola++;
                            if (dozvola == 2)
                            {
                                dozvola = 0;
                            }
                        }
                    }

                    if (dozvola == 0)
                    {
                        row.Foreground = Brushes.Black;
                    }
                    else if (dozvola == 1)
                    {
                        row.Foreground = Brushes.DarkViolet;
                    }



                }
            }

        }

        public void CollorLekarGridIstorija()
        {

            DateTime trenutni = new DateTime();
            int dozvola = 0;
            PrikazPregleda preg = new PrikazPregleda();
            PrikazOperacije oper = new PrikazOperacije();

            for (int i = 0; i < lekarGridIstorija.Items.Count + 1; i++)
            {

                var row = (DataGridRow)lekarGridIstorija.ItemContainerGenerator.ContainerFromIndex(i);
                if (row != null)
                {


                    var Objekat = row.Item;
                    if (Objekat.GetType().Equals(preg.GetType()))
                    {
                        preg = Objekat as PrikazPregleda;
                        if (trenutni.Date != preg.Datum.Date)
                        {
                            trenutni = preg.Datum;
                            dozvola++;
                            if (dozvola == 2)
                            {
                                dozvola = 0;
                            }
                        }
                    }
                    else if (Objekat.GetType().Equals(oper.GetType()))
                    {
                        oper = Objekat as PrikazOperacije;
                        if (trenutni.Date != oper.Datum.Date)
                        {
                            trenutni = oper.Datum;
                            dozvola++;
                            if (dozvola == 2)
                            {
                                dozvola = 0;
                            }
                        }
                    }

                    if (dozvola == 0)
                    {
                        row.Foreground = Brushes.Black;
                    }
                    else if (dozvola == 1)
                    {
                        row.Foreground = Brushes.DarkViolet;
                    }



                }
            }
        }



        private void Collor(object sender, RoutedEventArgs e)
        {
            CollorLekarGrid();
        }

        private void CollorIstorija(object sender, RoutedEventArgs e)
        {
            CollorLekarGridIstorija();
        }



        private void focusTab(object sender, RoutedEventArgs e)
        {
            PreglediTab.Focus();
        }

        private void Anamneza(object sender, RoutedEventArgs e)
        {
            Anamneza();
        }

        public void Anamneza()
        {
            if (lekarGrid.SelectedCells.Count > 0)
            {

                var objekat = lekarGrid.SelectedValue;
                PrikazPregleda p1 = new PrikazPregleda();
                PrikazOperacije op = new PrikazOperacije();

                if (objekat.GetType().Equals(prikazPregleda.GetType()))
                {
                    PrikazPregleda pri = objekat as PrikazPregleda;
                    if (pri.Datum < DateTime.Now)
                    {
                        for (int i = 0; i < listaPregleda.Count; i++)
                        {
                            if (pri.Id.Equals(listaPregleda[i].Id))
                            {

                                p1 = lekarGrid.SelectedItem as PrikazPregleda;
                                FormNapraviAnamnezuLekar form = new FormNapraviAnamnezuLekar(p1, lekarTrenutni);
                                form.Show();
                                break;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Pregled nije poceo");
                        return;
                    }


                }
                if (objekat.GetType().Equals(prikazOperacije.GetType()))
                {
                    PrikazOperacije pri = objekat as PrikazOperacije;
                    if (pri.Datum < DateTime.Now)
                    {
                        for (int i = 0; i < listaOperacija.Count; i++)
                        {
                            if (pri.Id.Equals(listaOperacija[i].Id))
                            {

                                op = lekarGrid.SelectedItem as PrikazOperacije;

                                FormNapraviAnamnezuLekar form = new FormNapraviAnamnezuLekar(op, lekarTrenutni);
                                form.Show();
                                break;
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Operacija nije pocela");
                        return;
                    }

                }

            }
        }

        

        private void AnamnezaIstorija(object sender, RoutedEventArgs e)
        {
            AnamnezaIstorija();
        }

        public void AnamnezaIstorija()
        {
            if (lekarGridIstorija.SelectedCells.Count > 0)
            {

                var objekat = lekarGridIstorija.SelectedValue;
                PrikazPregleda p1 = new PrikazPregleda();
                PrikazOperacije op = new PrikazOperacije();

                if (objekat.GetType().Equals(prikazPregleda.GetType()))
                {
                    PrikazPregleda pri = objekat as PrikazPregleda;

                    for (int i = 0; i < listaPregleda.Count; i++)
                    {
                        if (pri.Id.Equals(listaPregleda[i].Id))
                        {

                            p1 = lekarGridIstorija.SelectedItem as PrikazPregleda;
                            FormNapraviAnamnezuLekar form = new FormNapraviAnamnezuLekar(p1, lekarTrenutni);
                            form.Show();
                            break;
                        }
                    }

                }
                if (objekat.GetType().Equals(prikazOperacije.GetType()))
                {
                    PrikazOperacije pri = objekat as PrikazOperacije;

                    for (int i = 0; i < listaOperacija.Count; i++)
                    {
                        if (pri.Id.Equals(listaOperacija[i].Id))
                        {

                            op = lekarGridIstorija.SelectedItem as PrikazOperacije;

                            FormNapraviAnamnezuLekar form = new FormNapraviAnamnezuLekar(op, lekarTrenutni);
                            form.Show();
                            break;
                        }
                    }

                }


            }
        }

        private void IzmeniLek(object sender, RoutedEventArgs e)
        {
            IzmeniLek();
            
        }

        public void IzmeniLek()
        {
            PrikazLek p = dataGridLekovi.SelectedItem as PrikazLek;
            lekovi = sviLekovi.GetAll();

            for (int i = 0; i < lekovi.Count; i++)
            {
                if (lekovi[i].Status.Equals(StatusLeka.odbijen) || lekovi[i].Obrisan)
                {
                    lekovi.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < lekovi.Count; i++)
            {
                if (lekovi[i].Id.Equals(p.Id))
                {
                    FormIzmeniLekLekar form = new FormIzmeniLekLekar(lekovi[i]);
                    form.Show();
                    break;

                }
            }
        }

        private void JumpOnButtonLek(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                Odobri.Focus();

            }

            if (e.Key == Key.Left)
            {
                e.Handled = true;
                lekTab.Focus();
            }



            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                var row = dataGridLekovi.SelectedIndex;
                if (row < dataGridLekovi.Items.Count - 1)
                {
                    row = row + 1;
                    dataGridLekovi.SelectedIndex = row;


                }
                else
                {
                    row = 0;
                    dataGridLekovi.SelectedIndex = row;
                }
            }


        }

        private void OdobriLek(object sender, RoutedEventArgs e)
        {
            OdobriLek();
        }

        public void OdobriLek()
        {
            PrikazLek p = dataGridLekovi.SelectedItem as PrikazLek;
            
            if (p.Status.Equals(StatusLeka.cekaValidaciju))
            {
                lekovi = sviLekovi.GetAll();

                for (int i = 0; i < lekovi.Count; i++)
                {
                    if (lekovi[i].Status.Equals(StatusLeka.odbijen) || lekovi[i].Obrisan)
                    {
                        lekovi.RemoveAt(i);
                        i--;
                    }
                }
                for (int i = 0; i < lekovi.Count; i++)
                {
                    if (lekovi[i].Id.Equals(p.Id))
                    {
                        lekovi[i].Status = StatusLeka.odobren;
                        sviLekovi.Delete(lekovi[i]);
                        sviLekovi.Save(lekovi[i]);
                        break;

                    }
                }
                for (int i = 0; i < lekoviPrikaz.Count; i++)
                {
                    if (lekoviPrikaz[i].Id.Equals(p.Id))
                    {
                        lekoviPrikaz[i].Status = StatusLeka.odobren;
                        dataGridLekovi.Items.Refresh();
                        break;

                    }
                }
                Obavestenje obavestenje = new Obavestenje();
                FileStorageObavestenja svaObavestenja = new FileStorageObavestenja();
                FileStorageKorisnici sviKorisnici = new FileStorageKorisnici();
                List<Korisnik> svi = sviKorisnici.GetAll();
                List<Obavestenje> sva = svaObavestenja.GetAll();
                int max = 0;
                for (int i = 0; i < sva.Count; i++)
                {
                    if (max < sva[i].Id)
                    {
                        max = sva[i].Id;
                    }
                }
                max = max + 1;
                obavestenje.Id = max;

                for (int i = 0; i < svi.Count; i++)
                {
                    if (svi[i].TipKorisnika.Equals(TipKorisnika.upravnik))
                    {
                        obavestenje.Korisnici.Add(svi[i]);
                    }
                }
                obavestenje.Naslov = "Lek " + p.Naziv + " je prihvacen";
                obavestenje.Obrisan = false;
                obavestenje.Sadrzaj = "Lek " + p.Naziv + " sa dozom " + p.KolicinaUMg + " i sastojcima: " + p.Sastojak + " je prihvacen. ";
                obavestenje.Datum = DateTime.Now;
                FileStorageObavestenja oba = new FileStorageObavestenja();
                oba.Save(obavestenje);


            }
            else
            {
                MessageBox.Show("Lek je vec odobren");
            }
        }

        private void VratiNaIzmenu(object sender, RoutedEventArgs e)
        {
            VratiNaIzmenu();      
            
        }

        public void VratiNaIzmenu()
        {
            PrikazLek p = dataGridLekovi.SelectedItem as PrikazLek;
            if (p.Status.Equals(StatusLeka.cekaValidaciju))
            {
                FormKomentarLekaLekar lek = new FormKomentarLekaLekar(p);
                lek.Show();
            }
            else
            {
                MessageBox.Show("Niste odabrali lek koji ceka validaciju");
            }
        }

        private void isPreglediTab(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.IsKeyDown(Key.LeftCtrl) || e.KeyboardDevice.IsKeyDown(Key.RightCtrl))
            {
                switch (e.Key)
                {
                    case Key.P:
                        ZakaziPregled();
                        break;
                    case Key.O:
                        OtkaziPregled();
                        break;
                    case Key.H:
                        IzmeniPregled();
                        break;
                    case Key.N:
                        Anamneza();
                        break;
                    case Key.I:
                        InformacijeOPacijentu();
                        break;
                }
            }
        }

        private void isIstorijaTab(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.IsKeyDown(Key.LeftCtrl) || e.KeyboardDevice.IsKeyDown(Key.RightCtrl))
            {
                switch (e.Key)
                {
                    
                    case Key.N:
                        AnamnezaIstorija();
                        break;
                    
                }
            }

        }

        private void isLekTab(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.IsKeyDown(Key.LeftCtrl) || e.KeyboardDevice.IsKeyDown(Key.RightCtrl))
            {
                switch (e.Key)
                {
                    case Key.P:
                        OdobriLek();
                        break;
                    case Key.O:
                        VratiNaIzmenu();
                        break;
                    case Key.H:
                        IzmeniLek();
                        break;
                    
                }
            }
        }
    }
}
