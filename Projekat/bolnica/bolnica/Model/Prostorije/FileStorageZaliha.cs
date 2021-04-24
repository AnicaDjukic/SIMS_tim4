﻿using Bolnica.Model.Prostorije;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Model.Prostorije
{
    public class FileStorageZaliha
    {
        private string fileLocation;

        public FileStorageZaliha()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            fileLocation = System.IO.Path.Combine(path, @"Resources\", "Zalihe.json");
        }
        public List<Zaliha> GetAll()
        {
            var json = File.ReadAllText(fileLocation);
            List<Zaliha> zalihe = JsonConvert.DeserializeObject<List<Zaliha>>(json);
            return zalihe;
        }

        public void Save(Zaliha novaZaliha)
        {
            var json = File.ReadAllText(fileLocation);
            List<Zaliha> zalihe = JsonConvert.DeserializeObject<List<Zaliha>>(json);
            if (zalihe == null)
            {
                zalihe = new List<Zaliha>();
            }
            zalihe.Add(novaZaliha);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(zalihe));
        }

        public void Delete(Zaliha zaliha)
        {
            var json = File.ReadAllText(fileLocation);
            List<Zaliha> zalihe = JsonConvert.DeserializeObject<List<Zaliha>>(json);
            if (zalihe != null)
            {
                for (int i = 0; i < zalihe.Count; i++)
                {
                    if (zalihe[i].BrojProstorije == zaliha.BrojProstorije && zalihe[i].SifraOpreme == zaliha.SifraOpreme)
                    {
                        zalihe.Remove(zalihe[i]);
                        break;
                    }
                }
                File.WriteAllText(fileLocation, JsonConvert.SerializeObject(zalihe));
            }
        }
    }
}