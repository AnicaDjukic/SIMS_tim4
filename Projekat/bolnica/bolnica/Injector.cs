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

        private PregledService pregledService = new PregledService();

        private ReceptService receptService = new ReceptService();

        private KomentarLekaService komentarLekaService = new KomentarLekaService();

        private IzmeniLekLekarService izmeniLekLekarService = new IzmeniLekLekarService();

        public FileStoragePregledi StoragePregledi
        {
            get { return storagePregledi; }
        }

        public PregledService PregledService
        {
            get { return pregledService; }
        }

        public ReceptService ReceptService
        {
            get { return receptService; }
        }

        public KomentarLekaService KomentarLekaService
        {
            get { return komentarLekaService; }
        }

        public IzmeniLekLekarService IzmeniLekLekarService
        {
            get { return izmeniLekLekarService; }
        }
    }
}
