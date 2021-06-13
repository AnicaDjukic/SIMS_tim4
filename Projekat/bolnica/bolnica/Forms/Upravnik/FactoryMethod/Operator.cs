using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Forms.Upravnik.FactoryMethod
{
    public abstract class Operator<T, ID>
    {
        public abstract IOperacije<T, ID> FactoryMethod();
        
        public void OperacijaDodavanja()
        {
            var product = FactoryMethod();
            product.Dodavanje();
        }

        public void OperacijaPrikazivanja(ID id)
        {
            var product = FactoryMethod();
            product.Prikazivanje(id);
        }

        public void OperacijaIzmene(ID id)
        {
            var product = FactoryMethod();
            product.Izmena(id);
        }

        public void OperacijaBrisanja(T zaBrisanje)
        {
            var product = FactoryMethod();
            product.Brisanje(zaBrisanje);
        }

    }
}
