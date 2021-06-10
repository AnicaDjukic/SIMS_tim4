using Model.Pacijenti;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.DTO
{
    public class ZdravstveniKartonDTO
    {
        public int BrojKartona { get; set; }
        public string Zanimanje { get; set; }
        public BracniStatus BracniStatus { get; set; }
        public bool Osiguranje { get; set; }

        public ZdravstveniKartonDTO() 
        { 
        }

        public ZdravstveniKartonDTO(int brojKartona, string zanimanje, BracniStatus bracniStatus, bool osiguranje)
        {
            this.BrojKartona = brojKartona;
            this.Zanimanje = zanimanje;
            this.BracniStatus = bracniStatus;
            this.Osiguranje = osiguranje;
        }
    }
}
