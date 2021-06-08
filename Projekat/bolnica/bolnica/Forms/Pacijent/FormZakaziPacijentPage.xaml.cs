using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.Model.Prostorije;
using Bolnica.Repository.Korisnici;
using Bolnica.Repository.Prostorije;
using Bolnica.ViewModel;
using Model.Korisnici;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormZakaziPacijentPage.xaml
    /// </summary>
    public partial class FormZakaziPacijentPage : Page
    {
        public FormZakaziPacijentPage(ZakaziPregledPacijentViewModel zakaziPregledPacijentViewModel)
        {
            InitializeComponent();

            this.DataContext = zakaziPregledPacijentViewModel;
            FormPacijentWeb.Forma.Pocetna.Content = this;
        }

        /*private void Potvrdi(object sender, RoutedEventArgs e)
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
                DateTime datumPregleda = new DateTime(godina, mesec, dan, sat, minut, 0);

                string imeLekara = comboLekar.Text;
                String[] splited = imeLekara.Split(" ");
                string ime = splited[0];
                string prezime = splited[1];

                Lekar lekar = new Lekar();
                foreach (Lekar l in lekari)
                {
                    if (ime.Equals(l.Ime) && prezime.Equals(l.Prezime))
                    {
                        lekar = l;
                        break;
                    }
                }

                PrikazPregleda prikaz = new PrikazPregleda
                {
                    Datum = datumPregleda,
                    Lekar = lekar,
                    Trajanje = 15,
                    Zavrsen = false,
                    Pacijent = pacijent
                };

                FileRepositoryProstorija storageProstorije = new FileRepositoryProstorija();
                List<Prostorija> prostorije = storageProstorije.GetAll();

                bool slobodna = false;
                foreach (Prostorija p in prostorije)
                {
                    if (p.TipProstorije.Equals(TipProstorije.salaZaPreglede) && !p.Obrisana && !NaRenoviranju(p))
                    {
                        prikaz.Prostorija = p;
                        slobodna = true;
                        break;
                    }
                }
                if (!slobodna)
                {
                    MessageBox.Show("U izabranom terminu nema slobodnih sala za pregled! Molimo Vas odaberite neki drugi termin.");
                    datumPicker.IsEnabled = true;
                    datumPicker.Background = Brushes.Aqua;
                }
                else
                {
                    pregledi = storagePregledi.GetAll();
                    int max = 0;
                    foreach (Pregled p in pregledi)
                    {
                        if (p.Id > max)
                        {
                            max = p.Id;
                        }
                    }
                    prikaz.Id = max + 1;

                    PacijentPageViewModel.PrikazNezavrsenihPregleda.Add(prikaz);

                    Pregled pregled = new Pregled
                    {
                        Id = prikaz.Id,
                        Lekar = prikaz.Lekar,
                        Pacijent = prikaz.Pacijent,
                        Prostorija = prikaz.Prostorija,
                        Datum = prikaz.Datum,
                        Trajanje = prikaz.Trajanje,
                        Zavrsen = prikaz.Zavrsen
                    };
                    pregled.Anamneza.Id = -1;

                    storagePregledi.Save(pregled);

                    AntiTrol antiTrol = new AntiTrol
                    {
                        Id = DobijIdAntiTrol(),
                        Pacijent = prikaz.Pacijent,
                        Datum = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
                    };
                    storageAntiTrol.Save(antiTrol);

                    PacijentPageViewModel pacijentPageViewModel = new PacijentPageViewModel(prikaz.Pacijent);
                    FormPacijentWeb.Forma.Pocetna.Content = new FormPacijentPage(pacijentPageViewModel*prikaz.Pacijent*);
                }
            }
        }

        private int DobijIdAntiTrol()
        {
            List<AntiTrol> antiTrolList = storageAntiTrol.GetAll();
            int max = 0;
            foreach (AntiTrol antiTrol in antiTrolList)
            {
                if (antiTrol.Id > max)
                {
                    max = antiTrol.Id;
                }
            }
            return max + 1;
        }

        private void Otkazi(object sender, RoutedEventArgs e)
        {
            PacijentPageViewModel pacijentPageViewModel = new PacijentPageViewModel(pacijent);
            FormPacijentWeb.Forma.Pocetna.Content = new FormPacijentPage(pacijentPageViewModel*pacijent*);
        }

        private void NasiPredlozi(object sender, RoutedEventArgs e)
        {
            DateTime datum = new DateTime(1, 1, 1);
            int sat = -1;
            int minut = -1;
            string imeLekara;
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
                foreach (Lekar l in lekari)
                {
                    if (ime.Equals(l.Ime) && prezime.Equals(l.Prezime))
                    {
                        lekar = l;
                        break;
                    }
                }
            }
            FormPacijentWeb.Forma.Pocetna.Content = new FormNasiPredloziPage(pacijent, datum, sat, minut, lekar);
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
        }*/

        private void RadioButton_Checked_Datum(object sender, RoutedEventArgs e)
        {
            datumPicker.IsEnabled = true;
            datumPicker.Focus();
            datumPicker.Background = Brushes.Aqua;
            comboSat.IsEnabled = false;
            comboMinut.IsEnabled = false;
            comboLekar.IsEnabled = false;
            potvrdi.IsEnabled = false;
            nasiPredlozi.IsEnabled = true;
            datum.IsEnabled = false;
            lekar.IsEnabled = false;
        }

        private void RadioButton_Checked_Lekar(object sender, RoutedEventArgs e)
        {
            comboLekar.IsEnabled = true;
            comboLekar.Focus();
            comboLekar.Background = Brushes.Aqua;
            datumPicker.IsEnabled = false;
            comboSat.IsEnabled = false;
            comboMinut.IsEnabled = false;
            potvrdi.IsEnabled = false;
            nasiPredlozi.IsEnabled = true;
            datum.IsEnabled = false;
            lekar.IsEnabled = false;
        }

        private void SelectedDateChanged_Datum(object sender, SelectionChangedEventArgs e)
        {
            comboSat.IsEnabled = true;
            comboMinut.IsEnabled = false;
            datumPicker.IsEnabled = false;
            datumPicker.FontWeight = FontWeights.UltraBold;
            datumPicker.Background = Brushes.Green;
            datumPicker.Foreground = Brushes.Green;
            comboSat.Background = Brushes.Aqua;
        }

        private void SelectionChanged_Sat(object sender, SelectionChangedEventArgs e)
        {
            comboSat.IsEnabled = false;
            comboMinut.IsEnabled = false;
            datumPicker.IsEnabled = false;
            comboSat.Foreground = Brushes.Green;
            comboMinut.IsEnabled = true;
            comboMinut.Background = Brushes.Aqua;
        }

        private void SelectionChanged_Minut(object sender, SelectionChangedEventArgs e)
        {
            comboSat.IsEnabled = false;
            comboMinut.IsEnabled = false;
            datumPicker.IsEnabled = false;
            comboMinut.Foreground = Brushes.Green;
            if (datum.IsChecked == false)
            {
                potvrdi.IsEnabled = true;
            }
            else
            {
                comboLekar.IsEnabled = true;
                comboLekar.Background = Brushes.Aqua;
            }
        }

        private void SelectionChanged_Lekar(object sender, SelectionChangedEventArgs e)
        {
            if (!(comboLekar.SelectedItem is null))
            {
                comboLekar.IsEnabled = false;
                comboLekar.Background = Brushes.Green;
                comboLekar.Foreground = Brushes.Green;
                if (lekar.IsChecked == false)
                {
                    potvrdi.IsEnabled = true;
                }
                else
                {
                    datumPicker.IsEnabled = true;
                    datumPicker.Background = Brushes.Aqua;
                }
            }
        }
    }
}
