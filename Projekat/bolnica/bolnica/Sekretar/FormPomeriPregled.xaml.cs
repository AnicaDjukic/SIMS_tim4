using Bolnica.Model.Pregledi;
using Model.Korisnici;
using Model.Pacijenti;
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

namespace Bolnica.Sekretar
{
    /// <summary>
    /// Interaction logic for FormPomeriPregled.xaml
    /// </summary>
    public partial class FormPomeriPregled : Window
    {
        private List<Lekar> lekari = new List<Lekar>();
        private FileStoragePacijenti sviPacijenti = new FileStoragePacijenti();
        private FileStorageProstorija sveProstorije = new FileStorageProstorija();
        private FileStoragePregledi sviPregledi = new FileStoragePregledi();
        private List<Pacijent> pacijenti = new List<Pacijent>();
        private List<Prostorija> prostorije = new List<Prostorija>();
        private PrikazPregleda trenutniPregled = new PrikazPregleda();
        private PrikazPregleda stariPregled = new PrikazPregleda();
        public FormPomeriPregled(PrikazPregleda pp, List<Lekar> lekari)
        {
            InitializeComponent();
            trenutniPregled = pp;
            this.lekari = lekari;
            stariPregled = pp;

            this.DataContext = this;

            pacijenti = sviPacijenti.GetAll();
            prostorije = sveProstorije.GetAllProstorije();

            txtTrajanje.Text = trenutniPregled.Trajanje.ToString();
            txtTrajanje.IsEnabled = false;

            for (int vre = 0; vre < 24; vre++)
            {
                for (int min = 0; min < 60;)
                {
                    TimeSpan ts = new TimeSpan(vre, min, 0);
                    min = min + 15;
                    comboVreme.Items.Add(ts);
                }

            }

            dpDatum.SelectedDate = trenutniPregled.Datum;
            txtImeLekara.Text = trenutniPregled.Lekar.Ime;
            txtPrezimeLekara.Text = trenutniPregled.Lekar.Prezime;
            txtMbrLekara.Text = trenutniPregled.Lekar.Mbr.ToString();
            string[] s = trenutniPregled.Datum.ToString().Split(" ");
            int sati = trenutniPregled.Datum.Hour;
            string satiString;
            if (sati > 9)
                satiString = sati.ToString();
            else
                satiString = "0" + sati.ToString();
            int minuta = trenutniPregled.Datum.Minute;
            string minutaString;
            if (minuta == 0)
                minutaString = "00";
            else
                minutaString = minuta.ToString();
            comboVreme.Text = satiString + ":" + minutaString + ":00"; 
            txtImePacijenta.Text = trenutniPregled.Pacijent.Ime;
            txtImePacijenta.IsEnabled = false;
            txtPrezimePacijenta.Text = trenutniPregled.Pacijent.Prezime;
            txtPrezimePacijenta.IsEnabled = false;
            txtJmbgPacijenta.Text = trenutniPregled.Pacijent.Jmbg;
            txtJmbgPacijenta.IsEnabled = false;
            txtProstorija.Text = trenutniPregled.Prostorija.BrojProstorije.ToString();
        }

        private void PomeriTermin(object sender, RoutedEventArgs e)
        {
            DateTime datum = (DateTime)dpDatum.SelectedDate;
            int godina = datum.Year;
            int mesec = datum.Month;
            int dan = datum.Day;
            string sati = comboVreme.Text.Split(":")[0];
            string minuti = comboVreme.Text.Split(":")[1];
            trenutniPregled.Datum = new DateTime(godina, mesec, dan, Int32.Parse(sati), Int32.Parse(minuti), 0);
            trenutniPregled.Trajanje = int.Parse(txtTrajanje.Text);

            for (int i = 0; i < lekari.Count; i++)
            {
                if (lekari[i].Ime.Equals(txtImeLekara.Text) && lekari[i].Prezime.Equals(txtPrezimeLekara.Text) && lekari[i].Mbr.Equals(Int32.Parse(txtMbrLekara.Text)))
                {
                    trenutniPregled.Lekar = lekari[i];
                    break;
                }
            }
            if (trenutniPregled.Lekar == null)
            {
                MessageBox.Show("Nepostojeći lekar", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                txtImeLekara.Focusable = true;
                Keyboard.Focus(txtImeLekara);
                return;
            }


            for (int i = 0; i < pacijenti.Count; i++)
            {
                if (pacijenti[i].Ime == txtImePacijenta.Text && pacijenti[i].Prezime == txtPrezimePacijenta.Text && pacijenti[i].Jmbg == txtJmbgPacijenta.Text)
                {
                    trenutniPregled.Pacijent = pacijenti[i];
                    break;
                }
            }
            if (trenutniPregled.Pacijent == null)
            {
                MessageBox.Show("Nepostojeći pacijent", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                txtImePacijenta.Focusable = true;
                Keyboard.Focus(txtImePacijenta);
                return;
            }

            for (int i = 0; i < prostorije.Count; i++)
            {
                if (prostorije[i].BrojProstorije.ToString().Equals(txtProstorija.Text))
                {
                    trenutniPregled.Prostorija = prostorije[i];
                    break;
                }
            }
            if (trenutniPregled.Prostorija == null)
            {
                MessageBox.Show("Nepostojeća prostorija", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                txtProstorija.Focusable = true;
                Keyboard.Focus(txtProstorija);
                return;
            }

            for (int i = 0; i < FormPregledi.listaPregleda.Count; i++)
            {
                if (FormPregledi.listaPregleda[i].Equals(stariPregled))
                {
                    FormPregledi.listaPregleda[i].Id = trenutniPregled.Id;
                    FormPregledi.listaPregleda[i].lekarJmbg = trenutniPregled.Lekar.Jmbg;
                    FormPregledi.listaPregleda[i].pacijentJmbg = trenutniPregled.Pacijent.Jmbg;
                    FormPregledi.listaPregleda[i].Trajanje = trenutniPregled.Trajanje;
                    FormPregledi.listaPregleda[i].Zavrsen = trenutniPregled.Zavrsen;
                    FormPregledi.listaPregleda[i].AnamnezaId = trenutniPregled.AnamnezaId;
                    FormPregledi.listaPregleda[i].brojProstorije = trenutniPregled.Prostorija.BrojProstorije;
                    FormPregledi.listaPregleda[i].Datum = trenutniPregled.Datum;
                    break;
                }
            }

            for (int i = 0; i < FormPregledi.Pregledi.Count; i++)
            {
                if (FormPregledi.Pregledi[i].Equals(stariPregled))
                {
                    FormPregledi.Pregledi.Remove(stariPregled);
                    Pregled p = new Pregled();
                    p.Id = trenutniPregled.Id;
                    p.lekarJmbg = trenutniPregled.Lekar.Jmbg;
                    p.pacijentJmbg = trenutniPregled.Pacijent.Jmbg;
                    p.Trajanje = trenutniPregled.Trajanje;
                    p.Zavrsen = trenutniPregled.Zavrsen;
                    p.AnamnezaId = trenutniPregled.AnamnezaId;
                    p.brojProstorije = trenutniPregled.Prostorija.BrojProstorije;
                    p.Datum = trenutniPregled.Datum;
                    sviPregledi.Izmeni(p);
                    FormPregledi.Pregledi.Add(trenutniPregled);
                }

            }
            this.Close();
        }
    }
}
