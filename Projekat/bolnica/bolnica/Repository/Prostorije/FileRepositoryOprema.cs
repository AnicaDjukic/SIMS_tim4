using Bolnica.Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Repository.Prostorije
{
    public class FileRepositoryOprema : IRepositoryOprema
    {
        public string fileLocation { get; set; }

        public void Delete(Oprema entity)
        {
            throw new NotImplementedException();
        }

        public List<Oprema> GetAll()
        {
            throw new NotImplementedException();
        }

        public Oprema GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(Oprema newEntity)
        {
            throw new NotImplementedException();
        }

        public void Update(Oprema entity)
        {
            throw new NotImplementedException();
        }
    }
}
