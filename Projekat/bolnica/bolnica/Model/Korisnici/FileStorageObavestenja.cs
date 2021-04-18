using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace Bolnica.Model.Korisnici
{
    public class FileStorageObavestenja
    {
        private string fileLocation;

        public FileStorageObavestenja()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            fileLocation = System.IO.Path.Combine(path, @"Resources", "Obavestenja.json");
        }

        public List<Obavestenje> GetAll()
        {
            var json = File.ReadAllText(fileLocation);
            var obavestenja = JsonConvert.DeserializeObject<List<Obavestenje>>(json);
            return obavestenja;
        }

        public void Save(Obavestenje novoObavestenje)
        {
            var json = File.ReadAllText(fileLocation);
            List<Obavestenje> obavestenja = JsonConvert.DeserializeObject<List<Obavestenje>>(json);
            if (obavestenja == null)
            {
                obavestenja = new List<Obavestenje>();
            }
            obavestenja.Add(novoObavestenje);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(obavestenja));
        }

        public void Delete(Obavestenje obavestenje)
        {
            var json = File.ReadAllText(fileLocation);
            List<Obavestenje> obavestenja = JsonConvert.DeserializeObject<List<Obavestenje>>(json);
            if (obavestenja != null)
            {
                for (int i = 0; i < obavestenja.Count; i++)
                {
                    if (obavestenja[i].Id == obavestenje.Id)
                    {
                        obavestenja.Remove(obavestenja[i]);
                        break;
                    }
                }
                File.WriteAllText(fileLocation, JsonConvert.SerializeObject(obavestenja));
            }
        }
    }
}
