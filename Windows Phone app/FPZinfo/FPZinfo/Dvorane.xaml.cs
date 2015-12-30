using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Net.NetworkInformation;

namespace FPZinfo
{
    public partial class Dvorane : PhoneApplicationPage
    {
        public Dvorane()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();
            if (checkNetworkConnection() == false) { MessageBox.Show("Otvorite internet vezu"); }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday) { MessageBox.Show("Fakultet ne radi vikendom"); generacija.IsEnabled = false; }
            else if ((DateTime.Now.TimeOfDay < new TimeSpan(07, 59, 00)) || (DateTime.Now.TimeOfDay > new TimeSpan(20, 01, 00)))
            {

                MessageBox.Show("Dnevni raspored još nije počeo");
                generacija.IsEnabled = false;

            }
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





        #region
        List<string> vukeliceva_lista = new List<string>();
        List<string> objekt69_lista = new List<string>();
        List<string> objekt70_lista = new List<string>();
        List<string> objekt71_lista = new List<string>();
        List<string> listajj = new List<string>();

        #endregion






        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (checkNetworkConnection() == true)
            {
                ServiceReference1.Service1Client ldr = new ServiceReference1.Service1Client();
                ldr.GetAvailableRoomCompleted += (senderr, ee) =>
                {
                    if (ee.Result != null)
                    {
                        listajj = ee.Result;
                        for (int i = 0; i < listajj.Count; i++)
                        {
                            if (listajj[i].Contains("69")) { objekt69_lista.Add(listajj[i]); }
                            else if (listajj[i].Contains("70")) { objekt70_lista.Add(listajj[i]); }
                            else if (listajj[i].Contains("71")) { objekt71_lista.Add(listajj[i]); }
                            else if (listajj[i].Contains("4")) { vukeliceva_lista.Add(listajj[i]); }
                        }
                        listbox2.ItemsSource = objekt69_lista;
                        listbox3.ItemsSource = objekt70_lista;
                        listbox4.ItemsSource = objekt71_lista;
                        listbox1.ItemsSource = vukeliceva_lista;
                    }
                    else { MessageBox.Show("SVE DVORANE SU ZAUZETE!"); }
                };
                ldr.GetAvailableRoomAsync();
            }
            else { MessageBox.Show("Otvorite internet vezu"); }
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




    }
}

