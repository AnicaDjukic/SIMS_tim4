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
            Anamneza novaAnamneza = new Anamneza();

            novaAnamneza.Simptomi = anamnezaDTO.simptomi;
            novaAnamneza.Dijagnoza = anamnezaDTO.dijagnoza;
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
            Operacija novaOperacija = new Operacija();
            novaOperacija.Id = anamnezaDTO.trenutnaOperacija.Id;
            novaOperacija.Hitan = anamnezaDTO.trenutnaOperacija.Hitan;
            novaOperacija.Lekar = anamnezaDTO.trenutnaOperacija.Lekar;
            novaOperacija.Pacijent = anamnezaDTO.trenutnaOperacija.Pacijent;
            novaOperacija.TipOperacije = anamnezaDTO.trenutnaOperacija.TipOperacije;
            novaOperacija.Trajanje = anamnezaDTO.trenutnaOperacija.Trajanje;
            novaOperacija.Zavrsen = anamnezaDTO.trenutnaOperacija.Zavrsen;
            novaOperacija.Anamneza = anamnezaDTO.novaAnamneza;
            novaOperacija.Prostorija = anamnezaDTO.trenutnaOperacija.Prostorija;
            novaOperacija.Datum = anamnezaDTO.trenutnaOperacija.Datum;
            novaOperacija.Zavrsen = true;
            return novaOperacija;
        }

        public Pregled PopuniPregled(NapraviAnamnezuLekarServiceDTO anamnezaDTO)
        {
            Pregled noviPregled = new Pregled();
            noviPregled.Id = anamnezaDTO.trenutniPregled.Id;
            noviPregled.Hitan = anamnezaDTO.trenutniPregled.Hitan;
            noviPregled.Lekar = anamnezaDTO.trenutniPregled.Lekar;
            noviPregled.Pacijent = anamnezaDTO.trenutniPregled.Pacijent;
            noviPregled.Trajanje = anamnezaDTO.trenutniPregled.Trajanje;
            noviPregled.Zavrsen = anamnezaDTO.trenutniPregled.Zavrsen;
            noviPregled.Anamneza = anamnezaDTO.novaAnamneza;
            noviPregled.Prostorija = anamnezaDTO.trenutniPregled.Prostorija;
            noviPregled.Datum = anamnezaDTO.trenutniPregled.Datum;
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
            Recept selektovaniRecept = new Recept();
            selektovaniRecept.DatumIzdavanja = selektovaniPrikazRecepta.DatumIzdavanja;
            selektovaniRecept.Id = selektovaniPrikazRecepta.Id;
            selektovaniRecept.Kolicina = selektovaniPrikazRecepta.Kolicina;
            selektovaniRecept.Lek = selektovaniPrikazRecepta.lek;
            selektovaniRecept.Trajanje = selektovaniPrikazRecepta.Trajanje;
            selektovaniRecept.VremeUzimanja = selektovaniPrikazRecepta.VremeUzimanja;
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
