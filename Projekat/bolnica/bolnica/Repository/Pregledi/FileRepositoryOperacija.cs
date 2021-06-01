using Bolnica.Model.Pregledi;
using Bolnica.Repository.Prostorije;

using Model.Pregledi;
using Model.Prostorije;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Repository.Pregledi
{
    public class FileRepositoryOperacija : IRepositoryOperacija
    {
        private string fileLocation;
        public static bool serializeKorisnik;
        public FileRepositoryOperacija()
        {
            FileRepositoryPregled.serializeKorisnik = false;
            FileRepositoryZaliha.serializeProstorija = false;
            FileRepositoryAnamneza.serializeAnamneza = false;
            string FileLocation = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            fileLocation = System.IO.Path.Combine(FileLocation, @"Resources\", "Operacije.json");
        }

        public List<Operacija> GetAll()
        {
            FileRepositoryPregled.serializeKorisnik = false;
            FileRepositoryZaliha.serializeProstorija = false;
            FileRepositoryAnamneza.serializeAnamneza = false;
            var json = File.ReadAllText(fileLocation);
            var operacije = JsonConvert.DeserializeObject<List<Operacija>>(json);
            if (operacije == null)
            {
                operacije = new List<Operacija>();
            }
            return operacije;
        }

        public void Save(Operacija novaOperacija)
        {
            FileRepositoryPregled.serializeKorisnik = false;
            FileRepositoryZaliha.serializeProstorija = false;
            FileRepositoryAnamneza.serializeAnamneza = false;
            List<Operacija> noveOperacije = new List<Operacija>();
            noveOperacije = GetAll();
            noveOperacije.Add(novaOperacija);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(noveOperacije));
        }

        public void Update(Operacija novaOperacija)
        {
            FileRepositoryPregled.serializeKorisnik = false;
            FileRepositoryZaliha.serializeProstorija = false;
            FileRepositoryAnamneza.serializeAnamneza = false;
            List<Operacija> noveOperacije = new List<Operacija>();
            noveOperacije = GetAll();
            for (int i = 0; i < noveOperacije.Count; i++)
            {
                if (noveOperacije[i].Id.Equals(novaOperacija.Id))
                {
                    noveOperacije[i] = novaOperacija;
                    break;
                }
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(noveOperacije));
        }

        public void Delete(Operacija novaOperacija)
        {
            FileRepositoryPregled.serializeKorisnik = false;
            FileRepositoryZaliha.serializeProstorija = false;
            FileRepositoryAnamneza.serializeAnamneza = false;
            List<Operacija> noveOperacije = new List<Operacija>();
            noveOperacije = GetAll();
            for (int i = 0; i < noveOperacije.Count; i++)
            {
                if (noveOperacije[i].Id.Equals(novaOperacija.Id))
                {
                    noveOperacije.RemoveAt(i);
                    break;
                }
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(noveOperacije));
        }

        public Operacija GetById(int id)
        {
            FileRepositoryPregled.serializeKorisnik = false;
            FileRepositoryZaliha.serializeProstorija = false;
            FileRepositoryAnamneza.serializeAnamneza = false;
            var json = File.ReadAllText(fileLocation);
            var operacije = JsonConvert.DeserializeObject<List<Operacija>>(json);

            Operacija operacija = new Operacija();
            foreach (Operacija o in operacije)
                if (o.Id == id)
                    operacija = o;
            return operacija;
        }
    }
}
