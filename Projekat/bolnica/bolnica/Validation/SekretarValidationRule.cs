using Model.Korisnici;
using Model.Pacijenti;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Bolnica.Validation
{
    public class NameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                Regex rgxIme = new Regex(@"^[A-Za-z]+$");
                var s = value as string;
                if (rgxIme.IsMatch(s) && s != "")
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "Nije uneto ispravno ime");
            }
            catch
            {
                return new ValidationResult(false, "Desila se nepoznata greška");
            }
        }
    }

    public class SurnameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                Regex rgxIme = new Regex(@"^[A-Za-z]+$");
                var s = value as string;
                if (rgxIme.IsMatch(s) && s != "")
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "Nije uneto ispravno prezime");
            }
            catch
            {
                return new ValidationResult(false, "Desila se nepoznata greška");
            }
        }
    }

    public class JmbgLengthValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                Regex rgxJmbg = new Regex(@"^[0-9]{13}$");
                var s = value as string;
                if (rgxJmbg.IsMatch(s))
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "JMBG mora da se sastoji od 13 cifara");
            }
            catch
            {
                return new ValidationResult(false, "Desila se nepoznata greška");
            }
        }
    }

    public class JmbgZauzetValidationRule : ValidationRule
    {
        FileRepositoryPacijent storage = new FileRepositoryPacijent();
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                List<Pacijent> pacijenti = storage.GetAll();
                var s = value as string;

                foreach (Pacijent p in pacijenti)
                {
                    if (String.Equals(p.Jmbg, s))
                    {
                        return new ValidationResult(false, "Pacijent sa unetim JMBG-om već postoji");
                    }
                }
                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Desila se nepoznata greška");
            }
        }
    }

    public class LivingAddressValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                if (s != "")
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "Nije uneta adresa stanovanja");
            }
            catch
            {
                return new ValidationResult(false, "Desila se nepoznata greška");
            }
        }
    }

    public class PhoneNumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                Regex rgxBrojTelefona = new Regex(@"^\([0-9]{3}\)\s[0-9]{3}-[0-9]{3,4}$");
                var s = value as string;
                if (rgxBrojTelefona.IsMatch(s) && s != "")
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "Nepostojeći broj telefona");
            }
            catch
            {
                return new ValidationResult(false, "Desila se nepoznata greška");
            }
        }
    }

    public class EmailValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                bool isEmail = Regex.IsMatch(s, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                if (isEmail && s != "")
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "Nepostojeći email");
            }
            catch
            {
                return new ValidationResult(false, "Desila se nepoznata greška");
            }
        }
    }

    public class UsernameEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                if (s != "")
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "Nije uneto korisničko ime");
            }
            catch
            {
                return new ValidationResult(false, "Desila se nepoznata greška");
            }
        }
    }

    public class UsernameExistsValidationRule : ValidationRule
    {
        FileRepositoryPacijent storage = new FileRepositoryPacijent();
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                List<Pacijent> pacijenti = storage.GetAll();
                var s = value as string;

                foreach (Pacijent p in pacijenti)
                {
                    if (String.Equals(s, p.KorisnickoIme))
                    {
                        return new ValidationResult(false, "Korisničko ime već postoji");
                    }
                }
                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Desila se nepoznata greška");
            }
        }
    }

    public class PasswordEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                if (s != "")
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "Nije uneta lozinka");
            }
            catch
            {
                return new ValidationResult(false, "Desila se nepoznata greška");
            }
        }
    }

    public class JobValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                if (s != "")
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "Nije uneto zanimanje");
            }
            catch
            {
                return new ValidationResult(false, "Desila se nepoznata greška");
            }
        }
    }

    public class IdCardValueValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int d;

            try
            {
                d = int.Parse((string)value);
            }
            catch (FormatException)
            {
                return new ValidationResult(false, "Broj kartona mora biti pozitivan celi broj");
            }
            catch (OverflowException)
            {
                return new ValidationResult(false, "Unet je preveliki broj kartona");
            }

            if (d <= 0)
            {
                return new ValidationResult(false, "Broj kartona mora biti pozitivan celi broj");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }

    public class IdCardExistsValidationRule : ValidationRule
    {
        FileRepositoryPacijent storage = new FileRepositoryPacijent();
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                List<Pacijent> pacijenti = storage.GetAll();
                var s = value as string;

                foreach (Pacijent p in pacijenti)
                {
                    if (!p.Guest && p.ZdravstveniKarton.BrojKartona.ToString() == s)
                    {
                        return new ValidationResult(false, "Pacijent sa unetim brojem kartona već postoji");
                    }
                }
                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Desila se nepoznata greška");
            }

        }
    }

    public class DateTimeEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(value != null)
                return new ValidationResult(true, null);
            else
                return new ValidationResult(false, "Nije unet datum rođenja");
        }
    }

    public class DateTimeValidValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var d = (DateTime)value;

            if (DateTime.Compare(d, DateTime.Now) > 0 || d.Year < 1900)
            {
                return new ValidationResult(false, "Neispravan datum rođenja");
            }
            else
                return new ValidationResult(true, null);
        }
    }
}
