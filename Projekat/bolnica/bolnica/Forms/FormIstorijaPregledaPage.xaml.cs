using Bolnica.Model.Pregledi;
using Model.Korisnici;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormIstorijaPregledaPage.xaml
    /// </summary>
    public partial class FormIstorijaPregledaPage : Page
    {
        private Pacijent trenutniPacijent = new Pacijent();
        private FileStoragePregledi storage;
        public static ObservableCollection<PrikazPregleda> PrikazZavrsenihPregleda
        {
            get;
            set;
        }
        private List<Lekar> lekari = new List<Lekar>();
        private List<Prostorija> prostorije = new List<Prostorija>();

        private FormPacijentWeb form;

        public FormIstorijaPregledaPage(Pacijent pacijent, FormPacijentWeb formPacijentWeb)
        {
            InitializeComponent();

            form = formPacijentWeb;
            trenutniPacijent = pacijent;

            this.DataContext = this;

            PrikazZavrsenihPregleda = new ObservableCollection<PrikazPregleda>();

            Lekar l1 = new Lekar();
            Lekar l2 = new Lekar();
            Lekar l3 = new Lekar();
            Lekar l4 = new Lekar();

            l1.AdresaStanovanja = "AAA";
            l1.BrojSlobodnihDana = 15;
            l1.BrojTelefona = "111111";
            l1.DatumRodjenja = new DateTime();
            l1.Email = "dada@dada.com";
            l1.GodineStaza = 11;
            l1.Ime = "Mico";
            l1.Prezime = "Govedarica";
            l1.Jmbg = "342425";
            l1.KorisnickoIme = "Pero";
            l1.Lozinka = "Admin";
            l1.Mbr = 21312;
            l1.Plata = 1000;
            Specijalizacija sp = new Specijalizacija();
            sp.Id = 121;
            sp.Naziv = "neka";
            sp.OblastMedicine = "nekaa";
            l1.Specijalizacija = sp;
            l1.TipKorisnika = TipKorisnika.lekar;
            l1.Zaposlen = true;

            l2.AdresaStanovanja = "BBB";
            l2.BrojSlobodnihDana = 15;
            l2.BrojTelefona = "22222";
            l2.DatumRodjenja = new DateTime();
            l2.Email = "bada@dada.com";
            l2.GodineStaza = 7;
            l2.Ime = "Radendko";
            l2.Prezime = "Salapura";
            l2.Jmbg = "222222";
            l2.KorisnickoIme = "Peki";
            l2.Lozinka = "Baja";
            l2.Mbr = 3232;
            l2.Plata = 10000;
            Specijalizacija spa = new Specijalizacija();
            spa.Id = 1211;
            spa.Naziv = "neeka";
            spa.OblastMedicine = "nekaaa";
            l2.Specijalizacija = spa;
            l2.TipKorisnika = TipKorisnika.lekar;
            l2.Zaposlen = true;

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

            lekari.Add(l1);
            lekari.Add(l2);
            lekari.Add(l3);
            lekari.Add(l4);

            FileStorageProstorija storageProstorije = new FileStorageProstorija();
            prostorije = storageProstorije.GetAllProstorije();

            storage = new FileStoragePregledi();

            List<Pregled> pregledi = storage.GetAllPregledi();
            foreach (Pregled p in pregledi)
            {
                if (p.pacijentJmbg.Equals(pacijent.Jmbg))
                {
                    if (!pacijent.Obrisan && !pacijent.Guest && p.Zavrsen)
                    {
                        PrikazPregleda prikaz = new PrikazPregleda();
                        prikaz.Datum = p.Datum;
                        prikaz.Trajanje = p.Trajanje;
                        prikaz.Zavrsen = p.Zavrsen;
                        prikaz.Hitan = p.Hitan;
                        prikaz.Id = p.Id;
                        prikaz.AnamnezaId = p.AnamnezaId;

                        prikaz.Pacijent = pacijent;

                        foreach (Lekar l in lekari)
                        {
                            if (p.lekarJmbg.Equals(l.Jmbg))
                            {
                                prikaz.Lekar = l;
                            }
                        }
                        
                        foreach (Prostorija pro in prostorije)
                        {
                            if (p.brojProstorije.Equals(pro.BrojProstorije))
                            {
                                prikaz.Prostorija = pro;
                            }
                        }

                        PrikazZavrsenihPregleda.Add(prikaz);
                    }
                }
            }
            List<Operacija> operacije = storage.GetAllOperacije();
            foreach (Operacija o in operacije)
            {
                if (o.pacijentJmbg.Equals(pacijent.Jmbg))
                {
                    if (!pacijent.Obrisan && !pacijent.Guest && o.Zavrsen)
                    {
                        PrikazOperacije prikaz = new PrikazOperacije();
                        prikaz.Datum = o.Datum;
                        prikaz.Trajanje = o.Trajanje;
                        prikaz.Zavrsen = o.Zavrsen;
                        prikaz.Hitan = o.Hitan;
                        prikaz.Id = o.Id;
                        prikaz.AnamnezaId = o.AnamnezaId;
                        prikaz.TipOperacije = o.TipOperacije;

                        prikaz.Pacijent = pacijent;

                        foreach (Lekar l in lekari)
                        {
                            if (o.lekarJmbg.Equals(l.Jmbg))
                            {
                                prikaz.Lekar = l;
                            }
                        }

                        foreach (Prostorija pro in prostorije)
                        {
                            if (o.brojProstorije.Equals(pro.BrojProstorije))
                            {
                                prikaz.Prostorija = pro;
                            }
                        }

                        PrikazZavrsenihPregleda.Add(prikaz);
                    }
                }
            }
        }

        private void Button_Click_Oceni_Lekara(object sender, RoutedEventArgs e)
        {
            var objekat = pacijentIstorijaGrid.SelectedValue;

            if (objekat != null)
            {
                PrikazPregleda p = (PrikazPregleda)pacijentIstorijaGrid.SelectedItem;
                form.Pocetna.Content = new FormOceniLekaraPage(p, form);
            }
            else
            {
                MessageBox.Show("Morate odabrati pregled kod odredjenog lekara koga zelite da ocenite!", "Upozorenje");
            }
        }

        private void Button_Click_Oceni_Bolnicu(object sender, RoutedEventArgs e)
        {
            form.Pocetna.Content = new FormOceniBolnicuPage(trenutniPacijent, form);
        }

        private void Button_Click_Istorija_Ocena_I_Komentara(object sender, RoutedEventArgs e)
        {
            form.Pocetna.Content = new FormIstorijaOcenaPage(trenutniPacijent, form);
        }
    }
}
