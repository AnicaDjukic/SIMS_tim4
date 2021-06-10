using Bolnica.Controller;
using Bolnica.DTO;
using Bolnica.Model.Pregledi;
using Bolnica.Sekretar;
using Model.Korisnici;
using Model.Pacijenti;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for FormDodajGosta.xaml
    /// </summary>
    public partial class FormDodajGosta : Window, INotifyPropertyChanged
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

        private PacijentiController pacijentiController;
        public static Pacijent pacijent;
        private ComboBox comboPacijent;
        public FormDodajGosta(ComboBox comboPacijent)
        {
            InitializeComponent();
            this.comboPacijent = comboPacijent;
            pacijent = new Pacijent();
            pacijentiController = new PacijentiController();
            InicijalizujGUI();
        }

        private void InicijalizujGUI() 
        {
            this.DataContext = this;
            FormAlergeniDodaj.DodatiAlergeni = null;
            FormAlergeniDodaj.SviAlergeni = null;
            DatumRodjenja = DateTime.Now;
        }

        private void Zatvori(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Kreiraj(object sender, RoutedEventArgs e)
        {
            PacijentDTO pacijent = new PacijentDTO
            {
                TipKorisnika = TipKorisnika.pacijent,
                Jmbg = txtJMBG.Text,
                Ime = txtIme.Text,
                Prezime = txtPrezime.Text,
                Pol = PostaviPoljePolPacijenta(),
                BrojTelefona = txtBrojTelefona.Text,
                AdresaStanovanja = txtAdresaStanovanja.Text,
                Email = txtEmail.Text,
                Obrisan = false,
                Guest = true,
            };

            PostaviPoljeAlergeniPacijenta(pacijent);

            if (!PostaviPoljeDatumRodjenjaPacijenta(pacijent) || !SvaPoljaValidna(pacijent))
                return;

            DodajGostPacijenta(pacijent);
        }

        private void DodajGostPacijenta(PacijentDTO pacijentDTO) 
        {
            pacijent = new Pacijent
            {
                TipKorisnika = TipKorisnika.pacijent,
                Jmbg = pacijentDTO.Jmbg,
                Ime = pacijentDTO.Ime,
                Prezime = pacijentDTO.Prezime,
                DatumRodjenja = pacijentDTO.DatumRodjenja,
                Alergeni = pacijentDTO.Alergeni,
                Pol = pacijentDTO.Pol,
                BrojTelefona = pacijentDTO.BrojTelefona,
                AdresaStanovanja = pacijentDTO.AdresaStanovanja,
                Email = pacijentDTO.Email,
                Obrisan = false,
                Guest = true,
            };

            comboPacijent.Text = pacijent.Ime + " " + pacijent.Prezime + " " + pacijent.Jmbg;
            FormZakaziHitanTermin.guest = true;
            this.Close();
        }

        private bool SvaPoljaValidna(PacijentDTO pacijent)
        {
            List<PacijentDTO> sviPacijenti = pacijentiController.GetAllPacijente();
            if (!BrojTelefonaPoljeValidno(pacijent) || !JednostavneValidacijeValidne(pacijent) || !EmailPoljeValidno(pacijent) || !JmbgPoljeValidno(pacijent, sviPacijenti) || !DatumRodjenjaPoljeValidno(pacijent))
                return false;
            return true;
        }

        private bool BrojTelefonaPoljeValidno(PacijentDTO pacijent)
        {
            Regex rgxBrojTelefona = new Regex(@"^\([0-9]{3}\)\s[0-9]{3}-[0-9]{3,4}$");
            if (!rgxBrojTelefona.IsMatch(pacijent.BrojTelefona) || pacijent.BrojTelefona == "")
            {
                MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private bool EmailPoljeValidno(PacijentDTO pacijent)
        {
            bool isEmail = Regex.IsMatch(pacijent.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (!isEmail || pacijent.Email == "")
            {
                MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private bool DatumRodjenjaPoljeValidno(PacijentDTO pacijent)
        {
            if (DateTime.Compare(pacijent.DatumRodjenja, DateTime.Now) > 0 || pacijent.DatumRodjenja.Year < 1900)
            {
                MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private bool JmbgPoljeValidno(PacijentDTO pacijent, List<PacijentDTO> sviPacijenti)
        {
            Regex rgxJmbg = new Regex(@"^[0-9]{13}$");
            
            if (!rgxJmbg.IsMatch(pacijent.Jmbg))
            {
                MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            foreach (PacijentDTO p in sviPacijenti)
                if (String.Equals(p.Jmbg, pacijent.Jmbg))
                {
                    MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

            if ((Int32.Parse(pacijent.Jmbg.Substring(0, 2)) != pacijent.DatumRodjenja.Day) || (Int32.Parse(pacijent.Jmbg.Substring(2, 2)) != pacijent.DatumRodjenja.Month) || (Int32.Parse(pacijent.Jmbg.Substring(4, 3)) != (pacijent.DatumRodjenja.Year % 1000)))
            {
                MessageBox.Show("JMBG i datum rođenja se ne poklapaju", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private bool JednostavneValidacijeValidne(PacijentDTO pacijent)
        {
            if (pacijent.Ime == "" || pacijent.Prezime == "" || pacijent.AdresaStanovanja == "")
            {
                MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void PostaviPoljeAlergeniPacijenta(PacijentDTO pacijent) 
        {
            if (FormAlergeniDodaj.DodatiAlergeni != null && FormAlergeniDodaj.DodatiAlergeni.Count != 0)
                pacijent.Alergeni = FormAlergeniDodaj.DodatiAlergeni.ToList();
            else
                pacijent.Alergeni = null;
        }

        private bool PostaviPoljeDatumRodjenjaPacijenta(PacijentDTO pacijent)
        {
            try
            {
                pacijent.DatumRodjenja = dpDatumRodjenja.SelectedDate.Value.Date;
                return true;
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private Pol PostaviPoljePolPacijenta()
        {
            Pol pol = new Pol();
            if ((bool)rb1.IsChecked)
                pol = Pol.muski;
            else
                pol = Pol.zenski;

            return pol;
        }

        private void DodajAlergene(object sender, RoutedEventArgs e)
        {
            var s = new FormAlergeniDodaj(txtJMBG);
            s.btnUkloni.IsEnabled = false;
            s.ShowDialog();
        }
    }
}
