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
    class FileRepositoryKorisnik : IRepositoryKorisnik
    {
        public string fileLocation { get; set; }

        public FileRepositoryKorisnik()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            fileLocation = System.IO.Path.Combine(path, @"Resources\", "Korisnici.json");
        }

        public List<Korisnik> GetAll()
        {
            FileRepositoryPregled.serializeKorisnik = true;
            var json = File.ReadAllText(fileLocation);
            var korisnici = JsonConvert.DeserializeObject<List<Korisnik>>(json);
            return korisnici;
        }

        public void Save(Korisnik noviKorisnik)
        {
            FileRepositoryPregled.serializeKorisnik = true;
            var json = File.ReadAllText(fileLocation);
            List<Korisnik> korisnici = JsonConvert.DeserializeObject<List<Korisnik>>(json);
            if (korisnici == null)
            {
                korisnici = new List<Korisnik>();
            }
            korisnici.Add(noviKorisnik);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(korisnici));
        }

        public void Delete(Korisnik korisnik)
        {
            FileRepositoryPregled.serializeKorisnik = true;
            var json = File.ReadAllText(fileLocation);
            List<Korisnik> korisnici = JsonConvert.DeserializeObject<List<Korisnik>>(json);
            if (korisnici != null)
            {
                for (int i = 0; i < korisnici.Count; i++)
                {
                    if (korisnici[i].KorisnickoIme == korisnik.KorisnickoIme)
                    {
                        korisnici.Remove(korisnici[i]);
                        break;
                    }
                }
                File.WriteAllText(fileLocation, JsonConvert.SerializeObject(korisnici));
            }
        }

        public void Update(Korisnik entity)
        {
            FileRepositoryPregled.serializeKorisnik = true;
            List<Korisnik> korisnici = new List<Korisnik>();
            korisnici = GetAll();

            for (int i = 0; i < korisnici.Count; i++)
            {
                if (korisnici[i].KorisnickoIme.Equals(entity.KorisnickoIme))
                {
                    korisnici[i] = entity;
                    break;
                }
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(korisnici));
        }

        public Korisnik GetById(string id)
        {
            FileRepositoryPregled.serializeKorisnik = true;
            var json = File.ReadAllText(fileLocation);
            var korisnici = JsonConvert.DeserializeObject<List<Korisnik>>(json);

            Korisnik korisnik = new Korisnik();
            foreach (Korisnik k in korisnici)
                if (k.KorisnickoIme == id)
                    korisnik = k;
            return korisnik;
        }
    }
}
