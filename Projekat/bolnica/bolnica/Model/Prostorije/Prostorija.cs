using System;

namespace Model.Prostorije
{
    public class Prostorija
    {
        public int BrojProstorije { get; set; }
        public int Sprat { get; set; }
        public double Kvadratura { get; set; }
        public TipProstorije TipProstorije { get; set; }
        public bool Zauzeta { get; set; }
   }
}