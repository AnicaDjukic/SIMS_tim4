using Bolnica.Controller;
using Bolnica.Forms;
using Bolnica.Model.Pregledi;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Bolnica.Service
{
    public class AnamnezaPacijentService
    {
        private RepositoryController repositoryController = new RepositoryController();
        private RacunajIdController racunajIdController = new RacunajIdController();

        public void PostaviVremeComboBox()
        {
            FormAnamnezaPage.Vremena = new List<TimeSpan>();
            for (int i = 0; i < 24; i++)
            {
                FormAnamnezaPage.Vremena.Add(new TimeSpan(i, 0, 0));
                FormAnamnezaPage.Vremena.Add(new TimeSpan(i, 30, 0));
            }
        }

        public void PostaviDatumComboBox()
        {
            FormAnamnezaPage.Datumi = new List<DateTime>();
            DateTime danas = DateTime.Today;
            for (int i = 0; i < 10; i++)
            {
                FormAnamnezaPage.Datumi.Add(danas.AddDays(i + 1));
            }
        }

        public void PostaviSveLekovePacijentu(Anamneza anamneza)
        {
            FormAnamnezaPage.LekoviPacijenta = new List<PrikazRecepta>();
            foreach (Recept r in anamneza.Recept)
            {
                DodajLekPacijentu(r);
            }
        }

        private void DodajLekPacijentu(Recept recept)
        {
            List<Lek> lekovi = repositoryController.DobijLekove();
            foreach (Lek lek in lekovi)
            {
                if (recept.Lek.Id.Equals(lek.Id))
                {
                    PrikazRecepta pr = new PrikazRecepta(lek, recept.DatumIzdavanja, recept.Trajanje, recept.Kolicina, recept.VremeUzimanja);
                    FormAnamnezaPage.LekoviPacijenta.Add(pr);
                }
            }
        }

        public Anamneza DobijAnamnezu(PrikazPregleda prikazPregleda)
        {
            List<Anamneza> anamneze = repositoryController.DobijAnamneze();
            foreach (Anamneza anamneza in anamneze)
            {
                if (anamneza.Id.Equals(prikazPregleda.Anamneza.Id))
                {
                    return anamneza;
                }
            }
            return null;
        }

        public Beleska DobijBelesku(Anamneza anamneza)
        {
            List<Beleska> beleske = repositoryController.DobijBeleske();
            foreach (Beleska beleska in beleske)
            {
                if (anamneza.Beleska.Id.Equals(beleska.Id))
                {
                    return beleska;
                }
            }
            return null;
        }

        public void SacuvajNovuBelesku(Beleska novaBeleska, PrikazPregleda prikaz)
        {
            bool izmenjen = false;
            List<Beleska> beleske = repositoryController.DobijBeleske();
            foreach (Beleska beleska in beleske)
            {
                if (novaBeleska.Id.Equals(beleska.Id))
                {
                    repositoryController.IzmeniBelesku(novaBeleska);
                    izmenjen = true;
                    MessageBox.Show("Beleska uspesno izmenjena.");
                    break;
                }
            }
            if (!izmenjen)
            {
                novaBeleska.Id = racunajIdController.IzracunajIdBeleske();
                repositoryController.SacuvajBelesku(novaBeleska);
                Anamneza novaAnamneza = DobijAnamnezu(prikaz);
                novaAnamneza.Beleska.Id = novaBeleska.Id;
                repositoryController.IzmeniAnamnezu(novaAnamneza);
                MessageBox.Show("Beleska uspesno napravljena.");
            }
        }
    }
}
