using Bolnica.DTO;
using Bolnica.Forms;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.ViewModel;
using Model.Korisnici;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Services
{
    public class KomentarLekaLekarService
    {
        private FileStorageLek skladisteLekova = new FileStorageLek();
        FileStorageObavestenja skladisteObavestenja = new FileStorageObavestenja();
        FileStorageKorisnici skladisteKorisnika = new FileStorageKorisnici();
        public void Potvrdi(KomentarLekaLekarServiceDTO komentarDTO)
        {
            
            List<Korisnik> sviKorisnici = skladisteKorisnika.GetAll();
            List<Obavestenje> svaObavestenja = skladisteObavestenja.GetAll();
            skladisteObavestenja.Save(PopuniObavestenje(sviKorisnici,komentarDTO,svaObavestenja));
            AzurirajLek(komentarDTO);
            
        }
        public void AzurirajLek(KomentarLekaLekarServiceDTO komentarDTO)
        {
            LekarViewModel.lekoviPrikaz.Remove(komentarDTO.prikazLeka);
            for (int i = 0; i < komentarDTO.listaLekova.Count; i++)
            {
                if (komentarDTO.listaLekova[i].Id.Equals(komentarDTO.prikazLeka.Id))
                {
                    komentarDTO.listaLekova[i].Status = StatusLeka.odbijen;
                    skladisteLekova.Delete(komentarDTO.listaLekova[i]);
                    skladisteLekova.Save(komentarDTO.listaLekova[i]);
                }
            }
        }
        public Obavestenje PopuniObavestenje(List<Korisnik> sviKorisnici, KomentarLekaLekarServiceDTO komentarDTO,List<Obavestenje> svaObavestenja)
        {
            Obavestenje obavestenje = new Obavestenje();
            obavestenje.Id = IzracunajId(svaObavestenja);
            for (int i = 0; i < sviKorisnici.Count; i++)
            {
                if (sviKorisnici[i].TipKorisnika.Equals(TipKorisnika.upravnik))
                {
                    obavestenje.Korisnici.Add(sviKorisnici[i]);
                }
            }
            obavestenje.Naslov = "Lek " + komentarDTO.prikazLeka.Naziv + " je odbijen";
            obavestenje.Obrisan = false;
            obavestenje.Sadrzaj = "Lek " + komentarDTO.prikazLeka.Naziv + " sa dozom " + komentarDTO.prikazLeka.KolicinaUMg + " i sastojcima: " + komentarDTO.prikazLeka.Sastojak + " je odbijen. " + "Komentar: " + komentarDTO.komentar;
            obavestenje.Datum = DateTime.Now;
            return obavestenje;
        }
        public int IzracunajId(List<Obavestenje> svaObavestenja)
        {
            int max = 0;
            for (int i = 0; i < svaObavestenja.Count; i++)
            {
                if (max < svaObavestenja[i].Id)
                {
                    max = svaObavestenja[i].Id;
                }
            }
            max = max + 1;
            return max;
        }
    }
}
