using Bolnica.Forms;
using Bolnica.Model.Pregledi;
using Bolnica.Repository.Pregledi;
using Bolnica.Services.Pregledi;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Bolnica.Service
{
    public class AnamnezaPacijentService
    {
        private FileRepositoryAnamneza repositoryAnamneza = new FileRepositoryAnamneza();
        private BeleskaService serviceBeleska = new BeleskaService();
        private BeleskaIdService serviceBeleskaId = new BeleskaIdService();
        private ServiceLek serviceLek = new ServiceLek();

        public List<Anamneza> DobaviSveAnamneze()
        {
            return repositoryAnamneza.GetAll();
        }

        public void SacuvajAnamnezu(Anamneza novaAmaneza)
        {
            repositoryAnamneza.Save(novaAmaneza);
        }

        public void IzmeniAnamnezu(Anamneza novaAmaneza)
        {
            repositoryAnamneza.Update(novaAmaneza);
        }

        public void IzbrisiAnamnezu(Anamneza anamneza)
        {
            repositoryAnamneza.Delete(anamneza);
        }

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

        public void DobijLekoveIzAnamneze(Anamneza anamneza)
        {
            FormAnamnezaPage.LekoviPacijenta = new List<PrikazRecepta>();
            foreach (Recept r in anamneza.Recept)
            {
                DodajLekPacijentu(r);
            }
        }

        private void DodajLekPacijentu(Recept recept)
        {
            List<Lek> lekovi = serviceLek.DobaviSveLekove();
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
            List<Anamneza> anamneze = DobaviSveAnamneze();
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
            List<Beleska> beleske = serviceBeleska.DobaviSveBeleske();
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
            List<Beleska> beleske = serviceBeleska.DobaviSveBeleske();
            foreach (Beleska beleska in beleske)
            {
                if (novaBeleska.Id.Equals(beleska.Id))
                {
                    serviceBeleska.IzmeniBelesku(novaBeleska);
                    izmenjen = true;
                    MessageBox.Show("Beleska uspesno izmenjena.");
                    break;
                }
            }
            if (!izmenjen)
            {
                novaBeleska.Id = serviceBeleskaId.IzracunajIdBeleske();
                serviceBeleska.SacuvajBelesku(novaBeleska);
                Anamneza novaAnamneza = DobijAnamnezu(prikaz);
                novaAnamneza.Beleska.Id = novaBeleska.Id;
                IzmeniAnamnezu(novaAnamneza);
                MessageBox.Show("Beleska uspesno napravljena.");
            }
        }

        public string DobijNazivLeka(int id)
        {
            List<Lek> lekovi = serviceLek.DobaviSveLekove();
            foreach (Lek lek in lekovi)
            {
                if (id.Equals(lek.Id))
                {
                    return lek.Naziv;
                }
            }
            return "";
        }
    }
}
