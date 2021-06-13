using Model.Pacijenti;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Model.Pregledi
{
    public class Sastojak
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public bool ShouldSerializeNaziv()
        {

            return FileRepositoryPacijent.serializeAlergeni;
        }
    }
}
