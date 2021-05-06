using Bolnica.Model.Prostorije;
using System.ComponentModel;

namespace Model.Pregledi
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum TipOperacije
   {

        [Description("Operacija I kategorije")]
        prvaKat,
        [Description("Operacija II kategorije")]
        drugaKat,
        [Description("Operacija III kategorije")]
        trecaKat
   }
}