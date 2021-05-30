using Bolnica.Model.Prostorije;
using Bolnica.Repository.Prostorije;
using Model.Prostorije;
using Bolnica.Model.Prostorije;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Repository.Prostorije
{
    public class FileRepositoryRenoviranje : RepositoryRenoviranje
    {
        private string fileLocation;

        public FileRepositoryRenoviranje()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            fileLocation = System.IO.Path.Combine(path, @"Resources\", "Renoviranja.json");
        }

        public List<Renoviranje> GetAll()
        {
            FileRepositoryZaliha.serializeProstorija = false;
            string json = File.ReadAllText(fileLocation);
            List<Renoviranje> renoviranja = JsonConvert.DeserializeObject<List<Renoviranje>>(json);
            return renoviranja;
        }
        public void Delete(Renoviranje entity)
        {
            FileRepositoryZaliha.serializeProstorija = false;
            string json = File.ReadAllText(fileLocation);
            List<Renoviranje> renoviranja = JsonConvert.DeserializeObject<List<Renoviranje>>(json);
            if (renoviranja != null)
            {
                for (int i = 0; i < renoviranja.Count; i++)
                {
                    if (renoviranja[i].Prostorija.BrojProstorije == entity.Prostorija.BrojProstorije && renoviranja[i].PocetakRenoviranja == entity.PocetakRenoviranja
                        && renoviranja[i].KrajRenoviranja == entity.KrajRenoviranja)
                    {
                        renoviranja.Remove(renoviranja[i]);
                        break;
                    }
                }
                File.WriteAllText(fileLocation, JsonConvert.SerializeObject(renoviranja));
            }
        }

        public Renoviranje GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Save(Renoviranje newEntity)
        {
            FileRepositoryZaliha.serializeProstorija = false;
            string json = File.ReadAllText(fileLocation);
            List<Renoviranje> renoviranja = JsonConvert.DeserializeObject<List<Renoviranje>>(json);
            if (renoviranja == null)
            {
                renoviranja = new List<Renoviranje>();
            }
            renoviranja.Add(newEntity);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(renoviranja));
        }

        public void Update(Renoviranje entity)
        {
            throw new NotImplementedException();
        }
    }
}
