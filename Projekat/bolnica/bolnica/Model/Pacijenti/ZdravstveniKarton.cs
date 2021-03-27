namespace Model.Pacijenti
{
    public class ZdravstveniKarton
    {
        public int BrojKartona { get; set; }

        public string Zanimanje { get; set; }
        public BracniStatus BracniStatus { get; set; }
        public bool Osiguranje { get; set; }
    }
}