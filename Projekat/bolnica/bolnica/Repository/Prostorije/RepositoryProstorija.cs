using System;
using System.Collections.Generic;
using System.Text;
using Bolnica.Repository.Korisnici;
using Model.Prostorije;

namespace Bolnica.Repository.Prostorije
{
    public interface RepositoryProstorija : IRepository<Prostorija, string>
    {
        public void DeleteById(string brojProstorije);
    }
}
