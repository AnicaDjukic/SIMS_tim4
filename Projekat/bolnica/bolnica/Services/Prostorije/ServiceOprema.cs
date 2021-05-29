using Bolnica.Model.Prostorije;
using Bolnica.Repository.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Services.Prostorije
{
    public class ServiceOprema
    {
        private RepositoryOprema repository;
        private ServiceZaliha serviceZaliha;
        public ServiceOprema()
        {
            repository = new FileRepositoryOprema();
            serviceZaliha = new ServiceZaliha();
        }
        public bool OpremaPostoji(string sifra)
        {
            bool postoji = false;
            foreach (Oprema o in repository.GetAll())
            {
                if (o.Sifra == sifra)
                {
                    postoji = true;
                    break;
                }
            }
            return postoji;
        }

        public void ObrisiOpremu(string sifra)
        {
            repository.DeleteById(sifra);
        }

        public void SacuvajOpremu(Oprema oprema)
        {
            repository.Save(oprema);
        }

        public int IzracunajRezervisanuKolicinu(string sifraOpreme)
        {
            int rezervisanaKolicina = 0;
            foreach (Zaliha z in serviceZaliha.DobaviZalihe())
            {
                if (z.Oprema.Sifra == sifraOpreme && z.Prostorija.BrojProstorije != "magacin")
                    rezervisanaKolicina += z.Kolicina;
            }
            return rezervisanaKolicina;
        }

        public bool MoguceSmanjitiKolicinu(int ukKolicina, int rezervisanaKolicina)
        {
            return ukKolicina - rezervisanaKolicina >= 0;
        }
    }
}
