using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Forms.Upravnik.FactoryMethod
{
    public class OperatorProstorije : Operator<Prostorija, string>
    {
        public override IOperacije<Prostorija, string> FactoryMethod()
        {
            return new OperacijeProstorije();
        }
    }
}
