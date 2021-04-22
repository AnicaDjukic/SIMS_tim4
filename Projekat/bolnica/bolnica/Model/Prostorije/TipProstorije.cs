using Bolnica.Model.Prostorije;
using System.ComponentModel;

namespace Model.Prostorije
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum TipProstorije
    {
        [Description("Sala za preglede")]
        salaZaPreglede,
        [Description("Operaciona sala")]
        operacionaSala,
        [Description("Bolnička soba")]
        bolnickaSoba
    }
}