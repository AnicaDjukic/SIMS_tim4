using Newtonsoft.Json;
using Model.Korisnici;
using System;
using System.Collections.Generic;
using System.IO;
using Model.Pregledi;

namespace Model.Pacijenti
{
   public class FileStoragePacijenti
   {
        private string fileLocation;
      
        public FileStoragePacijenti() {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            fileLocation = System.IO.Path.Combine(path, @"Resources", "Pacijenti.json");
        }

        public List<Pacijent> GetAll()
        {
            FileStoragePregledi.serializeKorisnik = true;
            var json = File.ReadAllText(fileLocation);
            var pacijenti = JsonConvert.DeserializeObject<List<Pacijent>>(json);
            return pacijenti;
        }
      
        public void Save(Pacijent noviPacijent)
        {
            FileStoragePregledi.serializeKorisnik = true;
            var json = File.ReadAllText(fileLocation);
            List<Pacijent> pacijenti = JsonConvert.DeserializeObject<List<Pacijent>>(json);
            if (pacijenti == null)
            {
                pacijenti = new List<Pacijent>();
            }
            pacijenti.Add(noviPacijent);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(pacijenti));
        }

        public void Delete(Pacijent pacijent)
        {
            FileStoragePregledi.serializeKorisnik = true;
            var json = File.ReadAllText(fileLocation);
            List<Pacijent> pacijenti = JsonConvert.DeserializeObject<List<Pacijent>>(json);
            if (pacijenti != null)
            {
                for (int i = 0; i < pacijenti.Count; i++)
                {
                    if (pacijenti[i].Jmbg == pacijent.Jmbg)
                    {
                        pacijenti.Remove(pacijenti[i]);
                        break;
                    }
                }
                File.WriteAllText(fileLocation, JsonConvert.SerializeObject(pacijenti));
            }
        }
    }
}