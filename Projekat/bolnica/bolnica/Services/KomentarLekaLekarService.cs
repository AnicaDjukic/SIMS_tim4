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
        private FileStorageLek sviLekovi = new FileStorageLek();
        public void Potvrdi(string komentar, PrikazLek prik,List <Lek> leko)
        {
            

            Obavestenje obavestenje = new Obavestenje();
            FileStorageObavestenja svaObavestenja = new FileStorageObavestenja();
            FileStorageKorisnici sviKorisnici = new FileStorageKorisnici();
            List<Korisnik> svi = sviKorisnici.GetAll();
            List<Obavestenje> sva = svaObavestenja.GetAll();
            int max = 0;
            for (int i = 0; i < sva.Count; i++)
            {
                if (max < sva[i].Id)
                {
                    max = sva[i].Id;
                }
            }
            max = max + 1;
            obavestenje.Id = max;

            for (int i = 0; i < svi.Count; i++)
            {
                if (svi[i].TipKorisnika.Equals(TipKorisnika.upravnik))
                {
                    obavestenje.Korisnici.Add(svi[i]);
                }
            }
            obavestenje.Naslov = "Lek " + prik.Naziv + " je odbijen";
            obavestenje.Obrisan = false;
            obavestenje.Sadrzaj = "Lek " + prik.Naziv + " sa dozom " + prik.KolicinaUMg + " i sastojcima: " + prik.Sastojak + " je odbijen. " + "Komentar: " + komentar;
            obavestenje.Datum = DateTime.Now;
            FileStorageObavestenja oba = new FileStorageObavestenja();
            oba.Save(obavestenje);

            LekarViewModel.lekoviPrikaz.Remove(prik);
            for (int i = 0; i < leko.Count; i++)
            {
                if (leko[i].Id.Equals(prik.Id))
                {
                    leko[i].Status = StatusLeka.odbijen;
                    sviLekovi.Delete(leko[i]);
                    sviLekovi.Save(leko[i]);
                }
            }
        }
    }
}
