using Bolnica.Model.Pregledi;
using Bolnica.Repository.Korisnici;
using Model.Pacijenti;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Repository.Pregledi
{
    public class FileRepositorySastojak : IRepositorySastojak
    {
        private string fileLocation;

        public FileRepositorySastojak()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            fileLocation = System.IO.Path.Combine(path, @"Resources", "Sastojci.json");
        }

        public List<Sastojak> GetAll()
        {
            FileRepositoryPacijent.serializeAlergeni = true;

            var json = File.ReadAllText(fileLocation);
            var alergeni = JsonConvert.DeserializeObject<List<Sastojak>>(json);
            return alergeni;
        }

        public void Save(Sastojak noviSastojak)
        {
            FileRepositoryPacijent.serializeAlergeni = true;

            var json = File.ReadAllText(fileLocation);
            List<Sastojak> alergeni = JsonConvert.DeserializeObject<List<Sastojak>>(json);
            if (alergeni == null)
            {
                alergeni = new List<Sastojak>();
            }
            alergeni.Add(noviSastojak);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(alergeni));
        }


        public void Delete(Sastojak sastojak)
        {
            FileRepositoryPacijent.serializeAlergeni = true;
            var json = File.ReadAllText(fileLocation);
            List<Sastojak> alergeni = JsonConvert.DeserializeObject<List<Sastojak>>(json);
            if (alergeni != null)
            {
                for (int i = 0; i < alergeni.Count; i++)
                {
                    if (alergeni[i].Id == sastojak.Id)
                    {
                        alergeni.Remove(alergeni[i]);
                        break;
                    }
                }
                File.WriteAllText(fileLocation, JsonConvert.SerializeObject(alergeni));
            }
        }
        public void Update(Sastojak entity)
        {
            FileRepositoryPacijent.serializeAlergeni = true;
            List<Sastojak> alergeni = new List<Sastojak>();
            alergeni = GetAll();

            for (int i = 0; i < alergeni.Count; i++)
            {
                if (alergeni[i].Id.Equals(entity.Id))
                {
                    alergeni[i] = entity;
                    break;
                }
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(alergeni));
        }

        public Sastojak GetById(int id)
        {
            FileRepositoryPacijent.serializeAlergeni = true;
            Sastojak sastojak = new Sastojak();
            List<Sastojak> alergeni = new List<Sastojak>();
            alergeni = GetAll();

            for (int i = 0; i < alergeni.Count; i++)
            {
                if (alergeni[i].Id.Equals(id))
                {
                    sastojak = alergeni[i];
                    break;
                }
            }
            return sastojak;
        }

        public void DeleteById(int id)
        {
            FileRepositoryPacijent.serializeAlergeni = true;
            var json = File.ReadAllText(fileLocation);
            List<Sastojak> alergeni = JsonConvert.DeserializeObject<List<Sastojak>>(json);
            if (alergeni != null)
            {
                for (int i = 0; i < alergeni.Count; i++)
                {
                    if (alergeni[i].Id == id)
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
