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
        public int VremeUzimanja { get; set; }
        public DateTime Trajanje { get; set; }

        public PrikazRecepta()
        {

        }

        public PrikazRecepta(Lek lek, DateTime datumPrepisivanja, DateTime datumPrekida, int kolicina, int vremeUzimanja)
        {
            this.lek = lek;
            this.DatumIzdavanja = datumPrepisivanja;
            this.Trajanje = datumPrekida;
            this.Kolicina = kolicina;
            this.VremeUzimanja = vremeUzimanja;
        }
    }
}
