using Bolnica.Model.Prostorije;
using Bolnica.Repository.Korisnici;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Repository.Prostorije
{
    public interface RepositoryOprema : Repository<Oprema, string>
    {
        void DeleteById(string sifra);
    }
}
