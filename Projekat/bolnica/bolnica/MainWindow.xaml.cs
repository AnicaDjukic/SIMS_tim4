using bolnica.Forms;
using Bolnica.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace bolnica
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username, password;
            username = txtUser.Text;
            password = txtPassword.Password;
            string[] lines = System.IO.File.ReadAllLines(@"E:\SIMS_tim4\Projekat\bolnica\bolnica\Resources\Users.txt");
            bool found = false;

            foreach (string line in lines)
            {

                string[] fields = line.Split("|");
                string name = fields[0].Split(":")[1];
                string pass = fields[1].Split(":")[1];
                if (username == name && password == pass)
                {
                    string type = fields[2].Split(":")[1];
                    if (type == "upravnik")
                    {
                        var s = new FormUpravnik();
                        s.Show();
                        //this.Close();
                    }
                    else if (type == "sekretar")
                    {
                        var s = new FormSekretar();
                        s.Show();
                        //this.Close();
                    }
                    else if (type == "lekar")
                    {
                        var s = new FormLekar();
                        s.Show();
                        //this.Close();
                    }
                    found = true;
                    break;
                }
            }

            if (!found)
                MessageBox.Show("error");

            Close();

        }
    }
}
