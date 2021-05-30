using Model.Korisnici;
using System;

namespace Bolnica.Model.Korisnici
{
    public class AntiTrol
    {
        public int Id { get; set; }
        public Pacijent Pacijent { get; set; }
        public DateTime Datum { get; set; }
    }
}
