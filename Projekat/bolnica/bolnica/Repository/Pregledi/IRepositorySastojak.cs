using Bolnica.Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Repository.Korisnici
{
    public interface IRepositorySastojak : IRepository<Sastojak, int>
    {
        public void DeleteById(int id);
    }
}
