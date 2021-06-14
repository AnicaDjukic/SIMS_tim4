using Bolnica.DTO.Sekretar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Service.Sekretar
{
    public class Context
    {
        private IStrategyTermina strategija;

        public Context() 
        {
        }

        public Context(IStrategyTermina strategy)
        {
            strategija = strategy;
        }

        public void PostaviStrategiju(IStrategyTermina strategy)
        {
            strategija = strategy;
        }

        public void IzvrsiPoslovnuLogiku(GodisnjiDTO godisnji, int daniNaGodisnjem) 
        {
            strategija.IzvrsiAlgoritam(godisnji, daniNaGodisnjem);
        }
    }
}
