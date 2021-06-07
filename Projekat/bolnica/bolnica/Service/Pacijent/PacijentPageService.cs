using Bolnica.Forms;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Pregledi;
using Bolnica.Repository.Korisnici;
using Bolnica.ViewModel;
using Model.Korisnici;
using Model.Pacijenti;
using Model.Pregledi;
using Model.Prostorije;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Bolnica.Service
{
    public class PacijentPageService
    {
        private FileRepositoryPregled storagePregledi = new FileRepositoryPregled();
        private FileRepositoryAntiTrol storageAntiTrol = new FileRepositoryAntiTrol();

        public void ZakaziPregled(Pacijent trenutniPacijent)
        {
            int brojac = FormPacijentWeb.Forma.DobijBrojAktivnosti();

            if (brojac > 5)
            {
                FormPacijentWeb.Forma.BlokirajPacijenta();
                FormPacijentWeb.Forma.Close();
            }
            else
            {
                PosaljiPoslednjeUpozorenje(brojac);
                ZakaziPregledPacijentViewModel zakaziPregledPacijentViewModel = new ZakaziPregledPacijentViewModel(trenutniPacijent);
                FormPacijentWeb.Forma.Pocetna.Content = new FormZakaziPacijentPage(/*trenutniPacijent*/zakaziPregledPacijentViewModel);
            }
        }

        private static void PosaljiPoslednjeUpozorenje(int brojac)
        {
            if (brojac > 4)
            {
                MessageBox.Show("Posljednje upozorenje pred gasenje Vaseg naloga. Ukoliko nastavite da zloupotrebljavate " +
                    "nasu aplikaciju pristup samoj aplikaciji ce Vam biti onemogucen!", "Upozorenje");
            }
        }

        public void OtkaziPregled(PrikazPregleda SelektovaniRed)
        {
            var objekat = SelektovaniRed;
            if (objekat != null)
            {
                Operacija operacija = new Operacija();

                PrikazPregleda prikaz = SelektovaniRed;

                DateTime datum = prikaz.Datum;
                int dan = datum.Day;
                int mesec = datum.Month;
                int godina = datum.Year;

                DateTime danas = DateTime.Today;
                DateTime prekosutra = danas.AddDays(2);
                DateTime datumPregleda = new DateTime(godina, mesec, dan);

                if (prekosutra.CompareTo(datumPregleda) >= 0)
                {
                    MessageBox.Show("Ne mozete da otkazete pregled koji je zakazan za sledeca 2 dana!");
                }
                else
                {
                    if (objekat.GetType().Equals(operacija.GetType()))
                    {
                        foreach (PrikazPregleda p in PacijentPageViewModel.PrikazNezavrsenihPregleda)
                        {
                            if (objekat.Id.Equals(p.Id))
                            {
                                PrikazOperacije prikazOperacije = (PrikazOperacije)objekat;
                                ObrisiOperacijuIzTabele(prikazOperacije);
                                ObrisiOperaciju(prikazOperacije);
                                SacuvajAntiTrol(prikazOperacije);

                                PacijentPageViewModel pacijentPageViewModel = new PacijentPageViewModel(prikaz.Pacijent);
                                FormPacijentWeb.Forma.Pocetna.Content = new FormPacijentPage(pacijentPageViewModel);

                                break;
                            }
                        }
                    }
                    else
                    {
                        foreach (PrikazPregleda p in PacijentPageViewModel.PrikazNezavrsenihPregleda)
                        {
                            if (objekat.Id.Equals(p.Id))
                            {
                                ObrisiPregledIzTabele(objekat);
                                ObrisiPregled(objekat);
                                SacuvajAntiTrol(objekat);

                                PacijentPageViewModel pacijentPageViewModel = new PacijentPageViewModel(prikaz.Pacijent);
                                FormPacijentWeb.Forma.Pocetna.Content = new FormPacijentPage(pacijentPageViewModel);

                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Morate odabrati pregled ili operaciju koju zelite da otkazete!", "Upozorenje");
            }
        }

        private static void ObrisiOperacijuIzTabele(PrikazOperacije prikazOperacije)
        {
            PacijentPageViewModel.PrikazNezavrsenihPregleda.Remove(prikazOperacije);
        }

        private void ObrisiOperaciju(PrikazOperacije prikazOperacije)
        {
            Operacija operacija = new Operacija
            {
                Id = prikazOperacije.Id
            };
            storagePregledi.Delete(operacija);
        }

        private static void ObrisiPregledIzTabele(PrikazPregleda prikazPregleda)
        {
            PacijentPageViewModel.PrikazNezavrsenihPregleda.Remove(prikazPregleda);
        }

        private void ObrisiPregled(PrikazPregleda prikazPregleda)
        {
            Pregled pregled = new Pregled
            {
                Id = prikazPregleda.Id
            };
            storagePregledi.Delete(pregled);
        }

        private void SacuvajAntiTrol(PrikazOperacije prikazOperacije)
        {
            AntiTrol antiTrol = new AntiTrol
            {
                Id = DobijIdAntiTrol(),
                Pacijent = prikazOperacije.Pacijent,
                Datum = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
            };
            storageAntiTrol.Save(antiTrol);
        }

        private int DobijIdAntiTrol()
        {
            List<AntiTrol> antiTrolList = storageAntiTrol.GetAll();
            int max = 0;
            foreach (AntiTrol antiTrol in antiTrolList)
            {
                if (antiTrol.Id > max)
                {
                    max = antiTrol.Id;
                }
            }
            return max + 1;
        }

        private void SacuvajAntiTrol(PrikazPregleda prikazPregleda)
        {
            AntiTrol antiTrol = new AntiTrol
            {
                Id = DobijIdAntiTrol(),
                Pacijent = prikazPregleda.Pacijent,
                Datum = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
            };
            storageAntiTrol.Save(antiTrol);
        }

        public void IzmeniPregled(PrikazPregleda SelektovaniRed)
        {
            var objekat = SelektovaniRed;
            if (objekat != null)
            {
                if (objekat.GetType().Equals(new PrikazOperacije().GetType()))
                {
                    MessageBox.Show("Ne mozete da izmenite operaciju!");
                }
                else
                {
                    PrikazPregleda prikaz = SelektovaniRed;

                    DateTime datum = prikaz.Datum;
                    int dan = datum.Day;
                    int mesec = datum.Month;
                    int godina = datum.Year;

                    DateTime danas = DateTime.Today;
                    DateTime prekosutra = danas.AddDays(2);
                    DateTime datumPregleda = new DateTime(godina, mesec, dan);

                    if (prekosutra.CompareTo(datumPregleda) >= 0)
                    {
                        MessageBox.Show("Ne mozete da menjate pregled koji je zakazan za sledeca 2 dana, ili je vec prosao!");
                    }
                    else
                    {
                        int brojac = FormPacijentWeb.Forma.DobijBrojAktivnosti();

                        if (brojac > 5)
                        {
                            FormPacijentWeb.Forma.BlokirajPacijenta();
                            FormPacijentWeb.Forma.Close();
                        }
                        else
                        {
                            if (brojac > 4)
                            {
                                MessageBox.Show("Posljednje upozorenje pred gasenje Vaseg naloga. Ukoliko nastavite da zloupotrebljavate " +
                                    "nasu aplikaciju pristup samoj aplikaciji ce Vam biti onemogucen!", "Upozorenje");
                            }
                            FormPacijentWeb.Forma.Pocetna.Content = new FormIzmeniPacijentPage(prikaz);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Morate odabrati pregled koju zelite da izmenite!", "Upozorenje");
            }
        }

        public void IstorijaPregleda(Pacijent trenutniPacijent)
        {
            FormPacijentWeb.Forma.Pocetna.Content = new FormIstorijaPregledaPage(trenutniPacijent);
        }

        public void ObavestenjaPacijent(Pacijent trenutniPacijent)
        {
            FormPacijentWeb.Forma.Pocetna.Content = new FormObavestenjaPacijentPage(trenutniPacijent);
        }
    }
}
