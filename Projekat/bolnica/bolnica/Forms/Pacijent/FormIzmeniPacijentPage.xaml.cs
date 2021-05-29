using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.Model.Prostorije;
using Model.Korisnici;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormIzmeniPacijentPage.xaml
    /// </summary>
    public partial class FormIzmeniPacijentPage : Page
    {
        private PrikazPregleda prikazPregleda;

        private FileRepositoryPregled storagePregledi = new FileRepositoryPregled();
        private FileRepositoryLekar storageLekari = new FileRepositoryLekar();
        private FileStorageProstorija storageProstorije = new FileStorageProstorija();
        private FileStorageRenoviranje storageRenoviranje = new FileStorageRenoviranje();
        private FileStorageAntiTrol storageAntiTrol = new FileStorageAntiTrol();

        private List<Lekar> lekari = new List<Lekar>();
        private List<Prostorija> prostorije = new List<Prostorija>();

        public FormIzmeniPacijentPage(PrikazPregleda prikazPre)
        {
            InitializeComponent();

            prikazPregleda = prikazPre;

            DodajLekareUComboBox();
            DobijVrednostPolja();
        }

        private void DodajLekareUComboBox()
        {
            lekari = storageLekari.GetAll();
            foreach (Lekar l in lekari)
            {
                comboLekar.Items.Add(l.Ime + " " + l.Prezime);
            }
        }

        private void DobijVrednostPolja()
        {
            DateTime datum = prikazPregleda.Datum;
            int sat = datum.Hour;
            int minut = datum.Minute;
            string ime = prikazPregleda.Lekar.Ime;
            string prezime = prikazPregleda.Lekar.Prezime;
            NamestiVrednostPolja(datum, sat, minut, ime, prezime);
        }

        private void NamestiVrednostPolja(DateTime datum, int sat, int minut, string ime, string prezime)
        {
            datumPicker.SelectedDate = datum;
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
            comboLekar.Text = ime + " " + prezime;
        }

        private void PotvrdiIzmenu(object sender, RoutedEventArgs e)
        {
            if (ProveriPopunjenostDatuma() && ProveriPopunjenostSati() && ProveriPopunjenostMinuta() && ProveriPopunjenostLekara())
            {
                PrikazPregleda prikaz = SetPrikaz();
                if (prikaz.Prostorija is null)
                {
                    MessageBox.Show("U izabranom terminu nema slobodnih sala za pregled! Molimo Vas odaberite neki drugi termin.");
                }
                else
                {
                    Pregled pregled = SetPregled(prikaz);
                    FormPacijentPage.PrikazNezavrsenihPregleda.Remove(this.prikazPregleda);
                    FormPacijentPage.PrikazNezavrsenihPregleda.Add(prikaz);
                    storagePregledi.Update(pregled);

                    AntiTrol antiTrol = new AntiTrol
                    {
                        Pacijent = prikaz.Pacijent,
                        Datum = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
                    };
                    storageAntiTrol.Save(antiTrol);

                    FormPacijentWeb.Forma.Pocetna.Content = new FormPacijentPage(prikaz.Pacijent);
                }
            }
        }

        private void OtkaziIzmenu(object sender, RoutedEventArgs e)
        {
            FormPacijentWeb.Forma.Pocetna.Content = new FormPacijentPage(prikazPregleda.Pacijent);
        }

        private void NasiPredlozi(object sender, RoutedEventArgs e)
        {
            DateTime datum = new DateTime(1, 1, 1);
            int sat = -1;
            int minut = -1;
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
                lekar = DobijLekara(DobijImeLekara(), DobijPrezimeLekara());
            }

            FormPacijentWeb.Forma.Pocetna.Content = new FormNasiPredloziPage(prikazPregleda.Pacijent, datum, sat, minut, lekar);
        }
        
        private PrikazPregleda SetPrikaz()
        {
            PrikazPregleda prikaz = new PrikazPregleda
            {
                Id = prikazPregleda.Id,
                Pacijent = prikazPregleda.Pacijent,
                Datum = DobijDatumPregleda(),
                Zavrsen = false,
                Trajanje = 30,
                Lekar = DobijLekara(DobijImeLekara(), DobijPrezimeLekara()),
                Prostorija = DobijProstoriju()
            };
            return prikaz;
        }

        private Pregled SetPregled(PrikazPregleda prikaz)
        {
            Pregled pregled = new Pregled
            {
                Id = prikaz.Id,
                Datum = prikaz.Datum,
                Trajanje = prikaz.Trajanje,
                Zavrsen = prikaz.Zavrsen,
                Anamneza = prikazPregleda.Anamneza,
                Lekar = prikaz.Lekar,
                Prostorija = prikaz.Prostorija,
                Pacijent = prikaz.Pacijent
            };
            return pregled;
        }

        private DateTime DobijDatumPregleda()
        {
            DateTime datum = (DateTime)datumPicker.SelectedDate;
            int dan = datum.Day;
            int mesec = datum.Month;
            int godina = datum.Year;
            int sat = comboSat.SelectedIndex;
            int minut = comboMinut.SelectedIndex * 15;
            DateTime datumPregleda = new DateTime(godina, mesec, dan, sat, minut, 0);
            return datumPregleda;
        }

        private Lekar DobijLekara(string ime, string prezime)
        {
            Lekar lekar = new Lekar();
            foreach (Lekar l in lekari)
            {
                if (ime.Equals(l.Ime) && prezime.Equals(l.Prezime))
                {
                    lekar = l;
                    break;
                }
            }
            return lekar;
        }

        private string DobijImeLekara()
        {
            string lekar = comboLekar.Text;
            String[] splited = lekar.Split(" ");
            string ime = splited[0];
            return ime;
        }

        private string DobijPrezimeLekara()
        {
            string lekar = comboLekar.Text;
            String[] splited = lekar.Split(" ");
            string prezime = splited[1];
            return prezime;
        }

        private Prostorija DobijProstoriju()
        {
            prostorije = storageProstorije.GetAllProstorije();
            foreach (Prostorija prostorija in prostorije)
            {
                if (prostorija.TipProstorije.Equals(TipProstorije.salaZaPreglede) && !prostorija.Obrisana && !NaRenoviranju(prostorija))
                {
                    return prostorija;
                }
            }
            return null;
        }

        private bool NaRenoviranju(Prostorija p)
        {
            List<Renoviranje> renoviranja = storageRenoviranje.GetAll();
            foreach (Renoviranje r in renoviranja)
            {
                if (p.BrojProstorije.Equals(r.Prostorija.BrojProstorije))
                {
                    if (r.PocetakRenoviranja.Date <= datumPicker.SelectedDate.Value && datumPicker.SelectedDate.Value <= r.KrajRenoviranja.Date)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool ProveriPopunjenostDatuma()
        {
            if (datumPicker.Text.Length == 0)
            {
                MessageBox.Show("Molimo Vas odaberite datum pregelda.");
                return false;
            }
            return true;
        }
        private bool ProveriPopunjenostSati()
        {
            if (comboSat.Text.Length == 0)
            {
                MessageBox.Show("Molimo Vas odaberite vreme pregleda (sat).");
                return false;
            }
            return true;
        }

        private bool ProveriPopunjenostMinuta()
        {
            if (comboMinut.Text.Length == 0)
            {
                MessageBox.Show("Molimo Vas odaberite vreme pregleda (minut).");
                return false;
            }
            return true;
        }

        private bool ProveriPopunjenostLekara()
        {
            if (comboLekar.Text.Length == 0)
            {
                MessageBox.Show("Molimo Vas odaberite zeljenog lekara.");
                return false;
            }
            return true;
        }
    }
}
