using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.Repository.Pregledi;
using Bolnica.Repository.Prostorije;
using Model.Korisnici;
using Model.Pacijenti;
using Model.Pregledi;
using Model.Prostorije;

namespace Bolnica.Validation
{

    public class ComboBoxLekarValidationRule : ValidationRule
    {
        private FileRepositoryLekar storage { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value as string != "" && value != null)
            {
                storage = new FileRepositoryLekar();

                List<Lekar> sviLekari = new List<Lekar>();
                sviLekari = storage.GetAll();

                for (int i = 0; i < sviLekari.Count; i++)
                {
                    string s;
                    s = sviLekari[i].Prezime + ' ' + sviLekari[i].Ime + ' ' + sviLekari[i].Jmbg;
                    if (s.Equals(value as string))
                    {
                        return new ValidationResult(true, null);
                    }
                }
                return new ValidationResult(false, "Ne postoji lekar");
            }
            return new ValidationResult(false, "Popunite");
        }
    }

    public class ComboBoxSpecijalizacijaValidationRule : ValidationRule
    {
        private FileRepositoryLekar storage { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value as string != "" && value != null)
            {
                storage = new FileRepositoryLekar();

                List<Lekar> sviLekari = new List<Lekar>();
                sviLekari = storage.GetAll();

                for (int i = 0; i < sviLekari.Count; i++)
                {
                    string s;
                    s = sviLekari[i].Specijalizacija.OblastMedicine;
                    if (s.Equals(value as string))
                    {
                        return new ValidationResult(true, null);
                    }
                }
                return new ValidationResult(false, "Ne postoji specijalizacija");
            }
            return new ValidationResult(false, "Popunite");
        }
    }

    public class ComboBoxPrezimeValidationRule : ValidationRule
    {
        private FileRepositoryPacijent storage {  get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value as string != "" && value != null)
            {
                storage = new FileRepositoryPacijent();

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
        private FileRepositoryProstorija storage { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value as string != "" && value != null)
            {
                storage = new FileRepositoryProstorija();

                List<Prostorija> sveProstorije = new List<Prostorija>();
                sveProstorije = storage.GetAll();


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

    public class ComboBoxBolnickaSobaValidationRule : ValidationRule
    {
        private FileRepositoryBolnickaSoba storage { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value as string != "" && value != null)
            {
                storage = new FileRepositoryBolnickaSoba();

                List<BolnickaSoba> sveProstorije = new List<BolnickaSoba>();
                sveProstorije = storage.GetAll();


                for (int i = 0; i < sveProstorije.Count; i++)
                {
                    if (sveProstorije[i].BrojProstorije.ToString().Equals(value as string))
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
        private FileRepositoryProstorija storage { get; set; }
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

    public class ComboBoxLekValidationRule : ValidationRule
    {
        private FileRepositoryLek storage { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value as string != "" && value != null)
            {
                storage = new FileRepositoryLek();

                List<Lek> sviLekovi = new List<Lek>();
                sviLekovi = storage.GetAll();

                for (int i = 0; i < sviLekovi.Count; i++)
                {
                    string s;
                    s = sviLekovi[i].Naziv;
                    if (s.Equals(value as string))
                    {
                        return new ValidationResult(true, null);
                    }
                }
                return new ValidationResult(false, "Ne postoji lek");
            }
            return new ValidationResult(false, "Popunite");
        }
    }

    public class ComboBoxDozaValidationRule : ValidationRule
    {
        private FileRepositoryLek storage { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value as string != "" && value != null)
            {
                storage = new FileRepositoryLek();

                List<Lek> sviLekovi = new List<Lek>();
                sviLekovi = storage.GetAll();

                for (int i = 0; i < sviLekovi.Count; i++)
                {
                    string s;
                    s = sviLekovi[i].KolicinaUMg.ToString();
                    if (s.Equals(value as string))
                    {
                        return new ValidationResult(true, null);
                    }
                }
                return new ValidationResult(false, "Ne postoji doza");
            }
            return new ValidationResult(false, "Popunite");
        }
    }

    public class ComboBoxProizvodjacValidationRule : ValidationRule
    {
        private FileRepositoryLek storage { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value as string != "" && value != null)
            {
                storage = new FileRepositoryLek();

                List<Lek> sviLekovi = new List<Lek>();
                sviLekovi = storage.GetAll();

                for (int i = 0; i < sviLekovi.Count; i++)
                {
                    string s;
                    s = sviLekovi[i].Proizvodjac;
                    if (s.Equals(value as string))
                    {
                        return new ValidationResult(true, null);
                    }
                }
                return new ValidationResult(false, "Ne postoji proizvodjac");
            }
            return new ValidationResult(false, "Popunite");
        }
    }

    public class DatePickerDatumPrekidaValidationRule : ValidationRule
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

    public class DatePickerDatumIzdavanjaValidationRule : ValidationRule
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

    public class ComboBoxBrojKutijaValidationRule : ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                int r;
                if (int.TryParse(value as string, out r))
                {
                    if (r > 10)
                    {
                        return new ValidationResult(false, "MAX(10)");
                    }
                    if (r < 1)
                    {
                        return new ValidationResult(false, "MIN(1)");
                    }
                    return new ValidationResult(true, null);
                }
                else
                {
                    return new ValidationResult(false, "Unesite cijeli broj");
                }
            }
            catch
            {
                return new ValidationResult(false, "Nepoznata greska se desila");
            }
        }

    }

    public class ComboBoxVremeUzimanjaValidationRule : ValidationRule
    {
        private FileRepositoryProstorija storage { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value as string != "" && value != null)
            {
                
                for (int vre = 1; vre < 48; vre++)
                {
                    
                    if (vre.ToString().Equals(value))
                        {
                            return new ValidationResult(true, null);
                        }

                }
                return new ValidationResult(false, "Nije moguce u to vrijeme");




            }
            return new ValidationResult(false, "Popunite");
        }

    }

    public class TextBoxSimptomiValidationRule : ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value.ToString().Length > 2)
            {
                return new ValidationResult(true, null);
            }
            return new ValidationResult(false, "Popunite");
        }

    }

    public class TextBoxDijagnozaValidationRule : ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value.ToString().Length>2)
            {
                return new ValidationResult(true, null);
            }
            return new ValidationResult(false, "Popunite");
        }

    }

    public class IzmeniLekValidationRule : ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value.ToString().Length > 2)
            {
                return new ValidationResult(true, null);
            }
            return new ValidationResult(false, "Popunite");
        }

    }

}





