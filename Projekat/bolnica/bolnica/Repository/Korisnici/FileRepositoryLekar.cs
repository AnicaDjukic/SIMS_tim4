using Bolnica.Repository.Pregledi;
using Model.Korisnici;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Repository.Korisnici
{
    public class FileRepositoryLekar : IRepositoryLekar
    {
        public string FileLocation { get; set; }

        public FileRepositoryLekar()
        {
            string putanja = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            FileLocation = System.IO.Path.Combine(putanja, @"Resources\", "Lekari.json");
        }

        public void Delete(Lekar entity)
        {
            FileRepositoryPregled.serializeKorisnik = true;
            List<Lekar> lekari = GetAll();
            for (int i = 0; i < lekari.Count; i++)
            {
                if (entity.KorisnickoIme.Equals(lekari[i].KorisnickoIme))
                {
                    lekari.Remove(lekari[i]);
                    break;
                }
            }
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(lekari));
        }

        public List<Lekar> GetAll()
        {
            FileRepositoryPregled.serializeKorisnik = true;
            var json = File.ReadAllText(FileLocation);
            var lekari = JsonConvert.DeserializeObject<List<Lekar>>(json);
            if (lekari == null)
            {
                lekari = new List<Lekar>();
            }
            return lekari;
        }

        public Lekar GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Save(Lekar newEntity)
        {
            FileRepositoryPregled.serializeKorisnik = true;
            List<Lekar> lekari = GetAll();
            lekari.Add(newEntity);
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(lekari));
        }

        public void Update(Lekar entity)
        {
            FileRepositoryPregled.serializeKorisnik = true;
            List<Lekar> lekari = GetAll();
            for (int i = 0; i < lekari.Count; i++)
            {
                if (entity.KorisnickoIme.Equals(lekari[i].KorisnickoIme))
                {
                    lekari[i] = entity;
                    break;
                }
            }
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(lekari));
        }
    }
}
