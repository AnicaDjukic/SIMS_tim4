using Bolnica.Model.Korisnici;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Repository.Korisnici
{
    public class FileRepositoryOcenaAplikacije : IRepositoryOcenaAplikacije
    {
        private string fileLocation;

        public FileRepositoryOcenaAplikacije()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            fileLocation = System.IO.Path.Combine(path, @"Resources", "OceneAplikacije.json");
        }
        public void Delete(OcenaAplikacije entity)
        {
            throw new NotImplementedException();
        }

        public List<OcenaAplikacije> GetAll()
        {
            throw new NotImplementedException();
        }

        public OcenaAplikacije GetById(object id)
        {
            throw new NotImplementedException();
        }

        public void Save(OcenaAplikacije newEntity)
        {
            var json = File.ReadAllText(fileLocation);
            List<OcenaAplikacije> ocene = JsonConvert.DeserializeObject<List<OcenaAplikacije>>(json);
            if (ocene == null)
            {
                ocene = new List<OcenaAplikacije>();
            }
            ocene.Add(newEntity);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(ocene, Formatting.Indented));
        }

        public void Update(OcenaAplikacije entity)
        {
            throw new NotImplementedException();
        }
    }
}
