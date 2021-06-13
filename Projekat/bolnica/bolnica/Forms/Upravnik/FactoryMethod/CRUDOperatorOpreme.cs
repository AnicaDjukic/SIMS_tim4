using Bolnica.Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Forms.Upravnik.FactoryMethod
{
    public class CRUDOperatorOpreme : CRUDOperator<Oprema>
    {
        public override IOperacije<Oprema> FactoryMethod()
        {
            return new OperacijeOpreme();
        }
    }
}
