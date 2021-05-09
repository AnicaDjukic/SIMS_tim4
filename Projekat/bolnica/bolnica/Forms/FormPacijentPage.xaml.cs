using bolnica;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormPacijentPage.xaml
    /// </summary>
    public partial class FormPacijentPage : Page
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
        private FileStorageAntiTrol storageAntiTrol = new FileStorageAntiTrol();
        private FileStorageLekar storageLekari = new FileStorageLekar();
        private List<Lekar> lekari = new List<Lekar>();

        private List<PrikazPregleda> preglediPrikaz = new List<PrikazPregleda>();
        private List<PrikazOperacije> operacijePrikaz = new List<PrikazOperacije>();
        private List<Anamneza> anamneze = new List<Anamneza>();

        private FormPacijentWeb form;

        public FormPacijentPage(Pacijent pacijent, FormPacijentWeb formPacijentWeb)
        {
            InitializeComponent();

            this.DataContext = this;

            form = formPacijentWeb;

            trenutniPacijent = pacijent;

            List<Pregled> pregledi = storagePregledi.GetAllPregledi();
            List<Operacija> operacije = storagePregledi.GetAllOperacije();

            PrikazNezavrsenihPregleda = new ObservableCollection<PrikazPregleda>();
            FormObavestenjaPacijent.ObavestenjaZaPacijente = new ObservableCollection<string>();

            storagePregledi = new FileStoragePregledi();

            lekari = storageLekari.GetAll();

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
                prikaz.Anamneza = p.Anamneza;

                foreach (Prostorija pro in prostorije)
                {
                    if (p.Prostorija.BrojProstorije.Equals(pro.BrojProstorije))
                    {
                        prikaz.Prostorija = pro;
                        break;
                    }
                }

                foreach (Pacijent pac in pacijenti)
                {
                    if (p.Pacijent.Jmbg.Equals(pac.Jmbg))
                    {
                        prikaz.Pacijent = pac;
                        break;
                    }
                }

                foreach (Lekar l in lekari)
                {
                    if (p.Lekar.Jmbg.Equals(l.Jmbg))
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
                prikaz.Anamneza = o.Anamneza;
                prikaz.TipOperacije = o.TipOperacije;

                foreach (Prostorija pro in prostorije)
                {
                    if (o.Prostorija.BrojProstorije.Equals(pro.BrojProstorije))
                    {
                        prikaz.Prostorija = pro;
                        break;
                    }
                }

                foreach (Pacijent pac in pacijenti)
                {
                    if (o.Pacijent.Jmbg.Equals(pac.Jmbg))
                    {
                        prikaz.Pacijent = pac;
                        break;
                    }
                }

                foreach (Lekar l in lekari)
                {
                    if (o.Lekar.Jmbg.Equals(l.Jmbg))
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
                                if (p.Anamneza.Id.Equals(a.Id))
                                {
                                    foreach (Recept r in a.Recept)
                                    {
                                        if (r.DatumIzdavanja.CompareTo(DateTime.Today) == 0)
                                        {
                                            string poruka = "";
                                            if (r.VremeUzimanja.Hours == 0)
                                            {
                                                poruka = DateTime.Today.ToShortDateString() + ": Postovani, danas Vam je izdata terapija koja traje do " +
                                                r.Trajanje.ToShortDateString() + ". " + "Prepisan Vam je lek '" + GetNazivLeka(r.Lek.Id) +
                                                "' koji treba da pijete jednom" +
                                                " dnevno u razmaku.";
                                                MessageBox.Show(poruka, "Obaveštenje");
                                                FormObavestenjaPacijent.ObavestenjaZaPacijente.Add(poruka);
                                            }
                                            else
                                            {
                                                poruka = DateTime.Today.ToShortDateString() + ": Postovani, danas Vam je izdata terapija koja traje do " +
                                                r.Trajanje.ToShortDateString() + ". " + "Prepisan Vam je lek '" + GetNazivLeka(r.Lek.Id) +
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
                                                poruka = DateTime.Today.ToShortDateString() + ": Danas treba da popijete lek '" + GetNazivLeka(r.Lek.Id) +
                                                "'. Ovaj lek se pije " + " jednom dnevno.";
                                                MessageBox.Show(poruka, "Obaveštenje");
                                                FormObavestenjaPacijent.ObavestenjaZaPacijente.Add(poruka);
                                            }
                                            else
                                            {
                                                poruka = DateTime.Today.ToShortDateString() + ": Danas treba da popijete lek '" + GetNazivLeka(r.Lek.Id) +
                                                "'. Ovaj lek se pije " + 24 / r.VremeUzimanja.Hours + " puta dnevno u razmaku od po "
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
            storageAntiTrol = new FileStorageAntiTrol();
            List<AntiTrol> antiTrol = storageAntiTrol.GetAll();
            int brojac = 0;
            foreach (AntiTrol a in antiTrol)
            {
                if (a.PacijentJMBG.Equals(trenutniPacijent.Jmbg))
                {
                    if (a.Datum.AddDays(3).CompareTo(DateTime.Now) > 0)
                    {
                        brojac++;
                    }
                }
            }
            if (brojac > 5)
            {
                trenutniPacijent.Obrisan = true;
                storagePacijenti.Update(trenutniPacijent);
                MessageBox.Show("Zbog zloupotrebe nase aplikacije prinudjeni smo da Vam onemogucimo pristup istoj. " +
                    "Vas nalog ce biti obrisan i vise necete moci da se ulogujete na Vas profil!", "Iskljucenje");
                var s = new MainWindow();
                s.Show();
                form.Close();
            }
            else
            {
                if (brojac > 3)
                {
                    MessageBox.Show("Posljednje upozorenje pred gasenje Vaseg naloga. Ukoliko nastavite da zloupotrebljavate " +
                        "nasu aplikaciju pristup samoj aplikaciji ce Vam biti onemogucen!", "Upozorenje");
                }
                form.Pocetna.Content = new FormZakaziPacijentPage(trenutniPacijent, form);
            }
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

                                FileStorageAntiTrol storageAntiTrol = new FileStorageAntiTrol();
                                AntiTrol antiTrol = new AntiTrol();
                                antiTrol.PacijentJMBG = prikaz.Pacijent.Jmbg;
                                antiTrol.Datum = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                                storageAntiTrol.Save(antiTrol);
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

                                FileStorageAntiTrol storageAntiTrol = new FileStorageAntiTrol();
                                AntiTrol antiTrol = new AntiTrol();
                                antiTrol.PacijentJMBG = prikaz.Pacijent.Jmbg;
                                antiTrol.Datum = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                                storageAntiTrol.Save(antiTrol);
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
                        MessageBox.Show("Ne mozete da menjate pregled koji je zakazan za sledeca 2 dana, ili je vec prosao!");
                    }
                    else
                    {
                        form.Pocetna.Content = new FormIzmeniPacijentPage(p, form);
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
            form.Pocetna.Content = new FormIstorijaPregledaPage(trenutniPacijent, form);
        }

        private void ObavestenjaPacijent(object sender, RoutedEventArgs e)
        {
            var s = new FormObavestenjaPacijent();
            s.Show();
        }

        private string GetNazivLeka(int id)
        {
            List<Lek> lekovi = new List<Lek>();
            FileStorageLek storageLekovi = new FileStorageLek();
            lekovi = storageLekovi.GetAll();
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
