using Model.Pacijenti;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Controller.Sekretar
{
    public interface IBracniStatusMehanizam
    {
        public BracniStatus PostaviPoljeBracniStatusPacijenta();
    }
}
