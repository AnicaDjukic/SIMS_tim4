﻿using Bolnica.Model.Pregledi;
using Bolnica.Service;
using Model.Korisnici;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Controller
{
    public class NasiPredloziController
    {
        private NasiPredloziService service = new NasiPredloziService();

        public void PopuniPredlozeneTermine(Pacijent pacijent, DateTime datum, int sat, int minut, Lekar lekar)
        {
            service.PopuniPredlozeneTermine(pacijent, datum, sat, minut, lekar);
        }

        public void OdaberiTermin(PrikazPregleda prikazPregleda)
        {
            service.OdaberiTermin(prikazPregleda);
        }
    }
}
