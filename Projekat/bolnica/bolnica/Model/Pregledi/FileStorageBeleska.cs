using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Model.Pregledi
{
    public class FileStorageBeleska
    {
        public string FileLocationBeleske { get; set; }


        public static bool serializeBeleska;
        public FileStorageBeleska()
        {
            serializeBeleska = false;
            FileStorageAnamneza.serializeAnamneza = false;
            string FileLocation = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            FileLocationBeleske = System.IO.Path.Combine(FileLocation, @"Resources\", "Beleske.json");
        }

        public List<Beleska> GetAll()
        {
            serializeBeleska = false;
            FileStorageAnamneza.serializeAnamneza = false;
            var json = File.ReadAllText(FileLocationBeleske);
            var beleske = JsonConvert.DeserializeObject<List<Beleska>>(json);
            if (beleske == null)
            {
                beleske = new List<Beleska>();
            }
            return beleske;
        }

        public void Save(Beleska novaBeleska)
        {
            serializeBeleska = false;
            FileStorageAnamneza.serializeAnamneza = false;
            List<Beleska> noveBeleske = GetAll();
            noveBeleske.Add(novaBeleska);
            File.WriteAllText(FileLocationBeleske, JsonConvert.SerializeObject(noveBeleske));
        }

        public void Izmeni(Beleska novaBeleska)
        {
            serializeBeleska = false;
            FileStorageAnamneza.serializeAnamneza = false;
            List<Beleska> noveBeleske = GetAll();
            foreach (Beleska beleska in noveBeleske)
            {
                if (beleska.Id.Equals(novaBeleska.Id))
                {
                    noveBeleske.Remove(beleska);
                    noveBeleske.Add(novaBeleska);
                    break;
                }
            }
            File.WriteAllText(FileLocationBeleske, JsonConvert.SerializeObject(noveBeleske));

        }

        public void Delete(Beleska novaBeleska)
        {
            serializeBeleska = false;
            FileStorageAnamneza.serializeAnamneza = false;
            List<Beleska> noveBeleske = GetAll();
            foreach (Beleska beleska in noveBeleske)
            {
                if (beleska.Id.Equals(novaBeleska.Id))
                {
                    noveBeleske.Remove(beleska);
                    break;
                }
            }
            File.WriteAllText(FileLocationBeleske, JsonConvert.SerializeObject(noveBeleske));
        }
    }
}
