using Bolnica.DTO;
using Bolnica.Model.Korisnici;
using Model.Korisnici;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.InterfejsiLekar
{
    public interface IKomentarLekaLekarInterface
    {
        List<Lek> DobijLek();
        void Potvrdi(KomentarLekaLekarDTO komentarDTO);
        void AzurirajLek(KomentarLekaLekarDTO komentarDTO);
        Obavestenje PopuniObavestenje(List<Korisnik> sviKorisnici, KomentarLekaLekarDTO komentarDTO, List<Obavestenje> svaObavestenja);
        int IzracunajId(List<Obavestenje> svaObavestenja);
       
    }
}
