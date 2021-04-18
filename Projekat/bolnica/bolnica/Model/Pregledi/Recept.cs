// File:    Recept.cs
// Author:  Anica
// Created: 3. ????? 2021 14:42:34
// Purpose: Definition of Class Recept

using System;

namespace Model.Pregledi
{
   public class Recept
   {
      
      
      public int Lek_id { get; set; }

      public int Id { get; set; }
      public DateTime DatumIzdavanja { get; set; }
    public int Kolicina { get; set; }
        public TimeSpan VremeUzimanja { get; set; }
        public DateTime Trajanje { get; set; }

        public int Sedmicno { get; set; } //dodato
   
   }
}