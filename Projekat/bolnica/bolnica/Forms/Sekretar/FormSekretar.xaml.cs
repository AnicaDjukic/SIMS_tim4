using Bolnica.Controller;
using Bolnica.DTO;
using Bolnica.Forms.Sekretar;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pacijenti;
using Bolnica.Model.Pregledi;
using Bolnica.Repository.Pregledi;
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
        public static ObservableCollection<PacijentDTO> RedovniPacijenti { get; set; }
        public static ObservableCollection<PacijentDTO> GostiPacijenti { get; set; }
        public static ObservableCollection<PacijentDTO> ObrisaniPacijenti { get; set; }
        public static bool clickedDodaj;
        private PacijentiController controller;

        public FormSekretar()
        {
            InitializeComponent();
            dataGridRedovniPacijenti.DataContext = this;
            dataGridGostiPacijenti.DataContext = this;
            dataGridObrisaniPacijenti.DataContext = this;

            btnPacijenti.Background = new SolidColorBrush(Color.FromArgb(255, 169, 169, 169));
            RedovniPacijenti = new ObservableCollection<PacijentDTO>();
            GostiPacijenti = new ObservableCollection<PacijentDTO>();
            ObrisaniPacijenti = new ObservableCollection<PacijentDTO>();
            controller = new PacijentiController();
            clickedDodaj = false;

            List<Pacijent> pacijenti = controller.DobaviPacijente();
            foreach (Pacijent p in pacijenti)
            {
                if (p.Obrisan == false && !p.Guest)
                    RedovniPacijenti.Add(new PacijentDTO(p));
                else if (p.Obrisan == false && p.Guest)
                    GostiPacijenti.Add(new PacijentDTO(p));
                else if (p.Obrisan)
                    ObrisaniPacijenti.Add(new PacijentDTO(p));
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
                PacijentDTO pacijent = (PacijentDTO)dataGridRedovniPacijenti.SelectedItem;
                if (pacijent != null)
                {
                    FormDodajPacijenta s = new FormDodajPacijenta(pacijent.Pacijent.Alergeni);
                    s.btnAlergeni.Content = "Izmeni";
                    s.btnKreiraj.Content = "Izmeni";

                    List<Pacijent> pacijenti = controller.DobaviPacijente();
                    List<ZdravstveniKarton> zdravstveniKartoni = controller.DobaviZdravstveneKartone();
          
                    foreach (Pacijent p in pacijenti)
                    {
                        if (p.Jmbg == pacijent.Pacijent.Jmbg)
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
                PacijentDTO pacijent = (PacijentDTO)dataGridRedovniPacijenti.SelectedItem;
                if (pacijent != null)
                {
                    MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite blokirati ovog pacijenta?",
                                              "Blokiranje pacijenta",
                                              MessageBoxButton.YesNo,
                                              MessageBoxImage.Exclamation);
                    if (result == MessageBoxResult.Yes)
                        controller.ObrisiRedovnogPacijenta(pacijent);
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
                PacijentDTO pacijent = (PacijentDTO)dataGridGostiPacijenti.SelectedItem;
                if (pacijent != null)
                {
                    MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite blokirati ovog pacijenta?",
                                              "Blokiranje pacijenta",
                                              MessageBoxButton.YesNo,
                                              MessageBoxImage.Exclamation);
                    if (result == MessageBoxResult.Yes)
                        controller.ObrisiGostPacijenta(pacijent);
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
            PacijentDTO pacijent = new PacijentDTO();
            if (ti1.IsSelected)
                pacijent = (PacijentDTO)dataGridRedovniPacijenti.SelectedItem;
            else if (ti2.IsSelected)
                pacijent = (PacijentDTO)dataGridGostiPacijenti.SelectedItem;
            else if (ti3.IsSelected)
                pacijent = (PacijentDTO)dataGridObrisaniPacijenti.SelectedItem;
            if (pacijent != null)
            {
                List<Pacijent> pacijenti = controller.DobaviPacijente();
                List<ZdravstveniKarton> zdravstveniKartoni = controller.DobaviZdravstveneKartone();
                foreach (Pacijent p in pacijenti)
                {
                    if (p.Jmbg == pacijent.Pacijent.Jmbg)
                    {
                        if (!pacijent.Pacijent.Guest)
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
            //btnPacijenti.Background = new SolidColorBrush(Color.FromArgb(255, 112, 112, 112));
            var s = new FormObavestenja();
            s.btnObavestenja.Background = new SolidColorBrush(Color.FromArgb(255, 169, 169, 169));
            s.Show();
            this.Close();
        }

        private void Button_Click_Pregledi(object sender, RoutedEventArgs e)
        {
            var s = new FormPregledi();
            s.btnPregledi.Background = new SolidColorBrush(Color.FromArgb(255, 169, 169, 169));
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
                var filtered = RedovniPacijenti.Where(pacijent => pacijent.Pacijent.Ime.StartsWith(searchBoxRedovni.Text, StringComparison.InvariantCultureIgnoreCase));
                dataGridRedovniPacijenti.ItemsSource = filtered;
            }
            else if (searchBoxText.Length == 2)
            {
                var filtered = RedovniPacijenti.Where(pacijent => pacijent.Pacijent.Ime.StartsWith(searchBoxText[0], StringComparison.InvariantCultureIgnoreCase) && pacijent.Pacijent.Prezime.StartsWith(searchBoxText[1], StringComparison.InvariantCultureIgnoreCase));
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
                var filtered = GostiPacijenti.Where(pacijent => pacijent.Pacijent.Ime.StartsWith(searchBoxGosti.Text, StringComparison.InvariantCultureIgnoreCase));
                dataGridGostiPacijenti.ItemsSource = filtered;
            }
            else if (searchBoxText.Length == 2)
            {
                var filtered = GostiPacijenti.Where(pacijent => pacijent.Pacijent.Ime.StartsWith(searchBoxText[0], StringComparison.InvariantCultureIgnoreCase) && pacijent.Pacijent.Prezime.StartsWith(searchBoxText[1], StringComparison.InvariantCultureIgnoreCase));
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
                var filtered = ObrisaniPacijenti.Where(pacijent => pacijent.Pacijent.Ime.StartsWith(searchBoxObrisani.Text, StringComparison.InvariantCultureIgnoreCase));
                dataGridObrisaniPacijenti.ItemsSource = filtered;
            }
            else if (searchBoxText.Length == 2)
            {
                var filtered = ObrisaniPacijenti.Where(pacijent => pacijent.Pacijent.Ime.StartsWith(searchBoxText[0], StringComparison.InvariantCultureIgnoreCase) && pacijent.Pacijent.Prezime.StartsWith(searchBoxText[1], StringComparison.InvariantCultureIgnoreCase));
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
            PacijentDTO pacijent = (PacijentDTO)dataGridObrisaniPacijenti.SelectedItem;
            if (pacijent != null)
            {
                MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite odblokirati ovog pacijenta?",
                                          "Odblokiranje pacijenta",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                    controller.OdblokirajPacijenta(pacijent);
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
            s.btnLekari.Background = new SolidColorBrush(Color.FromArgb(255, 169, 169, 169));
            s.Show();
            this.Close();
        }

        private void Button_Click_Statistika(object sender, RoutedEventArgs e)
        {
            var s = new FormStatistika();
            s.btnStats.Background = new SolidColorBrush(Color.FromArgb(255, 169, 169, 169));
            s.Show();
            this.Close();
        }
    }
}
