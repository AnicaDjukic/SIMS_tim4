using Bolnica.Controller;
using Bolnica.Controller.Korisnici;
using Bolnica.Controller.Pregledi;
using Bolnica.Controller.Prostorije;
using Bolnica.Service;
using Bolnica.Controller.Korisnici;
using Bolnica.Services;


namespace Bolnica
{
    public class Injector
    {

        #region PACIJENT
        private PacijentPageService pacijentPageService = new PacijentPageService();
        public PacijentPageService PacijentPageService
        {
            get { return pacijentPageService; }
        }
        private PacijentPageController pacijentPageController = new PacijentPageController();
        public PacijentPageController PacijentPageController
        {
            get { return pacijentPageController; }
        }

        private ZakaziPregledPacijentService zakaziPregledPacijentService = new ZakaziPregledPacijentService();
        public ZakaziPregledPacijentService ZakaziPregledPacijentService
        {
            get { return zakaziPregledPacijentService; }
        }
        private ZazaziPregledPacijentController zakaziPregledPacijentController = new ZazaziPregledPacijentController();
        public ZazaziPregledPacijentController ZakaziPregledPacijentController
        {
            get { return zakaziPregledPacijentController; }
        }

        private IstorijaPregledaService istorijaPregledaService = new IstorijaPregledaService();
        public IstorijaPregledaService IstorijaPregledaService
        {
            get { return istorijaPregledaService; }
        }
        private IstorijaPregledaController istorijaPregledaController = new IstorijaPregledaController();
        public IstorijaPregledaController IstorijaPregledaController
        {
            get { return istorijaPregledaController; }
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

        private PacijentLekarController informacijeOPacijentuLekarController = new PacijentLekarController();

        public PacijentLekarController InformacijeOPacijentuLekarController
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

        private ControllerOprema controllerOprema = new ControllerOprema();

        private ControllerProstorija controllerProstorija = new ControllerProstorija();

        private ControllerBolnickaSoba controllerBolnickaSoba = new ControllerBolnickaSoba();

        private ControllerBuducaZaliha controllerBuducaZaliha = new ControllerBuducaZaliha();

        private ControllerRenoviranje controllerRenoviranje = new ControllerRenoviranje();

        private ControllerZaliha controllerZaliha = new ControllerZaliha();

        private ControllerOcenaAplikacije controllerOcenaAplikacije = new ControllerOcenaAplikacije();

        private ControllerPregled controllerPregled = new ControllerPregled();

        private ControllerOperacija controllerOperacija = new ControllerOperacija();
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

        public ControllerOprema ControllerOprema
        {
            get { return controllerOprema; }
        }

        public ControllerProstorija ControllerProstorija 
        { 
            get { return controllerProstorija; }
        }
        public ControllerBolnickaSoba ControllerBolnickaSoba 
        { 
            get { return controllerBolnickaSoba; } 
        }

        public ControllerBuducaZaliha ControllerBuducaZaliha
        {
            get { return controllerBuducaZaliha; }
        }

        public ControllerRenoviranje ControllerRenoviranje
        {
            get { return controllerRenoviranje; }
        }

        public ControllerZaliha ControllerZaliha
        {
            get { return controllerZaliha; }
        }

        public ControllerOcenaAplikacije ControllerOcenaAplikacije
        {
            get { return controllerOcenaAplikacije; }
        }

        public ControllerPregled ControllerPregled
        {
            get { return controllerPregled; }
        }

        public ControllerOperacija ControllerOperacija
        {
            get { return controllerOperacija; }
        }

        #endregion

        #region REPOZITORIJUMI
        private RepositoryService repositoryService = new RepositoryService();
        public RepositoryService RepositoryService
        {
            get { return repositoryService; }
        }
        private RepositoryController repositoryController = new RepositoryController();
        public RepositoryController RepositoryController
        {
            get { return repositoryController; }
        }
        #endregion

    }
}
