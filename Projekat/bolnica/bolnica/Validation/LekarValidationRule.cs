using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Model.Korisnici;
using Model.Pacijenti;
using Model.Prostorije;

namespace Bolnica.Validation
{
   

    public class ComboBoxPrezimeValidationRule : ValidationRule
    {
        private FileStoragePacijenti storage {  get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value as string != "" && value != null)
            {
                storage = new FileStoragePacijenti();

                List<Pacijent> sviPacijenti = new List<Pacijent>();
                sviPacijenti = storage.GetAll();

                for (int i = 0; i < sviPacijenti.Count; i++)
                {
                    string s;
                    s = sviPacijenti[i].Prezime + ' ' + sviPacijenti[i].Ime + ' ' + sviPacijenti[i].Jmbg;
                    if (s.Equals(value as string))
                    {
                        return new ValidationResult(true, null);
                    }
                }
                return new ValidationResult(false, "Ne postoji pacijent");
            }
            return new ValidationResult(false, "Popunite");
        }



    }

    

    public class ComboBoxProstorijaValidationRule : ValidationRule
    {
        private FileStorageProstorija storage { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value as string != "" && value != null)
            {
                storage = new FileStorageProstorija();

                List<Prostorija> sveProstorije = new List<Prostorija>();
                sveProstorije = storage.GetAllProstorije();


                for (int i = 0; i < sveProstorije.Count; i++)
                {
                    if (sveProstorije[i].BrojProstorije.ToString().Equals(value as string) && sveProstorije[i].TipProstorije != TipProstorije.bolnickaSoba)
                    {
                        return new ValidationResult(true, null);
                    }
                }
                return new ValidationResult(false, "Ne postoji slobodna prostorija");
            }
            return new ValidationResult(false, "Popunite");
        }



    }

    public class ComboBoxVremeValidationRule : ValidationRule
    {
        private FileStorageProstorija storage { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value as string != "" && value != null)
            {

                for (int vre = 0; vre < 24; vre++)
                {
                    for (int min = 0; min < 59;)
                    {
                        TimeSpan ts = new TimeSpan(vre, min, 0);
                        min = min + 15;
                        if (ts.ToString().Equals(value))
                        {
                            return new ValidationResult(true, null);
                        }
                    }

                }
                return new ValidationResult(false, "Nije moguce u to vrijeme");




            }
            return new ValidationResult(false, "Popunite");
        }
          
    }

    public class ComboBoxTipOperacijeValidationRule : ValidationRule
    {
        
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value as string != null)
            {
                return new ValidationResult(true, null);
            }
            return new ValidationResult(false, "Popunite");
        }

    }

    public class TextBoxTrajanjeValidationRule : ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                int r;
                if (int.TryParse(value as string, out r))
                {
                    if (r > 300)
                    {
                        return new ValidationResult(false, "MAX(300)");
                    }
                    if (r < 30)
                    {
                        return new ValidationResult(false, "MIN(30)");
                    }
                    return new ValidationResult(true, null);
                }
                else
                {
                    return new ValidationResult(false, "Unesite cijeli broj");
                }
            }
            catch {
                return new ValidationResult(false, "Nepoznata greska se desila"); 
            }
        }

    }

    public class DatePickerDatumValidationRule : ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null)
            {
                return new ValidationResult(true, null);
            }
            return new ValidationResult(false, "Popunite");
        }

    }

}




