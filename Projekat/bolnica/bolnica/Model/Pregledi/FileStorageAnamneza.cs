using Model.Pregledi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bolnica.Model.Pregledi
{
    public class FileStorageAnamneza
    {
        public string FileLocationAnamneza { get; set; }

        public static bool serializeAnamneza;

        public FileStorageAnamneza()
        {
            serializeAnamneza = true;
            FileStorageLek.serializeLek = false;
            string FileLocation = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            FileLocationAnamneza = System.IO.Path.Combine(FileLocation, @"Resources\", "Anamneze.json");


        }

        public List<Anamneza> GetAll()
        {
            serializeAnamneza = true;
            FileStorageLek.serializeLek = false;
            var json = File.ReadAllText(FileLocationAnamneza);
            var anameze = JsonConvert.DeserializeObject<List<Anamneza>>(json);
            if (anameze?.Count == null)
            {
                anameze = new List<Anamneza>();
            }
            return anameze;
        }

        public void Save(Anamneza novaAnamneza)
        {
            serializeAnamneza = true;
            FileStorageLek.serializeLek = false;
            List<Anamneza> noveAnamneze = new List<Anamneza>();
            noveAnamneze = GetAll();
            noveAnamneze.Add(novaAnamneza);
            File.WriteAllText(FileLocationAnamneza, JsonConvert.SerializeObject(noveAnamneze));
        }



        public void Izmeni(Anamneza novaAnamneza)
        {
            serializeAnamneza = true;
            FileStorageLek.serializeLek = false;
            List<Anamneza> noveAnamneze = new List<Anamneza>();
            noveAnamneze = GetAll();

            for (int i = 0; i < noveAnamneze.Count; i++)
            {
                if (noveAnamneze[i].Id.Equals(novaAnamneza.Id))
                {
                    noveAnamneze[i] = novaAnamneza;
                    break;
                }
            }
            File.WriteAllText(FileLocationAnamneza, JsonConvert.SerializeObject(noveAnamneze));

        }


        public void Delete(Anamneza novaAnamneza)
        {
            serializeAnamneza = true;
            FileStorageLek.serializeLek = false;
            List<Anamneza> noveAnamneze = new List<Anamneza>();
            noveAnamneze = GetAll();

            for (int i = 0; i < noveAnamneze.Count; i++)
            {
                if (noveAnamneze[i].Id.Equals(novaAnamneza.Id))
                {
                    noveAnamneze.RemoveAt(i);
                    break;
                }
            }
            File.WriteAllText(FileLocationAnamneza, JsonConvert.SerializeObject(noveAnamneze));

        }


    }
}
