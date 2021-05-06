using Model.Korisnici;
using Model.Prostorije;
using System;

namespace Model.Pregledi
{
    public class Pregled
    {  
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public int Trajanje { get; set; }
        public bool Zavrsen { get; set; }
        public bool Hitan { get; set; }
        public int AnamnezaId { get; set; }
        public string lekarJmbg { get; set; }
        public Lekar Lekar { get; set; }
        public string brojProstorije { get; set; }
        public Prostorija Prostorija { get; set; }
        public string pacijentJmbg { get; set; }

        public Pacijent Pacijent { get; set; }
    }
}