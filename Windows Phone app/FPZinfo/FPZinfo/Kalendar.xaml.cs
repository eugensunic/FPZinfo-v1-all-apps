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

namespace FPZinfo
{
    public partial class Raspored : PhoneApplicationPage
    {
        public Raspored()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string putanja = "/Assets/akademski.png";
            Uri uriPutanja1 = new Uri(putanja, UriKind.Relative);
            BitmapImage fotka = new BitmapImage(uriPutanja1);
            image1.Source = fotka;
        }
    }
}