using Bolnica.Forms;
using Bolnica.Model.Pregledi;
using Bolnica.ViewModel;
using Model.Korisnici;
using Model.Pregledi;
using System;
using System.Windows;

namespace Bolnica.Service
{
    public class PacijentPageService
    {
        private PodsetnikService servicePodsetnik = new PodsetnikService();
        private AntiTrolService serviceAntiTrol = new AntiTrolService();
        private PregledService servicePregled = new PregledService();

        public void ZakaziPregled(Pacijent trenutniPacijent)
        {
            int brojac = serviceAntiTrol.DobijBrojAktivnosti(trenutniPacijent);

            if (brojac > 5)
            {
                servicePodsetnik.BlokirajPacijenta(trenutniPacijent);
                FormPacijentWeb.Forma.Close();
            }
            else
            {
                PosaljiPoslednjeUpozorenje(brojac);
                ZakaziPregledPacijentViewModel zakaziPregledPacijentViewModel = new ZakaziPregledPacijentViewModel(trenutniPacijent);
                FormPacijentWeb.Forma.Pocetna.Content = new FormZakaziPacijentPage(zakaziPregledPacijentViewModel);
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
                                ObrisiPregled(prikazOperacije);
                                SacuvajAntiTrol(prikazOperacije);

                                PacijentPageViewModel pacijentPageViewModel = new PacijentPageViewModel(prikaz.Pacijent);
                                FormPacijentWeb.Forma.Pocetna.Content = new FormPacijentPage(pacijentPageViewModel);
                                MessageBox.Show("Operacija uspešno otkazana!");

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
                                MessageBox.Show("Pregled uspešno otkazan!");

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
            servicePregled.IzbrisiPregled(pregled);
        }

        private void SacuvajAntiTrol(PrikazPregleda prikazPregleda)
        {
            AntiTrol antiTrol = serviceAntiTrol.KreirajAntiTrol(prikazPregleda);
            serviceAntiTrol.SacuvajAntiTrol(antiTrol);
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
                        int brojac = serviceAntiTrol.DobijBrojAktivnosti(SelektovaniRed.Pacijent);

                        if (brojac > 5)
                        {
                            servicePodsetnik.BlokirajPacijenta(SelektovaniRed.Pacijent);
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
            IstorijaPregledaPacijentViewModel istorijaPregledaPacijentViewModel = new IstorijaPregledaPacijentViewModel(trenutniPacijent);
            FormPacijentWeb.Forma.Pocetna.Content = new FormIstorijaPregledaPage(istorijaPregledaPacijentViewModel);
        }

        public void ObavestenjaPacijent(Pacijent trenutniPacijent)
        {
            FormPacijentWeb.Forma.Pocetna.Content = new FormObavestenjaPacijentPage(trenutniPacijent);
        }
    }
}
