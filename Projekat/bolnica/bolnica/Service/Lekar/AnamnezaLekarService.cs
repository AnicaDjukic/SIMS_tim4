using Bolnica.DTO;
using Bolnica.Forms;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.Repository.Pregledi;
using Bolnica.ViewModel;
using Model.Korisnici;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bolnica.Services
{
    public class AnamnezaLekarService
    {
        private FileRepositoryPregled skladistePregleda = new FileRepositoryPregled();
        private FileRepositoryOperacija skladisteOperacija = new FileRepositoryOperacija();
        private FileRepositoryLek skladisteLekova = new FileRepositoryLek();
        private FileRepositoryAnamneza skladisteAnamneza = new FileRepositoryAnamneza();
        private FileRepositoryLekar skladisteLekara = new FileRepositoryLekar();


        public List<Pregled> DobijPreglede()
        {
            return skladistePregleda.GetAll();
        }
        public List<Operacija> DobijOperacije()
        {
            return skladisteOperacija.GetAll();
        }
        public List<Lek> DobijLekove()
        {
            return skladisteLekova.GetAll();
        }
        public List<Anamneza> DobijAnamneze()
        {
            return skladisteAnamneza.GetAll();
        }
        public List<Lekar> DobijLekare()
        {
            return skladisteLekara.GetAll();
        }
        public void DodajLek(AnamnezaLekarDTO anamnezaDTO)
        {
            ReceptLekarViewModel vm = new ReceptLekarViewModel(anamnezaDTO.trenutniPacijent);
            FormNapraviReceptLekar form = new FormNapraviReceptLekar(vm);

        }


        public void Potvrdi(AnamnezaLekarDTO anamnezaDTO)
        {
            Anamneza novaAnamneza = PopuniAnamnezu(anamnezaDTO);
            if (!anamnezaDTO.DaLiPostojiAnamneza)
            {
                novaAnamneza.Id = IzracunajSlobodniIdAnamneze(anamnezaDTO);
                anamnezaDTO.novaAnamneza = novaAnamneza;
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
                anamnezaDTO.novaAnamneza = novaAnamneza;
                skladisteAnamneza.Update(novaAnamneza);
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

        private int IzracunajSlobodniIdAnamneze(AnamnezaLekarDTO anamnezaDTO)
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
        private Anamneza PopuniAnamnezu(AnamnezaLekarDTO anamnezaDTO)
        {
            Anamneza novaAnamneza = new Anamneza(anamnezaDTO.simptomi, anamnezaDTO.dijagnoza);
            novaAnamneza.Recept = new List<Recept>();
            for (int i = 0; i < AnamnezaLekarViewModel.Recepti.Count; i++)
            {
                novaAnamneza.Recept.Add(PopuniRecept(i));
            }

            return novaAnamneza;
        }

        private Recept PopuniRecept(int i)
        {
            Recept recept = new Recept(AnamnezaLekarViewModel.Recepti[i].Id, AnamnezaLekarViewModel.Recepti[i].lek, AnamnezaLekarViewModel.Recepti[i].DatumIzdavanja, AnamnezaLekarViewModel.Recepti[i].Kolicina, AnamnezaLekarViewModel.Recepti[i].VremeUzimanja, AnamnezaLekarViewModel.Recepti[i].Trajanje);
            return recept;
        }

        private void AzurirajIstorijuPregleda(AnamnezaLekarDTO anamnezaDTO)
        {
            for (int p = 0; p < LekarViewModel.podaciListaIstorija.Items.Count; p++)
            {
                if (LekarViewModel.podaciListaIstorija.Items[p].Equals(anamnezaDTO.stariPregled))
                {
                    anamnezaDTO.stariPregled.Anamneza = anamnezaDTO.novaAnamneza;
                    anamnezaDTO.stariPregled.Zavrsen = true;
                    LekarViewModel.podaciListaIstorija.Items[p] = anamnezaDTO.stariPregled;
                    LekarViewModel.RefreshPodaciListuIstorija();
                    skladistePregleda.Update(PopuniPregled(anamnezaDTO));
                }
            }
        }

        private void AzurirajOperaciju(AnamnezaLekarDTO anamnezaDTO)
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
                    skladisteOperacija.Update(PopuniOperaciju(anamnezaDTO));
                }
            }
        }
        private void AzurirajPregled(AnamnezaLekarDTO anamnezaDTO)
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
                    skladistePregleda.Update(PopuniPregled(anamnezaDTO));
                }
            }
        }
        private void AzurirajIstorijuOperacija(AnamnezaLekarDTO anamnezaDTO)
        {
            for (int p = 0; p < LekarViewModel.podaciListaIstorija.Items.Count; p++)
            {
                if (LekarViewModel.podaciListaIstorija.Items[p].Equals(anamnezaDTO.staraOperacija))
                {
                    anamnezaDTO.trenutnaOperacija.Anamneza = anamnezaDTO.novaAnamneza;
                    anamnezaDTO.trenutnaOperacija.Zavrsen = true;
                    LekarViewModel.podaciListaIstorija.Items[p] = anamnezaDTO.trenutnaOperacija;
                    LekarViewModel.RefreshPodaciListuIstorija();
                    skladisteOperacija.Update(PopuniOperaciju(anamnezaDTO));
                }
            }
        }
        private Operacija PopuniOperaciju(AnamnezaLekarDTO anamnezaDTO)
        {
            Operacija novaOperacija = new Operacija(anamnezaDTO.trenutnaOperacija);
            novaOperacija.Anamneza = anamnezaDTO.novaAnamneza;
            novaOperacija.Zavrsen = true;
            return novaOperacija;
        }

        private Pregled PopuniPregled(AnamnezaLekarDTO anamnezaDTO)
        {
            Pregled noviPregled = new Pregled(anamnezaDTO.trenutniPregled);
            noviPregled.Anamneza = anamnezaDTO.novaAnamneza;
            noviPregled.Zavrsen = true;
            return noviPregled;
        }
        public void ObrisiRecept(AnamnezaLekarDTO anamnezaDTO)
        {
            if (anamnezaDTO.selektovaniIndeks > -1)
            {
                if (MessageBox.Show("Da li ste sigurni", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    
                }
                else
                {
                    int index = anamnezaDTO.selektovaniIndeks;
                    AnamnezaLekarViewModel.Recepti.RemoveAt(index);
                }
               
            }
            else
            {
                MessageBox.Show("Odaberite lek");
            }
        }
        public void ZakaziPregled(AnamnezaLekarDTO anamnezaDTO)
        {
            TerminLekarViewModel vm = new TerminLekarViewModel(anamnezaDTO.ulogovaniLekar,anamnezaDTO.trenutniPacijent);
            FormNapraviTerminLekar ff = new FormNapraviTerminLekar(vm);
        }


        public void VidiDetaljeOReceptu(AnamnezaLekarDTO anamnezaDTO)
        {
            if (anamnezaDTO.selektovaniIndeks > -1)
            {
                ReceptLekarViewModel vm = new ReceptLekarViewModel(anamnezaDTO.trenutniPacijent, PretvoriPrikazReceptaURecept(anamnezaDTO));
                FormVidiReceptLekar form = new FormVidiReceptLekar(vm);
            }
            else
            {
                MessageBox.Show("Odaberite lek");
            }
        }

        private Recept PretvoriPrikazReceptaURecept(AnamnezaLekarDTO anamnezaDTO)
        {
            PrikazRecepta selektovaniPrikazRecepta = new PrikazRecepta();
            selektovaniPrikazRecepta = anamnezaDTO.selektovaniItem;
            Recept selektovaniRecept = new Recept(selektovaniPrikazRecepta.Id, selektovaniPrikazRecepta.lek, selektovaniPrikazRecepta.DatumIzdavanja, selektovaniPrikazRecepta.Kolicina, selektovaniPrikazRecepta.VremeUzimanja, selektovaniPrikazRecepta.Trajanje);
            return selektovaniRecept;
        }


        



            
    }
}
