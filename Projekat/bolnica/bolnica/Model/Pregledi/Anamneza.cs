using Bolnica.Model.Pregledi;
using System.Collections.Generic;

namespace Model.Pregledi
{
   public class Anamneza
   {
        public Anamneza()
        {
            this.Recept = new List<Recept>();
            this.Beleska = new Beleska();
        }

        public int Id { get; set; }

        public string Simptomi { get; set; }
        public bool ShouldSerializeSimptomi()
        {
            return FileStorageAnamneza.serializeAnamneza;
        }

        public string Dijagnoza { get; set; }
        public bool ShouldSerializeDijagnoza()
        {
            return FileStorageAnamneza.serializeAnamneza;
        }

        public List<Recept> Recept { get; set; }
        public bool ShouldSerializeRecept()
        {
            return FileStorageAnamneza.serializeAnamneza;
        }

        public Beleska Beleska { get; set; }
        public bool ShouldSerializeBeleska()
        {
            return FileStorageAnamneza.serializeAnamneza;
        }

        public Anamneza(string Simptomi, string Dijagnoza)
        {
            this.Recept = new List<Recept>();
            this.Simptomi = Simptomi;
            this.Dijagnoza = Dijagnoza;
        }
    }
}

