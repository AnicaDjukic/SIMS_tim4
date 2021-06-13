using Bolnica.DTO;
using Bolnica.DTO.Sekretar;
using Bolnica.Model.Pregledi;
using Bolnica.Repository.Korisnici;
using Bolnica.Repository.Pregledi;
using Bolnica.Services;
using Model.Korisnici;
using Model.Pacijenti;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Service.Sekretar
{
    public class SastojakService
    {
        private IRepositorySastojak skladisteAlergena;

        public SastojakService()
        {
            skladisteAlergena = new FileRepositorySastojak();
        }

        public List<SastojakDTO> GetDodatiAlergeni(PacijentDTO pacijentDTO) 
        {
            List<Sastojak> alergeniIzSkladista = skladisteAlergena.GetAll();
            List<SastojakDTO> dodatiAlergeni = new List<SastojakDTO>();
            if (pacijentDTO.IdsAlergena != null)
                foreach (int id in pacijentDTO.IdsAlergena)
                    foreach (Sastojak sas in alergeniIzSkladista)
                        if (sas.Id == id)
                            dodatiAlergeni.Add(new SastojakDTO(sas.Id, sas.Naziv));
            return dodatiAlergeni;
        }

        public List<SastojakDTO> GetSviAlergeni(List<SastojakDTO> dodatiAlergeni) 
        {
            List<Sastojak> alergeniIzSkladista = skladisteAlergena.GetAll();
            List<SastojakDTO> sviAlergeni = new List<SastojakDTO>();
            foreach (Sastojak s in alergeniIzSkladista)
                sviAlergeni.Add(new SastojakDTO(s.Id, s.Naziv));

            for (int i = alergeniIzSkladista.Count - 1; i >= 0; i--)
                for (int j = 0; j < dodatiAlergeni.Count; j++)
                    if (string.Equals(alergeniIzSkladista[i].Naziv, dodatiAlergeni[j].Naziv))
                        sviAlergeni.RemoveAt(i);
            return sviAlergeni;
        }

        public SastojakDTO GetAlergenById(int id)
        {
            Sastojak alergen = skladisteAlergena.GetById(id);
            SastojakDTO alergenDTO = new SastojakDTO(alergen.Id, alergen.Naziv);
            return alergenDTO;
        }
    }
}
