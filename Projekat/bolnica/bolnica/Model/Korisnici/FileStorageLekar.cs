using Model.Korisnici;
using Model.Pregledi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Model.Korisnici
{
    class FileStorageLekar
    {

        public string fileLocation { get; set; }

        public FileStorageLekar()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            fileLocation = System.IO.Path.Combine(path, @"Resources\", "Lekari.json");
        }

        public List<Lekar> GetAll()
        {
            FileStoragePregledi.serializeKorisnik = true;
            var json = File.ReadAllText(fileLocation);
            var lekari = JsonConvert.DeserializeObject<List<Lekar>>(json);
            return lekari;
        }

        public void Save(Lekar noviLekar)
        {
            FileStoragePregledi.serializeKorisnik = true;
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
            FileStoragePregledi.serializeKorisnik = true;
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
    }
}
