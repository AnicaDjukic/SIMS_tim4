using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Pregledi
{
    public class PrikazRecepta
    {
        private int id;
        private DateTime datumIzdavanja;
        private int kolicina;
        private TimeSpan vremeUzimanja;
        private DateTime trajanje;

        public Lek lek { get; set; }

        public int Id { get; set; }
        public DateTime DatumIzdavanja { get; set; }
        public int Kolicina { get; set; }
        public TimeSpan VremeUzimanja { get; set; }
        public DateTime Trajanje { get; set; }
    }
}
