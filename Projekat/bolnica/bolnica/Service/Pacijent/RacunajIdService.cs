using Bolnica.Controller;
using Model.Korisnici;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Service
{
    public class RacunajIdService
    {
        private RepositoryController repositoryController = new RepositoryController();

        public int IzracunajIdBeleske()
        {
            int max = 0;
            List<Beleska> beleske = repositoryController.DobijBeleske();
            foreach (Beleska beleska in beleske)
            {
                if (beleska.Id > max)
                {
                    max = beleska.Id;
                }
            }
            return max + 1;
        }

        public int IzracunajIdPregleda()
        {
            int max = 0;
            List<Pregled> pregledi = repositoryController.DobijPreglede();
            foreach (Pregled pregled in pregledi)
            {
                if (pregled.Id > max)
                {
                    max = pregled.Id;
                }
            }
            return max + 1;
        }

        public int IzracunajIdOperacije()
        {
            int max = 0;
            List<Operacija> operacije = repositoryController.DobijOperacije();
            foreach (Operacija operacija in operacije)
            {
                if (operacija.Id > max)
                {
                    max = operacija.Id;
                }
            }
            return max + 1;
        }

        public int IzracunajIdOcene()
        {
            int max = 0;
            List<Ocena> ocene = repositoryController.DobijOcene();
            foreach (Ocena ocena in ocene)
            {
                if (ocena.IdOcene > max)
                {
                    max = ocena.IdOcene;
                }
            }
            return max + 1;
        }

        public int IzracunajIdAntiTrol()
        {
            int max = 0;
            List<AntiTrol> antiTrolovi = repositoryController.DobijAntiTrol();
            foreach (AntiTrol antiTrol in antiTrolovi)
            {
                if (antiTrol.Id > max)
                {
                    max = antiTrol.Id;
                }
            }
            return max + 1;
        }
    }
}
