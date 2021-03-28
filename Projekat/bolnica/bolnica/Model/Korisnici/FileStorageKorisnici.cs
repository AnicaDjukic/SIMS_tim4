using Model.Korisnici;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Model.Korisnici
{
    class FileStorageKorisnici
    {
        public string fileLocation { get; set; }

        public FileStorageKorisnici()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            fileLocation = System.IO.Path.Combine(path, @"Resources\", "Korisnici.json");
        }

        public List<Korisnik> GetAll()
        {
            var json = File.ReadAllText(fileLocation);
            var korisnici = JsonConvert.DeserializeObject<List<Korisnik>>(json);
            return korisnici;
        }

        public void Save(Korisnik noviKorisnik)
        {
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
    }
}
