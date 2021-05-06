using Bolnica.Forms;
using Bolnica.Forms.Sekretar;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Model.Korisnici;
using Model.Pacijenti;
using Model.Pregledi;
using Model.Prostorije;
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
        private FileStoragePregledi sviPregledi = new FileStoragePregledi();
        private FileStoragePregledi sveOperacije = new FileStoragePregledi();
        private FileStoragePacijenti sviPacijenti = new FileStoragePacijenti();
        private FileStorageProstorija sveProstorije = new FileStorageProstorija();
        private FileStorageLekar sviLekari = new FileStorageLekar();
        private List<Pacijent> listaPacijenata = new List<Pacijent>();
        private List<Prostorija> listaProstorija = new List<Prostorija>();
        private PrikazPregleda prikazPregleda = new PrikazPregleda();
        private PrikazOperacije prikazOperacije = new PrikazOperacije();

        public FormPregledi()
        {
            InitializeComponent();
            dataGridPregledi.DataContext = this;
            Pregledi = new ObservableCollection<PrikazPregleda>();
            

            listaPregleda = sviPregledi.GetAllPregledi();
            listaOperacija = sveOperacije.GetAllOperacije();
            listaPacijenata = sviPacijenti.GetAll();
            listaProstorija = sveProstorije.GetAllProstorije();
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
                    prikazPregleda.AnamnezaId = listaPregleda[i].AnamnezaId;
                    prikazPregleda.Hitan = listaPregleda[i].Hitan;
                    for (int p = 0; p < listaPacijenata.Count; p++)
                    {
                        if (listaPregleda[i].pacijentJmbg.Equals(listaPacijenata[p].Jmbg) && listaPacijenata[p].Obrisan == false)
                        {
                            prikazPregleda.Pacijent = listaPacijenata[p];
                            break;
                        }
                    }
                    for (int p = 0; p < listaProstorija.Count; p++)
                    {
                        if (listaPregleda[i].brojProstorije.Equals(listaProstorija[p].BrojProstorije) && listaProstorija[p].Obrisana == false)
                        {
                            prikazPregleda.Prostorija = listaProstorija[p];
                            break;
                        }
                    }
                    for (int p = 0; p < listaLekara.Count; p++)
                    {
                        if (listaPregleda[i].lekarJmbg.Equals(listaLekara[p].Jmbg))
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
                    prikazOperacije.AnamnezaId = listaOperacija[i].AnamnezaId;
                    prikazOperacije.Hitan = listaOperacija[i].Hitan;
                    prikazOperacije.TipOperacije = listaOperacija[i].TipOperacije;
                    for (int p = 0; p < listaOperacija.Count; p++)
                    {
                        if (listaOperacija[i].pacijentJmbg.Equals(listaPacijenata[p].Jmbg) && listaPacijenata[p].Obrisan == false)
                        {
                            prikazOperacije.Pacijent = listaPacijenata[p];
                            break;
                        }
                    }
                    for (int p = 0; p < listaProstorija.Count; p++)
                    {
                        if (listaOperacija[i].brojProstorije.Equals(listaProstorija[p].BrojProstorije) && listaProstorija[p].Obrisana == false)
                        {
                            prikazOperacije.Prostorija = listaProstorija[p];
                            break;
                        }
                    }
                    for (int p = 0; p < listaLekara.Count; p++)
                    {
                        if (listaOperacija[i].lekarJmbg.Equals(listaLekara[p].Jmbg))
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
            s.Show();
            this.Close();
        }

        private void Button_Click_Obavestenja(object sender, RoutedEventArgs e)
        {
            var s = new FormObavestenja();
            s.Show();
            this.Close();
        }

        private void OtkaziTermin(object sender, RoutedEventArgs e)
        {
            if (dataGridPregledi.SelectedCells.Count > 0)
            {
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
    }
}