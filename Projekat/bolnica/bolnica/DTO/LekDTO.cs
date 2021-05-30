
using Bolnica.Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.DTO
{
    class LekDTO
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Proizvodjac { get; set; }
        public int KolicinaUMg { get; set; }
        public StatusLeka Status { get; set; }
        public int Zalihe { get; set; }
    }
}
