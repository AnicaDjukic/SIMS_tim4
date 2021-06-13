using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Forms.Upravnik.FactoryMethod
{
    class OperatorLeka : Operator<Lek, int>
    {
        public override IOperacije<Lek, int> FactoryMethod()
        {
            return new OperacijeLeka();
        }
    }
}
