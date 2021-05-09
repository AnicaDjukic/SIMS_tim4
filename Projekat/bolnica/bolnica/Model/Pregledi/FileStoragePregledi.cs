using Bolnica.Model.Pregledi;
using Model.Prostorije;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Model.Pregledi
{
    public class FileStoragePregledi
    {
        public string FileLocationPregledi { get; set; }

        public string FileLocationOperacije { get; set; }

        public static bool serializeKorisnik;
        public FileStoragePregledi()
        {
            serializeKorisnik = false;
            FileStorageZaliha.serializeProstorija = false;
            FileStorageAnamneza.serializeAnamneza = false;
            string FileLocation = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            FileLocationPregledi = System.IO.Path.Combine(FileLocation, @"Resources\", "Pregledi.json");
            FileLocationOperacije = System.IO.Path.Combine(FileLocation, @"Resources\", "Operacije.json");

        }

        public List<Pregled> GetAllPregledi()
        {
            serializeKorisnik = false;
            FileStorageZaliha.serializeProstorija = false;
            FileStorageAnamneza.serializeAnamneza = false;
            var json = File.ReadAllText(FileLocationPregledi);
            var pregledi = JsonConvert.DeserializeObject<List<Pregled>>(json);
            if (pregledi?.Count == null)
            {
                pregledi = new List<Pregled>();
            }
            return pregledi;
        }

        public List<Operacija> GetAllOperacije()
        {
            serializeKorisnik = false;
            FileStorageZaliha.serializeProstorija = false;
            FileStorageAnamneza.serializeAnamneza = false;
            var json = File.ReadAllText(FileLocationOperacije);
            var operacije = JsonConvert.DeserializeObject<List<Operacija>>(json);
            if (operacije?.Count == null)
            {
                operacije = new List<Operacija>();
            }
            return operacije;
        }

        public void Save(Pregled noviPregled)
        {
            serializeKorisnik = false;
            FileStorageZaliha.serializeProstorija = false;
            FileStorageAnamneza.serializeAnamneza = false;
            List<Pregled> noviPregledi = new List<Pregled>();
            noviPregledi = GetAllPregledi();
            noviPregledi.Add(noviPregled);
            File.WriteAllText(FileLocationPregledi, JsonConvert.SerializeObject(noviPregledi));
        }

        public void Save(Operacija novaOperacija)
        {
            serializeKorisnik = false;
            FileStorageZaliha.serializeProstorija = false;
            FileStorageAnamneza.serializeAnamneza = false;
            List<Operacija> noveOperacije = new List<Operacija>();
            noveOperacije = GetAllOperacije();
            noveOperacije.Add(novaOperacija);
            File.WriteAllText(FileLocationOperacije, JsonConvert.SerializeObject(noveOperacije));
        }

        public void Izmeni(Pregled noviPregled)
        {
            serializeKorisnik = false;
            FileStorageZaliha.serializeProstorija = false;
            FileStorageAnamneza.serializeAnamneza = false;
            List<Pregled> noviPregledi = new List<Pregled>();
            noviPregledi = GetAllPregledi();

            for (int i = 0; i < noviPregledi.Count; i++)
            {
                if (noviPregledi[i].Id.Equals(noviPregled.Id))
                {
                    noviPregledi[i] = noviPregled;
                    break;
                }
            }
            File.WriteAllText(FileLocationPregledi, JsonConvert.SerializeObject(noviPregledi));

        }

        public void Izmeni(Operacija novaOperacija)
        {
            serializeKorisnik = false;
            FileStorageZaliha.serializeProstorija = false;
            FileStorageAnamneza.serializeAnamneza = false;
            List<Operacija> noveOperacije = new List<Operacija>();
            noveOperacije = GetAllOperacije();
            for (int i = 0; i < noveOperacije.Count; i++)
            {
                if (noveOperacije[i].Id.Equals(novaOperacija.Id))
                {
                    noveOperacije[i] = novaOperacija;
                    break;
                }
            }
            File.WriteAllText(FileLocationOperacije, JsonConvert.SerializeObject(noveOperacije));
        }
        public void Delete(Pregled noviPregled)
        {
            serializeKorisnik = false;
            FileStorageZaliha.serializeProstorija = false;
            FileStorageAnamneza.serializeAnamneza = false;
            List<Pregled> noviPregledi = new List<Pregled>();
            noviPregledi = GetAllPregledi();

            for (int i = 0; i < noviPregledi.Count; i++)
            {
                if (noviPregledi[i].Id.Equals(noviPregled.Id))
                {
                    noviPregledi.RemoveAt(i);
                    break;
                }
            }
            File.WriteAllText(FileLocationPregledi, JsonConvert.SerializeObject(noviPregledi));

        }

        public void Delete(Operacija novaOperacija)
        {
            serializeKorisnik = false;
            FileStorageZaliha.serializeProstorija = false;
            FileStorageAnamneza.serializeAnamneza = false;
            List<Operacija> noveOperacije = new List<Operacija>();
            noveOperacije = GetAllOperacije();
            for (int i = 0; i < noveOperacije.Count; i++)
            {
                if (noveOperacije[i].Id.Equals(novaOperacija.Id))
                {
                    noveOperacije.RemoveAt(i);
                    break;
                }
            }
            File.WriteAllText(FileLocationOperacije, JsonConvert.SerializeObject(noveOperacije));
        }
    }

}