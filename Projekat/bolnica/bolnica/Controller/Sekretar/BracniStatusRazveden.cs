using Model.Pacijenti;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Controller.Sekretar
{
    public class BracniStatusRazveden : IBracniStatusMehanizam
    {
        public BracniStatus PostaviPoljeBracniStatusPacijenta()
        {
            return BracniStatus.razveden_razvedena;
        }
    }
}
