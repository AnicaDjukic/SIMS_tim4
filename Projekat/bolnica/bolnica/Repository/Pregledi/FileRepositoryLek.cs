using Bolnica.Repository.Korisnici;
using Model.Pacijenti;

using Model.Pregledi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Repository.Pregledi
{
    public class FileRepositoryLek : IRepositoryLek
    {
        private string fileLocation;
        public static bool serializeLek;
        public FileRepositoryLek()
        {
            serializeLek = true;
            string putanja = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            fileLocation = System.IO.Path.Combine(putanja, @"Resources\", "Lekovi.json");
        }

        public List<Lek> GetAll()
        {
            serializeLek = true;
            FileRepositoryPacijent.serializeAlergeni = false;
            var json = File.ReadAllText(fileLocation);
            var lekovi = JsonConvert.DeserializeObject<List<Lek>>(json);
            if (lekovi?.Count == null)
            {
                lekovi = new List<Lek>();
            }
            return lekovi;
        }

        public void Save(Lek noviLek)
        {
            serializeLek = true;
            FileRepositoryPacijent.serializeAlergeni = false;
            var json = File.ReadAllText(fileLocation);
            List<Lek> lekovi = JsonConvert.DeserializeObject<List<Lek>>(json);
            if (lekovi == null)
            {
                lekovi = new List<Lek>();
            }
            lekovi.Add(noviLek);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(lekovi));
        }

        public void Update(Lek noviLek)
        {
            serializeLek = true;
            FileRepositoryPacijent.serializeAlergeni = false;
            List<Lek> lekovi = GetAll();
            for (int i = 0; i < lekovi.Count; i++)
            {
                if (noviLek.Id.Equals(lekovi[i].Id))
                {
                    lekovi[i] = noviLek;
                    break;
                }
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(lekovi));
        }

        public void Delete(Lek lekZaBrisanje)
        {
            serializeLek = true;
            FileRepositoryPacijent.serializeAlergeni = false;
            var json = File.ReadAllText(fileLocation);
            List<Lek> lekovi = JsonConvert.DeserializeObject<List<Lek>>(json);
            if (lekovi != null)
            {
                for (int i = 0; i < lekovi.Count; i++)
                {
                    if (lekovi[i].Id == lekZaBrisanje.Id)
                    {
                        lekovi.Remove(lekovi[i]);
                        break;
                    }
                }
                File.WriteAllText(fileLocation, JsonConvert.SerializeObject(lekovi));
            }
        }
        public Lek GetById(int id)
        {
            serializeLek = true;
            FileRepositoryPacijent.serializeAlergeni = false;
            Lek rezultat = new Lek();
            var json = File.ReadAllText(fileLocation);
            List<Lek> lekovi = JsonConvert.DeserializeObject<List<Lek>>(json);
            if (lekovi != null)
            {
                for (int i = 0; i < lekovi.Count; i++)
                {
                    if (lekovi[i].Id == id)
                    {
                        rezultat = lekovi[i];
                        break;
                    }
                }
            }
            return rezultat;
        }
    }
}
