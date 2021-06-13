using Model.Pacijenti;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Controller.Sekretar
{
    public class BracniStatusNeozenjen : IBracniStatusMehanizam
    {
        public BracniStatus PostaviPoljeBracniStatusPacijenta()
        {
            return BracniStatus.neozenjen_neudata;
        }
    }
}
