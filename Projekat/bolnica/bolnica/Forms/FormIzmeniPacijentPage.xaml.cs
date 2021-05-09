using Bolnica.Model.Korisnici;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormIzmeniPacijentPage.xaml
    /// </summary>
    public partial class FormIzmeniPacijentPage : Page
    {
        private PrikazPregleda prikazPregleda;
        private FileStorageLekar storageLekari = new FileStorageLekar();
        private List<Lekar> lekari = new List<Lekar>();
        private FormPacijentWeb form;

        public FormIzmeniPacijentPage(PrikazPregleda p, FormPacijentWeb formPacijentWeb)
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

            form = formPacijentWeb;

            lekari = storageLekari.GetAll();

            foreach (Lekar l in lekari)
            {
                comboLekar.Items.Add(l.Ime + " " + l.Prezime);
            }

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
                    FormPacijentPage.PrikazNezavrsenihPregleda.Remove(this.prikazPregleda);
                    FormPacijentPage.PrikazNezavrsenihPregleda.Add(prikaz);

                    Pregled pregled = new Pregled();
                    pregled.Id = prikaz.Id;
                    pregled.Datum = prikaz.Datum;
                    pregled.Trajanje = prikaz.Trajanje;
                    pregled.Zavrsen = prikaz.Zavrsen;
                    pregled.Anamneza = prikazPregleda.Anamneza;
                    pregled.Lekar = prikaz.Lekar;
                    pregled.Prostorija = prikaz.Prostorija;
                    pregled.Pacijent = prikaz.Pacijent;

                    FileStoragePregledi storagePregledi = new FileStoragePregledi();
                    storagePregledi.Izmeni(pregled);

                    FileStorageAntiTrol storageAntiTrol = new FileStorageAntiTrol();
                    AntiTrol antiTrol = new AntiTrol();
                    antiTrol.PacijentJMBG = prikaz.Pacijent.Jmbg;
                    antiTrol.Datum = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second); ;
                    storageAntiTrol.Save(antiTrol);

                    form.Pocetna.Content = new FormPacijentPage(prikaz.Pacijent, form);
                }

            }
        }

        private void OtkaziIzmenu(object sender, RoutedEventArgs e)
        {
            form.Pocetna.Content = new FormPacijentPage(prikazPregleda.Pacijent, form);
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
        }
    }
}
