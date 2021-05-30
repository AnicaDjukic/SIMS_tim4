using Bolnica.Model.Pregledi;
using Model.Pregledi;
using Model.Prostorije;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Model.Prostorije
{
    public class FileStorageHospitalizacija
    {
        public string FileLocationHospitalizacija { get; set; }
        public FileStorageHospitalizacija()
        {
            FileRepositoryPregled.serializeKorisnik = false;
            FileStorageZaliha.serializeProstorija = false;
            string FileLocation = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            FileLocationHospitalizacija = System.IO.Path.Combine(FileLocation, @"Resources\", "Hospitalizacija.json");

        }

        public List<Hospitalizacija> GetAll()
        {
            FileRepositoryPregled.serializeKorisnik = false;
            FileStorageZaliha.serializeProstorija = false;
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
            FileStorageZaliha.serializeProstorija = false;
            List<Hospitalizacija> noveHospitalizacije = new List<Hospitalizacija>();
            noveHospitalizacije = GetAll();
            noveHospitalizacije.Add(novaHospitalizacija);
            File.WriteAllText(FileLocationHospitalizacija, JsonConvert.SerializeObject(noveHospitalizacije));
        }

        public void Delete(Hospitalizacija novaHospitalizacija)
        {
            FileRepositoryPregled.serializeKorisnik = false;
            FileStorageZaliha.serializeProstorija = false;
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

       
    }
}
