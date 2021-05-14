using bolnica;
using Bolnica.Model.Korisnici;
using Model.Korisnici;
using Model.Pacijenti;
using System;
using System.Collections.Generic;
using System.Windows;

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
        private List<AntiTrol> antiTrol = new List<AntiTrol>();
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
    }
}
