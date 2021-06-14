using Bolnica.DTO.Sekretar;
using Bolnica.Model.Pregledi;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Service.Sekretar
{
    public class StrategyDeleteTermine : IStrategyTermina
    {
        private PregledService pregledService = new PregledService();
        private OperacijaService operacijaService = new OperacijaService();
        public void IzvrsiAlgoritam(GodisnjiDTO godisnji, int daniNaGodisnjem)
        {
            foreach (PrikazPregleda pregledDTO in pregledService.GetAllPregledi())
                if (godisnji.KorisnickoImeLekara == pregledDTO.Lekar.KorisnickoIme && godisnji.PocetakGodisnjeg <= pregledDTO.Datum && godisnji.KrajGodisnjeg.AddDays(1) > pregledDTO.Datum)
                    pregledService.DeletePregled(pregledDTO);

            foreach (PrikazOperacije operacijaDTO in operacijaService.GetAllOperacije())
                if (godisnji.KorisnickoImeLekara == operacijaDTO.Lekar.KorisnickoIme && godisnji.PocetakGodisnjeg <= operacijaDTO.Datum && godisnji.KrajGodisnjeg.AddDays(1) > operacijaDTO.Datum)
                    operacijaService.DeleteOperacija(operacijaDTO);
        }
    }
}
