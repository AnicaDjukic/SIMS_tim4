﻿using Bolnica.Model.Pregledi;
using Bolnica.Repository.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Services.Prostorije
{
    public class ServiceSastojak
    {
        private RepositorySastojak repository;

        public ServiceSastojak()
        {
            repository = new FileRepositorySastojak();
        }
        public bool SastojakPostoji(string text)
        {
            bool postoji = false;
            foreach (Sastojak s in repository.GetAll())
            {
                if (s.Naziv == text)
                    postoji = true;
            }
            return postoji;
        }

        public int MaxId()
        {
            int maxId = 0;
            foreach (Sastojak s in repository.GetAll())
            {
                if (s.Id > maxId)
                    maxId = s.Id;
            }
            return maxId;
        }

        public void SacuvajSastojak(Sastojak noviSastojak)
        {
            repository.Save(noviSastojak);
        }
    }
}