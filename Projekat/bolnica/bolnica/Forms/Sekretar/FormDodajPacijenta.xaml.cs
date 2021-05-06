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

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormDodajPacijenta.xaml
    /// </summary>
    public partial class FormDodajPacijenta : Window
    {
        private FileStorageKorisnici storageKorisnici;
        private List<Sastojak> alergeni;
        public FormDodajPacijenta(List<Sastojak> alergeni)
        {
            InitializeComponent();
            storageKorisnici = new FileStorageKorisnici();
            this.alergeni = alergeni;
            FormAlergeniDodaj.DodatiAlergeni = null;
            FormAlergeniDodaj.SviAlergeni = null;
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
                MessageBox.Show("Nije unet broj kartona", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                txtIDKarton.Focusable = true;
                Keyboard.Focus(txtIDKarton);
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
            FileStoragePacijenti storage = new FileStoragePacijenti();
            List<Pacijent> pacijenti = storage.GetAll();
            Regex rgxBrojTelefona = new Regex(@"^\([0-9]{3}\)\s[0-9]{3}-[0-9]{3,4}$");
            bool isEmail = Regex.IsMatch(pacijent.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            Regex rgxIDKartona = new Regex(@"^[1-9][0-9]*$");
            Regex rgxJmbg = new Regex(@"^[0-9]{13}$");

            if (pacijent.Ime == "")
            {
                MessageBox.Show("Nije uneto ime", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                txtIme.Focusable = true;
                Keyboard.Focus(txtIme);
                return;
            }

            if (pacijent.Prezime == "")
            {
                MessageBox.Show("Nije uneto prezime", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                txtPrezime.Focusable = true;
                Keyboard.Focus(txtPrezime);
                return;
            }

            if (FormSekretar.clickedDodaj)
            {
                if (!rgxJmbg.IsMatch(pacijent.Jmbg))
                {
                    MessageBox.Show("JMBG mora da se sastoji od 13 cifara", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtJMBG.Focusable = true;
                    Keyboard.Focus(txtJMBG);
                    return;
                }
            }

            if (FormSekretar.clickedDodaj)
            {
                foreach (Pacijent p in pacijenti)
                {
                    if (String.Equals(p.Jmbg, pacijent.Jmbg))
                    {
                        MessageBox.Show("Pacijent sa unetim JMBG-om već postoji", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                        txtJMBG.Focusable = true;
                        Keyboard.Focus(txtJMBG);
                        return;
                    }
                }
            }

            if ((Int32.Parse(pacijent.Jmbg.Substring(0, 2)) != pacijent.DatumRodjenja.Day) || (Int32.Parse(pacijent.Jmbg.Substring(2, 2)) != pacijent.DatumRodjenja.Month) || (Int32.Parse(pacijent.Jmbg.Substring(4, 3)) != (pacijent.DatumRodjenja.Year % 1000)))
            {
                MessageBox.Show("JMBG i datum rođenja se ne poklapaju", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
                

            if(pacijent.AdresaStanovanja == "")
            {
                MessageBox.Show("Nije uneta adresa stanovanja", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                txtAdresaStanovanja.Focusable = true;
                Keyboard.Focus(txtAdresaStanovanja);
                return;
            }
                
            if (!rgxBrojTelefona.IsMatch(pacijent.BrojTelefona) || pacijent.BrojTelefona == "")
            {
                MessageBox.Show("Nepostojeći broj telefona ili nije u dobrom formatu", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                txtBrojTelefona.Focusable = true;
                Keyboard.Focus(txtBrojTelefona);
                return;
            }

            if (!isEmail || pacijent.Email == "")
            {
                MessageBox.Show("Nepostojeći email", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                txtEmail.Focusable = true;
                Keyboard.Focus(txtEmail);
                return;
            }

            if (pacijent.KorisnickoIme == "")
            {
                MessageBox.Show("Nije uneto korisničko ime", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                txtKorisnickoIme.Focusable = true;
                Keyboard.Focus(txtKorisnickoIme);
                return;
            }

            if (FormSekretar.clickedDodaj)
            {
                foreach (Pacijent p in pacijenti)
                {
                    if (String.Equals(pacijent.KorisnickoIme, p.KorisnickoIme))
                    {
                        MessageBox.Show("Korisničko ime već postoji", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                        txtKorisnickoIme.Focusable = true;
                        Keyboard.Focus(txtKorisnickoIme);
                        return;
                    }
                }
            }

            if (pacijent.Lozinka == "")
            {
                MessageBox.Show("Nije uneta lozinka", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                txtLozinka.Focusable = true;
                Keyboard.Focus(txtLozinka);
                return;
            }


            if (FormSekretar.clickedDodaj)
            {
                if (!rgxIDKartona.IsMatch(pacijent.ZdravstveniKarton.BrojKartona.ToString()))
                {
                    MessageBox.Show("Broj kartona mora da bude pozitivan celi broj", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtIDKarton.Focusable = true;
                    Keyboard.Focus(txtIDKarton);
                    return;
                }
            }

            if (FormSekretar.clickedDodaj)
            {
                foreach (Pacijent p in pacijenti)
                {
                    if (!p.Guest && p.ZdravstveniKarton.BrojKartona == pacijent.ZdravstveniKarton.BrojKartona)
                    {
                        MessageBox.Show("Pacijent sa unetim brojem zdravstvenog kartona već postoji", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                        txtIDKarton.Focusable = true;
                        Keyboard.Focus(txtIDKarton);
                        return;
                    }
                }
            }

            if (pacijent.ZdravstveniKarton.Zanimanje == "")
            {
                MessageBox.Show("Nije uneto zanimanje", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                txtZanimanje.Focusable = true;
                Keyboard.Focus(txtZanimanje);
                return;
            }

            if (FormSekretar.clickedDodaj)
            {
                storage.Save(pacijent);
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
                s.ti1.IsSelected = true;
                s.btnUkloni.IsEnabled = false;
            }
            else
            {
                s.ti2.IsSelected = true;
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
