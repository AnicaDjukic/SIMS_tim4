using Bolnica.Model.Korisnici;
using Model.Pregledi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Repository.Korisnici
{
    class FileRepositoryObavestenje : RepositoryObavestenje
    {
        private string fileLocation;

        public FileRepositoryObavestenje()
        {
            FileStoragePregledi.serializeKorisnik = false;
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            fileLocation = System.IO.Path.Combine(path, @"Resources", "Obavestenja.json");
        }

        public List<Obavestenje> GetAll()
        {
            FileStoragePregledi.serializeKorisnik = false;
            var json = File.ReadAllText(fileLocation);
            var obavestenja = JsonConvert.DeserializeObject<List<Obavestenje>>(json);
            return obavestenja;
        }
        public void Delete(Obavestenje entity)
        {
            throw new NotImplementedException();
        }

        public Obavestenje GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(Obavestenje newEntity)
        {
            throw new NotImplementedException();
        }

        public void Update(Obavestenje entity)
        {
            throw new NotImplementedException();
        }
    }
}
