using Model.Korisnici;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Model.Pregledi
{
    public class PrikazPregleda
    {
        public Lekar lekar;
        public Prostorija prostorija;
        public Pacijent pacijent;

        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public int Trajanje { get; set; }
        public bool Zavrsen { get; set; }

        public int AnamnezaId { get; set; }
        public Lekar Lekar
        {
            get
            {
                return lekar;
            }
            set
            {
                this.lekar = value;
            }
        }
        public Prostorija Prostorija
        {
            get
            {
                return prostorija;
            }
            set
            {
                this.prostorija = value;
            }
        }
        public Pacijent Pacijent
        {
            get
            {
                return pacijent;
            }
            set
            {
                this.pacijent = value;
            }
        }
    }
}
