using Bolnica.Model.Pregledi;
using Bolnica.Sekretar;
using Model.Korisnici;
using Model.Pacijenti;
using System;
using System.Collections.Generic;
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
    public partial class FormDodajGosta : Window
    {
        public static Pacijent pacijent;
        private ComboBox comboPacijent;
        public FormDodajGosta(ComboBox comboPacijent)
        {
            InitializeComponent();
            this.comboPacijent = comboPacijent;
            pacijent = new Pacijent();
            FormAlergeniDodaj.DodatiAlergeni = null;
            FormAlergeniDodaj.SviAlergeni = null;
        }

        private void Zatvori(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Kreiraj(object sender, RoutedEventArgs e)
        {
            TipKorisnika tipKorisnika = TipKorisnika.pacijent;
            bool guestNalog = true;
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
                MessageBox.Show("Nije unet datum rođenja", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                dpDatumRodjenja.Focusable = true;
                Keyboard.Focus(dpDatumRodjenja);
                return;
            }

            if (datumRodjenja.Year > 2021 || datumRodjenja.Year < 1900)
            {
                MessageBox.Show("Neispravna godina rođenja", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                dpDatumRodjenja.Focusable = true;
                Keyboard.Focus(dpDatumRodjenja);
                return;
            }

            Pacijent pacijent = new Pacijent();
            pacijent.TipKorisnika = tipKorisnika;
            pacijent.Guest = guestNalog;
            pacijent.Obrisan = obrisan;
            pacijent.Pol = pol;
            pacijent.Jmbg = jmbg;
            pacijent.Ime = ime;
            pacijent.Prezime = prezime;
            pacijent.BrojTelefona = brojTelefona;
            pacijent.AdresaStanovanja = adresaStanovanja;
            pacijent.Email = email;
            pacijent.DatumRodjenja = datumRodjenja;
            if (FormAlergeniDodaj.DodatiAlergeni != null && FormAlergeniDodaj.DodatiAlergeni.Count != 0)
                pacijent.Alergeni = FormAlergeniDodaj.DodatiAlergeni.ToList();
            else
                pacijent.Alergeni = null;

            addPatient(pacijent);
        }

        private void addPatient(Pacijent pac)
        {
            FileStoragePacijenti storage = new FileStoragePacijenti();
            List<Pacijent> pacijenti = storage.GetAll();
            Regex rgxBrojTelefona = new Regex(@"^\([0-9]{3}\)\s[0-9]{3}-[0-9]{3,4}$");
            bool isEmail = Regex.IsMatch(pac.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            Regex rgxJmbg = new Regex(@"^[0-9]{13}$");

            if (pac.Ime == "")
            {
                MessageBox.Show("Nije uneto ime", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                txtIme.Focusable = true;
                Keyboard.Focus(txtIme);
                return;
            }

            if (pac.Prezime == "")
            {
                MessageBox.Show("Nije uneto prezime", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                txtPrezime.Focusable = true;
                Keyboard.Focus(txtPrezime);
                return;
            }

            if (!rgxJmbg.IsMatch(pac.Jmbg))
            {
                MessageBox.Show("JMBG mora da se sastoji od 13 cifara", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                txtJMBG.Focusable = true;
                Keyboard.Focus(txtJMBG);
                return;
            }
            
            foreach (Pacijent p in pacijenti)
            {
                if (String.Equals(p.Jmbg, pac.Jmbg))
                {
                    MessageBox.Show("Pacijent sa unetim JMBG-om već postoji", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtJMBG.Focusable = true;
                    Keyboard.Focus(txtJMBG);
                    return;
                }
            }

            if ((Int32.Parse(pac.Jmbg.Substring(0, 2)) != pac.DatumRodjenja.Day) || (Int32.Parse(pac.Jmbg.Substring(2, 2)) != pac.DatumRodjenja.Month) || (Int32.Parse(pac.Jmbg.Substring(4, 3)) != (pac.DatumRodjenja.Year % 1000)))
            {
                MessageBox.Show("JMBG i datum rođenja se ne poklapaju", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (pac.AdresaStanovanja == "")
            {
                MessageBox.Show("Nije uneta adresa stanovanja", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                txtAdresaStanovanja.Focusable = true;
                Keyboard.Focus(txtAdresaStanovanja);
                return;
            }

            if (!rgxBrojTelefona.IsMatch(pac.BrojTelefona) && pac.BrojTelefona != "")
            {
                MessageBox.Show("Nepostojeći broj telefona ili nije u dobrom formatu", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                txtBrojTelefona.Focusable = true;
                Keyboard.Focus(txtBrojTelefona);
                return;
            }

            if (!isEmail && pac.Email != "")
            {
                MessageBox.Show("Nepostojeći email", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                txtEmail.Focusable = true;
                Keyboard.Focus(txtEmail);
                return;
            }

            pacijent = pac;
            comboPacijent.Text = pacijent.Ime + " " + pacijent.Prezime + " " + pacijent.Jmbg;
            FormZakaziHitanTermin.guest = true;
            this.Close();
        }

        private void DodajAlergene(object sender, RoutedEventArgs e)
        {
            var s = new FormAlergeniDodaj(txtJMBG);
            s.ti1.IsSelected = true;
            s.btnUkloni.IsEnabled = false;
            s.ShowDialog();
        }
    }
}
