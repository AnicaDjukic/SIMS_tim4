using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace Model.Pregledi
{
   public class FileStoragePregledi
   {
      
      public string FileLocationPregledi { get; set; }

      public string FileLocationOperacije { get; set; }
      public FileStoragePregledi()
        {
            string FileLocation = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            FileLocationPregledi = System.IO.Path.Combine(FileLocation, @"Resources\", "Pregledi.json");
            FileLocationOperacije = System.IO.Path.Combine(FileLocation, @"Resources\", "Operacije.json");

        }

      public List<Pregled> GetAllPregledi()
      {
            var json = File.ReadAllText(FileLocationPregledi);
            var pregledi = JsonConvert.DeserializeObject<List<Pregled>>(json);
            return pregledi;
        }

      public List<Operacija> GetAllOperacije()
      {
            var json = File.ReadAllText(FileLocationOperacije);
            var operacije = JsonConvert.DeserializeObject<List<Operacija>>(json);
            return operacije;
        }

        public void Save(Pregled noviPregled)
      {
            List<Pregled> noviPregledi = new List<Pregled>();
            noviPregledi = GetAllPregledi();
            noviPregledi.Add(noviPregled);
            File.WriteAllText(FileLocationPregledi, JsonConvert.SerializeObject(noviPregledi));
        }
      
        public void Save(Operacija novaOperacija)
        {
            List<Operacija> noveOperacije = new List<Operacija>();
            noveOperacije = GetAllOperacije();
            noveOperacije.Add(novaOperacija);
            File.WriteAllText(FileLocationOperacije, JsonConvert.SerializeObject(noveOperacije));
        }

        public void Izmeni(Pregled noviPregled)
        {
            List<Pregled> noviPregledi = new List<Pregled>();
            noviPregledi = GetAllPregledi();

            for (int i = 0; i < noviPregledi.Count; i++)
            {
                if (noviPregledi[i].Trajanje.Equals(noviPregled.Trajanje))
                {
                    noviPregledi[i] = noviPregled;
                    break;
                }
            }
            File.WriteAllText(FileLocationPregledi, JsonConvert.SerializeObject(noviPregledi));

        }

        public void Izmeni(Operacija novaOperacija)
        {
            List<Operacija> noveOperacije = new List<Operacija>();
            noveOperacije = GetAllOperacije();
            for (int i = 0; i < noveOperacije.Count; i++)
            {
                if (noveOperacije[i].Trajanje.Equals(novaOperacija.Trajanje))
                {
                    noveOperacije[i]=novaOperacija;
                    break;
                }
            }
            File.WriteAllText(FileLocationOperacije, JsonConvert.SerializeObject(noveOperacije));
        }
        public void Delete(Pregled noviPregled)
        {
            List<Pregled> noviPregledi = new List<Pregled>();
            noviPregledi = GetAllPregledi();

            for (int i = 0; i < noviPregledi.Count; i++)
                {
                    if (noviPregledi[i].Trajanje.Equals(noviPregled.Trajanje))
                    {
                        noviPregledi.RemoveAt(i);
                        break;
                    }
                }
                File.WriteAllText(FileLocationPregledi, JsonConvert.SerializeObject(noviPregledi));
            
        }

        public void Delete(Operacija novaOperacija)
        {
            List<Operacija> noveOperacije = new List<Operacija>();
            noveOperacije = GetAllOperacije();
                for (int i = 0; i < noveOperacije.Count; i++)
                {
                    if (noveOperacije[i].Trajanje.Equals(novaOperacija.Trajanje))
                    {
                        noveOperacije.RemoveAt(i);
                        break;
                    }
                }
                File.WriteAllText(FileLocationOperacije, JsonConvert.SerializeObject(noveOperacije));
            }
        }
    
}