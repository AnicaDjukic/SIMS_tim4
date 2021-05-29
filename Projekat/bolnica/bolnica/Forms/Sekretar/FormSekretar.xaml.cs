using Bolnica.Forms.Sekretar;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pacijenti;
using Bolnica.Model.Pregledi;
using Bolnica.Sekretar;
using Model.Korisnici;
using Model.Pacijenti;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        public static ObservableCollection<Pacijent> ObrisaniPacijenti
        {
            get;
            set;
        }
        private FileRepositoryPacijent storage;
        private FileRepositoryZdravstveniKarton storageZdravstveniKarton;
        public static bool clickedDodaj;

        public FormSekretar()
        {
            InitializeComponent();
            dataGridRedovniPacijenti.DataContext = this;
            dataGridGostiPacijenti.DataContext = this;
            dataGridObrisaniPacijenti.DataContext = this;

            searchBoxGosti.Visibility = Visibility.Hidden;
            searchBoxObrisani.Visibility = Visibility.Hidden;
            btnOdblokiraj.Visibility = Visibility.Hidden;
            RedovniPacijenti = new ObservableCollection<Pacijent>();
            GostiPacijenti = new ObservableCollection<Pacijent>();
            ObrisaniPacijenti = new ObservableCollection<Pacijent>();
            clickedDodaj = false;
            storage = new FileRepositoryPacijent();
            storageZdravstveniKarton = new FileRepositoryZdravstveniKarton();

            List<Pacijent> pacijenti = storage.GetAll();
            foreach (Pacijent p in pacijenti)
            {
                if (p.Obrisan == false && !p.Guest)
                    RedovniPacijenti.Add(p);
                else if (p.Obrisan == false && p.Guest)
                    GostiPacijenti.Add(p);
                else if (p.Obrisan)
                    ObrisaniPacijenti.Add(p);
            }
        }

        private void Button_Click_Dodaj(object sender, RoutedEventArgs e)
        {
            clickedDodaj = true;
            FormDodajPacijenta s = new FormDodajPacijenta(null);
            s.btnAlergeni.Content = "Dodaj";
            s.btnKreiraj.Content = "Kreiraj";
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
                    List<ZdravstveniKarton> zdravstveniKartoni = storageZdravstveniKarton.GetAll();
                    FormDodajPacijenta s = new FormDodajPacijenta(pacijent.Alergeni);

                    s.btnAlergeni.Content = "Izmeni";
                    s.btnKreiraj.Content = "Izmeni";
                    foreach (Pacijent p in pacijenti)
                    {
                        if (p.Jmbg == pacijent.Jmbg)
                        {
                            s.Ime = p.Ime;
                            s.Prezime = p.Prezime;
                            if (p.Pol == Pol.muski)
                                s.rb1.IsChecked = true;
                            else
                                s.rb2.IsChecked = true;
                            s.DatumRodjenja = p.DatumRodjenja;
                            s.dpDatumRodjenja.IsEnabled = false;
                            s.Jmbg = p.Jmbg;
                            s.txtJMBG.IsEnabled = false;
                            s.AdresaStanovanja = p.AdresaStanovanja;
                            s.Telefon = p.BrojTelefona;
                            s.Email = p.Email;
                            s.KorisnickoIme = p.KorisnickoIme;
                            s.Lozinka = p.Lozinka;
                            s.BrojKartona = p.ZdravstveniKarton.BrojKartona.ToString();
                            foreach (ZdravstveniKarton zk in zdravstveniKartoni)
                                if (zk.BrojKartona == p.ZdravstveniKarton.BrojKartona) 
                                {
                                    s.Zanimanje = zk.Zanimanje;
                                    s.txtIDKarton.IsEnabled = false;
                                    s.checkOsiguranje.IsChecked = zk.Osiguranje;
                                    if (zk.BracniStatus == BracniStatus.neozenjen_neudata)
                                        s.comboBracniStatus.SelectedIndex = 0;
                                    else if (zk.BracniStatus == BracniStatus.ozenjen_udata)
                                        s.comboBracniStatus.SelectedIndex = 1;
                                    else if (zk.BracniStatus == BracniStatus.udovac_udovica)
                                        s.comboBracniStatus.SelectedIndex = 2;
                                    else if (zk.BracniStatus == BracniStatus.razveden_razvedena)
                                        s.comboBracniStatus.SelectedIndex = 3;
                                    break;
                                }

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
                    MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite blokirati ovog pacijenta?",
                                              "Blokiranje pacijenta",
                                              MessageBoxButton.YesNo,
                                              MessageBoxImage.Exclamation);
                    if (result == MessageBoxResult.Yes)
                    {
                        Korisnik korisnik = new Korisnik() { KorisnickoIme = pacijent.KorisnickoIme, Lozinka = pacijent.Lozinka, TipKorisnika = TipKorisnika.pacijent };
                        FileRepositoryKorisnik storageKorisnici = new FileRepositoryKorisnik();
                        storageKorisnici.Delete(korisnik);
                        FileRepositoryPregled termini = new FileRepositoryPregled();
                        List<Pregled> pregledi = termini.GetAllPregledi();
                        List<Operacija> operacije = termini.GetAllOperacije();
                        foreach (Pregled p in pregledi)
                            if (p.Pacijent.Jmbg == pacijent.Jmbg)
                                termini.Delete(p);
                        foreach (Operacija o in operacije)
                            if (o.Pacijent.Jmbg == pacijent.Jmbg)
                                termini.Delete(o);
                        if(FormPregledi.Pregledi != null)
                            for (int i = FormPregledi.Pregledi.Count - 1; i >= 0; i--)
                                if (FormPregledi.Pregledi[i].Pacijent.Jmbg == pacijent.Jmbg)
                                    FormPregledi.Pregledi.RemoveAt(i);
                        RedovniPacijenti.Remove(pacijent);
                        ObrisaniPacijenti.Add(pacijent);
                        List<Pacijent> pacijenti = storage.GetAll();
                        storage.Delete(pacijent);
                        pacijent.Obrisan = true;
                        storage.Save(pacijent);
                    }
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Odaberite pacijenta za blokiranje.",
                                              "Blokiranje pacijenta",
                                              MessageBoxButton.OK,
                                              MessageBoxImage.Information);
                }
            }
            else
            {
                Pacijent pacijent = (Pacijent)dataGridGostiPacijenti.SelectedItem;
                if (pacijent != null)
                {
                    MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite blokirati ovog pacijenta?",
                                              "Blokiranje pacijenta",
                                              MessageBoxButton.YesNo,
                                              MessageBoxImage.Exclamation);
                    if (result == MessageBoxResult.Yes)
                    {
                        GostiPacijenti.Remove(pacijent);
                        ObrisaniPacijenti.Add(pacijent);
                        List<Pacijent> pacijenti = storage.GetAll();
                        storage.Delete(pacijent);
                        pacijent.Obrisan = true;
                        storage.Save(pacijent);
                    }
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Odaberite pacijenta za blokiranje.",
                                              "Blokiranje pacijenta",
                                              MessageBoxButton.OK,
                                              MessageBoxImage.Information);
                }
            }
        }

        private void Button_Click_Prikazi(object sender, RoutedEventArgs e)
        {
            Pacijent pacijent = new Pacijent();
            if (ti1.IsSelected)
                pacijent = (Pacijent)dataGridRedovniPacijenti.SelectedItem;
            else if (ti2.IsSelected)
                pacijent = (Pacijent)dataGridGostiPacijenti.SelectedItem;
            else if (ti3.IsSelected)
                pacijent = (Pacijent)dataGridObrisaniPacijenti.SelectedItem;
            if (pacijent != null)
            {
                List<Pacijent> pacijenti = storage.GetAll();
                List<ZdravstveniKarton> zdravstveniKartoni = storageZdravstveniKarton.GetAll();
                foreach (Pacijent p in pacijenti)
                {
                    if (p.Jmbg == pacijent.Jmbg)
                    {
                        if (!pacijent.Guest)
                        {
                            var s = new FormPrikazPacijenta();
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
                            s.lblIDKar.Content = p.ZdravstveniKarton.BrojKartona.ToString();
                            foreach(ZdravstveniKarton zk in zdravstveniKartoni)
                                if (zk.BrojKartona == p.ZdravstveniKarton.BrojKartona) 
                                {
                                    s.lblZan.Content = zk.Zanimanje;
                                    s.checkOsig.IsChecked = zk.Osiguranje;
                                    s.checkOsig.IsEnabled = false;
                                    if (p.Pol == Pol.muski)
                                    {
                                        if (zk.BracniStatus == BracniStatus.neozenjen_neudata)
                                            s.lblBrStatus.Content = "Neoženjen";
                                        else if (zk.BracniStatus == BracniStatus.ozenjen_udata)
                                            s.lblBrStatus.Content = "Oženjen";
                                        else if (zk.BracniStatus == BracniStatus.udovac_udovica)
                                            s.lblBrStatus.Content = "Udovac";
                                        else if (zk.BracniStatus == BracniStatus.razveden_razvedena)
                                            s.lblBrStatus.Content = "Razveden";
                                    }
                                    else
                                    {
                                        if (zk.BracniStatus == BracniStatus.neozenjen_neudata)
                                            s.lblBrStatus.Content = "Neudata";
                                        else if (zk.BracniStatus == BracniStatus.ozenjen_udata)
                                            s.lblBrStatus.Content = "Udata";
                                        else if (zk.BracniStatus == BracniStatus.udovac_udovica)
                                            s.lblBrStatus.Content = "Udovica";
                                        else if (zk.BracniStatus == BracniStatus.razveden_razvedena)
                                            s.lblBrStatus.Content = "Razvedena";
                                    }
                                }

                            s.ShowDialog();
                            break;
                        }
                        else 
                        {
                            var s = new FormPrikazGosta();
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
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Odaberite pacijenta za prikaz njegovih informacija.",
                                            "Prikaz pacijenta",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Information);
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

        private void DataGridMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
                searchBoxRedovni.Visibility = Visibility.Visible;
                searchBoxGosti.Visibility = Visibility.Hidden;
                searchBoxObrisani.Visibility = Visibility.Hidden;
                btnDodaj.Visibility = Visibility.Visible;
                btnIzmeni.Visibility = Visibility.Visible;
                btnObrisi.Visibility = Visibility.Visible;
                btnOdblokiraj.Visibility = Visibility.Hidden;
                btnObrisi.Margin = new Thickness(845, 380, 100, 0);
                btnOdblokiraj.IsEnabled = false;
            }
            else if (ti2.IsSelected)
            {
                searchBoxRedovni.Visibility = Visibility.Hidden;
                searchBoxGosti.Visibility = Visibility.Visible;
                searchBoxObrisani.Visibility = Visibility.Hidden;
                btnDodaj.Visibility = Visibility.Hidden;
                btnIzmeni.Visibility = Visibility.Hidden;
                btnObrisi.Visibility = Visibility.Visible;
                btnOdblokiraj.Visibility = Visibility.Hidden;
                btnObrisi.Margin = new Thickness(608, 380, 260, 0);
                btnOdblokiraj.IsEnabled = false;
            }
            else if (ti3.IsSelected)
            {
                searchBoxRedovni.Visibility = Visibility.Hidden;
                searchBoxGosti.Visibility = Visibility.Hidden;
                searchBoxObrisani.Visibility = Visibility.Visible;
                btnDodaj.Visibility = Visibility.Hidden;
                btnIzmeni.Visibility = Visibility.Hidden;
                btnObrisi.Visibility = Visibility.Hidden;
                btnOdblokiraj.Visibility = Visibility.Visible;
                btnOdblokiraj.IsEnabled = true;
            }
        }

        private void SearchBoxRedovniKeyUp(object sender, KeyEventArgs e)
        {
            string[] searchBoxText = searchBoxRedovni.Text.Split(" ");

            if (searchBoxText.Length == 1)
            {
                var filtered = RedovniPacijenti.Where(pacijent => pacijent.Ime.StartsWith(searchBoxRedovni.Text, StringComparison.InvariantCultureIgnoreCase));
                dataGridRedovniPacijenti.ItemsSource = filtered;
            }
            else if (searchBoxText.Length == 2)
            {
                var filtered = RedovniPacijenti.Where(pacijent => pacijent.Ime.StartsWith(searchBoxText[0], StringComparison.InvariantCultureIgnoreCase) && pacijent.Prezime.StartsWith(searchBoxText[1], StringComparison.InvariantCultureIgnoreCase));
                dataGridRedovniPacijenti.ItemsSource = filtered;
            }
            else if (searchBoxText.Length > 2)
            {
                dataGridRedovniPacijenti.ItemsSource = null;
                dataGridRedovniPacijenti.Items.Refresh();
            }
        }

        private void SearchBoxGostiKeyUp(object sender, KeyEventArgs e)
        {
            string[] searchBoxText = searchBoxGosti.Text.Split(" ");

            if (searchBoxText.Length == 1)
            {
                var filtered = GostiPacijenti.Where(pacijent => pacijent.Ime.StartsWith(searchBoxGosti.Text, StringComparison.InvariantCultureIgnoreCase));
                dataGridGostiPacijenti.ItemsSource = filtered;
            }
            else if (searchBoxText.Length == 2)
            {
                var filtered = GostiPacijenti.Where(pacijent => pacijent.Ime.StartsWith(searchBoxText[0], StringComparison.InvariantCultureIgnoreCase) && pacijent.Prezime.StartsWith(searchBoxText[1], StringComparison.InvariantCultureIgnoreCase));
                dataGridGostiPacijenti.ItemsSource = filtered;
            }
            else if(searchBoxText.Length > 2) 
            {
                dataGridGostiPacijenti.ItemsSource = null;
                dataGridGostiPacijenti.Items.Refresh();
            }
        }

        private void SearchBoxObrisaniKeyUp(object sender, KeyEventArgs e)
        {
            string[] searchBoxText = searchBoxObrisani.Text.Split(" ");

            if (searchBoxText.Length == 1)
            {
                var filtered = ObrisaniPacijenti.Where(pacijent => pacijent.Ime.StartsWith(searchBoxObrisani.Text, StringComparison.InvariantCultureIgnoreCase));
                dataGridObrisaniPacijenti.ItemsSource = filtered;
            }
            else if (searchBoxText.Length == 2)
            {
                var filtered = ObrisaniPacijenti.Where(pacijent => pacijent.Ime.StartsWith(searchBoxText[0], StringComparison.InvariantCultureIgnoreCase) && pacijent.Prezime.StartsWith(searchBoxText[1], StringComparison.InvariantCultureIgnoreCase));
                dataGridObrisaniPacijenti.ItemsSource = filtered;
            }
            else if (searchBoxText.Length > 2)
            {
                dataGridObrisaniPacijenti.ItemsSource = null;
                dataGridObrisaniPacijenti.Items.Refresh();
            }
        }

        private void Button_Click_Odblokiraj(object sender, RoutedEventArgs e)
        {
            Pacijent pacijent = (Pacijent)dataGridObrisaniPacijenti.SelectedItem;
            if (pacijent != null)
            {
                MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite odblokirati ovog pacijenta?",
                                          "Odblokiranje pacijenta",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    if (!pacijent.Guest) 
                    {
                        Korisnik korisnik = new Korisnik() { KorisnickoIme = pacijent.KorisnickoIme, Lozinka = pacijent.Lozinka, TipKorisnika = TipKorisnika.pacijent };
                        FileRepositoryKorisnik storageKorisnici = new FileRepositoryKorisnik();
                        storageKorisnici.Save(korisnik);
                    }
                    
                    ObrisaniPacijenti.Remove(pacijent);
                    if(!pacijent.Guest)
                        RedovniPacijenti.Add(pacijent);
                    else
                        GostiPacijenti.Add(pacijent);
                    List<Pacijent> pacijenti = storage.GetAll();
                    storage.Delete(pacijent);
                    pacijent.Obrisan = false;
                    storage.Save(pacijent);
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Odaberite pacijenta za odblokiranje.",
                                          "Odblokiranje pacijenta",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Information);
            }
        }

        private void Button_Click_Lekari(object sender, RoutedEventArgs e)
        {
            var s = new FormLekari();
            s.Show();
            this.Close();
        }

        private void Button_Click_Statistika(object sender, RoutedEventArgs e)
        {
            var s = new FormStatistika();
            s.Show();
            this.Close();
        }
    }
}
