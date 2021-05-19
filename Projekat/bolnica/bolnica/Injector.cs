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
        private FileStoragePregledi storagePregledi = new FileStoragePregledi();

        private IzmeniINapraviTerminLekarService pregledService = new IzmeniINapraviTerminLekarService();

        private NapraviIVidiReceptLekarService receptService = new NapraviIVidiReceptLekarService();

        private KomentarLekaLekarService komentarLekaService = new KomentarLekaLekarService();

        private IzmeniLekLekarService izmeniLekLekarService = new IzmeniLekLekarService();

        private NapraviAnamnezuLekarService napraviAnamnezuLekarService = new NapraviAnamnezuLekarService();

        private LekarService lekarService = new LekarService();
        public FileStoragePregledi StoragePregledi
        {
            get { return storagePregledi; }
        }

        public IzmeniINapraviTerminLekarService PregledService
        {
            get { return pregledService; }
        }

        public NapraviIVidiReceptLekarService ReceptService
        {
            get { return receptService; }
        }

        public KomentarLekaLekarService KomentarLekaService
        {
            get { return komentarLekaService; }
        }

        public IzmeniLekLekarService IzmeniLekLekarService
        {
            get { return izmeniLekLekarService; }
        }

        public NapraviAnamnezuLekarService NapraviAnamnezuLekarService
        {
            get { return napraviAnamnezuLekarService; }
        }

        public LekarService LekarService
        {
            get { return lekarService; }
        }
    }
}
