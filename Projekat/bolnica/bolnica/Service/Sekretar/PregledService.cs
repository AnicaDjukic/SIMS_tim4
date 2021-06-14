using Bolnica.Model.Pregledi;
using Bolnica.Repository.Pregledi;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Service.Sekretar
{
    public class PregledService
    {
        private IRepositoryPregled skladistePregleda;

        public PregledService()
        {
            skladistePregleda = new FileRepositoryPregled();
        }

        public List<PrikazPregleda> GetAllPregledi() 
        {
            List<Pregled> sviPregledi = skladistePregleda.GetAll();
            List<PrikazPregleda> sviPreglediDTO = new List<PrikazPregleda>();

            foreach (Pregled p in sviPregledi)
                sviPreglediDTO.Add(new PrikazPregleda(p.Id, p.Datum, p.Trajanje, p.Zavrsen, p.Hitan, p.Anamneza, p.Lekar, p.Prostorija, p.Pacijent));

            return sviPreglediDTO;
        }

        public void DeletePregled(PrikazPregleda pregledDTO) 
        {
            Pregled pregled = new Pregled { Id = pregledDTO.Id, Pacijent = pregledDTO.Pacijent, Anamneza = pregledDTO.Anamneza, Datum = pregledDTO.Datum, Hitan = pregledDTO.Hitan, Lekar = pregledDTO.Lekar, Prostorija = pregledDTO.Prostorija, Trajanje = pregledDTO.Trajanje, Zavrsen = pregledDTO.Zavrsen };
            skladistePregleda.Delete(pregled);
        }

        public void UpdateDatumPregleda(PrikazPregleda pregledDTO, int brojDanaZaPomeranje)
        {
            Pregled pregled = new Pregled { Id = pregledDTO.Id, Pacijent = pregledDTO.Pacijent, Anamneza = pregledDTO.Anamneza, Datum = pregledDTO.Datum.AddDays(brojDanaZaPomeranje), Hitan = pregledDTO.Hitan, Lekar = pregledDTO.Lekar, Prostorija = pregledDTO.Prostorija, Trajanje = pregledDTO.Trajanje, Zavrsen = pregledDTO.Zavrsen };
            skladistePregleda.Update(pregled);
        }

        public PrikazPregleda GetPregledById(int id)
        {
            Pregled pregled = skladistePregleda.GetById(id);
            PrikazPregleda pregledDTO = new PrikazPregleda(pregled.Id, pregled.Datum, pregled.Trajanje, pregled.Zavrsen, pregled.Hitan, pregled.Anamneza, pregled.Lekar, pregled.Prostorija, pregled.Pacijent);
            return pregledDTO;
        }
    }
}
