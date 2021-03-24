using System;

namespace Model.Pregledi
{
   public class Pregled
   {
      private DateTime datum;
      private int trajanje;
      private bool zavrsen;
      
      public Model.Korisnici.Lekar lekar;
      public Model.Prostorije.Prostorija prostorija;
      public Model.Korisnici.Pacijent pacijent;
      
      
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