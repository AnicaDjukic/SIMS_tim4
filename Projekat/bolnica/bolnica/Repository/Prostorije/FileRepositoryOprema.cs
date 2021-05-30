using Bolnica.Model.Prostorije;
using Bolnica.Repository.Prostorije;
using Model.Prostorije;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Repository.Prostorije
{
    public class FileRepositoryOprema : RepositoryOprema
    {
        private string fileLocation;
        public FileRepositoryOprema()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            fileLocation = System.IO.Path.Combine(path, @"Resources\", "Oprema.json");
        }
        public void Delete(Oprema entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(string sifra)
        {
            FileRepositoryZaliha.serializeOprema = true;
            string json = File.ReadAllText(fileLocation);
            List<Oprema> oprema = JsonConvert.DeserializeObject<List<Oprema>>(json);
            if (oprema != null)
            {
                for (int i = 0; i < oprema.Count; i++)
                {
                    if (oprema[i].Sifra == sifra)
                    {
                        oprema.Remove(oprema[i]);
                        break;
                    }
                }
                File.WriteAllText(fileLocation, JsonConvert.SerializeObject(oprema));
            }
        }

        public List<Oprema> GetAll()
        {
            FileRepositoryZaliha.serializeOprema = true;
            string json = File.ReadAllText(fileLocation);
            List<Oprema> oprema = JsonConvert.DeserializeObject<List<Oprema>>(json);
            return oprema;
        }

        public Oprema GetById(string id)
        {
            FileRepositoryZaliha.serializeOprema = true;
            string json = File.ReadAllText(fileLocation);
            Oprema rezultat = new Oprema();
            List<Oprema> oprema = JsonConvert.DeserializeObject<List<Oprema>>(json);
            if (oprema != null)
            {
                for (int i = 0; i < oprema.Count; i++)
                {
                    if (oprema[i].Sifra == id)
                    {
                        rezultat = oprema[i];
                        break;
                    }
                }
            }
            return rezultat;
        }

        public void Save(Oprema newEntity)
        {
            FileRepositoryZaliha.serializeOprema = true;
            string json = File.ReadAllText(fileLocation);
            List<Oprema> oprema = JsonConvert.DeserializeObject<List<Oprema>>(json);
            if (oprema == null)
            {
                oprema = new List<Oprema>();
            }
            oprema.Add(newEntity);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(oprema));
        }

        public void Update(Oprema entity)
        {
            FileRepositoryZaliha.serializeOprema = true;
            string json = File.ReadAllText(fileLocation);
            List<Oprema> oprema = JsonConvert.DeserializeObject<List<Oprema>>(json);
            if (oprema != null)
            {
                for (int i = 0; i < oprema.Count; i++)
                {
                    if (oprema[i].Sifra == opremaZaBrisanje.Sifra)
                    {
                        oprema.Remove(oprema[i]);
                        break;
                    }
                }
                File.WriteAllText(fileLocation, JsonConvert.SerializeObject(oprema));
            }
        }
    }
}
