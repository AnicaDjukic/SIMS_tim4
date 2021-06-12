using Bolnica.Model.Pregledi;
using Bolnica.Service;
using Model.Pregledi;

namespace Bolnica.Controller
{
    public class AnamnezaPacijentController
    {
        private AnamnezaPacijentService service = new AnamnezaPacijentService();

        public void PostaviVremeComboBox()
        {
            service.PostaviVremeComboBox();
        }

        public void PostaviDatumComboBox()
        {
            service.PostaviDatumComboBox();
        }

        public void PostaviSveLekovePacijentu(Anamneza anamneza)
        {
            service.PostaviSveLekovePacijentu(anamneza);
        }

        public Anamneza DobijAnamnezu(PrikazPregleda prikazPregleda)
        {
            return service.DobijAnamnezu(prikazPregleda);
        }

        public Beleska DobijBelesku(Anamneza anamneza)
        {
            return service.DobijBelesku(anamneza);
        }

        public void SacuvajNovuBelesku(Beleska novaBeleska, PrikazPregleda prikaz)
        {
            service.SacuvajNovuBelesku(novaBeleska, prikaz);
        }
    }
}
