using Bolnica.Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Forms.Upravnik.FactoryMethod
{
    public class OperatorOpreme : Operator<Oprema, string>
    {
        public override IOperacijeNadEntitetima<Oprema, string> FactoryMethod()
        {
            return new OperacijeNadOpremom();
        }
    }
}
