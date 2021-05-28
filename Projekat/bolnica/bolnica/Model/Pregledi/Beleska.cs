using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Pregledi
{
    public class Beleska
    {
        public Beleska()
        {
            this.Id = -1;
        }

        public int Id { get; set; }

        public string Zabeleska { get; set; }
        public bool ShouldSerializeZabeleska()
        {
            return FileStorageBeleska.serializeBeleska;
        }

        public bool Podsetnik { get; set; }
        public bool ShouldSerializePodsetnik()
        {
            return FileStorageBeleska.serializeBeleska;
        }

        public TimeSpan Vreme { get; set; }
        public bool ShouldSerializeVreme()
        {
            return FileStorageBeleska.serializeBeleska;

        }

        public DateTime DatumPrekida { get; set; }
        public bool ShouldSerializeDatumPrekida()
        {
            return FileStorageBeleska.serializeBeleska;
        }

        public bool Prikazana { get; set; }
        public bool ShouldSerializePrikazana()
        {
            return FileStorageBeleska.serializeBeleska;
        }
    }
}
