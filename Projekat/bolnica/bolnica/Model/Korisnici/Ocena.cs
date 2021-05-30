using Model.Korisnici;
using System;

namespace Bolnica.Model.Korisnici
{
    public class Ocena
    {
        public int IdOcene { get; set; }
        public DateTime Datum { get; set; }
        public int BrojOcene { get; set; }
        public string Sadrzaj { get; set; }
        public Pacijent Pacijent { get; set; }
        public Lekar Lekar { get; set; }
    }
}
