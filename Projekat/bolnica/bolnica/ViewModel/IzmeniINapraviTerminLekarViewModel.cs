using Bolnica.Commands;
using Bolnica.DTO;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
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
    public class IzmeniINapraviTerminLekarViewModel : ViewModel
    {
        private List<Lekar> sviLekari = new List<Lekar>();
        private Lekar ulogovaniLekar = new Lekar();
        private bool daLiJeOperacija = false;
        private FileStoragePacijenti skladistePacijenata = new FileStoragePacijenti();
        private FileStorageProstorija skladisteProstorija = new FileStorageProstorija();
        private FileStoragePregledi skladistePregleda = new FileStoragePregledi();
        private FileStorageLekar skladisteLekara = new FileStorageLekar();
        private List<Pacijent> sviPacijenti = new List<Pacijent>();
        private List<Prostorija> sveProstorije = new List<Prostorija>();
        private string nazivIzfiltriranogLeka = "";
        private DateTime datumZaFiltriranjeLeka = new DateTime();
        private PrikazPregleda trenutniPregled = new PrikazPregleda();  
        private PrikazOperacije trenutnaOperacija = new PrikazOperacije();
        private PrikazPregleda stariPregled = new PrikazPregleda();
        private PrikazOperacije staraOperacija = new PrikazOperacije();
        private string proslaSpecijalizacija = "";
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

        private bool checkBoxOperacijaEnabled;
        public bool CheckBoxOperacijaEnabled
        {
            get { return checkBoxOperacijaEnabled; }
            set
            {
                checkBoxOperacijaEnabled = value;
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

        private bool pacijentEnabled;
        public bool PacijentEnabled
        {
            get { return pacijentEnabled; }
            set
            {
                pacijentEnabled = value;
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
        public string pacijentPodaci { get; set; }

        public string lekarPodaci { get; set; }

        public DateTime datumPregleda { get; set; }

        public string vremePregleda { get; set; }

        public string brojProstorije { get; set; }

        public TipOperacije tipOperacije { get; set; }

        public string trajanjePregleda;

        public string TrajanjePregleda
        {
            get { return trajanjePregleda; }
            set
            {
                trajanjePregleda = value;
                OnPropertyChanged();
            }
        }

        private Injector inject;

        public Injector Inject
        {
            get { return inject; }
            set
            {
                inject = value;
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
            if (!inject.PregledService.PostojiLekar(new IzmeniINapraviTerminLekarServiceDTO(Specijalizacija, lekarPodaci)))
            {
                MessageBox.Show("Ne postoji lekar");
                return;
            }
            if (!inject.PregledService.LekarSlobodanUToVreme(new IzmeniINapraviTerminLekarServiceDTO(lekarPodaci, datumPregleda, TrajanjePregleda, vremePregleda)))
            {
                MessageBox.Show("Lekar nije slobodan u to vreme");
                return;
            }
            if (!inject.PregledService.PacijentSlobodanUToVreme(new IzmeniINapraviTerminLekarServiceDTO(sviPacijenti, pacijentPodaci, datumPregleda, TrajanjePregleda, vremePregleda)))
            {
                MessageBox.Show("Pacijent nije slobodan u to vreme");
                return;
            }
            if (!inject.PregledService.PostojiProstorija(new IzmeniINapraviTerminLekarServiceDTO(sveProstorije, brojProstorije)))
            {
                MessageBox.Show("Prostorija ne postoji");
                return;
            }
            if (!inject.PregledService.ProstorijaSlobodna(new IzmeniINapraviTerminLekarServiceDTO(sveProstorije, brojProstorije, datumPregleda)))
            {
                MessageBox.Show("Prostorija nije slobodna");
                return;
            }
            inject.PregledService.Potvrdi(new IzmeniINapraviTerminLekarServiceDTO(datumPregleda.ToShortDateString(), vremePregleda, TrajanjePregleda, sviLekari, lekarPodaci, sviPacijenti, pacijentPodaci, sveProstorije, ItemSourceDaLiJeOperacije, tipOperacije, ItemSourceDaLiJeHitan, brojProstorije, ulogovaniLekar));
            CloseAction();
            
        }

        public bool CanExecute_DodajPregledLekarCommand(object obj)
        {
            return true;
        }

        private RelayCommand izmeniPregledLekarCommand;

        public RelayCommand IzmeniPregledLekarCommand
        {
            get { return izmeniPregledLekarCommand; }
            set
            {
                izmeniPregledLekarCommand = value;
            }
        }

        public void Executed_IzmeniPregledLekarCommand(object obj)
        {
            if (!inject.PregledService.PostojiLekar(new IzmeniINapraviTerminLekarServiceDTO(Specijalizacija, lekarPodaci)))
            {
                MessageBox.Show("Ne postoji lekar");
                return;
            }
            if (!inject.PregledService.LekarSlobodanUToVremeIzmeni(new IzmeniINapraviTerminLekarServiceDTO(lekarPodaci, datumPregleda, TrajanjePregleda, vremePregleda,trenutniPregled,trenutnaOperacija)))
            {
                MessageBox.Show("Lekar nije slobodan u to vreme");
                return;
            }
            if (!inject.PregledService.PacijentSlobodanUToVremeIzmeni(new IzmeniINapraviTerminLekarServiceDTO(pacijentPodaci, datumPregleda, TrajanjePregleda, vremePregleda,trenutniPregled,trenutnaOperacija)))
            {
                MessageBox.Show("Pacijent nije slobodan u to vreme");
                return;
            }
            if (!inject.PregledService.PostojiProstorija(new IzmeniINapraviTerminLekarServiceDTO(sveProstorije, brojProstorije)))
            {
                MessageBox.Show("Prostorija ne postoji");
                return;
            }
            if (!inject.PregledService.ProstorijaSlobodna(new IzmeniINapraviTerminLekarServiceDTO(sveProstorije, brojProstorije, datumPregleda)))
            {
                MessageBox.Show("Prostorija nije slobodna");
                return;
            }
            inject.PregledService.PotvrdiIzmenu(new IzmeniINapraviTerminLekarServiceDTO(datumPregleda.ToShortDateString(), vremePregleda, TrajanjePregleda, lekarPodaci, pacijentPodaci, ItemSourceDaLiJeOperacije, tipOperacije, ItemSourceDaLiJeHitan, brojProstorije, ulogovaniLekar, staraOperacija, stariPregled));
            CloseAction();

        }

        public bool CanExecute_IzmeniPregledLekarCommand(object obj)
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
            
            ItemSourceLekarB = inject.PregledService.SpecijalizacijaComboNaTab(new IzmeniINapraviTerminLekarServiceDTO(specijalizacije, Specijalizacija, sviLekari));
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
            
            Specijalizacija = inject.PregledService.LekarComboNaTab(new IzmeniINapraviTerminLekarServiceDTO(nazivIzfiltriranogLeka, lekarPodaci, sviLekari, Specijalizacija));
            ItemSourceVremeB = inject.PregledService.LekarFiltriranje(new IzmeniINapraviTerminLekarServiceDTO(ItemSourceVremeB, nazivIzfiltriranogLeka, sviLekari, lekarPodaci, datumPregleda));
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
            ItemSourceBrojProstorije = inject.PregledService.DatumFiltriranje(new IzmeniINapraviTerminLekarServiceDTO(datumZaFiltriranjeLeka, datumPregleda, sveProstorije, ItemSourceBrojProstorije));
            ItemSourceVremeB = inject.PregledService.LekarFiltriranje(new IzmeniINapraviTerminLekarServiceDTO(ItemSourceVremeB, nazivIzfiltriranogLeka, sviLekari, lekarPodaci, datumPregleda));
            TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
            (Keyboard.FocusedElement as FrameworkElement).MoveFocus(request);
        }

        public bool CanExecute_DatumComboOpenCommand(object obj)
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
            if (daLiJeOperacija)
            {
                TrajanjePregleda = "30";
                ItemSourceDaLiJeTrajanje = false;
                ItemSourceDaLiJeOperacije = false;
                ItemSourceBrojProstorije = new List<String>();
                List<String> brojeviProstorija = new List<String>();
                for (int pr = 0; pr < sveProstorije.Count; pr++)
                {
                    if (sveProstorije[pr].Obrisana == false && sveProstorije[pr].Zauzeta == false && sveProstorije[pr].TipProstorije.Equals(TipProstorije.salaZaPreglede))
                    {
                        brojeviProstorija.Add(sveProstorije[pr].BrojProstorije.ToString());
                    }
                }
                ItemSourceBrojProstorije = brojeviProstorija;
                daLiJeOperacija = false;
                ItemSourceVisibility = Visibility.Hidden;
                List<TipOperacije> tipOperacije = new List<TipOperacije>();
                tipOperacije.Add(TipOperacije.prvaKat);
                tipOperacije.Add(TipOperacije.drugaKat);
                tipOperacije.Add(TipOperacije.trecaKat);
                ItemSourceTipOperacije = tipOperacije;
                ItemSourceDaLiJeOperacije = false;
            }
            else
            {
                TrajanjePregleda = "";
                ItemSourceDaLiJeTrajanje = true;
                ItemSourceDaLiJeOperacije = true;
                ItemSourceBrojProstorije = new List<String>();
                List<String> brojeviProstorija = new List<String>();
                for (int pr = 0; pr < sveProstorije.Count; pr++)
                {
                    if (sveProstorije[pr].Obrisana == false && sveProstorije[pr].Zauzeta == false && sveProstorije[pr].TipProstorije.Equals(TipProstorije.operacionaSala))
                    {
                        brojeviProstorija.Add(sveProstorije[pr].BrojProstorije);
                    }
                }
                ItemSourceBrojProstorije = brojeviProstorija;
                daLiJeOperacija = true;
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


        public IzmeniINapraviTerminLekarViewModel(Lekar izabraniLekar)
        {
            InicirajZajednickaPoljaKonstruktora(izabraniLekar);
            checkBoxOperacijaEnabled = true;
            pacijentEnabled = true;
            PopuniIPodesiPodatkePregleda(izabraniLekar);
            NapraviKomande();
        }


        public IzmeniINapraviTerminLekarViewModel(Lekar izabraniLekar, Pacijent pacijent)
        {
            InicirajZajednickaPoljaKonstruktora(izabraniLekar);
            checkBoxOperacijaEnabled = true;
            pacijentEnabled = true;
            PopuniIPodesiPodatkePregleda(pacijent);
            NapraviKomande();
        }

        public IzmeniINapraviTerminLekarViewModel(PrikazPregleda pregled, Lekar izabraniLekar)
        {
            InicirajPoljaIzmenePregleda(pregled, izabraniLekar);
            PopuniIPodesiPodatkeIzmenePregleda();
            NapraviKomande();
        }

        public IzmeniINapraviTerminLekarViewModel(PrikazOperacije operacija, Lekar izabraniLekar)
        {
            InicijalizujPoljaIzmeneOperacije(operacija,izabraniLekar);
            PopuniIPodesiPodatkeIzmeneOperacije();
            NapraviKomande();
        }
        public void NapraviKomande()
        {
            DodajPregledLekarCommand = new RelayCommand(Executed_DodajPregledLekarCommand, CanExecute_DodajPregledLekarCommand);
            IzmeniPregledLekarCommand = new RelayCommand(Executed_IzmeniPregledLekarCommand, CanExecute_IzmeniPregledLekarCommand);
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
            ZatvoriCommand = new RelayCommand(Executed_ZatvoriCommand, CanExecute_ZatvoriCommand);
        }
        public void InicirajZajednickaPoljaKonstruktora(Lekar izabraniLekar)
        {
            ItemSourceSpecijalizacija = new List<string>();
            ItemSourcePrezimeB = new List<string>();
            ItemSourceLekarB = new List<string>();
            ItemSourceVremeB = new List<TimeSpan>();
            ItemSourceBrojProstorije = new List<string>();
            ItemSourceTipOperacije = new List<TipOperacije>();
            Inject = new Injector();
            sviLekari = skladisteLekara.GetAll();
            sviPacijenti = skladistePacijenata.GetAll();
            pregledi = skladistePregleda.GetAllPregledi();
            operacije = skladistePregleda.GetAllOperacije();
            sveProstorije = skladisteProstorija.GetAllProstorije();
            specijalizacije = new List<String>();
            ulogovaniLekar = izabraniLekar;
            checkBoxOperacijaEnabled = true;
        }
        public void InicirajPoljaIzmenePregleda(PrikazPregleda pregled, Lekar izabraniLekar)
        {
            InicirajZajednickaPoljaKonstruktora(izabraniLekar);
            checkBoxOperacijaEnabled = false;
            pacijentEnabled = true;
            trenutniPregled = pregled;
            stariPregled = pregled;

        }
        public void InicijalizujPoljaIzmeneOperacije(PrikazOperacije operacija, Lekar izabraniLekar)
        {
            InicirajZajednickaPoljaKonstruktora(izabraniLekar);
            checkBoxOperacijaEnabled = false;
            pacijentEnabled = true;
            trenutnaOperacija = operacija;
            staraOperacija = operacija;
        }
        public void PopuniIPodesiPodatkeIzmenePregleda()
        {
            PopuniComboBoxove();
            RukujPoljimaPregleda();
            PostaviComboBoxoveNaPotrebneVrednostiIzmenePregleda();
        }

        public void PopuniIPodesiPodatkePregleda(Pacijent pacijent)
        {
            PopuniComboBoxove(pacijent);
            RukujPoljimaPregleda();
            datumPregleda = DateTime.Now;
        }

        public void PopuniIPodesiPodatkePregleda(Lekar izabraniLekar)
        {
            PopuniComboBoxove();
            RukujPoljimaPregleda();
            datumPregleda = DateTime.Now;
        }
        public void PopuniIPodesiPodatkeIzmeneOperacije()
        {
            PopuniComboBoxove();
            RukujPoljimaIzmeneOperacije();
            PostaviComboBoxoveNaPotrebneVrednostiIzmeneOperacije();
            
        }
        public void PostaviComboBoxoveNaPotrebneVrednostiIzmenePregleda()
        {
            Specijalizacija = trenutniPregled.Lekar.Specijalizacija.OblastMedicine;
            pacijentPodaci = trenutniPregled.Pacijent.Prezime + ' ' + trenutniPregled.Pacijent.Ime + ' ' + trenutniPregled.Pacijent.Jmbg;
            lekarPodaci = trenutniPregled.Lekar.Prezime + ' ' + trenutniPregled.Lekar.Ime + ' ' + trenutniPregled.Lekar.Jmbg;
            string[] datum = trenutniPregled.Datum.ToString().Split(" ");
            string[] dioDatuma = datum[0].Split(".");
            string vreme = datum[1];
            DateTime izabraniDatum = new DateTime(Int32.Parse(dioDatuma[2]), Int32.Parse(dioDatuma[1]), Int32.Parse(dioDatuma[0]));
            datumPregleda = izabraniDatum;
            vremePregleda = TimeSpan.Parse(vreme).ToString();
            brojProstorije = trenutniPregled.Prostorija.BrojProstorije.ToString();
            TrajanjePregleda = trenutniPregled.Trajanje.ToString();
            ItemSourceDaLiJeHitan = trenutniPregled.Hitan;
        }
        public void PostaviComboBoxoveNaPotrebneVrednostiIzmeneOperacije()
        {
            Specijalizacija = trenutnaOperacija.Lekar.Specijalizacija.OblastMedicine;
            pacijentPodaci = trenutnaOperacija.Pacijent.Prezime + ' ' + trenutnaOperacija.Pacijent.Ime + ' ' + trenutnaOperacija.Pacijent.Jmbg;
            lekarPodaci = trenutnaOperacija.Lekar.Prezime + ' ' + trenutnaOperacija.Lekar.Ime + ' ' + trenutnaOperacija.Lekar.Jmbg;
            string[] datum = trenutnaOperacija.Datum.ToString().Split(" ");
            string[] dioDatuma = datum[0].Split(".");
            string vreme = datum[1];
            DateTime izabraniDatum = new DateTime(Int32.Parse(dioDatuma[2]), Int32.Parse(dioDatuma[1]), Int32.Parse(dioDatuma[0]));
            datumPregleda = izabraniDatum;
            vremePregleda = TimeSpan.Parse(vreme).ToString();
            brojProstorije = trenutnaOperacija.Prostorija.BrojProstorije.ToString();
            TrajanjePregleda = trenutnaOperacija.Trajanje.ToString();
            ItemSourceDaLiJeHitan = trenutnaOperacija.Hitan;
            tipOperacije = trenutnaOperacija.TipOperacije;
        }
        public void RukujPoljimaIzmeneOperacije()
        {
            RukovanjeTextBoxTrajanjeIzmeneOperacije();
            OnemoguciOperacijuAkoJeOpsta();
            RukovanjePoljimaZaOperaciju();
        }
        public void RukujPoljimaPregleda()
        {
            RukovanjeTextBoxTrajanjePregleda();
            OnemoguciOperacijuAkoJeOpsta();
            RukovanjePoljimaZaPregled();
        }

        public void RukovanjePoljimaZaPregled()
        {
            ItemSourceDaLiJeOperacije = false;
            itemSourceVisibility = Visibility.Hidden;
        }
        public void PopuniComboBoxTipOperacije()
        {
            List<TipOperacije> tipOperacije = new List<TipOperacije>();
            tipOperacije.Add(TipOperacije.prvaKat);
            tipOperacije.Add(TipOperacije.drugaKat);
            tipOperacije.Add(TipOperacije.trecaKat);
            ItemSourceTipOperacije = tipOperacije;
        }
        public void RukovanjePoljimaZaOperaciju()
        {
            ItemSourceDaLiJeOperacije = true;
            itemSourceVisibility = Visibility.Visible;
        }
        public void PopuniComboBoxove()
        {
            PopuniComboBoxLekar();
            PopuniComboBoxSpecijalizacija();
            PopuniComboBoxVreme();
            PopuniComboBoxPacijent();
            PopuniComboBoxProstorija();
            PopuniComboBoxTipOperacije();
        }

        public void PopuniComboBoxove(Pacijent pacijent)
        {
            PopuniComboBoxLekar();
            PopuniComboBoxSpecijalizacija();
            PopuniComboBoxVreme();
            PopuniComboBoxPacijent(pacijent);
            PopuniComboBoxProstorija();
            PopuniComboBoxTipOperacije();
        }
        public void PopuniComboBoxLekar()
        {
            for (int i = 0; i < sviLekari.Count; i++)
            {
                if (sviLekari[i].Specijalizacija.OblastMedicine != null)
                {
                    string podaciOLekaru = sviLekari[i].Prezime + ' ' + sviLekari[i].Ime + ' ' + sviLekari[i].Jmbg;
                    ItemSourceLekarB.Add(podaciOLekaru);
                }
            }
        }
        public void PopuniComboBoxSpecijalizacija()
        {
            for (int i = 0; i < sviLekari.Count; i++)
            {
                if (sviLekari[i].Specijalizacija.OblastMedicine != null && !specijalizacije.Contains(sviLekari[i].Specijalizacija.OblastMedicine))
                {
                    specijalizacije.Add(sviLekari[i].Specijalizacija.OblastMedicine);
                    ItemSourceSpecijalizacija.Add(sviLekari[i].Specijalizacija.OblastMedicine);
                }
            }
        }
        public void PopuniComboBoxVreme()
        {
            for (int sati = 0; sati < 24; sati++)
            {
                for (int min = 0; min < 59;)
                {
                    TimeSpan ts = new TimeSpan(sati, min, 0);
                    min = min + 15;
                    ItemSourceVremeB.Add(ts);
                }

            }
        }
        public void OnemoguciOperacijuAkoJeOpsta()
        {
            if (ulogovaniLekar.Specijalizacija.OblastMedicine.Equals("Opsta"))
            {
                CheckBoxOperacijaEnabled = false;
            }
        }
        public void RukovanjeTextBoxTrajanjePregleda()
        {
            TrajanjePregleda = "30";
            ItemSourceDaLiJeTrajanje = false;
        }
        public void RukovanjeTextBoxTrajanjeIzmeneOperacije()
        {
            TrajanjePregleda = "30";
            ItemSourceDaLiJeTrajanje = true;
        }
        public void PopuniComboBoxPacijent()
        {
            for (int i = 0; i < sviPacijenti.Count; i++)
            {
                if (sviPacijenti[i].Obrisan == false)
                {
                    string podaciOPacijentu;
                    podaciOPacijentu = sviPacijenti[i].Prezime + ' ' + sviPacijenti[i].Ime + ' ' + sviPacijenti[i].Jmbg;

                    ItemSourcePrezimeB.Add(podaciOPacijentu);

                }
            }
        }
        public void PopuniComboBoxPacijent(Pacijent pacijent)
        {
            for (int i = 0; i < sviPacijenti.Count; i++)
            {
                if (sviPacijenti[i].Obrisan == false)
                {
                    string podaciOPacijentu;
                    string podaciOIzabranomPacijentu;
                    podaciOPacijentu = sviPacijenti[i].Prezime + ' ' + sviPacijenti[i].Ime + ' ' + sviPacijenti[i].Jmbg;
                    podaciOIzabranomPacijentu = pacijent.Prezime + ' ' + pacijent.Ime + ' ' + pacijent.Jmbg;
                    ItemSourcePrezimeB.Add(podaciOPacijentu);
                    if (podaciOPacijentu.Equals(podaciOIzabranomPacijentu))
                    {
                        pacijentPodaci = podaciOIzabranomPacijentu;
                        pacijentEnabled = false;
                    }


                }
            }
        }
        public void PopuniComboBoxProstorija()
        {
            for (int i = 0; i < sveProstorije.Count; i++)
            {
                if (sveProstorije[i].Obrisana == false && sveProstorije[i].Zauzeta == false && sveProstorije[i].TipProstorije.Equals(TipProstorije.salaZaPreglede))
                {
                    ItemSourceBrojProstorije.Add(sveProstorije[i].BrojProstorije);
                }
            }
        }

    }
}
