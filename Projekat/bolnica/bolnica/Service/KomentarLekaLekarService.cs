using Bolnica.DTO;
using Bolnica.Forms;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.Repository.Pregledi;
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
        private FileRepositoryLek skladisteLekova = new FileRepositoryLek();
        FileRepositoryObavestenje skladisteObavestenja = new FileRepositoryObavestenje();
        FileRepositoryKorisnik skladisteKorisnika = new FileRepositoryKorisnik();

        public List<Lek> DobijLek()
        {
            return skladisteLekova.GetAll();
        }
        public void Potvrdi(KomentarLekaLekarDTO komentarDTO)
        {
            
            List<Korisnik> sviKorisnici = skladisteKorisnika.GetAll();
            List<Obavestenje> svaObavestenja = skladisteObavestenja.GetAll();
            skladisteObavestenja.Save(PopuniObavestenje(sviKorisnici,komentarDTO,svaObavestenja));
            AzurirajLek(komentarDTO);
            
        }
        private void AzurirajLek(KomentarLekaLekarDTO komentarDTO)
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
        private Obavestenje PopuniObavestenje(List<Korisnik> sviKorisnici, KomentarLekaLekarDTO komentarDTO,List<Obavestenje> svaObavestenja)
        {
            Obavestenje obavestenje = new Obavestenje(IzracunajId(svaObavestenja),DateTime.Now, "Lek " + komentarDTO.prikazLeka.Naziv + " sa dozom " + komentarDTO.prikazLeka.KolicinaUMg + " i sastojcima: " + komentarDTO.prikazLeka.Sastojak + " je odbijen. " + "Komentar: " + komentarDTO.komentar, "Lek " + komentarDTO.prikazLeka.Naziv + " je odbijen",false);
            for (int i = 0; i < sviKorisnici.Count; i++)
            {
                if (sviKorisnici[i].TipKorisnika.Equals(TipKorisnika.upravnik))
                {
                    obavestenje.Korisnici.Add(sviKorisnici[i]);
                }
            }
            return obavestenje;
        }
        private int IzracunajId(List<Obavestenje> svaObavestenja)
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
