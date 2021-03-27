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
            fileLocation = @"C:\Users\Minja\Documents\GitHub\SIMS_tim4\Projekat\bolnica\bolnica\Resources\";
        }

      public List<Pregled> GetAllPregledi()
      {
            var json = File.ReadAllText(fileLocation+"Pregledi.json");
            var pregledi = JsonConvert.DeserializeObject<List<Pregled>>(json);
            return pregledi;
        }

      public List<Operacija> GetAllOperacije()
      {
            var json = File.ReadAllText(fileLocation + "Operacije.json");
            var operacije = JsonConvert.DeserializeObject<List<Operacija>>(json);
            return operacije;
        }

        public void Save(List<Pregled> noviPregledi)
      {
            File.WriteAllText(fileLocation + "Pregledi.json", JsonConvert.SerializeObject(noviPregledi));
        }
      
        public void Save(List<Operacija> noveOperacije)
        {
            File.WriteAllText(fileLocation + "Operacije.json", JsonConvert.SerializeObject(noveOperacije));
        }

       

       

    }
}