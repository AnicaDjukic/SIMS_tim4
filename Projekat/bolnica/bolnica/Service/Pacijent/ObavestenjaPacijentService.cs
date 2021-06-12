using Bolnica.Controller;
using Bolnica.Forms;
using Bolnica.Model.Korisnici;
using Model.Korisnici;
using Model.Pregledi;
using System;
using System.Collections.Generic;

namespace Bolnica.Service
{
    public class ObavestenjaPacijentService
    {
        private RepositoryController repositoryController = new RepositoryController();

        public void PrikaziObavestenjaBolnice(Pacijent pacijent)
        {
            List<Obavestenje> obavestenja = repositoryController.DobijObavestenja();
            FormObavestenjaPacijentPage.Obavestenja = new List<Obavestenje>();
            foreach (Obavestenje o in obavestenja)
            {
                foreach (Korisnik k in o.Korisnici)
                {
                    if (pacijent.KorisnickoIme.Equals(k.KorisnickoIme))
                    {
                        FormObavestenjaPacijentPage.Obavestenja.Add(o);
                        break;
                    }
                }
            }
        }

        public void PrikaziObavestenjaOLekovima(Pacijent pacijent)
        {
            List<Pregled> preglediOperacije = DobijSvePregledeIOperacije();
            List<Anamneza> anamneze = repositoryController.DobijAnamneze();
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

        private List<Pregled> DobijSvePregledeIOperacije()
        {
            List<Pregled> preglediOperacije = new List<Pregled>();
            List<Pregled> pregledi = repositoryController.DobijPreglede();
            List<Operacija> operacije = repositoryController.DobijOperacije();
            foreach (Pregled pregled in pregledi)
            {
                preglediOperacije.Add(pregled);
            }
            foreach (Operacija operacija in operacije)
            {
                preglediOperacije.Add(operacija);
            }

            return preglediOperacije;
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
                    string obavestenje = NapraviPorukuObavestenja(nazivLeka, vremeUzimanja, datumZavrsetka);

                    List<string> prethodnaObavestenja = DobijPrethodnaObavestenja();
                    if (!prethodnaObavestenja.Contains(obavestenje))
                    {
                        Obavestenje o = KreirajObavestenje(obavestenje);
                        FormObavestenjaPacijentPage.ObavestenjaPacijent.Add(o);
                    }
                }
            }
        }

        private string NapraviPorukuObavestenja(string nazivLeka, int vremeUzimanja, string datumZavrsetka)
        {
            return "Danas treba da popijete lek '" + nazivLeka + "'. " +
                                "Ovaj lek se pije " + DobijBrojUzimanjaDnevno(vremeUzimanja) +
                                " dnevno u razmaku od po " + DobijVremeUzimanja(vremeUzimanja) +
                                "Ovaj lek Vam je prepisan do " + datumZavrsetka + ".";
        }

        private static List<string> DobijPrethodnaObavestenja()
        {
            List<string> prethodnaObavestenja = new List<string>();
            foreach (Obavestenje o in FormObavestenjaPacijentPage.ObavestenjaPacijent)
            {
                prethodnaObavestenja.Add(o.Sadrzaj);
            }

            return prethodnaObavestenja;
        }

        private static Obavestenje KreirajObavestenje(string obavestenje)
        {
            return new Obavestenje()
            {
                Id = -1,
                Datum = DateTime.Now,
                Naslov = "Obavestenje o leku",
                Sadrzaj = obavestenje
            };
        }

        private string DobijNazivLeka(int id)
        {
            List<Lek> lekovi = repositoryController.DobijLekove();
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
            if (vremeUzimanja == 24)
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
