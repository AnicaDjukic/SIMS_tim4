using Model.Prostorije;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Model.Prostorije
{
    class FileStorageOprema
    {
        private string fileLocation;

        public FileStorageOprema()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            fileLocation = System.IO.Path.Combine(path, @"Resources\", "Oprema.json");
        }

        public List<Oprema> GetAll()
        {
            FileStorageZaliha.serializeOprema = true;
            string json = File.ReadAllText(fileLocation);
            List<Oprema> oprema = JsonConvert.DeserializeObject<List<Oprema>>(json);
            return oprema;
        }

        public void Save(Oprema novaOprema)
        {
            FileStorageZaliha.serializeOprema = true;
            string json = File.ReadAllText(fileLocation);
            List<Oprema> oprema = JsonConvert.DeserializeObject<List<Oprema>>(json);
            if (oprema == null)
            {
                oprema = new List<Oprema>();
            }
            oprema.Add(novaOprema);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(oprema));
        }

        public void Delete(Oprema opremaZaBrisanje)
        {
            FileStorageZaliha.serializeOprema = true;
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
