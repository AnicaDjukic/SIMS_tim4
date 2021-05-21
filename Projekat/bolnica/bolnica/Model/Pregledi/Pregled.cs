using Bolnica.Model.Pregledi;
using Model.Korisnici;
using Model.Prostorije;
using System;

namespace Model.Pregledi
{
    public class Pregled
    {
        public Pregled()
        {
            this.Anamneza = new Anamneza();
            this.Lekar = new Lekar();
            this.Prostorija = new Prostorija();
            this.Pacijent = new Pacijent();
        }
        public Pregled(PrikazPregleda p)
        {
            this.Id = p.Id;
            this.Datum = p.Datum;
            this.Trajanje = p.Trajanje;
            this.Zavrsen = p.Zavrsen;
            this.Hitan = p.Hitan;
            this.Anamneza = p.Anamneza;
            this.Lekar = p.Lekar;
            this.Prostorija = p.Prostorija;
            this.Pacijent = p.Pacijent;
        }

        
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public int Trajanje { get; set; }
        public bool Zavrsen { get; set; }
        public bool Hitan { get; set; }
        public Anamneza Anamneza { get; set; }
        public Lekar Lekar { get; set; }
        public Prostorija Prostorija { get; set; }
        public Pacijent Pacijent { get; set; }
    }
}