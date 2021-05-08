namespace Model.Prostorije
{
    public class Prostorija
    {
        public string BrojProstorije { get; set; }
        public int Sprat { get; set; }
        public bool ShouldSerializeSprat()
        {

            return FileStorageZaliha.serializeProstorija;
        }
        public double Kvadratura { get; set; }
        public bool ShouldSerializeKvadratura()
        {

            return FileStorageZaliha.serializeProstorija;
        }
        public TipProstorije TipProstorije { get; set; }
        public bool ShouldSerializeTipProstorije()
        {

            return FileStorageZaliha.serializeProstorija;
        }
        public bool Zauzeta { get; set; }
        public bool ShouldSerializeZauzeta()
        {

            return FileStorageZaliha.serializeProstorija;
        }
        public bool Obrisana { get; set; }
        public bool ShouldSerializeObrisana()
        {

            return FileStorageZaliha.serializeProstorija;
        }

        public Prostorija()
        {
            Obrisana = false;
        }
    }
}