using Bolnica.Controller;
using Bolnica.Forms;
using Model.Korisnici;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Bolnica.Service
{
    public class IstorijaOcenaService
    {
        private RepositoryController repositoryController = new RepositoryController();

        public void PopuniTabeluOcena(Pacijent trenutniPacijent)
        {
            FormIstorijaOcenaPage.PrikazOcenaIKomentara = new ObservableCollection<PrikazOcena>();
            List<Ocena> ocene = repositoryController.DobijOcene();
            foreach (Ocena ocena in ocene)
            {
                if (trenutniPacijent.Jmbg.Equals(ocena.Pacijent.Jmbg))
                {
                    PrikazOcena prikazOcene = NapraviPrikazOcene(ocena);
                    PostaviLekaraZaOcenu(ocena, prikazOcene);
                    DodajOcenuUTabelu(prikazOcene);
                }
            }
        }

        private static PrikazOcena NapraviPrikazOcene(Ocena ocena)
        {
            return new PrikazOcena
            {
                IdOcene = ocena.IdOcene,
                Datum = ocena.Datum,
                BrojOcene = ocena.BrojOcene,
                Sadrzaj = ocena.Sadrzaj,
                Pacijent = ocena.Pacijent,
                ImeIPrezime = "Bolnica"
            };
        }

        private void PostaviLekaraZaOcenu(Ocena ocena, PrikazOcena prikazOcene)
        {
            List<Lekar> lekari = repositoryController.DobijLekare();
            foreach (Lekar l in lekari)
            {
                if (l.Jmbg.Equals(ocena.Lekar.Jmbg))
                {
                    prikazOcene.ImeIPrezime = l.Ime + " " + l.Prezime;
                    break;
                }
            }
        }

        private static void DodajOcenuUTabelu(PrikazOcena prikaz)
        {
            FormIstorijaOcenaPage.PrikazOcenaIKomentara.Add(prikaz);
        }
    }
}
