using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Bolnica.Forms
{
    /// <summary>
    /// Interaction logic for FormObavestenjaPacijentPage.xaml
    /// </summary>
    public partial class FormObavestenjaPacijentPage : Page
    {
        public static ObservableCollection<string> ObavestenjaZaPacijente
        {
            get;
            set;
        }
        public FormObavestenjaPacijentPage()
        {
            InitializeComponent();

            this.DataContext = this;
        }
    }
}
