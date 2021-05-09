using Bolnica.Forms.Upravnik;
using Bolnica.Model.Prostorije;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace Bolnica.Validation
{
    public class DateValidationRenoviranje : ValidationRule
    {
        private FileStorageRenoviranje storageRenoviranje;
        private FileStoragePregledi StoragePregledi;
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(value as string != "" && value != null)
            {
                storageRenoviranje = new FileStorageRenoviranje();
                StoragePregledi = new FileStoragePregledi();

                List<Renoviranje> renoviranja = storageRenoviranje.GetAll();
                List<Pregled> pregledi = StoragePregledi.GetAllPregledi();
                List<Operacija> operacije = StoragePregledi.GetAllOperacije();

                foreach(Renoviranje r in renoviranja)
                {
                    if(r.Prostorija.BrojProstorije == FormRenoviranje.novoRenoviranje.Prostorija.BrojProstorije)
                    {
                        if ((DateTime)value >= r.PocetakRenoviranja && (DateTime)value <= r.KrajRenoviranja)
                        {
                            return new ValidationResult(false, "Datum nije slobodan");
                        }
                    }

                }

                foreach(Pregled p in pregledi)
                {
                    if(p.Prostorija.BrojProstorije == FormRenoviranje.novoRenoviranje.Prostorija.BrojProstorije)
                    {
                        if(((DateTime)value).Date == p.Datum.Date)
                        {
                            return new ValidationResult(false, "Datum nije slobodan");
                        }
                    }
                }

                foreach (Operacija o in operacije)
                {
                    if (o.Prostorija.BrojProstorije == FormRenoviranje.novoRenoviranje.Prostorija.BrojProstorije)
                    {
                        if (((DateTime)value).Date == o.Datum.Date)
                        {
                            return new ValidationResult(false, "Datum nije slobodan");
                        }
                    }
                }
                return new ValidationResult(true, null);
            }
            return new ValidationResult(false, "Popunite");
        }
    }
}
