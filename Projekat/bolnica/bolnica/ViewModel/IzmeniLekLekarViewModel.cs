using Bolnica.Commands;
using Bolnica.Model.Pregledi;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bolnica.ViewModel
{
    public class IzmeniLekLekarViewModel : ViewModel
    {
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

        private RelayCommand proizvodjacComboOpenCommand;
        public RelayCommand ProizvodjacComboOpenCommand
        {
            get { return proizvodjacComboOpenCommand; }
            set
            {
                proizvodjacComboOpenCommand = value;

            }
        }

        public void Executed_ProizvodjacComboOpenCommand(object obj)
        {
            ItemSourceProizvodjacComboOpen = true;

        }

        public bool CanExecute_ProizvodjacComboOpenCommand(object obj)
        {
            return true;
        }
        private RelayCommand proizvodjacComboOpenTabCommand;
        public RelayCommand ProizvodjacComboOpenTabCommand
        {
            get { return proizvodjacComboOpenTabCommand; }
            set
            {
                proizvodjacComboOpenTabCommand = value;

            }
        }

        public void Executed_ProizvodjacComboOpenTabCommand(object obj)
        {

            ItemSourceNazivLeka = inject.IzmeniLekLekarService.ProizvodjacOpenTab(proizvodjac,ref stariProizvodjac,lekovi,ItemSourceNazivLeka);
            ItemSourceProizvodjacComboOpen = false;
            TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
            (Keyboard.FocusedElement as FrameworkElement).MoveFocus(request);

        }
        public bool CanExecute_ProizvodjacComboOpenTabCommand(object obj)
        {
            return true;
        }
        private RelayCommand dozaComboOpenCommand;
        public RelayCommand DozaComboOpenCommand
        {
            get { return dozaComboOpenCommand; }
            set
            {
                dozaComboOpenCommand = value;

            }
        }

        public void Executed_DozaComboOpenCommand(object obj)
        {
            ItemSourceDozaComboOpen = true;

        }

        public bool CanExecute_DozaComboOpenCommand(object obj)
        {
            return true;
        }
        private RelayCommand lekComboOpenCommand;
        public RelayCommand LekComboOpenCommand
        {
            get { return lekComboOpenCommand; }
            set
            {
                lekComboOpenCommand = value;

            }
        }
        public void Executed_LekComboOpenCommand(object obj)
        {
            ItemSourceNazivLekaComboOpen = true;

        }

        public bool CanExecute_LekComboOpenCommand(object obj)
        {
            return true;
        }


        private RelayCommand lekComboOpenTabCommand;
        public RelayCommand LekComboOpenTabCommand
        {
            get { return lekComboOpenTabCommand; }
            set
            {
                lekComboOpenTabCommand = value;

            }
        }

        public void Executed_LekComboOpenTabCommand(object obj)
        {

            ItemSourceDozaLeka = inject.IzmeniLekLekarService.NazivLekaOpenTab(lek,ref stariLek, lekovi,ItemSourceDozaLeka);
            ItemSourceNazivLekaComboOpen = false;
            TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
            (Keyboard.FocusedElement as FrameworkElement).MoveFocus(request);

        }
        public bool CanExecute_LekComboOpenTabCommand(object obj)
        {
            return true;
        }

        private RelayCommand selectSastojakCommand;
        public RelayCommand SelectSastojakCommand
        {
            get { return selectSastojakCommand; }
            set
            {
                selectSastojakCommand = value;

            }
        }

        public void Executed_SelectSastojakCommand(object obj)
        {
            inject.IzmeniLekLekarService.SelectSastojakNaEnter(sastojciBox);

        }

        public bool CanExecute_SelectSastojakCommand(object obj)
        {
            return true;
        }

        private RelayCommand selectZamenaCommand;
        public RelayCommand SelectZamenaCommand
        {
            get { return selectZamenaCommand; }
            set
            {
                selectZamenaCommand = value;

            }
        }

        public void Executed_SelectZamenaCommand(object obj)
        {
            inject.IzmeniLekLekarService.SelectZamenaNaEnter(zameneBox);

        }

        public bool CanExecute_SelectZamenaCommand(object obj)
        {
            return true;
        }

        private RelayCommand izmeniLekCommand;
        public RelayCommand IzmeniLekCommand
        {
            get { return izmeniLekCommand; }
            set
            {
                izmeniLekCommand = value;

            }
        }

        public void Executed_IzmeniLekCommand(object obj)
        {
           
                inject.IzmeniLekLekarService.Potvrdi(l,doza,lek,proizvodjac,sastojciBox,sas,zameneBox,lekovi);
                CloseAction();
            

        }

        public bool CanExecute_IzmeniLekCommand(object obj)
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


        private Lek l = new Lek();

        public string proizvodjac { get; set; }
        public string lek { get; set; }

        public string doza { get; set; }

        public String stariLek = "";

        public String stariProizvodjac = "";
        

        private FileStorageSastojak sviSastojci = new FileStorageSastojak();
        private FileStorageLek sviLekovi = new FileStorageLek();

        private List<Lek> lekovi = new List<Lek>();
        private List<Sastojak> sas = new List<Sastojak>();
        
        public ListBox itemSastojci { get; set; }
        public ListBox itemZamene { get; set; }

        public Action CloseAction { get; set; }

        private Injector inject;

        public Injector Inject
        {
            get { return inject; }
            set
            {
                inject = value;
            }
        }

        private ListBox sastojciBox;
        private ListBox zameneBox;

        public IzmeniLekLekarViewModel(Lek p)
        {
            ItemSourceProizvodjacLeka = new List<string>();
            
            ItemSourceDozaLeka = new List<int>();
            ItemSourceNazivLeka = new List<string>();
            Inject = new Injector();
            lekovi = sviLekovi.GetAll();
            for (int i = 0; i < lekovi.Count; i++)
            {
                if (lekovi[i].Status.Equals(StatusLeka.odbijen) || lekovi[i].Obrisan)
                {
                    lekovi.RemoveAt(i);
                    i--;
                }
            }
            l = p;
           
        
            List<string> ItemSourceLekovi = new List<string>();
            List<string> proizvodjaci = new List<string>();
            for (int i = 0; i < lekovi.Count; i++)
            {
                if (!ItemSourceLekovi.Contains(lekovi[i].Naziv))
                {
                    ItemSourceLekovi.Add(lekovi[i].Naziv);
                }
                if (!proizvodjaci.Contains(lekovi[i].Proizvodjac))
                {
                    proizvodjaci.Add(lekovi[i].Proizvodjac);   
                }
            }
            ItemSourceNazivLeka = ItemSourceLekovi;
            ItemSourceProizvodjacLeka = proizvodjaci;
            List<int> ItemSourceDoza = new List<int>();
            for (int i = 0; i < lekovi.Count; i++)
            {
                if (lekovi[i].Naziv.Equals(l.Naziv) && !ItemSourceDoza.Contains(lekovi[i].KolicinaUMg))
                {
                    ItemSourceDoza.Add(lekovi[i].KolicinaUMg);
                }
            }
            itemSourceDozaLeka = ItemSourceDoza;
            proizvodjac = l.Proizvodjac;
            lek = l.Naziv;
            doza = l.KolicinaUMg.ToString();
            stariLek = lek;
            stariProizvodjac = proizvodjac;

            sas = sviSastojci.GetAll();


            LekComboOpenCommand = new RelayCommand(Executed_LekComboOpenCommand, CanExecute_LekComboOpenCommand);
            LekComboOpenTabCommand = new RelayCommand(Executed_LekComboOpenTabCommand, CanExecute_LekComboOpenTabCommand);
            ProizvodjacComboOpenCommand = new RelayCommand(Executed_ProizvodjacComboOpenCommand, CanExecute_ProizvodjacComboOpenCommand);
            ProizvodjacComboOpenTabCommand = new RelayCommand(Executed_ProizvodjacComboOpenTabCommand, CanExecute_ProizvodjacComboOpenTabCommand);
            DozaComboOpenCommand = new RelayCommand(Executed_DozaComboOpenCommand, CanExecute_DozaComboOpenCommand);
            SelectSastojakCommand = new RelayCommand(Executed_SelectSastojakCommand, CanExecute_SelectSastojakCommand);
            SelectZamenaCommand = new RelayCommand(Executed_SelectZamenaCommand, CanExecute_SelectZamenaCommand);
            IzmeniLekCommand = new RelayCommand(Executed_IzmeniLekCommand, CanExecute_IzmeniLekCommand);
            ZatvoriCommand = new RelayCommand(Executed_ZatvoriCommand, CanExecute_ZatvoriCommand);







        }

        public void popuni(ListBox sastojakk,ListBox zamenaa)
        {
            sastojciBox = sastojakk;
            zameneBox = zamenaa;
            for (int i = 0; i < sas.Count; i++)
            {
                int dozvola = 0;
                for (int m = 0; m < l.Sastojak.Count; m++)
                {
                    if (l.Sastojak[m].Id.Equals(sas[i].Id))
                    {
                        dozvola = 1;
                    }
                }
                sastojciBox.Items.Add(sas[i].Naziv);
                if (dozvola == 1)
                {
                    sastojciBox.SelectedItems.Add(sas[i].Naziv);
                }



            }
            for (int i = 0; i < lekovi.Count; i++)
            {
                int dozvola = 0;
                for (int m = 0; m < l.IdZamena.Count; m++)
                {
                    Lek novi = new Lek();
                    for (int mo = 0; mo < lekovi.Count; mo++)
                    {
                        if (l.IdZamena[m].Equals(lekovi[mo].Id))
                        {
                            novi = lekovi[mo];
                        }
                    }
                    if (novi.Naziv.Equals(lekovi[i].Naziv) && novi.Proizvodjac.Equals(lekovi[i].Proizvodjac) && novi.KolicinaUMg.Equals(lekovi[i].KolicinaUMg))
                    {
                        dozvola = 1;
                    }
                }
                string k = lekovi[i].Proizvodjac + ", " + lekovi[i].Naziv + ", " + lekovi[i].KolicinaUMg;


                zameneBox.Items.Add(k);
                if (dozvola == 1)
                {
                    zameneBox.SelectedItems.Add(k);
                }



            }

            for (int m = 0; m < zameneBox.Items.Count; m++)
            {
                for (int a = 0; a < lekovi.Count; a++)
                {
                    if (zameneBox.Items[m].ToString().Equals(lekovi[a].Proizvodjac + ", " + lekovi[a].Naziv + ", " + lekovi[a].KolicinaUMg))
                    {
                        if (lekovi[a].Status.Equals(StatusLeka.cekaValidaciju))
                        {
                            zameneBox.Items.RemoveAt(m);
                            m--;
                            break;
                        }
                    }
                }
            }

        }



       
    }
}
