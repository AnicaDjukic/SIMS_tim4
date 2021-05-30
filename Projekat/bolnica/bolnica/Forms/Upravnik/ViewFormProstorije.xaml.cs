using Bolnica.Model.Prostorije;
using Bolnica.Services.Prostorije;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for ViewFormProstorije.xaml
    /// </summary>
    public partial class ViewFormProstorije : Window
    {
        public static ObservableCollection<Zaliha> OpremaSobe
        {
            get;
            set;
        }

        private ServiceZaliha serviceZaliha = new ServiceZaliha();
        private ServiceBuducaZaliha serviceBuducaZaliha = new ServiceBuducaZaliha();
        private ServiceOprema serviceOprema = new ServiceOprema();
        public ViewFormProstorije(string brojProstorije)
        {
            InitializeComponent();
            this.DataContext = this;
            OpremaSobe = new ObservableCollection<Zaliha>();
            AzurirajSveZalihe();
            PrikaziOpremuProstorije(brojProstorije);
        }

        private void AzurirajSveZalihe()
        {
            FileStorageBuducaZaliha storageBuducaZaliha = new FileStorageBuducaZaliha();
            List<Zaliha> noveZalihe = new List<Zaliha>();
            noveZalihe = NapraviNoveZaliheOdBuducih();
            ZameniStareZaliheNovim(noveZalihe);
        }

        private List<Zaliha> NapraviNoveZaliheOdBuducih()
        {
            List<BuducaZaliha> buduceZalihe = serviceBuducaZaliha.DobaviBuduceZaliheIsteklogDatuma();
            List<Zaliha> noveZalihe = serviceZaliha.NapraviZaliheOdBuducihZaliha(buduceZalihe);
            serviceBuducaZaliha.ObrisiBuduceZalihe(buduceZalihe);
            return noveZalihe;
        }

        private void ZameniStareZaliheNovim(List<Zaliha> noveZalihe)
        {
            if (serviceZaliha.DobaviZalihe() != null)
            {
                foreach (Zaliha z in serviceZaliha.DobaviZalihe())
                {
                    foreach (Zaliha nz in noveZalihe)
                    {
                        if (z.Oprema.Sifra == nz.Oprema.Sifra)
                        {
                            serviceZaliha.ObrisiZalihu(z);

                        }
                    }
                }
            }

            serviceZaliha.SacuvajZalihe(noveZalihe);

        }

        private void PrikaziOpremuProstorije(string brojProstorije)
        {
            foreach (Zaliha zaliha in serviceZaliha.DobaviZalihe())
            {
                if (zaliha.Prostorija.BrojProstorije == brojProstorije)
                {
                    zaliha.Oprema = serviceOprema.DobaviOpremu(zaliha.Oprema.Sifra);
                    OpremaSobe.Add(zaliha);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
