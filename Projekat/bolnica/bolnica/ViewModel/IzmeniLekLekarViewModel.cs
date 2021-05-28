using Bolnica.Commands;
using Bolnica.DTO;
using Bolnica.Model.Pregledi;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bolnica.ViewModel
{
    public class IzmeniLekLekarViewModel : ViewModel
    {
        #region POLJA
        private bool itemSourceDozaComboOpen;
        public bool ItemSourceDozaComboOpen
        {
            get { return itemSourceDozaComboOpen; }
            set
            {
                itemSourceDozaComboOpen = value;
                OnPropertyChanged();
            }
        }
        private bool itemSourceNazivLekaComboOpen;
        public bool ItemSourceNazivLekaComboOpen
        {
            get { return itemSourceNazivLekaComboOpen; }
            set
            {
                itemSourceNazivLekaComboOpen = value;
                OnPropertyChanged();
            }
        }
        private bool itemSourceProizvodjacComboOpen;
        public bool ItemSourceProizvodjacComboOpen
        {
            get { return itemSourceProizvodjacComboOpen; }
            set
            {
                itemSourceProizvodjacComboOpen = value;
                OnPropertyChanged();
            }
        }
        private List<string> itemSourceNazivLeka;
        public List<string> ItemSourceNazivLeka
        {
            get { return itemSourceNazivLeka; }
            set
            {
                itemSourceNazivLeka = value;
                OnPropertyChanged();
            }
        }
        private List<int> itemSourceDozaLeka;
        public List<int> ItemSourceDozaLeka
        {
            get { return itemSourceDozaLeka; }
            set
            {
                itemSourceDozaLeka = value;
                OnPropertyChanged();
            }
        }
        private List<string> itemSourceProizvodjacLeka;
        public List<string> ItemSourceProizvodjacLeka
        {
            get { return itemSourceProizvodjacLeka; }
            set
            {
                itemSourceProizvodjacLeka = value;
                OnPropertyChanged();
            }
        }
        private Lek izabraniLek = new Lek();
        public string proizvodjac { get; set; }
        public string lek { get; set; }
        public string doza { get; set; }
        public String stariLek = "";
        public String stariProizvodjac = "";


        private FileStorageSastojak skladisteSastojaka = new FileStorageSastojak();
        private FileStorageLek skladisteLekova = new FileStorageLek();

        private List<Lek> sviLekovi = new List<Lek>();
        private List<Sastojak> sviSastojci = new List<Sastojak>();

        public Action ZatvoriAkcija { get; set; }

        private Injector inject;

        public Injector Inject
        {
            get { return inject; }
            set
            {
                inject = value;
            }
        }

        private ListBox sastojciKutija;
        private ListBox zameneKutija;
        #endregion
        #region KOMANDE
        private RelayCommand proizvodjacComboNaEnterKomanda;
        public RelayCommand ProizvodjacComboNaEnterKomanda
        {
            get { return proizvodjacComboNaEnterKomanda; }
            set
            {
                proizvodjacComboNaEnterKomanda = value;

            }
        }

        public void Executed_ProizvodjacComboNaEnterKomanda(object obj)
        {
            ItemSourceProizvodjacComboOpen = true;

        }

        public bool CanExecute_ProizvodjacComboNaEnterKomanda(object obj)
        {
            return true;
        }
        private RelayCommand proizvodjacComboNaTabKomanda;
        public RelayCommand ProizvodjacComboNaTabKomanda
        {
            get { return proizvodjacComboNaTabKomanda; }
            set
            {
                proizvodjacComboNaTabKomanda = value;

            }
        }

        public void Executed_ProizvodjacComboNaTabKomanda(object obj)
        {

            ItemSourceNazivLeka = inject.IzmeniLekLekarController.ProizvodjacComboNaTab(new IzmeniLekLekarServiceDTO(proizvodjac, sviLekovi, ItemSourceNazivLeka), ref stariProizvodjac);
            ItemSourceProizvodjacComboOpen = false;
            TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
            (Keyboard.FocusedElement as FrameworkElement).MoveFocus(request);

        }
        public bool CanExecute_ProizvodjacComboNaTabKomanda(object obj)
        {
            return true;
        }
        private RelayCommand dozaComboNaEnterKomanda;
        public RelayCommand DozaComboNaEnterKomanda
        {
            get { return dozaComboNaEnterKomanda; }
            set
            {
                dozaComboNaEnterKomanda = value;

            }
        }

        public void Executed_DozaComboNaEnterKomanda(object obj)
        {
            ItemSourceDozaComboOpen = true;

        }

        public bool CanExecute_DozaComboNaEnterKomanda(object obj)
        {
            return true;
        }
        private RelayCommand otvoriComboLekNaEnterKomanda;
        public RelayCommand OtvoriComboLekNaEnterKomanda
        {
            get { return otvoriComboLekNaEnterKomanda; }
            set
            {
                otvoriComboLekNaEnterKomanda = value;

            }
        }
        public void Executed_OtvoriComboLekNaEnterKomanda(object obj)
        {
            ItemSourceNazivLekaComboOpen = true;

        }

        public bool CanExecute_OtvoriComboLekNaEnterKomanda(object obj)
        {
            return true;
        }


        private RelayCommand comboLekNaTabKomanda;
        public RelayCommand ComboLekNaTabKomanda
        {
            get { return comboLekNaTabKomanda; }
            set
            {
                comboLekNaTabKomanda = value;

            }
        }

        public void Executed_ComboLekNaTabKomanda(object obj)
        {

            ItemSourceDozaLeka = inject.IzmeniLekLekarController.LekComboNaTab(new IzmeniLekLekarServiceDTO(lek, sviLekovi, ItemSourceDozaLeka), ref stariLek);
            ItemSourceNazivLekaComboOpen = false;
            TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
            (Keyboard.FocusedElement as FrameworkElement).MoveFocus(request);

        }
        public bool CanExecute_ComboLekNaTabKomanda(object obj)
        {
            return true;
        }

        private RelayCommand selektujSastojakNaEnterKomanda;
        public RelayCommand SelektujSastojakNaEnterKomanda
        {
            get { return selektujSastojakNaEnterKomanda; }
            set
            {
                selektujSastojakNaEnterKomanda = value;

            }
        }

        public void Executed_SelektujSastojakNaEnterKomanda(object obj)
        {
            inject.IzmeniLekLekarController.SelektujSastojakNaEnter(new IzmeniLekLekarServiceDTO(sastojciKutija));

        }

        public bool CanExecute_SelektujSastojakNaEnterKomanda(object obj)
        {
            return true;
        }

        private RelayCommand selektujZamenuNaEnterKomanda;
        public RelayCommand SelektujZamenuNaEnterKomanda
        {
            get { return selektujZamenuNaEnterKomanda; }
            set
            {
                selektujZamenuNaEnterKomanda = value;

            }
        }

        public void Executed_SelektujZamenuNaEnterKomanda(object obj)
        {
            inject.IzmeniLekLekarController.SelektujZameneNaEnter(new IzmeniLekLekarServiceDTO(zameneKutija));

        }

        public bool CanExecute_SelektujZamenuNaEnterKomanda(object obj)
        {
            return true;
        }

        private RelayCommand izmeniLekKomanda;
        public RelayCommand IzmeniLekKomanda
        {
            get { return izmeniLekKomanda; }
            set
            {
                izmeniLekKomanda = value;

            }
        }

        public void Executed_IzmeniLekKomanda(object obj)
        {

            inject.IzmeniLekLekarController.Potvrdi(new IzmeniLekLekarServiceDTO(izabraniLek, doza, lek, proizvodjac, sastojciKutija, sviSastojci, zameneKutija, sviLekovi));
            ZatvoriAkcija();


        }

        public bool CanExecute_IzmeniLekKomanda(object obj)
        {
            return true;
        }

        private RelayCommand zatvoriKomanda;
        public RelayCommand ZatvoriKomanda
        {
            get { return zatvoriKomanda; }
            set
            {
                zatvoriKomanda = value;

            }
        }

        public void Executed_ZatvoriKomanda(object obj)
        {

            ZatvoriAkcija();


        }

        public bool CanExecute_ZatvoriKomanda(object obj)
        {
            return true;
        }

        #endregion
       

        public IzmeniLekLekarViewModel(Lek izabraniLekIzTabele)
        {
            InicijalizujPodatke(izabraniLekIzTabele);
            FiltrirajLekove();
            PopuniNaziveIProizvodjace();
            PopuniDoze();
            PostaviComboBoxove();
            NapraviKomande();
        }
        public void FiltrirajLekove()
        {
            sviLekovi = skladisteLekova.GetAll();
            for (int i = 0; i < sviLekovi.Count; i++)
            {
                if (sviLekovi[i].Status.Equals(StatusLeka.odbijen) || sviLekovi[i].Obrisan)
                {
                    sviLekovi.RemoveAt(i);
                    i--;
                }
            }
        }
        public void InicijalizujPodatke(Lek p)
        {
            ItemSourceProizvodjacLeka = new List<string>();

            ItemSourceDozaLeka = new List<int>();
            ItemSourceNazivLeka = new List<string>();
            Inject = new Injector();
            izabraniLek = p;
            sviSastojci = skladisteSastojaka.GetAll();
        }
        public void PostaviComboBoxove()
        {
            proizvodjac = izabraniLek.Proizvodjac;
            lek = izabraniLek.Naziv;
            doza = izabraniLek.KolicinaUMg.ToString();
            stariLek = lek;
            stariProizvodjac = proizvodjac;
        }
        public void PopuniNaziveIProizvodjace()
        {
            List<string> ItemSourceLekovi = new List<string>();
            List<string> proizvodjaci = new List<string>();
            for (int i = 0; i < sviLekovi.Count; i++)
            {
                if (!ItemSourceLekovi.Contains(sviLekovi[i].Naziv) && sviLekovi[i].Proizvodjac.Equals(izabraniLek.Proizvodjac))
                {
                    ItemSourceLekovi.Add(sviLekovi[i].Naziv);
                }
                if (!proizvodjaci.Contains(sviLekovi[i].Proizvodjac))
                {
                    proizvodjaci.Add(sviLekovi[i].Proizvodjac);
                }
            }
            ItemSourceNazivLeka = ItemSourceLekovi;
            ItemSourceProizvodjacLeka = proizvodjaci;
        }

        public void PopuniDoze()
        {
            List<int> ItemSourceDoza = new List<int>();
            for (int i = 0; i < sviLekovi.Count; i++)
            {
                if (sviLekovi[i].Naziv.Equals(izabraniLek.Naziv) && !ItemSourceDoza.Contains(sviLekovi[i].KolicinaUMg))
                {
                    ItemSourceDoza.Add(sviLekovi[i].KolicinaUMg);
                }
            }
            itemSourceDozaLeka = ItemSourceDoza;
        }
        public void NapraviKomande()
        {
            OtvoriComboLekNaEnterKomanda = new RelayCommand(Executed_OtvoriComboLekNaEnterKomanda, CanExecute_OtvoriComboLekNaEnterKomanda);
            ComboLekNaTabKomanda = new RelayCommand(Executed_ComboLekNaTabKomanda, CanExecute_ComboLekNaTabKomanda);
            ProizvodjacComboNaEnterKomanda = new RelayCommand(Executed_ProizvodjacComboNaEnterKomanda, CanExecute_ProizvodjacComboNaEnterKomanda);
            ProizvodjacComboNaTabKomanda = new RelayCommand(Executed_ProizvodjacComboNaTabKomanda, CanExecute_ProizvodjacComboNaTabKomanda);
            DozaComboNaEnterKomanda = new RelayCommand(Executed_DozaComboNaEnterKomanda, CanExecute_DozaComboNaEnterKomanda);
            SelektujSastojakNaEnterKomanda = new RelayCommand(Executed_SelektujSastojakNaEnterKomanda, CanExecute_SelektujSastojakNaEnterKomanda);
            SelektujZamenuNaEnterKomanda = new RelayCommand(Executed_SelektujZamenuNaEnterKomanda, CanExecute_SelektujZamenuNaEnterKomanda);
            IzmeniLekKomanda = new RelayCommand(Executed_IzmeniLekKomanda, CanExecute_IzmeniLekKomanda);
            ZatvoriKomanda = new RelayCommand(Executed_ZatvoriKomanda, CanExecute_ZatvoriKomanda);
        }

        public void PopuniSastojkeIZamene(ListBox sastojakKutija,ListBox zamenaKutija)
        {
            sastojciKutija = sastojakKutija;
            zameneKutija = zamenaKutija;
            PopuniSastojke();
            PopuniZamene();
            IzfiltrirajZamene();
            

           

        }

        public void PopuniSastojke()
        {
            for (int i = 0; i < sviSastojci.Count; i++)
            {
                int dozvola = 0;
                for (int m = 0; m < izabraniLek.Sastojak.Count; m++)
                {
                    if (izabraniLek.Sastojak[m].Id.Equals(sviSastojci[i].Id))
                    {
                        dozvola = 1;
                    }
                }
                sastojciKutija.Items.Add(sviSastojci[i].Naziv);
                if (dozvola == 1)
                {
                    sastojciKutija.SelectedItems.Add(sviSastojci[i].Naziv);
                }



            }
        }

        public void PopuniZamene()
        {
            for (int i = 0; i < sviLekovi.Count; i++)
            {
                int dozvola = 0;
                for (int m = 0; m < izabraniLek.IdZamena.Count; m++)
                {
                    Lek novi = new Lek();
                    for (int mo = 0; mo < sviLekovi.Count; mo++)
                    {
                        if (izabraniLek.IdZamena[m].Equals(sviLekovi[mo].Id))
                        {
                            novi = sviLekovi[mo];
                        }
                    }
                    if (novi.Naziv.Equals(sviLekovi[i].Naziv) && novi.Proizvodjac.Equals(sviLekovi[i].Proizvodjac) && novi.KolicinaUMg.Equals(sviLekovi[i].KolicinaUMg))
                    {
                        dozvola = 1;
                    }
                }
                string k = sviLekovi[i].Proizvodjac + ", " + sviLekovi[i].Naziv + ", " + sviLekovi[i].KolicinaUMg;
                zameneKutija.Items.Add(k);
                if (dozvola == 1)
                {
                    zameneKutija.SelectedItems.Add(k);
                }
            }
        }

        public void IzfiltrirajZamene()
        {
            for (int m = 0; m < zameneKutija.Items.Count; m++)
            {
                for (int a = 0; a < sviLekovi.Count; a++)
                {
                    if (zameneKutija.Items[m].ToString().Equals(sviLekovi[a].Proizvodjac + ", " + sviLekovi[a].Naziv + ", " + sviLekovi[a].KolicinaUMg))
                    {
                        if (sviLekovi[a].Status.Equals(StatusLeka.cekaValidaciju))
                        {
                            zameneKutija.Items.RemoveAt(m);
                            m--;
                            break;
                        }
                    }
                }
            }
        }



       
    }
}
