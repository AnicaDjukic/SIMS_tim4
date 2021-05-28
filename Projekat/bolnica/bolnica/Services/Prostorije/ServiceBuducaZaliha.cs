using Bolnica.Model.Prostorije;
using Bolnica.Repository.Prostorije;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Services.Prostorije
{
    public class ServiceBuducaZaliha
    {
        private RepositoryBuducaZaliha repository;

        public ServiceBuducaZaliha()
        {
            repository = new FileRepositoryBuducaZaliha();
        }
        public List<BuducaZaliha> DobaviBuduceZalihe(Oprema opremaZaSkladistenje)
        {
            List<BuducaZaliha> buduceZalihe = new List<BuducaZaliha>();
            if (repository.GetAll() != null)
            {
                foreach (BuducaZaliha bz in repository.GetAll())
                {
                    if (bz.Oprema.Sifra == opremaZaSkladistenje.Sifra && bz.Datum <= DateTime.Now.Date)
                    {
                        buduceZalihe.Add(bz);
                        repository.Delete(bz);
                    }
                }               
            }

            return buduceZalihe;
        }
    }
}
