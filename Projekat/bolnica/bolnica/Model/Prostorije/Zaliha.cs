using Model.Prostorije;
using Newtonsoft.Json;

namespace Bolnica.Model.Prostorije
{
    public class Zaliha
    {
        public int Id { get; set; }
        public int Kolicina { get; set; }
        public Oprema Oprema { get; set; }
        public Prostorija Prostorija { get; set; }
        
    }
}
