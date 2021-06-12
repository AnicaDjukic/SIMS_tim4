using Bolnica.Service;
using System;

namespace Bolnica.Controller
{
    public class RacunajIdController
    {
        private RacunajIdService service = new RacunajIdService();

        public int IzracunajIdBeleske()
        {
            return service.IzracunajIdBeleske();
        }

        public int IzracunajIdPregleda()
        {
            return service.IzracunajIdPregleda();
        }

        public int IzracunajIdOperacije()
        {
            return service.IzracunajIdOperacije();
        }

        public int IzracunajIdOcene()
        {
            return service.IzracunajIdOcene();
        }

        public int IzracunajIdAntiTrol()
        {
            return service.IzracunajIdAntiTrol();
        }
    }
}
