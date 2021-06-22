using Bolnica.Model.Pregledi;
using Bolnica.Repository.Pregledi;
using Model.Pregledi;
using System.Collections.Generic;

namespace Bolnica.Service
{
    public class PregledService
    {
        private FileRepositoryPregled repositoryPregled = new FileRepositoryPregled();
        private FileRepositoryOperacija repositoryOperacija = new FileRepositoryOperacija();

        public List<Pregled> DobaviSvePreglede()
        {
            return repositoryPregled.GetAll();
        }

        public void SacuvajPregled(Pregled noviPregled)
        {
            repositoryPregled.Save(noviPregled);
        }

        public void IzmeniPregled(Pregled noviPregled)
        {
            repositoryPregled.Update(noviPregled);
        }

        public void IzbrisiPregled(Pregled pregled)
        {
            repositoryPregled.Delete(pregled);
        }

        public Pregled KreirajPregled(PrikazPregleda p)
        {
            return new Pregled
            {
                Id = p.Id,
                Datum = p.Datum,
                Trajanje = p.Trajanje,
                Zavrsen = p.Zavrsen,
                Prostorija = p.Prostorija,
                Anamneza = p.Anamneza,
                Lekar = p.Lekar,
                Pacijent = p.Pacijent
            };
        }

        public List<Pregled> DobijSvePregledeIOperacije()
        {
            List<Pregled> preglediOperacije = new List<Pregled>();
            List<Pregled> pregledi = DobaviSvePreglede();
            List<Operacija> operacije = repositoryOperacija.GetAll();
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
    }
}
