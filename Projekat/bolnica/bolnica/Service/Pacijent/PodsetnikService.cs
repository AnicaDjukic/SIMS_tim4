using bolnica;
using Bolnica.Forms;
using Bolnica.Model.Korisnici;
using Model.Korisnici;
using Model.Pacijenti;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Bolnica.Service
{
    public class PodsetnikService
    {
        private List<int> obavestenjaPregled = new List<int>();
        private List<string> obavestenjaTerapija = new List<string>();

        private PregledService servicePregled = new PregledService();
        private AnamnezaPacijentService serviceAnamneza = new AnamnezaPacijentService();
        private BeleskaService serviceBeleska = new BeleskaService();

        public void ProveriObavestenja(Pacijent trenutniPacijent)
        {
            List<Pregled> pregledi = servicePregled.DobijSvePregledeIOperacije();
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

        private void ProveriVremeZaTerapiju(Pregled pregled)
        {
            List<Anamneza> anamneze = serviceAnamneza.DobaviSveAnamneze();
            foreach (Anamneza a in anamneze)
            {
                if (pregled.Anamneza.Id.Equals(a.Id))
                {
                    foreach (Recept r in a.Recept)
                    {
                        if (r.Trajanje.CompareTo(DateTime.Now) >= 0)
                        {
                            if (r.VremeUzimanja == 24)
                            {
                                if (DateTime.Now.AddMinutes(15).CompareTo(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 0, 0)) >= 0
                                    && DateTime.Now.CompareTo(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 0, 0)) <= 0)
                                {
                                    string poruka = "Podsetnik o terapiji:\n" +
                                        "Danas treba da popijete lek '" + serviceAnamneza.DobijNazivLeka(r.Lek.Id) +
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
            List<Anamneza> anamneze = serviceAnamneza.DobaviSveAnamneze();
            List<Beleska> beleske = serviceBeleska.DobaviSveBeleske();
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
            serviceBeleska.IzmeniBelesku(beleska);
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
                serviceBeleska.IzmeniBelesku(beleska);
            }
        }

        private static DateTime DobijTacnoVremeObavestenje(Beleska beleska)
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, beleska.Vreme.Hours, beleska.Vreme.Minutes, beleska.Vreme.Seconds);
        }

        public void BlokirajPacijenta(Pacijent trenutniPacijent)
        {
            trenutniPacijent.Obrisan = true;
            FileRepositoryPacijent repositoryPacijent = new FileRepositoryPacijent();
            repositoryPacijent.Update(trenutniPacijent);
            MessageBox.Show("Zbog zloupotrebe nase aplikacije prinudjeni smo da Vam onemogucimo pristup istoj. " +
                "Vas nalog ce biti obrisan i vise necete moci da se ulogujete na Vas profil!", "Iskljucenje");
            new MainWindow().Show();
        }
    }
}
