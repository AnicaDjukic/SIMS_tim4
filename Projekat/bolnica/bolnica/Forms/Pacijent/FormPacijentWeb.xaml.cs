using bolnica;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Model.Korisnici;
using Model.Pacijenti;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormPacijentWeb.xaml
    /// </summary>
    public partial class FormPacijentWeb : Window
    {
        public static FormPacijentWeb Forma;
        public static Pacijent Pacijent;

        private Pacijent trenutniPacijent = new Pacijent();
        private FileStorageAntiTrol storageAntiTrol = new FileStorageAntiTrol();
        private FileStoragePacijenti storagePacijenti = new FileStoragePacijenti();
        private FileStoragePregledi storagePregledi = new FileStoragePregledi();
        private FileStorageAnamneza storageAnamneza = new FileStorageAnamneza();
        private FileStorageBeleska storageBeleska = new FileStorageBeleska();

        private List<AntiTrol> antiTrol = new List<AntiTrol>();
        private List<Pregled> pregledi = new List<Pregled>();
        private List<Operacija> operacije = new List<Operacija>();
        private List<Anamneza> anamneze = new List<Anamneza>();
        private List<Beleska> beleske = new List<Beleska>();

        public static string ImeIPre
        {
            get;
            set;
        }

        public FormPacijentWeb(Pacijent pacijent)
        {
            InitializeComponent();

            this.DataContext = this;
            Forma = this;
            Pacijent = pacijent;
            Pocetna.Content = new FormPacijentPage(pacijent);
            trenutniPacijent = pacijent;
            ImeIPre = pacijent.Ime + " " + pacijent.Prezime;
        }

        private void Button_Click_Pocetna_Stranica(object sender, RoutedEventArgs e)
        {
            Pocetna.Content = new FormPacijentPage(trenutniPacijent);
        }

        private void Button_Click_Zakazivanje_Pregleda(object sender, RoutedEventArgs e)
        {
            int brojac = DobijBrojAktivnosti();
            
            if (brojac > 5)
            {
                BlokirajPacijenta();
                this.Close();
            }
            else
            {
                if (brojac > 4)
                {
                    MessageBox.Show("Posljednje upozorenje pred gasenje Vaseg naloga. Ukoliko nastavite da zloupotrebljavate " +
                        "nasu aplikaciju pristup samoj aplikaciji ce Vam biti onemogucen!", "Upozorenje");
                }
                Pocetna.Content = new FormZakaziPacijentPage(trenutniPacijent);
            }
            
        }

        public void DanasnjaObavestenja()
        {
            new FormPacijentPage(trenutniPacijent).PrikaziObavestenja();
        }

        private void Button_Click_Istorija_Pregleda(object sender, RoutedEventArgs e)
        {
            Pocetna.Content = new FormIstorijaPregledaPage(trenutniPacijent);
        }

        private void Button_Click_Obavestenja(object sender, RoutedEventArgs e)
        {
            Pocetna.Content = new FormObavestenjaPacijentPage(trenutniPacijent);
        }

        public int DobijBrojAktivnosti()
        {
            int brojac = 0;
            storageAntiTrol = new FileStorageAntiTrol();
            antiTrol = storageAntiTrol.GetAll();
            foreach (AntiTrol a in antiTrol)
            {
                if (trenutniPacijent.Jmbg.Equals(a.Pacijent.Jmbg) && a.Datum.AddDays(3).CompareTo(DateTime.Now) > 0)
                {
                    brojac++;
                }
            }
            return brojac;
        }

        public void BlokirajPacijenta()
        {
            trenutniPacijent.Obrisan = true;
            storagePacijenti.Update(trenutniPacijent);
            MessageBox.Show("Zbog zloupotrebe nase aplikacije prinudjeni smo da Vam onemogucimo pristup istoj. " +
                "Vas nalog ce biti obrisan i vise necete moci da se ulogujete na Vas profil!", "Iskljucenje");
            new MainWindow().Show();
        }

        private void Button_Click_Odjava(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void Button_Click_Lekovi_Terapije(object sender, RoutedEventArgs e)
        {
            Pocetna.Content = new FormLekoviTerapijePage(trenutniPacijent);
        }

        private void Button_Click_Zdravstveni_Karton(object sender, RoutedEventArgs e)
        {
            Pocetna.Content = new FormZdravstveniKartonPage(trenutniPacijent);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer(TimeSpan.FromSeconds(1), DispatcherPriority.Normal, (object s, EventArgs ev) =>
            {
                vremeLabel.Content = DateTime.Now;
                NadjiSvePreglede();
                NadjiPacijenta();
            }, this.Dispatcher);
            timer.Start();
        }

        private void NadjiSvePreglede()
        {
            pregledi = storagePregledi.GetAllPregledi();
            operacije = storagePregledi.GetAllOperacije();
            foreach (Operacija o in operacije)
            {
                pregledi.Add(o);
            }
        }

        private void NadjiPacijenta()
        {
            foreach (Pregled pregled in pregledi)
            {
                if (trenutniPacijent.Jmbg.Equals(pregled.Pacijent.Jmbg))
                {
                    ProveriNotifikacije(pregled);
                }
            }
        }

        private void ProveriNotifikacije(Pregled pregled)
        {
            anamneze = storageAnamneza.GetAll();
            beleske = storageBeleska.GetAll();
            foreach (Anamneza anamneza in anamneze)
            {
                if (pregled.Anamneza.Id.Equals(anamneza.Id))
                {
                    foreach (Beleska beleska in beleske)
                    {
                        if (anamneza.Beleska.Id.Equals(beleska.Id))
                        {
                            SlanjeNotifikacijeiZaJednuBelesku(beleska);
                        }
                    }
                }
            }
        }

        private void SlanjeNotifikacijeiZaJednuBelesku(Beleska beleska)
        {
            DateTime vremeObavestenja = DobijTacnoVremeObavestenje(beleska);
            if (ProveraVremenaObavestenja(beleska, vremeObavestenja))
            {
                PosaljiNotifikaciju(beleska, vremeObavestenja);
            }
        }

        private void PosaljiNotifikaciju(Beleska beleska, DateTime vremeObavestenja)
        {
            MessageBox.Show(vremeObavestenja.ToString() + " - Obavestenje na osnovu vase beleske:\r" + beleska.Zabeleska, "Obavestenje!");
            beleska.Prikazana = true;
            storageBeleska.Izmeni(beleska);
        }

        private bool ProveraVremenaObavestenja(Beleska beleska, DateTime vremeObavestenja)
        {
            if (beleska.Podsetnik && DateTime.Now.CompareTo(beleska.DatumPrekida) <= 0)
            {
                if (DateTime.Now.CompareTo(vremeObavestenja.AddMinutes(30)) <= 0)
                {
                    return !beleska.Prikazana && DateTime.Now.CompareTo(vremeObavestenja) >= 0;
                }
                else
                {
                    IzmeniStatusBeleske(beleska);
                    return false;
                }
            }
            return false;
        }

        private void IzmeniStatusBeleske(Beleska beleska)
        {
            if (beleska.Prikazana)
            {
                beleska.Prikazana = false;
                storageBeleska.Izmeni(beleska);
            }
        }

        private static DateTime DobijTacnoVremeObavestenje(Beleska beleska)
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, beleska.Vreme.Hours, beleska.Vreme.Minutes, beleska.Vreme.Seconds);
        }
    }
}
