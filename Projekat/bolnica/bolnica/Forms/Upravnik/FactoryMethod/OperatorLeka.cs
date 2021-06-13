using bolnica.Forms;
using Bolnica.Controller.Pregledi;
using Bolnica.DTO;
using Bolnica.Localization;
using Bolnica.Model.Pregledi;
using Bolnica.ViewModel.Upravnik;
using Model.Pregledi;
using System.Collections.Generic;
using System.Windows;

namespace Bolnica.Forms.Upravnik.FactoryMethod
{
    public class OperatorLeka : IOperacijeNadEntitetima<Lek, int>
    {
        ControllerLek controllerLek = new ControllerLek();
        public void Dodavanje()
        {
            LekDTO noviLek = new LekDTO();
            ViewModelCreateFormLekovi vm = new ViewModelCreateFormLekovi(noviLek.Id);
            CreateFormLekovi l = new CreateFormLekovi(vm);
        }

        public void Prikazivanje(int id)
        {
            var s = new ViewFormLek(id);
            s.Show();
        }

        public void Izmena(int id)
        {
            Lek lek = controllerLek.DobaviLek(id);
            if (lek.Status == StatusLeka.cekaValidaciju)
                MessageBox.Show(LocalizedStrings.Instance["Nije moguće izmeniti lek koji čeka validaciju!"]);
            else
            {
                ViewModelCreateFormLekovi vm = new ViewModelCreateFormLekovi(id);
                CreateFormLekovi s = new CreateFormLekovi(vm);
            }
        }

        public void Brisanje(Lek zaBrisanje)
        {
            MessageBoxResult rsltMessageBox = UpitZaBrisanjeLeka(zaBrisanje);
            if (rsltMessageBox == MessageBoxResult.Yes)
            {
                if (zaBrisanje.Status == StatusLeka.cekaValidaciju)
                {
                    MessageBox.Show(LocalizedStrings.Instance["Nije moguće obrisati lek koji čeka validaciju!"]);
                }
                else
                {
                    controllerLek.ObrisiLek(zaBrisanje.Id);
                    FormUpravnik.Lekovi.Remove(zaBrisanje);
                }
            }
        }

        private MessageBoxResult UpitZaBrisanjeLeka(Lek row)
        {
            string sMessageBoxText = LocalizedStrings.Instance["Da li ste sigurni da želite da obrišete lek sa nazivom"] + " \"" + row.Naziv + " \"" + LocalizedStrings.Instance["i id-jem"] + " \"" + row.Id + "\"?";
            string sCaption = LocalizedStrings.Instance["Brisanje leka"];

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            return rsltMessageBox;
        }

        public List<Lek> Pretraga(string text)
        {
            throw new System.NotImplementedException();
        }
    }
}
