using Bolnica.Commands;
using Bolnica.DTO;
using Bolnica.Model.Korisnici;
using Model.Korisnici;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace Bolnica.ViewModel
{
    public class ZakaziPregledPacijentViewModel : ViewModel
    {
        private Pacijent pacijent = new Pacijent();

        private Injector inject;
        public Injector Inject
        {
            get { return inject; }
            set
            {
                inject = value;
            }
        }

        private DateTime selektovaniDatum;
        public DateTime SelektovaniDatum
        {
            get { return selektovaniDatum; }
            set
            {
                selektovaniDatum = value;
                OnPropertyChanged();
            }
        }

        private String selektovaniSat;
        public String SelektovaniSat
        {
            get { return selektovaniSat; }
            set
            {
                selektovaniSat = value.Split(":")[1].Trim();
                OnPropertyChanged();
            }
        }

        private String selektovaniMinut;
        public String SelektovaniMinut
        {
            get { return selektovaniMinut; }
            set
            {
                selektovaniMinut = value.Split(":")[1].Trim();
                OnPropertyChanged();
            }
        }

        private String selektovaniLekar;
        public String SelektovaniLekar
        {
            get { return selektovaniLekar; }
            set
            {
                selektovaniLekar = value.Split(":")[1].Trim();
                OnPropertyChanged();
            }
        }

        private bool datumEnable;
        public bool DatumEnable
        {
            get { return datumEnable; }
            set
            {
                datumEnable = value;
                OnPropertyChanged();
            }
        }

        private bool satEnable;
        public bool SatEnable
        {
            get { return satEnable; }
            set
            {
                satEnable = value;
                OnPropertyChanged();
            }
        }

        private bool minutEnable;
        public bool MinutEnable
        {
            get { return minutEnable; }
            set
            {
                minutEnable = value;
                OnPropertyChanged();
            }
        }

        private bool lekarEnable;
        public bool LekarEnable
        {
            get { return lekarEnable; }
            set
            {
                lekarEnable = value;
                OnPropertyChanged();
            }
        }

        private bool potvrdiEnable;
        public bool PotvrdiEnable
        {
            get { return potvrdiEnable; }
            set
            {
                potvrdiEnable = value;
                OnPropertyChanged();
            }
        }

        private bool nasiPredloziEnable;
        public bool NasiPredloziEnable
        {
            get { return nasiPredloziEnable; }
            set
            {
                nasiPredloziEnable = value;
                OnPropertyChanged();
            }
        }

        private bool radioDatumEnable;
        public bool RadioDatumEnable
        {
            get { return radioDatumEnable; }
            set
            {
                radioDatumEnable = value;
                OnPropertyChanged();
            }
        }

        private bool radioDatumChecked;
        public bool RadioDatumChecked
        {
            get { return radioDatumChecked; }
            set
            {
                radioDatumChecked = value;
                OnPropertyChanged();
            }
        }

        private bool radioLekarChecked;
        public bool RadioLekarChecked
        {
            get { return radioLekarChecked; }
            set
            {
                radioLekarChecked = value;
                OnPropertyChanged();
            }
        }

        private bool radioLekarEnable;
        public bool RadioLekarEnable
        {
            get { return radioLekarEnable; }
            set
            {
                radioLekarEnable = value;
                OnPropertyChanged();
            }
        }

        private Brush datumPozadina;
        public Brush DatumPozadina
        {
            get { return datumPozadina; }
            set
            {
                datumPozadina = value;
                OnPropertyChanged();
            }
        }

        private Brush satPozadina;
        public Brush SatPozadina
        {
            get { return satPozadina; }
            set
            {
                satPozadina = value;
                OnPropertyChanged();
            }
        }

        private Brush minutPozadina;
        public Brush MinutPozadina
        {
            get { return minutPozadina; }
            set
            {
                minutPozadina = value;
                OnPropertyChanged();
            }
        }

        private Brush lekarPozadina;
        public Brush LekarPozadina
        {
            get { return lekarPozadina; }
            set
            {
                lekarPozadina = value;
                OnPropertyChanged();
            }
        }

        #region Potvrdi
        private RelayCommand potvrdiZakazivanjePregledaKomanda;
        public RelayCommand PotvrdiZakazivanjePregledaKomanda
        {
            get { return potvrdiZakazivanjePregledaKomanda; }
            set
            {
                potvrdiZakazivanjePregledaKomanda = value;
            }
        }
        public void Executed_PotvrdiZakazivanjePregledaKomanda(object obj)
        {
            inject.ZakaziPregledPacijentController.Potvrdi(new ZakazaniPregledDTO(pacijent, SelektovaniDatum, SelektovaniSat, SelektovaniMinut, SelektovaniLekar));
        }

        public bool CanExecute_PotvrdiZakazivanjePregledaKomanda(object obj)
        {
            return true;
        }
        #endregion

        #region Otkazi
        private RelayCommand otkaziZakazivanjePregledaKomanda;
        public RelayCommand OtkaziZakazivanjePregledaKomanda
        {
            get { return otkaziZakazivanjePregledaKomanda; }
            set
            {
                otkaziZakazivanjePregledaKomanda = value;
            }
        }
        public void Executed_OtkaziZakazivanjePregledaKomanda(object obj)
        {
            inject.ZakaziPregledPacijentController.Otkazi(new ZakazaniPregledDTO(pacijent, SelektovaniDatum, SelektovaniSat, SelektovaniMinut, SelektovaniLekar));
        }

        public bool CanExecute_OtkaziZakazivanjePregledaKomanda(object obj)
        {
            return true;
        }
        #endregion

        #region NasiPredlozi
        private RelayCommand nasiPredloziKomanda;
        public RelayCommand NasiPredloziKomanda
        {
            get { return nasiPredloziKomanda; }
            set
            {
                nasiPredloziKomanda = value;
            }
        }
        public void Executed_NasiPredloziKomanda(object obj)
        {
            inject.ZakaziPregledPacijentController.NasiPredlozi(new ZakazaniPregledDTO(pacijent, SelektovaniDatum, SelektovaniSat, SelektovaniMinut, SelektovaniLekar));
        }

        public bool CanExecute_NasiPredloziKomanda(object obj)
        {
            return true;
        }
        #endregion

        #region DatumPrioritet
        private RelayCommand checkedDatum;
        public RelayCommand CheckedDatum
        {
            get { return checkedDatum; }
            set
            {
                checkedDatum = value;
            }
        }
        public void Executed_CheckedDatum(object obj)
        {
            DatumEnable = true;
            DatumPozadina = Brushes.Aqua;
            SatEnable = false;
            MinutEnable = false;
            LekarEnable = false;
            potvrdiEnable = false;
            nasiPredloziEnable = true;
            RadioDatumEnable = false;
            RadioLekarEnable = false;
        }

        public bool CanExecute_CheckedDatum(object obj)
        {
            return true;
        }
        #endregion

        #region LekarPrioritet
        private RelayCommand checkedLekar;
        public RelayCommand CheckedLekar
        {
            get { return checkedLekar; }
            set
            {
                checkedLekar = value;
            }
        }
        public void Executed_CheckedLekar(object obj)
        {
            DatumEnable = false;
            SatEnable = false;
            MinutEnable = false;
            LekarEnable = true;
            LekarPozadina = Brushes.Aqua;
            potvrdiEnable = false;
            nasiPredloziEnable = true;
            RadioDatumEnable = false;
            RadioLekarEnable = false;
        }

        public bool CanExecute_CheckedLekar(object obj)
        {
            return true;
        }
        #endregion

        public ZakaziPregledPacijentViewModel(Pacijent trenutniPacijent)
        {
            Inject = new Injector();
            pacijent = trenutniPacijent;

            RadioDatumEnable = true;
            RadioLekarEnable = true;

            DatumEnable = false;
            SatEnable = false;
            MinutEnable = false;
            LekarEnable = false;
            PotvrdiEnable = false;
            NasiPredloziEnable = false;

            SelektovaniDatum = DateTime.Today;

            PostaviKomande();
        }

        private void PostaviKomande()
        {
            PotvrdiZakazivanjePregledaKomanda = new RelayCommand(Executed_PotvrdiZakazivanjePregledaKomanda, CanExecute_PotvrdiZakazivanjePregledaKomanda);
            OtkaziZakazivanjePregledaKomanda = new RelayCommand(Executed_OtkaziZakazivanjePregledaKomanda, CanExecute_OtkaziZakazivanjePregledaKomanda);
            NasiPredloziKomanda = new RelayCommand(Executed_NasiPredloziKomanda, CanExecute_NasiPredloziKomanda);
            CheckedDatum = new RelayCommand(Executed_CheckedDatum, CanExecute_CheckedDatum);
            CheckedLekar = new RelayCommand(Executed_CheckedLekar, CanExecute_CheckedLekar);
        }
        
        private void Checked_Datum()
        {
            DatumEnable = true;
            DatumPozadina = Brushes.Aqua;
            SatEnable = false;
            MinutEnable = false;
            LekarEnable = false;
            potvrdiEnable = false;
            nasiPredloziEnable = true;
            RadioDatumEnable = false;
            RadioLekarEnable = false;
        }

        private void Checked_Lekar()
        {
            DatumEnable = false;
            SatEnable = false;
            MinutEnable = false;
            LekarEnable = true;
            LekarPozadina = Brushes.Aqua;
            potvrdiEnable = false;
            nasiPredloziEnable = true;
            RadioDatumEnable = false;
            RadioLekarEnable = false;
        }

        private void SelectedDateChanged_Datum()
        {
            DatumEnable = false;
            DatumPozadina = Brushes.Green;
            SatEnable = true;
            SatPozadina = Brushes.Aqua;
            MinutEnable = false;
        }

        private void SelectionChanged_Sat()
        {
            DatumEnable = false;
            SatEnable = false;
            MinutEnable = true;
            MinutPozadina = Brushes.Aqua;
        }

        private void SelectionChanged_Minut()
        {
            SatEnable = false;
            MinutEnable = false;
            DatumEnable = false;
            if (RadioDatumChecked)
            {
                LekarEnable = true;
                LekarPozadina = Brushes.Aqua;
            }
            else
            {
                PotvrdiEnable = true;
            }
        }

        private void SelectionChanged_Lekar()
        {
            SatEnable = false;
            MinutEnable = false;
            LekarEnable = false;
            if (RadioLekarChecked)
            {
                DatumEnable = true;
                DatumPozadina = Brushes.Aqua;
            }
            else
            {
                PotvrdiEnable = true;
            }
        }

    }
}
