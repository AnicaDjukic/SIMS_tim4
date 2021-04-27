using Bolnica.Model.Prostorije;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Bolnica.Model.Pregledi
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum StatusLeka
    {
        [Description("Odobren")]
        odobren,
        [Description("Odbijen")]
        odbijen,
        [Description("Čeka validaciju")]
        cekaValidaciju
    }
}
