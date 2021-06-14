using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.Repository.Korisnici;
using Bolnica.Repository.Pregledi;
using Bolnica.Repository.Prostorije;
using Model.Korisnici;
using Model.Pacijenti;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public const int BROJ_SATI_U_DANU = 24;
        public const int BROJ_MINUTA_U_SATU = 60;
        public const int POMAK_IZMEDJU_TERMINA = 15;
        public const int TRAJANJE_PREGLEDA = 30;
        public const int PRAZNA_ANAMNEZA = -1;
        private FileRepositoryPacijent skladistePacijenata;
        private FileRepositoryLekar skladisteLekara;
        private FileRepositoryProstorija skladisteProstorija;
        private FileRepositoryPregled skladistePregleda;
        private FileRepositoryOperacija skladisteOperacija;
        private FileRepositoryGodisnji skladisteGodisnjih;
        private FileRepositorySmena skladisteSmena;
        private List<Pacijent> sviPacijenti;
        private List<Lekar> sviLekari;
        private List<Prostorija> sveProstorije;
        private List<Pregled> sviPregledi;
        private List<Operacija> sveOperacije;
        private List<Godisnji> sviGodisnji;
        private Pregled zakazivaniPregled;

        public FormZakaziPregled()
        {
            InitializeComponent();
            inicijalizujPoljaKlase();
            inicijalizujGUIElemente();
        }

        private void inicijalizujPoljaKlase()
        {
            skladistePregleda = new FileRepositoryPregled();
            skladisteOperacija = new FileRepositoryOperacija();
            skladistePacijenata = new FileRepositoryPacijent();
            skladisteLekara = new FileRepositoryLekar();
            skladisteProstorija = new FileRepositoryProstorija();
            skladisteGodisnjih = new FileRepositoryGodisnji();
            skladisteSmena = new FileRepositorySmena();
            sviPacijenti = skladistePacijenata.GetAll();
            sviLekari = skladisteLekara.GetAll();
            sveProstorije = skladisteProstorija.GetAll();
            sviPregledi = skladistePregleda.GetAll();
            sveOperacije = skladisteOperacija.GetAll();
            sviGodisnji = skladisteGodisnjih.GetAll();
            zakazivaniPregled = new Pregled();
        }

        private void inicijalizujGUIElemente()
        {
            inicijalizujComboBoxPacijenata();
            inicijalizujComboBoxLekara();
            inicijalizujComboBoxProstorija();
            inicijalizujDatePickerDatumaTermina();
            inicijalizujComboBoxVremenaTermina();
            inicijalizujTextBoxTrajanjeTermina();
        }

        private void inicijalizujComboBoxProstorija()
        {
            foreach (Prostorija p in sveProstorije)
                if (!p.Obrisana)
                    comboBoxProstorija.Items.Add(p.BrojProstorije);
        }

        private void inicijalizujComboBoxLekara()
        {
            foreach (Lekar l in sviLekari)
                comboBoxLekara.Items.Add(l.Ime + " " + l.Prezime + " " + l.Jmbg);
        }

        private void inicijalizujComboBoxPacijenata()
        {
            foreach (Pacijent p in sviPacijenti)
                if (!p.Obrisan && !p.Guest)
                    comboBoxPacijenata.Items.Add(p.Ime + " " + p.Prezime + " " + p.Jmbg);
        }

        private void inicijalizujComboBoxVremenaTermina()
        {
            for (int sat = 0; sat < BROJ_SATI_U_DANU; sat++)
                for (int min = 0; min < BROJ_MINUTA_U_SATU;)
                {
                    TimeSpan ts = new TimeSpan(sat, min, 0);
                    min = min + POMAK_IZMEDJU_TERMINA;
                    comboBoxVremenaPregleda.Items.Add(string.Format("{0:00}:{1:00}", ts.Hours, ts.Minutes));
                }
        }

        private void inicijalizujDatePickerDatumaTermina()
        {
            datePickerDatumPregleda.SelectedDate = DateTime.Now;
        }

        private void inicijalizujTextBoxTrajanjeTermina()
        {
            textBoxTrajanjePregleda.Text = TRAJANJE_PREGLEDA.ToString();
            textBoxTrajanjePregleda.IsEnabled = false;
        }

        private void ZakaziPregled(object sender, RoutedEventArgs e)
        {
            if (!PostavljenaSvaPoljaZakazanogPregleda() || TerminZauzet() || LekarNaGodisnjem() || LekarNijeUSmeni())
                return;

            SnimiZakazaniPregled();
            DodajZakazaniPregledNaPrikaz();

            this.Close();
        }

        private bool PostavljenaSvaPoljaZakazanogPregleda()
        {
            PostaviPoljeIdZakazivanogPregleda();
            PostaviPoljeHitanZakazivanogPregleda();
            PostaviPoljeAnamnezaZakazivanogPregleda();
            PostaviPoljeZavrsenZakazivanogPregleda();
            PostaviPoljeTrajanjeZakazivanogPregleda();
            PostaviPoljeDatumZakazivanogPregleda();
            
            if (!PostavljenaPoljaPacijentLekarProstorijaZakazivanogPregleda())
                return false;
            
            return true;
        }

        private void PostaviPoljeIdZakazivanogPregleda() 
        {
            int maxId = 0;
            for (int i = 0; i < sviPregledi.Count; i++)
                if (sviPregledi[i].Id > maxId)
                    maxId = sviPregledi[i].Id;
            for (int i = 0; i < sveOperacije.Count; i++)
                if (sveOperacije[i].Id > maxId)
                    maxId = sveOperacije[i].Id;

            zakazivaniPregled.Id = maxId + 1;
        }

        private void PostaviPoljeAnamnezaZakazivanogPregleda() 
        {
            zakazivaniPregled.Anamneza.Id = PRAZNA_ANAMNEZA;
        }

        private void PostaviPoljeHitanZakazivanogPregleda() 
        {
            zakazivaniPregled.Hitan = false;
        }

        private void PostaviPoljeZavrsenZakazivanogPregleda()
        {
            zakazivaniPregled.Zavrsen = false;
        }

        private void PostaviPoljeTrajanjeZakazivanogPregleda()
        {
            zakazivaniPregled.Trajanje = TRAJANJE_PREGLEDA;
        }

        private void PostaviPoljeDatumZakazivanogPregleda() 
        {
            DateTime datum = (DateTime)datePickerDatumPregleda.SelectedDate;
            string sati = comboBoxVremenaPregleda.Text.Split(":")[0];
            string minuti = comboBoxVremenaPregleda.Text.Split(":")[1];
            zakazivaniPregled.Datum = new DateTime(datum.Year, datum.Month, datum.Day, Int32.Parse(sati), Int32.Parse(minuti), 0);
        }

        private bool PostavljenaPoljaPacijentLekarProstorijaZakazivanogPregleda()
        {
            if (!PostavljenoPoljePacijentZakazivanogPregleda() || !PostavljenoPoljeLekarZakazivanogPregleda() || !PostavljenoPoljeProstorijaZakazivanogPregleda())
                return false;

            return true;
        }

        private bool PostavljenoPoljePacijentZakazivanogPregleda()
        {
            string[] pacijentPodaci = comboBoxPacijenata.Text.Split(" ");

            if (pacijentPodaci.Length != 3 || !PostaviPoljePacijentZakazivanogPregleda(pacijentPodaci))
            {
                PacijentNepostojeciGreska();
                return false;
            }

            return true;
        }

        private bool PostavljenoPoljeLekarZakazivanogPregleda()
        {
            string[] lekarPodaci = comboBoxLekara.Text.Split(" ");

            if (lekarPodaci.Length != 3 || !PostaviPoljeLekarZakazivanogPregleda(lekarPodaci))
            {
                LekarNepostojeciGreska();
                return false;
            }

            return true;
        }

        private bool LekarNaGodisnjem() 
        {
            string[] lekarPodaci = comboBoxLekara.Text.Split(" ");
            string imeLekara = lekarPodaci[0];
            string prezimeLekara = lekarPodaci[1];
            string jmbgLekara = lekarPodaci[2];

            for (int i = 0; i < sviLekari.Count; i++)
                if (sviLekari[i].Ime.Equals(imeLekara) && sviLekari[i].Prezime.Equals(prezimeLekara) && sviLekari[i].Jmbg.Equals(jmbgLekara))
                {
                    for (int j = 0; j < sviGodisnji.Count; j++) 
                        if (sviGodisnji[j].Lekar.Jmbg == sviLekari[i].Jmbg && (sviGodisnji[j].PocetakGodisnjeg <= datePickerDatumPregleda.SelectedDate.Value && sviGodisnji[j].KrajGodisnjeg >= datePickerDatumPregleda.SelectedDate.Value)) 
                        {
                            LekarNaGodisnjemGreska();
                            return true;
                        }
                }

            return false;
        }

        private bool LekarNijeUSmeni()
        {
            string[] lekarPodaci = comboBoxLekara.Text.Split(" ");
            string imeLekara = lekarPodaci[0];
            string prezimeLekara = lekarPodaci[1];
            string jmbgLekara = lekarPodaci[2];
            
            int sati = zakazivaniPregled.Datum.Hour;
            int minute = zakazivaniPregled.Datum.Minute;

            for (int i = 0; i < sviLekari.Count; i++)
                if (sviLekari[i].Ime.Equals(imeLekara) && sviLekari[i].Prezime.Equals(prezimeLekara) && sviLekari[i].Jmbg.Equals(jmbgLekara))
                {
                    Smena smena = skladisteSmena.GetById(sviLekari[i].Smena.Id);
                    DateTime pocetakSmene = smena.PocetakSmene;
                    DateTime krajSmene = smena.KrajSmene;

                    if (pocetakSmene.Date == zakazivaniPregled.Datum.Date)
                    {
                        if (pocetakSmene <= zakazivaniPregled.Datum && krajSmene > zakazivaniPregled.Datum)
                            return false;
                        LekarNijeUSmeniGreska();
                        return true;
                    }
                    else
                    {
                        if (skladisteSmena.GetById(sviLekari[i].Smena.Id).PodrazumevanaSmena == PodrazumevanaSmena.Prva && (sati < 7 || sati >= 15 || (sati == 14 && minute > 30)))
                        {
                            LekarNijeUSmeniGreska();
                            return true;
                        }
                        else if (skladisteSmena.GetById(sviLekari[i].Smena.Id).PodrazumevanaSmena == PodrazumevanaSmena.Druga && (sati < 15 || sati >= 23 || (sati == 22 && minute > 30)))
                        {
                            LekarNijeUSmeniGreska();
                            return true;
                        }
                        else if (skladisteSmena.GetById(sviLekari[i].Smena.Id).PodrazumevanaSmena == PodrazumevanaSmena.Treca && !(sati >= 23 || sati < 7) && !(sati == 6 && minute > 30))
                        {
                            LekarNijeUSmeniGreska();
                            return true;
                        }
                    }
                }

            return false;
        }

        private bool PostavljenoPoljeProstorijaZakazivanogPregleda()
        {
            if (!PostaviPoljeProstorijaZakazivanogPregleda())
            {
                ProstorijaNepostojecaGreska();
                return false;
            }

            return true;
        }

        private bool PostaviPoljeProstorijaZakazivanogPregleda() 
        {
            for (int i = 0; i < sveProstorije.Count; i++)
                    if (!sveProstorije[i].Obrisana && sveProstorije[i].BrojProstorije.ToString().Equals(comboBoxProstorija.Text))
                    {
                        zakazivaniPregled.Prostorija = sveProstorije[i];
                        return true;
                    }
            
            return false;
        }

        private bool PostaviPoljeLekarZakazivanogPregleda(string[] lekarPodaci) 
        {
            string imeLekara = lekarPodaci[0];
            string prezimeLekara = lekarPodaci[1];
            string jmbgLekara = lekarPodaci[2];

            for (int i = 0; i < sviLekari.Count; i++)
                if (sviLekari[i].Ime.Equals(imeLekara) && sviLekari[i].Prezime.Equals(prezimeLekara) && sviLekari[i].Jmbg.Equals(jmbgLekara))
                {
                    zakazivaniPregled.Lekar = sviLekari[i];
                    return true;
                }
            
            return false;
        }

        private bool PostaviPoljePacijentZakazivanogPregleda(string[] pacijentPodaci) 
        {
            string imePacijenta = pacijentPodaci[0];
            string prezimePacijenta = pacijentPodaci[1];
            string jmbgPacijenta = pacijentPodaci[2];

            for (int i = 0; i < sviPacijenti.Count; i++)
            {
                if (!sviPacijenti[i].Obrisan && !sviPacijenti[i].Guest && sviPacijenti[i].Ime.Equals(imePacijenta) && sviPacijenti[i].Prezime.Equals(prezimePacijenta) && sviPacijenti[i].Jmbg.Equals(jmbgPacijenta))
                {
                    zakazivaniPregled.Pacijent = sviPacijenti[i];
                    return true;
                }
            }
            
            return false;
        }

        private void ProstorijaNepostojecaGreska() 
        {
            MessageBox.Show("Nepostojeća prostorija", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            comboBoxProstorija.Focusable = true;
            Keyboard.Focus(comboBoxProstorija);
        }
        
        private void LekarNepostojeciGreska() 
        {
            MessageBox.Show("Nepostojeći lekar", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            comboBoxLekara.Focusable = true;
            Keyboard.Focus(comboBoxLekara);
        }

        private void LekarNaGodisnjemGreska()
        {
            MessageBox.Show("Lekar je na godišnjem tog datuma", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            comboBoxLekara.Focusable = true;
            Keyboard.Focus(comboBoxLekara);
        }

        private void LekarNijeUSmeniGreska()
        {
            MessageBox.Show("Lekar nije u smeni", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            comboBoxLekara.Focusable = true;
            Keyboard.Focus(comboBoxLekara);
        }

        private void PacijentNepostojeciGreska()
        {
            MessageBox.Show("Nepostojeći pacijent", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            comboBoxPacijenata.Focusable = true;
            Keyboard.Focus(comboBoxPacijenata);
        }

        private bool TerminZauzet()
        {
            if (PacijentZauzet())
            {
                PacijentZauzetGreska();
                return true;
            }

            if (LekarZauzet())
            {
                LekarZauzetGreska();
                return true;
            }

            if (ProstorijaZauzeta())
            {
                ProstorijaZauzetaGreska();
                return true;
            }

            return false;
        }

        private bool ProstorijaZauzeta()
        {
            foreach (Pregled p in sviPregledi)
                if (p.Prostorija.BrojProstorije.Equals(zakazivaniPregled.Prostorija.BrojProstorije) && UTerminuPregleda(p))
                    return true;

            foreach (Operacija o in sveOperacije)
                if (o.Prostorija.BrojProstorije.Equals(zakazivaniPregled.Prostorija.BrojProstorije) && UTerminuOperacije(o))
                    return true;

            return false;
        }

        private bool LekarZauzet()
        {
            foreach (Pregled p in sviPregledi)
                if (p.Lekar.Jmbg.Equals(zakazivaniPregled.Lekar.Jmbg) && UTerminuPregleda(p))
                    return true;

            foreach (Operacija o in sveOperacije)
                if (o.Lekar.Jmbg.Equals(zakazivaniPregled.Lekar.Jmbg) && UTerminuOperacije(o))
                    return true;

            return false;
        }

        private bool PacijentZauzet()
        {
            foreach (Pregled p in sviPregledi)
                if (p.Pacijent.Jmbg.Equals(zakazivaniPregled.Pacijent.Jmbg) && UTerminuPregleda(p))
                    return true;

            foreach (Operacija o in sveOperacije)
                if (o.Pacijent.Jmbg.Equals(zakazivaniPregled.Pacijent.Jmbg) && UTerminuOperacije(o))
                    return true;

            return false;
        }

        private bool UTerminuPregleda(Pregled p)
        {
            if ((DateTime.Compare(zakazivaniPregled.Datum, p.Datum) >= 0 && DateTime.Compare(zakazivaniPregled.Datum, p.Datum.AddMinutes(p.Trajanje)) < 0) || (DateTime.Compare(zakazivaniPregled.Datum, p.Datum) <= 0 && DateTime.Compare(zakazivaniPregled.Datum.AddMinutes(zakazivaniPregled.Trajanje), p.Datum) > 0))
                return true;

            return false;
        }

        private bool UTerminuOperacije(Operacija o)
        {
            if ((DateTime.Compare(zakazivaniPregled.Datum, o.Datum) >= 0 && DateTime.Compare(zakazivaniPregled.Datum, o.Datum.AddMinutes(o.Trajanje)) < 0) || (DateTime.Compare(zakazivaniPregled.Datum, o.Datum) <= 0 && DateTime.Compare(zakazivaniPregled.Datum.AddMinutes(zakazivaniPregled.Trajanje), o.Datum) > 0))
                return true;

            return false;
        }

        private void PacijentZauzetGreska() 
        {
            MessageBox.Show("Pacijent je zauzet u tom terminu", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            comboBoxPacijenata.Focusable = true;
            Keyboard.Focus(comboBoxPacijenata);
        }

        private void LekarZauzetGreska() 
        {
            MessageBox.Show("Lekar je zauzet u tom terminu", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            comboBoxPacijenata.Focusable = true;
            Keyboard.Focus(comboBoxPacijenata);
        }

        private void ProstorijaZauzetaGreska() 
        {
            MessageBox.Show("Prostorija je zauzeta u tom terminu", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            comboBoxProstorija.Focusable = true;
            Keyboard.Focus(comboBoxProstorija);
            return;
        }

        private void SnimiZakazaniPregled() 
        {
            skladistePregleda.Save(zakazivaniPregled);
        }

        private void DodajZakazaniPregledNaPrikaz() 
        {
            PrikazPregleda trenutniPregled = new PrikazPregleda
            {
                Id = zakazivaniPregled.Id,
                Anamneza = zakazivaniPregled.Anamneza,
                Hitan = zakazivaniPregled.Hitan,
                Trajanje = zakazivaniPregled.Trajanje,
                Datum = zakazivaniPregled.Datum,
                Pacijent = zakazivaniPregled.Pacijent,
                Lekar = zakazivaniPregled.Lekar,
                Prostorija = zakazivaniPregled.Prostorija,
                Zavrsen = zakazivaniPregled.Zavrsen
            };
            FormPregledi.listaPregleda.Add(zakazivaniPregled);
            FormPregledi.Pregledi.Add(trenutniPregled);
        }

        private void Zatvori(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
