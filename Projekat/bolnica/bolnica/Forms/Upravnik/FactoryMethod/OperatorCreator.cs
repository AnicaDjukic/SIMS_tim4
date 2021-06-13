using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Forms.Upravnik.FactoryMethod
{
    public abstract class OperatorCreator<T, ID>
    {
        public abstract IOperacijeNadEntitetima<T, ID> FactoryMethod();
        
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

        public List<T> OperacijaPretrage(string text)
        {
            var product = FactoryMethod();
            return product.Pretraga(text);
        }
    }
}
