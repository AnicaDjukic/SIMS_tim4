using System;

namespace Model.Pacijenti
{
    public class ZdravstveniKarton
    {
        private int brojKartona;
        private string zanimanje;
        private BracniStatus bracniStatus;
        private bool osiguranje;

        public int BrojKartona { get; set; }

        public string Zanimanje { get; set; }
        public BracniStatus BracniStatus { get; set; }
        public bool Osiguranje { get; set; }
    }
}