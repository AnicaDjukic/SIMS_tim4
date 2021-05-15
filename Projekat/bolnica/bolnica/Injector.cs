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
    }
}
