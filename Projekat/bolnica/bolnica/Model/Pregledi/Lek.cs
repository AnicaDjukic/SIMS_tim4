using Bolnica.Model.Pregledi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Model.Pregledi
{
   public class Lek
   {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int KolicinaUMg { get; set; }
        public StatusLeka Status { get; set; }

        public string Proizvodjac { get; set; }

        public int Zalihe { get; set; }
        public List<int> ZamenaId { get; set; }

        [JsonIgnore]
        public List<Sastojak> sastojak;


      
      /// <summary>
      /// Property for collection of Alergen
      /// </summary>
      /// <pdGenerated>Default opposite class collection property</pdGenerated>

      public List<Sastojak> Sastojak
        {
         get
         {
            if (sastojak == null)
             sastojak = new List<Sastojak>();
            return sastojak;
         }
         set
         {
              RemoveAllSastojak();
            if (value != null)
            {
               foreach (Sastojak oSastojak in value)
                AddSastojak(oSastojak);

            }
         }
      }

        /// <summary>
        /// Add a new Alergen in the collection
        /// </summary>
        /// <pdGenerated>Default Add</pdGenerated>

        public void AddSastojak(Sastojak newSastojak)
        {
            if (newSastojak == null)
                return;
            if (this.sastojak == null)
                this.sastojak = new List<Sastojak>();
            if (!this.sastojak.Contains(newSastojak))
                this.sastojak.Add(newSastojak);
        }
      
      /// <summary>
      /// Remove an existing Alergen from the collection
      /// </summary>
      /// <pdGenerated>Default Remove</pdGenerated>

      public void RemoveSastojak(Sastojak oldSastojak)
            {
                if (oldSastojak == null)
                    return;
                if (this.sastojak != null)
                    if (this.sastojak.Contains(oldSastojak))
                        this.sastojak.Remove(oldSastojak);

            }
      /// <summary>
      /// Remove all instances of Alergen from the collection
      /// </summary>
      /// <pdGenerated>Default removeAll</pdGenerated>

      public void RemoveAllSastojak()
            {
                if (sastojak != null)
                    sastojak.Clear();
            }
   
   }
}

