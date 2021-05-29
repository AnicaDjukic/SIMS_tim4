using Bolnica.Repository.Korisnici;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Repository.Prostorije
{
    public interface RepositoryBolnickaSoba : Repository<BolnickaSoba, string>
    {
        public void DeleteById(string brojProstorije);
    }
}
