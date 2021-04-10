// File:    Anamneza.cs
// Author:  Anica
// Created: 3. ????? 2021 14:41:40
// Purpose: Definition of Class Anamneza

using System;
using System.Collections.Generic;

namespace Model.Pregledi
{
   public class Anamneza
   {
      private int id;
      private string simptomi;
      private string dijagnoza;
      
      public int Id { get; set; }
      public string Simptomi { get; set; }

      public string Dijagnoza { get; set; }

      public List<Recept> recept;
      
      /// <summary>
      /// Property for collection of Recept
      /// </summary>
      /// <pdGenerated>Default opposite class collection property</pdGenerated>
      public List<Recept> Recept
      {
         get
         {
            if (recept == null)
               recept = new List<Recept>();
            return recept;
         }
         set
         {
            RemoveAllRecept();
            if (value != null)
            {
               foreach (Recept oRecept in value)
                  AddRecept(oRecept);
            }
         }
      }
      
      /// <summary>
      /// Add a new Recept in the collection
      /// </summary>
      /// <pdGenerated>Default Add</pdGenerated>
      public void AddRecept(Recept newRecept)
      {
         if (newRecept == null)
            return;
         if (this.recept == null)
            this.recept = new List<Recept>();
         if (!this.recept.Contains(newRecept))
            this.recept.Add(newRecept);
      }
      
      /// <summary>
      /// Remove an existing Recept from the collection
      /// </summary>
      /// <pdGenerated>Default Remove</pdGenerated>
      public void RemoveRecept(Recept oldRecept)
      {
         if (oldRecept == null)
            return;
         if (this.recept != null)
            if (this.recept.Contains(oldRecept))
               this.recept.Remove(oldRecept);
      }
      
      /// <summary>
      /// Remove all instances of Recept from the collection
      /// </summary>
      /// <pdGenerated>Default removeAll</pdGenerated>
      public void RemoveAllRecept()
      {
         if (recept != null)
            recept.Clear();
      }
   
   }
}