using Bolnica.Commands;
using Bolnica.Forms;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.Repository.Korisnici;
using Bolnica.Repository.Pregledi;
using Bolnica.Repository.Prostorije;
using Model.Korisnici;
using Model.Pacijenti;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Bolnica.ViewModel
{
    public class PacijentPageViewModel : ViewModel
    {
        private Pacijent trenutniPacijent = new Pacijent();
        public static ObservableCollection<PrikazPregleda> PrikazNezavrsenihPregleda
        {
            get;
            set;
        }

        private Injector inject;
        public Injector Inject
        {
            get { return inject; }
            set
            {
                inject = value;
            }
        }

        private PrikazPregleda selektovaniRed;
        public PrikazPregleda SelektovaniRed
        {
            get { return selektovaniRed; }
            set
            {
                selektovaniRed = value;
                OnPropertyChanged();
            }
        }

        private FileRepositoryPregled storagePregledi = new FileRepositoryPregled();
        private FileRepositoryOperacija storageOperacije = new FileRepositoryOperacija();
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
        private List<Anamneza> anamneze = new List<Anamneza>();

        #region ZakaziPregled
        private RelayCommand zakaziPregledKomanda;
        public RelayCommand ZakaziPregledKomanda
        {
            get { return zakaziPregledKomanda; }
            set
            {
                zakaziPregledKomanda = value;
            }
        }
        public void Executed_ZakaziPregledKomanda(object obj)
        {
            inject.PacijentPageController.ZakaziPregled(trenutniPacijent);
        }

        public bool CanExecute_ZakaziPregledKomanda(object obj)
        {
            return true;
        }
        #endregion

        #region OtkaziPregled
        private RelayCommand otkaziPregledKomanda;
        public RelayCommand OtkaziPregledKomanda
        {
            get { return otkaziPregledKomanda; }
            set
            {
                otkaziPregledKomanda = value;
            }
        }
        public void Executed_OtkaziPregledKomanda(object obj)
        {
            inject.PacijentPageController.OtkaziPregled(SelektovaniRed);
        }

        public bool CanExecute_OtkaziPregledKomanda(object obj)
        {
            return true;
        }
        #endregion

        #region IzmeniPregled
        private RelayCommand izmeniPregledKomanda;
        public RelayCommand IzmeniPregledKomanda
        {
            get { return izmeniPregledKomanda; }
            set
            {
                izmeniPregledKomanda = value;
            }
        }
        public void Executed_IzmeniPregledKomanda(object obj)
        {
            inject.PacijentPageController.IzmeniPregled(SelektovaniRed);
        }

        public bool CanExecute_IzmeniPregledKomanda(object obj)
        {
            return true;
        }
        #endregion

        #region IstorijaPregleda
        private RelayCommand istorijaPregledaKomanda;
        public RelayCommand IstorijaPregledaKomanda
        {
            get { return istorijaPregledaKomanda; }
            set
            {
                istorijaPregledaKomanda = value;

            }
        }
        public void Executed_IstorijaPregledaKomanda(object obj)
        {
            inject.PacijentPageController.IstorijaPregleda(trenutniPacijent);
        }

        public bool CanExecute_IstorijaPregledaKomanda(object obj)
        {
            return true;
        }
        #endregion

        #region ObavestenjaPacijenta
        private RelayCommand obavestenjaPacijentKomanda;
        public RelayCommand ObavestenjaPacijentKomanda
        {
            get { return obavestenjaPacijentKomanda; }
            set
            {
                obavestenjaPacijentKomanda = value;

            }
        }
        public void Executed_ObavestenjaPacijentKomanda(object obj)
        {
            inject.PacijentPageController.ObavestenjaPacijent(trenutniPacijent);
        }

        public bool CanExecute_ObavestenjaPacijentKomanda(object obj)
        {
            return true;
        }
        #endregion

        public PacijentPageViewModel(Pacijent pacijent)
        {
            Inject = new Injector();
            trenutniPacijent = pacijent;
            PrikazNezavrsenihPregleda = new ObservableCollection<PrikazPregleda>();
            FormObavestenjaPacijentPage.ObavestenjaZaPacijente = new ObservableCollection<string>();

            UcitajPodatke();
            UcitajPreglede(pacijent);
            UcitajOperacije(pacijent);

            PostaviKomande();
        }

        private void PostaviKomande()
        {
            ZakaziPregledKomanda = new RelayCommand(Executed_ZakaziPregledKomanda, CanExecute_ZakaziPregledKomanda);
            OtkaziPregledKomanda = new RelayCommand(Executed_OtkaziPregledKomanda, CanExecute_OtkaziPregledKomanda);
            IzmeniPregledKomanda = new RelayCommand(Executed_IzmeniPregledKomanda, CanExecute_IzmeniPregledKomanda);
            IstorijaPregledaKomanda = new RelayCommand(Executed_IstorijaPregledaKomanda, CanExecute_IstorijaPregledaKomanda);
            ObavestenjaPacijentKomanda = new RelayCommand(Executed_ObavestenjaPacijentKomanda, CanExecute_ObavestenjaPacijentKomanda);
        }

        private void UcitajPreglede(Pacijent pacijent)
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
            pregledi = storagePregledi.GetAll();
            operacije = storageOperacije.GetAll();
            pacijenti = storagePacijenti.GetAll();
            lekari = storageLekari.GetAll();
            prostorije = storageProstorija.GetAll();
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
        }
    }
}
