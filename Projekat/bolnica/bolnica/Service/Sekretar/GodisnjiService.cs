using Bolnica.Model.Korisnici;
using Bolnica.Repository.Korisnici;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.Service.Sekretar
{
    public class GodisnjiService
    {
        private IRepositoryGodisnji skladisteGodisnji;

        public GodisnjiService() 
        {
            skladisteGodisnji = new FileRepositoryGodisnji();
        }


    }
}
