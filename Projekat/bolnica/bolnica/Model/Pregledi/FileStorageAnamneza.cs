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

        public FileStorageAnamneza()
        {
            string FileLocation = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            FileLocationAnamneza = System.IO.Path.Combine(FileLocation, @"Resources\", "Anamneze.json");
            

        }

        public List<Anamneza> GetAll()
        {
            var json = File.ReadAllText(FileLocationAnamneza);
            var anameze = JsonConvert.DeserializeObject<List<Anamneza>>(json);
            return anameze;
        }

        public void Save(Anamneza novaAnamneza)
        {
            List<Anamneza> noveAnamneze = new List<Anamneza>();
            noveAnamneze = GetAll();
            noveAnamneze.Add(novaAnamneza);
            File.WriteAllText(FileLocationAnamneza, JsonConvert.SerializeObject(noveAnamneze));
        }

       

        public void Izmeni(Anamneza novaAnamneza)
        {
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
