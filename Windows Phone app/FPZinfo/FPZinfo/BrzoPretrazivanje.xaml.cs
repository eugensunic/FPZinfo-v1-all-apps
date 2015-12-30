using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using FPZinfo.ServiceReference1;
using System.IO;

namespace FPZinfo
{
    public partial class BrzoPretrazivanje : PhoneApplicationPage
    {
        public BrzoPretrazivanje()
        {

            populacijakonzultacija = new List<string>();
            InitializeComponent();
            BuildLocalizedApplicationBar();
            Service1Client dr = new Service1Client();
            dr.GetNastavnikFromDatabaseAsync();
            dr.GetNastavnikFromDatabaseCompleted += (senderr, ee) => { myautocompletee.FilterMode = AutoCompleteFilterMode.StartsWith; myautocompletee.ItemsSource = ee.Result; };
        }
        List<string> populacijakonzultacija;
        private void BuildLocalizedApplicationBar()
        {
            ApplicationBar = new ApplicationBar();

            ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/home.png", UriKind.Relative));
            appBarButton.Text = "Home";
            appBarButton.Click += appBarButton_Click;
            ApplicationBar.Buttons.Add(appBarButton);

            ApplicationBarIconButton appBarButton2 = new ApplicationBarIconButton(new Uri("/Assets/time.png", UriKind.Relative));
            appBarButton2.Text = "Dvorane";
            appBarButton2.Click += appBarButton_Click2;
            ApplicationBar.Buttons.Add(appBarButton2);

            ApplicationBarIconButton appBarButton3 = new ApplicationBarIconButton(new Uri("/Assets/bus.png", UriKind.Relative));
            appBarButton3.Text = "Zet";
            appBarButton3.Click += appBarButton_Click3;
            ApplicationBar.Buttons.Add(appBarButton3);

            ApplicationBarIconButton appBarButton4 = new ApplicationBarIconButton(new Uri("/Assets/food.png", UriKind.Relative));
            appBarButton4.Text = "Menza";
            appBarButton4.Click += appBarButton_Click4;
            ApplicationBar.Buttons.Add(appBarButton4);

        }
        private void appBarButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void appBarButton_Click2(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Dvorane.xaml", UriKind.Relative));
        }
        private void appBarButton_Click3(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/ZetLinije.xaml", UriKind.Relative));
        }
        private void appBarButton_Click4(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Menza.xaml", UriKind.Relative));
        }
        private string Getsurname(string initialstring)
        {
            return initialstring.Substring(0, initialstring.IndexOf(' ') + 1);
        }

        private string Getfirstname(string initialstring)
        {
            return initialstring.Substring(initialstring.IndexOf(' ') + 1, initialstring.Length - (initialstring.IndexOf(' ') + 1));
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string namee = Getfirstname(myautocompletee.Text);
                string lnamee = Getsurname(myautocompletee.Text);
                Imetxt.Text = Getfirstname(namee);
                Prezimetxt.Text = Getsurname(lnamee);
                Emailtxt.Text = namee.ToLower() + "." + lnamee.ToLower() + "@fpz.hr";
                Service1Client dr2 = new Service1Client();
                dr2.GetDatabaseStringCompleted += (senderr, ee) => { if (ee.Result != null) Sobatxt.Text = ee.Result; };
                dr2.GetDatabaseStringAsync(@"SELECT SOBA.Broj FROM Soba,Nastavnik      
                                            WHERE Soba.ID=Nastavnik.sobaID AND                        
                                            Nastavnik.ime=@ime AND
                                            Nastavnik.prezime=@prezime", namee, lnamee);

                Service1Client dr4 = new Service1Client();
                dr4.GetDatabaseStringCompleted += (senderr, ee) => { if (ee.Result != null)kattxt.Text = ee.Result; };
                dr4.GetDatabaseStringAsync(@"SELECT Soba.Kat FROM Soba,Nastavnik
                                            WHERE soba.ID=Nastavnik.sobaID AND 
                                            Nastavnik.ime=@ime AND
                                            Nastavnik.prezime=@prezime", namee, lnamee);
                Service1Client dr5 = new Service1Client();
                dr5.GetDatabaseStringCompleted += (senderr, ee) => { if (ee.Result != null) Objekttxt.Text = ee.Result; };
                dr5.GetDatabaseStringAsync(@"SELECT Objekt.Naziv FROM Nastavnik,soba,Objekt
                                          WHERE Nastavnik.sobaID=Soba.ID AND soba.ObjektID=Objekt.ID AND
                                          Nastavnik.ime=@ime and Nastavnik.prezime=@prezime", namee, lnamee);
                Service1Client dr6 = new Service1Client();
                dr6.KonzultacijeCompleted += (senderr, ee) =>
                {
                    if (ee.Result != null)
                    {
                        populacijakonzultacija = ee.Result; string[] array = populacijakonzultacija.ToArray();

                        konztxt.Text = array[0].ToString();
                        konztxt2.Text = array[1].ToString();
                    }

                };
                dr6.KonzultacijeAsync(@"SELECT tjedan_nastavnik.Pocetak_konz,tjedan_nastavnik.Kraj_konz,Tjedan.Dan From tjedan_nastavnik,Nastavnik,Tjedan
                                         where Nastavnik.ID=tjedan_nastavnik.Nastavnik_ID and tjedan_nastavnik.TjedanID=Tjedan.ID and
                                         nastavnik.ime=@ime AND Nastavnik.prezime=@prezime", namee, lnamee);
                Service1Client dr7 = new Service1Client();
                dr7.ReadWrite_ImageCompleted += (senderr, ee) =>
                {
                    if (ee.Result.Length != 0)
                    {
                        MemoryStream stream = new MemoryStream(ee.Result);
                        BitmapImage image = new BitmapImage();
                        image.SetSource(stream);
                        Picturebox1.Source = image;
                    }
                    else
                    {
                        string putanja = "/Assets/none.jpg";
                        Uri uriPutanja = new Uri(putanja, UriKind.Relative);
                        BitmapImage fotka = new BitmapImage(uriPutanja);
                        Picturebox1.Source = fotka;
                    }

                };
                dr7.ReadWrite_ImageAsync(namee, lnamee);
                Service1Client dr8 = new Service1Client();
                dr8.Spec_konzultacijeeeeeeCompleted += (senderr, ee) =>
                {
                    if (ee.Result != null) { konzspec.Text = ee.Result; }

                };
                dr8.Spec_konzultacijeeeeeeAsync(@"SELECT tjedan_nastavnik.Pocetak_konzspec,tjedan_nastavnik.Kraj_konzspec,Tjedan.Dan From tjedan_nastavnik,Nastavnik,Tjedan
                                         where Nastavnik.ID=tjedan_nastavnik.Nastavnik_ID and tjedan_nastavnik.TjedanID=Tjedan.ID and
                                         nastavnik.ime=@ime AND Nastavnik.prezime=@prezime", namee, lnamee);

            }




            catch
            {
                MessageBox.Show("Unesite pravilan naziv predmeta");

            }

        }
    }
}