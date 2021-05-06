﻿using Bolnica.Forms.Sekretar;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.Sekretar;
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
        public static ObservableCollection<Pacijent> RedovniPacijenti
        {
            get;
            set;
        }
        public static ObservableCollection<Pacijent> GostiPacijenti
        {
            get;
            set;
        }
        private FileStoragePacijenti storage;
        public static bool clickedDodaj;
        public static string korisnik;

        public FormSekretar()
        {
            InitializeComponent();
            dataGridRedovniPacijenti.DataContext = this;
            dataGridGostiPacijenti.DataContext = this;

            RedovniPacijenti = new ObservableCollection<Pacijent>();
            GostiPacijenti = new ObservableCollection<Pacijent>();
            clickedDodaj = false;
            korisnik = "Sekretar";
            storage = new FileStoragePacijenti();
            
            List<Pacijent> pacijenti = storage.GetAll();
            foreach (Pacijent p in pacijenti)
            {
                if (p.Obrisan == false && !p.Guest)
                    RedovniPacijenti.Add(p);
                else if (p.Obrisan == false && p.Guest)
                    GostiPacijenti.Add(p);
            }
        }

        private void Button_Click_Dodaj(object sender, RoutedEventArgs e)
        {
            clickedDodaj = true;
            FormDodajPacijenta s = new FormDodajPacijenta(null);
            s.btnAlergeni.Content = "Dodaj";
            s.ShowDialog();
        }

        private void Button_Click_Izmeni(object sender, RoutedEventArgs e)
        {
            if (ti1.IsSelected) 
            { 
                Pacijent pacijent = (Pacijent)dataGridRedovniPacijenti.SelectedItem;
                if (pacijent != null)
                {
                    List<Pacijent> pacijenti = storage.GetAll();
                    FormDodajPacijenta s = new FormDodajPacijenta(pacijent.Alergeni);

                    s.btnAlergeni.Content = "Izmeni";
                    foreach (Pacijent p in pacijenti)
                    {
                        if (p.Jmbg == pacijent.Jmbg)
                        {
                            s.txtIme.Text = p.Ime;
                            s.txtPrezime.Text = p.Prezime;
                            if (p.Pol == Pol.muski)
                                s.rb1.IsChecked = true;
                            else
                                s.rb2.IsChecked = true;
                            s.dpDatumRodjenja.SelectedDate = p.DatumRodjenja;
                            s.dpDatumRodjenja.IsEnabled = false;
                            s.txtJMBG.Text = p.Jmbg;
                            s.txtJMBG.IsEnabled = false;
                            s.txtAdresaStanovanja.Text = p.AdresaStanovanja;
                            s.txtBrojTelefona.Text = p.BrojTelefona;
                            s.txtEmail.Text = p.Email;
                            s.txtKorisnickoIme.Text = p.KorisnickoIme;
                            s.txtLozinka.Text = p.Lozinka;
                            s.txtZanimanje.Text = p.ZdravstveniKarton.Zanimanje;
                            s.txtIDKarton.Text = p.ZdravstveniKarton.BrojKartona.ToString();
                            s.txtIDKarton.IsEnabled = false;
                            s.checkOsiguranje.IsChecked = p.ZdravstveniKarton.Osiguranje;
                            if (p.ZdravstveniKarton.BracniStatus == BracniStatus.neozenjen_neudata)
                                s.comboBracniStatus.SelectedIndex = 0;
                            else if (p.ZdravstveniKarton.BracniStatus == BracniStatus.ozenjen_udata)
                                s.comboBracniStatus.SelectedIndex = 1;
                            else if (p.ZdravstveniKarton.BracniStatus == BracniStatus.udovac_udovica)
                                s.comboBracniStatus.SelectedIndex = 2;
                            else if (p.ZdravstveniKarton.BracniStatus == BracniStatus.razveden_razvedena)
                                s.comboBracniStatus.SelectedIndex = 3; 

                            clickedDodaj = false;
                            s.ShowDialog();
                            break;
                        }
                    }
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Odaberite pacijenta za izmenu njegovih informacija.",
                                              "Izmena pacijenta",
                                              MessageBoxButton.OK,
                                              MessageBoxImage.Information);
                }
            }
        }
        private void Button_Click_Obrisi(object sender, RoutedEventArgs e)
        {
            if (ti1.IsSelected)
            {
                Pacijent pacijent = (Pacijent)dataGridRedovniPacijenti.SelectedItem;
                if (pacijent != null)
                {
                    MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite izbrisati ovog pacijenta?",
                                              "Brisanje pacijenta",
                                              MessageBoxButton.YesNo,
                                              MessageBoxImage.Exclamation);
                    if (result == MessageBoxResult.Yes)
                    {
                        Korisnik korisnik = new Korisnik() { KorisnickoIme = pacijent.KorisnickoIme, Lozinka = pacijent.Lozinka, TipKorisnika = TipKorisnika.pacijent };
                        FileStorageKorisnici storageKorisnici = new FileStorageKorisnici();
                        storageKorisnici.Delete(korisnik);
                        RedovniPacijenti.Remove(pacijent);
                        List<Pacijent> pacijenti = storage.GetAll();
                        storage.Delete(pacijent);
                        pacijent.Obrisan = true;
                        storage.Save(pacijent);
                    }
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Odaberite pacijenta za brisanje.",
                                              "Brisanje pacijenta",
                                              MessageBoxButton.OK,
                                              MessageBoxImage.Information);
                }
            }
            else
            {
                Pacijent pacijent = (Pacijent)dataGridGostiPacijenti.SelectedItem;
                if (pacijent != null)
                {
                    MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite izbrisati ovog pacijenta?",
                                              "Brisanje pacijenta",
                                              MessageBoxButton.YesNo,
                                              MessageBoxImage.Exclamation);
                    if (result == MessageBoxResult.Yes)
                    {
                        Korisnik korisnik = new Korisnik() { KorisnickoIme = pacijent.KorisnickoIme, Lozinka = pacijent.Lozinka, TipKorisnika = TipKorisnika.pacijent };
                        FileStorageKorisnici storageKorisnici = new FileStorageKorisnici();
                        storageKorisnici.Delete(korisnik);
                        GostiPacijenti.Remove(pacijent);
                        List<Pacijent> pacijenti = storage.GetAll();
                        storage.Delete(pacijent);
                        pacijent.Obrisan = true;
                        storage.Save(pacijent);
                    }
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Odaberite pacijenta za brisanje.",
                                              "Brisanje pacijenta",
                                              MessageBoxButton.OK,
                                              MessageBoxImage.Information);
                }
            }
        }

        private void Button_Click_Prikazi(object sender, RoutedEventArgs e)
        {
            if (ti1.IsSelected)
            {
                Pacijent pacijent = (Pacijent)dataGridRedovniPacijenti.SelectedItem;
                if (pacijent != null)
                {
                    List<Pacijent> pacijenti = storage.GetAll();
                    var s = new FormPrikazPacijenta();
                    foreach (Pacijent p in pacijenti)
                    {
                        if (p.Jmbg == pacijent.Jmbg)
                        {
                            s.lblIme.Content = p.Ime;
                            s.lblPrezime.Content = p.Prezime;
                            if (p.Pol == Pol.muski)
                                s.lblPol.Content = "Muški";
                            else
                                s.lblPol.Content = "Ženski";
                            s.lblDatumRodjenja.Content = p.DatumRodjenja.ToShortDateString();
                            s.lblJMBG.Content = p.Jmbg;
                            s.lblAdresaStanovanja.Content = p.AdresaStanovanja;
                            s.lblBrojTelefona.Content = p.BrojTelefona;
                            s.lblEmail.Content = p.Email;

                            s.lblKorIme.Content = p.KorisnickoIme;
                            s.lblLoz.Content = p.Lozinka;
                            s.lblZan.Content = p.ZdravstveniKarton.Zanimanje;
                            s.lblIDKar.Content = p.ZdravstveniKarton.BrojKartona.ToString();
                            s.checkOsig.IsChecked = p.ZdravstveniKarton.Osiguranje;
                            s.checkOsig.IsEnabled = false;
                            if (p.Pol == Pol.muski)
                            {
                                if (p.ZdravstveniKarton.BracniStatus == BracniStatus.neozenjen_neudata)
                                    s.lblBrStatus.Content = "Neoženjen";
                                else if (p.ZdravstveniKarton.BracniStatus == BracniStatus.ozenjen_udata)
                                    s.lblBrStatus.Content = "Oženjen";
                                else if (p.ZdravstveniKarton.BracniStatus == BracniStatus.udovac_udovica)
                                    s.lblBrStatus.Content = "Udovac";
                                else if (p.ZdravstveniKarton.BracniStatus == BracniStatus.razveden_razvedena)
                                    s.lblBrStatus.Content = "Razveden";
                            }
                            else 
                            {
                                if (p.ZdravstveniKarton.BracniStatus == BracniStatus.neozenjen_neudata)
                                    s.lblBrStatus.Content = "Neudata";
                                else if (p.ZdravstveniKarton.BracniStatus == BracniStatus.ozenjen_udata)
                                    s.lblBrStatus.Content = "Udata";
                                else if (p.ZdravstveniKarton.BracniStatus == BracniStatus.udovac_udovica)
                                    s.lblBrStatus.Content = "Udovica";
                                else if (p.ZdravstveniKarton.BracniStatus == BracniStatus.razveden_razvedena)
                                    s.lblBrStatus.Content = "Razvedena";
                            }

                            s.ShowDialog();
                            break;
                        }
                    }
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Odaberite pacijenta za prikaz njegovih informacija.",
                                              "Prikaz pacijenta",
                                              MessageBoxButton.OK,
                                              MessageBoxImage.Information);
                }
            }
            else 
            {
                Pacijent pacijent = (Pacijent)dataGridGostiPacijenti.SelectedItem;
                if (pacijent != null)
                {
                    List<Pacijent> pacijenti = storage.GetAll();
                    var s = new FormPrikazGosta();
                    foreach (Pacijent p in pacijenti)
                    {
                        if (p.Jmbg == pacijent.Jmbg)
                        {
                            s.lblIme.Content = p.Ime;
                            s.lblPrezime.Content = p.Prezime;
                            if (p.Pol == Pol.muski)
                                s.lblPol.Content = "Muški";
                            else
                                s.lblPol.Content = "Ženski";
                            s.lblDatumRodjenja.Content = p.DatumRodjenja.ToShortDateString();
                            s.lblJMBG.Content = p.Jmbg;
                            s.lblAdresaStanovanja.Content = p.AdresaStanovanja;
                            s.lblBrojTelefona.Content = p.BrojTelefona;
                            s.lblEmail.Content = p.Email;
                            s.ShowDialog();
                            break;
                        }
                    }
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Odaberite pacijenta za prikaz njegovih informacija.",
                                              "Prikaz pacijenta",
                                              MessageBoxButton.OK,
                                              MessageBoxImage.Information);
                }
            }
        }

        private void Button_Click_Obavestenja(object sender, RoutedEventArgs e)
        {
            var s = new FormObavestenja();
            s.Show();
            this.Close();
        }

        private void Button_Click_Pregledi(object sender, RoutedEventArgs e)
        {
            var s = new FormPregledi();
            s.Show();
            this.Close();
        }

        private void DataGridRedovniMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid grid = sender as DataGrid;
                if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                {
                    DataGridRow dgr = grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem) as DataGridRow;
                    if (!dgr.IsMouseOver)
                    {
                        (dgr as DataGridRow).IsSelected = false;
                    }
                }
            }
        }

        private void DataGridGostiMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid grid = sender as DataGrid;
                if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                {
                    DataGridRow dgr = grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem) as DataGridRow;
                    if (!dgr.IsMouseOver)
                    {
                        (dgr as DataGridRow).IsSelected = false;
                    }
                }
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ti1.IsSelected)
            {
                btnDodaj.Visibility = Visibility.Visible;
                btnIzmeni.Visibility = Visibility.Visible;
                btnObrisi.Margin = new Thickness(845, 380, 100, 0);
            }
            else if (ti2.IsSelected)
            {
                btnDodaj.Visibility = Visibility.Hidden;
                btnIzmeni.Visibility = Visibility.Hidden;
                btnObrisi.Margin = new Thickness(608, 380, 260, 0);
            }
        }

        private void SearchBoxKeyUp(object sender, KeyEventArgs e)
        {

        }
    }
}
