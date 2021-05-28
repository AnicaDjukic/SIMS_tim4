using Bolnica.Repository.Pregledi;
using Model.Korisnici;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Repository.Korisnici
{
    public class FileRepositoryPacijent : IRepositoryPacijent
    {
        public string FileLocation { get; set; }
        public static bool serializeAlergeni;
        public static bool serializeKarton;

        public FileRepositoryPacijent()
        {
            string putanja = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            FileLocation = System.IO.Path.Combine(putanja, @"Resources", "Pacijenti.json");
        }

        public void Delete(Pacijent entity)
        {
            serializeAlergeni = false;
            serializeKarton = false;
            FileRepositoryPregled.serializeKorisnik = true;
            List<Pacijent> pacijenti = GetAll();
            for (int i = 0; i < pacijenti.Count; i++)
            {
                if (entity.Jmbg.Equals(pacijenti[i].Jmbg))
                {
                    pacijenti.Remove(pacijenti[i]);
                    break;
                }
            }
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(pacijenti));
        }

        public List<Pacijent> GetAll()
        {
            serializeAlergeni = false;
            serializeKarton = false;
            FileRepositoryPregled.serializeKorisnik = true;
            var json = File.ReadAllText(FileLocation);
            var pacijenti = JsonConvert.DeserializeObject<List<Pacijent>>(json);
            if (pacijenti is null)
            {
                pacijenti = new List<Pacijent>();
            }
            return pacijenti;
        }

        public Pacijent GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Save(Pacijent newEntity)
        {
            serializeAlergeni = false;
            serializeKarton = false;
            FileRepositoryPregled.serializeKorisnik = true;
            List<Pacijent> pacijenti = GetAll();
            pacijenti.Add(newEntity);
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(pacijenti));
        }

        public void Update(Pacijent entity)
        {
            serializeAlergeni = false;
            serializeKarton = false;
            FileRepositoryPregled.serializeKorisnik = true;
            List<Pacijent> pacijenti = GetAll();
            for (int i = 0; i < pacijenti.Count; i++)
            {
                if (entity.Jmbg.Equals(pacijenti[i].Jmbg))
                {
                    pacijenti[i] = entity;
                    break;
                }
            }
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(pacijenti));
        }
    }
}
