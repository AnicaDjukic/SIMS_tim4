using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using bolnica.Forms;
using System.Collections.ObjectModel;

namespace Model.Prostorije
{
    public class FileStorageProstorija
    {
        private string fileLocationProstorije;
        private string fileLocationBolnickeSobe;

        public FileStorageProstorija()
        {
            fileLocationProstorije = @"E:\SIMS_tim4\Projekat\bolnica\bolnica\Resources\Prostorije.json";
            fileLocationBolnickeSobe = @"E:\SIMS_tim4\Projekat\bolnica\bolnica\Resources\BolnickeSobe.json";
        }
        public List<Prostorija> GetAllProstorije()
        {
            var json = File.ReadAllText(fileLocationProstorije);
            var prostorije = JsonConvert.DeserializeObject<List<Prostorija>>(json);
            return prostorije;   
        }

        public List<BolnickaSoba> GetAllBolnickeSobe()
        {
            var json = File.ReadAllText(fileLocationBolnickeSobe);
            var prostorije = JsonConvert.DeserializeObject<List<BolnickaSoba>>(json);
            return prostorije;
        }

        public void Save(Prostorija novaProstorija)
        {
            var json = File.ReadAllText(fileLocationProstorije);
            List<Prostorija> prostorije = JsonConvert.DeserializeObject<List<Prostorija>>(json);
            if(prostorije == null)
            {
                prostorije = new List<Prostorija>();
            }
            prostorije.Add(novaProstorija);
            File.WriteAllText(fileLocationProstorije, JsonConvert.SerializeObject(prostorije));
        }

        public void Save(BolnickaSoba novaBolnickaSoba)
        {
            var json = File.ReadAllText(fileLocationBolnickeSobe);
            var bolnickeSobe = JsonConvert.DeserializeObject<List<BolnickaSoba>>(json);
            if (bolnickeSobe == null)
            {
                bolnickeSobe = new List<BolnickaSoba>();
            }
            bolnickeSobe.Add(novaBolnickaSoba);
            File.WriteAllText(fileLocationBolnickeSobe, JsonConvert.SerializeObject(bolnickeSobe));
        }

    }
}