using Bolnica.Repository.Korisnici;
using Model.Pacijenti;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Model.Pacijenti
{
    public class FileRepositoryZdravstveniKarton : IRepositoryZdravstveniKarton
    {
        private string fileLocation;

        public FileRepositoryZdravstveniKarton()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            fileLocation = System.IO.Path.Combine(path, @"Resources", "ZdravstveniKarton.json");
        }

        public List<ZdravstveniKarton> GetAll()
        {
            FileRepositoryPacijent.serializeZdravstveniKarton = true;
            var json = File.ReadAllText(fileLocation);
            var zdravstveniKartoni = JsonConvert.DeserializeObject<List<ZdravstveniKarton>>(json);
            return zdravstveniKartoni;
        }

        public void Save(ZdravstveniKarton noviZdravstveniKarton)
        {
            FileRepositoryPacijent.serializeZdravstveniKarton = true;
            var json = File.ReadAllText(fileLocation);
            List<ZdravstveniKarton> zdravstveniKartoni = JsonConvert.DeserializeObject<List<ZdravstveniKarton>>(json);
            if (zdravstveniKartoni == null)
            {
                zdravstveniKartoni = new List<ZdravstveniKarton>();
            }
            zdravstveniKartoni.Add(noviZdravstveniKarton);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(zdravstveniKartoni));
        }

        public void Update(ZdravstveniKarton zdravstveniKarton)
        {
            FileRepositoryPacijent.serializeZdravstveniKarton = true;
            List<ZdravstveniKarton> zdravstveniKartoni = new List<ZdravstveniKarton>();
            zdravstveniKartoni = GetAll();

            for (int i = 0; i < zdravstveniKartoni.Count; i++)
            {
                if (zdravstveniKartoni[i].BrojKartona.Equals(zdravstveniKarton.BrojKartona))
                {
                    zdravstveniKartoni[i] = zdravstveniKarton;
                    break;
                }
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(zdravstveniKartoni));
        }

        public void Delete(ZdravstveniKarton zdravstveniKarton)
        {
            FileRepositoryPacijent.serializeZdravstveniKarton = true;
            var json = File.ReadAllText(fileLocation);
            List<ZdravstveniKarton> zdravstveniKartoni = JsonConvert.DeserializeObject<List<ZdravstveniKarton>>(json);
            if (zdravstveniKartoni != null)
            {
                for (int i = 0; i < zdravstveniKartoni.Count; i++)
                {
                    if (zdravstveniKartoni[i].BrojKartona == zdravstveniKarton.BrojKartona)
                    {
                        zdravstveniKartoni.Remove(zdravstveniKartoni[i]);
                        break;
                    }
                }
                File.WriteAllText(fileLocation, JsonConvert.SerializeObject(zdravstveniKartoni));
            }
        }

        public ZdravstveniKarton GetById(int id)
        {
            FileRepositoryPacijent.serializeZdravstveniKarton = true;
            var json = File.ReadAllText(fileLocation);
            var zdravstveniKartoni = JsonConvert.DeserializeObject<List<ZdravstveniKarton>>(json);

            ZdravstveniKarton zdravstveniKarton = new ZdravstveniKarton();
            foreach (ZdravstveniKarton zk in zdravstveniKartoni)
                if (zk.BrojKartona == id)
                    zdravstveniKarton = zk;
            return zdravstveniKarton;
        }
    }
}
