﻿using Bolnica.Model.Korisnici;
using Model.Korisnici;
using Model.Pacijenti;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// <summary>
    /// Interaction logic for FormSekretar.xaml
    /// </summary>
    public partial class FormSekretar : Window
    {
        public static ObservableCollection<Pacijent> Pacijenti
        {
            get;
            set;
        }
        private FileStoragePacijenti storage;
        public static bool clickedDodaj;

        public FormSekretar()
        {
            InitializeComponent();
            dataGridPacijenti.DataContext = this;
            Pacijenti = new ObservableCollection<Pacijent>();
            clickedDodaj = false;
            storage = new FileStoragePacijenti();
            
            List<Pacijent> pacijenti = storage.GetAll();
            foreach (Pacijent p in pacijenti)
            {
                if (p.Obrisan == false)
                    Pacijenti.Add(p);
            }
        }

        private void Button_Click_Dodaj(object sender, RoutedEventArgs e)
        {
            var s = new FormDodajPacijenta();
            clickedDodaj = true;
            s.Show();
        }

        private void Button_Click_Izmeni(object sender, RoutedEventArgs e)
        {
            Pacijent pacijent = (Pacijent)dataGridPacijenti.SelectedItem;
            if (pacijent != null)
            {
                List<Pacijent> pacijenti = storage.GetAll();
                var s = new FormDodajPacijenta();
                foreach (Pacijent p in pacijenti)
                {
                    if (p.Jmbg == pacijent.Jmbg)
                    {
                        s.checkGuest.IsChecked = p.Guest;
                        s.txtIme.Text = p.Ime;
                        s.txtPrezime.Text = p.Prezime;
                        if (p.Pol == Pol.muski)
                            s.rb1.IsChecked = true;
                        else
                            s.rb2.IsChecked = true;
                        s.dpDatumRodjenja.SelectedDate = p.DatumRodjenja;
                        s.txtJMBG.Text = p.Jmbg;
                        s.txtAdresaStanovanja.Text = p.AdresaStanovanja;
                        s.txtBrojTelefona.Text = p.BrojTelefona;
                        s.txtEmail.Text = p.Email;

                        if (!p.Guest)
                        {
                            s.txtKorisnickoIme.Text = p.KorisnickoIme;
                            s.txtLozinka.Text = p.Lozinka;
                            s.txtZanimanje.Text = p.ZdravstveniKarton.Zanimanje;
                            s.txtIDKarton.Text = p.ZdravstveniKarton.BrojKartona.ToString();
                            s.checkOsiguranje.IsChecked = p.ZdravstveniKarton.Osiguranje;
                            if (p.ZdravstveniKarton.BracniStatus == BracniStatus.neozenjen_neudata)
                                s.lblBracniStatus.Content = "Neozenjen/Neudata";
                            else if (p.ZdravstveniKarton.BracniStatus == BracniStatus.ozenjen_udata)
                                s.lblBracniStatus.Content = "Ozenjen/Ozenjena";
                            else if (p.ZdravstveniKarton.BracniStatus == BracniStatus.udovac_udovica)
                                s.lblBracniStatus.Content = "Udovac/Udovica";
                            else if (p.ZdravstveniKarton.BracniStatus == BracniStatus.razveden_razvedena)
                                s.lblBracniStatus.Content = "Razveden/Razvedena";
                        }

                        clickedDodaj = false;
                        s.Show();
                        break;
                    }
                }
            }
        }
        private void Button_Click_Obrisi(object sender, RoutedEventArgs e)
        {
            Pacijent pacijent = (Pacijent)dataGridPacijenti.SelectedItem;
            if (pacijent != null)
            {
                Korisnik korisnik = new Korisnik() { KorisnickoIme = pacijent.KorisnickoIme, Lozinka = pacijent.Lozinka, TipKorisnika = TipKorisnika.pacijent };
                FileStorageKorisnici storageKorisnici = new FileStorageKorisnici();
                storageKorisnici.Delete(korisnik);
                Pacijenti.Remove(pacijent);
                List<Pacijent> pacijenti = storage.GetAll();
                storage.Delete(pacijent);
                pacijent.Obrisan = true;
                storage.Save(pacijent);
            }
        }

        private void Button_Click_Prikazi(object sender, RoutedEventArgs e)
        {
            Pacijent pacijent = (Pacijent)dataGridPacijenti.SelectedItem;
            if (pacijent != null)
            {
                List<Pacijent> pacijenti = storage.GetAll();
                var s = new FormPrikazPacijenta();
                foreach (Pacijent p in pacijenti)
                {
                    if (p.Jmbg == pacijent.Jmbg)
                    {
                        s.checkGuest.IsChecked = p.Guest;
                        s.checkGuest.IsEnabled = false;
                        s.lblIme.Content = p.Ime;
                        s.lblPrezime.Content = p.Prezime;
                        if (p.Pol == Pol.muski)
                            s.lblPol.Content = "Muski";
                        else
                            s.lblPol.Content = "Zenski";
                        s.lblDatumRodjenja.Content = p.DatumRodjenja.ToShortDateString();
                        s.lblJMBG.Content = p.Jmbg;
                        s.lblAdresaStanovanja.Content = p.AdresaStanovanja;
                        s.lblBrojTelefona.Content = p.BrojTelefona;
                        s.lblEmail.Content = p.Email;

                        s.lblKorisnickoIme.Visibility = Visibility.Hidden;
                        s.lblKorIme.Visibility = Visibility.Hidden;
                        s.lblLozinka.Visibility = Visibility.Hidden;
                        s.lblLoz.Visibility = Visibility.Hidden;
                        s.lblZan.Visibility = Visibility.Hidden;
                        s.lblZanimanje.Visibility = Visibility.Hidden;
                        s.lblIDKar.Visibility = Visibility.Hidden;
                        s.lblIDKarton.Visibility = Visibility.Hidden;
                        s.lblBracniStatus.Visibility = Visibility.Hidden;
                        s.lblBrStatus.Visibility = Visibility.Hidden;
                        s.checkOsig.Visibility = Visibility.Hidden;
                        s.lblOsiguranje.Visibility = Visibility.Hidden;

                        if (!p.Guest)
                        {
                            s.lblKorIme.Content = p.KorisnickoIme;
                            s.lblLoz.Content = p.Lozinka;
                            s.lblZan.Content = p.ZdravstveniKarton.Zanimanje;
                            s.lblIDKar.Content = p.ZdravstveniKarton.BrojKartona.ToString();
                            s.checkOsig.IsChecked = p.ZdravstveniKarton.Osiguranje;
                            s.checkOsig.IsEnabled = false;
                            if (p.ZdravstveniKarton.BracniStatus == BracniStatus.neozenjen_neudata)
                                s.lblBrStatus.Content = "Neozenjen/Neudata";
                            else if (p.ZdravstveniKarton.BracniStatus == BracniStatus.ozenjen_udata)
                                s.lblBrStatus.Content = "Ozenjen/Ozenjena";
                            else if (p.ZdravstveniKarton.BracniStatus == BracniStatus.udovac_udovica)
                                s.lblBrStatus.Content = "Udovac/Udovica";
                            else if (p.ZdravstveniKarton.BracniStatus == BracniStatus.razveden_razvedena)
                                s.lblBrStatus.Content = "Razveden/Razvedena";

                            s.lblKorisnickoIme.Visibility = Visibility.Visible;
                            s.lblKorIme.Visibility = Visibility.Visible;
                            s.lblLozinka.Visibility = Visibility.Visible;
                            s.lblLoz.Visibility = Visibility.Visible;
                            s.lblZan.Visibility = Visibility.Visible;
                            s.lblZanimanje.Visibility = Visibility.Visible;
                            s.lblIDKar.Visibility = Visibility.Visible;
                            s.lblIDKarton.Visibility = Visibility.Visible;
                            s.lblBracniStatus.Visibility = Visibility.Visible;
                            s.lblBrStatus.Visibility = Visibility.Visible;
                            s.checkOsig.Visibility = Visibility.Visible;
                            s.lblOsiguranje.Visibility = Visibility.Visible;
                        }

                        s.Show();
                        break;
                    }
                }
            }
        }
    }
}