using System;
using System.Collections.Generic;

namespace Model.Pregledi
{
   public class FileStoragePregledi
   {
      private string fileLocation;

      public string FileLocation { get; set; }

      public List<Pregled> GetAll()
      {
         throw new NotImplementedException();
      }
      
      public void Save(Pregled noviPregled)
      {
         throw new NotImplementedException();
      }
   
   }
}