using Model.Pacijenti;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Controller.Sekretar
{
    public class BracniStatusUdovac : IBracniStatusMehanizam
    {
        public BracniStatus PostaviPoljeBracniStatusPacijenta()
        {
            return BracniStatus.udovac_udovica;
        }
    }
}
