using Bolnica.Repository.Korisnici;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Repository.Pregledi
{
    public interface IRepositoryOperacija : IRepository<Operacija, int>
    {
    }
}
