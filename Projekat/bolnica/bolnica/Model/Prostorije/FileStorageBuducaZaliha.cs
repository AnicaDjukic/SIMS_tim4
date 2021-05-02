using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Model.Prostorije
{
    public class FileStorageBuducaZaliha
    {
        private string fileLocation;

        public FileStorageBuducaZaliha()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            fileLocation = System.IO.Path.Combine(path, @"Resources\", "BuduceZalihe.json");
        }

        public List<BuducaZaliha> GetAll()
        {
            var json = File.ReadAllText(fileLocation);
            var buduceZalihe = JsonConvert.DeserializeObject<List<BuducaZaliha>>(json);
            return buduceZalihe;
        }

        public void Save(BuducaZaliha novaBuducaZaliha)
        {
            var json = File.ReadAllText(fileLocation);
            List<BuducaZaliha> buduceZalihe = JsonConvert.DeserializeObject<List<BuducaZaliha>>(json);
            if (buduceZalihe == null)
            {
                buduceZalihe = new List<BuducaZaliha>();
            }
            buduceZalihe.Add(novaBuducaZaliha);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(buduceZalihe));
        }

        public void Delete(BuducaZaliha buducaZalihaZaBrisanje)
        {
            var json = File.ReadAllText(fileLocation);
            List<BuducaZaliha> buduceZalihe = JsonConvert.DeserializeObject<List<BuducaZaliha>>(json);
            if (buduceZalihe != null)
            {
                for (int i = 0; i < buduceZalihe.Count; i++)
                {
                    if (buduceZalihe[i].Datum == buducaZalihaZaBrisanje.Datum && buduceZalihe[i].SifraOpreme == buducaZalihaZaBrisanje.SifraOpreme)
                    {
                        buduceZalihe.Remove(buduceZalihe[i]);
                        break;
                    }
                }
                File.WriteAllText(fileLocation, JsonConvert.SerializeObject(buduceZalihe));
            }
        }
    }
}
