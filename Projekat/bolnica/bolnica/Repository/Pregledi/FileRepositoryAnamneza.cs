using Bolnica.Repository.Pregledi;
using Model.Pregledi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Repository.Pregledi
{
    public class FileRepositoryAnamneza : IRepositoryAnamneza
    {
        public string FileLocation { get; set; }
        public static bool serializeAnamneza;

        public FileRepositoryAnamneza()
        {
            serializeAnamneza = true;
            FileRepositoryLek.serializeLek = false;
           // FileRepositoryBeleska.serializeBeleska = false;
            string putanja = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            FileLocation = System.IO.Path.Combine(putanja, @"Resources\", "Anamneze.json");
        }

        public void Delete(Anamneza entity)
        {
            serializeAnamneza = true;
            FileRepositoryLek.serializeLek = false;
           // FileRepositoryBeleska.serializeBeleska = false;
            List<Anamneza> anamneze = GetAll();
            for (int i = 0; i < anamneze.Count; i++)
            {
                if (entity.Id.Equals(anamneze[i].Id))
                {
                    anamneze.RemoveAt(i);
                    break;
                }
            }
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(anamneze));
        }

        public List<Anamneza> GetAll()
        {
            serializeAnamneza = true;
            FileRepositoryLek.serializeLek = false;
           // FileRepositoryBeleska.serializeBeleska = false;
            var json = File.ReadAllText(FileLocation);
            var anameze = JsonConvert.DeserializeObject<List<Anamneza>>(json);
            if (anameze is null)
            {
                anameze = new List<Anamneza>();
            }
            return anameze;
        }

        public Anamneza GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(Anamneza newEntity)
        {
            serializeAnamneza = true;
            FileRepositoryLek.serializeLek = false;
           // FileRepositoryBeleska.serializeBeleska = false;
            List<Anamneza> anamneze = GetAll();
            anamneze.Add(newEntity);
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(anamneze));
        }

        public void Update(Anamneza entity)
        {
            serializeAnamneza = true;
            FileRepositoryLek.serializeLek = false;
          //  FileRepositoryBeleska.serializeBeleska = false;
            List<Anamneza> anamneze = GetAll();
            for (int i = 0; i < anamneze.Count; i++)
            {
                if (entity.Id.Equals(anamneze[i].Id))
                {
                    anamneze[i] = entity;
                    break;
                }
            }
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(anamneze));
        }
    }
}
