using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Forms.Upravnik.FactoryMethod
{
    public class CRUDOperatorProstorije : CRUDOperator<Prostorija>
    {
        public override IOperacije<Prostorija> FactoryMethod()
        {
            return new OperacijeProstorije();
        }
    }
}
