using bolnica;
using Bolnica.Controller;
using Bolnica.Forms;
using Bolnica.Model.Korisnici;
using Model.Korisnici;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Bolnica.Service
{
    public class PodsetnikService
    {
        private RepositoryController repositoryController = new RepositoryController();

        private List<int> obavestenjaPregled = new List<int>();
        private List<string> obavestenjaTerapija = new List<string>();

        public void ProveriObavestenja(Pacijent trenutniPacijent)
        {
            List<Pregled> pregledi = NadjiSvePreglede();
            foreach (Pregled pregled in pregledi)
            {
                if (trenutniPacijent.Jmbg.Equals(pregled.Pacijent.Jmbg))
                {
                    ProveriVremeZaTerapiju(pregled);
                    ProveriPocetakPregleda(pregled);
                    ProveriNotifikacije(pregled);
                }
            }
        }

        private List<Pregled> NadjiSvePreglede()
        {
            List<Pregled> pregledi = repositoryController.DobijPreglede();
            List<Operacija> operacije = repositoryController.DobijOperacije();
            foreach (Operacija o in operacije)
            {
                pregledi.Add(o);
            }

            return pregledi;
        }

        private void ProveriVremeZaTerapiju(Pregled pregled)
        {
            List<Anamneza> anamneze = repositoryController.DobijAnamneze();
            foreach (Anamneza a in anamneze)
            {
                if (pregled.Anamneza.Id.Equals(a.Id))
                {
                    foreach (Recept r in a.Recept)
                    {
                        if (r.Trajanje.CompareTo(DateTime.Now) >= 0)
                        {
                            if (r.VremeUzimanja == 4)
                            {

                            }
                            else if (r.VremeUzimanja == 6)
                            {

                            }
                            else if (r.VremeUzimanja == 8)
                            {

                            }
                            else if (r.VremeUzimanja == 12)
                            {

                            }
                            else if (r.VremeUzimanja == 24)
                            {
                                if (DateTime.Now.AddMinutes(15).CompareTo(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 0, 0)) >= 0
                                    && DateTime.Now.CompareTo(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 0, 0)) <= 0)
                                {
                                    string poruka = "Podsetnik o terapiji:\n" +
                                        "Danas treba da popijete lek '" + GetNazivLeka(r.Lek.Id) +
                                        "' u 16:00h.";
                                    if (!obavestenjaTerapija.Contains(poruka))
                                    {
                                        obavestenjaTerapija.Add(poruka);
                                        MessageBox.Show(poruka);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ProveriPocetakPregleda(Pregled pregled)
        {
            if (!pregled.Zavrsen && DateTime.Now.AddHours(1).CompareTo(pregled.Datum) >= 0 && DateTime.Now.CompareTo(pregled.Datum) <= 0)
            {
                if (!obavestenjaPregled.Contains(pregled.Id))
                {
                    string poruka = "Vaš zakazani pregled/operacija počinje danas " +
                    pregled.Datum.ToShortDateString() + ". godine u " + pregled.Datum.ToShortTimeString() + ".\n" +
                    "Pregled se održava u prostoriji broj " + pregled.Prostorija.BrojProstorije + ".";
                    MessageBox.Show(poruka);
                    FormObavestenjaPacijentPage.ObavestenjaPacijent.Add(new Obavestenje(-1, DateTime.Now, poruka, "Početak pregelda/operacije", false));
                    obavestenjaPregled.Add(pregled.Id);
                }
            }
        }

        private void ProveriNotifikacije(Pregled pregled)
        {
            List<Anamneza> anamneze = repositoryController.DobijAnamneze();
            List<Beleska> beleske = repositoryController.DobijBeleske();
            foreach (Anamneza anamneza in anamneze)
            {
                if (pregled.Anamneza.Id.Equals(anamneza.Id))
                {
                    foreach (Beleska beleska in beleske)
                    {
                        if (anamneza.Beleska.Id.Equals(beleska.Id))
                        {
                            SlanjeNotifikacijeiZaJednuBelesku(beleska);
                        }
                    }
                }
            }
        }

        private void SlanjeNotifikacijeiZaJednuBelesku(Beleska beleska)
        {
            DateTime vremeObavestenja = DobijTacnoVremeObavestenje(beleska);
            if (ProveraVremenaObavestenja(beleska, vremeObavestenja))
            {
                PosaljiNotifikaciju(beleska, vremeObavestenja);
            }
        }

        private void PosaljiNotifikaciju(Beleska beleska, DateTime vremeObavestenja)
        {
            string poruka = vremeObavestenja.ToString() + " - Obavestenje na osnovu Vaše beleske:\r" + beleska.Zabeleska;
            MessageBox.Show(poruka, "Obavestenje na osnovu Vaše beleške!");
            FormObavestenjaPacijentPage.ObavestenjaPacijent.Add(new Obavestenje(-1, vremeObavestenja, beleska.Zabeleska, "Obaveštenje na osnovu Vaše beleške", false));
            beleska.Prikazana = true;
            repositoryController.IzmeniBelesku(beleska);
        }

        private bool ProveraVremenaObavestenja(Beleska beleska, DateTime vremeObavestenja)
        {
            if (beleska.Podsetnik && DateTime.Now.CompareTo(beleska.DatumPrekida) <= 0)
            {
                if (DateTime.Now.CompareTo(vremeObavestenja.AddMinutes(30)) <= 0)
                {
                    return !beleska.Prikazana && DateTime.Now.CompareTo(vremeObavestenja) >= 0;
                }
                else
                {
                    IzmeniStatusBeleske(beleska);
                    return false;
                }
            }
            return false;
        }

        private void IzmeniStatusBeleske(Beleska beleska)
        {
            if (beleska.Prikazana)
            {
                beleska.Prikazana = false;
                repositoryController.IzmeniBelesku(beleska);
            }
        }

        private static DateTime DobijTacnoVremeObavestenje(Beleska beleska)
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, beleska.Vreme.Hours, beleska.Vreme.Minutes, beleska.Vreme.Seconds);
        }

        private string GetNazivLeka(int id)
        {
            List<Lek> lekovi = repositoryController.DobijLekove();
            foreach (Lek l in lekovi)
            {
                if (l.Id.Equals(id))
                {
                    return l.Naziv;
                }
            }
            return "";
        }

        public void BlokirajPacijenta(Pacijent trenutniPacijent)
        {
            trenutniPacijent.Obrisan = true;
            repositoryController.IzmeniPacijenta(trenutniPacijent);
            MessageBox.Show("Zbog zloupotrebe nase aplikacije prinudjeni smo da Vam onemogucimo pristup istoj. " +
                "Vas nalog ce biti obrisan i vise necete moci da se ulogujete na Vas profil!", "Iskljucenje");
            new MainWindow().Show();
        }

        public int DobijBrojAktivnosti(Pacijent trenutniPacijent)
        {
            int brojac = 0;
            List<AntiTrol> antiTrol = repositoryController.DobijAntiTrol();
            foreach (AntiTrol a in antiTrol)
            {
                if (trenutniPacijent.Jmbg.Equals(a.Pacijent.Jmbg) && a.Datum.AddDays(3).CompareTo(DateTime.Now) > 0)
                {
                    brojac++;
                }
            }
            return brojac;
        }
    }
}
