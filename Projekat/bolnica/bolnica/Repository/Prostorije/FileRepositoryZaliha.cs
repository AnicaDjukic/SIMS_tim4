using Bolnica.Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Repository.Prostorije
{
    public class FileRepositoryZaliha : IRepositoryZaliha
    {
        public string fileLocation { get; set; }
        public static bool serializeOprema;
        public static bool serializeProstorija;

        public void Delete(Zaliha entity)
        {
            throw new NotImplementedException();
        }

        public List<Zaliha> GetAll()
        {
            throw new NotImplementedException();
        }

        public Zaliha GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(Zaliha newEntity)
        {
            throw new NotImplementedException();
        }

        public void Update(Zaliha entity)
        {
            throw new NotImplementedException();
        }
    }
}
