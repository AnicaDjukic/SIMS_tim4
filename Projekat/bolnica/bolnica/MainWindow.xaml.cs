using bolnica.Forms;
using Bolnica.Forms;
using Bolnica.Model.Korisnici;
using Model.Korisnici;
using Model.Pacijenti;
using System.Collections.Generic;
using System.Windows;


namespace bolnica
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileStorageKorisnici storage = new FileStorageKorisnici();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string korisnickoIme = txtUser.Text;
            string lozinka = txtPassword.Password;
            List<Korisnik> korisnici = storage.GetAll();
            bool found = false;

            foreach (Korisnik korisnik in korisnici)
            {
                if (korisnickoIme == korisnik.KorisnickoIme && lozinka == korisnik.Lozinka)
                {
                    if (korisnik.TipKorisnika == TipKorisnika.upravnik)

                    {
                        var s = new FormUpravnik();
                        s.Show();
                    }
                    else if (korisnik.TipKorisnika == TipKorisnika.sekretar)
                    {
                        var s = new FormSekretar();
                        s.Show();
                    }
                    else if (korisnik.TipKorisnika == TipKorisnika.lekar)
                    {
                        FileStorageLekar storageLekar = new FileStorageLekar();
                        List<Lekar> lekari = storageLekar.GetAll();
                        Lekar l = new Lekar();
                        foreach(Lekar ln in lekari)
                        {
                            if(ln.KorisnickoIme.Equals(korisnickoIme) && ln.Lozinka.Equals(lozinka))
                            {
                                l = ln;
                                break;
                            }
                        }
                        var s = new FormLekar(l);
                        s.Show();

                    }
                    else if (korisnik.TipKorisnika == TipKorisnika.pacijent)
                    {
                        FileStoragePacijenti storagePacijenti = new FileStoragePacijenti();
                        List<Pacijent> pacijenti = storagePacijenti.GetAll();
                        Pacijent pac = new Pacijent();
                        foreach (Pacijent p in pacijenti)
                        {
                            if (!p.Obrisan && !p.Guest)
                            {
                                if (p.KorisnickoIme.Equals(korisnickoIme) && p.Lozinka.Equals(lozinka))
                                {
                                    pac = p;
                                    break;
                                }
                            }
                        }
                        var s = new FormPacijent(pac);
                        s.Show();
                        s.PrikaziObavestenja();
                    }

                    found = true;
                    break;
                }
            }

            if (!found)
                MessageBox.Show("error");

            Close();

        }
    }
}
