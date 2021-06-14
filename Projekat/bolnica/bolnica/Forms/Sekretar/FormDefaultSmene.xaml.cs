using Bolnica.Model.Korisnici;
using Bolnica.Repository.Korisnici;
using Bolnica.Repository.Pregledi;
using Bolnica.Sekretar;
using Model.Korisnici;
using Model.Pregledi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bolnica.Forms.Sekretar
{
    /// <summary>
    /// Interaction logic for FormSmene.xaml
    /// </summary>
    public partial class FormDefaultSmene : Window
    {
        private string korisnickoIme;
        private FileRepositoryLekar skladisteLekara;
        private FileRepositorySmena skladisteSmena;
        private FileRepositoryPregled skladistePregleda;
        private FileRepositoryOperacija skladisteOperacija;
        public FormDefaultSmene(string korisnickoIme)
        {
            InitializeComponent();
            this.korisnickoIme = korisnickoIme;
            skladisteLekara = new FileRepositoryLekar();
            skladisteSmena = new FileRepositorySmena();
            skladistePregleda = new FileRepositoryPregled();
            skladisteOperacija = new FileRepositoryOperacija();
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            int selectedSmena = comboSmena.SelectedIndex;
            PodrazumevanaSmena novaSmena = new PodrazumevanaSmena();
            if (selectedSmena == 0)
                novaSmena = PodrazumevanaSmena.Prva;
            else if (selectedSmena == 1)
                novaSmena = PodrazumevanaSmena.Druga;
            else if (selectedSmena == 2)
                novaSmena = PodrazumevanaSmena.Treca;

            Lekar lekar = skladisteLekara.GetById(korisnickoIme);
            Smena smena = skladisteSmena.GetById(lekar.Smena.Id);
            for (int i = 0; i < FormLekari.Lekari.Count; i++) 
                if (FormLekari.Lekari[i].KorisnickoIme == lekar.KorisnickoIme) 
                {
                    FormLekari.Lekari.RemoveAt(i);
                    break;
                }
            smena.PodrazumevanaSmena = novaSmena;
            skladisteSmena.Update(smena);
            lekar.Smena = smena;
            FormLekari.Lekari.Add(lekar);

            foreach (Pregled p in skladistePregleda.GetAll())
                if (smena.PocetakSmene.Date == p.Datum.Date)
                    continue;
                else 
                {
                    int sati = p.Datum.Hour;
                    int minute = p.Datum.Minute;

                    if (smena.Id == skladisteLekara.GetById(p.Lekar.KorisnickoIme).Smena.Id && smena.PodrazumevanaSmena == PodrazumevanaSmena.Prva && (sati < 7 || sati >= 15 || (sati == 14 && minute > 30))) 
                    {
                        skladistePregleda.Delete(p);
                        for (int i = 0; i < FormPregledi.Pregledi.Count; i++)
                            if (FormPregledi.Pregledi[i].Id == p.Id)
                            {
                                FormPregledi.Pregledi.RemoveAt(i);
                                break;
                            }
                    }
                    else if (smena.Id == skladisteLekara.GetById(p.Lekar.KorisnickoIme).Smena.Id && smena.PodrazumevanaSmena == PodrazumevanaSmena.Druga && (sati < 15 || sati >= 23 || (sati == 22 && minute > 30))) 
                    {
                        skladistePregleda.Delete(p);
                        for (int i = 0; i < FormPregledi.Pregledi.Count; i++)
                            if (FormPregledi.Pregledi[i].Id == p.Id)
                            {
                                FormPregledi.Pregledi.RemoveAt(i);
                                break;
                            }
                    }
                    else if (smena.Id == skladisteLekara.GetById(p.Lekar.KorisnickoIme).Smena.Id && smena.PodrazumevanaSmena == PodrazumevanaSmena.Treca && !(sati >= 23 || sati < 7) && !(sati == 6 && minute > 30)) 
                    {
                        skladistePregleda.Delete(p);
                        for (int i = 0; i < FormPregledi.Pregledi.Count; i++)
                            if (FormPregledi.Pregledi[i].Id == p.Id)
                            {
                                FormPregledi.Pregledi.RemoveAt(i);
                                break;
                            }
                    }
                }

            foreach (Operacija o in skladisteOperacija.GetAll())
                if (smena.PocetakSmene.Date == o.Datum.Date)
                    continue;
                else
                {
                    int sati = o.Datum.Hour;
                    int minute = o.Datum.Minute;

                    if (smena.Id == skladisteLekara.GetById(o.Lekar.KorisnickoIme).Smena.Id && smena.PodrazumevanaSmena == PodrazumevanaSmena.Prva && (sati < 7 || sati >= 15 || (sati == 14 && minute > 30)))
                    {
                        skladisteOperacija.Delete(o);
                        for (int i = 0; i < FormPregledi.Pregledi.Count; i++)
                            if (FormPregledi.Pregledi[i].Id == o.Id)
                            {
                                FormPregledi.Pregledi.RemoveAt(i);
                                break;
                            }
                    }
                    else if (smena.Id == skladisteLekara.GetById(o.Lekar.KorisnickoIme).Smena.Id && smena.PodrazumevanaSmena == PodrazumevanaSmena.Druga && (sati < 15 || sati >= 23 || (sati == 22 && minute > 30)))
                    {
                        skladisteOperacija.Delete(o);
                        for (int i = 0; i < FormPregledi.Pregledi.Count; i++)
                            if (FormPregledi.Pregledi[i].Id == o.Id)
                            {
                                FormPregledi.Pregledi.RemoveAt(i);
                                break;
                            }
                    }
                    else if (smena.Id == skladisteLekara.GetById(o.Lekar.KorisnickoIme).Smena.Id && smena.PodrazumevanaSmena == PodrazumevanaSmena.Treca && !(sati >= 23 || sati < 7) && !(sati == 6 && minute > 30))
                    {
                        skladisteOperacija.Delete(o);
                        for (int i = 0; i < FormPregledi.Pregledi.Count; i++)
                            if (FormPregledi.Pregledi[i].Id == o.Id)
                            {
                                FormPregledi.Pregledi.RemoveAt(i);
                                break;
                            }
                    }
                }

            Close();
        }

        private void Zatvori(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
