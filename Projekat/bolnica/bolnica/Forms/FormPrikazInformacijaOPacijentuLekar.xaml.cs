using Bolnica.Model.Pregledi;
using Model.Korisnici;
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
    
    public partial class FormPrikazInformacijaOPacijentuLekar : Window
    {
        public FormPrikazInformacijaOPacijentuLekar(Pacijent p1)
        {
            InitializeComponent();

            checkGuest.IsChecked = p1.Guest;
            ime.Content = p1.Ime;
            prezime.Content = p1.Prezime;
            pol.Content = p1.Pol;
            jmbg.Content = p1.Jmbg;
            datumRodjenja.Content = p1.DatumRodjenja.ToString();
            adresaStanovanja.Content = p1.AdresaStanovanja;
            brojTelefona.Content = p1.BrojTelefona;
            emailAdresa.Content = p1.Email;
            List<Sastojak>? l = p1.Alergeni;
            if (l != null)
            {
                for (int i = 0; i < p1.Alergeni.Count; i++)
                {
                    alergeni2.Items.Add(p1.Alergeni[i].Naziv);
                }
            }

            if (p1.Guest)
            {
                korisnickoIme.Visibility = Visibility.Hidden;
                korisnickoIme2.Visibility = Visibility.Hidden;
                zanimanje.Visibility = Visibility.Hidden;
                zanimanje2.Visibility = Visibility.Hidden;
                bracniStatus.Visibility = Visibility.Hidden;
                bracniStatus2.Visibility = Visibility.Hidden;
                osiguranje.Visibility = Visibility.Hidden;
                osiguranje2.Visibility = Visibility.Hidden;
            }
            else
            {
                korisnickoIme2.Content = p1.KorisnickoIme;
                
                zanimanje2.Content = p1.ZdravstveniKarton.Zanimanje;
                bracniStatus2.Content = p1.ZdravstveniKarton.BracniStatus;
                osiguranje2.IsChecked = p1.ZdravstveniKarton.Osiguranje;

            }

        }

        private void Zatvori(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
