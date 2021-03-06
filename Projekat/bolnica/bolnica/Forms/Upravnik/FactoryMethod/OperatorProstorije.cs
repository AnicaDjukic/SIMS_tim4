using bolnica.Forms;
using Bolnica.Controller.Prostorije;
using Bolnica.Localization;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Bolnica.Forms.Upravnik.FactoryMethod
{
    public class OperatorProstorije : IOperacijeNadEntitetima<Prostorija, string>
    {
        ControllerProstorija controllerProstorija = new ControllerProstorija();
        ControllerBolnickaSoba controllerBolnickaSoba = new ControllerBolnickaSoba();
        public void Dodavanje()
        {
            var p = new CreateFormProstorije(null);
            p.Show();
        }

        public void Prikazivanje(string id)
        {
            var s = new ViewFormProstorije(id);
            s.Show();
        }

        public void Izmena(string id)
        {
            var s = new CreateFormProstorije(id);
            s.Show();
        }

        public void Brisanje(Prostorija zaBrisanje)
        {
            MessageBoxResult rsltMessageBox = UpitZaBrisanjeProstorije(zaBrisanje.BrojProstorije);
            if (rsltMessageBox == MessageBoxResult.Yes)
            {
                if (zaBrisanje.Zauzeta)
                    MessageBox.Show(LocalizedStrings.Instance["Prostorija je trenutno zauzeta, ne možete je obrisati."]);
                else
                    ObrisiProstoriju(zaBrisanje);
            }
        }

        private MessageBoxResult UpitZaBrisanjeProstorije(string brojProstorije)
        {
            string sMessageBoxText = LocalizedStrings.Instance["Da li ste sigurni da želite da obrišete prostoriju"] + " \"" + brojProstorije + "\"?";
            string sCaption = LocalizedStrings.Instance["Brisanje prostorije"];

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            return rsltMessageBox;
        }

        private void ObrisiProstoriju(Prostorija prostorija)
        {
            if (prostorija.TipProstorije != TipProstorije.bolnickaSoba)
                controllerProstorija.ObrisiProstoriju(prostorija.BrojProstorije);
            else
                controllerBolnickaSoba.ObrisiBolnickuSobu(prostorija.BrojProstorije);

            FormUpravnik.Prostorije.Remove(prostorija);
        }

        public List<Prostorija> Pretraga(string text)
        {
            throw new NotImplementedException();
        }
    }
}
