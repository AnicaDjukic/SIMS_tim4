using Bolnica.DTO;
using Bolnica.Model.Pacijenti;
using Bolnica.Repository.Korisnici;
using Model.Pacijenti;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Service.Sekretar
{
    public class ZdravstveniKartonService
    {
        private IRepositoryZdravstveniKarton skladisteZdravstvenihKartona;

        public ZdravstveniKartonService()
        {
            skladisteZdravstvenihKartona = new FileRepositoryZdravstveniKarton();
        }

        public ZdravstveniKartonDTO GetZdravstveniKartonByID(int brojKartona) 
        {
            ZdravstveniKarton zdravstveniKarton = skladisteZdravstvenihKartona.GetById(brojKartona);
            ZdravstveniKartonDTO zdravstveniKartonDTO = new ZdravstveniKartonDTO(zdravstveniKarton.BrojKartona, zdravstveniKarton.Zanimanje, zdravstveniKarton.BracniStatus, zdravstveniKarton.Osiguranje);
            return zdravstveniKartonDTO;
        }

        public void SaveZdravstveniKarton(ZdravstveniKartonDTO zdravstveniKartonDTO) 
        {
            ZdravstveniKarton zdravstveniKarton = new ZdravstveniKarton
            {
                BrojKartona = zdravstveniKartonDTO.BrojKartona,
                Zanimanje = zdravstveniKartonDTO.Zanimanje,
                Osiguranje = zdravstveniKartonDTO.Osiguranje,
                BracniStatus = zdravstveniKartonDTO.BracniStatus
            };

            skladisteZdravstvenihKartona.Save(zdravstveniKarton);
        }

        public void UpdateZdravstveniKarton(ZdravstveniKartonDTO zdravstveniKartonDTO) 
        {
            ZdravstveniKarton zdravstveniKarton = new ZdravstveniKarton
            {
                BrojKartona = zdravstveniKartonDTO.BrojKartona,
                Zanimanje = zdravstveniKartonDTO.Zanimanje,
                Osiguranje = zdravstveniKartonDTO.Osiguranje,
                BracniStatus = zdravstveniKartonDTO.BracniStatus
            };

            skladisteZdravstvenihKartona.Update(zdravstveniKarton);
        }
    }
}
