using Model.Korisnici;
using Model.Pregledi;
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
using Model.Pacijenti;
using Model.Prostorije;
using Bolnica.Model.Pregledi;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormPacijent.xaml
    /// </summary>
    public partial class FormPacijent : Window
    {
        private Pacijent trenutniPacijent = new Pacijent();

        public static ObservableCollection<PrikazPregleda> PrikazNezavrsenihPregleda
        {
            get;
            set;
        }

        private FileStoragePregledi storagePregledi = new FileStoragePregledi();
        private FileStorageProstorija storageProstorija = new FileStorageProstorija();
        private FileStoragePacijenti storagePacijenti = new FileStoragePacijenti();
        private FileStorageAnamneza storageAnamneza = new FileStorageAnamneza();
        private List<Lekar> lekari = new List<Lekar>();

        private List<PrikazPregleda> preglediPrikaz = new List<PrikazPregleda>();
        private List<PrikazOperacije> operacijePrikaz = new List<PrikazOperacije>();
        private List<Anamneza> anamneze = new List<Anamneza>();

        public FormPacijent(Pacijent pacijent)
        {
            InitializeComponent();

            this.DataContext = this;

            trenutniPacijent = pacijent;

            List<Pregled> pregledi = storagePregledi.GetAllPregledi();
            List<Operacija> operacije = storagePregledi.GetAllOperacije();

            PrikazNezavrsenihPregleda = new ObservableCollection<PrikazPregleda>();
            FormObavestenjaPacijent.ObavestenjaZaPacijente = new ObservableCollection<string>();

            storagePregledi = new FileStoragePregledi();

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

            anamneze = storageAnamneza.GetAll();
            List<Prostorija> prostorije = storageProstorija.GetAllProstorije();
            List<Pacijent> pacijenti = storagePacijenti.GetAll();

            foreach (Pregled p in pregledi)
            {
                PrikazPregleda prikaz = new PrikazPregleda();
                prikaz.Datum = p.Datum;
                prikaz.Trajanje = p.Trajanje;
                prikaz.Zavrsen = p.Zavrsen;
                prikaz.Id = p.Id;
                prikaz.AnamnezaId = p.AnamnezaId;

                foreach (Prostorija pro in prostorije)
                {
                    if (p.brojProstorije.Equals(pro.BrojProstorije))
                    {
                        prikaz.Prostorija = pro;
                        break;
                    }
                }

                foreach (Pacijent pac in pacijenti)
                {
                    if (p.pacijentJmbg.Equals(pac.Jmbg))
                    {
                        prikaz.Pacijent = pac;
                        break;
                    }
                }
                
                foreach (Lekar l in lekari)
                {
                    if (p.lekarJmbg.Equals(l.Jmbg))
                    {
                        prikaz.Lekar = l;
                        break;
                    }
                }

                preglediPrikaz.Add(prikaz);
            }

            foreach (PrikazPregleda p in preglediPrikaz)
            {
                if (p.Pacijent.Guest == false)
                {
                    if (p.Pacijent.KorisnickoIme.Equals(pacijent.KorisnickoIme))
                    {
                        if (p.Zavrsen == false)
                        {
                            PrikazNezavrsenihPregleda.Add(p);
                        }
                    }
                }
            }

            foreach (Operacija o in operacije)
            {
                PrikazOperacije prikaz = new PrikazOperacije();
                prikaz.Datum = o.Datum;
                prikaz.Trajanje = o.Trajanje;
                prikaz.Zavrsen = o.Zavrsen;
                prikaz.Id = o.Id;
                prikaz.AnamnezaId = o.AnamnezaId;
                prikaz.TipOperacije = o.TipOperacije;

                foreach (Prostorija pro in prostorije)
                {
                    if (o.brojProstorije.Equals(pro.BrojProstorije))
                    {
                        prikaz.Prostorija = pro;
                        break;
                    }
                }

                foreach (Pacijent pac in pacijenti)
                {
                    if (o.pacijentJmbg.Equals(pac.Jmbg))
                    {
                        prikaz.Pacijent = pac;
                        break;
                    }
                }

                foreach (Lekar l in lekari)
                {
                    if (o.lekarJmbg.Equals(l.Jmbg))
                    {
                        prikaz.Lekar = l;
                        break;
                    }
                }

                operacijePrikaz.Add(prikaz);
            }

            foreach (PrikazOperacije o in operacijePrikaz)
            {
                if (o.Pacijent.Guest == false)
                {
                    if (o.Zavrsen == false && o.Pacijent.KorisnickoIme.Equals(pacijent.KorisnickoIme))
                        PrikazNezavrsenihPregleda.Add(o);
                }
            }
        }

        public void PrikaziObavestenja()
        {
            foreach (PrikazPregleda p in preglediPrikaz)
            {
                if (p.Pacijent.Guest == false)
                {
                    if (p.Pacijent.KorisnickoIme.Equals(trenutniPacijent.KorisnickoIme))
                    {
                        if (p.Zavrsen == true)
                        {
                            foreach (Anamneza a in anamneze)
                            {
                                if (p.AnamnezaId.Equals(a.Id))
                                {
                                    foreach (Recept r in a.Recept)
                                    {
                                        if (r.DatumIzdavanja.CompareTo(DateTime.Today) == 0)
                                        {
                                            string poruka = "";
                                            if (r.VremeUzimanja.Hours == 0)
                                            {
                                                poruka = DateTime.Today.ToShortDateString() + ": Postovani, danas Vam je izdata terapija koja traje do " + 
                                                r.Trajanje.ToShortDateString() + ". " + "Prepisan Vam je lek '" + GetNazivLeka(r.Lek_id) + 
                                                "' koji treba da pijete jednom" +
                                                " dnevno u razmaku.";
                                                MessageBox.Show(poruka, "Obaveštenje");
                                                FormObavestenjaPacijent.ObavestenjaZaPacijente.Add(poruka);
                                            }
                                            else
                                            {
                                                poruka = DateTime.Today.ToShortDateString() + ": Postovani, danas Vam je izdata terapija koja traje do " + 
                                                r.Trajanje.ToShortDateString() + ". " + "Prepisan Vam je lek '" + GetNazivLeka(r.Lek_id) + 
                                                "' koji treba da pijete " + 24 / r.VremeUzimanja.Hours +
                                                " puta dnevno u razmaku od po " + r.VremeUzimanja.Hours + " sati.";
                                                MessageBox.Show(poruka, "Obaveštenje");
                                                FormObavestenjaPacijent.ObavestenjaZaPacijente.Add(poruka);
                                            }
                                           
                                        }
                                        if (r.Trajanje.CompareTo(DateTime.Now) > 0)
                                        {
                                            string poruka = "";
                                            if (r.VremeUzimanja.Hours == 0)
                                            {
                                                poruka = DateTime.Today.ToShortDateString() + ": Danas treba da popijete lek '" + GetNazivLeka(r.Lek_id) + 
                                                "'. Ovaj lek se pije " + " jednom dnevno.";
                                                MessageBox.Show(poruka, "Obaveštenje");
                                                FormObavestenjaPacijent.ObavestenjaZaPacijente.Add(poruka);
                                            }
                                            else
                                            {
                                                poruka = DateTime.Today.ToShortDateString() + ": Danas treba da popijete lek '" + GetNazivLeka(r.Lek_id) + 
                                                "'. Ovaj lek se pije " + 24/r.VremeUzimanja.Hours + " puta dnevno u razmaku od po " 
                                                + r.VremeUzimanja.Hours + " sati.";
                                                MessageBox.Show(poruka, "Obaveštenje");
                                                FormObavestenjaPacijent.ObavestenjaZaPacijente.Add(poruka);
                                            }
                                            
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ZakaziPregled(object sender, RoutedEventArgs e)
        {
            var s = new FormNapraviTerminPacijent(trenutniPacijent);
            s.Show();
        }

        private void OtkaziPregled(object sender, RoutedEventArgs e)
        {
            var objekat = pacijentGrid.SelectedValue;
            
            if (objekat != null)
            {
                Operacija operacija = new Operacija();

                PrikazPregleda p = (PrikazPregleda)pacijentGrid.SelectedItem;

                DateTime datum = p.Datum;
                int dan = datum.Day;
                int mesec = datum.Month;
                int godina = datum.Year;

                DateTime danas = DateTime.Today;
                DateTime prekosutra = danas.AddDays(2);
                DateTime datumPregleda = new DateTime(godina, mesec, dan);

                if (prekosutra.CompareTo(datumPregleda) >= 0)
                {
                    MessageBox.Show("Ne mozete da otkazete pregled koji je zakazan za sledeca 2 dana!");
                }
                else
                {
                    if (objekat.GetType().Equals(operacija.GetType()))
                    {
                        for (int i = 0; i < PrikazNezavrsenihPregleda.Count; i++)
                        {
                            if (objekat.Equals(PrikazNezavrsenihPregleda[i]))
                            {
                                PrikazNezavrsenihPregleda.RemoveAt(i);
                                PrikazOperacije prikaz = (PrikazOperacije)objekat;
                                operacija.Id = prikaz.Id;
                                FileStoragePregledi storage = new FileStoragePregledi();
                                storage.Delete(operacija);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < PrikazNezavrsenihPregleda.Count; i++)
                        {
                            if (objekat.Equals(PrikazNezavrsenihPregleda[i]))
                            {
                                PrikazNezavrsenihPregleda.RemoveAt(i);
                                PrikazPregleda prikaz = (PrikazPregleda)objekat;
                                Pregled pregled = new Pregled();
                                pregled.Id = prikaz.Id;
                                FileStoragePregledi storage = new FileStoragePregledi();
                                storage.Delete(pregled);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Morate odabrati pregled ili operaciju koju zelite da otkazete!", "Upozorenje");
            }
        }

        private void IzmeniPregled(object sender, RoutedEventArgs e)
        {
            var objekat = pacijentGrid.SelectedValue;

            if (objekat != null)
            {
                Operacija o = new Operacija();

                if (objekat.GetType().Equals(o.GetType()))
                {
                    MessageBox.Show("Ne mozete da izmenite operaciju!");
                }
                else
                {
                    PrikazPregleda p = (PrikazPregleda)pacijentGrid.SelectedItem;

                    DateTime datum = p.Datum;
                    int dan = datum.Day;
                    int mesec = datum.Month;
                    int godina = datum.Year;

                    DateTime danas = DateTime.Today;
                    DateTime prekosutra = danas.AddDays(2);
                    DateTime datumPregleda = new DateTime(godina, mesec, dan);

                    if (prekosutra.CompareTo(datumPregleda) >= 0)
                    {
                        MessageBox.Show("Ne mozete da menjate pregled koji je zakazan za sledeca 2 dana!");
                    }
                    else
                    {
                        var s = new FormIzmeniTerminPacijent(p);
                        s.Show();
                    }
                }
            }
            else
            {
                MessageBox.Show("Morate odabrati pregled koju zelite da izmenite!", "Upozorenje");
            }
        }

        private void IstorijaPregleda(object sender, RoutedEventArgs e)
        {
            var s = new FormIstorijaPregledaPacijent(trenutniPacijent);
            s.Show();
        }

        private void ObavestenjaPacijent(object sender, RoutedEventArgs e)
        {
            var s = new FormObavestenjaPacijent();
            s.Show();
        }

        private string GetNazivLeka(int id)
        {
            List<Lek> lekovi = new List<Lek>();
            Lek l1 = new Lek();
            l1.Id = 1;
            l1.Naziv = "Aspirin";
            Lek l2 = new Lek();
            l2.Id = 2;
            l2.Naziv = "Brufen";
            Lek l3 = new Lek();
            l3.Id = 3;
            l3.Naziv = "Paracetamol";
            Lek l4 = new Lek();
            l4.Id = 4;
            l4.Naziv = "Andol";
            lekovi.Add(l1);
            lekovi.Add(l2);
            lekovi.Add(l3);
            lekovi.Add(l4);
            foreach (Lek l in lekovi)
            {
                if (l.Id.Equals(id))
                {
                    return l.Naziv;
                }
            }
            return "";
        }
    }
}
