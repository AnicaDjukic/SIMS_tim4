using Bolnica.Model.Pregledi;
using Bolnica.Repository.Korisnici;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Repository.Pregledi
{
    public class FileRepositorySastojak : IRepositorySastojak
    {
        public string FileLocation { get; set; }

        public FileRepositorySastojak()
        {
            string putanja = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            FileLocation = System.IO.Path.Combine(putanja, @"Resources", "Sastojci.json");
        }

        public void Delete(Sastojak entity)
        {
            FileRepositoryPacijent.serializeAlergeni = true;
            List<Sastojak> alergeni = GetAll();
            for (int i = 0; i < alergeni.Count; i++)
            {
                if (entity.Id.Equals(alergeni[i].Id))
                {
                    alergeni.Remove(alergeni[i]);
                    break;
                }
            }
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(alergeni));
        }

        public List<Sastojak> GetAll()
        {
            FileRepositoryPacijent.serializeAlergeni = true;
            var json = File.ReadAllText(FileLocation);
            var alergeni = JsonConvert.DeserializeObject<List<Sastojak>>(json);
            if (alergeni is null)
            {
                alergeni = new List<Sastojak>();
            }
            return alergeni;
        }

        public Sastojak GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(Sastojak newEntity)
        {
            FileRepositoryPacijent.serializeAlergeni = true;
            List<Sastojak> alergeni = GetAll();
            alergeni.Add(newEntity);
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(alergeni));
        }

        public void Update(Sastojak entity)
        {
            FileRepositoryPacijent.serializeAlergeni = true;
            List<Sastojak> alergeni = GetAll();
            for (int i = 0; i < alergeni.Count; i++)
            {
                if (entity.Id.Equals(alergeni[i].Id))
                {
                    alergeni[i] = entity;
                    break;
                }
            }
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(alergeni));
        }
    }
}
