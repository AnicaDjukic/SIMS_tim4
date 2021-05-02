using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Model.Prostorije
{
    public class FileStorageRenoviranje
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
    }
}
