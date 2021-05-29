using Model.Prostorije;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Repository.Prostorije
{
    public class FileRepositoryBolnickaSoba : RepositoryBolnickaSoba
    {
        private string fileLocation;

        public FileRepositoryBolnickaSoba()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            fileLocation = System.IO.Path.Combine(path, @"Resources\", "BolnickeSobe.json");
        }

        public List<BolnickaSoba> GetAll()
        {
            FileRepositoryZaliha.serializeProstorija = true;
            string json = File.ReadAllText(fileLocation);
            List<BolnickaSoba> prostorije = JsonConvert.DeserializeObject<List<BolnickaSoba>>(json);
            return prostorije;
        }

        public void Save(BolnickaSoba newEntity)
        {
            FileRepositoryZaliha.serializeProstorija = true;
            string json = File.ReadAllText(fileLocation);
            List<BolnickaSoba> bolnickeSobe = JsonConvert.DeserializeObject<List<BolnickaSoba>>(json);
            if (bolnickeSobe == null)
            {
                bolnickeSobe = new List<BolnickaSoba>();
            }
            bolnickeSobe.Add(newEntity);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(bolnickeSobe));
        }

        public void Delete(BolnickaSoba entity)
        {
            FileRepositoryZaliha.serializeProstorija = true;
            string json = File.ReadAllText(fileLocation);
            List<BolnickaSoba> prostorije = JsonConvert.DeserializeObject<List<BolnickaSoba>>(json);
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
                File.WriteAllText(fileLocation, JsonConvert.SerializeObject(prostorije));
            }
        }

        public BolnickaSoba GetById(string id)
        {
            throw new NotImplementedException();
        }
        public void Update(BolnickaSoba entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(string brojProstorije)
        {
            FileRepositoryZaliha.serializeProstorija = true;
            string json = File.ReadAllText(fileLocation);
            List<BolnickaSoba> prostorije = JsonConvert.DeserializeObject<List<BolnickaSoba>>(json);
            if (prostorije != null)
            {
                for (int i = 0; i < prostorije.Count; i++)
                {
                    if (prostorije[i].BrojProstorije == brojProstorije)
                    {
                        prostorije.Remove(prostorije[i]);
                        break;
                    }
                }
                File.WriteAllText(fileLocation, JsonConvert.SerializeObject(prostorije));
            }
        }
    }
}
