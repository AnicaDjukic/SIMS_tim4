using bolnica;
using Bolnica.Forms;
using Bolnica.Forms.Sekretar;
using Bolnica.Model.Korisnici;
using Bolnica.Repository.Korisnici;
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

namespace Bolnica.Sekretar
{
    /// <summary>
    /// Interaction logic for FormObavestenja.xaml
    /// </summary>
    public partial class FormObavestenja : Window
    {
        public static ObservableCollection<Obavestenje> Obavestenja
        {
            get;
            set;
        }
        private FileRepositoryObavestenje storage;
        public static bool clickedDodaj;

        public FormObavestenja()
        {
            InitializeComponent();
            dataGridObavestenja.DataContext = this;
            Obavestenja = new ObservableCollection<Obavestenje>();
            storage = new FileRepositoryObavestenje();

            List<Obavestenje> obavestenja = storage.GetAll();
            foreach (Obavestenje o in obavestenja)
            {
                if (o.Obrisan == false)
                    Obavestenja.Add(o);
            }
        }

        private void Button_Click_Dodaj(object sender, RoutedEventArgs e)
        {
            clickedDodaj = true;
            FormDodajObavestenje s = new FormDodajObavestenje(-1, null);
            s.btnDodajPacijente.Content = "Dodaj";
            s.btnDodajPrimaoce.Content = "Dodaj";
            s.ShowDialog();
        }

        private void Button_Click_Prikazi(object sender, RoutedEventArgs e)
        {
            Obavestenje obavestenje = (Obavestenje)dataGridObavestenja.SelectedItem;
            if (obavestenje != null)
            {
                List<Obavestenje> obavestenja = storage.GetAll();
                var s = new FormPrikazObavestenja(obavestenje.Id);
                foreach (Obavestenje o in obavestenja)
                {
                    if (o.Id == obavestenje.Id)
                    {
                        s.lblDatum.Content = o.Datum.ToShortDateString();
                        s.lblNaslov.Content = o.Naslov;
                        s.tbText.Text = o.Sadrzaj;
                        
                        s.ShowDialog();
                        break;
                    }
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Odaberite obaveštenje za njegov prikaz.",
                                          "Prikaz obaveštenja",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Information);
            }
        }

        private void Button_Click_Izmeni(object sender, RoutedEventArgs e)
        {
            Obavestenje obavestenje = (Obavestenje)dataGridObavestenja.SelectedItem;
            if (obavestenje != null)
            {
                List<Obavestenje> obavestenja = storage.GetAll();
                FormDodajObavestenje s = new FormDodajObavestenje(obavestenje.Id, obavestenje.Korisnici);

                s.btnDodajPacijente.Content = "Izmeni";
                s.btnDodajPrimaoce.Content = "Izmeni";
                foreach (Obavestenje o in obavestenja)
                {
                    if (o.Id == obavestenje.Id)
                    {
                        s.dpDatum.SelectedDate = o.Datum;
                        s.dpDatum.IsEnabled = false;
                        s.txtNaslov.Text = o.Naslov;
                        s.txtText.Text = o.Sadrzaj;
                        
                        clickedDodaj = false;
                        s.ShowDialog();
                        break;
                    }
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Odaberite obaveštenje za izmenu njegovih informacija.",
                                          "Izmena obaveštenja",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Information);
            }
        }

        private void Button_Click_Obrisi(object sender, RoutedEventArgs e)
        {
            Obavestenje obavestenje = (Obavestenje)dataGridObavestenja.SelectedItem;
            if (obavestenje != null)
            {
                MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite izbrisati ovo obaveštenje?",
                                          "Brisanje obaveštenja",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    Obavestenja.Remove(obavestenje);
                    List<Obavestenje> obavestenja = storage.GetAll();
                    storage.Delete(obavestenje);
                    obavestenje.Obrisan = true;
                    storage.Save(obavestenje);
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Odaberite obaveštenje za brisanje.",
                                          "Brisanje obaveštenja",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Information);
            }
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

        private void DataGridObavestenjaMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

        private void SearchBoxObavestenjaKeyUp(object sender, KeyEventArgs e)
        {
            var filtered = Obavestenja.Where(obavestenje => obavestenje.Naslov.StartsWith(searchBoxObavestenja.Text, StringComparison.InvariantCultureIgnoreCase));
            dataGridObavestenja.ItemsSource = filtered;
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
