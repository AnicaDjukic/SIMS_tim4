using Bolnica.Repository.Pregledi;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Service.Sekretar
{
    public class OperacijaService
    {
        private IRepositoryOperacija skladisteOperacija;

        public OperacijaService()
        {
            skladisteOperacija = new FileRepositoryOperacija();
        }

        public List<PrikazOperacije> GetAllOperacije()
        {
            List<Operacija> sveOperacije = skladisteOperacija.GetAll();
            List<PrikazOperacije> sveOperacijeDTO = new List<PrikazOperacije>();

            foreach (Operacija o in sveOperacije)
                sveOperacijeDTO.Add(new PrikazOperacije(o.Id, o.Datum, o.Trajanje, o.Zavrsen, o.Hitan, o.Anamneza, o.Lekar, o.Prostorija, o.Pacijent, o.TipOperacije));

            return sveOperacijeDTO;
        }

        public void DeleteOperacija(PrikazOperacije operacijaDTO)
        {
            Operacija operacija = new Operacija { Id = operacijaDTO.Id, Pacijent = operacijaDTO.Pacijent, Anamneza = operacijaDTO.Anamneza, Datum = operacijaDTO.Datum, Hitan = operacijaDTO.Hitan, Lekar = operacijaDTO.Lekar, Prostorija = operacijaDTO.Prostorija, Trajanje = operacijaDTO.Trajanje, Zavrsen = operacijaDTO.Zavrsen, TipOperacije = operacijaDTO.TipOperacije };
            skladisteOperacija.Delete(operacija);
        }
    }
}
