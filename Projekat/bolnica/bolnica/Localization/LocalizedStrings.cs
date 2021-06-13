using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using WPFLocalizeExtension.Engine;

namespace Bolnica.Localization
{
    public class LocalizedStrings
    {
        private LocalizedStrings()
        {

        }

        public static LocalizedStrings Instance { get; } = new LocalizedStrings();
        public object LocalizedDictionary { get; private set; }

        public void SetCulture(string cultureCode)
        {
            var newCulture = new CultureInfo(cultureCode);
            LocalizeDictionary.Instance.Culture = newCulture;
        }

        public string this[string key]
        {
            get
            {
                var result = LocalizeDictionary.Instance.GetLocalizedObject("Bolnica", "Strings", key, LocalizeDictionary.Instance.Culture);
                return result as string;
            }
        }
    }
}
