using Bolnica.Model.Pregledi;
using Bolnica.Repository.Prostorije;
using Model.Pregledi;
using Model.Prostorije;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Model.Prostorije
{
    public class FileRepositoryHospitalizacija : IRepositoryHospitalizacija
    {
        public string FileLocationHospitalizacija { get; set; }
        public FileRepositoryHospitalizacija()
        {
            FileRepositoryPregled.serializeKorisnik = false;
            FileRepositoryZaliha.serializeProstorija = false;
            string FileLocation = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            FileLocationHospitalizacija = System.IO.Path.Combine(FileLocation, @"Resources\", "Hospitalizacija.json");

        }

        public List<Hospitalizacija> GetAll()
        {
            FileRepositoryPregled.serializeKorisnik = false;
            FileRepositoryZaliha.serializeProstorija = false;
            var json = File.ReadAllText(FileLocationHospitalizacija);
            var pregledi = JsonConvert.DeserializeObject<List<Hospitalizacija>>(json);
            if (pregledi == null)
            {
                pregledi = new List<Hospitalizacija>();
            }
            return pregledi;
        }


        public void Save(Hospitalizacija novaHospitalizacija)
        {
            FileRepositoryPregled.serializeKorisnik = false;
            FileRepositoryZaliha.serializeProstorija = false;
            List<Hospitalizacija> noveHospitalizacije = new List<Hospitalizacija>();
            noveHospitalizacije = GetAll();
            noveHospitalizacije.Add(novaHospitalizacija);
            File.WriteAllText(FileLocationHospitalizacija, JsonConvert.SerializeObject(noveHospitalizacije));
        }

        public void Delete(Hospitalizacija novaHospitalizacija)
        {
            FileRepositoryPregled.serializeKorisnik = false;
            FileRepositoryZaliha.serializeProstorija = false;
            List<Hospitalizacija> noveHospitalizacije = new List<Hospitalizacija>();
            noveHospitalizacije = GetAll();

            for (int i = 0; i < noveHospitalizacije.Count; i++)
            {
                if (noveHospitalizacije[i].Id.Equals(novaHospitalizacija.Id))
                {
                    noveHospitalizacije.RemoveAt(i);
                    break;
                }
            }
            File.WriteAllText(FileLocationHospitalizacija, JsonConvert.SerializeObject(noveHospitalizacije));

        }

        public void Update(Hospitalizacija novaHospitalizacija)
        {
            FileRepositoryPregled.serializeKorisnik = false;
            FileRepositoryZaliha.serializeProstorija = false;
            List<Hospitalizacija> noveHospitalizacije = new List<Hospitalizacija>();
            noveHospitalizacije = GetAll();

            for (int i = 0; i < noveHospitalizacije.Count; i++)
            {
                if (noveHospitalizacije[i].Id.Equals(novaHospitalizacija.Id))
                {
                    noveHospitalizacije[i] = novaHospitalizacija;
                    break;
                }
            }
            File.WriteAllText(FileLocationHospitalizacija, JsonConvert.SerializeObject(noveHospitalizacije));

        }

        public Hospitalizacija GetById(int id)
        {
            FileRepositoryPregled.serializeKorisnik = false;
            FileRepositoryZaliha.serializeProstorija = false;
            var json = File.ReadAllText(FileLocationHospitalizacija);
            var hospitalizacije = JsonConvert.DeserializeObject<List<Hospitalizacija>>(json);

            Hospitalizacija hospitalizacija = new Hospitalizacija();
            foreach (Hospitalizacija p in hospitalizacije)
                if (p.Id == id)
                    hospitalizacija = p;
            return hospitalizacija;
        }
    }
}
