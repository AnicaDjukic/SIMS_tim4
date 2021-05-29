using Bolnica.Repository.Korisnici;
using Model.Pregledi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Model.Korisnici
{
    public class FileRepositoryGodisnji : IRepositoryGodisnji
    {
        private string fileLocation;

        public FileRepositoryGodisnji()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            fileLocation = System.IO.Path.Combine(path, @"Resources\", "Godisnji.json");
        }

        public void Delete(Godisnji entity)
        {
            throw new NotImplementedException();
        }

        public List<Godisnji> GetAll()
        {
            FileRepositoryPregled.serializeKorisnik = false;
            string json = File.ReadAllText(fileLocation);
            List<Godisnji> godisnji = JsonConvert.DeserializeObject<List<Godisnji>>(json);
            return godisnji;
        }

        public Godisnji GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(Godisnji noviGodisnji)
        {
            FileRepositoryPregled.serializeKorisnik = false;
            string json = File.ReadAllText(fileLocation);
            List<Godisnji> godisnji = JsonConvert.DeserializeObject<List<Godisnji>>(json);
            if (godisnji == null)
            {
                godisnji = new List<Godisnji>();
            }
            godisnji.Add(noviGodisnji);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(godisnji));
        }

        public void Update(Godisnji entity)
        {
            throw new NotImplementedException();
        }
    }
}
