using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Model.Pregledi
{
    public class PrikazLek
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int KolicinaUMg { get; set; }
        public StatusLeka Status { get; set; }
        public int Zalihe { get; set; }
        public String Zamena { get; set; }
        public string Proizvodjac { get; set; }
        public string Sastojak { get; set; }
    }
}
