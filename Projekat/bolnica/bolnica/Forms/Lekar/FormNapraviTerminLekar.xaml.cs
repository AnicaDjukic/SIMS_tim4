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
using Model.Pregledi;
using Model.Korisnici;
using Model.Prostorije;
using Model.Pacijenti;
using Bolnica.Validation;
using Bolnica.Model.Pregledi;
using Bolnica.Model.Korisnici;
using Bolnica.Model.Prostorije;
using Bolnica.ViewModel;

namespace Bolnica.Forms
{

    public partial class FormNapraviTerminLekar : Window
    {
        

        public FormNapraviTerminLekar(NapraviTerminLekarViewModel viewModel)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Application.Current.MainWindow;
            this.DataContext = viewModel;
            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(this.Close);
            this.Show();

        }

        

        /*public FormNapraviTerminLekar(Lekar neki,Pacijent pacij)
        {
           
           
            specijalizacije = new List<String>();
            ulogovaniLekar = neki;
            lekariTrenutni = sviLekari.GetAll();
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Application.Current.MainWindow;
            datumB = DateTime.Now;

            this.DataContext = this;

            pacijentiZa = sviPacijenti.GetAll();
            pregledi = sviPregledi.GetAllPregledi();
            operacije = sviPregledi.GetAllOperacije();
            prostorijaZa = sveProstorije.GetAllProstorije();
           

            for (int le = 0; le < lekariTrenutni.Count; le++)
            {
                if (lekariTrenutni[le].Specijalizacija.OblastMedicine != null)
                {
                    string s = lekariTrenutni[le].Prezime + ' ' + lekariTrenutni[le].Ime + ' ' + lekariTrenutni[le].Jmbg;
                    textLekar.Items.Add(s);
                }
            }

            for (int le = 0; le < lekariTrenutni.Count; le++)
            {
                if (lekariTrenutni[le].Specijalizacija.OblastMedicine != null && !specijalizacije.Contains(lekariTrenutni[le].Specijalizacija.OblastMedicine))
                {
                    specijalizacije.Add(lekariTrenutni[le].Specijalizacija.OblastMedicine);
                    textSpecijalizacija.Items.Add(lekariTrenutni[le].Specijalizacija.OblastMedicine);
                }
            }

            for (int vre = 0; vre < 24; vre++)
            {
                for (int min = 0; min < 59;)
                {
                    TimeSpan ts = new TimeSpan(vre, min, 0);
                    min = min + 15;
                    textVreme.Items.Add(ts);
                }

            }

            if (ulogovaniLekar.Specijalizacija.OblastMedicine.Equals("Opsta"))
            {
                checkOperacija.IsEnabled = false;
            }

            trajanjeB = "30";
            textTrajanje.IsEnabled = false;



             
            for (int i = 0; i < pacijentiZa.Count; i++)
            {
                string t;
                t = pacij.Prezime + ' ' + pacij.Ime + ' ' + pacij.Jmbg;
                if (pacijentiZa[i].Obrisan == false)
                {
                    string s;
                    s = pacijentiZa[i].Prezime + ' ' + pacijentiZa[i].Ime + ' ' + pacijentiZa[i].Jmbg;

                    textPrezime.Items.Add(s);
                    if (s.Equals(t))
                    {
                        prezimeB = s;

                    }
                }
            }
            for (int pr = 0; pr < prostorijaZa.Count; pr++)
            {
                if (prostorijaZa[pr].Obrisana == false && prostorijaZa[pr].Zauzeta == false && prostorijaZa[pr].TipProstorije.Equals(TipProstorije.salaZaPreglede))
                {
                    textProstorija.Items.Add(prostorijaZa[pr].BrojProstorije);
                }
            }
           
          
            textPrezime.IsEnabled = false;

        }

        
        */
    }
}
