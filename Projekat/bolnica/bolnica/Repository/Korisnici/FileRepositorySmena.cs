using Bolnica.Model.Korisnici;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Repository.Korisnici
{
    public class FileRepositorySmena
    {
        private string fileLocation;

        public FileRepositorySmena()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            fileLocation = System.IO.Path.Combine(path, @"Resources", "Smena.json");
        }

        public List<Smena> GetAll()
        {
            FileRepositoryLekar.serializeSmena = true;
            var json = File.ReadAllText(fileLocation);
            var smene = JsonConvert.DeserializeObject<List<Smena>>(json);
            return smene;
        }

        public void Save(Smena novaSmena)
        {
            FileRepositoryLekar.serializeSmena = true;
            var json = File.ReadAllText(fileLocation);
            List<Smena> smene = JsonConvert.DeserializeObject<List<Smena>>(json);
            if (smene == null)
            {
                smene = new List<Smena>();
            }
            smene.Add(novaSmena);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(smene));
        }

        public void Update(Smena smena)
        {
            FileRepositoryLekar.serializeSmena = true;
            List<Smena> smene = new List<Smena>();
            smene = GetAll();

            for (int i = 0; i < smene.Count; i++)
            {
                if (smene[i].Id.Equals(smena.Id))
                {
                    smene[i] = smena;
                    break;
                }
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(smene));
        }

        public void Delete(Smena smena)
        {
            FileRepositoryLekar.serializeSmena = true;
            var json = File.ReadAllText(fileLocation);
            List<Smena> smene = JsonConvert.DeserializeObject<List<Smena>>(json);
            if (smene != null)
            {
                for (int i = 0; i < smene.Count; i++)
                {
                    if (smene[i].Id == smena.Id)
                    {
                        smene.Remove(smene[i]);
                        break;
                    }
                }
                File.WriteAllText(fileLocation, JsonConvert.SerializeObject(smene));
            }
        }

        public Smena GetById(int id)
        {
            FileRepositoryLekar.serializeSmena = true;
            var json = File.ReadAllText(fileLocation);
            var smene = JsonConvert.DeserializeObject<List<Smena>>(json);

            Smena smena = new Smena();
            foreach (Smena s in smene)
                if (s.Id == id)
                    smena = s;
            return smena;
        }
    }
}
