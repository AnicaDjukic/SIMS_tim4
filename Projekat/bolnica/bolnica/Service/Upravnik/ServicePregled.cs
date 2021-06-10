using Bolnica.Repository.Pregledi;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Service.Upravnik
{
    public class ServicePregled
    {
        private IRepositoryPregled repository;
        
        public ServicePregled()
        {
            repository = new FileRepositoryPregled();
        }

        public List<Pregled> DobaviSvePreglede()
        {
            return repository.GetAll();
        }
    }
}
