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
    /// Interaction logic for FormZakaziPregled.xaml
    /// </summary>
    public partial class FormZakaziPregled : Window
    {
        private List<Lekar> lekari = new List<Lekar>();
        private FileStoragePacijenti sviPacijenti = new FileStoragePacijenti();
        private FileStorageProstorija sveProstorije = new FileStorageProstorija();
        private FileStoragePregledi sviPregledi = new FileStoragePregledi();
        private List<Pacijent> pacijenti = new List<Pacijent>();
        private List<Prostorija> prostorije = new List<Prostorija>();

        public FormZakaziPregled(List<Lekar> lekari)
        {
            InitializeComponent();
            this.lekari = lekari;

            this.DataContext = this;

            pacijenti = sviPacijenti.GetAll();
            prostorije = sveProstorije.GetAllProstorije();

            for (int vre = 0; vre < 24; vre++)
            {
                for (int min = 0; min < 60;)
                {
                    TimeSpan ts = new TimeSpan(vre, min, 0);
                    min = min + 15;
                    comboVreme.Items.Add(ts);
                }

            }

            dpDatum.SelectedDate = DateTime.Now;
            txtTrajanje.Text = "30";
            txtTrajanje.IsEnabled = false;
        }

        private void ZakaziTermin(object sender, RoutedEventArgs e)
        {
            PrikazPregleda trenutniPregled = new PrikazPregleda();
            DateTime datum = (DateTime)dpDatum.SelectedDate;
            int godina = datum.Year;
            int mesec = datum.Month;
            int dan = datum.Day;
            string sati = comboVreme.Text.Split(":")[0];
            string minuti = comboVreme.Text.Split(":")[1];
            trenutniPregled.Datum = new DateTime(godina, mesec, dan, Int32.Parse(sati), Int32.Parse(minuti), 0);
            trenutniPregled.Trajanje = int.Parse(txtTrajanje.Text);
            trenutniPregled.Zavrsen = false;

            for (int i = 0; i < lekari.Count; i++)
            {
                if (lekari[i].Ime.Equals(txtImeLekara.Text) && lekari[i].Prezime.Equals(txtPrezimeLekara.Text) && lekari[i].Mbr.Equals(Int32.Parse(txtMbrLekara.Text)))
                {
                    trenutniPregled.Lekar = lekari[i];
                    break;
                }
            }
            if(trenutniPregled.Lekar == null)
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

            List<Pregled> pregledi = new List<Pregled>();
            pregledi = sviPregledi.GetAllPregledi();
            int max = 0;
            for (int i = 0; i < pregledi.Count; i++)
            {
                if (pregledi[i].Id > max)
                    max = pregledi[i].Id;
            }
            trenutniPregled.Id = max + 1;
            Pregled p = new Pregled();
            p.Id = trenutniPregled.Id;
            p.lekarJmbg = trenutniPregled.Lekar.Jmbg;
            p.pacijentJmbg = trenutniPregled.Pacijent.Jmbg;
            p.Trajanje = trenutniPregled.Trajanje;
            p.Zavrsen = trenutniPregled.Zavrsen;
            p.AnamnezaId = -1;
            p.brojProstorije = trenutniPregled.Prostorija.BrojProstorije;
            p.Datum = trenutniPregled.Datum;
                
            FormPregledi.listaPregleda.Add(p);
            FormPregledi.Pregledi.Add(trenutniPregled);
            sviPregledi.Save(p);
            this.Close();
        }
    }
}
