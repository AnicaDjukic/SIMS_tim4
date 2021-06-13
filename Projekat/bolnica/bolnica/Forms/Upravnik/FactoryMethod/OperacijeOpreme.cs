﻿using bolnica.Forms;
using Bolnica.Controller.Prostorije;
using Bolnica.Localization;
using Bolnica.Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Bolnica.Forms.Upravnik.FactoryMethod
{
    class OperacijeOpreme : IOperacije<Oprema, string>
    {
        ControllerOprema controllerOprema = new ControllerOprema();

        public void Dodavanje()
        {
            var o = new CreateFormOprema(null);
            o.Show();
        }

        public void Prikazivanje(string id)
        {
            var s = new ViewFormOprema(id);
            s.Show();
        }

        public void Izmena(string id)
        {
            var s = new CreateFormOprema(id);
            s.Show();
        }

        public void Brisanje(Oprema zaBrisanje)
        {
            MessageBoxResult rsltMessageBox = UpitZaBrisanjeOpreme(zaBrisanje);
            if (rsltMessageBox == MessageBoxResult.Yes)
            {
                controllerOprema.ObrisiOpremu(zaBrisanje.Sifra);
                FormUpravnik.Oprema.Remove(zaBrisanje);
            }
        }

        private MessageBoxResult UpitZaBrisanjeOpreme(Oprema row)
        {
            string sMessageBoxText = LocalizedStrings.Instance["Da li ste sigurni da želite da obrišete opremu sa nazivom"] + " \"" + row.Naziv + " \"" + LocalizedStrings.Instance["i šifrom"] + " \"" + row.Sifra + "\"?";
            string sCaption = LocalizedStrings.Instance["Brisanje opreme"];

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            return rsltMessageBox;
        }
    }
}
