using Model.Pacijenti;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Controller.Sekretar
{
    public class BracniStatusOzenjen : IBracniStatusMehanizam
    {
        public BracniStatus PostaviPoljeBracniStatusPacijenta()
        {
            return BracniStatus.ozenjen_udata;
        }
    }
}
