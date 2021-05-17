using Bolnica.Forms;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.ViewModel;
using Model.Korisnici;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bolnica.Services
{
    public class NapraviAnamnezuLekarService
    {
        private FileStoragePregledi skladistePregleda = new FileStoragePregledi();
        private FileStorageLek skladisteLekova = new FileStorageLek();
        private FileStorageAnamneza skladisteAnamneza = new FileStorageAnamneza();
        private FileStorageLekar skladisteLekara = new FileStorageLekar();


        public void DodajLek(Pacijent trenutniPacijent)
        {
            NapraviIVidiReceptLekarViewModel vm = new NapraviIVidiReceptLekarViewModel(trenutniPacijent);
            FormNapraviReceptLekar form = new FormNapraviReceptLekar(vm);

        }


        public void Potvrdi(int DaLiPostojiAnamneza, int DaLiJePregled,int idAnamneze,string simptomi,string dijagnoza,PrikazPregleda stariPregled,PrikazPregleda trenutniPregled,PrikazOperacije staraOperacija,PrikazOperacije trenutnaOperacija,List<Anamneza> sveAnamneze)
        {
            Anamneza novaAnamneza = PopuniAnamnezu(simptomi,dijagnoza);
            if (DaLiPostojiAnamneza == 0)
            {
                novaAnamneza.Id = IzracunajSlobodniIdAnamneze(sveAnamneze);
                skladisteAnamneza.Save(novaAnamneza);
                if (DaLiJePregled == 1)
                {
                    AzurirajPregled(novaAnamneza,stariPregled,trenutniPregled);
                }
                else
                {
                    AzurirajOperaciju(novaAnamneza,staraOperacija,trenutnaOperacija);
                }
            }
            else
            {
                novaAnamneza.Id = idAnamneze;
                skladisteAnamneza.Izmeni(novaAnamneza);
                if (DaLiJePregled == 1)
                {
                    AzurirajIstorijuPregleda(novaAnamneza,stariPregled,trenutniPregled);
                }
                else
                {
                    AzurirajIstorijuOperacija(novaAnamneza,trenutnaOperacija,trenutnaOperacija);
                }
            }
        }

        public int IzracunajSlobodniIdAnamneze(List<Anamneza> sveAnamneze)
        {
            int max = 0;
            for (int id = 0; id < sveAnamneze.Count; id++)
            {
                if (sveAnamneze[id].Id > max)
                {
                    max = sveAnamneze[id].Id;
                }
            }
            return max + 1;
        }
        public Anamneza PopuniAnamnezu(string simptomi,string dijagnoza)
        {
            Anamneza novaAnamneza = new Anamneza();

            novaAnamneza.Simptomi = simptomi;
            novaAnamneza.Dijagnoza = dijagnoza;
            novaAnamneza.Recept = new List<Recept>();
            for (int i = 0; i < NapraviAnamnezuLekarViewModel.Recepti.Count; i++)
            {
                novaAnamneza.Recept.Add(PopuniRecept(i));
            }

            return novaAnamneza;
        }

        public Recept PopuniRecept(int i)
        {
            Recept recept = new Recept();
            recept.DatumIzdavanja = NapraviAnamnezuLekarViewModel.Recepti[i].DatumIzdavanja;
            recept.Id = NapraviAnamnezuLekarViewModel.Recepti[i].Id;
            recept.Kolicina = NapraviAnamnezuLekarViewModel.Recepti[i].Kolicina;
            recept.Lek = NapraviAnamnezuLekarViewModel.Recepti[i].lek;
            recept.Trajanje = NapraviAnamnezuLekarViewModel.Recepti[i].Trajanje;
            recept.VremeUzimanja = NapraviAnamnezuLekarViewModel.Recepti[i].VremeUzimanja;
            return recept;
        }

        public void AzurirajIstorijuPregleda(Anamneza novaAnamneza,PrikazPregleda stariPregled,PrikazPregleda trenutniPregled)
        {
            for (int p = 0; p < LekarViewModel.dataListIstorija.Items.Count; p++)
            {
                if (LekarViewModel.dataListIstorija.Items[p].Equals(stariPregled))
                {
                    stariPregled.Anamneza = novaAnamneza;
                    stariPregled.Zavrsen = true;
                    LekarViewModel.dataListIstorija.Items[p] = stariPregled;
                    LekarViewModel.dataIstorija();
                    skladistePregleda.Izmeni(PopuniPregled(novaAnamneza,trenutniPregled));
                }
            }
        }

        public void AzurirajOperaciju(Anamneza novaAnamneza,PrikazOperacije staraOperacija,PrikazOperacije trenutnaOperacija)
        {
            for (int p = 0; p < LekarViewModel.dataList.Items.Count; p++)
            {
                if (LekarViewModel.dataList.Items[p].Equals(staraOperacija))
                {
                    trenutnaOperacija.Anamneza = novaAnamneza;
                    trenutnaOperacija.Zavrsen = true;
                    LekarViewModel.dataListIstorija.Items.Add(trenutnaOperacija);
                    LekarViewModel.dataList.Items.RemoveAt(p);
                    LekarViewModel.dataIstorija();
                    LekarViewModel.data();
                    skladistePregleda.Izmeni(PopuniOperaciju(novaAnamneza,trenutnaOperacija));
                }
            }
        }
        public void AzurirajPregled(Anamneza novaAnamneza,PrikazPregleda stariPregled,PrikazPregleda trenutniPregled)
        {
            for (int p = 0; p < LekarViewModel.dataList.Items.Count; p++)
            {
                if (LekarViewModel.dataList.Items[p].Equals(stariPregled))
                {
                    trenutniPregled.Anamneza = novaAnamneza;
                    trenutniPregled.Zavrsen = true;
                    LekarViewModel.dataListIstorija.Items.Add(trenutniPregled);
                    LekarViewModel.dataList.Items.RemoveAt(p);
                    LekarViewModel.dataIstorija();
                    LekarViewModel.data();
                    skladistePregleda.Izmeni(PopuniPregled(novaAnamneza,trenutniPregled));
                }
            }
        }
        public void AzurirajIstorijuOperacija(Anamneza novaAnamneza,PrikazOperacije staraOperacija,PrikazOperacije trenutnaOperacija)
        {
            for (int p = 0; p < LekarViewModel.dataListIstorija.Items.Count; p++)
            {
                if (LekarViewModel.dataListIstorija.Items[p].Equals(staraOperacija))
                {
                    trenutnaOperacija.Anamneza = novaAnamneza;
                    trenutnaOperacija.Zavrsen = true;
                    LekarViewModel.dataListIstorija.Items[p] = trenutnaOperacija;
                    LekarViewModel.dataIstorija();
                    skladistePregleda.Izmeni(PopuniOperaciju(novaAnamneza,trenutnaOperacija));
                }
            }
        }
        public Operacija PopuniOperaciju(Anamneza novaAnamneza,PrikazOperacije trenutnaOperacija)
        {
            Operacija novaOperacija = new Operacija();
            novaOperacija.Id = trenutnaOperacija.Id;
            novaOperacija.Hitan = trenutnaOperacija.Hitan;
            novaOperacija.Lekar = trenutnaOperacija.Lekar;
            novaOperacija.Pacijent = trenutnaOperacija.Pacijent;
            novaOperacija.TipOperacije = trenutnaOperacija.TipOperacije;
            novaOperacija.Trajanje = trenutnaOperacija.Trajanje;
            novaOperacija.Zavrsen = trenutnaOperacija.Zavrsen;
            novaOperacija.Anamneza = novaAnamneza;
            novaOperacija.Prostorija = trenutnaOperacija.Prostorija;
            novaOperacija.Datum = trenutnaOperacija.Datum;
            novaOperacija.Zavrsen = true;
            return novaOperacija;
        }

        public Pregled PopuniPregled(Anamneza novaAnamneza,PrikazPregleda trenutniPregled)
        {
            Pregled noviPregled = new Pregled();
            noviPregled.Id = trenutniPregled.Id;
            noviPregled.Hitan = trenutniPregled.Hitan;
            noviPregled.Lekar = trenutniPregled.Lekar;
            noviPregled.Pacijent = trenutniPregled.Pacijent;
            noviPregled.Trajanje = trenutniPregled.Trajanje;
            noviPregled.Zavrsen = trenutniPregled.Zavrsen;
            noviPregled.Anamneza = novaAnamneza;
            noviPregled.Prostorija = trenutniPregled.Prostorija;
            noviPregled.Datum = trenutniPregled.Datum;
            noviPregled.Zavrsen = true;
            return noviPregled;
        }
        public void ObrisiRecept(DataGrid dataGridLekovi)
        {
            if (dataGridLekovi.SelectedCells.Count > 0)
            {
                int index = dataGridLekovi.SelectedIndex;
                NapraviAnamnezuLekarViewModel.Recepti.RemoveAt(index);
            }
        }
        public void ZakaziPregled(Lekar ulogovaniLekar,Pacijent trenutniPacijent)
        {
            IzmeniINapraviTerminLekarViewModel vm = new IzmeniINapraviTerminLekarViewModel(ulogovaniLekar, trenutniPacijent);
            FormNapraviTerminLekar ff = new FormNapraviTerminLekar(vm);
        }


        public void VidiDetaljeOReceptu(DataGrid dataGridLekovi,Pacijent trenutniPacijent)
        {
            if (dataGridLekovi.SelectedCells.Count > 0)
            {
                NapraviIVidiReceptLekarViewModel vm = new NapraviIVidiReceptLekarViewModel(trenutniPacijent, PretvoriPrikazReceptaURecept(dataGridLekovi));
                FormVidiReceptLekar form = new FormVidiReceptLekar(vm);
            }
        }

        public Recept PretvoriPrikazReceptaURecept(DataGrid dataGridLekovi)
        {
            PrikazRecepta selektovaniPrikazRecepta = new PrikazRecepta();
            selektovaniPrikazRecepta = dataGridLekovi.SelectedItem as PrikazRecepta;
            Recept selektovaniRecept = new Recept();
            selektovaniRecept.DatumIzdavanja = selektovaniPrikazRecepta.DatumIzdavanja;
            selektovaniRecept.Id = selektovaniPrikazRecepta.Id;
            selektovaniRecept.Kolicina = selektovaniPrikazRecepta.Kolicina;
            selektovaniRecept.Lek = selektovaniPrikazRecepta.lek;
            selektovaniRecept.Trajanje = selektovaniPrikazRecepta.Trajanje;
            selektovaniRecept.VremeUzimanja = selektovaniPrikazRecepta.VremeUzimanja;
            return selektovaniRecept;
        }


        public void PredjiNaScrollBar(Button IzbrisiButton)
        {
            //key.right
                IzbrisiButton.MoveFocus(new TraversalRequest(FocusNavigationDirection.Right));

            
        }

        public void ZaustaviStrelice(ScrollViewer ScroolBar)
        {
            //key.up

                ScroolBar.ScrollToVerticalOffset(20);
            
        }

        
    }
}
