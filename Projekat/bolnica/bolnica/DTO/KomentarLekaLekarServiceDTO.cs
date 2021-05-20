using Bolnica.Model.Pregledi;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.DTO
{
    public class KomentarLekaLekarServiceDTO
    {
        public string komentar { get; set; }
        public PrikazLek prikazLeka { get; set; }
        public List<Lek> listaLekova { get; set; }

        public KomentarLekaLekarServiceDTO(string komentar, PrikazLek prikazLeka, List<Lek> listaLekova)
        {
            this.komentar = komentar;
            this.prikazLeka = prikazLeka;
            this.listaLekova = listaLekova;
        }
    }
}
