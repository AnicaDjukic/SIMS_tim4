using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Model.Prostorije
{
    class FileStorageRenoviranje
    {
        private string fileLocation;

        public FileStorageRenoviranje()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            fileLocation = System.IO.Path.Combine(path, @"Resources\", "Renoviranja.json");
        }

        public List<Renoviranje> GetAll()
        {
            var json = File.ReadAllText(fileLocation);
            var renoviranja = JsonConvert.DeserializeObject<List<Renoviranje>>(json);
            return renoviranja;
        }

        public void Save(Renoviranje novoRenoviranje)
        {
            var json = File.ReadAllText(fileLocation);
            List<Renoviranje> renoviranja = JsonConvert.DeserializeObject<List<Renoviranje>>(json);
            if (renoviranja == null)
            {
                renoviranja = new List<Renoviranje>();
            }
            renoviranja.Add(novoRenoviranje);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(renoviranja));
        }

        public void Delete(Renoviranje renoviranjeZaBrisanje)
        {
            var json = File.ReadAllText(fileLocation);
            List<Renoviranje> renoviranja = JsonConvert.DeserializeObject<List<Renoviranje>>(json);
            if (renoviranja != null)
            {
                for (int i = 0; i < renoviranja.Count; i++)
                {
                    if (renoviranja[i].Id == renoviranjeZaBrisanje.Id)
                    {
                        renoviranja.Remove(renoviranja[i]);
                        break;
                    }
                }
                File.WriteAllText(fileLocation, JsonConvert.SerializeObject(renoviranja));
            }
        }
    }
}
