using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Phone.Net.NetworkInformation;


namespace FPZinfo
{
    public partial class Menza : PhoneApplicationPage
    {
        public Menza()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();
          
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
        private async void getResult()
        {
            string strRequestUri = "http://www.sczg.unizg.hr/prehrana/restorani/zuk-borongaj/";
            string strResult = string.Empty;
            string websource1 = string.Empty;
            try
            {

                HttpClient httpClient = new HttpClient();
                websource1 = httpClient.BaseAddress;

                try
                {
                    // GetStringAsync
                    strResult = await httpClient.GetStringAsync(strRequestUri);

                    // GetByteArrayAsync

                }
                catch (Exception ex)
                {
                    strResult = ex.Message;
                }
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }
            finally
            {

                // Show the result.


                Dispatcher.BeginInvoke(delegate()
                {

                    Stream repo = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(strResult));
                    Generiraj_menu("MENU-MESNI:", listbox1, "MENU-VEGETARIJANSKI", repo, 1);
                    repo.SetLength(0);

                    repo = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(strResult));
                    Generiraj_menu("MENU-VEGETARIJANSKI", listbox2, "GLAVNA JELA", repo, 2);
                    repo.SetLength(0);

                    repo = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(strResult));
                    Generiraj_menu("GLAVNA JELA", listbox3, "PRILO", repo, 3);
                    repo.SetLength(0);

                    repo = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(strResult));
                    Generiraj_menu("PRILO", listbox4, "Normativ", repo, 4);
                    repo.SetLength(0);

                });
            }

        }
        public void Generiraj_menu(string menu, LongListSelector lb, string final_word, Stream websource, int id)
        {

            List<string> items = new List<string>();
            string concatenate = "";
            int position1 = 0;
            int position2 = 0;
            string maindata = "r";
            string read_data = string.Empty;


            StreamReader sr = new StreamReader(websource);
            while (!sr.EndOfStream)
            {
                read_data = sr.ReadLine();

                if (read_data.Contains("<p>") && read_data.Contains("<em>")
                    && read_data.Contains("<strong>") && read_data.Contains(menu))
                {



                    do
                    {
                        maindata = sr.ReadLine();
                        for (int i = 0; i < maindata.Length; i++)
                        {
                            if (maindata[i] == '<' && maindata[i + 1] == 'p' && maindata[i + 2] == '>')
                            {
                                position1 = i + 3;


                            }
                            else if (maindata[i] == '<' && maindata[i + 1] == '/')
                            {
                                position2 = i;
                            }
                        }

                        for (int j = position1; j < position2; j++)
                        {
                            concatenate = concatenate + maindata[j].ToString();


                        }

                        items.Add(concatenate);
                        concatenate = "";
                    } while (!maindata.Contains(final_word));

                    for (int i = 0; i < items.Count; i++)
                    {
                        if (items[i].Contains("<") || items[i].Contains(">"))
                        {
                            items[i] = "";
                        }
                    }
                    if (id == 1 || id == 2)
                    {
                        int counter = 1;
                        for (int i = 0; i < items.Count; i++)
                        {
                            if (items[i] != "" || !string.IsNullOrEmpty(items[i]))
                            {
                                items[i] = counter.ToString() + ". " + items[i];

                            }
                            counter++;


                        }
                    }
                    for (int i = 0; i < items.Count; i++)
                    {
                        if (items[i].Contains('7'))
                        {

                            items[i] = "";
                        }



                    }
                    lb.ItemsSource = items;
                }




            }






        }

     

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (checkNetworkConnection() == true)
            {
                getResult();
            }



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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Kalendar.xaml", UriKind.Relative));
        }

    }
}