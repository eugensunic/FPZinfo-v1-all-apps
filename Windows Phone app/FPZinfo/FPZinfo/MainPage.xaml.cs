using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using FPZinfo.Resources;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.IO;
using Microsoft.Phone.Net.NetworkInformation;
using FPZinfo.ServiceReference1;

namespace FPZinfo
{
    public partial class MainPage : PhoneApplicationPage
    {






        List<string> polje = new List<string>();
        string[] array;
        public MainPage()
        {

            InitializeComponent();
            BuildLocalizedApplicationBar();
            if (checkNetworkConnection() == false) { MessageBox.Show("Otvorite internet vezu"); }

            Service1Client drd = new Service1Client();

            drd.GetPredmetFromDatabaseCompleted += (senderr, ee) => { myautocomplete.FilterMode = AutoCompleteFilterMode.StartsWith; myautocomplete.ItemsSource = ee.Result; };
            drd.GetPredmetFromDatabaseAsync();

        }


        public static bool checkNetworkConnection()
        {
            var ni = NetworkInterface.NetworkInterfaceType;

            bool IsConnected = false;
            if ((ni == NetworkInterfaceType.Wireless80211) || (ni == NetworkInterfaceType.MobileBroadbandCdma) || (ni == NetworkInterfaceType.MobileBroadbandGsm))
                IsConnected = true;
            else if (ni == NetworkInterfaceType.None)
                IsConnected = false;
            return IsConnected;
        }
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

        private void MyLongListSelector2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<UserControl> listItems = new List<UserControl>();
            GetItemsRecursive<UserControl>(MyLongListSelector2, ref listItems);


            if (e.AddedItems.Count > 0 && e.AddedItems[0] != null)
            {
                foreach (UserControl userControl in listItems)
                {
                    if (e.AddedItems[0].Equals(userControl.DataContext))
                    {
                        VisualStateManager.GoToState(userControl, "Selected", true);
                    }
                }
            }

            if (e.RemovedItems.Count > 0 && e.RemovedItems[0] != null)
            {
                foreach (UserControl userControl in listItems)
                {
                    if (e.RemovedItems[0].Equals(userControl.DataContext))
                    {
                        VisualStateManager.GoToState(userControl, "Normal", true);
                    }
                }
            }
            try
            {


                string namee = Getfirstname(MyLongListSelector2.SelectedItem.ToString());
                string lnamee = Getsurname(MyLongListSelector2.SelectedItem.ToString());
                Imetxt.Text = Getfirstname(namee);
                Prezimetxt.Text = Getsurname(lnamee);
                Emailtxt.Text = namee.ToLower() + "." + lnamee.ToLower() + "@fpz.hr";
                Service1Client dr2 = new Service1Client();
                dr2.GetDatabaseStringAsync(@"SELECT SOBA.Broj FROM Soba,Nastavnik      
                                            WHERE Soba.ID=Nastavnik.sobaID AND                        
                                            Nastavnik.ime=@ime AND
                                            Nastavnik.prezime=@prezime", namee, lnamee);
                dr2.GetDatabaseStringCompleted += (senderr, ee) => { if (ee.Result != null) { Sobatxt.Text = ee.Result; } };


                Service1Client dr4 = new Service1Client();
                dr4.GetDatabaseStringAsync(@"SELECT Soba.Kat FROM Soba,Nastavnik
                                            WHERE soba.ID=Nastavnik.sobaID AND 
                                            Nastavnik.ime=@ime AND
                                            Nastavnik.prezime=@prezime", namee, lnamee);
                dr4.GetDatabaseStringCompleted += (senderr, ee) => { if (ee.Result != null)kattxt.Text = ee.Result; };

                Service1Client dr5 = new Service1Client();
                dr5.GetDatabaseStringAsync(@"SELECT Objekt.Naziv FROM Nastavnik,soba,Objekt
                                          WHERE Nastavnik.sobaID=Soba.ID AND soba.ObjektID=Objekt.ID AND
                                          Nastavnik.ime=@ime and Nastavnik.prezime=@prezime", namee, lnamee);
                dr5.GetDatabaseStringCompleted += (senderr, ee) => { if (ee.Result != null)Objekttxt.Text = ee.Result; };

                Service1Client dr6 = new Service1Client();
                dr6.KonzultacijeCompleted += (senderr, ee) =>
                {
                    if (ee.Result != null)
                    {
                        List<string> popul = new List<string>();
                        popul = ee.Result;
                        array = popul.ToArray();

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
                    if (ee.Result != null) { speckonz.Text = ee.Result; }
                    else { speckonz.Text = "Nema podataka"; }

                };
                dr8.Spec_konzultacijeeeeeeAsync(@"SELECT tjedan_nastavnik.Pocetak_konzspec,tjedan_nastavnik.Kraj_konzspec From tjedan_nastavnik,Nastavnik,Tjedan
                                         where Nastavnik.ID=tjedan_nastavnik.Nastavnik_ID and tjedan_nastavnik.TjedanID=Tjedan.ID and
                                         nastavnik.ime=@ime AND Nastavnik.prezime=@prezime", namee, lnamee);

                Service1Client d11 = new Service1Client();
                d11.Nazocnost_profesoraCompleted += (senderr, ee) =>
                {

                    bool b = ee.Result;
                    if (b == true) { cancan.Background = new SolidColorBrush(Color.FromArgb(255, 5, 245, 29)); }
                    else { cancan.Background = new SolidColorBrush(Color.FromArgb(255, 246, 5, 38)); }



                };
                d11.Nazocnost_profesoraAsync(namee, lnamee);


            }
            catch
            {
                MessageBox.Show("Unesite pravilan naziv predmeta");

            }


        }

        public static void GetItemsRecursive<T>(DependencyObject parents, ref List<T> objectList) where T : DependencyObject
        {
            var childrenCount = VisualTreeHelper.GetChildrenCount(parents);

            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parents, i);

                if (child is T)
                {
                    objectList.Add(child as T);
                }

                GetItemsRecursive<T>(child, ref objectList);
            }

            return;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Service1Client dr9 = new Service1Client();

            dr9.PopulacijasrodnihprofesoraCompleted += (senderr, ee) =>
            {
                MyLongListSelector2.ItemsSource = ee.Result;
                Emailtxt.Text = ""; kattxt.Text = ""; speckonz.Text = ""; Imetxt.Text = ""; Prezimetxt.Text = ""; konztxt.Text = ""; konztxt2.Text = ""; Sobatxt.Text = ""; Objekttxt.Text = "";
            };
            dr9.PopulacijasrodnihprofesoraAsync(myautocomplete.Text.Trim());
        }
        private string Getsurname(string initialstring)
        {
            return initialstring.Substring(0, initialstring.IndexOf(' ') + 1);


        }

        private string Getfirstname(string initialstring)
        {

            return initialstring.Substring(initialstring.IndexOf(' ') + 1, initialstring.Length - (initialstring.IndexOf(' ') + 1));
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
        private void appBarButton_Click5(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Kalendar.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/BrzoPretrazivanje.xaml", UriKind.Relative));
        }
    }
}