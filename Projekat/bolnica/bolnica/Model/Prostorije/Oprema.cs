using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Model.Prostorije
{
    public class Oprema
    {
        public string Sifra { get; set; }
        public string Naziv { get; set; }

        public TipOpreme TipOpreme { get; set; }
        public int Kolicina { get; set; }

        //public Dictionary<string, int> OpremaPoSobama { get; set; }

    }
}
