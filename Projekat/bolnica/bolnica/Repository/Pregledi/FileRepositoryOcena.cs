﻿using Model.Korisnici;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Repository.Pregledi
{
    public class FileRepositoryOcena : IRepositoryOcena
    {
        public string FileLocation { get; set; }

        public FileRepositoryOcena()
        {
            FileRepositoryPregled.serializeKorisnik = false;
            string putanja = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            FileLocation = System.IO.Path.Combine(putanja, @"Resources\", "Ocene.json");
        }

        public void Delete(Ocena entity)
        {
            FileRepositoryPregled.serializeKorisnik = false;
            List<Ocena> ocene = GetAll();
            foreach (Ocena o in ocene)
            {
                if (entity.IdOcene.Equals(o.IdOcene))
                {
                    ocene.Remove(o);
                    break;
                }
            }
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(ocene));
        }

        public List<Ocena> GetAll()
        {
            FileRepositoryPregled.serializeKorisnik = false;
            var json = File.ReadAllText(FileLocation);
            var ocene = JsonConvert.DeserializeObject<List<Ocena>>(json);
            if (ocene is null)
            {
                ocene = new List<Ocena>();
            }
            return ocene;
        }

        public Ocena GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(Ocena newEntity)
        {
            FileRepositoryPregled.serializeKorisnik = false;
            List<Ocena> ocene = GetAll();
            ocene.Add(newEntity);
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(ocene));
        }

        public void Update(Ocena entity)
        {
            FileRepositoryPregled.serializeKorisnik = false;
            List<Ocena> ocene = GetAll();
            foreach (Ocena ocena in ocene)
            {
                if (entity.IdOcene.Equals(ocena.IdOcene))
                {
                    ocene.Remove(ocena);
                    ocene.Add(entity);
                    break;
                }
            }
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(ocene));
        }
    }
}
