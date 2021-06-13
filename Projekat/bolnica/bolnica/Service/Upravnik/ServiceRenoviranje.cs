using Bolnica.Localization;
using Bolnica.Model.Prostorije;
using Bolnica.Repository.Prostorije;
using Model.Prostorije;
using System;
using System.Collections.Generic;

namespace Bolnica.Services.Prostorije
{
    public class ServiceRenoviranje
    {
        private RepositoryRenoviranje repository;
        private ServiceProstorija serviceProstorija;
        private ServiceZaliha serviceZaliha;
        private ServiceBuducaZaliha serviceBuducaZaliha;
        public ServiceRenoviranje()
        {
            repository = new FileRepositoryRenoviranje();
            serviceProstorija = new ServiceProstorija();
            serviceZaliha = new ServiceZaliha();
            serviceBuducaZaliha = new ServiceBuducaZaliha();
        }
        public List<Renoviranje> DobaviRenoviranjaProstorije(string brojProstorije)
        {
            List<Renoviranje> renoviranja = new List<Renoviranje>();
            foreach (Renoviranje r in repository.GetAll())
            {
                if (r.Prostorija.BrojProstorije == brojProstorije)
                    renoviranja.Add(r);
            }
            return renoviranja;
        }

        internal void SacuvajRenoviranje(Renoviranje novoRenoviranje)
        {
            repository.Save(novoRenoviranje);
        }

        internal List<Renoviranje> DobaviSvaRenoviranja()
        {
            return repository.GetAll();
        }

        internal void Izmeni(Renoviranje r)
        {
            repository.Delete(r);
            repository.Save(r);
        }

        public void PodeliISpojiProstorije()
        {
            foreach (Renoviranje r in DobaviSvaRenoviranja())
            {
                if (r.KrajRenoviranja <= DateTime.Now.Date)
                {
                    if (r.BrojNovihProstorija > 0 || r.ProstorijeZaSpajanje.Count > 0)
                    {
                        double novaKvadratura = serviceProstorija.DobaviKvadraturu(r.Prostorija.BrojProstorije) / r.BrojNovihProstorija;
                        if (r.BrojNovihProstorija > 0)
                            PodeliProstoriju(r, novaKvadratura);
                        else
                            SpojiProstorije(r, novaKvadratura);

                        AzurirajProstorijuIRenoviranje(r, novaKvadratura);
                    }
                }
            }
        }

        private void PodeliProstoriju(Renoviranje renoviranje, double novaKvadratura)
        {
            Prostorija prostorijaKojaSeDeli = serviceProstorija.DobaviProstoriju(renoviranje.Prostorija.BrojProstorije);
            for (int i = 0; i < renoviranje.BrojNovihProstorija - 1; i++)
            {
                Prostorija novaProstorija = new Prostorija();
                novaProstorija.BrojProstorije = prostorijaKojaSeDeli.BrojProstorije + LocalizedStrings.Instance["nova"] + (i + 1).ToString();
                novaProstorija.Sprat = prostorijaKojaSeDeli.Sprat;
                novaProstorija.Kvadratura = novaKvadratura;
                serviceProstorija.SacuvajProstoriju(novaProstorija);
            }
            renoviranje.BrojNovihProstorija = 0;
        }

        private void SpojiProstorije(Renoviranje renoviranje, double novaKvadratura)
        {
            foreach (Prostorija p in renoviranje.ProstorijeZaSpajanje)
            {
                Prostorija prostorija = serviceProstorija.DobaviProstoriju(p.BrojProstorije);
                novaKvadratura += prostorija.Kvadratura;
                serviceZaliha.ObrisiZaliheProstorije(prostorija.BrojProstorije);
                serviceBuducaZaliha.ObrisiBuduceZaliheProstorije(prostorija.BrojProstorije);
                serviceProstorija.ObrisiProstoriju(prostorija.BrojProstorije);
            }
        }

        private void AzurirajProstorijuIRenoviranje(Renoviranje renoviranje, double novaKvadratura)
        {
            Prostorija renoviranaProstorija = serviceProstorija.DobaviProstoriju(renoviranje.Prostorija.BrojProstorije);
            renoviranaProstorija.Kvadratura = novaKvadratura;
            serviceProstorija.IzmeniProstoriju(renoviranaProstorija);
            Izmeni(renoviranje);
        }
    }
}
