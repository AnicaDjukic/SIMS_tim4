using Bolnica.Controller;
using Bolnica.Services;
using Bolnica.Service;
using Bolnica.Controller.Pregledi;
using Bolnica.Controller.Korisnici;

namespace Bolnica
{
    public class Injector
    {

        #region PACIJENT
        private PacijentPageService pacijentPageService = new PacijentPageService();
        public PacijentPageService PacijentPageService
        {
            get { return pacijentPageService;  }
        }
        private PacijentPageController pacijentPageController = new PacijentPageController();
        public PacijentPageController PacijentPageController
        {
            get { return pacijentPageController; }
        }
        #endregion

        #region LEKAR

        private TerminLekarService pregledService = new TerminLekarService();

        private ReceptLekarService receptService = new ReceptLekarService();

        private KomentarLekaLekarService komentarLekaService = new KomentarLekaLekarService();

        private LekLekarService izmeniLekLekarService = new LekLekarService();

        private AnamnezaLekarService napraviAnamnezuLekarService = new AnamnezaLekarService();

        private LekarService lekarService = new LekarService();

        private HospitalizacijaLekarService hospitalizujLekarService = new HospitalizacijaLekarService();

        private TerminLekarController terminLekarController = new TerminLekarController();

        private ReceptLekarController receptLekarController = new ReceptLekarController();

        private KomentarLekaLekarController komentarLekaLekarController = new KomentarLekaLekarController();

        private LekLekarController lekLekarController = new LekLekarController();

        private AnamnezaLekarController anamnezaLekarController = new AnamnezaLekarController();

        private LekarController lekarController = new LekarController();

        private HospitalizacijaLekarController hospitalizacijaLekarController = new HospitalizacijaLekarController();

        private InformacijeOPacijentuLekarController informacijeOPacijentuLekarController = new InformacijeOPacijentuLekarController();

        public InformacijeOPacijentuLekarController InformacijeOPacijentuLekarController
        {
            get { return informacijeOPacijentuLekarController; }
        }
        public HospitalizacijaLekarController HospitalizacijaLekarController
        {
            get { return hospitalizacijaLekarController; }
        }
        public TerminLekarController TerminLekarController
        {
            get { return terminLekarController; }
        }

        public ReceptLekarController ReceptLekarController
        {
            get { return receptLekarController; }
        }

        public KomentarLekaLekarController KomentarLekaLekarController
        {
            get { return komentarLekaLekarController; }
        }

        public LekLekarController LekLekarController
        {
            get { return lekLekarController; }
        }

        public AnamnezaLekarController AnamnezaLekarController
        {
            get { return anamnezaLekarController; }
        }

        public LekarController LekarController
        {
            get { return lekarController; }
        }
        #endregion

        #region UPRAVNIK

        private ControllerLek controllerLek = new ControllerLek();

        private ControllerSastojak controllerSastojak = new ControllerSastojak();

        private ControllerObavestenje controllerObavestenje = new ControllerObavestenje();
        public ControllerLek ControllerLek
        {
            get { return controllerLek; }
        }

        public ControllerSastojak ControllerSastojak
        {
            get { return controllerSastojak; }
        }

        public ControllerObavestenje ControllerObavestenje
        {
            get { return controllerObavestenje; }
        }

        #endregion

    }
}
