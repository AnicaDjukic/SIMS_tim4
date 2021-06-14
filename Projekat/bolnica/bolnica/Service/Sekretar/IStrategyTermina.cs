using Bolnica.DTO.Sekretar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Service.Sekretar
{
    public interface IStrategyTermina
    {
        public void IzvrsiAlgoritam(GodisnjiDTO godisnji, int daniNaGodisnjem);
    }
}
