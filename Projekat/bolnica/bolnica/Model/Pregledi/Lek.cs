// File:    Lek.cs
// Author:  Anica
// Created: 3. ????? 2021 14:53:18
// Purpose: Definition of Class Lek

using System;
using System.Collections.Generic;

namespace Model.Pregledi
{
   public class Lek
   {
      private int id;
      private string naziv;
      private int kolicinaUMg;
      private bool odobren;

        public int Id { get; set; }
        public string Naziv { get; set; }
        public int KolicinaUMg { get; set; }
        public bool Odobren { get; set; }
      
      public List<Alergen> alergen;
      
      /// <summary>
      /// Property for collection of Alergen
      /// </summary>
      /// <pdGenerated>Default opposite class collection property</pdGenerated>
      public List<Alergen> Alergen
      {
         get
         {
            if (alergen == null)
               alergen = new List<Alergen>();
            return alergen;
         }
         set
         {
            RemoveAllAlergen();
            if (value != null)
            {
               foreach (Alergen oAlergen in value)
                  AddAlergen(oAlergen);
            }
         }
      }
      
      /// <summary>
      /// Add a new Alergen in the collection
      /// </summary>
      /// <pdGenerated>Default Add</pdGenerated>
      public void AddAlergen(Alergen newAlergen)
      {
         if (newAlergen == null)
            return;
         if (this.alergen == null)
            this.alergen = new List<Alergen>();
         if (!this.alergen.Contains(newAlergen))
            this.alergen.Add(newAlergen);
      }
      
      /// <summary>
      /// Remove an existing Alergen from the collection
      /// </summary>
      /// <pdGenerated>Default Remove</pdGenerated>
      public void RemoveAlergen(Alergen oldAlergen)
      {
         if (oldAlergen == null)
            return;
         if (this.alergen != null)
            if (this.alergen.Contains(oldAlergen))
               this.alergen.Remove(oldAlergen);
      }
      
      /// <summary>
      /// Remove all instances of Alergen from the collection
      /// </summary>
      /// <pdGenerated>Default removeAll</pdGenerated>
      public void RemoveAllAlergen()
      {
         if (alergen != null)
            alergen.Clear();
      }
   
   }
}