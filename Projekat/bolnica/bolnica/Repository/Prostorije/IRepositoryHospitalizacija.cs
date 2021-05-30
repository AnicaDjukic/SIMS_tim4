using Bolnica.Model.Pregledi;
using Bolnica.Model.Prostorije;
using Bolnica.Repository.Korisnici;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Repository.Prostorije
{
    public interface IRepositoryHospitalizacija : IRepository<Hospitalizacija, int>
    {
    }
}
