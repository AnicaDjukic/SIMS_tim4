using System;

namespace Model.Pregledi
{
   public class Pregled
   {  
      public Model.Korisnici.Lekar lekar;
      public Model.Prostorije.Prostorija prostorija;
      public Model.Korisnici.Pacijent pacijent;

      public DateTime Datum { get; set; }
      public int Trajanje { get; set; }
      public bool Zavrsen { get; set; }
      public Model.Korisnici.Lekar Lekar
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
      public Model.Prostorije.Prostorija Prostorija
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
      public Model.Korisnici.Pacijent Pacijent
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