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
        private string fileLocation;

        public FileStorageProstorija()
        {
            fileLocation = @"E:\SIMS_tim4\Projekat\bolnica\bolnica\Resources\Rooms.json";
        }
        public List<Prostorija> GetAll()
        {
            var json = File.ReadAllText(fileLocation);
            var prostorije = JsonConvert.DeserializeObject<List<Prostorija>>(json);
            return prostorije;   
        }

        public void Save(Prostorija novaProstorija)
        {
            var json = File.ReadAllText(fileLocation);
            List<Prostorija> prostorije = JsonConvert.DeserializeObject<List<Prostorija>>(json);
            if(prostorije == null)
            {
                prostorije = new List<Prostorija>();
            }
            prostorije.Add(novaProstorija);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(prostorije));
        }

    }
}