using Model.Prostorije;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Repository.Prostorije
{
    public class FileRepositoryProstorija : RepositoryProstorija
    {
        private string fileLocationProstorije;
        private string fileLocationBolnickeSobe;

        public FileRepositoryProstorija()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            fileLocationProstorije = System.IO.Path.Combine(path, @"Resources\", "Prostorije.json");
            fileLocationBolnickeSobe = System.IO.Path.Combine(path, @"Resources\", "BolnickeSobe.json");
        }
        public List<Prostorija> GetAll()
        {
            FileRepositoryZaliha.serializeProstorija = true;
            string json = File.ReadAllText(fileLocationProstorije);
            List<Prostorija> prostorije = JsonConvert.DeserializeObject<List<Prostorija>>(json);
            return prostorije;
        }

        public void Save(Prostorija newEntity)
        {
            FileRepositoryZaliha.serializeProstorija = true;
            string json = File.ReadAllText(fileLocationProstorije);
            List<Prostorija> prostorije = JsonConvert.DeserializeObject<List<Prostorija>>(json);
            if (prostorije == null)
            {
                prostorije = new List<Prostorija>();
            }
            prostorije.Add(newEntity);
            File.WriteAllText(fileLocationProstorije, JsonConvert.SerializeObject(prostorije));
        }

        public void Delete(Prostorija entity)
        {
            FileRepositoryZaliha.serializeProstorija = true;
            string json = File.ReadAllText(fileLocationProstorije);
            List<Prostorija> prostorije = JsonConvert.DeserializeObject<List<Prostorija>>(json);
            if (prostorije != null)
            {
                for (int i = 0; i < prostorije.Count; i++)
                {
                    if (prostorije[i].BrojProstorije == entity.BrojProstorije)
                    {
                        prostorije.Remove(prostorije[i]);
                        break;
                    }
                }
                File.WriteAllText(fileLocationProstorije, JsonConvert.SerializeObject(prostorije));
            }
        }

        public Prostorija GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(Prostorija entity)
        {
            throw new NotImplementedException();
        }
    }
}
