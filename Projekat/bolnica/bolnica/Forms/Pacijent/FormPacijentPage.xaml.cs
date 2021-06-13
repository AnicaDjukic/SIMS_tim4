using Bolnica.ViewModel;
using System.Windows.Controls;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormPacijentPage.xaml
    /// </summary>
    public partial class FormPacijentPage : Page
    {
        /*private Pacijent trenutniPacijent = new Pacijent();
        public static ObservableCollection<PrikazPregleda> PrikazNezavrsenihPregleda
        {
            get;
            set;
        }

        private FileRepositoryPregled storagePregledi = new FileRepositoryPregled();
        private FileRepositoryPacijent storagePacijenti = new FileRepositoryPacijent();
        private FileRepositoryLekar storageLekari = new FileRepositoryLekar();
        private FileRepositoryProstorija storageProstorija = new FileRepositoryProstorija();
        private FileRepositoryAnamneza storageAnamneza = new FileRepositoryAnamneza();
        private FileRepositoryAntiTrol storageAntiTrol = new FileRepositoryAntiTrol();

        private List<PrikazPregleda> preglediPrikaz = new List<PrikazPregleda>();
        private List<PrikazOperacije> operacijePrikaz = new List<PrikazOperacije>();
        private List<Pregled> pregledi = new List<Pregled>();
        private List<Operacija> operacije = new List<Operacija>();
        private List<Pacijent> pacijenti = new List<Pacijent>();
        private List<Lekar> lekari = new List<Lekar>();
        private List<Prostorija> prostorije = new List<Prostorija>();
        private List<Anamneza> anamneze = new List<Anamneza>();*/

        public FormPacijentPage(PacijentPageViewModel pacijentPageViewModel/*Pacijent pacijent*/)
        {
            InitializeComponent();

            this.DataContext = pacijentPageViewModel;
            FormPacijentWeb.Forma.Pocetna.Content = this;
            /*this.DataContext = this;
            trenutniPacijent = pacijent;
            PrikazNezavrsenihPregleda = new ObservableCollection<PrikazPregleda>();
            FormObavestenjaPacijentPage.ObavestenjaZaPacijente = new ObservableCollection<string>();

            UcitajPodatke();
            UcitajPreglede(pacijent);
            UcitajOperacije(pacijent);*/
        }

        /*private void UcitajPreglede(Pacijent pacijent)
        {
            foreach (Pregled pregled in pregledi)
            {
                PrikazPregleda prikaz = DobijPrikazPregleda(pregled);
                PostaviProstorijuZaPregled(pregled, prikaz);
                PostaviPacijentaZaPregled(pregled, prikaz);
                PostaviLekaraZaPregled(pregled, prikaz);
                DodajUListuPregleda(prikaz);
            }

            DodajPregledUTabelu(pacijent);
        }

        private void UcitajOperacije(Pacijent pacijent)
        {
            foreach (Operacija operacija in operacije)
            {
                PrikazOperacije prikaz = DobijPrikazOperacije(operacija);
                PostaviProstorijuZaOperaciju(operacija, prikaz);
                PostaviPacijentaZaOperaciju(operacija, prikaz);
                PostaviLekaraZaOperaciju(operacija, prikaz);
                DodajUListuOperacija(prikaz);
            }

            DodajOperacijeUTabelu(pacijent);
        }
        
        private void UcitajPodatke()
        {
            pregledi = storagePregledi.GetAllPregledi();
            operacije = storagePregledi.GetAllOperacije();
            pacijenti = storagePacijenti.GetAll();
            lekari = storageLekari.GetAll();
            prostorije = storageProstorija.GetAllProstorije();
            anamneze = storageAnamneza.GetAll();
        }

        private static PrikazPregleda DobijPrikazPregleda(Pregled pregled)
        {
            return new PrikazPregleda
            {
                Datum = pregled.Datum,
                Trajanje = pregled.Trajanje,
                Zavrsen = pregled.Zavrsen,
                Id = pregled.Id,
                Anamneza = pregled.Anamneza
            };
        }

        private void PostaviProstorijuZaPregled(Pregled pregled, PrikazPregleda prikaz)
        {
            foreach (Prostorija pro in prostorije)
            {
                if (pregled.Prostorija.BrojProstorije.Equals(pro.BrojProstorije))
                {
                    prikaz.Prostorija = pro;
                    break;
                }
            }
        }

        private void PostaviPacijentaZaPregled(Pregled pregled, PrikazPregleda prikaz)
        {
            foreach (Pacijent pac in pacijenti)
            {
                if (pregled.Pacijent.Jmbg.Equals(pac.Jmbg))
                {
                    prikaz.Pacijent = pac;
                    break;
                }
            }
        }

        private void PostaviLekaraZaPregled(Pregled pregled, PrikazPregleda prikaz)
        {
            foreach (Lekar l in lekari)
            {
                if (pregled.Lekar.Jmbg.Equals(l.Jmbg))
                {
                    prikaz.Lekar = l;
                    break;
                }
            }
        }

        private void DodajUListuPregleda(PrikazPregleda prikaz)
        {
            preglediPrikaz.Add(prikaz);
        }

        private void DodajPregledUTabelu(Pacijent pacijent)
        {
            foreach (PrikazPregleda prikaz in preglediPrikaz)
            {
                if (pacijent.Jmbg.Equals(prikaz.Pacijent.Jmbg) && !prikaz.Pacijent.Guest && !prikaz.Zavrsen)
                {
                    PrikazNezavrsenihPregleda.Add(prikaz);
                }
            }
        }

        private static PrikazOperacije DobijPrikazOperacije(Operacija operacija)
        {
            return new PrikazOperacije
            {
                Datum = operacija.Datum,
                Trajanje = operacija.Trajanje,
                Zavrsen = operacija.Zavrsen,
                Id = operacija.Id,
                Anamneza = operacija.Anamneza,
                TipOperacije = operacija.TipOperacije
            };
        }

        private void PostaviProstorijuZaOperaciju(Operacija operacija, PrikazOperacije prikaz)
        {
            foreach (Prostorija pro in prostorije)
            {
                if (operacija.Prostorija.BrojProstorije.Equals(pro.BrojProstorije))
                {
                    prikaz.Prostorija = pro;
                    break;
                }
            }
        }

        private void PostaviPacijentaZaOperaciju(Operacija operacija, PrikazOperacije prikaz)
        {
            foreach (Pacijent pac in pacijenti)
            {
                if (operacija.Pacijent.Jmbg.Equals(pac.Jmbg))
                {
                    prikaz.Pacijent = pac;
                    break;
                }
            }
        }

        private void PostaviLekaraZaOperaciju(Operacija operacija, PrikazOperacije prikaz)
        {
            foreach (Lekar l in lekari)
            {
                if (operacija.Lekar.Jmbg.Equals(l.Jmbg))
                {
                    prikaz.Lekar = l;
                    break;
                }
            }
        }

        private void DodajUListuOperacija(PrikazOperacije prikaz)
        {
            operacijePrikaz.Add(prikaz);
        }

        private void DodajOperacijeUTabelu(Pacijent pacijent)
        {
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
                                ObavestiKorisnikaOTekIzdatomReceptu(r);
                                ObavestiKorisnikaOKoriscenjuLeka(r);
                            }
                        }
                    }
                }
            }
        }

        private void ObavestiKorisnikaOTekIzdatomReceptu(Recept r)
        {
            if (r.DatumIzdavanja.CompareTo(DateTime.Today) == 0)
            {
                string poruka;
                if (r.VremeUzimanja == 0)
                {
                    poruka = DateTime.Today.ToShortDateString() + ": Postovani, danas Vam je izdata terapija koja traje do " +
                    r.Trajanje.ToShortDateString() + ". " + "Prepisan Vam je lek '" + GetNazivLeka(r.Lek.Id) +
                    "' koji treba da pijete jednom dnevno.";
                    MessageBox.Show(poruka, "Obaveštenje");
                }
                else
                {
                    poruka = DateTime.Today.ToShortDateString() + ": Postovani, danas Vam je izdata terapija koja traje do " +
                    r.Trajanje.ToShortDateString() + ". " + "Prepisan Vam je lek '" + GetNazivLeka(r.Lek.Id) +
                    "' koji treba da pijete " + 24 / r.VremeUzimanja +
                    " puta dnevno u razmaku od po " + r.VremeUzimanja + " sati.";
                    MessageBox.Show(poruka, "Obaveštenje");
                }

            }
        }

        private void ObavestiKorisnikaOKoriscenjuLeka(Recept r)
        {
            if (r.Trajanje.CompareTo(DateTime.Today) >= 0)
            {
                string poruka;
                if (r.VremeUzimanja == 0)
                {
                    poruka = DateTime.Today.ToShortDateString() + ": Danas treba da popijete lek '" + GetNazivLeka(r.Lek.Id) +
                    "'. Ovaj lek se pije " + " jednom dnevno.";
                    MessageBox.Show(poruka, "Obaveštenje");
                }
                else
                {
                    poruka = DateTime.Today.ToShortDateString() + ": Danas treba da popijete lek '" + GetNazivLeka(r.Lek.Id) +
                    "'. Ovaj lek se pije " + 24 / r.VremeUzimanja + " puta dnevno u razmaku od po "
                    + r.VremeUzimanja + " sati.";
                    MessageBox.Show(poruka, "Obaveštenje");
                }
            }
        }

        private void ZakaziPregled(object sender, RoutedEventArgs e)
        {
            int brojac = FormPacijentWeb.Forma.DobijBrojAktivnosti();
            
            if (brojac > 5)
            {
                FormPacijentWeb.Forma.BlokirajPacijenta();
                FormPacijentWeb.Forma.Close();
            }
            else
            {
                PosaljiPoslednjeUpozorenje(brojac);
                FormPacijentWeb.Forma.Pocetna.Content = new FormZakaziPacijentPage(trenutniPacijent);
            }
        }

        private static void PosaljiPoslednjeUpozorenje(int brojac)
        {
            if (brojac > 4)
            {
                MessageBox.Show("Posljednje upozorenje pred gasenje Vaseg naloga. Ukoliko nastavite da zloupotrebljavate " +
                    "nasu aplikaciju pristup samoj aplikaciji ce Vam biti onemogucen!", "Upozorenje");
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
                                ObrisiOperacijuIzTabele(i);
                                PrikazOperacije prikazOperacije = (PrikazOperacije)objekat;
                                ObrisiOperaciju(prikazOperacije);
                                SacuvajAntiTrol(prikazOperacije);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < PrikazNezavrsenihPregleda.Count; i++)
                        {
                            if (objekat.Equals(PrikazNezavrsenihPregleda[i]))
                            {
                                ObrisiPregledIzTabele(i);
                                PrikazPregleda prikazPregleda = (PrikazPregleda)objekat;
                                ObrisiPregled(prikazPregleda);
                                SacuvajAntiTrol(prikazPregleda);
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

        private static void ObrisiOperacijuIzTabele(int i)
        {
            PrikazNezavrsenihPregleda.RemoveAt(i);
        }

        private void ObrisiOperaciju(PrikazOperacije prikazOperacije)
        {
            Operacija operacija = new Operacija
            {
                Id = prikazOperacije.Id
            };
            storagePregledi.Delete(operacija);
        }

        private static void ObrisiPregledIzTabele(int i)
        {
            PrikazNezavrsenihPregleda.RemoveAt(i);
        }

        private void ObrisiPregled(PrikazPregleda prikazPregleda)
        {
            Pregled pregled = new Pregled
            {
                Id = prikazPregleda.Id
            };
            storagePregledi.Delete(pregled);
        }

        private void SacuvajAntiTrol(PrikazOperacije prikazOperacije)
        {
            AntiTrol antiTrol = new AntiTrol
            {
                Pacijent = prikazOperacije.Pacijent,
                Datum = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
            };
            storageAntiTrol.Save(antiTrol);
        }

        private void SacuvajAntiTrol(PrikazPregleda prikazPregleda)
        {
            AntiTrol antiTrol = new AntiTrol
            {
                Pacijent = prikazPregleda.Pacijent,
                Datum = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
            };
            storageAntiTrol.Save(antiTrol);
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
                        int brojac = FormPacijentWeb.Forma.DobijBrojAktivnosti();

                        if (brojac > 5)
                        {
                            FormPacijentWeb.Forma.BlokirajPacijenta();
                            FormPacijentWeb.Forma.Close();
                        }
                        else
                        {
                            if (brojac > 4)
                            {
                                MessageBox.Show("Posljednje upozorenje pred gasenje Vaseg naloga. Ukoliko nastavite da zloupotrebljavate " +
                                    "nasu aplikaciju pristup samoj aplikaciji ce Vam biti onemogucen!", "Upozorenje");
                            }
                            FormPacijentWeb.Forma.Pocetna.Content = new FormIzmeniPacijentPage(prikaz);
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
            FormPacijentWeb.Forma.Pocetna.Content = new FormIstorijaPregledaPage(trenutniPacijent);
        }

        private void ObavestenjaPacijent(object sender, RoutedEventArgs e)
        {
            FormPacijentWeb.Forma.Pocetna.Content = new FormObavestenjaPacijentPage(trenutniPacijent);
        }

        private string GetNazivLeka(int id)
        {
            FileRepositoryLek storageLekovi = new FileRepositoryLek();
            List<Lek> lekovi = storageLekovi.GetAll();
            foreach (Lek l in lekovi)
            {
                if (l.Id.Equals(id))
                {
                    return l.Naziv;
                }
            }
            return "";
        }*/
    }
}
