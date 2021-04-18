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
      
      
      public int Id { get; set; }
      public string Simptomi { get; set; }

      public string Dijagnoza { get; set; }

      
      
      /// <summary>
      /// Property for collection of Recept
      /// </summary>
      /// <pdGenerated>Default opposite class collection property</pdGenerated>
      public List<Recept> Recept
      {
            get; set;
      }
      
      
   
   }
}