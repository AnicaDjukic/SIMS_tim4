using bolnica;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.Repository.Korisnici;
using Bolnica.Repository.Pregledi;
using Bolnica.ViewModel;
using Model.Korisnici;
using Model.Pacijenti;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private List<int> obavestenjaPregled = new List<int>();

        private Pacijent trenutniPacijent = new Pacijent();
        private FileRepositoryAntiTrol storageAntiTrol = new FileRepositoryAntiTrol();
        private FileRepositoryPacijent storagePacijenti = new FileRepositoryPacijent();
        private FileRepositoryPregled storagePregledi = new FileRepositoryPregled();
        private FileRepositoryOperacija storageOperacije = new FileRepositoryOperacija();
        private FileRepositoryAnamneza storageAnamneza = new FileRepositoryAnamneza();
        private FileRepositoryBeleska storageBeleska = new FileRepositoryBeleska();

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
            FormObavestenjaPacijentPage.ObavestenjaPacijent = new ObservableCollection<Obavestenje>();

            InitializeComponent();

            this.DataContext = this;
            Forma = this;
            Pacijent = pacijent;
            PacijentPageViewModel pacijentPageViewModel = new PacijentPageViewModel(pacijent);
            Pocetna.Content = new FormPacijentPage(pacijentPageViewModel/*pacijent*/);
            trenutniPacijent = pacijent;
            ImeIPre = pacijent.Ime + " " + pacijent.Prezime;
        }

        private void Button_Click_Pocetna_Stranica(object sender, RoutedEventArgs e)
        {
            PacijentPageViewModel pacijentPageViewModel = new PacijentPageViewModel(trenutniPacijent);
            Pocetna.Content = new FormPacijentPage(pacijentPageViewModel/*trenutniPacijent*/);
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
                ZakaziPregledPacijentViewModel zakaziPregledPacijentViewModel = new ZakaziPregledPacijentViewModel(trenutniPacijent);
                Pocetna.Content = new FormZakaziPacijentPage(/*trenutniPacijent*/zakaziPregledPacijentViewModel);
            }
            
        }

        public void DanasnjaObavestenja()
        {
            //PacijentPageViewModel pacijentPageViewModel = new PacijentPageViewModel(trenutniPacijent);
            //new FormPacijentPage(pacijentPageViewModel/*trenutniPacijent*/).PrikaziObavestenja();
            new PacijentPageViewModel(trenutniPacijent).PrikaziObavestenja();
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
            storageAntiTrol = new FileRepositoryAntiTrol();
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
            pregledi = storagePregledi.GetAll();
            operacije = storageOperacije.GetAll();
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
                    ProveriPocetakPregleda(pregled);
                    ProveriNotifikacije(pregled);
                }
            }
        }

        private void ProveriPocetakPregleda(Pregled pregled)
        {
            if (!pregled.Zavrsen && DateTime.Now.AddHours(1).CompareTo(pregled.Datum) >= 0 && DateTime.Now.CompareTo(pregled.Datum) <= 0)
            {
                if (!obavestenjaPregled.Contains(pregled.Id))
                {
                    string poruka = "Vaš zakazani pregled/operacija počinje danas " +
                    pregled.Datum.ToShortDateString() + ". godine u " + pregled.Datum.ToShortTimeString() + ".\n" +
                    "Pregled se održava u prostoriji broj " + pregled.Prostorija.BrojProstorije + ".";
                    MessageBox.Show(poruka);
                    FormObavestenjaPacijentPage.ObavestenjaPacijent.Add(new Obavestenje(-1, DateTime.Now, poruka, "Početak pregelda/operacije", false));
                    obavestenjaPregled.Add(pregled.Id);
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
            string poruka = vremeObavestenja.ToString() + " - Obavestenje na osnovu Vaše beleske:\r" + beleska.Zabeleska;
            MessageBox.Show(poruka, "Obavestenje na osnovu Vaše beleške!");
            FormObavestenjaPacijentPage.ObavestenjaPacijent.Add(new Obavestenje(-1, vremeObavestenja, beleska.Zabeleska, "Obaveštenje na osnovu Vaše beleške", false));
            beleska.Prikazana = true;
            storageBeleska.Update(beleska);
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
                storageBeleska.Update(beleska);
            }
        }

        private static DateTime DobijTacnoVremeObavestenje(Beleska beleska)
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, beleska.Vreme.Hours, beleska.Vreme.Minutes, beleska.Vreme.Seconds);
        }
    }
}
