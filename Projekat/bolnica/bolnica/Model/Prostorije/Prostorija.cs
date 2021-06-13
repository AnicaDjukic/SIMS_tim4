using Bolnica.Repository.Prostorije;

namespace Model.Prostorije
{
    public class Prostorija
    {
        public string BrojProstorije { get; set; }
        public int Sprat { get; set; }
        public bool ShouldSerializeSprat()
        {

            return FileRepositoryZaliha.serializeProstorija;
        }
        public double Kvadratura { get; set; }
        public bool ShouldSerializeKvadratura()
        {

            return FileRepositoryZaliha.serializeProstorija;
        }
        public TipProstorije TipProstorije { get; set; }
        public bool ShouldSerializeTipProstorije()
        {

            return FileRepositoryZaliha.serializeProstorija;
        }
        public bool Zauzeta { get; set; }
        public bool ShouldSerializeZauzeta()
        {

            return FileRepositoryZaliha.serializeProstorija;
        }
        public bool Obrisana { get; set; }
        public bool ShouldSerializeObrisana()
        {

            return FileRepositoryZaliha.serializeProstorija;
        }

        public Prostorija()
        {
            Obrisana = false;
        }
    }
}