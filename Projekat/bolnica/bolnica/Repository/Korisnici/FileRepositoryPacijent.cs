using Newtonsoft.Json;
using Model.Korisnici;
using System;
using System.Collections.Generic;
using System.IO;
using Model.Pregledi;
using Bolnica.Repository.Korisnici;

namespace Model.Pacijenti
{
   public class FileRepositoryPacijent : IRepositoryPacijent
   {
        private string fileLocation;

        public static bool serializeAlergeni;
        public static bool serializeZdravstveniKarton;
        public FileRepositoryPacijent() {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            fileLocation = System.IO.Path.Combine(path, @"Resources", "Pacijenti.json");
        }

        public List<Pacijent> GetAll()
        {
            serializeAlergeni = false;
            serializeZdravstveniKarton = false;
            FileRepositoryPregled.serializeKorisnik = true;
            var json = File.ReadAllText(fileLocation);
            var pacijenti = JsonConvert.DeserializeObject<List<Pacijent>>(json);
            return pacijenti;
        }
      
        public void Save(Pacijent noviPacijent)
        {
            serializeAlergeni = false;
            serializeZdravstveniKarton = false;
            FileRepositoryPregled.serializeKorisnik = true;
            var json = File.ReadAllText(fileLocation);
            List<Pacijent> pacijenti = JsonConvert.DeserializeObject<List<Pacijent>>(json);
            if (pacijenti == null)
            {
                pacijenti = new List<Pacijent>();
            }
            pacijenti.Add(noviPacijent);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(pacijenti));
        }

        public void Update(Pacijent pacijent)
        {
            serializeAlergeni = false;
            serializeZdravstveniKarton = false;
            FileRepositoryPregled.serializeKorisnik = true;
            List<Pacijent> pacijenti = new List<Pacijent>();
            pacijenti = GetAll();

            for (int i = 0; i < pacijenti.Count; i++)
            {
                if (pacijenti[i].Jmbg.Equals(pacijent.Jmbg))
                {
                    pacijenti[i] = pacijent;
                    break;
                }
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(pacijenti));
        }

        public void Delete(Pacijent pacijent)
        {
            serializeAlergeni = false;
            serializeZdravstveniKarton = false;
            FileRepositoryPregled.serializeKorisnik = true;
            var json = File.ReadAllText(fileLocation);
            List<Pacijent> pacijenti = JsonConvert.DeserializeObject<List<Pacijent>>(json);
            if (pacijenti != null)
            {
                for (int i = 0; i < pacijenti.Count; i++)
                {
                    if (pacijenti[i].Jmbg == pacijent.Jmbg)
                    {
                        pacijenti.Remove(pacijenti[i]);
                        break;
                    }
                }
                File.WriteAllText(fileLocation, JsonConvert.SerializeObject(pacijenti));
            }
        }

        public Pacijent GetById(string id)
        {
            serializeAlergeni = false;
            serializeZdravstveniKarton = false;
            FileRepositoryPregled.serializeKorisnik = true;
            var json = File.ReadAllText(fileLocation);
            var pacijenti = JsonConvert.DeserializeObject<List<Pacijent>>(json);

            Pacijent pacijent = new Pacijent();
            foreach (Pacijent p in pacijenti)
                if (p.Jmbg == id)
                    pacijent = p;
            return pacijent;
        }
    }
}