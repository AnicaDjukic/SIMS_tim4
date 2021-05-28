using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Repository.Prostorije
{
    public class FileRepositoryBolnickaSoba : IRepositoryBolnickaSoba
    {
        public string fileLocation { get; set; }

        public void Delete(BolnickaSoba entity)
        {
            throw new NotImplementedException();
        }

        public List<BolnickaSoba> GetAll()
        {
            throw new NotImplementedException();
        }

        public BolnickaSoba GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(BolnickaSoba newEntity)
        {
            throw new NotImplementedException();
        }

        public void Update(BolnickaSoba entity)
        {
            throw new NotImplementedException();
        }
    }
}
