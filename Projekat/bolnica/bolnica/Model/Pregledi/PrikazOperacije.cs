using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Model.Pregledi
{
    public class PrikazOperacije: PrikazPregleda
    {
        private TipOperacije tipOperacije;




        public TipOperacije TipOperacije { get; set; }
    }
}
