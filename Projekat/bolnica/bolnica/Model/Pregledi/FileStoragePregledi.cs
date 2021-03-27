using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace Model.Pregledi
{
   public class FileStoragePregledi
   {
      public string FileLocation { get; set; }

      public FileStoragePregledi()
        {
            FileLocation = @"E:\SIMS_tim4\Projekat\bolnica\bolnica\Resources\Users.txt";
        }

      public List<Pregled> GetAllPregledi()
      {
            var json = File.ReadAllText(FileLocation+"Pregledi.json");
            var pregledi = JsonConvert.DeserializeObject<List<Pregled>>(json);
            return pregledi;
        }

      public List<Operacija> GetAllOperacije()
      {
            var json = File.ReadAllText(FileLocation + "Operacije.json");
            var operacije = JsonConvert.DeserializeObject<List<Operacija>>(json);
            return operacije;
        }

        public void Save(List<Pregled> noviPregledi)
      {
            File.WriteAllText(FileLocation + "Pregledi.json", JsonConvert.SerializeObject(noviPregledi));
        }
      
        public void Save(List<Operacija> noveOperacije)
        {
            File.WriteAllText(FileLocation + "Operacije.json", JsonConvert.SerializeObject(noveOperacije));
        }
    }
}