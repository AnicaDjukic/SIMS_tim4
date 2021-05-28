using Model.Prostorije;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Repository.Prostorije
{
    public class FileRepositoryProstorija : IRepositoryProstorija
    {
        public string FileLocation { get; set; }

        public FileRepositoryProstorija()
        {
            string putanja = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            FileLocation = System.IO.Path.Combine(putanja, @"Resources\", "Prostorije.json");
        }

        public void Delete(Prostorija entity)
        {
            FileStorageZaliha.serializeProstorija = true;
            List<Prostorija> prostorije = GetAll();
            for (int i = 0; i < prostorije.Count; i++)
            {
                if (entity.BrojProstorije.Equals(prostorije[i].BrojProstorije))
                {
                    prostorije.Remove(prostorije[i]);
                    break;
                }
            }
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(prostorije));
        }

        public List<Prostorija> GetAll()
        {
            FileRepositoryZaliha.serializeProstorija = true;
            string json = File.ReadAllText(FileLocation);
            List<Prostorija> prostorije = JsonConvert.DeserializeObject<List<Prostorija>>(json);
            if (prostorije is null)
            {
                prostorije = new List<Prostorija>();
            }
            return prostorije;
        }

        public Prostorija GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(Prostorija newEntity)
        {
            FileStorageZaliha.serializeProstorija = true;
            List<Prostorija> prostorije = GetAll();
            prostorije.Add(newEntity);
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(prostorije));
        }

        public void Update(Prostorija entity)
        {
            FileStorageZaliha.serializeProstorija = true;
            List<Prostorija> prostorije = GetAll();
            for (int i = 0; i < prostorije.Count; i++)
            {
                if (entity.BrojProstorije.Equals(prostorije[i].BrojProstorije))
                {
                    prostorije[i] = entity;
                    break;
                }
            }
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(prostorije));
        }
    }
}
