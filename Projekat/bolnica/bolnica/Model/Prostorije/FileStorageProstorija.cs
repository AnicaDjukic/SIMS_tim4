using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Model.Prostorije
{
    public class FileStorageProstorija
    {
        private string fileLocationProstorije;
        private string fileLocationBolnickeSobe;

        public FileStorageProstorija()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            fileLocationProstorije = System.IO.Path.Combine(path, @"Resources\", "Prostorije.json");
            fileLocationBolnickeSobe = System.IO.Path.Combine(path, @"Resources\", "BolnickeSobe.json");
        }
        public List<Prostorija> GetAllProstorije()
        {
            FileStorageZaliha.serializeProstorija = true;
            string json = File.ReadAllText(fileLocationProstorije);
            List<Prostorija> prostorije = JsonConvert.DeserializeObject<List<Prostorija>>(json);
            return prostorije;
        }

        public List<BolnickaSoba> GetAllBolnickeSobe()
        {

            FileStorageZaliha.serializeProstorija = true;
            string json = File.ReadAllText(fileLocationBolnickeSobe);
            List<BolnickaSoba> prostorije = JsonConvert.DeserializeObject<List<BolnickaSoba>>(json);
            return prostorije;
        }

        public void Save(Prostorija novaProstorija)
        {
            FileStorageZaliha.serializeProstorija = true;
            string json = File.ReadAllText(fileLocationProstorije);
            List<Prostorija> prostorije = JsonConvert.DeserializeObject<List<Prostorija>>(json);
            if (prostorije == null)
            {
                prostorije = new List<Prostorija>();
            }
            prostorije.Add(novaProstorija);
            File.WriteAllText(fileLocationProstorije, JsonConvert.SerializeObject(prostorije));
        }

        public void Save(BolnickaSoba novaBolnickaSoba)
        {
            FileStorageZaliha.serializeProstorija = true;
            string json = File.ReadAllText(fileLocationBolnickeSobe);
            List<BolnickaSoba> bolnickeSobe = JsonConvert.DeserializeObject<List<BolnickaSoba>>(json);
            if (bolnickeSobe == null)
            {
                bolnickeSobe = new List<BolnickaSoba>();
            }
            bolnickeSobe.Add(novaBolnickaSoba);
            File.WriteAllText(fileLocationBolnickeSobe, JsonConvert.SerializeObject(bolnickeSobe));
        }

        public void Delete(Prostorija prostorija)
        {
            FileStorageZaliha.serializeProstorija = true;
            string json = File.ReadAllText(fileLocationProstorije);
            List<Prostorija> prostorije = JsonConvert.DeserializeObject<List<Prostorija>>(json);
            if (prostorije != null)
            {
                for (int i = 0; i < prostorije.Count; i++)
                {
                    if (prostorije[i].BrojProstorije == prostorija.BrojProstorije)
                    {
                        prostorije.Remove(prostorije[i]);
                        break;
                    }
                }
                File.WriteAllText(fileLocationProstorije, JsonConvert.SerializeObject(prostorije));
            }
        }

        public void Delete(BolnickaSoba bolnickaSoba)
        {
            FileStorageZaliha.serializeProstorija = true;
            string json = File.ReadAllText(fileLocationBolnickeSobe);
            List<BolnickaSoba> prostorije = JsonConvert.DeserializeObject<List<BolnickaSoba>>(json);
            if (prostorije != null)
            {
                for (int i = 0; i < prostorije.Count; i++)
                {
                    if (prostorije[i].BrojProstorije == bolnickaSoba.BrojProstorije)
                    {
                        prostorije.Remove(prostorije[i]);
                        break;
                    }
                }
                File.WriteAllText(fileLocationBolnickeSobe, JsonConvert.SerializeObject(prostorije));
            }
        }

    }
}