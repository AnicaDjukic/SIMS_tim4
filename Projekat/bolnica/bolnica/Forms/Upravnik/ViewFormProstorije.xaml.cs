using Bolnica.Localization;
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
        public static ObservableCollection<Zaliha> OpremaProstorije
        {
            get;
            set;
        }

        private Injector inject;
        public Injector Inject
        {
            get { return inject; }
            set
            {
                inject = value;
            }
        }
        public ViewFormProstorije(string brojProstorije)
        {
            InitializeComponent();
            DataContext = this;
            Inject = new Injector();
            Title = LocalizedStrings.Instance["Prikaz prostorije"];
            AzurirajSveZalihe();
            PrikaziOpremuProstorije(brojProstorije);
        }

        private void AzurirajSveZalihe()
        {
            List<Zaliha> noveZalihe = NapraviNoveZaliheOdBuducih();
            ZameniStareZaliheNovim(noveZalihe);
        }

        private List<Zaliha> NapraviNoveZaliheOdBuducih()
        {
            List<BuducaZaliha> buduceZalihe = Inject.ControllerBuducaZaliha.DobaviBuduceZaliheIsteklogDatuma();
            List<Zaliha> noveZalihe = Inject.ControllerZaliha.NapraviZaliheOdBuducihZaliha(buduceZalihe);
            Inject.ControllerBuducaZaliha.ObrisiBuduceZalihe(buduceZalihe);
            return noveZalihe;
        }

        private void ZameniStareZaliheNovim(List<Zaliha> noveZalihe)
        {
            foreach (Zaliha z in Inject.ControllerZaliha.DobaviZalihe())
            {
                foreach (Zaliha nz in noveZalihe)
                {
                    if (z.Oprema.Sifra == nz.Oprema.Sifra)
                        Inject.ControllerZaliha.ObrisiZalihu(z);
                }
            }
            Inject.ControllerZaliha.SacuvajZalihe(noveZalihe);
        }

        private void PrikaziOpremuProstorije(string brojProstorije)
        {
            OpremaProstorije = new ObservableCollection<Zaliha>();
            foreach (Zaliha z in Inject.ControllerZaliha.DobaviZaliheProstorije(brojProstorije))
            {
                OpremaProstorije.Add(z);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
