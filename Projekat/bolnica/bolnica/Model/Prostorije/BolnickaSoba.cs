using Bolnica.Repository.Prostorije;

namespace Model.Prostorije
{
    public class BolnickaSoba : Prostorija
    {

        public int UkBrojKreveta { get; set; }
        public bool ShouldSerializeUkBrojKreveta()
        {

            return FileRepositoryZaliha.serializeProstorija;
        }
        public int BrojSlobodnihKreveta { get; set; }
        public bool ShouldSerializeBrojSlobodnihKreveta()
        {

            return FileRepositoryZaliha.serializeProstorija;
        }

        public BolnickaSoba(): base() { }
    }
}