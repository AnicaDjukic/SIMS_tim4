using System;

namespace Model.Pregledi
{
   public class Pregled
   {
      public DateTime Datum { get; set; }
      public int Trajanje { get; set; }
      public bool Zavrsen { get; set; }
      public Model.Korisnici.Lekar Lekar
      {
            get;
            set;
      }
      public Model.Prostorije.Prostorija Prostorija
      {
            get;
            set;
      }
      public Model.Korisnici.Pacijent Pacijent
      {
            get;
            set;
      }
   
   }
}
