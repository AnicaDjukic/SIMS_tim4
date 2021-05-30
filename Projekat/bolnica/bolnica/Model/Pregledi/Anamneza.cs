using Bolnica.Model.Pregledi;
using Bolnica.Repository.Pregledi;
using System.Collections.Generic;

namespace Model.Pregledi
{
   public class Anamneza
   {
        public Anamneza()
        {
            this.Recept = new List<Recept>();
        }
        public int Id { get; set; }
        public string Simptomi { get; set; }
        public bool ShouldSerializeSimptomi()
        {

            return FileRepositoryAnamneza.serializeAnamneza;
        }

        public string Dijagnoza { get; set; }

        public bool ShouldSerializeDijagnoza()
        {

            return FileRepositoryAnamneza.serializeAnamneza;
        }
        public List<Recept> Recept
        {
            get; set;
        }

        public bool ShouldSerializeRecept()
        {

            return FileRepositoryAnamneza.serializeAnamneza;
        }

        public Anamneza(string Simptomi, string Dijagnoza)
        {
            this.Recept = new List<Recept>();
            this.Simptomi = Simptomi;
            this.Dijagnoza = Dijagnoza;
        }

    }
}

