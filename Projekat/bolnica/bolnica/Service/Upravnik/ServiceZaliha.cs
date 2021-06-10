using Bolnica.Model.Prostorije;
using Bolnica.Repository.Prostorije;
using System;
using System.Collections.Generic;

namespace Bolnica.Services.Prostorije
{
    public class ServiceZaliha
    {
        private RepositoryZaliha repository;
        private ServiceOprema serviceOprema;
        public ServiceZaliha()
        {
            repository = new FileRepositoryZaliha();
            serviceOprema = new ServiceOprema();
        }

        public void ObrisiZaliheOpreme(string sifraOpreme)
        {
            foreach (Zaliha z in repository.GetAll())
            {
                if (z.Oprema.Sifra == sifraOpreme)
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

        public int IzracunajRezervisanuKolicinu(string sifraOpreme)
        {
            int rezervisanaKolicina = 0;
            foreach (Zaliha z in DobaviSveZalihe())
            {
                if (z.Oprema.Sifra == sifraOpreme && z.Prostorija.BrojProstorije != "magacin")
                    rezervisanaKolicina += z.Kolicina;
            }
            return rezervisanaKolicina;
        }

        public List<Zaliha> DobaviZaliheOpreme(string sifraOpreme)
        {
            List<Zaliha> zaliheOpreme = new List<Zaliha>();
            foreach (Zaliha z in repository.GetAll())
            {
                if (sifraOpreme == z.Oprema.Sifra)
                    zaliheOpreme.Add(z);
            }

            return zaliheOpreme;
        }

        internal void SacuvajZalihe(List<Zaliha> zalihe)
        {
            foreach (Zaliha z in zalihe)
                repository.Save(z);
        }

        public void SacuvajZalihu(Zaliha zaliha)
        {
            repository.Save(zaliha);
        }

        public List<Zaliha> DobaviSveZalihe()
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

        public List<Zaliha> DobaviZaliheProstorije(string brojProstorije)
        {
            List<Zaliha> zaliheProstorije = new List<Zaliha>();
            foreach (Zaliha zaliha in DobaviSveZalihe())
            {
                if (zaliha.Prostorija.BrojProstorije == brojProstorije)
                {
                    zaliha.Oprema = serviceOprema.DobaviOpremu(zaliha.Oprema.Sifra);
                    zaliheProstorije.Add(zaliha);
                }
            }
            return zaliheProstorije;
        }

        public List<Zaliha> NapraviZaliheOdBuducihZaliha(List<BuducaZaliha> buduceZalihe)
        {
            List<Zaliha> noveZalihe = new List<Zaliha>();
            foreach (BuducaZaliha bz in buduceZalihe)
            {
                Zaliha z = new Zaliha { Kolicina = bz.Kolicina };
                z.Prostorija = bz.Prostorija;
                z.Oprema = bz.Oprema;
                noveZalihe.Add(z);
            }

            return noveZalihe;
        }

        public void ObrisiZaliheProstorije(string brojProstorije)
        {
            foreach(Zaliha z in repository.GetAll())
            {
                if(z.Prostorija.BrojProstorije == brojProstorije)
                {
                    foreach(Zaliha zo in DobaviZaliheOpreme(z.Oprema.Sifra))
                    {
                        if(zo.Prostorija.BrojProstorije == "magacin")
                        {
                            repository.Delete(zo);
                            zo.Kolicina += z.Kolicina;
                            repository.Save(zo);
                        }
                    }
                    repository.Delete(z);
                }
            }
        }
    }
}
