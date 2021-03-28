namespace Model.Korisnici
{
   public class Zaposleni : Osoba
   {
      public int Mbr { get; set; }
      public double Plata { get; set; }
      public int BrojSlobodnihDana { get; set; }
      public int GodineStaza { get; set; }
      public bool Zaposlen { get; set; }
    }
}