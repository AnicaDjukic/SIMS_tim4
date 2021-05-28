using Bolnica.Repository.Pregledi;
using Model.Korisnici;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Repository.Korisnici
{
    public class FileRepositoryAntiTrol : IRepositoryAntiTrol
    {
        public string FileLocation { get; set; }

        public FileRepositoryAntiTrol()
        {
            FileRepositoryPregled.serializeKorisnik = false;
            string putanja = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            FileLocation = System.IO.Path.Combine(putanja, @"Resources\", "AntiTrol.json");
        }

        public void Delete(AntiTrol entity)
        {
            FileRepositoryPregled.serializeKorisnik = false;
            List<AntiTrol> antiTrolList = GetAll();
            foreach (AntiTrol antiTrol in antiTrolList)
            {
                if (entity.Id.Equals(antiTrol.Id))
                {
                    antiTrolList.Remove(antiTrol);
                    break;
                }
            }
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(antiTrolList));
        }

        public List<AntiTrol> GetAll()
        {
            FileRepositoryPregled.serializeKorisnik = false;
            var json = File.ReadAllText(FileLocation);
            var antiTrolList = JsonConvert.DeserializeObject<List<AntiTrol>>(json);
            if (antiTrolList is null)
            {
                antiTrolList = new List<AntiTrol>();
            }
            return antiTrolList;
        }

        public AntiTrol GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(AntiTrol newEntity)
        {
            FileRepositoryPregled.serializeKorisnik = false;
            List<AntiTrol> antiTrolList = GetAll();
            antiTrolList.Add(newEntity);
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(antiTrolList));
        }

        public void Update(AntiTrol entity)
        {
            FileRepositoryPregled.serializeKorisnik = false;
            List<AntiTrol> antiTrolList = GetAll();
            foreach (AntiTrol antiTrol in antiTrolList)
            {
                if (entity.Id.Equals(antiTrol.Id))
                {
                    antiTrolList.Remove(antiTrol);
                    antiTrolList.Add(entity);
                    break;
                }
            }
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(antiTrolList));
        }
    }
}
