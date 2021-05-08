using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Model.Korisnici;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormNasiPredlozi.xaml
    /// </summary>
    public partial class FormNasiPredlozi : Window
    {
        private DateTime datum;
        private int sat;
        private int minut;
        private Lekar lekar;

        private Pacijent trenutniPacijent = new Pacijent();

        private List<Lekar> lekari = new List<Lekar>();
        private List<Pregled> pregledi = new List<Pregled>();
        private List<DateTime> zauzetiTermini = new List<DateTime>();
        private List<Prostorija> prostorije = new List<Prostorija>();
        private List<Prostorija> slobodneProstorije = new List<Prostorija>();

        FileStoragePregledi storagePregledi = new FileStoragePregledi();
        FileStorageProstorija storageProstorije = new FileStorageProstorija();

        public static ObservableCollection<PrikazPregleda> PredlozeniTermini
        {
            get;
            set;
        }

        public FormNasiPredlozi(Pacijent pacijent, DateTime datum, int sat, int minut, Lekar lekar)
        {
            InitializeComponent();

            this.DataContext = this;

            this.datum = datum;
            this.sat = sat;
            this.minut = minut;
            this.lekar = lekar;

            trenutniPacijent = pacijent;

            PredlozeniTermini = new ObservableCollection<PrikazPregleda>();

            
            pregledi = storagePregledi.GetAllPregledi();
            foreach (Pregled p in pregledi)
            {
                zauzetiTermini.Add(p.Datum);
            }

            prostorije = storageProstorije.GetAllProstorije();
            foreach (Prostorija p in prostorije)
            {
                if (p.TipProstorije.Equals(TipProstorije.salaZaPreglede))
                {
                    if (p.Obrisana == false)
                    {
                        slobodneProstorije.Add(p);
                        break;
                    }
                }
            }

            Lekar l1 = new Lekar();
            Lekar l2 = new Lekar();
            Lekar l3 = new Lekar();
            Lekar l4 = new Lekar();

            l1.AdresaStanovanja = "AAA";
            l1.BrojSlobodnihDana = 15;
            l1.BrojTelefona = "111111";
            l1.DatumRodjenja = new DateTime();
            l1.Email = "dada@dada.com";
            l1.GodineStaza = 11;
            l1.Ime = "Mico";
            l1.Prezime = "Govedarica";
            l1.Jmbg = "342425";
            l1.KorisnickoIme = "Pero";
            l1.Lozinka = "Admin";
            l1.Mbr = 21312;
            l1.Plata = 1000;
            Specijalizacija sp = new Specijalizacija();
            sp.Id = 121;
            sp.Naziv = "neka";
            sp.OblastMedicine = "nekaa";
            l1.Specijalizacija = sp;
            l1.TipKorisnika = TipKorisnika.lekar;
            l1.Zaposlen = true;

            l2.AdresaStanovanja = "BBB";
            l2.BrojSlobodnihDana = 15;
            l2.BrojTelefona = "22222";
            l2.DatumRodjenja = new DateTime();
            l2.Email = "bada@dada.com";
            l2.GodineStaza = 7;
            l2.Ime = "Radendko";
            l2.Prezime = "Salapura";
            l2.Jmbg = "222222";
            l2.KorisnickoIme = "Peki";
            l2.Lozinka = "Baja";
            l2.Mbr = 3232;
            l2.Plata = 10000;
            Specijalizacija spa = new Specijalizacija();
            spa.Id = 1211;
            spa.Naziv = "neeka";
            spa.OblastMedicine = "nekaaa";
            l2.Specijalizacija = spa;
            l2.TipKorisnika = TipKorisnika.lekar;
            l2.Zaposlen = true;

            l3.AdresaStanovanja = "Tolstojeva 1";
            l3.BrojSlobodnihDana = 20;
            l3.BrojTelefona = "0642354578";
            l3.DatumRodjenja = new DateTime(1965, 3, 3);
            l3.Email = "pap@gmail.com";
            l3.GodineStaza = 30;
            l3.Ime = "Vatroslav";
            l3.Prezime = "Pap";
            l3.Jmbg = "0303965123456";
            l3.KorisnickoIme = "vatro";
            l3.Lozinka = "vatro";
            l3.Mbr = 123123;
            l3.Plata = 15000;
            Specijalizacija sp3 = new Specijalizacija();
            sp3.Id = 1251;
            sp3.Naziv = "kardioloski majstor";
            sp3.OblastMedicine = "kardiologija";
            l3.Specijalizacija = sp3;
            l3.TipKorisnika = TipKorisnika.lekar;
            l3.Zaposlen = true;

            l4.AdresaStanovanja = "Balzakova 21";
            l4.BrojSlobodnihDana = 17;
            l4.BrojTelefona = "0613579624";
            l4.DatumRodjenja = new DateTime(1988, 9, 9);
            l4.Email = "bodi@gmail.com";
            l4.GodineStaza = 6;
            l4.Ime = "Radmilo";
            l4.Prezime = "Bodiroga";
            l4.Jmbg = "090988131533";
            l4.KorisnickoIme = "bodi";
            l4.Lozinka = "bodi";
            l4.Mbr = 123456;
            l4.Plata = 8000;
            Specijalizacija sp4 = new Specijalizacija();
            sp4.Id = 1251;
            sp4.Naziv = "slusni specijalista";
            sp4.OblastMedicine = "otorinolaringologija";
            l4.Specijalizacija = sp3;
            l4.TipKorisnika = TipKorisnika.lekar;
            l4.Zaposlen = true;

            lekari.Add(l1);
            lekari.Add(l2);
            lekari.Add(l3);
            lekari.Add(l4);


            if (datum.Equals(new DateTime(1, 1, 1)) && sat == -1 && minut == -1 && lekar.Jmbg is null)
            {
                PrikazPregleda p1 = new PrikazPregleda();
                p1.Datum = DateTime.Today.AddDays(3).AddHours(10);
                p1.Lekar = l1;
                p1.Trajanje = 30;
                p1.Zavrsen = false;
                p1.Pacijent = pacijent;
                p1.Anamneza.Id = -1;

                PrikazPregleda p2 = new PrikazPregleda();
                p2.Datum = DateTime.Today.AddDays(4).AddHours(11).AddMinutes(45);
                p2.Lekar = l2;
                p2.Trajanje = 30;
                p2.Zavrsen = false;
                p2.Pacijent = pacijent;
                p2.Anamneza.Id = -1;

                PrikazPregleda p3 = new PrikazPregleda();
                p3.Datum = DateTime.Today.AddDays(5).AddHours(18).AddMinutes(15);
                p3.Lekar = l3;
                p3.Trajanje = 30;
                p3.Zavrsen = false;
                p3.Pacijent = pacijent;
                p3.Anamneza.Id = -1;

                PrikazPregleda p4 = new PrikazPregleda();
                p4.Datum = DateTime.Today.AddDays(6).AddHours(7).AddMinutes(30);
                p4.Lekar = l4;
                p4.Trajanje = 30;
                p4.Zavrsen = false;
                p4.Pacijent = pacijent;
                p4.Anamneza.Id = -1;

                if (slobodneProstorije.Count > 0)
                {
                    p1.Prostorija = slobodneProstorije[0];
                    if (slobodneProstorije.Count > 1)
                    {
                        p2.Prostorija = slobodneProstorije[1];
                        if (slobodneProstorije.Count > 2)
                        {
                            p3.Prostorija = slobodneProstorije[2];
                            if (slobodneProstorije.Count > 3)
                            {
                                p4.Prostorija = slobodneProstorije[3];
                            }
                            else
                            {
                                p4.Prostorija = slobodneProstorije[0];
                            }
                        }
                        else
                        {
                            p3.Prostorija = slobodneProstorije[0];
                            p4.Prostorija = slobodneProstorije[0];
                        }
                    }
                    else
                    {
                        p2.Prostorija = slobodneProstorije[0];
                        p3.Prostorija = slobodneProstorije[0];
                        p4.Prostorija = slobodneProstorije[0];
                    }
                }
                else
                {
                    MessageBox.Show("Nema slobodnih prostorija!");
                }

                int max = 0;
                for (int i = 0; i < pregledi.Count; i++)
                {
                    if (pregledi[i].Id > max)
                        max = pregledi[i].Id;
                }
                p1.Id = max + 1;
                p2.Id = max + 1;
                p3.Id = max + 1;
                p4.Id = max + 1;

                PredlozeniTermini.Add(p1);
                PredlozeniTermini.Add(p2);
                PredlozeniTermini.Add(p3);
                PredlozeniTermini.Add(p4);
            }
            else if (!datum.Equals(new DateTime(1, 1, 1)) && sat != -1 && minut != -1 && !(lekar.Jmbg is null))
            {
                PrikazPregleda p1 = new PrikazPregleda();
                p1.Datum = datum.AddHours(sat).AddMinutes(minut);
                p1.Lekar = lekar;
                p1.Trajanje = 30;
                p1.Zavrsen = false;
                p1.Pacijent = pacijent;
                p1.Anamneza.Id = -1;

                PrikazPregleda p2 = new PrikazPregleda();
                p2.Datum = datum.AddHours(sat+1).AddMinutes(minut+15);
                p2.Lekar = lekar;
                p2.Trajanje = 30;
                p2.Zavrsen = false;
                p2.Pacijent = pacijent;
                p2.Anamneza.Id = -1;

                PrikazPregleda p3 = new PrikazPregleda();
                p3.Datum = datum.AddHours(sat+2).AddMinutes(minut);
                p3.Lekar = lekar;
                p3.Trajanje = 30;
                p3.Zavrsen = false;
                p3.Pacijent = pacijent;
                p3.Anamneza.Id = -1;

                PrikazPregleda p4 = new PrikazPregleda();
                p4.Datum = datum.AddHours(sat+3).AddMinutes(minut+45);
                p4.Lekar = lekar;
                p4.Trajanje = 30;
                p4.Zavrsen = false;
                p4.Pacijent = pacijent;
                p4.Anamneza.Id = -1;

                if (slobodneProstorije.Count > 0)
                {
                    p1.Prostorija = slobodneProstorije[0];
                    if (slobodneProstorije.Count > 1)
                    {
                        p2.Prostorija = slobodneProstorije[1];
                        if (slobodneProstorije.Count > 2)
                        {
                            p3.Prostorija = slobodneProstorije[2];
                            if (slobodneProstorije.Count > 3)
                            {
                                p4.Prostorija = slobodneProstorije[3];
                            }
                            else
                            {
                                p4.Prostorija = slobodneProstorije[0];
                            }
                        }
                        else
                        {
                            p3.Prostorija = slobodneProstorije[0];
                            p4.Prostorija = slobodneProstorije[0];
                        }
                    }
                    else
                    {
                        p2.Prostorija = slobodneProstorije[0];
                        p3.Prostorija = slobodneProstorije[0];
                        p4.Prostorija = slobodneProstorije[0];
                    }
                }
                else
                {
                    MessageBox.Show("Nema slobodnih prostorija!");
                }

                int max = 0;
                for (int i = 0; i < pregledi.Count; i++)
                {
                    if (pregledi[i].Id > max)
                        max = pregledi[i].Id;
                }
                p1.Id = max + 1;
                p2.Id = max + 1;
                p3.Id = max + 1;
                p4.Id = max + 1;

                PredlozeniTermini.Add(p1);
                PredlozeniTermini.Add(p2);
                PredlozeniTermini.Add(p3);
                PredlozeniTermini.Add(p4);
            }
            else if (!datum.Equals(new DateTime(1,1,1)) && lekar.Jmbg is null)
            {
                if (sat != -1 && minut != -1)
                {
                    DateTime dat = new DateTime(datum.Year, datum.Month, datum.Day, sat, minut, 0);

                    PrikazPregleda p1 = new PrikazPregleda();
                    p1.Datum = dat;
                    p1.Lekar = l1;
                    p1.Trajanje = 30;
                    p1.Zavrsen = false;
                    p1.Pacijent = pacijent;
                    p1.Anamneza.Id = -1;

                    PrikazPregleda p2 = new PrikazPregleda();
                    p2.Datum = dat;
                    p2.Lekar = l2;
                    p2.Trajanje = 30;
                    p2.Zavrsen = false;
                    p2.Pacijent = pacijent;
                    p2.Anamneza.Id = -1;

                    PrikazPregleda p3 = new PrikazPregleda();
                    p3.Datum = dat;
                    p3.Lekar = l3;
                    p3.Trajanje = 30;
                    p3.Zavrsen = false; 
                    p3.Pacijent = pacijent;
                    p3.Anamneza.Id = -1;

                    PrikazPregleda p4 = new PrikazPregleda();
                    p4.Datum = dat;
                    p4.Lekar = l4;
                    p4.Trajanje = 30;
                    p4.Zavrsen = false;
                    p4.Pacijent = pacijent;
                    p4.Anamneza.Id = -1;

                    if (slobodneProstorije.Count > 0)
                    {
                        p1.Prostorija = slobodneProstorije[0];
                        if (slobodneProstorije.Count > 1)
                        {
                            p2.Prostorija = slobodneProstorije[1];
                            if (slobodneProstorije.Count > 2)
                            {
                                p3.Prostorija = slobodneProstorije[2];
                                if (slobodneProstorije.Count > 3)
                                {
                                    p4.Prostorija = slobodneProstorije[3];
                                }
                                else
                                {
                                    p4.Prostorija = slobodneProstorije[0];
                                }
                            }
                            else
                            {
                                p3.Prostorija = slobodneProstorije[0];
                                p4.Prostorija = slobodneProstorije[0];
                            }
                        }
                        else
                        {
                            p2.Prostorija = slobodneProstorije[0];
                            p3.Prostorija = slobodneProstorije[0];
                            p4.Prostorija = slobodneProstorije[0];
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nema slobodnih prostorija!");
                    }

                    int max = 0;
                    for (int i = 0; i < pregledi.Count; i++)
                    {
                        if (pregledi[i].Id > max)
                            max = pregledi[i].Id;
                    }
                    p1.Id = max + 1;
                    p2.Id = max + 1;
                    p3.Id = max + 1;
                    p4.Id = max + 1;

                    PredlozeniTermini.Add(p1);
                    PredlozeniTermini.Add(p2);
                    PredlozeniTermini.Add(p3);
                    PredlozeniTermini.Add(p4);
                }
                else
                {
                    DateTime dat1 = new DateTime(datum.Year, datum.Month, datum.Day, 7, 15, 0);
                    DateTime dat2 = new DateTime(datum.Year, datum.Month, datum.Day, 8, 45, 0);
                    DateTime dat3 = new DateTime(datum.Year, datum.Month, datum.Day, 14, 30, 0);
                    DateTime dat4 = new DateTime(datum.Year, datum.Month, datum.Day, 16, 0, 0);

                    PrikazPregleda p1 = new PrikazPregleda();
                    p1.Datum = dat1;
                    p1.Lekar = l1;
                    p1.Trajanje = 30;
                    p1.Zavrsen = false;
                    p1.Pacijent = pacijent;
                    p1.Anamneza.Id = -1;

                    PrikazPregleda p2 = new PrikazPregleda();
                    p2.Datum = dat2;
                    p2.Lekar = l2;
                    p2.Trajanje = 30;
                    p2.Zavrsen = false;
                    p2.Pacijent = pacijent; 
                    p2.Anamneza.Id = -1;

                    PrikazPregleda p3 = new PrikazPregleda();
                    p3.Datum = dat3;
                    p3.Lekar = l3;
                    p3.Trajanje = 30; 
                    p3.Zavrsen = false; 
                    p3.Pacijent = pacijent;
                    p3.Anamneza.Id = -1;

                    PrikazPregleda p4 = new PrikazPregleda();
                    p4.Datum = dat4;
                    p4.Lekar = l4;
                    p4.Trajanje = 30;
                    p4.Zavrsen = false;
                    p4.Pacijent = pacijent;
                    p4.Anamneza.Id = -1;

                    if (slobodneProstorije.Count > 0)
                    {
                        p1.Prostorija = slobodneProstorije[0];
                        if (slobodneProstorije.Count > 1)
                        {
                            p2.Prostorija = slobodneProstorije[1];
                            if (slobodneProstorije.Count > 2)
                            {
                                p3.Prostorija = slobodneProstorije[2];
                                if (slobodneProstorije.Count > 3)
                                {
                                    p4.Prostorija = slobodneProstorije[3];
                                }
                                else
                                {
                                    p4.Prostorija = slobodneProstorije[0];
                                }
                            }
                            else
                            {
                                p3.Prostorija = slobodneProstorije[0];
                                p4.Prostorija = slobodneProstorije[0];
                            }
                        }
                        else
                        {
                            p2.Prostorija = slobodneProstorije[0];
                            p3.Prostorija = slobodneProstorije[0];
                            p4.Prostorija = slobodneProstorije[0];
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nema slobodnih prostorija!");
                    }

                    int max = 0;
                    for (int i = 0; i < pregledi.Count; i++)
                    {
                        if (pregledi[i].Id > max)
                            max = pregledi[i].Id;
                    }
                    p1.Id = max + 1;
                    p2.Id = max + 1;
                    p3.Id = max + 1;
                    p4.Id = max + 1;

                    PredlozeniTermini.Add(p1);
                    PredlozeniTermini.Add(p2);
                    PredlozeniTermini.Add(p3);
                    PredlozeniTermini.Add(p4);
                }

                
            }
            else if (datum.Equals(new DateTime(1, 1, 1)) && !(lekar.Jmbg is null))
            {
                PrikazPregleda p1 = new PrikazPregleda();
                p1.Datum = DateTime.Today.AddDays(3);
                p1.Lekar = lekar;
                p1.Trajanje = 30;
                p1.Zavrsen = false;
                p1.Pacijent = pacijent;
                p1.Anamneza.Id = -1;

                PrikazPregleda p2 = new PrikazPregleda();
                p2.Datum = DateTime.Today.AddDays(4).AddHours(17).AddMinutes(30);
                p2.Lekar = lekar;
                p2.Trajanje = 30;
                p2.Zavrsen = false;
                p2.Pacijent = pacijent;
                p2.Anamneza.Id = -1;

                PrikazPregleda p3 = new PrikazPregleda();
                p3.Datum = DateTime.Today.AddDays(5).AddHours(8);
                p3.Lekar = lekar;
                p3.Trajanje = 30;
                p3.Zavrsen = false;
                p3.Pacijent = pacijent;
                p3.Anamneza.Id = -1;

                PrikazPregleda p4 = new PrikazPregleda();
                p4.Datum = DateTime.Today.AddDays(6).AddHours(14).AddMinutes(15); ;
                p4.Lekar = lekar;
                p4.Trajanje = 30;
                p4.Zavrsen = false;
                p4.Pacijent = pacijent;
                p4.Anamneza.Id = -1;

                if (slobodneProstorije.Count > 0)
                {
                    p1.Prostorija = slobodneProstorije[0];
                    if (slobodneProstorije.Count > 1)
                    {
                        p2.Prostorija = slobodneProstorije[1];
                        if (slobodneProstorije.Count > 2)
                        {
                            p3.Prostorija = slobodneProstorije[2];
                            if (slobodneProstorije.Count > 3)
                            {
                                p4.Prostorija = slobodneProstorije[3];
                            }
                            else
                            {
                                p4.Prostorija = slobodneProstorije[0];
                            }
                        }
                        else
                        {
                            p3.Prostorija = slobodneProstorije[0];
                            p4.Prostorija = slobodneProstorije[0];
                        }
                    }
                    else
                    {
                        p2.Prostorija = slobodneProstorije[0];
                        p3.Prostorija = slobodneProstorije[0];
                        p4.Prostorija = slobodneProstorije[0];
                    }
                }
                else
                {
                    MessageBox.Show("Nema slobodnih prostorija!");
                }

                int max = 0;
                for (int i = 0; i < pregledi.Count; i++)
                {
                    if (pregledi[i].Id > max)
                        max = pregledi[i].Id;
                }
                p1.Id = max + 1;
                p2.Id = max + 1;
                p3.Id = max + 1;
                p4.Id = max + 1;

                PredlozeniTermini.Add(p1);
                PredlozeniTermini.Add(p2);
                PredlozeniTermini.Add(p3);
                PredlozeniTermini.Add(p4);
            }

        }

        private void PotvrdiIzabraniTermin(object sender, RoutedEventArgs e)
        {
            var objekat = nasiPredloziGrid.SelectedValue;

            if (objekat != null)
            {
                PrikazPregleda p = (PrikazPregleda)nasiPredloziGrid.SelectedItem;

                DateTime datum = p.Datum;
                int dan = datum.Day;
                int mesec = datum.Month;
                int godina = datum.Year;
                foreach (PrikazPregleda prikaz in PredlozeniTermini)
                {
                    if (prikaz.Equals(p))
                    {
                        FormPacijentPage.PrikazNezavrsenihPregleda.Add(prikaz);
                        Pregled pregled = new Pregled();
                        pregled.Id = prikaz.Id;
                        pregled.Datum = prikaz.Datum;
                        pregled.Trajanje = prikaz.Trajanje;
                        pregled.Zavrsen = prikaz.Zavrsen;
                        pregled.Prostorija = prikaz.Prostorija;
                        pregled.Anamneza = prikaz.Anamneza;
                        pregled.Lekar = prikaz.Lekar;
                        pregled.Pacijent = prikaz.Pacijent;
                        FileStoragePregledi storage = new FileStoragePregledi();
                        storage.Save(pregled);

                        FileStorageAntiTrol storageAntiTrol = new FileStorageAntiTrol();
                        AntiTrol antiTrol = new AntiTrol();
                        antiTrol.PacijentJMBG = prikaz.Pacijent.Jmbg;
                        antiTrol.Datum = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                        storageAntiTrol.Save(antiTrol);

                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Morate odabrati pregled koji zelite da zakazete!", "Upozorenje");
            }
        }

        private void Odustani(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
