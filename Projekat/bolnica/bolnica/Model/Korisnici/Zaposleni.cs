using System;

namespace Model.Korisnici
{
   public abstract class Zaposleni : Osoba
   {
      private int mbr;
      private double plata;
      private int brojSlobodnihDana;
      private int godineStaza;
      private bool zaposlen;

      public int Mbr { get; set; }
      public double Plata { get; set; }
      public int BrojSlobodnihDana { get; set; }
      public int GodineStaza { get; set; }
      public bool Zaposlen { get; set; }
    }
}