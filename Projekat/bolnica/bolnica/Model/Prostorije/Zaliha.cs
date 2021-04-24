using Model.Prostorije;
using Newtonsoft.Json;

namespace Bolnica.Model.Prostorije
{
    public class Zaliha
    {
        public int Kolicina { get; set; }
        public string SifraOpreme { get; set; }
        public string BrojProstorije { get; set; }

        [JsonIgnore]
        public Oprema Oprema { get; set; }
        [JsonIgnore]
        public Prostorija Prostorija { get; set; }
        
    }
}
