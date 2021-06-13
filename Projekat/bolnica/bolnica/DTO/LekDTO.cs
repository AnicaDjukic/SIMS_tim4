
using Bolnica.Model.Pregledi;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.DTO
{
    public class LekDTO
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Proizvodjac { get; set; }
        public int KolicinaUMg { get; set; }
        public StatusLeka Status { get; set; }
        public int Zalihe { get; set; }

        public LekDTO () { }
        public LekDTO(int id, string naziv, string proizvodjac, int kolicinaUMg, StatusLeka status, int zalihe)
        {
            Id = id;
            Naziv = naziv;
            Proizvodjac = proizvodjac;
            KolicinaUMg = kolicinaUMg;
            Status = status;
            Zalihe = zalihe;
        }

        public LekDTO(Lek lek)
        {
            Id = lek.Id;
            Naziv = lek.Naziv;
            Proizvodjac = lek.Proizvodjac;
            KolicinaUMg = lek.KolicinaUMg;
            Status = lek.Status;
            Zalihe = lek.Zalihe;
        }
    }
}
