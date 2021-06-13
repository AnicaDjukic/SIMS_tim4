using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Forms.Upravnik.FactoryMethod
{
    public abstract class CRUDOperator<T>
    {
        public abstract IOperacije<T> FactoryMethod();

        public void OperacijaBrisanja(T zaBrisanje)
        {
            // Call the factory method to create a Product object.
            var product = FactoryMethod();
            // Now, use the product.
            product.Brisanje(zaBrisanje);
        }
    }
}
