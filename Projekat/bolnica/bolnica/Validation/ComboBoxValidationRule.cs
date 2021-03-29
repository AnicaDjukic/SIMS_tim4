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
    public class ComboBoxImeValidationRule : ValidationRule
    {
        private FileStoragePacijenti storage { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value as string != null)
            {
                storage = new FileStoragePacijenti();

                List<Pacijent> sviPacijenti = new List<Pacijent>();
                sviPacijenti = storage.GetAll();
                for (int i = 0; i < sviPacijenti.Count; i++)
                {
                    if (sviPacijenti[i].Ime.Equals(value as string))
                    {
                        return new ValidationResult(true, null);
                    }
                }
                return new ValidationResult(false, "Ne postoji pacijent sa tim imenom");
            }
            return new ValidationResult(false, "Popunite");

        }



    }

    public class ComboBoxPrezimeValidationRule : ValidationRule
    {
        private FileStoragePacijenti storage { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value as string != null)
            {
                storage = new FileStoragePacijenti();

                List<Pacijent> sviPacijenti = new List<Pacijent>();
                sviPacijenti = storage.GetAll();

                for (int i = 0; i < sviPacijenti.Count; i++)
                {
                    if (sviPacijenti[i].Prezime.Equals(value as string))
                    {
                        return new ValidationResult(true, null);
                    }
                }
                return new ValidationResult(false, "Ne postoji pacijent sa tim prezimenom");
            }
            return new ValidationResult(false, "Popunite");
        }



    }

    public class ComboBoxJmbgValidationRule : ValidationRule
    {
        private FileStoragePacijenti storage { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value as string != null)
            {
                storage = new FileStoragePacijenti();

                List<Pacijent> sviPacijenti = new List<Pacijent>();
                sviPacijenti = storage.GetAll();


                for (int i = 0; i < sviPacijenti.Count; i++)
                {
                    if (sviPacijenti[i].Jmbg.Equals(value as string))
                    {
                        return new ValidationResult(true, null);
                    }
                }
                return new ValidationResult(false, "Ne postoji pacijent sa tim jmbg-om");
            }
            return new ValidationResult(false, "Popunite");
        }



    }

    public class ComboBoxProstorijaValidationRule : ValidationRule
    {
        private FileStorageProstorija storage { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value as string != null)
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
                return new ValidationResult(false, "Ne postoji slobodna prostorija sa tim brojem");
            }
            return new ValidationResult(false, "Popunite");
        }



    }

    public class ComboBoxVremeValidationRule : ValidationRule
    {
        private FileStorageProstorija storage { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value as string != null)
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
                return new ValidationResult(false, "Nije moguce zakazati termin u to vrijeme");




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



}




