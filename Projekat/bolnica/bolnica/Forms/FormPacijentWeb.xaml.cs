using bolnica;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Model.Korisnici;
using Model.Pacijenti;
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

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormPacijentWeb.xaml
    /// </summary>
    public partial class FormPacijentWeb : Window
    {
        private Pacijent trenutniPacijent = new Pacijent();
        private FileStorageAntiTrol storageAntiTrol = new FileStorageAntiTrol();
        private FileStoragePacijenti storagePacijenti = new FileStoragePacijenti();

        public FormPacijentWeb(Pacijent pacijent)
        {
            InitializeComponent();

            Pocetna.Content = new FormPacijentPage(pacijent, this);
            trenutniPacijent = pacijent;
        }

        private void Button_Click_Pocetna_Stranica(object sender, RoutedEventArgs e)
        {
            Pocetna.Content = new FormPacijentPage(trenutniPacijent, this);
        }

        private void Button_Click_Zakazivanje_Pregleda(object sender, RoutedEventArgs e)
        {
            storageAntiTrol = new FileStorageAntiTrol();
            List<AntiTrol> antiTrol = storageAntiTrol.GetAll();
            int brojac = 0;
            foreach (AntiTrol a in antiTrol)
            {
                if (a.PacijentJMBG.Equals(trenutniPacijent.Jmbg))
                {
                    if (a.Datum.AddDays(3).CompareTo(DateTime.Now) > 0)
                    {
                        brojac++;
                    }
                }
            }
            if (brojac > 5)
            {
                trenutniPacijent.Obrisan = true;
                storagePacijenti.Update(trenutniPacijent);
                MessageBox.Show("Zbog zloupotrebe nase aplikacije prinudjeni smo da Vam onemogucimo pristup istoj. " +
                    "Vas nalog ce biti obrisan i vise necete moci da se ulogujete na Vas profil!", "Iskljucenje");
                var s = new MainWindow();
                s.Show();
                this.Close();
            }
            else
            {
                if (brojac > 3)
                {
                    MessageBox.Show("Posljednje upozorenje pred gasenje Vaseg naloga. Ukoliko nastavite da zloupotrebljavate " +
                        "nasu aplikaciju pristup samoj aplikaciji ce Vam biti onemogucen!", "Upozorenje");
                }
                Pocetna.Content = new FormZakaziPacijentPage(trenutniPacijent, this);
            }
            
        }

        public void DanasnjaObavestenja()
        {
            new FormPacijentPage(trenutniPacijent, this).PrikaziObavestenja();
        }

        private void Button_Click_Istorija_Pregleda(object sender, RoutedEventArgs e)
        {
            Pocetna.Content = new FormIstorijaPregledaPage(trenutniPacijent, this);
        }
    }
}
