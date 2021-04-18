using Bolnica.Model.Pregledi;
using Model.Korisnici;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for FormIzmeniTerminPacijent.xaml
    /// </summary>
    public partial class FormIzmeniTerminPacijent : Window
    {
        private PrikazPregleda prikazPregleda;
        private List<Lekar> lekari = new List<Lekar>();

        public FormIzmeniTerminPacijent(PrikazPregleda p)
        {
            prikazPregleda = p;

            DateTime datPre = p.Datum;
            int dan = datPre.Day;
            int mesec = datPre.Month;
            int godina = datPre.Year;
            int sat = datPre.Hour;
            int minut = datPre.Minute;
            string[] div = datPre.ToString().Split(" ");
            string[] d = div[0].Split(".");
            string[] v = div[1].Split(":");

            string imeL = p.Lekar.Ime;
            string prezimeL = p.Lekar.Prezime;
           
            InitializeComponent();

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

            comboLekar.Items.Add(l1.Ime + " " + l1.Prezime);
            comboLekar.Items.Add(l2.Ime + " " + l2.Prezime);
            comboLekar.Items.Add(l3.Ime + " " + l3.Prezime);
            comboLekar.Items.Add(l4.Ime + " " + l4.Prezime);

            DateTime dat = new DateTime(godina, mesec, dan);
            datumPicker.SelectedDate = dat;
            if (sat < 10)
            {
                comboSat.Text = "0" + sat.ToString();
            }
            else
            {
                comboSat.Text = sat.ToString();
            }
            if (minut == 0)
            {
                comboMinut.Text = "0" + minut.ToString();
            }
            else
            {
                comboMinut.Text = minut.ToString();
            }
            comboLekar.Text = imeL + " " + prezimeL;
        }

        private void PotvrdiIzmenu(object sender, RoutedEventArgs e)
        {
            if (datumPicker.Text.Length == 0)
            {
                MessageBox.Show("Molimo Vas odaberite datum pregelda");
            }
            else if (comboSat.Text.Length == 0)
            {
                MessageBox.Show("Molimo Vas odaberite vreme pregleda (sat)");
            }
            else if (comboMinut.Text.Length == 0)
            {
                MessageBox.Show("Molimo Vas odaberite vreme pregleda (minut)");
            }
            else if (comboLekar.Text.Length == 0)
            {
                MessageBox.Show("Molimo Vas odaberite zeljenog lekara");
            }
            else
            {
                DateTime datum = (DateTime)datumPicker.SelectedDate;
                int dan = datum.Day;
                int mesec = datum.Month;
                int godina = datum.Year;
                int sat = comboSat.SelectedIndex;
                int minut = comboMinut.SelectedIndex * 15;
                DateTime datumIVremePregleda = new DateTime(godina, mesec, dan, sat, minut, 0);

                string imeLekara = comboLekar.Text;
                String[] splited = imeLekara.Split(" ");
                string ime = splited[0];
                string prezime = splited[1];               

                PrikazPregleda prikaz = new PrikazPregleda();
                prikaz.Id = prikazPregleda.Id;
                prikaz.Pacijent = prikazPregleda.Pacijent;
                prikaz.Datum = datumIVremePregleda;
                prikaz.Zavrsen = false;
                prikaz.Trajanje = 30;

                Lekar l = new Lekar();
                foreach (Lekar lek in lekari)
                {
                    if (ime.Equals(lek.Ime) && prezime.Equals(lek.Prezime))
                    {
                        l = lek;
                        break;
                    }
                }
                prikaz.Lekar = l;

                FileStorageProstorija storageProstorije = new FileStorageProstorija();
                List<Prostorija> prostorije = storageProstorije.GetAllProstorije();

                bool slobodna = false;
                foreach (Prostorija pro in prostorije)
                {
                    if (pro.TipProstorije.Equals(TipProstorije.salaZaPreglede))
                    {
                        if (pro.Obrisana == false)
                        {
                            prikaz.Prostorija = pro;
                            slobodna = true;
                            break;
                        }
                    }
                }

                if (slobodna == false)
                {
                    MessageBox.Show("U izabranom terminu nema slobodnih sala za pregled! Molimo Vas odaberite neki drugi termin.");
                }
                else
                {
                    FormPacijent.PrikazNezavrsenihPregleda.Remove(this.prikazPregleda);
                    FormPacijent.PrikazNezavrsenihPregleda.Add(prikaz);

                    Pregled pregled = new Pregled();
                    pregled.Id = prikaz.Id;
                    pregled.Datum = prikaz.Datum;
                    pregled.Trajanje = prikaz.Trajanje;
                    pregled.Zavrsen = prikaz.Zavrsen;
                    pregled.AnamnezaId = prikazPregleda.AnamnezaId;
                    pregled.lekarJmbg = prikaz.Lekar.Jmbg;
                    pregled.brojProstorije = prikaz.Prostorija.BrojProstorije;
                    pregled.pacijentJmbg = prikaz.Pacijent.Jmbg;

                    FileStoragePregledi storagePregledi = new FileStoragePregledi();
                    storagePregledi.Izmeni(pregled);

                    this.Close();
                }

            }
        }

        private void OtkaziIzmenu(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NasiPredlozi(object sender, RoutedEventArgs e)
        {
            DateTime datum = new DateTime(1, 1, 1);
            int sat = -1;
            int minut = -1;
            string imeLekara = "";
            Lekar lekar = new Lekar();

            if (!(datumPicker.SelectedDate is null))
            {
                datum = (DateTime)datumPicker.SelectedDate;
            }

            if (comboSat.SelectedIndex >= 0)
            {
                sat = comboSat.SelectedIndex;
            }

            if (comboMinut.SelectedIndex >= 0)
            {
                minut = comboMinut.SelectedIndex * 15;
            }

            if (!comboLekar.Text.Equals(""))
            {
                imeLekara = comboLekar.Text;
                String[] splited = imeLekara.Split(" ");
                string ime = splited[0];
                string prezime = splited[1];
                foreach (Lekar lek in lekari)
                {
                    if (ime.Equals(lek.Ime) && prezime.Equals(lek.Prezime))
                    {
                        lekar = lek;
                        break;
                    }
                }
            }

            var s = new FormNasiPredlozi(prikazPregleda.Pacijent, datum, sat, minut, lekar);
            s.Show();
            this.Close();
        }
    }
}
