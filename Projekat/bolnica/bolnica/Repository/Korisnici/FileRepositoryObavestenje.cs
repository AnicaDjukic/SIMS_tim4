using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Model.Pregledi;
using Bolnica.Repository.Korisnici;

namespace Bolnica.Model.Korisnici
{
    public class FileRepositoryObavestenje : IRepositoryObavestenje
    {
        private string fileLocation;

        public FileRepositoryObavestenje()
        {
            FileRepositoryPregled.serializeKorisnik = false;
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            fileLocation = System.IO.Path.Combine(path, @"Resources", "Obavestenja.json");
        }

        public List<Obavestenje> GetAll()
        {
            FileRepositoryPregled.serializeKorisnik = false;
            var json = File.ReadAllText(fileLocation);
            var obavestenja = JsonConvert.DeserializeObject<List<Obavestenje>>(json);
            return obavestenja;
        }

        public void Save(Obavestenje novoObavestenje)
        {
            FileRepositoryPregled.serializeKorisnik = false;
            var json = File.ReadAllText(fileLocation);
            List<Obavestenje> obavestenja = JsonConvert.DeserializeObject<List<Obavestenje>>(json);
            if (obavestenja == null)
            {
                obavestenja = new List<Obavestenje>();
            }
            obavestenja.Add(novoObavestenje);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(obavestenja));
        }

        public void Delete(Obavestenje obavestenje)
        {
            FileRepositoryPregled.serializeKorisnik = false;
            var json = File.ReadAllText(fileLocation);
            List<Obavestenje> obavestenja = JsonConvert.DeserializeObject<List<Obavestenje>>(json);
            if (obavestenja != null)
            {
                for (int i = 0; i < obavestenja.Count; i++)
                {
                    if (obavestenja[i].Id == obavestenje.Id)
                    {
                        obavestenja.Remove(obavestenja[i]);
                        break;
                    }
                }
                File.WriteAllText(fileLocation, JsonConvert.SerializeObject(obavestenja));
            }
        }

        public void Update(Obavestenje entity)
        {
            FileRepositoryPregled.serializeKorisnik = false;
            List<Obavestenje> obavestenja = new List<Obavestenje>();
            obavestenja = GetAll();

            for (int i = 0; i < obavestenja.Count; i++)
            {
                if (obavestenja[i].Id.Equals(entity.Id))
                {
                    obavestenja[i] = entity;
                    break;
                }
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(obavestenja));
        }

        public Obavestenje GetById(int id)
        {
            FileRepositoryPregled.serializeKorisnik = false;
            var json = File.ReadAllText(fileLocation);
            var obavestenja = JsonConvert.DeserializeObject<List<Obavestenje>>(json);

            Obavestenje obavestenje = new Obavestenje();
            foreach (Obavestenje o in obavestenja)
                if (o.Id == id)
                    obavestenje = o;
            return obavestenje;
        }
    }
}
