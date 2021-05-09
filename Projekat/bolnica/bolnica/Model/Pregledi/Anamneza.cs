using Bolnica.Model.Pregledi;
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

            return FileStorageAnamneza.serializeAnamneza;
        }

        public string Dijagnoza { get; set; }

        public bool ShouldSerializeDijagnoza()
        {

            return FileStorageAnamneza.serializeAnamneza;
        }

        /// <summary>
        /// Property for collection of Recept
        /// </summary>
        /// <pdGenerated>Default opposite class collection property</pdGenerated>
        public List<Recept> Recept
        {
            get; set;
        }

        public bool ShouldSerializeRecept()
        {

            return FileStorageAnamneza.serializeAnamneza;
        }

    }
}

