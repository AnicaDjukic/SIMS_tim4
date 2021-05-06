using Bolnica.Model.Korisnici;
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
        private FileStorageLekar sviLekari = new FileStorageLekar();
        private FileStorageProstorija sveProstorije = new FileStorageProstorija();
        private FileStoragePregledi sviPregledi = new FileStoragePregledi();
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

            lekari = sviLekari.GetAll();
            pacijenti = sviPacijenti.GetAll();
            prostorije = sveProstorije.GetAllProstorije();

            txtTrajanje.Text = trenutniPregled.Trajanje.ToString();
            txtTrajanje.IsEnabled = false;

            foreach (Prostorija p in prostorije)
                if(!p.Obrisana)
                    comboProstorija.Items.Add(p.BrojProstorije);

            foreach (Lekar l in lekari)
            {
                bool postoji = false;
                foreach (string cbi in comboImeLekara.Items)
                {
                    if (cbi.Equals(l.Ime))
                        postoji = true;
                }

                if (!postoji)
                    comboImeLekara.Items.Add(l.Ime);
            }

            foreach (Lekar l in lekari)
            {
                bool postoji = false;
                foreach (string cbi in comboPrezimeLekara.Items)
                {
                    if (cbi.Equals(l.Prezime))
                        postoji = true;
                }

                if (!postoji)
                    comboPrezimeLekara.Items.Add(l.Prezime);
            }

            foreach (Lekar l in lekari)
            {
                bool postoji = false;
                foreach (int cbi in comboMbrLekara.Items)
                {
                    if (cbi == l.Mbr)
                        postoji = true;
                }

                if (!postoji)
                    comboMbrLekara.Items.Add(l.Mbr);
            }

            foreach (Pacijent p in pacijenti)
            {
                bool postoji = false;
                foreach (string cbi in comboImePacijenta.Items)
                {
                    if (cbi.Equals(p.Ime))
                        postoji = true;
                }

                if (!postoji)
                    comboImePacijenta.Items.Add(p.Ime);
            }

            foreach (Pacijent p in pacijenti)
            {
                bool postoji = false;
                foreach (string cbi in comboPrezimePacijenta.Items)
                {
                    if (cbi.Equals(p.Prezime))
                        postoji = true;
                }

                if (!postoji)
                    comboPrezimePacijenta.Items.Add(p.Prezime);
            }

            foreach (Pacijent p in pacijenti)
            {
                bool postoji = false;
                foreach (string cbi in comboJmbgPacijenta.Items)
                {
                    if (cbi.Equals(p.Jmbg))
                        postoji = true;
                }

                if (!postoji)
                    comboJmbgPacijenta.Items.Add(p.Jmbg);
            }

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
            comboImeLekara.Text = trenutniPregled.Lekar.Ime;
            comboPrezimeLekara.Text = trenutniPregled.Lekar.Prezime;
            comboMbrLekara.Text = trenutniPregled.Lekar.Mbr.ToString();
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
            comboImePacijenta.Text = trenutniPregled.Pacijent.Ime;
            comboImePacijenta.IsEnabled = false;
            comboPrezimePacijenta.Text = trenutniPregled.Pacijent.Prezime;
            comboPrezimePacijenta.IsEnabled = false;
            comboJmbgPacijenta.Text = trenutniPregled.Pacijent.Jmbg;
            comboJmbgPacijenta.IsEnabled = false;
            comboProstorija.Text = trenutniPregled.Prostorija.BrojProstorije.ToString();
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
                if (lekari[i].Ime.Equals(comboImeLekara.Text) && lekari[i].Prezime.Equals(comboPrezimeLekara.Text) && lekari[i].Mbr.Equals(Int32.Parse(comboMbrLekara.Text)))
                {
                    trenutniPregled.Lekar = lekari[i];
                    break;
                }
            }
            if (trenutniPregled.Lekar == null)
            {
                MessageBox.Show("Nepostojeći lekar", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                comboImeLekara.Focusable = true;
                Keyboard.Focus(comboImeLekara);
                return;
            }


            for (int i = 0; i < pacijenti.Count; i++)
            {
                if (pacijenti[i].Ime == comboImePacijenta.Text && pacijenti[i].Prezime == comboPrezimePacijenta.Text && pacijenti[i].Jmbg == comboJmbgPacijenta.Text)
                {
                    trenutniPregled.Pacijent = pacijenti[i];
                    break;
                }
            }

            for (int i = 0; i < prostorije.Count; i++)
            {
                if (prostorije[i].BrojProstorije.ToString().Equals(comboProstorija.Text))
                {
                    trenutniPregled.Prostorija = prostorije[i];
                    break;
                }
            }
            if (trenutniPregled.Prostorija == null)
            {
                MessageBox.Show("Nepostojeća prostorija", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                comboProstorija.Focusable = true;
                Keyboard.Focus(comboProstorija);
                return;
            }

            if (PacijentZauzet(trenutniPregled.Id, trenutniPregled.Pacijent, trenutniPregled.Datum, trenutniPregled.Trajanje))
            {
                MessageBox.Show("Pacijent je zauzet u tom terminu", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                comboImePacijenta.Focusable = true;
                Keyboard.Focus(comboImePacijenta);
                return;
            }

            if (LekarZauzet(trenutniPregled.Id, trenutniPregled.Lekar, trenutniPregled.Datum, trenutniPregled.Trajanje))
            {
                MessageBox.Show("Lekar je zauzet u tom terminu", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                comboImeLekara.Focusable = true;
                Keyboard.Focus(comboImeLekara);
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
                    FormPregledi.listaPregleda[i].lekarJmbg = trenutniPregled.Lekar.Jmbg;
                    FormPregledi.listaPregleda[i].pacijentJmbg = trenutniPregled.Pacijent.Jmbg;
                    FormPregledi.listaPregleda[i].Trajanje = trenutniPregled.Trajanje;
                    FormPregledi.listaPregleda[i].Zavrsen = trenutniPregled.Zavrsen;
                    FormPregledi.listaPregleda[i].AnamnezaId = trenutniPregled.AnamnezaId;
                    FormPregledi.listaPregleda[i].brojProstorije = trenutniPregled.Prostorija.BrojProstorije;
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
                    p.lekarJmbg = trenutniPregled.Lekar.Jmbg;
                    p.pacijentJmbg = trenutniPregled.Pacijent.Jmbg;
                    p.Trajanje = trenutniPregled.Trajanje;
                    p.Zavrsen = trenutniPregled.Zavrsen;
                    p.AnamnezaId = trenutniPregled.AnamnezaId;
                    p.brojProstorije = trenutniPregled.Prostorija.BrojProstorije;
                    p.Datum = trenutniPregled.Datum;
                    p.Hitan = trenutniPregled.Hitan;
                    sviPregledi.Izmeni(p);
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
            pregledi = sviPregledi.GetAllPregledi();
            operacije = sviPregledi.GetAllOperacije();

            foreach (Pregled p in pregledi)
                if (p.brojProstorije.Equals(prostorija.BrojProstorije))
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
                if (o.brojProstorije.Equals(prostorija.BrojProstorije))
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
            pregledi = sviPregledi.GetAllPregledi();
            operacije = sviPregledi.GetAllOperacije();

            foreach (Pregled p in pregledi)
                if (p.lekarJmbg.Equals(lekar.Jmbg))
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
                if (o.lekarJmbg.Equals(lekar.Jmbg))
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
            pregledi = sviPregledi.GetAllPregledi();
            operacije = sviPregledi.GetAllOperacije();

            foreach (Pregled p in pregledi)
                if (p.pacijentJmbg.Equals(pacijent.Jmbg))
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
                if (o.pacijentJmbg.Equals(pacijent.Jmbg))
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

        private void comboProstorija_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                comboProstorija.IsDropDownOpen = true;
        }

        private void comboVreme_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                comboVreme.IsDropDownOpen = true;
        }

        private void comboImeLekara_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                comboImeLekara.IsDropDownOpen = true;
        }

        private void comboPrezimeLekara_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                comboPrezimeLekara.IsDropDownOpen = true;
        }

        private void comboMbrLekara_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                comboMbrLekara.IsDropDownOpen = true;
        }

        private void filterImeLekara(object sender, EventArgs e)
        {
            List<string> mogucaImena = new List<string>();
            bool neIzbacuj = false;
            List<string> zaIzbaciti = new List<string>();

            for (int i = comboImeLekara.Items.Count - 1; i >= 0; i--)
                comboImeLekara.Items.RemoveAt(i);

            foreach (Lekar l in lekari)
            {
                bool postoji = false;
                foreach (string cbi in comboImeLekara.Items)
                {
                    if (cbi.Equals(l.Ime))
                        postoji = true;
                }

                if (!postoji)
                    comboImeLekara.Items.Add(l.Ime);
            }

            if (!string.IsNullOrEmpty(comboPrezimeLekara.Text))
            {
                foreach (Lekar l in lekari)
                    if (l.Prezime.Equals(comboPrezimeLekara.Text))
                        mogucaImena.Add(l.Ime);

                foreach (string cbi in comboImeLekara.Items)
                {
                    neIzbacuj = false;
                    for (int j = 0; j < mogucaImena.Count; j++)
                    {
                        if (cbi.Equals(mogucaImena[j]))
                            neIzbacuj = true;
                    }

                    if (!neIzbacuj)
                        zaIzbaciti.Add(cbi);
                }

                for (int i = zaIzbaciti.Count - 1; i >= 0; i--)
                    comboImeLekara.Items.Remove(zaIzbaciti[i]);
            }

            neIzbacuj = false;
            mogucaImena.Clear();
            if (!string.IsNullOrEmpty(comboMbrLekara.Text))
            {
                foreach (Lekar l in lekari)
                    if (l.Mbr == Int32.Parse(comboMbrLekara.Text))
                        mogucaImena.Add(l.Ime);

                foreach (string cbi in comboImeLekara.Items)
                {
                    neIzbacuj = false;
                    for (int j = 0; j < mogucaImena.Count; j++)
                    {
                        if (cbi.Equals(mogucaImena[j]))
                            neIzbacuj = true;
                    }

                    if (!neIzbacuj)
                        zaIzbaciti.Add(cbi);
                }

                for (int i = zaIzbaciti.Count - 1; i >= 0; i--)
                    comboImeLekara.Items.Remove(zaIzbaciti[i]);
            }

            if (comboImeLekara.Items.Count == 0)
                comboImeLekara.IsDropDownOpen = false;
            else
                comboImeLekara.IsDropDownOpen = true;
        }

        private void filterPrezimeLekara(object sender, EventArgs e)
        {
            List<string> mogucaPrezimena = new List<string>();
            bool neIzbacuj = false;
            List<string> zaIzbaciti = new List<string>();

            for (int i = comboPrezimeLekara.Items.Count - 1; i >= 0; i--)
                comboPrezimeLekara.Items.RemoveAt(i);

            foreach (Lekar l in lekari)
            {
                bool postoji = false;
                foreach (string cbi in comboPrezimeLekara.Items)
                {
                    if (cbi.Equals(l.Prezime))
                        postoji = true;
                }

                if (!postoji)
                    comboPrezimeLekara.Items.Add(l.Prezime);
            }

            if (!string.IsNullOrEmpty(comboImeLekara.Text))
            {
                foreach (Lekar l in lekari)
                    if (l.Ime.Equals(comboImeLekara.Text))
                        mogucaPrezimena.Add(l.Prezime);

                foreach (string cbi in comboPrezimeLekara.Items)
                {
                    neIzbacuj = false;
                    for (int j = 0; j < mogucaPrezimena.Count; j++)
                    {
                        if (cbi.Equals(mogucaPrezimena[j]))
                            neIzbacuj = true;
                    }

                    if (!neIzbacuj)
                        zaIzbaciti.Add(cbi);
                }

                for (int i = zaIzbaciti.Count - 1; i >= 0; i--)
                    comboPrezimeLekara.Items.Remove(zaIzbaciti[i]);
            }

            neIzbacuj = false;
            mogucaPrezimena.Clear();
            if (!string.IsNullOrEmpty(comboMbrLekara.Text))
            {
                foreach (Lekar l in lekari)
                    if (l.Mbr == Int32.Parse(comboMbrLekara.Text))
                        mogucaPrezimena.Add(l.Prezime);

                foreach (string cbi in comboPrezimeLekara.Items)
                {
                    neIzbacuj = false;
                    for (int j = 0; j < mogucaPrezimena.Count; j++)
                    {
                        if (cbi.Equals(mogucaPrezimena[j]))
                            neIzbacuj = true;
                    }

                    if (!neIzbacuj)
                        zaIzbaciti.Add(cbi);
                }

                for (int i = zaIzbaciti.Count - 1; i >= 0; i--)
                    comboPrezimeLekara.Items.Remove(zaIzbaciti[i]);
            }

            if (comboPrezimeLekara.Items.Count == 0)
                comboPrezimeLekara.IsDropDownOpen = false;
            else
                comboPrezimeLekara.IsDropDownOpen = true;
        }

        private void filterMbrLekara(object sender, EventArgs e)
        {
            List<int> moguciMbr = new List<int>();
            bool neIzbacuj = false;
            List<int> zaIzbaciti = new List<int>();

            for (int i = comboMbrLekara.Items.Count - 1; i >= 0; i--)
                comboMbrLekara.Items.RemoveAt(i);

            foreach (Lekar l in lekari)
                comboMbrLekara.Items.Add(l.Mbr);

            if (!string.IsNullOrEmpty(comboImeLekara.Text))
            {
                foreach (Lekar l in lekari)
                    if (l.Ime.Equals(comboImeLekara.Text))
                        moguciMbr.Add(l.Mbr);

                foreach (int cbi in comboMbrLekara.Items)
                {
                    neIzbacuj = false;
                    for (int j = 0; j < moguciMbr.Count; j++)
                    {
                        if (cbi == moguciMbr[j])
                            neIzbacuj = true;
                    }

                    if (!neIzbacuj)
                        zaIzbaciti.Add(cbi);
                }

                for (int i = zaIzbaciti.Count - 1; i >= 0; i--)
                    comboMbrLekara.Items.Remove(zaIzbaciti[i]);
            }

            neIzbacuj = false;
            moguciMbr.Clear();
            if (!string.IsNullOrEmpty(comboPrezimeLekara.Text))
            {
                foreach (Lekar l in lekari)
                    if (l.Prezime.Equals(comboPrezimeLekara.Text))
                        moguciMbr.Add(l.Mbr);

                foreach (int cbi in comboMbrLekara.Items)
                {
                    neIzbacuj = false;
                    for (int j = 0; j < moguciMbr.Count; j++)
                    {
                        if (cbi == moguciMbr[j])
                            neIzbacuj = true;
                    }

                    if (!neIzbacuj)
                        zaIzbaciti.Add(cbi);
                }

                for (int i = zaIzbaciti.Count - 1; i >= 0; i--)
                    comboMbrLekara.Items.Remove(zaIzbaciti[i]);
            }

            if (comboMbrLekara.Items.Count == 0)
                comboMbrLekara.IsDropDownOpen = false;
            else
                comboMbrLekara.IsDropDownOpen = true;
        }
    }
}