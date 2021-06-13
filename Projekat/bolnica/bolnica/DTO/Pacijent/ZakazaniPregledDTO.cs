using Model.Korisnici;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.DTO
{
    public class ZakazaniPregledDTO
    {
        public Pacijent Pacijent { get; set; }
        public DateTime Datum { get; set; }
        public String Sat { get; set; }
        public String Minut { get; set; }
        public String Lekar { get; set; }

        public ZakazaniPregledDTO() { }
        public ZakazaniPregledDTO(Pacijent pacijent, DateTime datum, String sat, String minut, String lekar)
        {
            Pacijent = pacijent;
            Datum = datum;
            Sat = sat;
            Minut = minut;
            Lekar = lekar;
        }
    }
}
