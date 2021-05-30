using Bolnica.Repository.Pregledi;
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
            return FileRepositoryBeleska.serializeBeleska;
        }

        public bool Podsetnik { get; set; }
        public bool ShouldSerializePodsetnik()
        {
            return FileRepositoryBeleska.serializeBeleska;
        }

        public TimeSpan Vreme { get; set; }
        public bool ShouldSerializeVreme()
        {
            return FileRepositoryBeleska.serializeBeleska;

        }

        public DateTime DatumPrekida { get; set; }
        public bool ShouldSerializeDatumPrekida()
        {
            return FileRepositoryBeleska.serializeBeleska;
        }

        public bool Prikazana { get; set; }
        public bool ShouldSerializePrikazana()
        {
            return FileRepositoryBeleska.serializeBeleska;
        }
    }
}
