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
        private List<Pacijent> listaPacijenata = new List<Pacijent>();
        private List<Prostorija> listaProstorija = new List<Prostorija>();
        private PrikazPregleda prikazPregleda = new PrikazPregleda();
        private PrikazOperacije prikazOperacije = new PrikazOperacije();
        private List<Lek> lekovi = new List<Lek>();
        
        
       




        public FormLekar()
        {
            InitializeComponent();
            //WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //Owner = Application.Current.MainWindow;
            //this.WindowState = WindowState.Maximized;
            Application.Current.MainWindow = this;
            Sastojak a = new Sastojak();
            Sastojak b = new Sastojak();
            Sastojak c = new Sastojak();
            Sastojak d = new Sastojak();
            a.Naziv = "aaaaaaa";
            b.Naziv = "bbbbbbb";
            c.Naziv = "Aspirin";
            d.Naziv = "Cliacil";
            c.Id = 1; 
            Lek l11 = new Lek();
            Lek l22 = new Lek();
            l11.Naziv = "Aspirin";
            l11.Status = StatusLeka.Odobren;
            l11.Id = 1;
            l11.Proizvodjac = "Google";
            l11.KolicinaUMg = 200;
            l11.ZamenaId = new List<int>();
            l22.Naziv = "Brufen";
            l22.Status = StatusLeka.CekaValidaciju;
            l22.Id = 2;
            l22.KolicinaUMg = 300;
            l22.Proizvodjac = "Amazon";
            l22.ZamenaId = new List<int>();
            Lek l3 = new Lek();
            l3.Id = 3;
            l3.Naziv = "Aspirin";
            l3.KolicinaUMg = 300;
            l3.Status = StatusLeka.Odbijen;
            l3.Proizvodjac = "Masina";
            l3.ZamenaId = new List<int>();
            Lek l4 = new Lek();
            l4.Id = 4;
            l4.Naziv = "Andol";
            l4.KolicinaUMg = 200;
            l4.Status = StatusLeka.Odobren;
            l4.Proizvodjac = "Masina";
            l4.ZamenaId = new List<int>();
            l22.Sastojak.Add(a);
            l3.Sastojak.Add(a);
            l4.Sastojak.Add(a);
            l22.Sastojak.Add(b);
            l3.Sastojak.Add(b);
            l4.Sastojak.Add(b);
            l11.Sastojak.Add(c);
            l22.Sastojak.Add(c);
            l3.Sastojak.Add(c);
            l4.Sastojak.Add(c);
            l11.Sastojak.Add(d);
            l22.Sastojak.Add(d);
            l3.Sastojak.Add(d);
            l4.Sastojak.Add(d);
            l11.ZamenaId.Add(l22.Id);
            l11.ZamenaId.Add(l4.Id);
            l22.ZamenaId.Add(l4.Id);
            l3.ZamenaId.Add(l4.Id);
            lekovi.Add(l11);
            lekovi.Add(l22);
            lekovi.Add(l3);
            lekovi.Add(l4);
            for(int i = 0; i < lekovi.Count; i++)
            {
                if (lekovi[i].Status.Equals(StatusLeka.Odbijen))
                {
                    lekovi.RemoveAt(i);
                    i--;
                }
            }

            lekarTrenutni.AdresaStanovanja = "AAA";     
            lekarTrenutni.BrojSlobodnihDana = 15;
            lekarTrenutni.BrojTelefona = "111111";
            lekarTrenutni.DatumRodjenja = new DateTime();
            lekarTrenutni.Email = "dada@dada.com";
            lekarTrenutni.GodineStaza = 11;
            lekarTrenutni.Ime = "Mico";
            lekarTrenutni.Prezime = "Govedarica";
            lekarTrenutni.Jmbg = "342425";
            lekarTrenutni.KorisnickoIme = "Pero";
            lekarTrenutni.Lozinka = "Admin";
            lekarTrenutni.Mbr = 21312;
            lekarTrenutni.Plata = 1000;
            Specijalizacija sp = new Specijalizacija();
            sp.Id = 121;
            sp.Naziv = "neka";
            sp.OblastMedicine = "nekaa";
            lekarTrenutni.Specijalizacija = sp;
            lekarTrenutni.TipKorisnika = TipKorisnika.lekar;
            lekarTrenutni.Zaposlen = true;

            lekarPomocni.AdresaStanovanja = "BBB";
            lekarPomocni.BrojSlobodnihDana = 15;
            lekarPomocni.BrojTelefona = "22222";
            lekarPomocni.DatumRodjenja = new DateTime();
            lekarPomocni.Email = "bada@dada.com";
            lekarPomocni.GodineStaza = 7;
            lekarPomocni.Ime = "Mio";
            lekarPomocni.Prezime = "Prodano";
            lekarPomocni.Jmbg = "222222";
            lekarPomocni.KorisnickoIme = "Peki";
            lekarPomocni.Lozinka = "Baja";
            lekarPomocni.Mbr = 3232;
            lekarPomocni.Plata = 10000;
            Specijalizacija spa = new Specijalizacija();
            spa.Id = 1211;
            spa.Naziv = "neeka";
            spa.OblastMedicine = "nekaaa";
            lekarPomocni.Specijalizacija = spa;
            lekarPomocni.TipKorisnika = TipKorisnika.lekar;
            lekarPomocni.Zaposlen = true;

            ll3.AdresaStanovanja = "Tolstojeva 1";
            ll3.BrojSlobodnihDana = 20;
            ll3.BrojTelefona = "0642354578";
            ll3.DatumRodjenja = new DateTime(1965, 3, 3);
            ll3.Email = "pap@gmail.com";
            ll3.GodineStaza = 30;
            ll3.Ime = "Vatroslav";
            ll3.Prezime = "Pap";
            ll3.Jmbg = "0303965123456";
            ll3.KorisnickoIme = "vatro";
            ll3.Lozinka = "vatro";
            ll3.Mbr = 123123;
            ll3.Plata = 15000;
            Specijalizacija sp3 = new Specijalizacija();
            sp3.Id = 1251;
            sp3.Naziv = "kardioloski majstor";
            sp3.OblastMedicine = "kardiologija";
            ll3.Specijalizacija = sp3;
            ll3.TipKorisnika = TipKorisnika.lekar;
            ll3.Zaposlen = true;

            ll4.AdresaStanovanja = "Balzakova 21";
            ll4.BrojSlobodnihDana = 17;
            ll4.BrojTelefona = "0613579624";
            ll4.DatumRodjenja = new DateTime(1988, 9, 9);
            ll4.Email = "bodi@gmail.com";
            ll4.GodineStaza = 6;
            ll4.Ime = "Radmilo";
            ll4.Prezime = "Bodiroga";
            ll4.Jmbg = "090988131533";
            ll4.KorisnickoIme = "bodi";
            ll4.Lozinka = "bodi";
            ll4.Mbr = 123456;
            ll4.Plata = 8000;
            Specijalizacija sp4 = new Specijalizacija();
            sp4.Id = 1251;
            sp4.Naziv = "slusni specijalista";
            sp4.OblastMedicine = "otorinolaringologija";
            ll4.Specijalizacija = sp3;
            ll4.TipKorisnika = TipKorisnika.lekar;
            ll4.Zaposlen = true;



            listaLekara.Add(lekarTrenutni);
            listaLekara.Add(lekarPomocni);
            listaLekara.Add(ll3);
            listaLekara.Add(ll4);


            listaPregleda = sviPregledi.GetAllPregledi();
            listaOperacija = sviPregledi.GetAllOperacije();
            listaPacijenata = sviPacijenti.GetAll();
            listaProstorija = sveProstorije.GetAllProstorije();


            for (int l = 0; l < listaPregleda.Count; l++)
            {
                if (!listaPregleda[l].lekarJmbg.Equals(lekarTrenutni.Jmbg))
                {
                    listaPregleda.RemoveAt(l);
                    l = l - 1;
                }
            }
            for (int l = 0; l < listaOperacija.Count; l++)
            {
                if (!listaOperacija[l].lekarJmbg.Equals(lekarTrenutni.Jmbg))
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
                    prikazPregleda.AnamnezaId = listaPregleda[i].AnamnezaId;
                    prikazPregleda.Hitan = listaPregleda[i].Hitan;
                    for (int p = 0; p<listaPacijenata.Count;p++)
                    {
                        if (listaPregleda[i].pacijentJmbg.Equals(listaPacijenata[p].Jmbg)&& listaPacijenata[p].Obrisan == false)
                        {
                            prikazPregleda.Pacijent = listaPacijenata[p];
                            break;
                        }
                        

                    }
                    for (int p = 0; p < listaProstorija.Count; p++)
                    {
                            if (listaPregleda[i].brojProstorije.Equals(listaProstorija[p].BrojProstorije)&& listaProstorija[p].Obrisana == false)
                        {
                            prikazPregleda.Prostorija = listaProstorija[p];
                            break;
                        }
                    }
                    for (int p=0;p < listaLekara.Count; p++)
                    {
                        if (listaPregleda[i].lekarJmbg.Equals(listaLekara[p].Jmbg))
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
                    prikazPregleda.AnamnezaId = listaPregleda[i].AnamnezaId;
                    prikazPregleda.Hitan = listaPregleda[i].Hitan;
                    for (int p = 0; p < listaPacijenata.Count; p++)
                    {
                        if (listaPregleda[i].pacijentJmbg.Equals(listaPacijenata[p].Jmbg) && listaPacijenata[p].Obrisan == false)
                        {
                            prikazPregleda.Pacijent = listaPacijenata[p];
                            break;
                        }


                    }
                    for (int p = 0; p < listaProstorija.Count; p++)
                    {
                        if (listaPregleda[i].brojProstorije.Equals(listaProstorija[p].BrojProstorije) && listaProstorija[p].Obrisana == false)
                        {
                            prikazPregleda.Prostorija = listaProstorija[p];
                            break;
                        }
                    }
                    for (int p = 0; p < listaLekara.Count; p++)
                    {
                        if (listaPregleda[i].lekarJmbg.Equals(listaLekara[p].Jmbg))
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
                    prikazOperacije.AnamnezaId = listaOperacija[i].AnamnezaId;
                    prikazOperacije.TipOperacije = listaOperacija[i].TipOperacije;
                    prikazOperacije.Hitan = listaOperacija[i].Hitan;
                    for (int p = 0; p < listaPacijenata.Count; p++)
                    {
                        if (listaOperacija[i].pacijentJmbg.Equals(listaPacijenata[p].Jmbg) && listaPacijenata[p].Obrisan == false)
                        {
                            prikazOperacije.Pacijent = listaPacijenata[p];
                            break;
                        }


                    }
                    for (int p = 0; p < listaProstorija.Count; p++)
                    {
                        if (listaOperacija[i].brojProstorije.Equals(listaProstorija[p].BrojProstorije) && listaProstorija[p].Obrisana == false)
                        {
                            prikazOperacije.Prostorija = listaProstorija[p];
                            break;
                        }
                    }
                    for (int p = 0; p < listaLekara.Count; p++)
                    {
                        if (listaOperacija[i].lekarJmbg.Equals(listaLekara[p].Jmbg))
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
                    prikazOperacije.AnamnezaId = listaOperacija[i].AnamnezaId;
                    prikazOperacije.TipOperacije = listaOperacija[i].TipOperacije;
                    prikazOperacije.Hitan = listaOperacija[i].Hitan;
                    for (int p = 0; p < listaPacijenata.Count; p++)
                    {
                        if (listaOperacija[i].pacijentJmbg.Equals(listaPacijenata[p].Jmbg) && listaPacijenata[p].Obrisan == false)
                        {
                            prikazOperacije.Pacijent = listaPacijenata[p];
                            break;
                        }


                    }
                    for (int p = 0; p < listaProstorija.Count; p++)
                    {
                        if (listaOperacija[i].brojProstorije.Equals(listaProstorija[p].BrojProstorije) && listaProstorija[p].Obrisana == false)
                        {
                            prikazOperacije.Prostorija = listaProstorija[p];
                            break;
                        }
                    }
                    for (int p = 0; p < listaLekara.Count; p++)
                    {
                        if (listaOperacija[i].lekarJmbg.Equals(listaLekara[p].Jmbg))
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
            for(int i = 0; i < lekovi.Count; i++)
            {
                if (!lekovi[i].Status.Equals(StatusLeka.Odbijen))
                {
                    PrikazLek p = new PrikazLek();
                    p.Id = lekovi[i].Id;
                    p.KolicinaUMg = lekovi[i].KolicinaUMg;
                    p.Naziv = lekovi[i].Naziv;
                    p.Status = lekovi[i].Status;
                    p.Proizvodjac = lekovi[i].Proizvodjac;
                    string l = "";
                    for (int m = 0; m < lekovi[i].sastojak.Count; m++)
                    {
                        if (m == 0)
                        {
                            l = l + " " + lekovi[i].sastojak[m].Naziv;
                        }
                        else
                        {
                            l = l + ", " + lekovi[i].sastojak[m].Naziv;
                        }
                    }
                    string h = "";
                    for (int m = 0; m < lekovi[i].ZamenaId.Count; m++)
                    {
                        Lek novi = new Lek();
                        for (int mo = 0; mo < lekovi.Count; mo++)
                        {
                            if (lekovi[i].ZamenaId[m].Equals(lekovi[mo].Id))
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
            FormNapraviTerminLekar forma = new FormNapraviTerminLekar(listaLekara, lekarTrenutni);
            forma.Show();
        }

        private void OtkaziPregled(object sender, RoutedEventArgs e)
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
                            FormIzmeniTerminLekar forma = new FormIzmeniTerminLekar(p1, listaLekara, lekarTrenutni);
                            forma.Show();
                            break;
                        }
                    }
                }
                else if (objekat.GetType().Equals(prikazOperacije.GetType()))
                {
                    if (!lekarTrenutni.Specijalizacija.OblastMedicine.Equals("opsta"))
                    {
                        PrikazOperacije pri = objekat as PrikazOperacije;
                        for (int i = 0; i < listaOperacija.Count; i++)
                        {
                            if (pri.Id.Equals(listaOperacija[i].Id))
                            {

                                op = lekarGrid.SelectedItem as PrikazOperacije;
                                FormIzmeniTerminLekar forma = new FormIzmeniTerminLekar(op, listaLekara, lekarTrenutni);
                                forma.Show();
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
            if(e.Key == Key.Enter)
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
                if (row < lekarGrid.Items.Count-1)
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

            if(e.Key== Key.Left)
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

            for (int i = 0; i < lekarGridIstorija.Items.Count+1; i++)
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
                                FormNapraviAnamnezuLekar form = new FormNapraviAnamnezuLekar(p1, listaLekara, lekarTrenutni);
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

                                FormNapraviAnamnezuLekar form = new FormNapraviAnamnezuLekar(op, listaLekara, lekarTrenutni);
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
                                FormNapraviAnamnezuLekar form = new FormNapraviAnamnezuLekar(p1, listaLekara, lekarTrenutni);
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

                                FormNapraviAnamnezuLekar form = new FormNapraviAnamnezuLekar(op, listaLekara, lekarTrenutni);
                                form.Show();
                                break;
                            }
                        }
                   
                }   
               

            }
        }

        private void IzmeniLek(object sender, RoutedEventArgs e)
        {
            PrikazLek p = dataGridLekovi.SelectedItem as PrikazLek;
            for(int i =0; i < lekovi.Count; i++)
            {
                if (lekovi[i].Id.Equals(p.Id))
                {
                    FormIzmeniLekLekar form = new FormIzmeniLekLekar(lekovi[i],lekovi);
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
                Izmeni.Focus();

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
            PrikazLek p = dataGridLekovi.SelectedItem as PrikazLek;
            if (p.Status.Equals(StatusLeka.CekaValidaciju))
            {
                for (int i = 0; i < lekovi.Count; i++)
                {
                    if (lekovi[i].Id.Equals(p.Id))
                    {
                        lekovi[i].Status = StatusLeka.Odobren;
                        break;

                    }
                }
                for (int i = 0; i < lekoviPrikaz.Count; i++)
                {
                    if (lekoviPrikaz[i].Id.Equals(p.Id))
                    {
                        lekoviPrikaz[i].Status = StatusLeka.Odobren;
                        dataGridLekovi.Items.Refresh();
                        break;

                    }
                }
                Obavestenje obavestenje = new Obavestenje();
                obavestenje.KorisnickaImena = new List<string>();
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
                        obavestenje.KorisnickaImena.Add(svi[i].KorisnickoIme);
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
            
            PrikazLek p = dataGridLekovi.SelectedItem as PrikazLek;
            if (p.Status.Equals(StatusLeka.CekaValidaciju))
            {
                FormKomentarLekaLekar lek = new FormKomentarLekaLekar(p, lekovi);
                lek.Show();
            }
            else
            {
                MessageBox.Show("Niste odabrali lek koji ceka validaciju");
            }
            
            
        }
    }
}
