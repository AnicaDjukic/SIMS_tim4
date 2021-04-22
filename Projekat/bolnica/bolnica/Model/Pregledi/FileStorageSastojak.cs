using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace Bolnica.Model.Pregledi
{
    class FileStorageSastojak
    {
        private string fileLocation;

        public FileStorageSastojak()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            fileLocation = System.IO.Path.Combine(path, @"Resources", "Sastojci.json");
        }

        public List<Sastojak> GetAll()
        {
            var json = File.ReadAllText(fileLocation);
            var alergeni = JsonConvert.DeserializeObject<List<Sastojak>>(json);
            return alergeni;
        }

        public void Save(Sastojak noviAlergen)
        {
            var json = File.ReadAllText(fileLocation);
            List<Sastojak> alergeni = JsonConvert.DeserializeObject<List<Sastojak>>(json);
            if (alergeni == null)
            {
                alergeni = new List<Sastojak>();
            }
            alergeni.Add(noviAlergen);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(alergeni));
        }

        public void Delete(Sastojak alergen)
        {
            var json = File.ReadAllText(fileLocation);
            List<Sastojak> alergeni = JsonConvert.DeserializeObject<List<Sastojak>>(json);
            if (alergeni != null)
            {
                for (int i = 0; i < alergeni.Count; i++)
                {
                    if (alergeni[i].Id == alergen.Id)
                    {
                        alergeni.Remove(alergeni[i]);
                        break;
                    }
                }
                File.WriteAllText(fileLocation, JsonConvert.SerializeObject(alergeni));
            }
        }
    }
}
