using System;
using System.Collections.Generic;
using System.Text;
using Model.Prostorije;
using Newtonsoft.Json;

namespace Bolnica.Model.Prostorije
{
    public class Oprema
    {
        public string Sifra { get; set; }
        public string Naziv { get; set; }
        public bool ShouldSerializeNaziv()
        {
            
            return FileStorageZaliha.serializeOprema;
        }
        public TipOpreme TipOpreme { get; set; }
        public bool ShouldSerializeTipOpreme()
        {

            return FileStorageZaliha.serializeOprema;
        }
        public int Kolicina { get; set; }
        public bool ShouldSerializeKolicina()
        {

            return FileStorageZaliha.serializeOprema;
        }

    }
}
