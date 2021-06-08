using Bolnica.Forms;
using Bolnica.Model.Pregledi;
using Bolnica.Repository.Pregledi;
using Model.Korisnici;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Bolnica.Service
{
    public class IstorijaPregledaService
    {
        public void Oceni_Lekara(PrikazPregleda SelektovaniRed)
        {
            var objekat = SelektovaniRed;

            if (objekat != null)
            {
                PrikazPregleda prikazPregleda = SelektovaniRed;
                if (PregledVecOcenjen(prikazPregleda))
                {
                    MessageBox.Show("Za izabrani pregled ste vec ocenili lekara!", "Upozorenje");
                }
                else
                {
                    FormPacijentWeb.Forma.Pocetna.Content = new FormOceniLekaraPage(prikazPregleda);
                }
            }
            else
            {
                MessageBox.Show("Morate odabrati pregled kod odredjenog lekara koga zelite da ocenite!", "Upozorenje");
            }
        }

        private bool PregledVecOcenjen(PrikazPregleda prikazPregleda)
        {
            List<Ocena> ocene = new FileRepositoryOcena().GetAll();
            foreach (Ocena ocena in ocene)
            {
                if (prikazPregleda.Id.Equals(ocena.Pregled.Id))
                {
                    return true;
                }
            }
            return false;
        }

        public void Oceni_Bolnicu(Pacijent pacijent)
        {
            FormPacijentWeb.Forma.Pocetna.Content = new FormOceniBolnicuPage(pacijent);
        }

        public void Istorija_Ocena_I_Komentara(Pacijent pacijent)
        {
            FormPacijentWeb.Forma.Pocetna.Content = new FormIstorijaOcenaPage(pacijent);
        }

        public void Anamneza(PrikazPregleda SelektovaniRed)
        {
            var objekat = SelektovaniRed;

            if (objekat != null)
            {
                PrikazPregleda prikazPregleda = SelektovaniRed;
                FormPacijentWeb.Forma.Pocetna.Content = new FormAnamnezaPage(prikazPregleda);
            }
            else
            {
                MessageBox.Show("Morate odabrati pregled za koji zelite da vidite anamnezu!", "Upozorenje");
            }
        }
    }
}
