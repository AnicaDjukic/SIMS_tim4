using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Repository.Korisnici
{
    public interface Repository<T, ID>
    {
        List<T> GetAll();
        void Save(T newEntity);
        void Delete(T entity);
        void Update(T entity);
        T GetById(ID id);
    }
}
