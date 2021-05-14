using Bolnica.Commands;
using Bolnica.Model.Korisnici;
using Model.Korisnici;
using Model.Pacijenti;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Bolnica.ViewModel
{
    public class NapraviTerminLekarViewModel : ViewModel
    {
        private List<Lekar> lekariTrenutni = new List<Lekar>();
        private Lekar ulogovaniLekar = new Lekar();
        private bool jeOpe = false;
        private FileStoragePacijenti sviPacijenti = new FileStoragePacijenti();
        private FileStorageProstorija sveProstorije = new FileStorageProstorija();
        private FileStoragePregledi sviPregledi = new FileStoragePregledi();
        private List<Pacijent> pacijentiZa = new List<Pacijent>();
        private List<Prostorija> prostorijaZa = new List<Prostorija>();
        private FileStorageLekar sviLekari = new FileStorageLekar();
        private string zaFilLek = "";
        private DateTime zaFilLekDat = new DateTime();
        
        private List<Pregled> pregledi;
        private List<Operacija> operacije;

        public List<String> specijalizacije { get; set;}

        private List<String> itemSourceSpecijalizacija;
        public List<String> ItemSourceSpecijalizacija { get { return itemSourceSpecijalizacija; }
            set
            {
                itemSourceSpecijalizacija = value;
                OnPropertyChanged();
            }}
        private List<String> itemSourcePrezimeB;
        public List<String> ItemSourcePrezimeB
        {
            get { return itemSourcePrezimeB; }
            set
            {
                itemSourcePrezimeB = value;
                OnPropertyChanged();
            }
        }
        private List<String> itemSourceLekarB;
        public List<String> ItemSourceLekarB
        {
            get { return itemSourceLekarB; }
            set
            {
                itemSourceLekarB = value;
                OnPropertyChanged();
            }
        }

        private Visibility itemSourceVisibility;
        public Visibility ItemSourceVisibility
        {
            get { return itemSourceVisibility; }
            set
            {
                itemSourceVisibility = value;
                OnPropertyChanged();
            }
        }

        private List<TimeSpan> itemSourceVremeB;
        public List<TimeSpan> ItemSourceVremeB
        {
            get { return itemSourceVremeB; }
            set
            {
                itemSourceVremeB = value;
                OnPropertyChanged();
            }
        }
        private List<String> itemSourceBrojProstorije;
        public List<String> ItemSourceBrojProstorije
        {
            get { return itemSourceBrojProstorije; }
            set
            {
                itemSourceBrojProstorije = value;
                OnPropertyChanged();
            }
        }
        private List<TipOperacije> itemSourceTipOperacije;
        public List<TipOperacije> ItemSourceTipOperacije
        {
            get { return itemSourceTipOperacije; }
            set
            {
                itemSourceTipOperacije = value;
                OnPropertyChanged();
            }
        }
        private bool itemSourceDaLiJeOperacije;
        public bool ItemSourceDaLiJeOperacije
        {
            get { return itemSourceDaLiJeOperacije; }
            set
            {
                itemSourceDaLiJeOperacije = value;
                OnPropertyChanged();
            }
        }

        private bool itemSourceDaLiJeHitan;
        public bool ItemSourceDaLiJeHitan
        {
            get { return itemSourceDaLiJeHitan; }
            set
            {
                itemSourceDaLiJeHitan = value;
                OnPropertyChanged();
            }
        }

        private bool itemSourceDaLiJeTrajanje;
        public bool ItemSourceDaLiJeTrajanje
        {
            get { return itemSourceDaLiJeTrajanje; }
            set
            {
                itemSourceDaLiJeTrajanje = value;
                OnPropertyChanged();
            }
        }

        private bool itemSourceVremeComboOpen;
        public bool ItemSourceVremeComboOpen {
            get { return itemSourceVremeComboOpen; }
            set
            {
                itemSourceVremeComboOpen = value;
                OnPropertyChanged();
            }
        }

        private bool itemSourceSpecijalizacijaComboOpen;
        public bool ItemSourceSpecijalizacijaComboOpen
        {
            get { return itemSourceSpecijalizacijaComboOpen; }
            set
            {
                itemSourceSpecijalizacijaComboOpen = value;
                OnPropertyChanged();
            }
        }

        private bool itemSourceLekarComboOpen;
        public bool ItemSourceLekarComboOpen
        {
            get { return itemSourceLekarComboOpen; }
            set
            {
                itemSourceLekarComboOpen = value;
                OnPropertyChanged();
            }
        }

        private bool itemSourceOperacijaComboOpen;
        public bool ItemSourceOperacijaComboOpen
        {
            get { return itemSourceOperacijaComboOpen; }
            set
            {
                itemSourceOperacijaComboOpen = value;
                OnPropertyChanged();
            }
        }

        private bool itemSourcePacijentComboOpen;
        public bool ItemSourcePacijentComboOpen
        {
            get { return itemSourcePacijentComboOpen; }
            set
            {
                itemSourcePacijentComboOpen = value;
                OnPropertyChanged();
            }
        }

        private bool itemSourceProstorijaComboOpen;
        public bool ItemSourceProstorijaComboOpen
        {
            get { return itemSourceProstorijaComboOpen; }
            set
            {
                itemSourceProstorijaComboOpen = value;
                OnPropertyChanged();
            }
        }

        private string specijalizacija;
        public string Specijalizacija {
            get { return specijalizacija; }
            set
            {
                specijalizacija = value;
                OnPropertyChanged();
            }
        }
        public string prezimeB { get; set; }

        public string lekarB { get; set; }

        public DateTime datumB { get; set; }

        public string vremeB { get; set; }

        public string brojProstorijeB { get; set; }

        public TipOperacije tipOperacijeB { get; set; }

        public string trajanjeB;

        public string TrajanjeB
        {
            get { return trajanjeB; }
            set
            {
                trajanjeB = value;
                OnPropertyChanged();
            }
        }

        private Injector inject;

        private Pregled preg;

        public Injector Inject
        {
            get { return inject; }
            set
            {
                inject = value;
            }
        }

        public Pregled Preg
        {
            get { return preg; }
            set
            {
                preg = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand dodajPregledLekarCommand;

        public RelayCommand DodajPregledLekarCommand
        {
            get { return dodajPregledLekarCommand; }
            set
            {
                dodajPregledLekarCommand = value;
            }
        }

        public void Executed_DodajPregledLekarCommand(object obj)
        {
            if (!inject.PregledService.PostojiLekar(Specijalizacija, lekarB))
            {
                MessageBox.Show("Ne postoji lekar");
                return;
            }
            if (!inject.PregledService.LekarSlobodanUToVreme(lekarB, datumB, TrajanjeB, vremeB))
            {
                MessageBox.Show("Lekar nije slobodan u to vreme");
                return;
            }
            if (!inject.PregledService.PacijentSlobodanUToVreme(pacijentiZa, prezimeB, datumB, TrajanjeB, vremeB))
            {
                MessageBox.Show("Pacijent nije slobodan u to vreme");
                return;
            }
            if (!inject.PregledService.PostojiProstorija(prostorijaZa, brojProstorijeB))
            {
                MessageBox.Show("Prostorija ne postoji");
                return;
            }
            if (!inject.PregledService.ProstorijaSlobodna(prostorijaZa, brojProstorijeB, datumB))
            {
                MessageBox.Show("Prostorija nije slobodna");
                return;
            }
            inject.PregledService.PotvrdiIzmenu(datumB.ToShortDateString(), vremeB, TrajanjeB, lekariTrenutni, lekarB, pacijentiZa, prezimeB, prostorijaZa, ItemSourceDaLiJeOperacije, tipOperacijeB, ItemSourceDaLiJeHitan, brojProstorijeB, ulogovaniLekar);
            CloseAction();
            
        }

        public bool CanExecute_DodajPregledLekarCommand(object obj)
        {
            return true;
        }
        private RelayCommand vremeComboOpenCommand;
        public RelayCommand VremeComboOpenCommand
        {
            get { return vremeComboOpenCommand; }
            set
            {
                vremeComboOpenCommand = value;
               
            }
        }

        public void Executed_VremeComboOpenCommand(object obj)
        {
            ItemSourceVremeComboOpen = true;

        }

        public bool CanExecute_VremeComboOpenCommand(object obj)
        {
            return true;
        }
        private RelayCommand specijalizacijaComboOpenCommand;
        public RelayCommand SpecijalizacijaComboOpenCommand
        {
            get { return specijalizacijaComboOpenCommand; }
            set
            {
                specijalizacijaComboOpenCommand = value;

            }
        }

        public void Executed_SpecijalizacijaComboOpenCommand(object obj)
        {
            ItemSourceSpecijalizacijaComboOpen = true;

        }

        public bool CanExecute_SpecijalizacijaComboOpenCommand(object obj)
        {
            return true;
        }
        private RelayCommand specijalizacijaComboOpenTabCommand;
        public RelayCommand SpecijalizacijaComboOpenTabCommand
        {
            get { return specijalizacijaComboOpenTabCommand; }
            set
            {
                specijalizacijaComboOpenTabCommand = value;
                
            }
        }

        public void Executed_SpecijalizacijaComboOpenTabCommand(object obj)
        {
            
            ItemSourceLekarB = inject.PregledService.SpecijalizacijaComboOpenTab(specijalizacije, Specijalizacija, lekariTrenutni);
            ItemSourceSpecijalizacijaComboOpen = false;
            TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
            (Keyboard.FocusedElement as FrameworkElement).MoveFocus(request);

        }

        public bool CanExecute_SpecijalizacijaComboOpenTabCommand(object obj)
        {
            return true;
        }

        private RelayCommand lekarComboOpenTabCommand;
        public RelayCommand LekarComboOpenTabCommand
        {
            get { return lekarComboOpenTabCommand; }
            set
            {
                lekarComboOpenTabCommand = value;

            }
        }

        public void Executed_LekarComboOpenTabCommand(object obj)
        {
            
            Specijalizacija = inject.PregledService.LekarComboOpenTab(zaFilLek, lekarB, lekariTrenutni, Specijalizacija);
            ItemSourceVremeB = inject.PregledService.filterLekar(zaFilLek, ItemSourceVremeB, lekariTrenutni, lekarB, datumB);
            ItemSourceLekarComboOpen = false;
            TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
            (Keyboard.FocusedElement as FrameworkElement).MoveFocus(request);

        }

        public bool CanExecute_LekarComboOpenTabCommand(object obj)
        {
            return true;
        }

        private RelayCommand lekarComboOpenCommand;
        public RelayCommand LekarComboOpenCommand
        {
            get { return lekarComboOpenCommand; }
            set
            {
                lekarComboOpenCommand = value;

            }
        }

        public void Executed_LekarComboOpenCommand(object obj)
        {
            ItemSourceLekarComboOpen = true;
        }

        public bool CanExecute_LekarComboOpenCommand(object obj)
        {
            return true;
        }

        private RelayCommand pacijentComboOpenCommand;
        public RelayCommand PacijentComboOpenCommand
        {
            get { return pacijentComboOpenCommand; }
            set
            {
                pacijentComboOpenCommand = value;

            }
        }

        public void Executed_PacijentComboOpenCommand(object obj)
        {
            ItemSourcePacijentComboOpen = true;
        }

        public bool CanExecute_PacijentComboOpenCommand(object obj)
        {
            return true;
        }

        private RelayCommand prostorijaComboOpenCommand;
        public RelayCommand ProstorijaComboOpenCommand
        {
            get { return prostorijaComboOpenCommand; }
            set
            {
                prostorijaComboOpenCommand = value;

            }
        }

        public void Executed_ProstorijaComboOpenCommand(object obj)
        {
            ItemSourceProstorijaComboOpen = true;
        }

        public bool CanExecute_ProstorijaComboOpenCommand(object obj)
        {
            return true;
        }

        private RelayCommand operacijaComboOpenCommand;
        public RelayCommand OperacijaComboOpenCommand
        {
            get { return operacijaComboOpenCommand; }
            set
            {
                operacijaComboOpenCommand = value;

            }
        }

        public void Executed_OperacijaComboOpenCommand(object obj)
        {
            ItemSourceOperacijaComboOpen = true;
        }

        public bool CanExecute_OperacijaComboOpenCommand(object obj)
        {
            return true;
        }

        private RelayCommand datumComboOpenCommand;
        public RelayCommand DatumComboOpenCommand
        {
            get { return datumComboOpenCommand; }
            set
            {
                datumComboOpenCommand = value;

            }
        }

        public void Executed_DatumComboOpenTabCommand(object obj)
        {
            ItemSourceBrojProstorije = inject.PregledService.DatumDateKey(zaFilLekDat, datumB, ItemSourceBrojProstorije, prostorijaZa);
            ItemSourceVremeB = inject.PregledService.filterLekar(zaFilLek, ItemSourceVremeB, lekariTrenutni, lekarB, datumB);
            TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
            (Keyboard.FocusedElement as FrameworkElement).MoveFocus(request);
        }

        public bool CanExecute_DatumComboOpenCommand(object obj)
        {
            return true;
        }

        private RelayCommand hitanCheckCommand;
        public RelayCommand HitanCheckCommand
        {
            get { return hitanCheckCommand; }
            set
            {
                hitanCheckCommand = value;

            }
        }

        public void Executed_HitanCheckCommand(object obj)
            {
            if (ItemSourceDaLiJeHitan) {
                ItemSourceDaLiJeHitan = false;
            }
            else
            {
                ItemSourceDaLiJeHitan = true;
            }
        }

        public bool CanExecute_HitanCheckCommand(object obj)
        {
            return true;
        }

        private RelayCommand operacijaCheckCommand;
        public RelayCommand OperacijaCheckCommand
        {
            get { return operacijaCheckCommand; }
            set
            {
                operacijaCheckCommand = value;

            }
        }

        public void Executed_OperacijaCheckCommand(object obj)
        {
            if (jeOpe)
            {
                TrajanjeB = "30";
                ItemSourceDaLiJeTrajanje = false;
                ItemSourceDaLiJeOperacije = false;
                ItemSourceBrojProstorije = new List<String>();
                List<String> zaBag = new List<String>();
                for (int pr = 0; pr < prostorijaZa.Count; pr++)
                {
                    if (prostorijaZa[pr].Obrisana == false && prostorijaZa[pr].Zauzeta == false && prostorijaZa[pr].TipProstorije.Equals(TipProstorije.salaZaPreglede))
                    {
                        zaBag.Add(prostorijaZa[pr].BrojProstorije.ToString());
                    }
                }
                ItemSourceBrojProstorije = zaBag;
                jeOpe = false;
                ItemSourceVisibility = Visibility.Hidden;
                List<TipOperacije> tipOperacije = new List<TipOperacije>();
                ItemSourceTipOperacije = tipOperacije;
                ItemSourceDaLiJeOperacije = false;
            }
            else
            {
                TrajanjeB = "";
                ItemSourceDaLiJeTrajanje = true;
                ItemSourceDaLiJeOperacije = true;
                ItemSourceBrojProstorije = new List<String>();
                List<String> zaBag = new List<String>();
                for (int pr = 0; pr < prostorijaZa.Count; pr++)
                {
                    if (prostorijaZa[pr].Obrisana == false && prostorijaZa[pr].Zauzeta == false && prostorijaZa[pr].TipProstorije.Equals(TipProstorije.operacionaSala))
                    {
                        zaBag.Add(prostorijaZa[pr].BrojProstorije);
                    }
                }
                ItemSourceBrojProstorije = zaBag;
                jeOpe = true;
                ItemSourceVisibility = Visibility.Visible;
                List<TipOperacije> tipOperacije = new List<TipOperacije>();
                tipOperacije.Add(TipOperacije.prvaKat);
                tipOperacije.Add(TipOperacije.drugaKat);
                tipOperacije.Add(TipOperacije.trecaKat);
                ItemSourceTipOperacije = tipOperacije;
            }
        }

        public bool CanExecute_OperacijaCheckCommand(object obj)
        {
            return true;
        }

        public Action CloseAction { get; set; }


        public NapraviTerminLekarViewModel(Lekar neki)
        {
            ItemSourceDaLiJeHitan = false;
            ItemSourceDaLiJeOperacije = false;
            ItemSourceDaLiJeTrajanje = false;
            ItemSourceVremeComboOpen = false;
            ItemSourceVisibility = Visibility.Hidden;
            ItemSourceSpecijalizacija = new List<string>();

            ItemSourcePrezimeB = new List<string>();

            ItemSourceLekarB = new List<string>();

            ItemSourceVremeB = new List<TimeSpan>();

            ItemSourceBrojProstorije = new List<string>();

            ItemSourceTipOperacije = new List<TipOperacije>();

      
            this.Preg = new Pregled();
            Inject = new Injector();
            specijalizacije = new List<String>();
            ulogovaniLekar = neki;
            lekariTrenutni = sviLekari.GetAll();
            
            datumB = DateTime.Now;
            pacijentiZa = sviPacijenti.GetAll();
            pregledi = sviPregledi.GetAllPregledi();
            operacije = sviPregledi.GetAllOperacije();
            prostorijaZa = sveProstorije.GetAllProstorije();


            for (int le = 0; le < lekariTrenutni.Count; le++)
            {
                if (lekariTrenutni[le].Specijalizacija.OblastMedicine != null)
                {
                    string s = lekariTrenutni[le].Prezime + ' ' + lekariTrenutni[le].Ime + ' ' + lekariTrenutni[le].Jmbg;
                    ItemSourceLekarB.Add(s);
                }
            }

            for (int le = 0; le < lekariTrenutni.Count; le++)
            {
                if (lekariTrenutni[le].Specijalizacija.OblastMedicine != null && !specijalizacije.Contains(lekariTrenutni[le].Specijalizacija.OblastMedicine))
                {
                    specijalizacije.Add(lekariTrenutni[le].Specijalizacija.OblastMedicine);
                    ItemSourceSpecijalizacija.Add(lekariTrenutni[le].Specijalizacija.OblastMedicine);
                }
            }

            for (int vre = 0; vre < 24; vre++)
            {
                for (int min = 0; min < 59;)
                {
                    TimeSpan ts = new TimeSpan(vre, min, 0);
                    min = min + 15;
                    ItemSourceVremeB.Add(ts);
                }

            }

            if (ulogovaniLekar.Specijalizacija.OblastMedicine.Equals("Opsta"))
            {
                ItemSourceDaLiJeOperacije = false;
            }

            TrajanjeB = "30";
            ItemSourceDaLiJeTrajanje = false;
            /* WindowStartupLocation = WindowStartupLocation.CenterOwner;
             Owner = Application.Current.MainWindow; */
            for (int i = 0; i < pacijentiZa.Count; i++)
            {
                if (pacijentiZa[i].Obrisan == false)
                {
                    string s;
                    s = pacijentiZa[i].Prezime + ' ' + pacijentiZa[i].Ime + ' ' + pacijentiZa[i].Jmbg;

                    ItemSourcePrezimeB.Add(s);

                }
            }
            for (int pr = 0; pr < prostorijaZa.Count; pr++)
            {
                if (prostorijaZa[pr].Obrisana == false && prostorijaZa[pr].Zauzeta == false && prostorijaZa[pr].TipProstorije.Equals(TipProstorije.salaZaPreglede))
                {
                    ItemSourceBrojProstorije.Add(prostorijaZa[pr].BrojProstorije);
                }
            }
            DodajPregledLekarCommand = new RelayCommand(Executed_DodajPregledLekarCommand, CanExecute_DodajPregledLekarCommand);
            VremeComboOpenCommand = new RelayCommand(Executed_VremeComboOpenCommand, CanExecute_VremeComboOpenCommand);
            SpecijalizacijaComboOpenCommand = new RelayCommand(Executed_SpecijalizacijaComboOpenCommand, CanExecute_SpecijalizacijaComboOpenCommand);
            SpecijalizacijaComboOpenTabCommand = new RelayCommand(Executed_SpecijalizacijaComboOpenTabCommand, CanExecute_SpecijalizacijaComboOpenTabCommand);
            LekarComboOpenCommand = new RelayCommand(Executed_LekarComboOpenCommand, CanExecute_LekarComboOpenCommand);
            LekarComboOpenTabCommand = new RelayCommand(Executed_LekarComboOpenTabCommand, CanExecute_LekarComboOpenTabCommand);
            PacijentComboOpenCommand = new RelayCommand(Executed_PacijentComboOpenCommand, CanExecute_PacijentComboOpenCommand);
            ProstorijaComboOpenCommand = new RelayCommand(Executed_ProstorijaComboOpenCommand, CanExecute_ProstorijaComboOpenCommand);
            OperacijaComboOpenCommand = new RelayCommand(Executed_OperacijaComboOpenCommand, CanExecute_OperacijaComboOpenCommand);
            OperacijaCheckCommand = new RelayCommand(Executed_OperacijaCheckCommand, CanExecute_OperacijaCheckCommand);
            DatumComboOpenCommand = new RelayCommand(Executed_DatumComboOpenTabCommand, CanExecute_DatumComboOpenCommand);
            HitanCheckCommand = new RelayCommand(Executed_HitanCheckCommand, CanExecute_HitanCheckCommand);
        }


    }
}
