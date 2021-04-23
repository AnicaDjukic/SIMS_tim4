using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Model.Prostorije
{
    public class Zaliha
    {
        public int Kolicina { get; set; }
        public Oprema Oprema { get; set; }
        public Prostorija Prostorija { get; set; }
    }
}
