using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Bolnica.Model.Prostorije
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum TipOpreme
    {
        [Description("Statička")]
        staticka,
        [Description("Dinamička")]
        dinamicka
    }
}
