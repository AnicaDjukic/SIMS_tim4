using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.DTO.Sekretar
{
    public class GodisnjiDTO
    {
        public DateTime PocetakGodisnjeg { get; set; }
        public DateTime KrajGodisnjeg { get; set; }
        public string KorisnickoImeLekara { get; set; }

        public GodisnjiDTO()
        {
        }

        public GodisnjiDTO(DateTime pocetakGodisnjeg, DateTime krajGodisnjeg, string korisnickoImeLekara)
        {
            PocetakGodisnjeg = pocetakGodisnjeg;
            KrajGodisnjeg = krajGodisnjeg;
            KorisnickoImeLekara = korisnickoImeLekara;
        }
    }
}
