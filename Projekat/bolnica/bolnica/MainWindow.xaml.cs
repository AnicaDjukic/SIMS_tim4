using bolnica.Forms;
using Bolnica.Forms;
using Bolnica.Model.Korisnici;
using Bolnica.ViewModel;
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
        private FileStoragePacijenti storagePacijenti = new FileStoragePacijenti();

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
                        LekarViewModel vm = new LekarViewModel(l);
                        FormLekar form = new FormLekar(vm);

                        

                    }
                    else if (korisnik.TipKorisnika == TipKorisnika.pacijent)
                    {
                        List<Pacijent> pacijenti = storagePacijenti.GetAll();
                        Pacijent pacijent = new Pacijent();
                        foreach (Pacijent p in pacijenti)
                        {
                            if (korisnickoIme.Equals(p.KorisnickoIme) && lozinka.Equals(p.Lozinka) && !p.Obrisan && !p.Guest)
                            {
                                pacijent = p;
                                break;
                            }
                        }
                        if (pacijent.Jmbg is null)
                        {
                            MessageBox.Show("Vas nalog je guest ili je obrisan.");
                        }
                        else
                        {
                            var s = new FormPacijentWeb(pacijent);
                            s.Show();
                            s.DanasnjaObavestenja();
                        }
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
