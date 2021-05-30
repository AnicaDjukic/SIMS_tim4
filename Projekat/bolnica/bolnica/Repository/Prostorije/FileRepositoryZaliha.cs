using Bolnica.Model.Prostorije;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Repository.Prostorije
{
    public class FileRepositoryZaliha : RepositoryZaliha
    {
        private string fileLocation;
        public static bool serializeOprema;
        public static bool serializeProstorija;

        public FileRepositoryZaliha()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            fileLocation = System.IO.Path.Combine(path, @"Resources\", "Zalihe.json");
        }
        public List<Zaliha> GetAll()
        {
            serializeOprema = false;
            serializeProstorija = false;
            string json = File.ReadAllText(fileLocation);
            List<Zaliha> zalihe = JsonConvert.DeserializeObject<List<Zaliha>>(json);
            return zalihe;
        }

        public void Save(Zaliha newEntity)
        {
            serializeOprema = false;
            serializeProstorija = false;
            string json = File.ReadAllText(fileLocation);
            List<Zaliha> zalihe = JsonConvert.DeserializeObject<List<Zaliha>>(json);
            if (zalihe == null)
            {
                zalihe = new List<Zaliha>();
            }
            zalihe.Add(newEntity);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(zalihe));
        }

        public void Delete(Zaliha entity)
        {
            serializeOprema = false;
            serializeProstorija = false;
            string json = File.ReadAllText(fileLocation);
            List<Zaliha> zalihe = JsonConvert.DeserializeObject<List<Zaliha>>(json);
            if (zalihe != null)
            {
                for (int i = 0; i < zalihe.Count; i++)
                {
                    if (zalihe[i].Prostorija.BrojProstorije == entity.Prostorija.BrojProstorije && zalihe[i].Oprema.Sifra == entity.Oprema.Sifra)
                    {
                        zalihe.Remove(zalihe[i]);
                        break;
                    }
                }
                File.WriteAllText(fileLocation, JsonConvert.SerializeObject(zalihe));
            }
        }

        public Zaliha GetById(object id)
        {
            throw new NotImplementedException();
        }

        public void Update(Zaliha entity)
        {
            throw new NotImplementedException();
        }
    }
}
