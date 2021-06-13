using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Forms.Upravnik.FactoryMethod
{
    class OperatorLeka : Operator<Lek, int>
    {
        public override IOperacijeNadEntitetima<Lek, int> FactoryMethod()
        {
            return new OperacijeNadLekom();
        }
    }
}
