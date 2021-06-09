using Bolnica.Localization;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Bolnica.Validation
{
    public class ValidationRuleNaziv : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex rgxNaziv = new Regex(@"^[A-Za-z]+$");
            var naziv = value as string;
            if (rgxNaziv.IsMatch(naziv.Trim()))
            {
                return new ValidationResult(true, null);
            }
            return new ValidationResult(false, LocalizedStrings.Instance["Mora sadržati samo slova!"]);
        }
    }

    public class ValidationRuleId : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex rgxNaziv = new Regex(@"^[0-9]+$");
            var naziv = value as string;
            if (rgxNaziv.IsMatch(naziv.Trim()))
            {
                return new ValidationResult(true, null);
            }
            return new ValidationResult(false, LocalizedStrings.Instance["Mora sadržati samo brojeve!"]);
        }
    }

    public class ValidationRuleKolicina : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            Regex rgxNaziv = new Regex(@"^[0-9]+$");
            var naziv = value as string;
            if (rgxNaziv.IsMatch(naziv.Trim()))
            {
                return new ValidationResult(true, null);
            }
            return new ValidationResult(false, LocalizedStrings.Instance["Mora biti brojčana vrednost!"]);
        }
    }

    public class ValidationRuleProizvodjac : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex rgxNaziv = new Regex(@"^[A-Z]+[a-zA-Z]*[0-9]*$");
            var naziv = value as string;
            if (rgxNaziv.IsMatch(naziv.Trim()))
            {
                return new ValidationResult(true, null);
            }
            return new ValidationResult(false, LocalizedStrings.Instance["Mora početi velikim slovom!"]);
        }
    }

    public class ValidationRuleKvadratura : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex rgxNaziv = new Regex(@"^[0-9]+.?[0-9]+$");
            var naziv = value as string;
            if (rgxNaziv.IsMatch(naziv.Trim()))
            {
                return new ValidationResult(true, null);
            }
            return new ValidationResult(false, LocalizedStrings.Instance["Mora biti brojčana vrednost!"]);
        }
    }
}
