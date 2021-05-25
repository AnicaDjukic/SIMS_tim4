using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Pregledi
{
    public class PrikazRecepta
    {
        public Lek lek { get; set; }
        public int Id { get; set; }
        public DateTime DatumIzdavanja { get; set; }
        public int Kolicina { get; set; }
        public TimeSpan VremeUzimanja { get; set; }
        public DateTime Trajanje { get; set; }

        public PrikazRecepta() { }
        public PrikazRecepta(int Id, DateTime DatumIzdavanja, int Kolicina, TimeSpan VremeUzimanja, DateTime Trajanje)
        {
            this.Id = Id;
            this.DatumIzdavanja = DatumIzdavanja;
            this.Kolicina = Kolicina;
            this.VremeUzimanja = VremeUzimanja;
            this.Trajanje = Trajanje;
        }
        public PrikazRecepta(DateTime DatumIzdavanja, int Kolicina, TimeSpan VremeUzimanja, DateTime Trajanje)
        {
            this.DatumIzdavanja = DatumIzdavanja;
            this.Kolicina = Kolicina;
            this.VremeUzimanja = VremeUzimanja;
            this.Trajanje = Trajanje;
        }
    }
}
