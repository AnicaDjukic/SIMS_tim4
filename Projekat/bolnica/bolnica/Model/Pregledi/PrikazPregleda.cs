using Model.Korisnici;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Bolnica.Model.Pregledi
{
    public class PrikazPregleda
    {

        public PrikazPregleda()
        {
            this.Anamneza = new Anamneza();
            this.Lekar = new Lekar();
            this.Prostorija = new Prostorija();
            this.Pacijent = new Pacijent();
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
