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
using Bolnica.DTO;
using Bolnica.Controller;
using Bolnica.DTO.Sekretar;
using Bolnica.Controller.Sekretar;

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

        private PacijentController pacijentiController;
        private List<int> idsAlergeni;
        public FormDodajPacijenta(List<int> idsAlergeni)
        {
            InitializeComponent();
            this.idsAlergeni = idsAlergeni;
            pacijentiController = new PacijentController();
            InicijalizujGUI();
        }

        private void InicijalizujGUI() 
        {
            this.DataContext = this;

            FormAlergeniDodaj.DodatiAlergeni = null;
            FormAlergeniDodaj.SviAlergeni = null;

            if (FormSekretar.clickedDodaj)
                DatumRodjenja = DateTime.Now;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PacijentDTO pacijent = new PacijentDTO
            {
                KorisnickoIme = txtKorisnickoIme.Text,
                Lozinka = txtLozinka.Text,
                TipKorisnika = TipKorisnika.pacijent,
                Jmbg = txtJMBG.Text,
                Ime = txtIme.Text,
                Prezime = txtPrezime.Text,
                Pol = PostaviPoljePolPacijenta(),
                BrojTelefona = txtBrojTelefona.Text,
                AdresaStanovanja = txtAdresaStanovanja.Text,
                Zanimanje = txtZanimanje.Text,
                BracniStatus = PostaviPoljeBracniStatusPacijenta(),
                Osiguranje = (bool)checkOsiguranje.IsChecked,
                Email = txtEmail.Text,
                Obrisan = false,
                Guest = false,
            };

            PostaviPoljeAlergeniPacijenta(pacijent);

            if (!PostaviPoljeDatumRodjenjaPacijenta(pacijent) || !PostaviPoljeBrojKartonaPacijenta(pacijent) || !SvaPoljaValidna(pacijent))
                return;

            DodajIliIzmeniRedovnogPacijenta(pacijent);
        }

        private void DodajIliIzmeniRedovnogPacijenta(PacijentDTO pacijent) 
        {
            pacijentiController.DodajIliIzmeniRedovnogPacijenta(pacijent);
            FormSekretar.clickedDodaj = false;
            DodajIliIzmeniAzurirajTabelu(pacijent);
            Close();
        }

        private bool SvaPoljaValidna(PacijentDTO pacijent) 
        {
            List<PacijentDTO> sviPacijenti = pacijentiController.GetAllPacijente();
            if (!BrojKartonaPoljeValidno(pacijent, sviPacijenti) || !BrojTelefonaPoljeValidno(pacijent) || !JednostavneValidacijeValidne(pacijent) || !EmailPoljeValidno(pacijent) || !KorisnickoImePoljeValidno(pacijent, sviPacijenti) || !JmbgPoljeValidno(pacijent, sviPacijenti) || !DatumRodjenjaPoljeValidno(pacijent))
                return false;
            return true;
        }

        private bool BrojKartonaPoljeValidno(PacijentDTO pacijent, List<PacijentDTO> sviPacijenti) 
        {
            Regex rgxIDKartona = new Regex(@"^[1-9][0-9]*$");

            if (FormSekretar.clickedDodaj)
                if (!rgxIDKartona.IsMatch(pacijent.BrojKartona.ToString()))
                {
                    MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

            if (FormSekretar.clickedDodaj)
                foreach (PacijentDTO p in sviPacijenti)
                    if (!p.Guest && p.BrojKartona == pacijent.BrojKartona)
                    {
                        MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }

            return true;
        }

        private bool KorisnickoImePoljeValidno(PacijentDTO pacijent, List<PacijentDTO> sviPacijenti) 
        {
            if (FormSekretar.clickedDodaj)
                foreach (PacijentDTO p in sviPacijenti)
                    if (String.Equals(pacijent.KorisnickoIme, p.KorisnickoIme))
                    {
                        MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
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
            if (FormSekretar.clickedDodaj)
                if (!rgxJmbg.IsMatch(pacijent.Jmbg))
                {
                    MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

            if (FormSekretar.clickedDodaj)
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
            if (pacijent.Ime == "" || pacijent.Prezime == "" || pacijent.AdresaStanovanja == "" || pacijent.KorisnickoIme == "" || pacijent.Lozinka == "" || pacijent.Zanimanje == "")
            {
                MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void PostaviPoljeAlergeniPacijenta(PacijentDTO pacijent) 
        {
            if (!FormSekretar.clickedDodaj && FormAlergeniDodaj.DodatiAlergeni == null)
                pacijent.IdsAlergena = idsAlergeni;
            else if (FormAlergeniDodaj.DodatiAlergeni != null && FormAlergeniDodaj.DodatiAlergeni.Count != 0) 
            {
                List<SastojakDTO> alergeni = FormAlergeniDodaj.DodatiAlergeni.ToList();
                pacijent.IdsAlergena = new List<int>();
                foreach (SastojakDTO sastojakDTO in alergeni)
                    pacijent.IdsAlergena.Add(sastojakDTO.Id);
            }
            else
                pacijent.IdsAlergena = null;
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

        private bool PostaviPoljeBrojKartonaPacijenta(PacijentDTO pacijent) 
        {
            try
            {
                pacijent.BrojKartona = Int32.Parse(txtIDKarton.Text);
                return true;
            }
            catch (FormatException)
            {
                MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (OverflowException)
            {
                MessageBox.Show("Postoje greške pri popunjavanju forme ili neki podatak nije unet", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private BracniStatus PostaviPoljeBracniStatusPacijenta() 
        {
            string selektovaniBracniStatus = ((ComboBoxItem)comboBracniStatus.SelectedItem).Content.ToString();
            if (selektovaniBracniStatus == "Neozenjen/Neudata")
                return pacijentiController.PostaviPoljeBracniStatusPacijenta(new BracniStatusNeozenjen());
            else if (selektovaniBracniStatus == "Ozenjen/Udata")
                return pacijentiController.PostaviPoljeBracniStatusPacijenta(new BracniStatusOzenjen());
            else if (selektovaniBracniStatus == "Udovac/Udovica")
                return pacijentiController.PostaviPoljeBracniStatusPacijenta(new BracniStatusUdovac());
            else
                return pacijentiController.PostaviPoljeBracniStatusPacijenta(new BracniStatusRazveden());
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

        private void DodajIliIzmeniAzurirajTabelu(PacijentDTO pacijent) 
        {
            if (FormSekretar.clickedDodaj)
                FormSekretar.RedovniPacijenti.Add(pacijent);
            else 
            {
                for (int i = 0; i < FormSekretar.RedovniPacijenti.Count; i++)
                    if (FormSekretar.RedovniPacijenti[i].Jmbg == pacijent.Jmbg)
                    {
                        FormSekretar.RedovniPacijenti.Remove(FormSekretar.RedovniPacijenti[i]);
                        break;
                    }

                FormSekretar.RedovniPacijenti.Add(pacijent);
            }
        }

        private void Zatvori(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
