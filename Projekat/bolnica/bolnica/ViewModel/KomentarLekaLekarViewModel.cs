using Bolnica.Commands;
using Bolnica.Model.Pregledi;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bolnica.ViewModel
{
    public class KomentarLekaLekarViewModel : ViewModel
    {

        private string komentar;

        public string Komentar
        {
            get { return komentar; }
            set
            {
                komentar = value;
                OnPropertyChanged();
            }
        }

        private PrikazLek prik = new PrikazLek();

        private List<Lek> leko = new List<Lek>();

        private FileStorageLek sviLekovi = new FileStorageLek();

        private Injector inject;
        public Injector Inject
        {
            get { return inject; }
            set
            {
                inject = value;
            }
        }
        public Action CloseAction { get; set; }

        private RelayCommand potvrdiCommand;
        public RelayCommand PotvrdiCommand
        {
            get { return potvrdiCommand; }
            set
            {
                potvrdiCommand = value;

            }
        }

        public void Executed_PotvrdiCommand(object obj)
        {
            inject.KomentarLekaService.Potvrdi(Komentar,prik,leko);
            CloseAction();
        }

        public bool CanExecute_PotvrdiCommand(object obj)
        {
            return true;
        }
        private RelayCommand zatvoriCommand;
        public RelayCommand ZatvoriCommand
        {
            get { return zatvoriCommand; }
            set
            {
                zatvoriCommand = value;

            }
        }

        public void Executed_ZatvoriCommand(object obj)
        {
            CloseAction();
        }

        public bool CanExecute_ZatvoriCommand(object obj)
        {
            return true;
        }

        public KomentarLekaLekarViewModel(PrikazLek p)
        {
            Komentar = "";
            Inject = new Injector();
            leko = sviLekovi.GetAll();
            for (int i = 0; i < leko.Count; i++)
            {
                if (leko[i].Status.Equals(StatusLeka.odbijen) || leko[i].Obrisan)
                {
                    leko.RemoveAt(i);
                    i--;
                }
            }
            prik = p;
            ZatvoriCommand = new RelayCommand(Executed_ZatvoriCommand, CanExecute_ZatvoriCommand);
            PotvrdiCommand = new RelayCommand(Executed_PotvrdiCommand, CanExecute_PotvrdiCommand);
        }
    }
}
