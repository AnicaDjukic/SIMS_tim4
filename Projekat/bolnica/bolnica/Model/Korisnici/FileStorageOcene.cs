using Bolnica.Model.Korisnici;
using Model.Pregledi;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Bolnica.Model
{
    class FileStorageOcene
    {
        public string FileLocation { get; set; }

        public FileStorageOcene()
        {
            FileStoragePregledi.serializeKorisnik = false;
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            FileLocation = System.IO.Path.Combine(path, @"Resources\", "Ocene.json");
        }

        public List<Ocena> GetAll()
        {
            FileStoragePregledi.serializeKorisnik = false;
            var json = File.ReadAllText(FileLocation);
            var ocene = JsonConvert.DeserializeObject<List<Ocena>>(json);
            return ocene;
        }

        public void Save(Ocena novaOcena)
        {
            FileStoragePregledi.serializeKorisnik = false;
            var json = File.ReadAllText(FileLocation);
            List<Ocena> ocene = JsonConvert.DeserializeObject<List<Ocena>>(json);
            if (ocene == null)
            {
                ocene = new List<Ocena>();
            }
            ocene.Add(novaOcena);
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(ocene));
        }

        public void Delete(Ocena ocena)
        {
            FileStoragePregledi.serializeKorisnik = false;
            var json = File.ReadAllText(FileLocation);
            List<Ocena> ocene = JsonConvert.DeserializeObject<List<Ocena>>(json);
            if (ocene != null)
            {
                foreach (Ocena o in ocene)
                {
                    if (o.IdOcene.Equals(ocena.IdOcene))
                    {
                        ocene.Remove(o);
                        break;
                    }
                }
                File.WriteAllText(FileLocation, JsonConvert.SerializeObject(ocene));
            }
        }
    }
}
