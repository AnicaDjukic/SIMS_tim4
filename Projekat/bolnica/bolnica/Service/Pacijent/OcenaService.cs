using Bolnica.Forms;
using Bolnica.Repository.Pregledi;
using Bolnica.Services;
using Model.Korisnici;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Bolnica.Service
{
    public class OcenaService
    {
        private FileRepositoryOcena repositoryOcena = new FileRepositoryOcena();
        private LekarService lekarService = new LekarService();

        public List<Ocena> DobaviSveOcene()
        {
            return repositoryOcena.GetAll();
        }

        public void SacuvajOcenu(Ocena novaOcena)
        {
            repositoryOcena.Save(novaOcena);
        }

        public void IzmeniOcenu(Ocena novaOcena)
        {
            repositoryOcena.Update(novaOcena);
        }

        public void IzbrisiOcenu(Ocena ocena)
        {
            repositoryOcena.Delete(ocena);
        }

        public int IzracunajIdOcene()
        {
            int max = 0;
            List<Ocena> ocene = DobaviSveOcene();
            foreach (Ocena ocena in ocene)
            {
                if (ocena.IdOcene > max)
                {
                    max = ocena.IdOcene;
                }
            }
            return max + 1;
        }

        public void PopuniTabeluOcena(Pacijent trenutniPacijent)
        {
            FormIstorijaOcenaPage.PrikazOcenaIKomentara = new ObservableCollection<PrikazOcena>();
            List<Ocena> ocene = DobaviSveOcene();
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
            List<Lekar> lekari = lekarService.DobijLekare();
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
