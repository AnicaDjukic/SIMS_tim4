using Bolnica.Repository.Korisnici;
using Model.Korisnici;
using Model.Pregledi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Model.Korisnici
{

    public class FileRepositoryLekar : IRepositoryLekar
        feature_sekretar7:Projekat/bolnica/bolnica/Repository/Korisnici/FileRepositoryLekar.cs
    {

        public string fileLocation { get; set; }

        public FileRepositoryLekar()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            fileLocation = System.IO.Path.Combine(path, @"Resources\", "Lekari.json");
        }

        public List<Lekar> GetAll()
        {
            FileRepositoryPregled.serializeKorisnik = true;
            var json = File.ReadAllText(fileLocation);
            var lekari = JsonConvert.DeserializeObject<List<Lekar>>(json);
            return lekari;
        }

        public void Save(Lekar noviLekar)
        {
            FileRepositoryPregled.serializeKorisnik = true;
            var json = File.ReadAllText(fileLocation);
            List<Lekar> lekari = JsonConvert.DeserializeObject<List<Lekar>>(json);
            if (lekari == null)
            {
                lekari = new List<Lekar>();
            }
            lekari.Add(noviLekar);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(lekari));
        }

        public void Delete(Lekar lekar)
        {
            FileRepositoryPregled.serializeKorisnik = true;
            var json = File.ReadAllText(fileLocation);
            List<Lekar> lekari = JsonConvert.DeserializeObject<List<Lekar>>(json);
            if (lekari != null)
            {
                for (int i = 0; i < lekari.Count; i++)
                {
                    if (lekari[i].KorisnickoIme == lekar.KorisnickoIme)
                    {
                        lekari.Remove(lekari[i]);
                        break;
                    }
                }
                File.WriteAllText(fileLocation, JsonConvert.SerializeObject(lekari));
            }
        }

        public void Update(Lekar entity)
        {
            FileRepositoryPregled.serializeKorisnik = true;
            List<Lekar> lekari = new List<Lekar>();
            lekari = GetAll();

            for (int i = 0; i < lekari.Count; i++)
            {
                if (lekari[i].KorisnickoIme.Equals(entity.KorisnickoIme))
                {
                    lekari[i] = entity;
                    break;
                }
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(lekari));
        }

        public Lekar GetById(string id)
        {
            FileRepositoryPregled.serializeKorisnik = true;
            var json = File.ReadAllText(fileLocation);
            var lekari = JsonConvert.DeserializeObject<List<Lekar>>(json);

            Lekar lekar = new Lekar();
            foreach (Lekar l in lekari)
                if (l.KorisnickoIme == id)
                    lekar = l;
            return lekar;
        }
    }
}
