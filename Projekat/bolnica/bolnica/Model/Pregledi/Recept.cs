using System;

namespace Model.Pregledi
{
    public class Recept
    {
        public Lek Lek { get; set; }
        public int Id { get; set; }
        public DateTime DatumIzdavanja { get; set; }
        public int Kolicina { get; set; }
        public int VremeUzimanja { get; set; }
        public DateTime Trajanje { get; set; }

        public Recept()
        {
            Lek = new Lek();
        }


        public Recept(int Id,Lek Lek, DateTime DatumIzdavanja, int Kolicina,int VremeUzimanja, DateTime Trajanje)
        {
            this.Id = Id;
            this.Lek = Lek;
            this.DatumIzdavanja = DatumIzdavanja;
            this.Kolicina = Kolicina;
            this.VremeUzimanja = VremeUzimanja;
            this.Trajanje = Trajanje;
        }
    }
}
