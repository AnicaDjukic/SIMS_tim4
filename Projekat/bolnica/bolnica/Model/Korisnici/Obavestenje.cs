using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Model.Korisnici
{
    public class Obavestenje
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public string Sadrzaj { get; set; }
        public string Naslov { get; set; }
        public bool Obrisan { get; set; }
        public List<string> KorisnickaImena { get; set; }

        public Obavestenje()
        {
            KorisnickaImena = new List<string>();
        }
    }
}
