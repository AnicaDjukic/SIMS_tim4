using Model.Korisnici;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Template
{
    public abstract class RacunajId
    {
        public int IzracunajId()
        {
            List<int> lista = DobijListu();
            int max = PronadjiNajveci(lista);
            int prviSlobodan = PronadjiPrviSlobodan(lista, max);
            return DobijId(max, prviSlobodan);
        }

        public abstract List<int> DobijListu();
        public int PronadjiNajveci(List<int> lista)
        {
            int max = 0;
            foreach (int id in lista)
            {
                if (id > max)
                {
                    max = id;
                }
            }
            return max;
        }
        public int PronadjiPrviSlobodan(List<int> lista, int max)
        {
            for (int i = 1; i < max; i++)
            {
                bool pronadjen = false;
                foreach (int antiTrol in lista)
                {
                    if (antiTrol.Equals(i))
                    {
                        pronadjen = true;
                        break;
                    }
                }
                if (!pronadjen)
                {
                    return i;
                }
            }
            return -1;
        }
        private int DobijId(int max, int prviSlobodan)
        {
            if (prviSlobodan > 0)
            {
                return prviSlobodan;
            }
            else
            {
                return max + 1;
            }
        }
    }
}
