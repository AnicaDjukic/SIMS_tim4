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
using System.Windows;
using System.Windows.Controls;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormPacijentPage.xaml
    /// </summary>
    public partial class FormPacijentPage : Page
    {
        private FormPacijentWeb form;
        private Pacijent trenutniPacijent = new Pacijent();
        public static ObservableCollection<PrikazPregleda> PrikazNezavrsenihPregleda
        {
            get;
            set;
        }

        private FileStoragePregledi storagePregledi = new FileStoragePregledi();
        private FileStoragePacijenti storagePacijenti = new FileStoragePacijenti();
        private FileStorageLekar storageLekari = new FileStorageLekar();
        private FileStorageProstorija storageProstorija = new FileStorageProstorija();
        private FileStorageAnamneza storageAnamneza = new FileStorageAnamneza();
        private FileStorageAntiTrol storageAntiTrol = new FileStorageAntiTrol();

        private List<PrikazPregleda> preglediPrikaz = new List<PrikazPregleda>();
        private List<PrikazOperacije> operacijePrikaz = new List<PrikazOperacije>();
        private List<Pregled> pregledi = new List<Pregled>();
        private List<Operacija> operacije = new List<Operacija>();
        private List<Pacijent> pacijenti = new List<Pacijent>();
        private List<Lekar> lekari = new List<Lekar>();
        private List<Prostorija> prostorije = new List<Prostorija>();
        private List<Anamneza> anamneze = new List<Anamneza>();

        public FormPacijentPage(Pacijent pacijent, FormPacijentWeb formPacijentWeb)
        {
            InitializeComponent();

            this.DataContext = this;

            form = formPacijentWeb;
            trenutniPacijent = pacijent;
            PrikazNezavrsenihPregleda = new ObservableCollection<PrikazPregleda>();
            FormObavestenjaPacijentPage.ObavestenjaZaPacijente = new ObservableCollection<string>();

            pregledi = storagePregledi.GetAllPregledi();
            operacije = storagePregledi.GetAllOperacije();
            pacijenti = storagePacijenti.GetAll();
            lekari = storageLekari.GetAll();
            prostorije = storageProstorija.GetAllProstorije();
            anamneze = storageAnamneza.GetAll();

            foreach (Pregled pregled in pregledi)
            {
                PrikazPregleda prikaz = new PrikazPregleda
                {
                    Datum = pregled.Datum,
                    Trajanje = pregled.Trajanje,
                    Zavrsen = pregled.Zavrsen,
                    Id = pregled.Id,
                    Anamneza = pregled.Anamneza
                };

                foreach (Prostorija pro in prostorije)
                {
                    if (pregled.Prostorija.BrojProstorije.Equals(pro.BrojProstorije))
                    {
                        prikaz.Prostorija = pro;
                        break;
                    }
                }

                foreach (Pacijent pac in pacijenti)
                {
                    if (pregled.Pacijent.Jmbg.Equals(pac.Jmbg))
                    {
                        prikaz.Pacijent = pac;
                        break;
                    }
                }

                foreach (Lekar l in lekari)
                {
                    if (pregled.Lekar.Jmbg.Equals(l.Jmbg))
                    {
                        prikaz.Lekar = l;
                        break;
                    }
                }

                preglediPrikaz.Add(prikaz);
            }

            foreach (PrikazPregleda prikaz in preglediPrikaz)
            {
                if (pacijent.Jmbg.Equals(prikaz.Pacijent.Jmbg) && !prikaz.Pacijent.Guest && !prikaz.Zavrsen)
                {
                    PrikazNezavrsenihPregleda.Add(prikaz);
                }
            }

            foreach (Operacija operacija in operacije)
            {
                PrikazOperacije prikaz = new PrikazOperacije
                {
                    Datum = operacija.Datum,
                    Trajanje = operacija.Trajanje,
                    Zavrsen = operacija.Zavrsen,
                    Id = operacija.Id,
                    Anamneza = operacija.Anamneza,
                    TipOperacije = operacija.TipOperacije
                };

                foreach (Prostorija pro in prostorije)
                {
                    if (operacija.Prostorija.BrojProstorije.Equals(pro.BrojProstorije))
                    {
                        prikaz.Prostorija = pro;
                        break;
                    }
                }

                foreach (Pacijent pac in pacijenti)
                {
                    if (operacija.Pacijent.Jmbg.Equals(pac.Jmbg))
                    {
                        prikaz.Pacijent = pac;
                        break;
                    }
                }

                foreach (Lekar l in lekari)
                {
                    if (operacija.Lekar.Jmbg.Equals(l.Jmbg))
                    {
                        prikaz.Lekar = l;
                        break;
                    }
                }

                operacijePrikaz.Add(prikaz);
            }

            foreach (PrikazOperacije prikaz in operacijePrikaz)
            {
                if (pacijent.Jmbg.Equals(prikaz.Pacijent.Jmbg) && !prikaz.Pacijent.Guest && !prikaz.Zavrsen)
                {
                    PrikazNezavrsenihPregleda.Add(prikaz);
                }
            }
        }

        public void PrikaziObavestenja()
        {
            foreach (PrikazPregleda prikaz in preglediPrikaz)
            {
                if (trenutniPacijent.Jmbg.Equals(prikaz.Pacijent.Jmbg) && !prikaz.Pacijent.Guest && prikaz.Zavrsen)
                {
                    foreach (Anamneza a in anamneze)
                    {
                        if (prikaz.Anamneza.Id.Equals(a.Id))
                        {
                            foreach (Recept r in a.Recept)
                            {
                                if (r.DatumIzdavanja.CompareTo(DateTime.Today) == 0)
                                {
                                    string poruka;
                                    if (r.VremeUzimanja.Hours == 0)
                                    {
                                        poruka = DateTime.Today.ToShortDateString() + ": Postovani, danas Vam je izdata terapija koja traje do " +
                                        r.Trajanje.ToShortDateString() + ". " + "Prepisan Vam je lek '" + GetNazivLeka(r.Lek.Id) +
                                        "' koji treba da pijete jednom dnevno u razmaku od po " + r.VremeUzimanja.Hours + " sati.";
                                        MessageBox.Show(poruka, "Obaveštenje");
                                        FormObavestenjaPacijentPage.ObavestenjaZaPacijente.Add(poruka);
                                    }
                                    else
                                    {
                                        poruka = DateTime.Today.ToShortDateString() + ": Postovani, danas Vam je izdata terapija koja traje do " +
                                        r.Trajanje.ToShortDateString() + ". " + "Prepisan Vam je lek '" + GetNazivLeka(r.Lek.Id) +
                                        "' koji treba da pijete " + 24 / r.VremeUzimanja.Hours +
                                        " puta dnevno u razmaku od po " + r.VremeUzimanja.Hours + " sati.";
                                        MessageBox.Show(poruka, "Obaveštenje");
                                        FormObavestenjaPacijentPage.ObavestenjaZaPacijente.Add(poruka);
                                    }

                                }
                                if (r.Trajanje.CompareTo(DateTime.Now) > 0)
                                {
                                    string poruka;
                                    if (r.VremeUzimanja.Hours == 0)
                                    {
                                        poruka = DateTime.Today.ToShortDateString() + ": Danas treba da popijete lek '" + GetNazivLeka(r.Lek.Id) +
                                        "'. Ovaj lek se pije " + " jednom dnevno od po " + r.VremeUzimanja.Hours + " sati.";
                                        MessageBox.Show(poruka, "Obaveštenje");
                                        FormObavestenjaPacijentPage.ObavestenjaZaPacijente.Add(poruka);
                                    }
                                    else
                                    {
                                        poruka = DateTime.Today.ToShortDateString() + ": Danas treba da popijete lek '" + GetNazivLeka(r.Lek.Id) +
                                        "'. Ovaj lek se pije " + 24 / r.VremeUzimanja.Hours + " puta dnevno u razmaku od po "
                                        + r.VremeUzimanja.Hours + " sati.";
                                        MessageBox.Show(poruka, "Obaveštenje");
                                        FormObavestenjaPacijentPage.ObavestenjaZaPacijente.Add(poruka);
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
            int brojac = form.DobijBrojAktivnosti();
            
            if (brojac > 5)
            {
                form.BlokirajPacijenta();
                form.Close();
            }
            else
            {
                if (brojac > 4)
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

                PrikazPregleda prikaz = (PrikazPregleda)pacijentGrid.SelectedItem;

                DateTime datum = prikaz.Datum;
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
                                PrikazOperacije prikazOperacije = (PrikazOperacije)objekat;
                                operacija.Id = prikazOperacije.Id;
                                storagePregledi.Delete(operacija);

                                AntiTrol antiTrol = new AntiTrol
                                {
                                    Pacijent = prikazOperacije.Pacijent,
                                    Datum = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
                                };
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
                                PrikazPregleda prikazPregleda = (PrikazPregleda)objekat;
                                Pregled pregled = new Pregled
                                {
                                    Id = prikazPregleda.Id
                                };
                                storagePregledi.Delete(pregled);

                                AntiTrol antiTrol = new AntiTrol
                                {
                                    Pacijent = prikazPregleda.Pacijent,
                                    Datum = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
                                };
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
                if (objekat.GetType().Equals(new Operacija().GetType()))
                {
                    MessageBox.Show("Ne mozete da izmenite operaciju!");
                }
                else
                {
                    PrikazPregleda prikaz = (PrikazPregleda)pacijentGrid.SelectedItem;

                    DateTime datum = prikaz.Datum;
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
                        int brojac = form.DobijBrojAktivnosti();

                        if (brojac > 5)
                        {
                            form.BlokirajPacijenta();
                            form.Close();
                        }
                        else
                        {
                            if (brojac > 4)
                            {
                                MessageBox.Show("Posljednje upozorenje pred gasenje Vaseg naloga. Ukoliko nastavite da zloupotrebljavate " +
                                    "nasu aplikaciju pristup samoj aplikaciji ce Vam biti onemogucen!", "Upozorenje");
                            }
                            form.Pocetna.Content = new FormIzmeniPacijentPage(prikaz, form);
                        }
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
            form.Pocetna.Content = new FormObavestenjaPacijentPage();
        }

        private string GetNazivLeka(int id)
        {
            FileStorageLek storageLekovi = new FileStorageLek();
            List<Lek> lekovi = storageLekovi.GetAll();
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
