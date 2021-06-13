using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Forms.Upravnik.FactoryMethod
{
    class CRUDOperatorLeka : CRUDOperator<Lek>
    {
        public override IOperacije<Lek> FactoryMethod()
        {
            return new OperacijeLeka();
        }
    }
}
