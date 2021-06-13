using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Model.Korisnici
{
    public class Smena
    {
        public int Id { get; set; }
        public PodrazumevanaSmena PodrazumevanaSmena { get; set; }
        public bool ShouldSerializePodrazumevanaSmena()
        {
            return FileRepositoryLekar.serializeSmena;
        }
        public DateTime PocetakSmene { get; set; }
        public bool ShouldSerializePocetakSmene()
        {
            return FileRepositoryLekar.serializeSmena;
        }
        public DateTime KrajSmene { get; set; }
        public bool ShouldSerializeKrajSmene()
        {
            return FileRepositoryLekar.serializeSmena;
        }
    }
}
