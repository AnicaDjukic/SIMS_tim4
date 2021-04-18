using System;

namespace Model.Pregledi
{
   public class Pregled
   {  
      public int Id { get; set; }
      public DateTime Datum { get; set; }
      public int Trajanje { get; set; }
      public bool Zavrsen { get; set; }

      public int AnamnezaId { get; set; }
      public string lekarJmbg { get; set; }
      
      public int brojProstorije { get; set; }
      
      public string pacijentJmbg { get; set; }
    }
}