using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.DTO.Sekretar
{
    public class SastojakDTO
    {
        public int Id { get; set; }
        public string Naziv { get; set; }

        public SastojakDTO()
        {
        }

        public SastojakDTO(int id, string naziv)
        {
            Id = id;
            Naziv = naziv;
        }
    }
}
