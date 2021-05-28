using Bolnica.Repository.Korisnici;
using Model.Pregledi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Repository.Pregledi
{
    public class FileRepositoryLek : IRepositoryLek
    {
        public string FileLocation { get; set; }
        public static bool serializeLek;

        public FileRepositoryLek()
        {
            serializeLek = true;
            string putanja = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            FileLocation = System.IO.Path.Combine(putanja, @"Resources\", "Lekovi.json");
        }

        public void Delete(Lek entity)
        {
            serializeLek = true;
            FileRepositoryPacijent.serializeAlergeni = false;
            List<Lek> lekovi = GetAll();
            for (int i = 0; i < lekovi.Count; i++)
            {
                if (entity.Id.Equals(lekovi[i].Id))
                {
                    lekovi.Remove(lekovi[i]);
                    break;
                }
            }
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(lekovi));
        }

        public List<Lek> GetAll()
        {
            serializeLek = true;
            FileRepositoryPacijent.serializeAlergeni = false;
            var json = File.ReadAllText(FileLocation);
            var lekovi = JsonConvert.DeserializeObject<List<Lek>>(json);
            if (lekovi is null)
            {
                lekovi = new List<Lek>();
            }
            return lekovi;
        }

        public Lek GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(Lek newEntity)
        {
            serializeLek = true;
            FileRepositoryPacijent.serializeAlergeni = false;
            List<Lek> lekovi = GetAll();
            if (lekovi == null)
            {
                lekovi = new List<Lek>();
            }
            lekovi.Add(newEntity);
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(lekovi));
        }

        public void Update(Lek entity)
        {
            serializeLek = true;
            FileRepositoryPacijent.serializeAlergeni = false;
            List<Lek> lekovi = GetAll();
            for (int i = 0; i < lekovi.Count; i++)
            {
                if (entity.Id.Equals(lekovi[i].Id))
                {
                    lekovi[i] = entity;
                    break;
                }
            }
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(lekovi));
        }
    }
}
