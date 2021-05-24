using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Model.Pregledi
{
    public class Beleska
    {
        public int Id { get; set; }
        public string Zabeleska { get; set; }
        public bool Podsetnik { get; set; }
        public TimeSpan Vreme { get; set; }
        public DateTime DatumPrekida { get; set; }
        public bool Prikazana { get; set; }
        public Anamneza Anamneza { get; set; } 
    }
}
