using Bolnica.Repository.Prostorije;
using Model.Pregledi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Repository.Pregledi
{
    public class FileRepositoryOperacija : IRepositoryOperacija
    {
        public string FileLocation { get; set; }
        public static bool serializeKorisnik;

        public FileRepositoryOperacija()
        {
            serializeKorisnik = false;
            FileRepositoryZaliha.serializeProstorija = false;
            FileRepositoryAnamneza.serializeAnamneza = false;
            string putanja = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            FileLocation = System.IO.Path.Combine(putanja, @"Resources\", "Operacije.json");
        }

        public void Delete(Operacija entity)
        {
            serializeKorisnik = false;
            FileRepositoryZaliha.serializeProstorija = false;
            FileRepositoryAnamneza.serializeAnamneza = false;
            List<Operacija> operacije = GetAll();
            for (int i = 0; i < operacije.Count; i++)
            {
                if (entity.Id.Equals(operacije[i].Id))
                {
                    operacije.RemoveAt(i);
                    break;
                }
            }
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(operacije));
        }

        public List<Operacija> GetAll()
        {
            serializeKorisnik = false;
            FileRepositoryZaliha.serializeProstorija = false;
            FileRepositoryAnamneza.serializeAnamneza = false;
            var json = File.ReadAllText(FileLocation);
            var operacije = JsonConvert.DeserializeObject<List<Operacija>>(json);
            if (operacije is null)
            {
                operacije = new List<Operacija>();
            }
            return operacije;
        }

        public Operacija GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(Operacija newEntity)
        {
            serializeKorisnik = false;
            FileRepositoryZaliha.serializeProstorija = false;
            FileRepositoryAnamneza.serializeAnamneza = false;
            List<Operacija> operacije = GetAll();
            operacije.Add(newEntity);
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(operacije));
        }

        public void Update(Operacija entity)
        {
            serializeKorisnik = false;
            FileRepositoryZaliha.serializeProstorija = false;
            FileRepositoryAnamneza.serializeAnamneza = false;
            List<Operacija> operacije = GetAll();
            for (int i = 0; i < operacije.Count; i++)
            {
                if (entity.Id.Equals(operacije[i].Id))
                {
                    operacije[i] = entity;
                    break;
                }
            }
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(operacije));
        }
    }
}
