using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.Sekretar;
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

namespace Bolnica.Forms.Sekretar
{
    /// <summary>
    /// Interaction logic for FormZakaziHitanTermin.xaml
    /// </summary>
    public partial class FormZakaziHitanTermin : Window
    {
        public static bool guest;
        private FileRepositoryLekar storageLekari;
        private List<Lekar> lekari;
        private FileRepositoryPacijent storagePacijenti;
        private List<Pacijent> pacijenti;
        private FileStorageProstorija storageProstorije;
        private List<Prostorija> prostorije;
        private FileRepositoryPregled sviPregledi;
        private DataGrid dataGrid;
        public FormZakaziHitanTermin(DataGrid dg)
        {
            InitializeComponent();
            comboTipOperacije.Visibility = Visibility.Hidden;
            lblTipOperacije.Visibility = Visibility.Hidden;
            txtTrajanje.Text = "30";
            txtTrajanje.IsEnabled = false;
            storageLekari = new FileRepositoryLekar();
            lekari = storageLekari.GetAll();
            storagePacijenti = new FileRepositoryPacijent();
            pacijenti = storagePacijenti.GetAll();
            storageProstorije = new FileStorageProstorija();
            prostorije = storageProstorije.GetAllProstorije();
            sviPregledi = new FileRepositoryPregled();
            dataGrid = dg;

            foreach (Pacijent p in pacijenti)
            {
                if(!p.Obrisan && !p.Guest)
                    comboPacijent.Items.Add(p.Ime + " " + p.Prezime + " " + p.Jmbg);
            }

            foreach (Lekar l in lekari)
                if (!comboSpecijalizacija.Items.Contains(l.Specijalizacija.OblastMedicine) && !l.Specijalizacija.OblastMedicine.Equals("Opsta"))
                    comboSpecijalizacija.Items.Add(l.Specijalizacija.OblastMedicine);
            comboSpecijalizacija.Items.Add("Opsta");

        }

        private void CheckBoxChecked(object sender, RoutedEventArgs e)
        {
            comboTipOperacije.Visibility = Visibility.Visible;
            lblTipOperacije.Visibility = Visibility.Visible;
            txtTrajanje.Text = "30";
            txtTrajanje.IsEnabled = true;
            comboTipOperacije.SelectedIndex = 2;
            comboSpecijalizacija.Items.Remove("Opsta");
        }

        private void CheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            comboTipOperacije.Visibility = Visibility.Hidden;
            lblTipOperacije.Visibility = Visibility.Hidden;
            txtTrajanje.Text = "30";
            txtTrajanje.IsEnabled = false;
            comboSpecijalizacija.Items.Add("Opsta");
        }

        private void Zatvori(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void KreirajGosta(object sender, RoutedEventArgs e)
        {
            FormDodajGosta s = new FormDodajGosta(comboPacijent);
            s.ShowDialog();
        }

        private void ZakaziHitanTermin(object sender, RoutedEventArgs e)
        {
            if ((bool)!checkOperacija.IsChecked) 
            {
                PrikazPregleda trenutniPregled = new PrikazPregleda();
                
                trenutniPregled.Trajanje = int.Parse(txtTrajanje.Text);
                trenutniPregled.Zavrsen = false;
                trenutniPregled.Hitan = true;

                string imePacijenta;
                string prezimePacijenta;
                string jmbgPacijenta;
                try
                {
                    imePacijenta = comboPacijent.Text.Split(" ")[0];
                    prezimePacijenta = comboPacijent.Text.Split(" ")[1];
                    jmbgPacijenta = comboPacijent.Text.Split(" ")[2];
                }
                catch (IndexOutOfRangeException)
                {
                    MessageBox.Show("Nepostojeći pacijent", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    comboPacijent.Focusable = true;
                    Keyboard.Focus(comboPacijent);
                    return;
                }

                bool pacijentSet = false;
                for (int i = 0; i < pacijenti.Count; i++)
                {
                    if(!pacijenti[i].Obrisan && !pacijenti[i].Guest)
                        if (pacijenti[i].Ime == imePacijenta && pacijenti[i].Prezime == prezimePacijenta && pacijenti[i].Jmbg == jmbgPacijenta)
                        {
                            pacijentSet = true;
                            guest = false;
                            break;
                        }
                }

                if (guest)
                    if(FormDodajGosta.pacijent.Ime == imePacijenta && FormDodajGosta.pacijent.Prezime == prezimePacijenta && FormDodajGosta.pacijent.Jmbg == jmbgPacijenta)
                        pacijentSet = true;

                if (!pacijentSet)
                {
                    MessageBox.Show("Nepostojeći pacijent", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    comboPacijent.Focusable = true;
                    Keyboard.Focus(comboPacijent);
                    return;
                }

                if (guest)
                    trenutniPregled.Pacijent = FormDodajGosta.pacijent;
                else 
                {
                    foreach (Pacijent pac in pacijenti)
                        if (pac.Jmbg.Equals(jmbgPacijenta))
                            trenutniPregled.Pacijent = pac;
                }

                if (comboSpecijalizacija.SelectedItem == null)
                {
                    MessageBox.Show("Nije izabrana specijalizacija", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                DateTime datum = DateTime.Now;
                int godina = datum.Year;
                int mesec = datum.Month;
                int dan = datum.Day;
                int sati = datum.Hour;
                int minuti;
                if (datum.Minute > 30 && datum.Minute <= 45)
                    minuti = 45;
                else if (datum.Minute > 15 && datum.Minute <= 30)
                    minuti = 30;
                else if (datum.Minute > 0 && datum.Minute <= 15)
                    minuti = 15;
                else if (datum.Minute == 0)
                    minuti = 0;
                else
                {
                    int min = 60 - datum.Minute;
                    datum = datum.AddMinutes(min);
                    godina = datum.Year;
                    mesec = datum.Month;
                    dan = datum.Day;
                    sati = datum.Hour;
                    minuti = 0;
                }

                trenutniPregled.Datum = new DateTime(godina, mesec, dan, sati, minuti, 0);
                DateTime dt = trenutniPregled.Datum;
                for (int i = 0; i < 4; i++)
                {
                    if (PacijentZauzet(trenutniPregled.Pacijent, trenutniPregled.Datum, trenutniPregled.Trajanje))
                    {
                        if (i != 3)
                        {
                            trenutniPregled.Datum = trenutniPregled.Datum.AddMinutes(15);
                            continue;
                        }
                        else
                        {
                            FormPomeranjeTermina s = new FormPomeranjeTermina(dataGrid, this, dt, (bool)checkOperacija.IsChecked, trenutniPregled.Trajanje, trenutniPregled.Pacijent, comboSpecijalizacija.Text, TipOperacije.prvaKat);
                            s.ShowDialog();
                            return;
                        }
                    }

                    bool zauzeteSveProstorije = true;
                    foreach (Prostorija pr in prostorije)
                        if (!pr.Obrisana && !ProstorijaZauzeta(pr, trenutniPregled.Datum, trenutniPregled.Trajanje))
                        {
                            zauzeteSveProstorije = false;
                            trenutniPregled.Prostorija = pr;
                            break;
                        }

                    if (zauzeteSveProstorije)
                    {
                        if (i != 3)
                        {
                            trenutniPregled.Datum = trenutniPregled.Datum.AddMinutes(15);
                            continue;
                        }
                        else
                        {
                            FormPomeranjeTermina s = new FormPomeranjeTermina(dataGrid, this, dt, (bool)checkOperacija.IsChecked, trenutniPregled.Trajanje, trenutniPregled.Pacijent, comboSpecijalizacija.Text, TipOperacije.prvaKat);
                            s.ShowDialog();
                            return;
                        }
                    }

                    bool zauzetiSviLekari = true;
                    foreach (Lekar l in lekari)
                        if (comboSpecijalizacija.Text.Equals(l.Specijalizacija.OblastMedicine) && !LekarZauzet(l, trenutniPregled.Datum, trenutniPregled.Trajanje))
                        {
                            zauzetiSviLekari = false;
                            trenutniPregled.Lekar = l;
                            break;
                        }

                    if (zauzetiSviLekari)
                    {
                        if (i != 3)
                        {
                            trenutniPregled.Datum = trenutniPregled.Datum.AddMinutes(15);
                            continue;
                        }
                        else
                        {
                            FormPomeranjeTermina s = new FormPomeranjeTermina(dataGrid, this, dt, (bool)checkOperacija.IsChecked, trenutniPregled.Trajanje, trenutniPregled.Pacijent, comboSpecijalizacija.Text, TipOperacije.prvaKat);
                            s.ShowDialog();
                            return;
                        }
                    }
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
                p.Lekar = trenutniPregled.Lekar;
                p.Pacijent = trenutniPregled.Pacijent;
                p.Trajanje = trenutniPregled.Trajanje;
                p.Zavrsen = trenutniPregled.Zavrsen;
                p.Anamneza.Id = -1;
                p.Prostorija = trenutniPregled.Prostorija;
                p.Datum = trenutniPregled.Datum;
                p.Hitan = trenutniPregled.Hitan;

                sviPregledi.Save(p);
                FormPregledi.listaPregleda.Add(p);
                FormPregledi.Pregledi.Add(trenutniPregled);

                if (guest)
                    storagePacijenti.Save(trenutniPregled.Pacijent);
            }
            else 
            {
                PrikazOperacije trenutnaOperacija = new PrikazOperacije();

                try
                {
                    trenutnaOperacija.Trajanje = int.Parse(txtTrajanje.Text);
                }
                catch(FormatException) 
                {
                    MessageBox.Show("Nije uneto trajanje termina", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtTrajanje.Focusable = true;
                    Keyboard.Focus(txtTrajanje);
                    return;
                }
                trenutnaOperacija.Zavrsen = false;
                trenutnaOperacija.Hitan = true;

                if (comboTipOperacije.Text.Equals("Prva"))
                    trenutnaOperacija.TipOperacije = TipOperacije.trecaKat;
                else if (comboTipOperacije.Text.Equals("Druga"))
                    trenutnaOperacija.TipOperacije = TipOperacije.drugaKat;
                else
                    trenutnaOperacija.TipOperacije = TipOperacije.prvaKat;

                if(trenutnaOperacija.Trajanje < 30)
                {
                    MessageBox.Show("Trajanje operacije je minimalno 30 minuta", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string imePacijenta;
                string prezimePacijenta;
                string jmbgPacijenta;
                try
                {
                    imePacijenta = comboPacijent.Text.Split(" ")[0];
                    prezimePacijenta = comboPacijent.Text.Split(" ")[1];
                    jmbgPacijenta = comboPacijent.Text.Split(" ")[2];
                }
                catch (IndexOutOfRangeException)
                {
                    MessageBox.Show("Nepostojeći pacijent", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    comboPacijent.Focusable = true;
                    Keyboard.Focus(comboPacijent);
                    return;
                }

                bool pacijentSet = false;
                for (int i = 0; i < pacijenti.Count; i++)
                {
                    if (!pacijenti[i].Obrisan && !pacijenti[i].Guest)
                        if (pacijenti[i].Ime == imePacijenta && pacijenti[i].Prezime == prezimePacijenta && pacijenti[i].Jmbg == jmbgPacijenta)
                        {
                            pacijentSet = true;
                            guest = false;
                            break;
                        }
                }

                if (guest)
                    if (FormDodajGosta.pacijent.Ime == imePacijenta && FormDodajGosta.pacijent.Prezime == prezimePacijenta && FormDodajGosta.pacijent.Jmbg == jmbgPacijenta)
                        pacijentSet = true;

                if (!pacijentSet)
                {
                    MessageBox.Show("Nepostojeći pacijent", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    comboPacijent.Focusable = true;
                    Keyboard.Focus(comboPacijent);
                    return;
                }

                if (guest)
                    trenutnaOperacija.Pacijent = FormDodajGosta.pacijent;
                else
                {
                    foreach (Pacijent p in pacijenti)
                        if (p.Jmbg.Equals(jmbgPacijenta))
                            trenutnaOperacija.Pacijent = p;
                }

                if (comboSpecijalizacija.SelectedItem == null)
                {
                    MessageBox.Show("Nije izabrana specijalizacija", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                DateTime datum = DateTime.Now;
                int godina = datum.Year;
                int mesec = datum.Month;
                int dan = datum.Day;
                int sati = datum.Hour;
                int minuti;
                if (datum.Minute > 30 && datum.Minute <= 45)
                    minuti = 45;
                else if (datum.Minute > 15 && datum.Minute <= 30)
                    minuti = 30;
                else if (datum.Minute > 0 && datum.Minute <= 15)
                    minuti = 15;
                else if (datum.Minute == 0)
                    minuti = 0;
                else
                {
                    int min = 60 - datum.Minute;
                    datum = datum.AddMinutes(min);
                    godina = datum.Year;
                    mesec = datum.Month;
                    dan = datum.Day;
                    sati = datum.Hour;
                    minuti = 0;
                }

                trenutnaOperacija.Datum = new DateTime(godina, mesec, dan, sati, minuti, 0);
                DateTime dt = trenutnaOperacija.Datum;
                for (int i = 0; i < 4; i++)
                {
                    if (PacijentZauzet(trenutnaOperacija.Pacijent, trenutnaOperacija.Datum, trenutnaOperacija.Trajanje))
                    {
                        if (i != 3)
                        {
                            trenutnaOperacija.Datum = trenutnaOperacija.Datum.AddMinutes(15);
                            continue;
                        }
                        else
                        {
                            FormPomeranjeTermina s = new FormPomeranjeTermina(dataGrid, this, dt, (bool)checkOperacija.IsChecked, trenutnaOperacija.Trajanje, trenutnaOperacija.Pacijent, comboSpecijalizacija.Text, trenutnaOperacija.TipOperacije);
                            s.ShowDialog();
                            return;
                        }
                    }

                    bool zauzeteSveProstorije = true;
                    foreach (Prostorija p in prostorije)
                        if (!p.Obrisana && !ProstorijaZauzeta(p, trenutnaOperacija.Datum, trenutnaOperacija.Trajanje))
                        {
                            zauzeteSveProstorije = false;
                            trenutnaOperacija.Prostorija = p;
                            break;
                        }

                    if (zauzeteSveProstorije)
                    {
                        if (i != 3)
                        {
                            trenutnaOperacija.Datum = trenutnaOperacija.Datum.AddMinutes(15);
                            continue;
                        }
                        else
                        {
                            FormPomeranjeTermina s = new FormPomeranjeTermina(dataGrid, this, dt, (bool)checkOperacija.IsChecked, trenutnaOperacija.Trajanje, trenutnaOperacija.Pacijent, comboSpecijalizacija.Text, trenutnaOperacija.TipOperacije);
                            s.ShowDialog();
                            return;
                        }
                    }

                    bool zauzetiSviLekari = true;
                    foreach (Lekar l in lekari)
                        if (comboSpecijalizacija.Text.Equals(l.Specijalizacija.OblastMedicine) && !LekarZauzet(l, trenutnaOperacija.Datum, trenutnaOperacija.Trajanje))
                        {
                            zauzetiSviLekari = false;
                            trenutnaOperacija.Lekar = l;
                            break;
                        }

                    if (zauzetiSviLekari)
                    {
                        if (i != 3)
                        {
                            trenutnaOperacija.Datum = trenutnaOperacija.Datum.AddMinutes(15);
                            continue;
                        }
                        else
                        {
                            FormPomeranjeTermina s = new FormPomeranjeTermina(dataGrid, this, dt, (bool)checkOperacija.IsChecked, trenutnaOperacija.Trajanje, trenutnaOperacija.Pacijent, comboSpecijalizacija.Text, trenutnaOperacija.TipOperacije);
                            s.ShowDialog();
                            return;
                        }
                    }
                }

                List<Operacija> operacije = new List<Operacija>();
                operacije = sviPregledi.GetAllOperacije();
                int max = 0;
                for (int i = 0; i < operacije.Count; i++)
                {
                    if (operacije[i].Id > max)
                        max = operacije[i].Id;
                }
                trenutnaOperacija.Id = max + 1;
                Operacija o = new Operacija();
                o.Id = trenutnaOperacija.Id;
                o.Lekar = trenutnaOperacija.Lekar;
                o.Pacijent = trenutnaOperacija.Pacijent;
                o.Trajanje = trenutnaOperacija.Trajanje;
                o.Zavrsen = trenutnaOperacija.Zavrsen;
                o.Anamneza.Id = -1;
                o.Prostorija = trenutnaOperacija.Prostorija;
                o.Datum = trenutnaOperacija.Datum;
                o.Hitan = trenutnaOperacija.Hitan;
                o.TipOperacije = trenutnaOperacija.TipOperacije;

                sviPregledi.Save(o);
                FormPregledi.listaOperacija.Add(o);
                FormPregledi.Pregledi.Add(trenutnaOperacija);

                if (guest)
                    storagePacijenti.Save(trenutnaOperacija.Pacijent);
            }
            Close();
        }

        private bool ProstorijaZauzeta(Prostorija prostorija, DateTime datum, int trajanje)
        {
            bool zauzet = false;
            List<Pregled> pregledi = new List<Pregled>();
            List<Pregled> preglediProstorije = new List<Pregled>();
            List<Operacija> operacije = new List<Operacija>();
            List<Operacija> operacijeProstorije = new List<Operacija>();
            pregledi = sviPregledi.GetAllPregledi();
            operacije = sviPregledi.GetAllOperacije();

            foreach (Pregled p in pregledi)
                if (p.Prostorija.BrojProstorije.Equals(prostorija.BrojProstorije))
                    preglediProstorije.Add(p);

            foreach (Pregled p in preglediProstorije)
            {
                if (DateTime.Compare(datum, p.Datum) >= 0 && DateTime.Compare(datum, p.Datum.AddMinutes(p.Trajanje)) < 0)
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

        private bool LekarZauzet(Lekar lekar, DateTime datum, int trajanje)
        {
            bool zauzet = false;
            List<Pregled> pregledi = new List<Pregled>();
            List<Pregled> preglediLekara = new List<Pregled>();
            List<Operacija> operacije = new List<Operacija>();
            List<Operacija> operacijeLekara = new List<Operacija>();
            pregledi = sviPregledi.GetAllPregledi();
            operacije = sviPregledi.GetAllOperacije();

            foreach (Pregled p in pregledi)
                if (p.Lekar.Jmbg.Equals(lekar.Jmbg))
                    preglediLekara.Add(p);

            foreach (Pregled p in preglediLekara)
            {
                if (DateTime.Compare(datum, p.Datum) >= 0 && DateTime.Compare(datum, p.Datum.AddMinutes(p.Trajanje)) < 0)
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

        private bool PacijentZauzet(Pacijent pacijent, DateTime datum, int trajanje)
        {
            bool zauzet = false;
            List<Pregled> pregledi = new List<Pregled>();
            List<Pregled> preglediPacijenta = new List<Pregled>();
            List<Operacija> operacije = new List<Operacija>();
            List<Operacija> operacijePacijenta = new List<Operacija>();
            pregledi = sviPregledi.GetAllPregledi();
            operacije = sviPregledi.GetAllOperacije();

            foreach (Pregled p in pregledi)
                if (p.Pacijent.Jmbg.Equals(pacijent.Jmbg))
                    preglediPacijenta.Add(p);

            foreach (Pregled p in preglediPacijenta)
            {
                if (DateTime.Compare(datum, p.Datum) >= 0 && DateTime.Compare(datum, p.Datum.AddMinutes(p.Trajanje)) < 0)
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
    }
}
