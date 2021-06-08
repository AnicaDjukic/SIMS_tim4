using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.Repository.Korisnici;
using Bolnica.Repository.Pregledi;
using Model.Korisnici;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormObavestenjaPacijentPage.xaml
    /// </summary>
    public partial class FormObavestenjaPacijentPage : Page
    {
        public static ObservableCollection<Obavestenje> ObavestenjaPacijent
        {
            get;
            set;
        }
        public List<Obavestenje> Obavestenja { get; set; }
        private Pacijent pacijent = new Pacijent();

        private FileRepositoryPregled storagePregledi = new FileRepositoryPregled();
        private FileRepositoryOperacija storageOperacije = new FileRepositoryOperacija();
        private FileRepositoryAnamneza storageAnamneze = new FileRepositoryAnamneza();
        private FileRepositoryLek storageLek = new FileRepositoryLek();
        private FileRepositoryObavestenje storageObavestenja = new FileRepositoryObavestenje();

        private List<Pregled> pregledi = new List<Pregled>();
        private List<Operacija> operacije = new List<Operacija>();
        private List<Anamneza> anamneze = new List<Anamneza>();
        private List<Lek> lekovi = new List<Lek>();
        private List<Obavestenje> obavestenja = new List<Obavestenje>();

        public static string DanasnjiDatum
        {
            get;
            set;
        }

        public FormObavestenjaPacijentPage(Pacijent trenutniPacijent)
        {
            InitializeComponent();

            this.DataContext = this;

            pacijent = trenutniPacijent;
            //ObavestenjaPacijent = new ObservableCollection<Obavestenje>();
            Obavestenja = new List<Obavestenje>();
            obavestenja = storageObavestenja.GetAll();
            foreach (Obavestenje o in obavestenja)
            {
                foreach (Korisnik k in o.Korisnici)
                {
                    if (trenutniPacijent.KorisnickoIme.Equals(k.KorisnickoIme))
                    {
                        Obavestenja.Add(o);
                        break;
                    }
                }
            }
            DanasnjiDatum = "Obaveštenja o lekovima za dan " + DateTime.Today.ToShortDateString();
            pregledi = storagePregledi.GetAll();
            operacije = storageOperacije.GetAll();
            anamneze = storageAnamneze.GetAll();
            List<Pregled> preglediOperacije = new List<Pregled>();
            foreach (Pregled pregled in pregledi)
            {
                preglediOperacije.Add(pregled);
            }
            foreach (Operacija operacija in operacije)
            {
                preglediOperacije.Add(operacija);
            }
            foreach (Pregled pregled in preglediOperacije)
            {
                if (pacijent.Jmbg.Equals(pregled.Pacijent.Jmbg))
                {
                    foreach (Anamneza anamneza in anamneze)
                    {
                        if (pregled.Anamneza.Id.Equals(anamneza.Id))
                        {
                            ProcitajRecepte(anamneza.Recept);
                        }
                    }
                }
            }
        }

        private void ProcitajRecepte(List<Recept> recepti)
        {
            foreach (Recept recept in recepti)
            {
                string nazivLeka = DobijNazivLeka(recept.Lek.Id);
                int vremeUzimanja = recept.VremeUzimanja;
                string datumZavrsetka = recept.Trajanje.ToShortDateString();
                if (recept.Trajanje.CompareTo(DateTime.Today) >= 0)
                {
                    string obavestenje = "Danas treba da popijete lek '" + nazivLeka + "'. " +
                    "Ovaj lek se pije " + DobijBrojUzimanjaDnevno(vremeUzimanja) +
                    " dnevno u razmaku od po " + DobijVremeUzimanja(vremeUzimanja) +
                    "Ovaj lek Vam je prepisan do " + datumZavrsetka + ".";
                    
                    List<string> list = new List<string>();
                    foreach(Obavestenje ob in ObavestenjaPacijent)
                    {
                        list.Add(ob.Sadrzaj);
                    }
                    if (!list.Contains(obavestenje))
                    {
                        Obavestenje o = new Obavestenje()
                        {
                            Id = -1,
                            Datum = DateTime.Now,
                            Naslov = "Obavestenje o leku",
                            Sadrzaj = obavestenje
                        };
                        ObavestenjaPacijent.Add(o);
                    }
                }
            }
        }

        private string DobijNazivLeka(int id)
        {
            lekovi = storageLek.GetAll();
            foreach (Lek lek in lekovi)
            {
                if (id.Equals(lek.Id))
                {
                    return lek.Naziv;
                }
            }
            return "";
        }

        private string DobijBrojUzimanjaDnevno(int vremeUzimanja)
        {
            if (vremeUzimanja == 0)
            {
                return "jednom";
            }
            else
            {
                return 24 / vremeUzimanja + " puta";
            }
        }

        private string DobijVremeUzimanja(int vremeUzimanja)
        {
            return vremeUzimanja switch
            {
                0 => "24 sata. ",
                1 => vremeUzimanja + " sat. ",
                2 => vremeUzimanja + " sata. ",
                3 => vremeUzimanja + " sata. ",
                4 => vremeUzimanja + " sata. ",
                _ => vremeUzimanja + " sati. ",
            };
        }
    }
}
