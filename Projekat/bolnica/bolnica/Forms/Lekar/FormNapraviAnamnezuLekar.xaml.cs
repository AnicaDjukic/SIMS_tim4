using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Model.Korisnici;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormNapraviAnamnezuLekar.xaml
    /// </summary>
    public partial class FormNapraviAnamnezuLekar : Window
    {

        public string simptomi { get; set; }
        public string dijagnoza { get; set; }

        public List<PrikazRecepta> recepti { get; set; }
        private int DaLiPostojiAnamneza = 0;
        private List<Lek> sviLekovi = new List<Lek>();
        private FileStoragePregledi skladistePregleda = new FileStoragePregledi();
        private FileStorageLek skladisteLekova = new FileStorageLek();
        private List<Pregled> sviPregledi = new List<Pregled>();
        private List<Operacija> sveOperacije = new List<Operacija>();
        private List<Lekar> sviLekari = new List<Lekar>();
        private Lekar ulogovaniLekar = new Lekar();
        private Pacijent trenutniPacijent = new Pacijent();
        private PrikazPregleda trenutniPregled = new PrikazPregleda();
        private PrikazOperacije trenutnaOperacija = new PrikazOperacije();
        private PrikazPregleda stariPregled = new PrikazPregleda();
        private PrikazOperacije staraOperacija = new PrikazOperacije();
        private List<Anamneza> sveAnamneze = new List<Anamneza>();
        private FileStorageAnamneza skladisteAnamneza = new FileStorageAnamneza();
        private FileStorageLekar skladisteLekara = new FileStorageLekar();
        private int idAnamneze;
        private int DaLiJePregled = 0;

        public static ObservableCollection<PrikazRecepta> Recepti
        {
            get;
            set;
        }


        public FormNapraviAnamnezuLekar(PrikazPregleda izabraniPregled, Lekar ulogovaniLekar)
        {
            DaLiJePregled = 1;
            FiltirajLekove();
            InicirajPodatkeZaPregled(izabraniPregled, ulogovaniLekar);
            InitializeComponent();
            this.DataContext = this;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Application.Current.MainWindow;


            for (int i = 0; i < sveAnamneze.Count; i++)
            {
                if (izabraniPregled.Anamneza.Id == sveAnamneze[i].Id)
                {
                    ProveriValidnostDatumaIzdavanja(izabraniPregled);
                    PopuniAnamnezu(izabraniPregled, i);
                    DaLiPostojiAnamneza = 1;
                    break;
                }
            }

            if (DaLiPostojiAnamneza == 0)
            {
                textSimptomi.Text = simptomi;
                textDijagnoza.Text = dijagnoza;
                Recepti = new ObservableCollection<PrikazRecepta>();
            }

        }

        public FormNapraviAnamnezuLekar(PrikazOperacije izabranaOperacija, Lekar ulogovaniLekar)
        {
            InicirajPodatkeZaOperaciju(izabranaOperacija, ulogovaniLekar);
            FiltirajLekove();
            InitializeComponent();
            this.DataContext = this;
            for (int i = 0; i < sveAnamneze.Count; i++)
            {
                if (izabranaOperacija.Anamneza.Id == sveAnamneze[i].Id)
                {
                    ProveriValidnostDatumaIzdavanja(izabranaOperacija);
                    PopuniAnamnezu(izabranaOperacija, i);
                    DaLiPostojiAnamneza = 1;
                    break;
                }
            }
            if (DaLiPostojiAnamneza == 0)
            {
                textSimptomi.Text = simptomi;
                textDijagnoza.Text = dijagnoza;
                Recepti = new ObservableCollection<PrikazRecepta>();
            }
        }

        private void PopuniAnamnezu(PrikazPregleda izabraniPregled, int i)
        {
            idAnamneze = izabraniPregled.Anamneza.Id;
            simptomi = sveAnamneze[i].Simptomi;
            dijagnoza = sveAnamneze[i].Dijagnoza;
            PrikazRecepta noviPrikazRecepta = new PrikazRecepta();


            for (int r = 0; r < sveAnamneze[i].Recept.Count; r++)
            {
                noviPrikazRecepta = new PrikazRecepta();
                noviPrikazRecepta.DatumIzdavanja = sveAnamneze[i].Recept[r].DatumIzdavanja;
                noviPrikazRecepta.Id = sveAnamneze[i].Recept[r].Id;
                noviPrikazRecepta.Kolicina = sveAnamneze[i].Recept[r].Kolicina;

                noviPrikazRecepta.Trajanje = sveAnamneze[i].Recept[r].Trajanje;
                noviPrikazRecepta.VremeUzimanja = sveAnamneze[i].Recept[r].VremeUzimanja;
                for (int le = 0; le < sviLekovi.Count; le++)
                {
                    if (sveAnamneze[i].Recept[r].Lek.Id.Equals(sviLekovi[le].Id))
                    {
                        noviPrikazRecepta.lek = sviLekovi[le];
                        break;
                    }
                }
                Recepti.Add(noviPrikazRecepta);

            }

        }
        private void PopuniAnamnezu(PrikazOperacije izabranaOperacija, int i)
        {
            idAnamneze = izabranaOperacija.Anamneza.Id;
            simptomi = sveAnamneze[i].Simptomi;
            dijagnoza = sveAnamneze[i].Dijagnoza;
            PrikazRecepta noviPrikazRecepta = new PrikazRecepta();


            for (int r = 0; r < sveAnamneze[i].Recept.Count; r++)
            {
                noviPrikazRecepta = new PrikazRecepta();
                noviPrikazRecepta.DatumIzdavanja = sveAnamneze[i].Recept[r].DatumIzdavanja;
                noviPrikazRecepta.Id = sveAnamneze[i].Recept[r].Id;
                noviPrikazRecepta.Kolicina = sveAnamneze[i].Recept[r].Kolicina;
                noviPrikazRecepta.Trajanje = sveAnamneze[i].Recept[r].Trajanje;
                noviPrikazRecepta.VremeUzimanja = sveAnamneze[i].Recept[r].VremeUzimanja;
                for (int le = 0; le < sviLekovi.Count; le++)
                {
                    if (sveAnamneze[i].Recept[r].Lek.Id.Equals(sviLekovi[le].Id))
                    {
                        noviPrikazRecepta.lek = sviLekovi[le];
                        break;
                    }
                }
                Recepti.Add(noviPrikazRecepta);

            }
        }

        private void ProveriValidnostDatumaIzdavanja(PrikazPregleda izabraniPregled)
        {
            if ((izabraniPregled.Datum > DateTime.Now) || (izabraniPregled.Datum.AddDays(7) < DateTime.Now))
            {
                textDijagnoza.IsEnabled = false;
                textSimptomi.IsEnabled = false;
                DodajButton.IsEnabled = false;
                IzbrisiButton.IsEnabled = false;
                PotvrdiButton.IsEnabled = false;
            }
        }

        private void ProveriValidnostDatumaIzdavanja(PrikazOperacije izabranaOperacija)
        {
            if ((izabranaOperacija.Datum > DateTime.Now) || (izabranaOperacija.Datum.AddDays(7) < DateTime.Now))
            {
                textDijagnoza.IsEnabled = false;
                textSimptomi.IsEnabled = false;
                DodajButton.IsEnabled = false;
                IzbrisiButton.IsEnabled = false;
                PotvrdiButton.IsEnabled = false;
            }

        }
        private void FiltirajLekove()
        {
            sviLekovi = skladisteLekova.GetAll();

            for (int i = 0; i < sviLekovi.Count; i++)
            {
                if (sviLekovi[i].Status.Equals(StatusLeka.odbijen) || sviLekovi[i].Obrisan)
                {
                    sviLekovi.RemoveAt(i);
                    i--;
                }
            }
        }

        private void InicirajPodatkeZaPregled(PrikazPregleda izabraniPregled, Lekar ulogovaniLekar)
        {
            trenutniPacijent = izabraniPregled.Pacijent;
            sveAnamneze = skladisteAnamneza.GetAll();
            Recepti = new ObservableCollection<PrikazRecepta>();
            simptomi = "";
            dijagnoza = "";
            trenutniPregled = izabraniPregled;
            sviLekari = skladisteLekara.GetAll();
            stariPregled = izabraniPregled;
            this.ulogovaniLekar = ulogovaniLekar;
        }
        private void InicirajPodatkeZaOperaciju(PrikazOperacije izabranaOperacija, Lekar ulogovaniLekar)
        {
            trenutniPacijent = izabranaOperacija.Pacijent;
            trenutnaOperacija = izabranaOperacija;
            staraOperacija = izabranaOperacija;
            sveAnamneze = skladisteAnamneza.GetAll();
            simptomi = "";
            dijagnoza = "";
            sviLekovi = skladisteLekova.GetAll();
            sviLekari = skladisteLekara.GetAll();
            this.ulogovaniLekar = ulogovaniLekar;
            Recepti = new ObservableCollection<PrikazRecepta>();
        }

        private void OtkaziDugme(object sender, RoutedEventArgs e)
        {
            OtkaziDugme();
        }

        public void OtkaziDugme()
        {
            this.Close();
        }

        private void DodajLek(object sender, RoutedEventArgs e)
        {
            DodajLek();
        }

        public void DodajLek()
        {
            FormNapraviReceptLekar form = new FormNapraviReceptLekar(trenutniPacijent);
            form.Show();
        }

        public void Potvrdi()
        {
            Anamneza novaAnamneza = PopuniAnamnezu();
            if (DaLiPostojiAnamneza == 0)
            {
                novaAnamneza.Id = IzracunajSlobodniIdAnamneze();
                skladisteAnamneza.Save(novaAnamneza);
                if (DaLiJePregled == 1)
                {
                    AzurirajPregled(novaAnamneza);
                }
                else
                {
                    AzurirajOperaciju(novaAnamneza);
                }
            }
            else
            {
                novaAnamneza.Id = idAnamneze;
                skladisteAnamneza.Izmeni(novaAnamneza);
                if (DaLiJePregled == 1)
                {
                    AzurirajIstorijuPregleda(novaAnamneza);
                }
                else
                {
                    AzurirajIstorijuOperacija(novaAnamneza);
                }
            }
            this.Close();
        }

        public int IzracunajSlobodniIdAnamneze()
        {
            int max = 0;
            for (int id = 0; id < sveAnamneze.Count; id++)
            {
                if (sveAnamneze[id].Id > max)
                {
                    max = sveAnamneze[id].Id;
                }
            }
            return max + 1;
        }
        public Anamneza PopuniAnamnezu()
        {
            Anamneza novaAnamneza = new Anamneza();

            novaAnamneza.Simptomi = textSimptomi.Text;
            novaAnamneza.Dijagnoza = textDijagnoza.Text;
            novaAnamneza.Recept = new List<Recept>();
            for (int i = 0; i < Recepti.Count; i++)
            {
                novaAnamneza.Recept.Add(PopuniRecept(i));
            }

            return novaAnamneza;
        }

        public Recept PopuniRecept(int i)
        {
            Recept recept = new Recept();
            recept.DatumIzdavanja = Recepti[i].DatumIzdavanja;
            recept.Id = Recepti[i].Id;
            recept.Kolicina = Recepti[i].Kolicina;
            recept.Lek = Recepti[i].lek;
            recept.Trajanje = Recepti[i].Trajanje;
            recept.VremeUzimanja = Recepti[i].VremeUzimanja;
            return recept;
        }

        public void AzurirajIstorijuPregleda(Anamneza novaAnamneza)
        {
            for (int p = 0; p < FormLekar.dataListIstorija.Items.Count; p++)
            {
                if (FormLekar.dataListIstorija.Items[p].Equals(stariPregled))
                {
                    stariPregled.Anamneza = novaAnamneza;
                    stariPregled.Zavrsen = true;
                    FormLekar.dataListIstorija.Items[p] = stariPregled;
                    FormLekar.dataIstorija();
                    skladistePregleda.Izmeni(PopuniPregled(novaAnamneza));
                }
            }
        }

        public void AzurirajOperaciju(Anamneza novaAnamneza)
        {
            for (int p = 0; p < FormLekar.dataList.Items.Count; p++)
            {
                if (FormLekar.dataList.Items[p].Equals(staraOperacija))
                {
                    trenutnaOperacija.Anamneza = novaAnamneza;
                    trenutnaOperacija.Zavrsen = true;
                    FormLekar.dataListIstorija.Items.Add(trenutnaOperacija);
                    FormLekar.dataList.Items.RemoveAt(p);
                    FormLekar.dataIstorija();
                    FormLekar.data();
                    skladistePregleda.Izmeni(PopuniOperaciju(novaAnamneza));
                }
            }
        }
        public void AzurirajPregled(Anamneza novaAnamneza)
        {
            for (int p = 0; p < FormLekar.dataList.Items.Count; p++)
            {
                if (FormLekar.dataList.Items[p].Equals(stariPregled))
                {
                    trenutniPregled.Anamneza = novaAnamneza;
                    trenutniPregled.Zavrsen = true;
                    FormLekar.dataListIstorija.Items.Add(trenutniPregled);
                    FormLekar.dataList.Items.RemoveAt(p);
                    FormLekar.dataIstorija();
                    FormLekar.data();
                    skladistePregleda.Izmeni(PopuniPregled(novaAnamneza));
                }
            }
        }
        public void AzurirajIstorijuOperacija(Anamneza novaAnamneza)
        {
            for (int p = 0; p < FormLekar.dataListIstorija.Items.Count; p++)
            {
                if (FormLekar.dataListIstorija.Items[p].Equals(staraOperacija))
                {
                    trenutnaOperacija.Anamneza = novaAnamneza;
                    trenutnaOperacija.Zavrsen = true;
                    FormLekar.dataListIstorija.Items[p] = trenutnaOperacija;
                    FormLekar.dataIstorija();
                    skladistePregleda.Izmeni(PopuniOperaciju(novaAnamneza));
                }
            }
        }
        public Operacija PopuniOperaciju(Anamneza novaAnamneza)
        {
            Operacija novaOperacija = new Operacija();
            novaOperacija.Id = trenutnaOperacija.Id;
            novaOperacija.Hitan = trenutnaOperacija.Hitan;
            novaOperacija.Lekar = trenutnaOperacija.Lekar;
            novaOperacija.Pacijent = trenutnaOperacija.Pacijent;
            novaOperacija.TipOperacije = trenutnaOperacija.TipOperacije;
            novaOperacija.Trajanje = trenutnaOperacija.Trajanje;
            novaOperacija.Zavrsen = trenutnaOperacija.Zavrsen;
            novaOperacija.Anamneza = novaAnamneza;
            novaOperacija.Prostorija = trenutnaOperacija.Prostorija;
            novaOperacija.Datum = trenutnaOperacija.Datum;
            novaOperacija.Zavrsen = true;
            return novaOperacija;
        }

        public Pregled PopuniPregled(Anamneza novaAnamneza)
        {
            Pregled noviPregled = new Pregled();
            noviPregled.Id = trenutniPregled.Id;
            noviPregled.Hitan = trenutniPregled.Hitan;
            noviPregled.Lekar = trenutniPregled.Lekar;
            noviPregled.Pacijent = trenutniPregled.Pacijent;
            noviPregled.Trajanje = trenutniPregled.Trajanje;
            noviPregled.Zavrsen = trenutniPregled.Zavrsen;
            noviPregled.Anamneza = novaAnamneza;
            noviPregled.Prostorija = trenutniPregled.Prostorija;
            noviPregled.Datum = trenutniPregled.Datum;
            noviPregled.Zavrsen = true;
            return noviPregled;
        }
        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            Potvrdi();
        }

        private void ObrisiReceptPritisnuto(object sender, RoutedEventArgs e)
        {
            ObrisiRecept();
        }

        public void ObrisiRecept()
        {
            if (dataGridLekovi.SelectedCells.Count > 0)
            {
                int index = dataGridLekovi.SelectedIndex;
                Recepti.RemoveAt(index);
            }
        }
        private void ZakaziPregledPritisnuto(object sender, RoutedEventArgs e)
        {
            ZakaziPregled();
        }

        public void ZakaziPregled()
        {
            FormNapraviTerminLekar form = new FormNapraviTerminLekar(ulogovaniLekar, trenutniPacijent);
            form.Show();
        }

        private void OtkaziPritisnuto(object sender, RoutedEventArgs e)
        {
            Otkazi();
        }

        public void Otkazi()
        {
            this.Close();
        }

        public void VidiDetaljeOReceptu()
        {
            if (dataGridLekovi.SelectedCells.Count > 0)
            {
                FormVidiReceptLekar form = new FormVidiReceptLekar(PretvoriPrikazReceptaURecept());
                form.Show();
            }
        }

        public Recept PretvoriPrikazReceptaURecept()
        {
            PrikazRecepta selektovaniPrikazRecepta = new PrikazRecepta();
            selektovaniPrikazRecepta = dataGridLekovi.SelectedItem as PrikazRecepta;
            Recept selektovaniRecept = new Recept();
            selektovaniRecept.DatumIzdavanja = selektovaniPrikazRecepta.DatumIzdavanja;
            selektovaniRecept.Id = selektovaniPrikazRecepta.Id;
            selektovaniRecept.Kolicina = selektovaniPrikazRecepta.Kolicina;
            selektovaniRecept.Lek = selektovaniPrikazRecepta.lek;
            selektovaniRecept.Trajanje = selektovaniPrikazRecepta.Trajanje;
            selektovaniRecept.VremeUzimanja = selektovaniPrikazRecepta.VremeUzimanja;
            return selektovaniRecept;
        }
        private void VidiDetaljePritisnuto(object sender, RoutedEventArgs e)
        {
            VidiDetaljeOReceptu();
        }

        private void AkoJeAkceleratorPritisnut(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.IsKeyDown(Key.LeftCtrl) || e.KeyboardDevice.IsKeyDown(Key.RightCtrl))
            {
                switch (e.Key)
                {
                    case Key.D:
                        DodajLek();
                        break;
                    case Key.I:
                        VidiDetaljeOReceptu();
                        break;
                    case Key.O:
                        ObrisiRecept();
                        break;
                    case Key.P:
                        ZakaziPregled();
                        break;
                    case Key.Q:
                        Potvrdi();
                        break;
                    case Key.W:
                        Otkazi();
                        break;


                }
            }
        }

        private void PredjiNaScrollBar(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Right))
            {
                 IzbrisiButton.MoveFocus(new TraversalRequest(FocusNavigationDirection.Right));
                
            }
        }

        private void zaustaviStrelice(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Up))
                {

                ScroolBar.ScrollToVerticalOffset(20); 
            }
        }
    }
}
