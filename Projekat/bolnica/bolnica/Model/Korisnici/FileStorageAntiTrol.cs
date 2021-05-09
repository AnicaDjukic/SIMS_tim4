using Model.Pregledi;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Bolnica.Model.Korisnici
{
    class FileStorageAntiTrol
    {
        public string FileLocation { get; set; }

        public FileStorageAntiTrol()
        {
            FileStoragePregledi.serializeKorisnik = false;
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            FileLocation = System.IO.Path.Combine(path, @"Resources\", "AntiTrol.json");
        }

        public List<AntiTrol> GetAll()
        {
            FileStoragePregledi.serializeKorisnik = false;
            var json = File.ReadAllText(FileLocation);
            var antiTrol = JsonConvert.DeserializeObject<List<AntiTrol>>(json);
            return antiTrol;
        }

        public void Save(AntiTrol antiTrol)
        {
            FileStoragePregledi.serializeKorisnik = false;
            var json = File.ReadAllText(FileLocation);
            List<AntiTrol> antiTrolList = JsonConvert.DeserializeObject<List<AntiTrol>>(json);
            if (antiTrolList == null)
            {
                antiTrolList = new List<AntiTrol>();
            }
            antiTrolList.Add(antiTrol);
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(antiTrolList));
        }

        public void Delete(AntiTrol antiTrol)
        {
            FileStoragePregledi.serializeKorisnik = false;
            var json = File.ReadAllText(FileLocation);
            List<AntiTrol> antiTrolList = JsonConvert.DeserializeObject<List<AntiTrol>>(json);
            if (antiTrolList != null)
            {
                foreach (AntiTrol at in antiTrolList)
                {
                    if (at.Pacijent.Jmbg.Equals(antiTrol.Pacijent.Jmbg))
                    {
                        antiTrolList.Remove(at);
                        break;
                    }
                }
                File.WriteAllText(FileLocation, JsonConvert.SerializeObject(antiTrolList));
            }
        }
    }
}
