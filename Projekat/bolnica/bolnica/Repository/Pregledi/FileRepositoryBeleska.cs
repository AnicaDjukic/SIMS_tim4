using Bolnica.Repository.Pregledi;
using Model.Pregledi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Repository.Pregledi
{
    public class FileRepositoryBeleska : IRepositoryBeleska
    {
        public string FileLocation { get; set; }
        public static bool serializeBeleska;

        public FileRepositoryBeleska()
        {
            serializeBeleska = true;
            string putanja = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            FileLocation = System.IO.Path.Combine(putanja, @"Resources\", "Beleske.json");
        }

        public void Delete(Beleska entity)
        {
            serializeBeleska = true;
            List<Beleska> beleske = GetAll();
            foreach (Beleska beleska in beleske)
            {
                if (entity.Id.Equals(beleska.Id))
                {
                    beleske.Remove(beleska);
                    break;
                }
            }
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(beleske));
        }

        public List<Beleska> GetAll()
        {
            serializeBeleska = true;
            var json = File.ReadAllText(FileLocation);
            var beleske = JsonConvert.DeserializeObject<List<Beleska>>(json);
            if (beleske is null)
            {
                beleske = new List<Beleska>();
            }
            return beleske;
        }

        public Beleska GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(Beleska newEntity)
        {
            serializeBeleska = true;
            List<Beleska> beleske = GetAll();
            beleske.Add(newEntity);
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(beleske));
        }

        public void Update(Beleska entity)
        {
            serializeBeleska = true;
            List<Beleska> beleske = GetAll();
            foreach (Beleska beleska in beleske)
            {
                if (entity.Id.Equals(beleska.Id))
                {
                    beleske.Remove(beleska);
                    beleske.Add(entity);
                    break;
                }
            }
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(beleske));
        }
    }
}
