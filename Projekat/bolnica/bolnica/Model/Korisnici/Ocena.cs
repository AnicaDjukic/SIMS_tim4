using Model.Pregledi;
using System;

namespace Model.Korisnici
{
    public class Ocena
    {
        public int IdOcene { get; set; }
        public DateTime Datum { get; set; }
        public int BrojOcene { get; set; }
        public string Sadrzaj { get; set; }
        public Pregled Pregled { get; set; }
        public Pacijent Pacijent { get; set; }
        public Lekar Lekar { get; set; }
    }
}
