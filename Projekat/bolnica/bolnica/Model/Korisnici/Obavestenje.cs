using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Model.Korisnici
{
    public class Obavestenje
    {
        private int id;
        private DateTime datum;
        private string sadrzaj;
        private string naslov;
        private bool obrisan;

        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public string Sadrzaj { get; set; }
        public string Naslov { get; set; }
        public bool Obrisan { get; set; }
    }
}
