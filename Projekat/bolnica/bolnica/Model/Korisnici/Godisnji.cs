using Model.Korisnici;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Model.Korisnici
{
    public class Godisnji
    {
        public DateTime PocetakGodisnjeg { get; set; }
        public DateTime KrajGodisnjeg { get; set; }
        public Lekar Lekar { get; set; }
    }
}
