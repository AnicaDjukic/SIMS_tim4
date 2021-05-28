using Bolnica.Repository.Pregledi;
using Model.Korisnici;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Repository.Korisnici
{
    public class FileRepositoryKorisnik : IRepositoryKorisnik
    {
        public string FileLocation { get; set; }

        public FileRepositoryKorisnik()
        {
            string putanja = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            FileLocation = System.IO.Path.Combine(putanja, @"Resources\", "Korisnici.json");
        }

        public void Delete(Korisnik entity)
        {
            FileRepositoryPregled.serializeKorisnik = true;
            List<Korisnik> korisnici = GetAll();
            for (int i = 0; i < korisnici.Count; i++)
            {
                if (entity.KorisnickoIme.Equals(korisnici[i].KorisnickoIme))
                {
                    korisnici.Remove(korisnici[i]);
                    break;
                }
            }
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(korisnici));
        }

        public List<Korisnik> GetAll()
        {
            FileRepositoryPregled.serializeKorisnik = true;
            var json = File.ReadAllText(FileLocation);
            var korisnici = JsonConvert.DeserializeObject<List<Korisnik>>(json);
            if (korisnici is null) 
            { 
                korisnici = new List<Korisnik>(); 
            }
            return korisnici;
        }

        public Korisnik GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Save(Korisnik newEntity)
        {
            FileRepositoryPregled.serializeKorisnik = true;
            List<Korisnik> korisnici = GetAll();
            korisnici.Add(newEntity);
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(korisnici));
        }

        public void Update(Korisnik entity)
        {
            FileRepositoryPregled.serializeKorisnik = true;
            List<Korisnik> korisnici = GetAll();
            for (int i = 0; i < korisnici.Count; i++)
            {
                if (entity.KorisnickoIme.Equals(korisnici[i].KorisnickoIme))
                {
                    korisnici[i] = entity;
                    break;
                }
            }
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(korisnici));
        }
    }
}
