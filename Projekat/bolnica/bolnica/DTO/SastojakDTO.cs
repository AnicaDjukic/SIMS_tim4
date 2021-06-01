using Bolnica.Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.DTO
{
    public class SastojakDTO
    {
        public int Id { get; set; }
        public string Naziv { get; set; }

        public SastojakDTO(Sastojak sastojak)
        {
            Id = sastojak.Id;
            Naziv = sastojak.Naziv;
        }
    }
}
