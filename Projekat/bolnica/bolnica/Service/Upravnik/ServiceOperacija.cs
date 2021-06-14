using Bolnica.Repository.Pregledi;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Service.Upravnik
{
    public class ServiceOperacija
    {
        private IRepositoryOperacija repository;

        public ServiceOperacija()
        {
            repository = new FileRepositoryOperacija();
        }

        public List<Operacija> DobaviSveOperacije()
        {
            return repository.GetAll();
        }
    }
}
