using Bolnica.Model.Korisnici;
using Bolnica.Repository.Pregledi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Repository.Korisnici
{
    public class FileRepositoryObavestenje : IRepositoryObavestenje
    {
        public string FileLocation { get; set; }

        public FileRepositoryObavestenje()
        {
            FileRepositoryPregled.serializeKorisnik = false;
            string putanja = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            FileLocation = System.IO.Path.Combine(putanja, @"Resources", "Obavestenja.json");
        }

        public void Delete(Obavestenje entity)
        {
            FileRepositoryPregled.serializeKorisnik = false;
            List<Obavestenje> obavestenja = GetAll();
            for (int i = 0; i < obavestenja.Count; i++)
            {
                if (entity.Id.Equals(obavestenja[i].Id))
                {
                    obavestenja.Remove(obavestenja[i]);
                    break;
                }
            }
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(obavestenja));
        }

        public List<Obavestenje> GetAll()
        {
            FileRepositoryPregled.serializeKorisnik = false;
            var json = File.ReadAllText(FileLocation);
            var obavestenja = JsonConvert.DeserializeObject<List<Obavestenje>>(json);
            if (obavestenja is null)
            {
                obavestenja = new List<Obavestenje>();
            }
            return obavestenja;
        }

        public Obavestenje GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(Obavestenje newEntity)
        {
            FileRepositoryPregled.serializeKorisnik = false;
            List<Obavestenje> obavestenja = GetAll();
            obavestenja.Add(newEntity);
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(obavestenja));
        }

        public void Update(Obavestenje entity)
        {
            FileRepositoryPregled.serializeKorisnik = false;
            List<Obavestenje> obavestenja = GetAll();
            for (int i = 0; i < obavestenja.Count; i++)
            {
                if (entity.Id.Equals(obavestenja[i].Id))
                {
                    obavestenja[i] = entity;
                    break;
                }
            }
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(obavestenja));
        }
    }
}
