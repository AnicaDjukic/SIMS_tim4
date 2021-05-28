using Bolnica.Model.Prostorije;
using Bolnica.Repository.Prostorije;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Bolnica.Services.Prostorije
{
    public class ServiceZaliha
    {
        private RepositoryZaliha repository;

        public ServiceZaliha()
        {
            repository = new FileRepositoryZaliha();
        }

        public void ObrisiZalihe(Oprema opremaZaSkladistenje)
        {
            foreach (Zaliha z in repository.GetAll())
            {
                if (z.Oprema.Sifra == opremaZaSkladistenje.Sifra)
                    repository.Delete(z);
            }
        }

        public void PrebaciBuduceZaliheUZalihe(List<BuducaZaliha> buduceZalihe)
        {
            foreach (BuducaZaliha bz in buduceZalihe)
            {
                Zaliha z = new Zaliha { Kolicina = bz.Kolicina };
                z.Prostorija = bz.Prostorija;
                z.Oprema = bz.Oprema;
                repository.Save(z);
            }
        }

        public List<Zaliha> DobaviZaliheOpreme(Oprema opremaZaSkladistenje)
        {
            List<Zaliha> zaliheOpreme = new List<Zaliha>();
            foreach (Zaliha z in repository.GetAll())
            {
                if (opremaZaSkladistenje.Sifra == z.Oprema.Sifra)
                    zaliheOpreme.Add(z);
            }

            return zaliheOpreme;
        }

        public void SacuvajZalihu(Zaliha zaliha)
        {
            repository.Save(zaliha);
        }

        public List<Zaliha> DobaviZalihe()
        {
            return repository.GetAll();
        }

        public void ObrisiZalihu(Zaliha zaliha)
        {
            repository.Delete(zaliha);
        }

        public bool ValidnaKolicina(int kolicina, Oprema oprema)
        {
            foreach (Zaliha zaliha in repository.GetAll())
            {
                if (zaliha.Oprema.Sifra == oprema.Sifra && zaliha.Prostorija.BrojProstorije == "magacin")
                    return kolicina != 0 && kolicina <= zaliha.Kolicina;
            }

            return false;
        }
    }
}
