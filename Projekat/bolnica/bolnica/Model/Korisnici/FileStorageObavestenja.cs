﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Model.Pregledi;

namespace Bolnica.Model.Korisnici
{
    public class FileStorageObavestenja
    {
        private string fileLocation;

        public FileStorageObavestenja()
        {
            FileStoragePregledi.serializeKorisnik = false;
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            fileLocation = System.IO.Path.Combine(path, @"Resources", "Obavestenja.json");
        }

        public List<Obavestenje> GetAll()
        {
            FileStoragePregledi.serializeKorisnik = false;
            var json = File.ReadAllText(fileLocation);
            var obavestenja = JsonConvert.DeserializeObject<List<Obavestenje>>(json);
            return obavestenja;
        }

        public void Save(Obavestenje novoObavestenje)
        {
            FileStoragePregledi.serializeKorisnik = false;
            var json = File.ReadAllText(fileLocation);
            List<Obavestenje> obavestenja = JsonConvert.DeserializeObject<List<Obavestenje>>(json);
            if (obavestenja == null)
            {
                obavestenja = new List<Obavestenje>();
            }
            obavestenja.Add(novoObavestenje);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(obavestenja));
        }

        public void Delete(Obavestenje obavestenje)
        {
            FileStoragePregledi.serializeKorisnik = false;
            var json = File.ReadAllText(fileLocation);
            List<Obavestenje> obavestenja = JsonConvert.DeserializeObject<List<Obavestenje>>(json);
            if (obavestenja != null)
            {
                for (int i = 0; i < obavestenja.Count; i++)
                {
                    if (obavestenja[i].Id == obavestenje.Id)
                    {
                        obavestenja.Remove(obavestenja[i]);
                        break;
                    }
                }
                File.WriteAllText(fileLocation, JsonConvert.SerializeObject(obavestenja));
            }
        }
    }
}