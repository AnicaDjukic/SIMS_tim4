using Bolnica.Forms;
using Bolnica.Forms.Sekretar;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.Repository.Pregledi;
using Bolnica.Repository.Prostorije;
using Model.Korisnici;
using Model.Pacijenti;
using Model.Pregledi;
using Model.Prostorije;
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
    /// Interaction logic for FormPregledi.xaml
    /// </summary>
    public partial class FormPregledi : Window
    {
        public static List<Pregled> listaPregleda = new List<Pregled>();
        public static List<Operacija> listaOperacija = new List<Operacija>();
        public static ObservableCollection<PrikazPregleda> Pregledi { get; set; }
        public static List<Lekar> listaLekara = new List<Lekar>();
        private FileRepositoryPregled sviPregledi = new FileRepositoryPregled();
        private FileRepositoryOperacija sveOperacije = new FileRepositoryOperacija();
        private FileRepositoryPacijent sviPacijenti = new FileRepositoryPacijent();
        private FileRepositoryProstorija sveProstorije = new FileRepositoryProstorija();
        private FileRepositoryLekar sviLekari = new FileRepositoryLekar();
        private List<Pacijent> listaPacijenata = new List<Pacijent>();
        private List<Prostorija> listaProstorija = new List<Prostorija>();
        private PrikazPregleda prikazPregleda = new PrikazPregleda();
        private PrikazOperacije prikazOperacije = new PrikazOperacije();
        private int selektovanComboSearchItemIndex;

        public FormPregledi()
        {
            InitializeComponent();
            dataGridPregledi.DataContext = this;
            Pregledi = new ObservableCollection<PrikazPregleda>();

            selektovanComboSearchItemIndex = 0;
            listaPregleda = sviPregledi.GetAll();
            listaOperacija = sveOperacije.GetAll();
            listaPacijenata = sviPacijenti.GetAll();
            listaProstorija = sveProstorije.GetAll();
            listaLekara = sviLekari.GetAll();

            for (int i = 0; i < listaPregleda.Count; i++)
            {
                if (listaPregleda[i].Zavrsen.Equals(false))
                {
                    prikazPregleda = new PrikazPregleda();
                    prikazPregleda.Id = listaPregleda[i].Id;
                    prikazPregleda.Trajanje = listaPregleda[i].Trajanje;
                    prikazPregleda.Zavrsen = listaPregleda[i].Zavrsen;
                    prikazPregleda.Datum = listaPregleda[i].Datum;
                    prikazPregleda.Anamneza.Id = listaPregleda[i].Anamneza.Id;
                    prikazPregleda.Hitan = listaPregleda[i].Hitan;
                    for (int p = 0; p < listaPacijenata.Count; p++)
                    {
                        if (listaPregleda[i].Pacijent.Jmbg.Equals(listaPacijenata[p].Jmbg) && listaPacijenata[p].Obrisan == false)
                        {
                            prikazPregleda.Pacijent = listaPacijenata[p];
                            break;
                        }
                    }
                    for (int p = 0; p < listaProstorija.Count; p++)
                    {
                        if (listaPregleda[i].Prostorija.BrojProstorije.Equals(listaProstorija[p].BrojProstorije) && listaProstorija[p].Obrisana == false)
                        {
                            prikazPregleda.Prostorija = listaProstorija[p];
                            break;
                        }
                    }
                    for (int p = 0; p < listaLekara.Count; p++)
                    {
                        if (listaPregleda[i].Lekar.Jmbg.Equals(listaLekara[p].Jmbg))
                        {
                            prikazPregleda.Lekar = listaLekara[p];
                        }
                    }
                    Pregledi.Add(prikazPregleda);
                }
            }

            for (int i = 0; i < listaOperacija.Count; i++)
            {
                if (listaOperacija[i].Zavrsen.Equals(false))
                {
                    prikazOperacije = new PrikazOperacije();
                    prikazOperacije.Id = listaOperacija[i].Id;
                    prikazOperacije.Trajanje = listaOperacija[i].Trajanje;
                    prikazOperacije.Zavrsen = listaOperacija[i].Zavrsen;
                    prikazOperacije.Datum = listaOperacija[i].Datum;
                    prikazOperacije.Anamneza.Id = listaOperacija[i].Anamneza.Id;
                    prikazOperacije.Hitan = listaOperacija[i].Hitan;
                    prikazOperacije.TipOperacije = listaOperacija[i].TipOperacije;
                    for (int p = 0; p < listaPacijenata.Count; p++)
                    {
                        if (listaOperacija[i].Pacijent.Jmbg.Equals(listaPacijenata[p].Jmbg) && listaPacijenata[p].Obrisan == false)
                        {
                            prikazOperacije.Pacijent = listaPacijenata[p];
                            break;
                        }
                    }
                    for (int p = 0; p < listaProstorija.Count; p++)
                    {
                        if (listaOperacija[i].Prostorija.BrojProstorije.Equals(listaProstorija[p].BrojProstorije) && listaProstorija[p].Obrisana == false)
                        {
                            prikazOperacije.Prostorija = listaProstorija[p];
                            break;
                        }
                    }
                    for (int p = 0; p < listaLekara.Count; p++)
                    {
                        if (listaOperacija[i].Lekar.Jmbg.Equals(listaLekara[p].Jmbg))
                        {
                            prikazOperacije.Lekar = listaLekara[p];
                        }
                    }
                    Pregledi.Add(prikazOperacije);
                }
            }
        }

        private void ZakaziTermin(object sender, RoutedEventArgs e)
        {
            FormZakaziPregled s = new FormZakaziPregled();
            s.ShowDialog();
        }

        private void PomeriTermin(object sender, RoutedEventArgs e)
        {
            if (dataGridPregledi.SelectedCells.Count > 0)
            {
                var objekat = dataGridPregledi.SelectedValue;
                PrikazPregleda pp = new PrikazPregleda();
                pp.Pacijent = new Pacijent();

                if (objekat.GetType().Equals(prikazPregleda.GetType()))
                {
                    PrikazPregleda pregled = objekat as PrikazPregleda;
                    for (int i = 0; i < listaPregleda.Count; i++)
                    {
                        if (pregled.Id.Equals(listaPregleda[i].Id))
                        {
                            pp = dataGridPregledi.SelectedItem as PrikazPregleda;
                            FormPomeriPregled s = new FormPomeriPregled(pp);
                            s.ShowDialog();
                            break;
                        }
                    }
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Samo lekar može upravljati operacijama.",
                                          "Pomeranje termina",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Odaberite termin za pomeranje.",
                                          "Pomeranje termina",
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

        private void Button_Click_Obavestenja(object sender, RoutedEventArgs e)
        {
            var s = new FormObavestenja();
            s.btnObavestenja.Background = new SolidColorBrush(Color.FromArgb(255, 169, 169, 169));
            s.Show();
            this.Close();
        }

        private void OtkaziTermin(object sender, RoutedEventArgs e)
        {
            if (dataGridPregledi.SelectedCells.Count > 0)
            {
                if (!dataGridPregledi.SelectedValue.GetType().Equals(prikazPregleda.GetType())) 
                {
                    MessageBoxResult msgBoxResult = MessageBox.Show("Samo lekar može upravljati operacijama.",
                                                  "Otkazivanje termina",
                                                  MessageBoxButton.OK,
                                                  MessageBoxImage.Information);
                    return;
                }

                MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite otkazati ovaj termin?",
                                          "Otkazivanje termina",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    var objekat = dataGridPregledi.SelectedValue;
                    if (objekat.GetType().Equals(prikazPregleda.GetType()))
                    {
                        PrikazPregleda pregled = objekat as PrikazPregleda;
                        for (int i = 0; i < listaPregleda.Count; i++)
                        {
                            if (pregled.Id.Equals(listaPregleda[i].Id))
                            {

                                sviPregledi.Delete(listaPregleda[i]);
                                listaPregleda.RemoveAt(i);
                                break;
                            }
                        }
                    }
                    int index = dataGridPregledi.SelectedIndex;
                    Pregledi.RemoveAt(index);
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Odaberite termin za otkazivanje.",
                                          "Otkazivanje termina",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Information);
            }
        }

        private void ZakaziHitanTermin(object sender, RoutedEventArgs e)
        {
            FormZakaziHitanTermin s = new FormZakaziHitanTermin(dataGridPregledi);
            s.ShowDialog();
        }

        private void SearchBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (comboSearch.SelectedIndex == 0)
            {
                string[] searchBoxText = searchBox.Text.Split(" ");

                if (searchBoxText.Length == 1)
                {
                    var filtered = Pregledi.Where(termin => termin.Pacijent.Ime.StartsWith(searchBox.Text, StringComparison.InvariantCultureIgnoreCase));
                    dataGridPregledi.ItemsSource = filtered;
                }
                else if (searchBoxText.Length == 2)
                {
                    var filtered = Pregledi.Where(termin => termin.Pacijent.Ime.StartsWith(searchBoxText[0], StringComparison.InvariantCultureIgnoreCase) && termin.Pacijent.Prezime.StartsWith(searchBoxText[1], StringComparison.InvariantCultureIgnoreCase));
                    dataGridPregledi.ItemsSource = filtered;
                }
                else if (searchBoxText.Length > 2)
                {
                    dataGridPregledi.ItemsSource = null;
                    dataGridPregledi.Items.Refresh();
                }
            }
            else if (comboSearch.SelectedIndex == 1)
            {
                string[] searchBoxText = searchBox.Text.Split(" ");

                if (searchBoxText.Length == 1)
                {
                    var filtered = Pregledi.Where(termin => termin.Lekar.Ime.StartsWith(searchBox.Text, StringComparison.InvariantCultureIgnoreCase));
                    dataGridPregledi.ItemsSource = filtered;
                }
                else if (searchBoxText.Length == 2)
                {
                    var filtered = Pregledi.Where(termin => termin.Lekar.Ime.StartsWith(searchBoxText[0], StringComparison.InvariantCultureIgnoreCase) && termin.Lekar.Prezime.StartsWith(searchBoxText[1], StringComparison.InvariantCultureIgnoreCase));
                    dataGridPregledi.ItemsSource = filtered;
                }
                else if (searchBoxText.Length > 2)
                {
                    dataGridPregledi.ItemsSource = null;
                    dataGridPregledi.Items.Refresh();
                }
            }
            else 
            {
                var filtered = Pregledi.Where(termin => termin.Prostorija.BrojProstorije.StartsWith(searchBox.Text, StringComparison.InvariantCultureIgnoreCase));
                dataGridPregledi.ItemsSource = filtered;
            }
        }

        private void ComboSearchDropDownClosed(object sender, EventArgs e)
        {
            if (comboSearch.SelectedIndex == selektovanComboSearchItemIndex)
                return;
            searchBox.Text = "";
            dataGridPregledi.ItemsSource = Pregledi;
            selektovanComboSearchItemIndex = comboSearch.SelectedIndex;
        }

        private void DataGridPreglediLeftMouseButtonDown(object sender, MouseButtonEventArgs e)
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