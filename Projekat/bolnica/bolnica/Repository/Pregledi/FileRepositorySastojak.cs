using Bolnica.Model.Pregledi;
using Model.Pacijenti;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Repository.Pregledi
{
    public class FileRepositorySastojak : RepositorySastojak
    {
        private string fileLocation;

        public FileRepositorySastojak()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            fileLocation = System.IO.Path.Combine(path, @"Resources", "Sastojci.json");
        }

        public List<Sastojak> GetAll()
        {
            FileStoragePacijenti.serializeAlergeni = true;
            var json = File.ReadAllText(fileLocation);
            var alergeni = JsonConvert.DeserializeObject<List<Sastojak>>(json);
            return alergeni;
        }

        public void Save(Sastojak noviSastojak)
        {
            FileStoragePacijenti.serializeAlergeni = true;
            var json = File.ReadAllText(fileLocation);
            List<Sastojak> alergeni = JsonConvert.DeserializeObject<List<Sastojak>>(json);
            if (alergeni == null)
            {
                alergeni = new List<Sastojak>();
            }
            alergeni.Add(noviSastojak);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(alergeni));
        }
        public void Delete(Sastojak entity)
        {
            throw new NotImplementedException();
        }

        public Sastojak GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Sastojak entity)
        {
            throw new NotImplementedException();
        }
    }
}
