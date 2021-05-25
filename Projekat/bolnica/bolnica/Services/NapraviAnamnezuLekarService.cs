using Bolnica.DTO;
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


        public void DodajLek(NapraviAnamnezuLekarServiceDTO anamnezaDTO)
        {
            NapraviIVidiReceptLekarViewModel vm = new NapraviIVidiReceptLekarViewModel(anamnezaDTO.trenutniPacijent);
            FormNapraviReceptLekar form = new FormNapraviReceptLekar(vm);

        }


        public void Potvrdi(NapraviAnamnezuLekarServiceDTO anamnezaDTO)
        {
            Anamneza novaAnamneza = PopuniAnamnezu(anamnezaDTO);
            if (!anamnezaDTO.DaLiPostojiAnamneza)
            {
                novaAnamneza.Id = IzracunajSlobodniIdAnamneze(anamnezaDTO);
                skladisteAnamneza.Save(novaAnamneza);
                if (anamnezaDTO.DaLiJePregled)
                {
                    AzurirajPregled(anamnezaDTO);
                }
                else
                {
                    AzurirajOperaciju(anamnezaDTO);
                }
            }
            else
            {
                novaAnamneza.Id = anamnezaDTO.idAnamneze;
                skladisteAnamneza.Izmeni(novaAnamneza);
                if (anamnezaDTO.DaLiJePregled)
                {
                    AzurirajIstorijuPregleda(anamnezaDTO);
                }
                else
                {
                    AzurirajIstorijuOperacija(anamnezaDTO);
                }
            }
        }

        public int IzracunajSlobodniIdAnamneze(NapraviAnamnezuLekarServiceDTO anamnezaDTO)
        {
            int max = 0;
            for (int id = 0; id < anamnezaDTO.sveAnamneze.Count; id++)
            {
                if (anamnezaDTO.sveAnamneze[id].Id > max)
                {
                    max = anamnezaDTO.sveAnamneze[id].Id;
                }
            }
            return max + 1;
        }
        public Anamneza PopuniAnamnezu(NapraviAnamnezuLekarServiceDTO anamnezaDTO)
        {
            Anamneza novaAnamneza = new Anamneza(anamnezaDTO.simptomi, anamnezaDTO.dijagnoza);
            novaAnamneza.Recept = new List<Recept>();
            for (int i = 0; i < NapraviAnamnezuLekarViewModel.Recepti.Count; i++)
            {
                novaAnamneza.Recept.Add(PopuniRecept(i));
            }

            return novaAnamneza;
        }

        public Recept PopuniRecept(int i)
        {
            Recept recept = new Recept(NapraviAnamnezuLekarViewModel.Recepti[i].Id, NapraviAnamnezuLekarViewModel.Recepti[i].lek, NapraviAnamnezuLekarViewModel.Recepti[i].DatumIzdavanja, NapraviAnamnezuLekarViewModel.Recepti[i].Kolicina, NapraviAnamnezuLekarViewModel.Recepti[i].VremeUzimanja, NapraviAnamnezuLekarViewModel.Recepti[i].Trajanje);
            return recept;
        }

        public void AzurirajIstorijuPregleda(NapraviAnamnezuLekarServiceDTO anamnezaDTO)
        {
            for (int p = 0; p < LekarViewModel.podaciListaIstorija.Items.Count; p++)
            {
                if (LekarViewModel.podaciListaIstorija.Items[p].Equals(anamnezaDTO.stariPregled))
                {
                    anamnezaDTO.stariPregled.Anamneza = anamnezaDTO.novaAnamneza;
                    anamnezaDTO.stariPregled.Zavrsen = true;
                    LekarViewModel.podaciListaIstorija.Items[p] = anamnezaDTO.stariPregled;
                    LekarViewModel.RefreshPodaciListuIstorija();
                    skladistePregleda.Izmeni(PopuniPregled(anamnezaDTO));
                }
            }
        }

        public void AzurirajOperaciju(NapraviAnamnezuLekarServiceDTO anamnezaDTO)
        {
            for (int p = 0; p < LekarViewModel.podaciLista.Items.Count; p++)
            {
                if (LekarViewModel.podaciLista.Items[p].Equals(anamnezaDTO.staraOperacija))
                {
                    anamnezaDTO.trenutnaOperacija.Anamneza = anamnezaDTO.novaAnamneza;
                    anamnezaDTO.trenutnaOperacija.Zavrsen = true;
                    LekarViewModel.podaciListaIstorija.Items.Add(anamnezaDTO.trenutnaOperacija);
                    LekarViewModel.podaciLista.Items.RemoveAt(p);
                    LekarViewModel.RefreshPodaciListuIstorija();
                    LekarViewModel.RefreshPodaciListu();
                    skladistePregleda.Izmeni(PopuniOperaciju(anamnezaDTO));
                }
            }
        }
        public void AzurirajPregled(NapraviAnamnezuLekarServiceDTO anamnezaDTO)
        {
            for (int p = 0; p < LekarViewModel.podaciLista.Items.Count; p++)
            {
                if (LekarViewModel.podaciLista.Items[p].Equals(anamnezaDTO.stariPregled))
                {
                    anamnezaDTO.trenutniPregled.Anamneza = anamnezaDTO.novaAnamneza;
                    anamnezaDTO.trenutniPregled.Zavrsen = true;
                    LekarViewModel.podaciListaIstorija.Items.Add(anamnezaDTO.trenutniPregled);
                    LekarViewModel.podaciLista.Items.RemoveAt(p);
                    LekarViewModel.RefreshPodaciListuIstorija();
                    LekarViewModel.RefreshPodaciListu();
                    skladistePregleda.Izmeni(PopuniPregled(anamnezaDTO));
                }
            }
        }
        public void AzurirajIstorijuOperacija(NapraviAnamnezuLekarServiceDTO anamnezaDTO)
        {
            for (int p = 0; p < LekarViewModel.podaciListaIstorija.Items.Count; p++)
            {
                if (LekarViewModel.podaciListaIstorija.Items[p].Equals(anamnezaDTO.staraOperacija))
                {
                    anamnezaDTO.trenutnaOperacija.Anamneza = anamnezaDTO.novaAnamneza;
                    anamnezaDTO.trenutnaOperacija.Zavrsen = true;
                    LekarViewModel.podaciListaIstorija.Items[p] = anamnezaDTO.trenutnaOperacija;
                    LekarViewModel.RefreshPodaciListuIstorija();
                    skladistePregleda.Izmeni(PopuniOperaciju(anamnezaDTO));
                }
            }
        }
        public Operacija PopuniOperaciju(NapraviAnamnezuLekarServiceDTO anamnezaDTO)
        {
            Operacija novaOperacija = new Operacija(anamnezaDTO.trenutnaOperacija);
            novaOperacija.Anamneza = anamnezaDTO.novaAnamneza;
            novaOperacija.Zavrsen = true;
            return novaOperacija;
        }

        public Pregled PopuniPregled(NapraviAnamnezuLekarServiceDTO anamnezaDTO)
        {
            Pregled noviPregled = new Pregled(anamnezaDTO.trenutniPregled);
            noviPregled.Anamneza = anamnezaDTO.novaAnamneza;
            noviPregled.Zavrsen = true;
            return noviPregled;
        }
        public void ObrisiRecept(NapraviAnamnezuLekarServiceDTO anamnezaDTO)
        {
            if (anamnezaDTO.dataGridLekovi.SelectedCells.Count > 0)
            {
                int index = anamnezaDTO.dataGridLekovi.SelectedIndex;
                NapraviAnamnezuLekarViewModel.Recepti.RemoveAt(index);
            }
        }
        public void ZakaziPregled(NapraviAnamnezuLekarServiceDTO anamnezaDTO)
        {
            IzmeniINapraviTerminLekarViewModel vm = new IzmeniINapraviTerminLekarViewModel(anamnezaDTO.ulogovaniLekar,anamnezaDTO.trenutniPacijent);
            FormNapraviTerminLekar ff = new FormNapraviTerminLekar(vm);
        }


        public void VidiDetaljeOReceptu(NapraviAnamnezuLekarServiceDTO anamnezaDTO)
        {
            if (anamnezaDTO.dataGridLekovi.SelectedCells.Count > 0)
            {
                NapraviIVidiReceptLekarViewModel vm = new NapraviIVidiReceptLekarViewModel(anamnezaDTO.trenutniPacijent, PretvoriPrikazReceptaURecept(anamnezaDTO));
                FormVidiReceptLekar form = new FormVidiReceptLekar(vm);
            }
        }

        public Recept PretvoriPrikazReceptaURecept(NapraviAnamnezuLekarServiceDTO anamnezaDTO)
        {
            PrikazRecepta selektovaniPrikazRecepta = new PrikazRecepta();
            selektovaniPrikazRecepta = anamnezaDTO.dataGridLekovi.SelectedItem as PrikazRecepta;
            Recept selektovaniRecept = new Recept(selektovaniPrikazRecepta.Id, selektovaniPrikazRecepta.lek, selektovaniPrikazRecepta.DatumIzdavanja, selektovaniPrikazRecepta.Kolicina, selektovaniPrikazRecepta.VremeUzimanja, selektovaniPrikazRecepta.Trajanje);
            return selektovaniRecept;
        }


        public void PredjiNaScrollBar(NapraviAnamnezuLekarServiceDTO anamnezaDTO)
        {

            anamnezaDTO.IzbrisiButton.MoveFocus(new TraversalRequest(FocusNavigationDirection.Right));

            
        }

        public void ZaustaviStrelice(NapraviAnamnezuLekarServiceDTO anamnezaDTO)
        {


            anamnezaDTO.ScroolBar.ScrollToVerticalOffset(20);
            
        }

        
    }
}
