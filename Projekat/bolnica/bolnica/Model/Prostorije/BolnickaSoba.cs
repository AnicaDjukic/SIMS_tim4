namespace Model.Prostorije
{
    public class BolnickaSoba : Prostorija
    {

        public int UkBrojKreveta { get; set; }
        public bool ShouldSerializeUkBrojKreveta()
        {

            return FileStorageZaliha.serializeProstorija;
        }
        public int BrojSlobodnihKreveta { get; set; }
        public bool ShouldSerializeBrojSlobodnihKreveta()
        {

            return FileStorageZaliha.serializeProstorija;
        }

        public BolnickaSoba(): base() { }
    }
}