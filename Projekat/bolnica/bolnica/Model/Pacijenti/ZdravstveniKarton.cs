namespace Model.Pacijenti
{
    public class ZdravstveniKarton
    {
        public int BrojKartona { get; set; }

        public string Zanimanje { get; set; }
        public bool ShouldSerializeZanimanje()
        {

            return FileStoragePacijenti.serializeKarton;
        }
        public BracniStatus BracniStatus { get; set; }
        public bool ShouldSerializeBracniStatus()
        {

            return FileStoragePacijenti.serializeKarton;
        }
        public bool Osiguranje { get; set; }
        public bool ShouldSerializeOsiguranje()
        {

            return FileStoragePacijenti.serializeKarton;
        }
    }
}