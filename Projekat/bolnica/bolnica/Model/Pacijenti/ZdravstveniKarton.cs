namespace Model.Pacijenti
{
    public class ZdravstveniKarton
    {
        public int BrojKartona { get; set; }
        public string Zanimanje { get; set; }
        public bool ShouldSerializeZanimanje()
        { 
            return FileRepositoryPacijent.serializeZdravstveniKarton;
        }
        public BracniStatus BracniStatus { get; set; }
        public bool ShouldSerializeBracniStatus()
        {
            return FileRepositoryPacijent.serializeZdravstveniKarton;
        }
        public bool Osiguranje { get; set; }
        public bool ShouldSerializeOsiguranje()
        {
            return FileRepositoryPacijent.serializeZdravstveniKarton;
        }
    }
}