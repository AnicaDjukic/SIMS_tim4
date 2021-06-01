using Bolnica.Model.Pregledi;
using Bolnica.Repository.Pregledi;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Services.Pregledi
{
    class ServiceLek
    {
        private IRepositoryLek repository;
        private ServiceSastojak serviceSastojak;
        public ServiceLek()
        {
            repository = new FileRepositoryLek();
            serviceSastojak = new ServiceSastojak();
        }
        public List<Lek> DobaviSveLekove()
        {
            return repository.GetAll();
        }

        public bool LekPostoji(int id)
        {
            bool postoji = false;
            foreach (Lek l in repository.GetAll())
            {
                if (l.Id == id)
                {
                    postoji = true;
                    break;
                }
            }
            return postoji;
        }

        public void ObrisiLek(int lekId)
        {
            repository.DeleteById(lekId);
        }

        public void SacuvajLek(Lek lek)
        {
            repository.Save(lek);
        }

        public Lek DobaviLek(int idLeka)
        {
            return repository.GetById(idLeka);
        }

        public List<Sastojak> DobaviSastojkeLeka(Lek lek)
        {
            List<Sastojak> sastojci = new List<Sastojak>();
            foreach(Sastojak sastojakLeka in lek.Sastojak)
            {
                foreach(Sastojak s in serviceSastojak.DobaviSveSastojke())
                {
                    if(s.Id == sastojakLeka.Id)
                        sastojci.Add(s);
                }
            }

            return sastojci;
        }

        public List<Lek> DobaviSveZameneLeka(Lek lek)
        {
            List<Lek> zameneLeka = new List<Lek>();
            foreach (Lek l in repository.GetAll())
            {
                foreach (int id in lek.IdZamena)
                {
                    if (l.Id == id)
                    {
                        if (!l.Obrisan)
                            zameneLeka.Add(l);
                    }
                }
            }

            return zameneLeka;
        }
    }
}
