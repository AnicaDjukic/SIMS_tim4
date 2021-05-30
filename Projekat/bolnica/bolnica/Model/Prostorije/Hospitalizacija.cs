using Model.Korisnici;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Model.Pregledi
{
    public class Hospitalizacija
    {
        public int Id { get; set; }
        public DateTime DatumPocetka { get; set; }
        public DateTime DatumZavrsetka { get; set; }
        public BolnickaSoba Prostorija { get; set; }
        public Pacijent Pacijent { get; set; }

        public Hospitalizacija(DateTime DatumPocetka,DateTime DatumZavrsetka,int Id,Pacijent Pacijent,BolnickaSoba Prostorija)
        {
            this.DatumPocetka = DatumPocetka;
            this.DatumZavrsetka = DatumZavrsetka;
            this.Id = Id;
            this.Pacijent = Pacijent;
            this.Prostorija = Prostorija;
        }

        public Hospitalizacija()
        {
      
        }



    }
}
