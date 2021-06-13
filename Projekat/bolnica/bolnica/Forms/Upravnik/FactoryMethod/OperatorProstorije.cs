using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Forms.Upravnik.FactoryMethod
{
    public class OperatorProstorije : Operator<Prostorija, string>
    {
        public override IOperacijeNadEntitetima<Prostorija, string> FactoryMethod()
        {
            return new OperacijeNadProstorijom();
        }
    }
}
