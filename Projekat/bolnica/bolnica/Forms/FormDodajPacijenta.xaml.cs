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
using bolnica.Forms;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormDodajPacijenta.xaml
    /// </summary>
    public partial class FormDodajPacijenta : Window
    {
        public FormDodajPacijenta()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TipKorisnika tipKorisnika = TipKorisnika.pacijent;
            bool guestNalog = (bool)checkGuest.IsChecked;
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
            DateTime datumRodjenja = dpDatumRodjenja.SelectedDate.Value.Date;

            if (guestNalog)
            {
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

                addPatient(pacijent);
            }
            else
            {
                String korisnickoIme = txtKorisnickoIme.Text;
                String lozinka = txtLozinka.Text;
                String zanimanje = txtZanimanje.Text;
                int brojKartona = Int32.Parse(txtIDKarton.Text);
                bool osiguranje = (bool)checkOsiguranje.IsChecked;
                int selectedBracniStatus = comboBracniStatus.SelectedIndex;
                BracniStatus bracniStatus = new BracniStatus();
                if (selectedBracniStatus == 0)
                    bracniStatus = BracniStatus.neozenjen_neudata;
                else if (selectedBracniStatus == 1)
                    bracniStatus = BracniStatus.ozenjen_udata;
                else if (selectedBracniStatus == 2)
                    bracniStatus = BracniStatus.udovac_udovica;
                else if(selectedBracniStatus == 3)
                    bracniStatus = BracniStatus.razveden_razvedena;

                Pacijent pacijent = new Pacijent()
                {
                    KorisnickoIme = korisnickoIme,
                    Lozinka = lozinka,
                    TipKorisnika = tipKorisnika,
                    Jmbg = jmbg,
                    Ime = ime,
                    Prezime = prezime,
                    Pol = pol,
                    DatumRodjenja = datumRodjenja,
                    BrojTelefona = brojTelefona,
                    AdresaStanovanja = adresaStanovanja,
                    Email = email,
                    Obrisan = obrisan,
                    Guest = guestNalog,
                    ZdravstveniKarton = new ZdravstveniKarton()
                    {
                        BrojKartona = brojKartona,
                        Zanimanje = zanimanje,
                        BracniStatus = bracniStatus,
                        Osiguranje = osiguranje
                    }
                };

                addPatient(pacijent);
            }
        }

        private void addPatient(Pacijent pacijent)
        {
            bool postojiPacijent = false;
            FileStoragePacijenti storage = new FileStoragePacijenti();
            List<Pacijent> pacijenti = storage.GetAll();

            foreach (Pacijent p in pacijenti)
            {
                if (String.Equals(p.Jmbg, pacijent.Jmbg))
                {
                    if (FormPacijent.clickedDodaj)
                    {
                        postojiPacijent = true;
                        FormPacijent.clickedDodaj = false;
                        MessageBox.Show("Pacijent vec postoji");
                    }
                    else
                    {
                        storage.Delete(p);

                        for (int i = 0; i < FormPacijent.Pacijenti.Count; i++)
                        {
                            if (FormPacijent.Pacijenti[i].Jmbg == pacijent.Jmbg)
                            {
                                FormPacijent.Pacijenti.Remove(FormPacijent.Pacijenti[i]);
                                break;
                            }
                        }
                    }
                }
            }

            if (!postojiPacijent)
            {
                storage.Save(pacijent);
                FormPacijent.Pacijenti.Add(pacijent);
                this.Close();
            }
        }

        private void CheckBoxChecked(object sender, RoutedEventArgs e)
        {
            lblBracniStatus.Visibility = Visibility.Hidden;
            comboBracniStatus.Visibility = Visibility.Hidden;
            lblIDKarton.Visibility = Visibility.Hidden;
            txtIDKarton.Visibility = Visibility.Hidden;
            lblKorisnickoIme.Visibility = Visibility.Hidden;
            txtKorisnickoIme.Visibility = Visibility.Hidden;
            lblLozinka.Visibility = Visibility.Hidden;
            txtLozinka.Visibility = Visibility.Hidden;
            lblOsiguranje.Visibility = Visibility.Hidden;
            checkOsiguranje.Visibility = Visibility.Hidden;
            lblZanimanje.Visibility = Visibility.Hidden;
            txtZanimanje.Visibility = Visibility.Hidden;
        }

        private void CheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            lblBracniStatus.Visibility = Visibility.Visible;
            comboBracniStatus.Visibility = Visibility.Visible;
            lblIDKarton.Visibility = Visibility.Visible;
            txtIDKarton.Visibility = Visibility.Visible;
            lblKorisnickoIme.Visibility = Visibility.Visible;
            txtKorisnickoIme.Visibility = Visibility.Visible;
            lblLozinka.Visibility = Visibility.Visible;
            txtLozinka.Visibility = Visibility.Visible;
            lblOsiguranje.Visibility = Visibility.Visible;
            checkOsiguranje.Visibility = Visibility.Visible;
            lblZanimanje.Visibility = Visibility.Visible;
            txtZanimanje.Visibility = Visibility.Visible;
        }
    }
}
