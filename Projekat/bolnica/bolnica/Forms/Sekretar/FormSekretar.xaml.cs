using bolnica;
using Bolnica.Controller;
using Bolnica.Controller.Sekretar;
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
        private PacijentController pacijentiController;
        private ZdravstveniKartonController zdravstveniKartonController;

        public FormSekretar()
        {
            InitializeComponent();
            pacijentiController = new PacijentController();
            zdravstveniKartonController = new ZdravstveniKartonController();
            clickedDodaj = false;
            InicijalizujGUI();
        }

        private void InicijalizujGUI() 
        {
            DataContext = this;
            btnPacijenti.Background = new SolidColorBrush(Color.FromArgb(255, 169, 169, 169));
            RedovniPacijenti = new ObservableCollection<PacijentDTO>(pacijentiController.GetRedovnePacijente());
            GostiPacijenti = new ObservableCollection<PacijentDTO>(pacijentiController.GetGostPacijente());
            ObrisaniPacijenti = new ObservableCollection<PacijentDTO>(pacijentiController.GetObrisanePacijente());
        }

        private void Button_Click_Dodaj(object sender, RoutedEventArgs e)
        {
            clickedDodaj = true;
            FormDodajPacijenta window = new FormDodajPacijenta(null);
            window.btnAlergeni.Content = "Dodaj";
            window.btnKreiraj.Content = "Kreiraj";
            window.ShowDialog();
        }

        private void Button_Click_Izmeni(object sender, RoutedEventArgs e)
        {
            PacijentDTO pacijent = (PacijentDTO)dataGridRedovniPacijenti.SelectedItem;
            if (pacijent != null)
            {
                FormDodajPacijenta window = new FormDodajPacijenta(pacijent.IdsAlergena);
                window.btnAlergeni.Content = "Izmeni";
                window.btnKreiraj.Content = "Izmeni";
                InicijalizujGUIZaOdabranogPacijentaIzmeni(pacijent, window);
            }
            else
                MessageBox.Show("Odaberite pacijenta za izmenu njegovih informacija.", "Izmena pacijenta", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void InicijalizujGUIZaOdabranogPacijentaIzmeni(PacijentDTO pacijent, FormDodajPacijenta window)
        {
            ZdravstveniKartonDTO zdravstveniKarton = zdravstveniKartonController.GetZdravstveniKartonByID(pacijent.BrojKartona);
            InicijalizujGUIElementePacijentIzmeni(pacijent, window);
            InicijalizujGUIElementeZdravstveniKartonIzmeni(zdravstveniKarton, window);

            clickedDodaj = false;
            window.ShowDialog();
        }

        private void InicijalizujGUIElementePacijentIzmeni(PacijentDTO pacijent, FormDodajPacijenta window) 
        {
            window.Ime = pacijent.Ime;
            window.Prezime = pacijent.Prezime;
            if (pacijent.Pol == Pol.muski)
                window.rb1.IsChecked = true;
            else
                window.rb2.IsChecked = true;
            window.DatumRodjenja = pacijent.DatumRodjenja;
            window.dpDatumRodjenja.IsEnabled = false;
            window.Jmbg = pacijent.Jmbg;
            window.txtJMBG.IsEnabled = false;
            window.AdresaStanovanja = pacijent.AdresaStanovanja;
            window.Telefon = pacijent.BrojTelefona;
            window.Email = pacijent.Email;
            window.KorisnickoIme = pacijent.KorisnickoIme;
            window.Lozinka = pacijent.Lozinka;
        }

        private void InicijalizujGUIElementeZdravstveniKartonIzmeni(ZdravstveniKartonDTO zdravstveniKarton, FormDodajPacijenta window) 
        {
            window.BrojKartona = zdravstveniKarton.BrojKartona.ToString();
            window.Zanimanje = zdravstveniKarton.Zanimanje;
            window.txtIDKarton.IsEnabled = false;
            window.checkOsiguranje.IsChecked = zdravstveniKarton.Osiguranje;
            if (zdravstveniKarton.BracniStatus == BracniStatus.neozenjen_neudata)
                window.comboBracniStatus.SelectedIndex = 0;
            else if (zdravstveniKarton.BracniStatus == BracniStatus.ozenjen_udata)
                window.comboBracniStatus.SelectedIndex = 1;
            else if (zdravstveniKarton.BracniStatus == BracniStatus.udovac_udovica)
                window.comboBracniStatus.SelectedIndex = 2;
            else if (zdravstveniKarton.BracniStatus == BracniStatus.razveden_razvedena)
                window.comboBracniStatus.SelectedIndex = 3;
        }

        private void Button_Click_Obrisi(object sender, RoutedEventArgs e)
        {
            if (ti1.IsSelected)
                BlokirajRedovnogPacijenta();
            else
                BlokirajGostPacijenta();
        }

        private void BlokirajRedovnogPacijenta() 
        {
            PacijentDTO pacijent = (PacijentDTO)dataGridRedovniPacijenti.SelectedItem;
            if (pacijent != null)
            {
                BlokirajPacijenta(pacijent);
            }
            else
                MessageBox.Show("Odaberite pacijenta za blokiranje.", "Blokiranje pacijenta", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BlokirajGostPacijenta() 
        {
            PacijentDTO pacijent = (PacijentDTO)dataGridGostiPacijenti.SelectedItem;
            if (pacijent != null)
            {
                BlokirajPacijenta(pacijent);
            }
            else
                MessageBox.Show("Odaberite pacijenta za blokiranje.", "Blokiranje pacijenta", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        
        private void BlokirajPacijenta(PacijentDTO pacijent) 
        {
            MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite blokirati ovog pacijenta?", "Blokiranje pacijenta", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (result == MessageBoxResult.Yes)
            {
                pacijentiController.BlokirajPacijenta(pacijent);
                UpdateObservableCollectionsBlokiraj(pacijent);
            }
        }

        private void UpdateObservableCollectionsBlokiraj(PacijentDTO pacijent)
        {
            if (FormPregledi.Pregledi != null)
                for (int i = FormPregledi.Pregledi.Count - 1; i >= 0; i--)
                    if (FormPregledi.Pregledi[i].Pacijent.Jmbg == pacijent.Jmbg)
                        FormPregledi.Pregledi.RemoveAt(i);
            if (!pacijent.Guest)
                FormSekretar.RedovniPacijenti.Remove(pacijent);
            else
                FormSekretar.GostiPacijenti.Remove(pacijent);
            FormSekretar.ObrisaniPacijenti.Add(pacijent);
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
                InicijalizujGUIZaOdabranogPacijentaPrikazi(pacijent);
            }
            else
                MessageBox.Show("Odaberite pacijenta za prikaz njegovih informacija.", "Prikaz pacijenta", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void InicijalizujGUIZaOdabranogPacijentaPrikazi(PacijentDTO pacijent) 
        {
            if (!pacijent.Guest)
            {
                FormPrikazPacijenta window = new FormPrikazPacijenta();
                ZdravstveniKartonDTO zdravstveniKarton = zdravstveniKartonController.GetZdravstveniKartonByID(pacijent.BrojKartona);
                InicijalizujGUIElementeRedovniPacijentPrikazi(pacijent, window);
                InicijalizujGUIElementeZdravstveniKartonPrikazi(zdravstveniKarton, window);

                window.ShowDialog();
            }
            else
            {
                FormPrikazGosta window = new FormPrikazGosta();
                InicijalizujGUIElementeGostPacijentPrikazi(pacijent, window);

                window.ShowDialog();
            }
        }

        private void InicijalizujGUIElementeGostPacijentPrikazi(PacijentDTO pacijent, FormPrikazGosta window)
        {
            window.lblIme.Content = pacijent.Ime;
            window.lblPrezime.Content = pacijent.Prezime;
            if (pacijent.Pol == Pol.muski)
                window.lblPol.Content = "Muški";
            else
                window.lblPol.Content = "Ženski";
            window.lblDatumRodjenja.Content = pacijent.DatumRodjenja.ToShortDateString();
            window.lblJMBG.Content = pacijent.Jmbg;
            window.lblAdresaStanovanja.Content = pacijent.AdresaStanovanja;
            window.lblBrojTelefona.Content = pacijent.BrojTelefona;
            window.lblEmail.Content = pacijent.Email;
        }

        private void InicijalizujGUIElementeRedovniPacijentPrikazi(PacijentDTO pacijent, FormPrikazPacijenta window) 
        {
            window.lblIme.Content = pacijent.Ime;
            window.lblPrezime.Content = pacijent.Prezime;
            if (pacijent.Pol == Pol.muski)
                window.lblPol.Content = "Muški";
            else
                window.lblPol.Content = "Ženski";
            window.lblDatumRodjenja.Content = pacijent.DatumRodjenja.ToShortDateString();
            window.lblJMBG.Content = pacijent.Jmbg;
            window.lblAdresaStanovanja.Content = pacijent.AdresaStanovanja;
            window.lblBrojTelefona.Content = pacijent.BrojTelefona;
            window.lblEmail.Content = pacijent.Email;

            window.lblKorIme.Content = pacijent.KorisnickoIme;
            window.lblLoz.Content = pacijent.Lozinka;
        }

        private void InicijalizujGUIElementeZdravstveniKartonPrikazi(ZdravstveniKartonDTO zdravstveniKarton, FormPrikazPacijenta window) 
        {
            window.lblIDKar.Content = zdravstveniKarton.BrojKartona.ToString();
            window.lblZan.Content = zdravstveniKarton.Zanimanje;
            window.checkOsig.IsChecked = zdravstveniKarton.Osiguranje;
            window.checkOsig.IsEnabled = false;
            if (zdravstveniKarton.BracniStatus == BracniStatus.neozenjen_neudata)
                window.lblBrStatus.Content = "Neoženjen/Neudata";
            else if (zdravstveniKarton.BracniStatus == BracniStatus.ozenjen_udata)
                window.lblBrStatus.Content = "Oženjen/Udata";
            else if (zdravstveniKarton.BracniStatus == BracniStatus.udovac_udovica)
                window.lblBrStatus.Content = "Udovac/Udovica";
            else if (zdravstveniKarton.BracniStatus == BracniStatus.razveden_razvedena)
                window.lblBrStatus.Content = "Razveden/Razvedena";
        }

        private void Button_Click_Obavestenja(object sender, RoutedEventArgs e)
        {
            FormObavestenja window = new FormObavestenja();
            window.btnObavestenja.Background = new SolidColorBrush(Color.FromArgb(255, 169, 169, 169));
            window.Show();
            Close();
        }

        private void Button_Click_Pregledi(object sender, RoutedEventArgs e)
        {
            FormPregledi window = new FormPregledi();
            window.btnPregledi.Background = new SolidColorBrush(Color.FromArgb(255, 169, 169, 169));
            window.Show();
            Close();
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
            PacijentDTO pacijent = (PacijentDTO)dataGridObrisaniPacijenti.SelectedItem;
            if (pacijent != null)
            {
                MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite odblokirati ovog pacijenta?", "Odblokiranje pacijenta", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    pacijentiController.OdblokirajPacijenta(pacijent);
                    UpdateObservableCollectionsOdblokiraj(pacijent);
                }
            }
            else
                MessageBox.Show("Odaberite pacijenta za odblokiranje.", "Odblokiranje pacijenta", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void UpdateObservableCollectionsOdblokiraj(PacijentDTO pacijent) 
        {
            FormSekretar.ObrisaniPacijenti.Remove(pacijent);
            if (!pacijent.Guest)
                FormSekretar.RedovniPacijenti.Add(pacijent);
            else
                FormSekretar.GostiPacijenti.Add(pacijent);
        }

        private void Button_Click_Lekari(object sender, RoutedEventArgs e)
        {
            FormLekari window = new FormLekari();
            window.btnLekari.Background = new SolidColorBrush(Color.FromArgb(255, 169, 169, 169));
            window.Show();
            Close();
        }

        private void Button_Click_Statistika(object sender, RoutedEventArgs e)
        {
            FormStatistika window = new FormStatistika();
            window.btnStats.Background = new SolidColorBrush(Color.FromArgb(255, 169, 169, 169));
            window.Show();
            Close();
        }

        private void Button_Click_Odjavljivanje(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite da se odjavite?", "Odjavljivanje", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (result == MessageBoxResult.Yes)
            {
                MainWindow window = new MainWindow();
                window.Show();
                Close();
            }
        }

        private void Button_Click_Feedback(object sender, RoutedEventArgs e)
        {
            var s = new FormFeedback();
            s.ShowDialog();
        }
    }
}
