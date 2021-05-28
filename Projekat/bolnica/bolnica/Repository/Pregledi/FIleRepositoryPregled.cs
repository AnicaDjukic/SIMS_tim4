using Bolnica.Repository.Prostorije;
using Model.Pregledi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Repository.Pregledi
{
    public class FileRepositoryPregled : IRepositoryPregled
    {
        public string FileLocation { get; set; }
        public static bool serializeKorisnik;

        public FileRepositoryPregled()
        {
            serializeKorisnik = false;
            FileRepositoryZaliha.serializeProstorija = false;
            FileRepositoryAnamneza.serializeAnamneza = false;
            string putanja = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            FileLocation = System.IO.Path.Combine(putanja, @"Resources\", "Pregledi.json");
        }

        public void Delete(Pregled entity)
        {
            serializeKorisnik = false;
            FileRepositoryZaliha.serializeProstorija = false;
            FileRepositoryAnamneza.serializeAnamneza = false;
            List<Pregled> pregledi = GetAll();
            for (int i = 0; i < pregledi.Count; i++)
            {
                if (entity.Id.Equals(pregledi[i].Id))
                {
                    pregledi.RemoveAt(i);
                    break;
                }
            }
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(pregledi));
        }

        public List<Pregled> GetAll()
        {
            serializeKorisnik = false;
            FileRepositoryZaliha.serializeProstorija = false;
            FileRepositoryAnamneza.serializeAnamneza = false;
            var json = File.ReadAllText(FileLocation);
            var pregledi = JsonConvert.DeserializeObject<List<Pregled>>(json);
            if (pregledi == null)
            {
                pregledi = new List<Pregled>();
            }
            return pregledi;
        }

        public Pregled GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(Pregled newEntity)
        {
            serializeKorisnik = false;
            FileRepositoryZaliha.serializeProstorija = false;
            FileRepositoryAnamneza.serializeAnamneza = false;
            List<Pregled> pregledi = GetAll();
            pregledi.Add(newEntity);
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(pregledi));
        }

        public void Update(Pregled entity)
        {
            serializeKorisnik = false;
            FileRepositoryZaliha.serializeProstorija = false;
            FileRepositoryAnamneza.serializeAnamneza = false;
            List<Pregled> pregledi = GetAll();
            for (int i = 0; i < pregledi.Count; i++)
            {
                if (entity.Id.Equals(pregledi[i].Id))
                {
                    pregledi[i] = entity;
                    break;
                }
            }
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(pregledi));
        }
    }
}
