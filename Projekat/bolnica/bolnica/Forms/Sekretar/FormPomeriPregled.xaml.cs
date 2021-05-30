using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.Repository.Pregledi;
using Bolnica.Repository.Prostorije;
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
        private List<Godisnji> godisnji = new List<Godisnji>();
        private FileRepositoryPacijent sviPacijenti = new FileRepositoryPacijent();
        private FileRepositoryLekar sviLekari = new FileRepositoryLekar();
        private FileRepositoryProstorija sveProstorije = new FileRepositoryProstorija();
        private FileRepositoryPregled sviPregledi = new FileRepositoryPregled();
        private FileRepositoryOperacija sveOperacije = new FileRepositoryOperacija();
        private FileRepositoryGodisnji sviGodisnji = new FileRepositoryGodisnji();
        private List<Pacijent> pacijenti = new List<Pacijent>();
        private List<Prostorija> prostorije = new List<Prostorija>();
        private PrikazPregleda trenutniPregled = new PrikazPregleda();
        private PrikazPregleda stariPregled = new PrikazPregleda();
        public FormPomeriPregled(PrikazPregleda pp)
        {
            InitializeComponent();
            trenutniPregled = pp;
            stariPregled = pp;

            this.DataContext = this;

            godisnji = sviGodisnji.GetAll();
            lekari = sviLekari.GetAll();
            pacijenti = sviPacijenti.GetAll();
            prostorije = sveProstorije.GetAll();

            txtTrajanje.Text = trenutniPregled.Trajanje.ToString();
            txtTrajanje.IsEnabled = false;

            foreach (Prostorija p in prostorije)
                if(!p.Obrisana)
                    comboProstorija.Items.Add(p.BrojProstorije);

            foreach (Lekar l in lekari)
                if(l.PostavljenaSmena)
                    comboLekar.Items.Add(l.Ime + " " + l.Prezime + " " + l.Jmbg);

            for (int vre = 0; vre < 24; vre++)
            {
                for (int min = 0; min < 60;)
                {
                    TimeSpan ts = new TimeSpan(vre, min, 0);
                    min = min + 15;
                    comboVreme.Items.Add(string.Format("{0:00}:{1:00}", ts.Hours, ts.Minutes));
                }

            }

            dpDatum.SelectedDate = trenutniPregled.Datum;
            comboLekar.Text = trenutniPregled.Lekar.Ime + " " + trenutniPregled.Lekar.Prezime + " " + trenutniPregled.Lekar.Jmbg;
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
            comboVreme.Text = satiString + ":" + minutaString;
            comboPacijent.Text = trenutniPregled.Pacijent.Ime + " " + trenutniPregled.Pacijent.Prezime + " " + trenutniPregled.Pacijent.Jmbg;
            comboPacijent.IsEnabled = false;
            comboProstorija.Text = trenutniPregled.Prostorija.BrojProstorije.ToString();
        }

        private void PomeriTermin(object sender, RoutedEventArgs e)
        {
            DateTime datum = (DateTime)dpDatum.SelectedDate;
            int godina = datum.Year;
            int mesec = datum.Month;
            int dan = datum.Day;
            int sati = Int32.Parse(comboVreme.Text.Split(":")[0]);
            int minuti = Int32.Parse(comboVreme.Text.Split(":")[1]);
            trenutniPregled.Datum = new DateTime(godina, mesec, dan, sati, minuti, 0);
            trenutniPregled.Trajanje = int.Parse(txtTrajanje.Text);

            string imeLekara;
            string prezimeLekara;
            string jmbgLekara;
            try
            {
                imeLekara = comboLekar.Text.Split(" ")[0];
                prezimeLekara = comboLekar.Text.Split(" ")[1];
                jmbgLekara = comboLekar.Text.Split(" ")[2];
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Nepostojeći lekar", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                comboLekar.Focusable = true;
                Keyboard.Focus(comboLekar);
                return;
            }

            string imePacijenta = comboPacijent.Text.Split(" ")[0];
            string prezimePacijenta = comboPacijent.Text.Split(" ")[1];
            string jmbgPacijenta = comboPacijent.Text.Split(" ")[2];

            bool lekarSet = false;
            for (int i = 0; i < lekari.Count; i++)
            {
                if (lekari[i].Ime.Equals(imeLekara) && lekari[i].Prezime.Equals(prezimeLekara) && lekari[i].Jmbg.Equals(jmbgLekara))
                {
                    trenutniPregled.Lekar = lekari[i];
                    lekarSet = true;
                    break;
                }
            }
            if (!lekarSet)
            {
                MessageBox.Show("Nepostojeći lekar", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                comboLekar.Focusable = true;
                Keyboard.Focus(comboLekar);
                return;
            }


            for (int i = 0; i < pacijenti.Count; i++)
            {
                if (pacijenti[i].Ime == imePacijenta && pacijenti[i].Prezime == prezimePacijenta && pacijenti[i].Jmbg == jmbgPacijenta)
                {
                    trenutniPregled.Pacijent = pacijenti[i];
                    break;
                }
            }

            bool prostorijaSet = false;
            for (int i = 0; i < prostorije.Count; i++)
            {
                if(!prostorije[i].Obrisana)
                    if (prostorije[i].BrojProstorije.ToString().Equals(comboProstorija.Text))
                    {
                        trenutniPregled.Prostorija = prostorije[i];
                        prostorijaSet = true;
                        break;
                    }
            }
            if (!prostorijaSet)
            {
                MessageBox.Show("Nepostojeća prostorija", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                comboProstorija.Focusable = true;
                Keyboard.Focus(comboProstorija);
                return;
            }

            for (int i = 0; i < godisnji.Count; i++)
                if (godisnji[i].Lekar.Jmbg == trenutniPregled.Lekar.Jmbg && (godisnji[i].PocetakGodisnjeg <= dpDatum.SelectedDate.Value && godisnji[i].KrajGodisnjeg >= dpDatum.SelectedDate.Value))
                {
                    MessageBox.Show("Lekar je na godišnjem tog datuma", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    comboLekar.Focusable = true;
                    Keyboard.Focus(comboLekar);
                    return;
                }

            if (trenutniPregled.Lekar.Smena == Smena.Prva && (sati < 7 || sati >= 15))
            {
                MessageBox.Show("Lekar nije u smeni", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                comboLekar.Focusable = true;
                Keyboard.Focus(comboLekar);
                return;
            }
            else if (trenutniPregled.Lekar.Smena == Smena.Druga && (sati < 15 || sati >= 23))
            {
                MessageBox.Show("Lekar nije u smeni", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                comboLekar.Focusable = true;
                Keyboard.Focus(comboLekar);
                return;
            }
            else if (trenutniPregled.Lekar.Smena == Smena.Treca && !(sati >= 23 || sati < 7))
            {
                MessageBox.Show("Lekar nije u smeni", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                comboLekar.Focusable = true;
                Keyboard.Focus(comboLekar);
                return;
            }

            if (PacijentZauzet(trenutniPregled.Id, trenutniPregled.Pacijent, trenutniPregled.Datum, trenutniPregled.Trajanje))
            {
                MessageBox.Show("Pacijent je zauzet u tom terminu", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                comboPacijent.Focusable = true;
                Keyboard.Focus(comboPacijent);
                return;
            }

            if (LekarZauzet(trenutniPregled.Id, trenutniPregled.Lekar, trenutniPregled.Datum, trenutniPregled.Trajanje))
            {
                MessageBox.Show("Lekar je zauzet u tom terminu", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                comboLekar.Focusable = true;
                Keyboard.Focus(comboLekar);
                return;
            }

            if (ProstorijaZauzeta(trenutniPregled.Id, trenutniPregled.Prostorija, trenutniPregled.Datum, trenutniPregled.Trajanje))
            {
                MessageBox.Show("Prostorija je zauzeta u tom terminu", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                comboProstorija.Focusable = true;
                Keyboard.Focus(comboProstorija);
                return;
            }

            for (int i = 0; i < FormPregledi.listaPregleda.Count; i++)
            {
                if (FormPregledi.listaPregleda[i].Equals(stariPregled))
                {
                    FormPregledi.listaPregleda[i].Id = trenutniPregled.Id;
                    FormPregledi.listaPregleda[i].Lekar = trenutniPregled.Lekar;
                    FormPregledi.listaPregleda[i].Pacijent = trenutniPregled.Pacijent;
                    FormPregledi.listaPregleda[i].Trajanje = trenutniPregled.Trajanje;
                    FormPregledi.listaPregleda[i].Zavrsen = trenutniPregled.Zavrsen;
                    FormPregledi.listaPregleda[i].Anamneza.Id = trenutniPregled.Anamneza.Id;
                    FormPregledi.listaPregleda[i].Prostorija = trenutniPregled.Prostorija;
                    FormPregledi.listaPregleda[i].Datum = trenutniPregled.Datum;
                    FormPregledi.listaPregleda[i].Hitan = trenutniPregled.Hitan;
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
                    p.Lekar = trenutniPregled.Lekar;
                    p.Pacijent = trenutniPregled.Pacijent;
                    p.Trajanje = trenutniPregled.Trajanje;
                    p.Zavrsen = trenutniPregled.Zavrsen;
                    p.Anamneza.Id = trenutniPregled.Anamneza.Id;
                    p.Prostorija = trenutniPregled.Prostorija;
                    p.Datum = trenutniPregled.Datum;
                    p.Hitan = trenutniPregled.Hitan;
                    sviPregledi.Update(p);
                    FormPregledi.Pregledi.Add(trenutniPregled);
                }

            }
            this.Close();
        }

        private bool ProstorijaZauzeta(int id, Prostorija prostorija, DateTime datum, int trajanje)
        {
            bool zauzet = false;
            List<Pregled> pregledi = new List<Pregled>();
            List<Pregled> preglediProstorije = new List<Pregled>();
            List<Operacija> operacije = new List<Operacija>();
            List<Operacija> operacijeProstorije = new List<Operacija>();
            pregledi = sviPregledi.GetAll();
            operacije = sveOperacije.GetAll();

            foreach (Pregled p in pregledi)
                if (p.Prostorija.BrojProstorije.Equals(prostorija.BrojProstorije))
                    preglediProstorije.Add(p);

            foreach (Pregled p in preglediProstorije)
            {
                if (p.Id == id)
                    continue;

                if (DateTime.Compare(datum, p.Datum) >= 0 && DateTime.Compare(datum, p.Datum.AddMinutes(trajanje)) < 0)
                {
                    zauzet = true;
                    break;
                }

                if (DateTime.Compare(datum, p.Datum) <= 0 && DateTime.Compare(datum.AddMinutes(trajanje), p.Datum) > 0)
                {
                    zauzet = true;
                    break;
                }
            }

            foreach (Operacija o in operacije)
                if (o.Prostorija.BrojProstorije.Equals(prostorija.BrojProstorije))
                    operacijeProstorije.Add(o);

            foreach (Operacija o in operacijeProstorije)
            {
                if (DateTime.Compare(datum, o.Datum) >= 0 && DateTime.Compare(datum, o.Datum.AddMinutes(o.Trajanje)) < 0)
                {
                    zauzet = true;
                    break;
                }

                if (DateTime.Compare(datum, o.Datum) <= 0 && DateTime.Compare(datum.AddMinutes(trajanje), o.Datum) > 0)
                {
                    zauzet = true;
                    break;
                }
            }

            return zauzet;
        }

        private bool LekarZauzet(int id, Lekar lekar, DateTime datum, int trajanje)
        {
            bool zauzet = false;
            List<Pregled> pregledi = new List<Pregled>();
            List<Pregled> preglediLekara = new List<Pregled>();
            List<Operacija> operacije = new List<Operacija>();
            List<Operacija> operacijeLekara = new List<Operacija>();
            pregledi = sviPregledi.GetAll();
            operacije = sveOperacije.GetAll();

            foreach (Pregled p in pregledi)
                if (p.Lekar.Jmbg.Equals(lekar.Jmbg))
                    preglediLekara.Add(p);

            foreach (Pregled p in preglediLekara)
            {
                if (p.Id == id)
                    continue;

                if (DateTime.Compare(datum, p.Datum) >= 0 && DateTime.Compare(datum, p.Datum.AddMinutes(trajanje)) < 0)
                {
                    zauzet = true;
                    break;
                }

                if (DateTime.Compare(datum, p.Datum) <= 0 && DateTime.Compare(datum.AddMinutes(trajanje), p.Datum) > 0)
                {
                    zauzet = true;
                    break;
                }
            }

            foreach (Operacija o in operacije)
                if (o.Lekar.Jmbg.Equals(lekar.Jmbg))
                    operacijeLekara.Add(o);

            foreach (Operacija o in operacijeLekara)
            {
                if (DateTime.Compare(datum, o.Datum) >= 0 && DateTime.Compare(datum, o.Datum.AddMinutes(o.Trajanje)) < 0)
                {
                    zauzet = true;
                    break;
                }

                if (DateTime.Compare(datum, o.Datum) <= 0 && DateTime.Compare(datum.AddMinutes(trajanje), o.Datum) > 0)
                {
                    zauzet = true;
                    break;
                }
            }

            return zauzet;
        }

        private bool PacijentZauzet(int id, Pacijent pacijent, DateTime datum, int trajanje)
        {
            bool zauzet = false;
            List<Pregled> pregledi = new List<Pregled>();
            List<Pregled> preglediPacijenta = new List<Pregled>();
            List<Operacija> operacije = new List<Operacija>();
            List<Operacija> operacijePacijenta = new List<Operacija>();
            pregledi = sviPregledi.GetAll();
            operacije = sveOperacije.GetAll();

            foreach (Pregled p in pregledi)
                if (p.Pacijent.Jmbg.Equals(pacijent.Jmbg))
                    preglediPacijenta.Add(p);

            foreach (Pregled p in preglediPacijenta)
            {
                if (p.Id == id)
                    continue;

                if (DateTime.Compare(datum, p.Datum) >= 0 && DateTime.Compare(datum, p.Datum.AddMinutes(trajanje)) < 0)
                {
                    zauzet = true;
                    break;
                }

                if (DateTime.Compare(datum, p.Datum) <= 0 && DateTime.Compare(datum.AddMinutes(trajanje), p.Datum) > 0)
                {
                    zauzet = true;
                    break;
                }
            }

            foreach (Operacija o in operacije)
                if (o.Pacijent.Jmbg.Equals(pacijent.Jmbg))
                    operacijePacijenta.Add(o);

            foreach (Operacija o in operacijePacijenta)
            {
                if (DateTime.Compare(datum, o.Datum) >= 0 && DateTime.Compare(datum, o.Datum.AddMinutes(o.Trajanje)) < 0)
                {
                    zauzet = true;
                    break;
                }

                if (DateTime.Compare(datum, o.Datum) <= 0 && DateTime.Compare(datum.AddMinutes(trajanje), o.Datum) > 0)
                {
                    zauzet = true;
                    break;
                }
            }

            return zauzet;
        }

        private void Zatvori(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}