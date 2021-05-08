using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
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
        private Pacijent pacijent = new Pacijent();
        private FileStorageLekar storageLekari = new FileStorageLekar();
        private List<Lekar> lekari = new List<Lekar>();

        private FormPacijentWeb form;

        public FormZakaziPacijentPage(Pacijent trenutniPacijent, FormPacijentWeb formPacijentWeb)
        {
            InitializeComponent();

            form = formPacijentWeb;

            datumPicker.IsEnabled = false;
            comboSat.IsEnabled = false;
            comboMinut.IsEnabled = false;
            comboLekar.IsEnabled = false;
            potvrdi.IsEnabled = false;
            nasiPredlozi.IsEnabled = false;

            lekari = storageLekari.GetAll();

            foreach (Lekar l in lekari)
            {
                comboLekar.Items.Add(l.Ime + " " + l.Prezime);
            }

            pacijent = trenutniPacijent;
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
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

                Lekar l = new Lekar();
                foreach (Lekar lek in lekari)
                {
                    if (ime.Equals(lek.Ime) && prezime.Equals(lek.Prezime))
                    {
                        l = lek;
                        break;
                    }
                }

                PrikazPregleda p = new PrikazPregleda();
                p.Datum = datumPregleda;
                p.Lekar = l;
                p.Trajanje = 15;
                p.Zavrsen = false;
                p.Pacijent = pacijent;

                FileStorageProstorija storageProstorije = new FileStorageProstorija();
                List<Prostorija> prostorije = storageProstorije.GetAllProstorije();

                bool slobodna = false;
                foreach (Prostorija pro in prostorije)
                {
                    if (pro.TipProstorije.Equals(TipProstorije.salaZaPreglede))
                    {
                        if (pro.Obrisana == false)
                        {
                            p.Prostorija = pro;
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
                    FileStoragePregledi sviPregledi = new FileStoragePregledi();
                    List<Pregled> zaId = sviPregledi.GetAllPregledi();
                    int max = 0;
                    for (int i = 0; i < zaId.Count; i++)
                    {
                        if (zaId[i].Id > max)
                            max = zaId[i].Id;
                    }
                    p.Id = max + 1;

                    FormPacijentPage.PrikazNezavrsenihPregleda.Add(p);

                    Pregled pre = new Pregled();
                    pre.Id = p.Id;
                    pre.Lekar = p.Lekar;
                    pre.Pacijent = p.Pacijent;
                    pre.Prostorija = p.Prostorija;
                    pre.Anamneza.Id = -1;
                    pre.Datum = p.Datum;
                    pre.Trajanje = p.Trajanje;
                    pre.Zavrsen = p.Zavrsen;

                    FileStoragePregledi storagePregledi = new FileStoragePregledi();
                    storagePregledi.Save(pre);

                    FileStorageAntiTrol storageAntiTrol = new FileStorageAntiTrol();
                    AntiTrol antiTrol = new AntiTrol();
                    antiTrol.PacijentJMBG = p.Pacijent.Jmbg;
                    antiTrol.Datum = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    storageAntiTrol.Save(antiTrol);

                    form.Pocetna.Content = new FormPacijentPage(p.Pacijent, form);
                }
            }
        }

        private void Otkazi(object sender, RoutedEventArgs e)
        {
            form.Pocetna.Content = new FormPacijentPage(pacijent, form);
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

            var s = new FormNasiPredlozi(pacijent, datum, sat, minut, lekar);
            s.Show();
        }

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
