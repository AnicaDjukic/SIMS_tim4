using bolnica.Forms;
using Bolnica.Controller.Prostorije;
using Bolnica.Localization;
using Bolnica.Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Bolnica.Forms.Upravnik.FactoryMethod
{
    class OperatorOpreme : IOperacijeNadEntitetima<Oprema, string>
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

        public List<Oprema> Pretraga(string text)
        {
            List<Oprema> oprema = new List<Oprema>();
            foreach (Oprema o in controllerOprema.DobaviSvuOpremu())
            {
                if (o.Sifra.ToLower().Contains(text.ToLower()))
                {
                    oprema.Remove(o);
                    oprema.Add(o);
                }

                if (o.Naziv.ToLower().Contains(text.ToLower()))
                {
                    oprema.Remove(o);
                    oprema.Add(o);
                }

                if (o.TipOpreme == TipOpreme.dinamicka)
                {
                    string dinamicka = LocalizedStrings.Instance["dinamička"];
                    if (dinamicka.Contains(text.ToLower()))
                    {
                        oprema.Remove(o);
                        oprema.Add(o);
                    }
                }

                if (o.TipOpreme == TipOpreme.staticka)
                {
                    string staticka = LocalizedStrings.Instance["statička"];
                    if (staticka.Contains(text.ToLower()))
                    {
                        oprema.Remove(o);
                        oprema.Add(o);
                    }
                }
            }

            return oprema;
        }
    }
}
