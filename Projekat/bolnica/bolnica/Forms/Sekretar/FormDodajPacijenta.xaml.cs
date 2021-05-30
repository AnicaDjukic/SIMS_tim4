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

        private PacijentiController controller;
        private List<Sastojak> alergeni;
        public static bool pacijentiUpdate;
        public FormDodajPacijenta(List<Sastojak> alergeni)
        {
            InitializeComponent();
            this.DataContext = this;
            this.alergeni = alergeni;
            FormAlergeniDodaj.DodatiAlergeni = null;
            FormAlergeniDodaj.SviAlergeni = null;
            controller = new PacijentiController();
            pacijentiUpdate = false;

            if (FormSekretar.clickedDodaj) 
                DatumRodjenja = DateTime.Now;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<TextBox> textboxes = new List<TextBox> { txtIme, txtPrezime, txtJMBG, txtAdresaStanovanja, txtBrojTelefona, txtEmail, txtKorisnickoIme, txtLozinka, txtIDKarton, txtZanimanje };
            PacijentDTO podaciPacijenta = new PacijentDTO(textboxes, dpDatumRodjenja, comboBracniStatus, checkOsiguranje, rb1, alergeni);
            controller.DodajIliIzmeniRedovnogPacijenta(podaciPacijenta);
            if(pacijentiUpdate)
            {
                this.Close();
                pacijentiUpdate = false;
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
