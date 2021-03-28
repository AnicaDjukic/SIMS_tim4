using bolnica.Forms;
using Bolnica.Forms;
using Model.Korisnici;
using Model.Pacijenti;
using System;
using System.Collections.Generic;
using System.IO;
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
        private FileStoragePacijenti storage = new FileStoragePacijenti();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username, password;
            username = txtUser.Text;
            password = txtPassword.Password;
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string fileLocation = System.IO.Path.Combine(path, @"Resources\", "Users.txt");
            string[] lines = System.IO.File.ReadAllLines(fileLocation);
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
                    else if (type == "pacijent")
                    {
                        List<Pacijent> pacijenti = storage.GetAll();
                        Pacijent pac = new Pacijent();
                        foreach (Pacijent p in pacijenti)
                        {
                            if (p.KorisnickoIme.Equals(username) && p.Lozinka.Equals(password))
                            {
                                pac = p;
                                break;
                            }
                        }
                        var s = new FormPacijent(pac);
                        s.Show();
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
