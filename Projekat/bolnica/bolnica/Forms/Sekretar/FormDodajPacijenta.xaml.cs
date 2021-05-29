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
using Model.Korisnici;
using Model.Pacijenti;
using Bolnica.Model.Korisnici;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Bolnica.Sekretar;
using System.Linq;
using Bolnica.Model.Pregledi;
using Bolnica.Model.Pacijenti;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormDodajPacijenta.xaml
    /// </summary>
    public partial class FormDodajPacijenta : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private string ime;
        private string prezime;
        private string jmbg;
        private string adresaStanovanja;
        private string telefon;
        private string email;
        private string korisnickoIme;
        private string lozinka;
        private string zanimanje;
        private string brojKartona;
        private DateTime datumRodjenja;

        public string Ime
        {
            get
            {
                return ime;
            }
            set
            {
                if (value != ime)
                {
                    ime = value;
                    OnPropertyChanged("Ime");
                }
            }
        }

        public string Prezime
        {
            get
            {
                return prezime;
            }
            set
            {
                if (value != prezime)
                {
                    prezime = value;
                    OnPropertyChanged("Prezime");
                }
            }
        }

        public string Jmbg
        {
            get
            {
                return jmbg;
            }
            set
            {
                if (value != jmbg)
                {
                    jmbg = value;
                    OnPropertyChanged("Jmbg");
                }
            }
        }

        public string AdresaStanovanja
        {
            get
            {
                return adresaStanovanja;
            }
            set
            {
                if (value != adresaStanovanja)
                {
                    adresaStanovanja = value;
                    OnPropertyChanged("AdresaStanovanja");
                }
            }
        }

        public string Telefon
        {
            get
            {
                return telefon;
            }
            set
            {
                if (value != telefon)
                {
                    telefon = value;
                    OnPropertyChanged("Telefon");
                }
            }
        }

        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                if (value != email)
                {
                    email = value;
                    OnPropertyChanged("Email");
                }
            }
        }

        public string KorisnickoIme
        {
            get
            {
                return korisnickoIme;
            }
            set
            {
                if (value != korisnickoIme)
                {
                    korisnickoIme = value;
                    OnPropertyChanged("KorisnickoIme");
                }
            }
        }

        public string Lozinka
        {
            get
            {
                return lozinka;
            }
            set
            {
                if (value != lozinka)
                {
                    lozinka = value;
                    OnPropertyChanged("Lozinka");
                }
            }
        }

        public string Zanimanje
        {
            get
            {
                return zanimanje;
            }
            set
            {
                if (value != zanimanje)
                {
                    zanimanje = value;
                    OnPropertyChanged("Zanimanje");
                }
            }
        }

        public string BrojKartona
        {
            get
            {
                return brojKartona;
            }
            set
            {
                if (value != brojKartona)
                {
                    brojKartona = value;
                    OnPropertyChanged("BrojKartona");
                }
            }
        }

        public DateTime DatumRodjenja
        {
            get
            {
                return datumRodjenja;
            }
            set
            {
                if (value != datumRodjenja)
                {
                    datumRodjenja = value;
                    OnPropertyChanged("DatumRodjenja");
                }
            }
        }

        private FileRepositoryKorisnik storageKorisnici;
        private FileRepositoryZdravstveniKarton storageZdravstveniKartoni;
        private List<Sastojak> alergeni;
        public FormDodajPacijenta(List<Sastojak> alergeni)
        {
            InitializeComponent();
            this.DataContext = this;
            storageKorisnici = new FileRepositoryKorisnik();
            storageZdravstveniKartoni = new FileRepositoryZdravstveniKarton();
            this.alergeni = alergeni;
            FormAlergeniDodaj.DodatiAlergeni = null;
            FormAlergeniDodaj.SviAlergeni = null;

            if (FormSekretar.clickedDodaj)
                DatumRodjenja = DateTime.Now;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TipKorisnika tipKorisnika = TipKorisnika.pacijent;
            bool guestNalog = false;
            bool obrisan = false;
            Pol pol = new Pol();
            if ((bool)rb1.IsChecked)
                pol = Pol.muski;
            else
                pol = Pol.zenski;
            String jmbg = txtJMBG.Text;
            String ime = txtIme.Text;
            String prezime = txtPrezime.Text;
            String brojTelefona = txtBrojTelefona.Text;
            String adresaStanovanja = txtAdresaStanovanja.Text;
            String email = txtEmail.Text;
            DateTime datumRodjenja;
            try
            {
                datumRodjenja = dpDatumRodjenja.SelectedDate.Value.Date;
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (DateTime.Compare(datumRodjenja, DateTime.Now) > 0 || datumRodjenja.Year < 1900)
            {
                MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            String korisnickoIme = txtKorisnickoIme.Text;
            String lozinka = txtLozinka.Text;
            String zanimanje = txtZanimanje.Text;
            int brojKartona;
            try
            {
                brojKartona = Int32.Parse(txtIDKarton.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (OverflowException)
            {
                MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            bool osiguranje = (bool)checkOsiguranje.IsChecked;
            int selectedBracniStatus = comboBracniStatus.SelectedIndex;
            BracniStatus bracniStatus = new BracniStatus();
            if (selectedBracniStatus == 0)
                bracniStatus = BracniStatus.neozenjen_neudata;
            else if (selectedBracniStatus == 1)
                bracniStatus = BracniStatus.ozenjen_udata;
            else if (selectedBracniStatus == 2)
                bracniStatus = BracniStatus.udovac_udovica;
            else if (selectedBracniStatus == 3)
                bracniStatus = BracniStatus.razveden_razvedena;

            Pacijent pacijent = new Pacijent();
            pacijent.KorisnickoIme = korisnickoIme;
            pacijent.Lozinka = lozinka;
            pacijent.TipKorisnika = tipKorisnika;
            pacijent.Jmbg = jmbg;
            pacijent.Ime = ime;
            pacijent.Prezime = prezime;
            pacijent.Pol = pol;
            pacijent.DatumRodjenja = datumRodjenja;
            pacijent.BrojTelefona = brojTelefona;
            pacijent.AdresaStanovanja = adresaStanovanja;
            pacijent.Email = email;
            pacijent.Obrisan = obrisan;
            pacijent.Guest = guestNalog;
            ZdravstveniKarton zk = new ZdravstveniKarton()
            {
                BrojKartona = brojKartona,
                Zanimanje = zanimanje,
                BracniStatus = bracniStatus,
                Osiguranje = osiguranje
            };
            pacijent.ZdravstveniKarton = zk;

            if (!FormSekretar.clickedDodaj && FormAlergeniDodaj.DodatiAlergeni == null)
                pacijent.Alergeni = alergeni;
            else if (FormAlergeniDodaj.DodatiAlergeni != null && FormAlergeniDodaj.DodatiAlergeni.Count != 0)
                pacijent.Alergeni = FormAlergeniDodaj.DodatiAlergeni.ToList();
            else
                pacijent.Alergeni = null;

            addPatient(pacijent);
        }

        private void addPatient(Pacijent pacijent)
        {
            FileRepositoryPacijent storage = new FileRepositoryPacijent();
            List<Pacijent> pacijenti = storage.GetAll();
            Regex rgxBrojTelefona = new Regex(@"^\([0-9]{3}\)\s[0-9]{3}-[0-9]{3,4}$");
            bool isEmail = Regex.IsMatch(pacijent.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            Regex rgxIDKartona = new Regex(@"^[1-9][0-9]*$");
            Regex rgxJmbg = new Regex(@"^[0-9]{13}$");

            if (pacijent.Ime == "")
            {
                MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (pacijent.Prezime == "")
            {
                MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (FormSekretar.clickedDodaj)
            {
                if (!rgxJmbg.IsMatch(pacijent.Jmbg))
                {
                    MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (FormSekretar.clickedDodaj)
            {
                foreach (Pacijent p in pacijenti)
                {
                    if (String.Equals(p.Jmbg, pacijent.Jmbg))
                    {
                        MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }

            if ((Int32.Parse(pacijent.Jmbg.Substring(0, 2)) != pacijent.DatumRodjenja.Day) || (Int32.Parse(pacijent.Jmbg.Substring(2, 2)) != pacijent.DatumRodjenja.Month) || (Int32.Parse(pacijent.Jmbg.Substring(4, 3)) != (pacijent.DatumRodjenja.Year % 1000)))
            {
                MessageBox.Show("JMBG i datum rođenja se ne poklapaju", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (pacijent.AdresaStanovanja == "")
            {
                MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!rgxBrojTelefona.IsMatch(pacijent.BrojTelefona) || pacijent.BrojTelefona == "")
            {
                MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!isEmail || pacijent.Email == "")
            {
                MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (pacijent.KorisnickoIme == "")
            {
                MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (FormSekretar.clickedDodaj)
            {
                foreach (Pacijent p in pacijenti)
                {
                    if (String.Equals(pacijent.KorisnickoIme, p.KorisnickoIme))
                    {
                        MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }

            if (pacijent.Lozinka == "")
            {
                MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (FormSekretar.clickedDodaj)
            {
                if (!rgxIDKartona.IsMatch(pacijent.ZdravstveniKarton.BrojKartona.ToString()))
                {
                    MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (FormSekretar.clickedDodaj)
            {
                foreach (Pacijent p in pacijenti)
                {
                    if (!p.Guest && p.ZdravstveniKarton.BrojKartona == pacijent.ZdravstveniKarton.BrojKartona)
                    {
                        MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }

            if (pacijent.ZdravstveniKarton.Zanimanje == "")
            {
                MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (FormSekretar.clickedDodaj)
            {
                storage.Save(pacijent);
                storageZdravstveniKartoni.Save(pacijent.ZdravstveniKarton);
                FormSekretar.RedovniPacijenti.Add(pacijent);
                
                Korisnik korisnik = new Korisnik() { KorisnickoIme = pacijent.KorisnickoIme, Lozinka = pacijent.Lozinka, TipKorisnika = TipKorisnika.pacijent };
                storageKorisnici.Save(korisnik);
                
                FormSekretar.clickedDodaj = false;
                this.Close();
            }
            else
            {
                foreach (Pacijent p in pacijenti)
                {
                    if (String.Equals(p.Jmbg, pacijent.Jmbg))
                    {
                        storage.Delete(p);
                        storageZdravstveniKartoni.Delete(p.ZdravstveniKarton);
                        storageKorisnici.Delete(p);

                        for (int i = 0; i < FormSekretar.RedovniPacijenti.Count; i++)
                        {
                            if (FormSekretar.RedovniPacijenti[i].Jmbg == pacijent.Jmbg)
                            {
                                FormSekretar.RedovniPacijenti.Remove(FormSekretar.RedovniPacijenti[i]);
                                break;
                            }
                        }

                        storage.Save(pacijent);
                        storageZdravstveniKartoni.Save(pacijent.ZdravstveniKarton);
                        FormSekretar.RedovniPacijenti.Add(pacijent);

                        Korisnik korisnik = new Korisnik() { KorisnickoIme = pacijent.KorisnickoIme, Lozinka = pacijent.Lozinka, TipKorisnika = TipKorisnika.pacijent };
                        storageKorisnici.Save(korisnik);

                        this.Close();
                        break;
                    }
                }
            }
        }

        private void Button_Click_Dodaj_Alergene(object sender, RoutedEventArgs e)
        {
            var s = new FormAlergeniDodaj(txtJMBG);
            if (FormSekretar.clickedDodaj)
            {
                s.btnUkloni.IsEnabled = false;
            }
            else
            {
                if (FormAlergeniDodaj.SviAlergeni.Count != 0)
                    s.btnDodaj.IsEnabled = true;
                else
                    s.btnDodaj.IsEnabled = false;

                if (FormAlergeniDodaj.DodatiAlergeni.Count != 0)
                    s.btnUkloni.IsEnabled = true;
                else
                    s.btnUkloni.IsEnabled = false;
            }
            s.ShowDialog();
        }

        private void Zatvori(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
