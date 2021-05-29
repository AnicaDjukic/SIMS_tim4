using Bolnica.Controller;
using Bolnica.Services;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica
{
    public class Injector
    {
        private IzmeniINapraviTerminLekarService pregledService = new IzmeniINapraviTerminLekarService();

        private NapraviIVidiReceptLekarService receptService = new NapraviIVidiReceptLekarService();

        private KomentarLekaLekarService komentarLekaService = new KomentarLekaLekarService();

        private IzmeniLekLekarService izmeniLekLekarService = new IzmeniLekLekarService();

        private NapraviAnamnezuLekarService napraviAnamnezuLekarService = new NapraviAnamnezuLekarService();

        private LekarService lekarService = new LekarService();

        private HospitalizujLekarService hospitalizujLekarService = new HospitalizujLekarService();

        private IzmeniINapraviTerminLekarController pregledController = new IzmeniINapraviTerminLekarController();

        private NapraviIVidiReceptLekarController receptController = new NapraviIVidiReceptLekarController();

        private KomentarLekaLekarController komentarLekaController = new KomentarLekaLekarController();

        private IzmeniLekLekarController izmeniLekLekarController = new IzmeniLekLekarController();

        private NapraviAnamnezuLekarController napraviAnamnezuLekarController = new NapraviAnamnezuLekarController();

        private LekarController lekarController = new LekarController();

        private HospitalizujLekarController hospitalizujLekarController = new HospitalizujLekarController();

        private InformacijeOPacijentuLekarController informacijeOPacijentuLekarController = new InformacijeOPacijentuLekarController();

        public InformacijeOPacijentuLekarController InformacijeOPacijentuLekarController
        {
            get { return informacijeOPacijentuLekarController; }
        }
        public HospitalizujLekarController HospitalizujLekarController
        {
            get { return hospitalizujLekarController; }
        }
        public IzmeniINapraviTerminLekarController PregledController
        {
            get { return pregledController; }
        }

        public NapraviIVidiReceptLekarController ReceptController
        {
            get { return receptController; }
        }

        public KomentarLekaLekarController KomentarLekaController
        {
            get { return komentarLekaController; }
        }

        public IzmeniLekLekarController IzmeniLekLekarController
        {
            get { return izmeniLekLekarController; }
        }

        public NapraviAnamnezuLekarController NapraviAnamnezuLekarController
        {
            get { return napraviAnamnezuLekarController; }
        }

        public LekarController LekarController
        {
            get { return lekarController; }
        }
    }
}
