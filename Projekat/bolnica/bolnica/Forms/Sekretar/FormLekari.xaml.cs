using bolnica;
using Bolnica.Model.Korisnici;
using Bolnica.Sekretar;
using Model.Korisnici;
using Model.Pacijenti;
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

namespace Bolnica.Forms.Sekretar
{
    /// <summary>
    /// Interaction logic for FormLekari.xaml
    /// </summary>
    public partial class FormLekari : Window
    {
        public static ObservableCollection<Lekar> Lekari
        {
            get;
            set;
        }
        public static ObservableCollection<Lekar> NoviLekari
        {
            get;
            set;
        }
        private FileRepositoryLekar storage;
        public FormLekari()
        {
            InitializeComponent();
            dataGridLekari.DataContext = this;
            dataGridLekariNovi.DataContext = this;

            NoviLekari = new ObservableCollection<Lekar>();
            Lekari = new ObservableCollection<Lekar>();
            storage = new FileRepositoryLekar();
            List<Lekar> lekari = storage.GetAll();
            foreach (Lekar l in lekari)
                if (l.PostavljenaSmena)
                    Lekari.Add(l);
                else
                    NoviLekari.Add(l);
        }

        private void Button_Click_Pacijenti(object sender, RoutedEventArgs e)
        {
            var s = new FormSekretar();
            s.btnPacijenti.Background = new SolidColorBrush(Color.FromArgb(255, 169, 169, 169));
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

        private void Button_Click_Obavestenja(object sender, RoutedEventArgs e)
        {
            var s = new FormObavestenja();
            s.btnObavestenja.Background = new SolidColorBrush(Color.FromArgb(255, 169, 169, 169));
            s.Show();
            this.Close();
        }

        private void SearchBoxLekariKeyUp(object sender, KeyEventArgs e)
        {
            string[] searchBoxText = searchBoxLekari.Text.Split(" ");

            if (searchBoxText.Length == 1)
            {
                var filtered = Lekari.Where(lekar => lekar.Ime.StartsWith(searchBoxLekari.Text, StringComparison.InvariantCultureIgnoreCase));
                dataGridLekari.ItemsSource = filtered;
            }
            else if (searchBoxText.Length == 2)
            {
                var filtered = Lekari.Where(lekar => lekar.Ime.StartsWith(searchBoxText[0], StringComparison.InvariantCultureIgnoreCase) && lekar.Prezime.StartsWith(searchBoxText[1], StringComparison.InvariantCultureIgnoreCase));
                dataGridLekari.ItemsSource = filtered;
            }
            else if (searchBoxText.Length > 2)
            {
                dataGridLekari.ItemsSource = null;
                dataGridLekari.Items.Refresh();
            }
        }

        private void DataGridLekariMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

        private void SearchBoxLekariNoviKeyUp(object sender, KeyEventArgs e)
        {
            string[] searchBoxText = searchBoxLekariNovi.Text.Split(" ");

            if (searchBoxText.Length == 1)
            {
                var filtered = NoviLekari.Where(lekar => lekar.Ime.StartsWith(searchBoxLekariNovi.Text, StringComparison.InvariantCultureIgnoreCase));
                dataGridLekariNovi.ItemsSource = filtered;
            }
            else if (searchBoxText.Length == 2)
            {
                var filtered = NoviLekari.Where(lekar => lekar.Ime.StartsWith(searchBoxText[0], StringComparison.InvariantCultureIgnoreCase) && lekar.Prezime.StartsWith(searchBoxText[1], StringComparison.InvariantCultureIgnoreCase));
                dataGridLekariNovi.ItemsSource = filtered;
            }
            else if (searchBoxText.Length > 2)
            {
                dataGridLekariNovi.ItemsSource = null;
                dataGridLekariNovi.Items.Refresh();
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ti1.IsSelected)
            {
                searchBoxLekari.Visibility = Visibility.Visible;
                searchBoxLekariNovi.Visibility = Visibility.Hidden;
                btnDodajSmenu.Visibility = Visibility.Hidden;
                btnDodajSmenu.IsEnabled = false;
                btnZakaziGodisnji.Visibility = Visibility.Visible;
                btnZakaziGodisnji.IsEnabled = true;
                btnGenerisiIzvestaj.Visibility = Visibility.Visible;
                btnGenerisiIzvestaj.IsEnabled = true;
            }
            else if (ti2.IsSelected)
            {
                searchBoxLekariNovi.Visibility = Visibility.Visible;
                searchBoxLekari.Visibility = Visibility.Hidden;
                btnDodajSmenu.Visibility = Visibility.Visible;
                btnDodajSmenu.IsEnabled = true;
                btnZakaziGodisnji.Visibility = Visibility.Hidden;
                btnZakaziGodisnji.IsEnabled = false;
                btnGenerisiIzvestaj.Visibility = Visibility.Hidden;
                btnGenerisiIzvestaj.IsEnabled = false;
            }
        }

        private void DodajSmenu(object sender, RoutedEventArgs e)
        {
            Lekar lekar = (Lekar)dataGridLekariNovi.SelectedItem;
            if (lekar != null)
            {
                var s = new FormSmene(lekar.Jmbg);
                s.ShowDialog();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Odaberite lekara za dodavanje smene.",
                                          "Dodavanje smene",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Information);
            }
        }

        private void ZakaziGodisnji(object sender, RoutedEventArgs e)
        {
            Lekar lekar = (Lekar)dataGridLekari.SelectedItem;
            if (lekar != null)
            {
                var s = new FormGodisnji(lekar.Jmbg);
                s.ShowDialog();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Odaberite lekara za zakazivanje godišnjeg.",
                                          "Zakazivanje godišnjeg",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Information);
            }
        }

        private void Button_Click_Statistika(object sender, RoutedEventArgs e)
        {
            var s = new FormStatistika();
            s.btnStats.Background = new SolidColorBrush(Color.FromArgb(255, 169, 169, 169));
            s.Show();
            this.Close();
        }

        private void GenerisiIzvestaj(object sender, RoutedEventArgs e)
        {
            Lekar lekar = (Lekar)dataGridLekari.SelectedItem;
            if (lekar != null)
            {
                IzvestajParametri s = new IzvestajParametri(lekar.Jmbg);
                s.ShowDialog();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Odaberite lekara za generisanje njegovog izveštaja.",
                                          "Generisanje izveštaja",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Information);
            }
        }

        private void Button_Click_Odjavljivanje(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite da se odjavite?",
                                              "Odjavljivanje",
                                              MessageBoxButton.YesNo,
                                              MessageBoxImage.Exclamation);
            if (result == MessageBoxResult.Yes)
            {
                var s = new MainWindow();
                s.Show();
                this.Close();
            }
        }
    }
}
