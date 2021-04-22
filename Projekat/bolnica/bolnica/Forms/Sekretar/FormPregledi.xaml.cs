using Bolnica.Forms;
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
        public static ObservableCollection<PrikazPregleda> Pregledi { get; set; }
        public static List<Lekar> listaLekara = new List<Lekar>();
        private Lekar lekarTrenutni = new Lekar();
        private Lekar lekarPomocni = new Lekar();
        private Lekar l3 = new Lekar();
        private Lekar l4 = new Lekar();
        private FileStoragePregledi sviPregledi = new FileStoragePregledi();
        private FileStoragePacijenti sviPacijenti = new FileStoragePacijenti();
        private FileStorageProstorija sveProstorije = new FileStorageProstorija();
        private List<Pacijent> listaPacijenata = new List<Pacijent>();
        private List<Prostorija> listaProstorija = new List<Prostorija>();
        private PrikazPregleda prikazPregleda = new PrikazPregleda();

        public FormPregledi()
        {
            InitializeComponent();
            dataGridPregledi.DataContext = this;
            Pregledi = new ObservableCollection<PrikazPregleda>();
            lekarTrenutni.AdresaStanovanja = "AAA";
            lekarTrenutni.BrojSlobodnihDana = 15;
            lekarTrenutni.BrojTelefona = "111111";
            lekarTrenutni.DatumRodjenja = new DateTime();
            lekarTrenutni.Email = "dada@dada.com";
            lekarTrenutni.GodineStaza = 11;
            lekarTrenutni.Ime = "Mico";
            lekarTrenutni.Prezime = "Govedarica";
            lekarTrenutni.Jmbg = "342425";
            lekarTrenutni.KorisnickoIme = "Pero";
            lekarTrenutni.Lozinka = "Admin";
            lekarTrenutni.Mbr = 21312;
            lekarTrenutni.Plata = 1000;
            Specijalizacija sp = new Specijalizacija();
            sp.Id = 121;
            sp.Naziv = "neka";
            sp.OblastMedicine = "nekaa";
            lekarTrenutni.Specijalizacija = sp;
            lekarTrenutni.TipKorisnika = TipKorisnika.lekar;
            lekarTrenutni.Zaposlen = true;

            lekarPomocni.AdresaStanovanja = "BBB";
            lekarPomocni.BrojSlobodnihDana = 15;
            lekarPomocni.BrojTelefona = "22222";
            lekarPomocni.DatumRodjenja = new DateTime();
            lekarPomocni.Email = "bada@dada.com";
            lekarPomocni.GodineStaza = 7;
            lekarPomocni.Ime = "Mio";
            lekarPomocni.Prezime = "Prodano";
            lekarPomocni.Jmbg = "222222";
            lekarPomocni.KorisnickoIme = "Peki";
            lekarPomocni.Lozinka = "Baja";
            lekarPomocni.Mbr = 3232;
            lekarPomocni.Plata = 10000;
            Specijalizacija spa = new Specijalizacija();
            spa.Id = 1211;
            spa.Naziv = "neeka";
            spa.OblastMedicine = "nekaaa";
            lekarPomocni.Specijalizacija = spa;
            lekarPomocni.TipKorisnika = TipKorisnika.lekar;
            lekarPomocni.Zaposlen = true;

            l3.AdresaStanovanja = "Tolstojeva 1";
            l3.BrojSlobodnihDana = 20;
            l3.BrojTelefona = "0642354578";
            l3.DatumRodjenja = new DateTime(1965, 3, 3);
            l3.Email = "pap@gmail.com";
            l3.GodineStaza = 30;
            l3.Ime = "Vatroslav";
            l3.Prezime = "Pap";
            l3.Jmbg = "0303965123456";
            l3.KorisnickoIme = "vatro";
            l3.Lozinka = "vatro";
            l3.Mbr = 123123;
            l3.Plata = 15000;
            Specijalizacija sp3 = new Specijalizacija();
            sp3.Id = 1251;
            sp3.Naziv = "kardioloski majstor";
            sp3.OblastMedicine = "kardiologija";
            l3.Specijalizacija = sp3;
            l3.TipKorisnika = TipKorisnika.lekar;
            l3.Zaposlen = true;

            l4.AdresaStanovanja = "Balzakova 21";
            l4.BrojSlobodnihDana = 17;
            l4.BrojTelefona = "0613579624";
            l4.DatumRodjenja = new DateTime(1988, 9, 9);
            l4.Email = "bodi@gmail.com";
            l4.GodineStaza = 6;
            l4.Ime = "Radmilo";
            l4.Prezime = "Bodiroga";
            l4.Jmbg = "090988131533";
            l4.KorisnickoIme = "bodi";
            l4.Lozinka = "bodi";
            l4.Mbr = 123456;
            l4.Plata = 8000;
            Specijalizacija sp4 = new Specijalizacija();
            sp4.Id = 1251;
            sp4.Naziv = "slusni specijalista";
            sp4.OblastMedicine = "otorinolaringologija";
            l4.Specijalizacija = sp3;
            l4.TipKorisnika = TipKorisnika.lekar;
            l4.Zaposlen = true;



            listaLekara.Add(lekarTrenutni);
            listaLekara.Add(lekarPomocni);
            listaLekara.Add(l3);
            listaLekara.Add(l4);


            listaPregleda = sviPregledi.GetAllPregledi();
            listaPacijenata = sviPacijenti.GetAll();
            listaProstorija = sveProstorije.GetAllProstorije();

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
        }

        private void ZakaziTermin(object sender, RoutedEventArgs e)
        {
            FormZakaziPregled s = new FormZakaziPregled(listaLekara);
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
                            FormPomeriPregled s = new FormPomeriPregled(pp, listaLekara);
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
    }
}
