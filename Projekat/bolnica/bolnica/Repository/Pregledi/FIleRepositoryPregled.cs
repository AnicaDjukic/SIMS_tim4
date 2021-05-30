using Bolnica.Model.Pregledi;
using Bolnica.Repository.Pregledi;
using Bolnica.Repository.Prostorije;
using Model.Prostorije;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Model.Pregledi
{
    public class FileRepositoryPregled : IRepositoryPregled
    {
        private string fileLocation;

        public static bool serializeKorisnik;
        public FileRepositoryPregled()
        {
            serializeKorisnik = false;
            FileRepositoryZaliha.serializeProstorija = false;
            FileRepositoryAnamneza.serializeAnamneza = false;
            string FileLocation = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            fileLocation = System.IO.Path.Combine(FileLocation, @"Resources\", "Pregledi.json");
        }

        public List<Pregled> GetAll()
        {
            serializeKorisnik = false;
            FileRepositoryZaliha.serializeProstorija = false;
            FileRepositoryAnamneza.serializeAnamneza = false;
            var json = File.ReadAllText(fileLocation);
            var pregledi = JsonConvert.DeserializeObject<List<Pregled>>(json);
            if(pregledi == null)
            {
                pregledi = new  List<Pregled>();
            }
            return pregledi;
        }

        public void Save(Pregled noviPregled)
        {
            serializeKorisnik = false;
            FileRepositoryZaliha.serializeProstorija = false;
            FileRepositoryAnamneza.serializeAnamneza = false;
            List<Pregled> noviPregledi = new List<Pregled>();
            noviPregledi = GetAll();
            noviPregledi.Add(noviPregled);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(noviPregledi));
        }

        public void Update(Pregled noviPregled)
        {
            serializeKorisnik = false;
            FileRepositoryZaliha.serializeProstorija = false;
            FileRepositoryAnamneza.serializeAnamneza = false;
            List<Pregled> noviPregledi = new List<Pregled>();
            noviPregledi = GetAll();

            for (int i = 0; i < noviPregledi.Count; i++)
            {
                if (noviPregledi[i].Id.Equals(noviPregled.Id))
                {
                    noviPregledi[i] = noviPregled;
                    break;
                }
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(noviPregledi));

        }

        public void Delete(Pregled noviPregled)
        {
            serializeKorisnik = false;
            FileRepositoryZaliha.serializeProstorija = false;
            FileRepositoryAnamneza.serializeAnamneza = false;
            List<Pregled> noviPregledi = new List<Pregled>();
            noviPregledi = GetAll();

            for (int i = 0; i < noviPregledi.Count; i++)
            {
                if (noviPregledi[i].Id.Equals(noviPregled.Id))
                {
                    noviPregledi.RemoveAt(i);
                    break;
                }
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(noviPregledi));
        }

        public Pregled GetById(int id)
        {
            serializeKorisnik = false;
            FileRepositoryZaliha.serializeProstorija = false;
            FileRepositoryAnamneza.serializeAnamneza = false;
            var json = File.ReadAllText(fileLocation);
            var pregledi = JsonConvert.DeserializeObject<List<Pregled>>(json);

            Pregled pregled = new Pregled();
            foreach (Pregled p in pregledi)
                if (p.Id == id)
                    pregled = p;
            return pregled;
        }
    }

}