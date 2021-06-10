using Bolnica.DTO;
using Bolnica.DTO.Sekretar;
using Bolnica.Model.Pregledi;
using Bolnica.Repository.Korisnici;
using Bolnica.Repository.Pregledi;
using Model.Korisnici;
using Model.Pacijenti;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Service.Sekretar
{
    public class SastojakService
    {
        private IRepositoryPacijent skladistePacijenata;
        private IRepositorySastojak skladisteAlergena;

        public SastojakService()
        {
            skladistePacijenata = new FileRepositoryPacijent();
            skladisteAlergena = new FileRepositorySastojak();
        }

        public List<SastojakDTO> GetDodatiAlergeni(string jmbg) 
        {
            Pacijent pacijent = skladistePacijenata.GetById(jmbg);
            List<Sastojak> alergeniIzSkladista = skladisteAlergena.GetAll();
            List<SastojakDTO> dodatiAlergeni = new List<SastojakDTO>();
            if (pacijent.Alergeni != null)
                foreach (Sastojak s in pacijent.Alergeni)
                    foreach (Sastojak sas in alergeniIzSkladista)
                        if (sas.Id == s.Id)
                            dodatiAlergeni.Add(new SastojakDTO { Id = sas.Id, Naziv = sas.Naziv });
            return dodatiAlergeni;
        }

        public List<SastojakDTO> GetSviAlergeni(List<SastojakDTO> dodatiAlergeni) 
        {
            List<Sastojak> alergeniIzSkladista = skladisteAlergena.GetAll();
            List<SastojakDTO> sviAlergeni = new List<SastojakDTO>();
            foreach (Sastojak s in alergeniIzSkladista)
                sviAlergeni.Add(new SastojakDTO { Id = s.Id, Naziv = s.Naziv });

            for (int i = alergeniIzSkladista.Count - 1; i >= 0; i--)
                for (int j = 0; j < dodatiAlergeni.Count; j++)
                    if (string.Equals(alergeniIzSkladista[i].Naziv, dodatiAlergeni[j].Naziv))
                        sviAlergeni.RemoveAt(i);
            return sviAlergeni;
        }
    }
}
