using Bolnica.Model.Prostorije;
using Bolnica.Repository.Korisnici;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Repository.Prostorije
{
    public interface RepositoryBuducaZaliha : Repository<BuducaZaliha, Object>
    {
        public void Save(List<BuducaZaliha> buduceZalihe);
    }
}
