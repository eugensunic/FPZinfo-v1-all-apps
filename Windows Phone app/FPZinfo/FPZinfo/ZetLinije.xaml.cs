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
using System.Windows.Threading;
using System.Threading.Tasks;


namespace FPZinfo
{
    public partial class ZetLinije : PhoneApplicationPage
    {
        public ZetLinije()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();
            listchekbox = new CheckBox[] { Check1, Check2, Check3, Check4 };
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += onclick;
            dan1.Text = Dani_tjedan(DateTime.Now.DayOfWeek.ToString());
            datum1.Text = DateTime.Now.ToShortDateString();

            if ((DateTime.Now.TimeOfDay < new TimeSpan(06, 45, 00)) || (DateTime.Now.TimeOfDay > new TimeSpan(20, 00, 00)))// zas sa andom ne funkcionira
            {
                potvrda.IsEnabled = false;
                zaustavi.IsEnabled = false;
                Check1.IsEnabled = false;
                Check2.IsEnabled = false;
                Check3.IsEnabled = false;
                Check4.IsEnabled = false;
                MessageBox.Show("Linije ne prometuju u ovo vrijeme");


            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                potvrda.IsEnabled = false;
                zaustavi.IsEnabled = false;
                Check1.IsEnabled = false;
                Check2.IsEnabled = false;
                Check3.IsEnabled = false;
                Check4.IsEnabled = false; MessageBox.Show("Linije ne prometuju vikendom!");
            }



        }

        DispatcherTimer timer;
        List<TimeSpan> lista = new List<TimeSpan>();
        string[] array;
        TimeSpan[] timespan;
        TimeSpan t2;
        TimeSpan t1;
        double total_seconds1 = 0;

        double total_seconds2 = 0;

        string[] path_names = new string[4] { "Zet_autobus/Caviceva_Kampus.txt", 
                                              "Zet_autobus/Kampus_Caviceva.txt",
                                              "Zet_autobus/Kampus_Kvatric.txt",
                                             "Zet_autobus/Kvatric_Kampus.txt"};


        public void Metodazabuseve(string path)
        {
            lista.Clear();
            bus1.Text = "";
            bus2.Text = "";
            Dtime dt2 = new Dtime();

            try
            {

                var str1 = Application.GetResourceStream(new Uri(path, UriKind.Relative));


                using (StreamReader str = new StreamReader(path))
                {

                    while (!str.EndOfStream)
                    {

                        string z = str.ReadLine();
                        if (!z.Contains("-"))
                        {
                            array = z.Split(';');
                            timespan = new TimeSpan[array.Length];
                            for (int i = 0; i < array.Length; i++)
                            {
                                timespan[i] = new TimeSpan(dt2.GetHours(array[i]), dt2.GetMinutes(array[i]), 0);

                            }

                            lista.AddRange((IEnumerable<TimeSpan>)timespan);

                        }

                    }

                }
                timespan = lista.ToArray();




                for (int i = 0; i < timespan.Length; i++)
                {
                    if (timespan[i] > DateTime.Now.TimeOfDay)
                    {
                        t1 = timespan[i].Subtract(DateTime.Now.TimeOfDay);
                        if (i == timespan.Length - 1) { t2 = new TimeSpan(0, 0, 0); }
                        else { t2 = timespan[i + 1].Subtract(DateTime.Now.TimeOfDay); }

                        total_seconds1 = t1.TotalSeconds;
                        total_seconds2 = t2.TotalSeconds;

                        bus1.Text = t1.ToString(@"mm\:ss");
                        bus2.Text = t2.ToString(@"mm\:ss");

                        break;
                    }
                }



            }
            catch (Exception ef)
            {
                MessageBox.Show("Doslo je do pogreške!" + ef);
            }


        }
        CheckBox[] listchekbox;

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            timer.Stop();
            bus1.Text = "";
            bus2.Text = "";
            linija.Text = "";
            progress1.Value = 0;
            progress2.Value = 0;

            if (Onlyoneclick(listchekbox, 0))
            {
                Metodazabuseve(path_names[0]); timer.Start(); linija.Text = "236";
                string minute = bus1.Text.Substring(0, 2);
                string sekunde = bus1.Text.Substring(3, 2);
                int minute1 = Convert.ToInt32(minute);
                int sekunde1 = Convert.ToInt32(sekunde);

                string minute2 = bus2.Text.Substring(0, 2);
                string sekunde2 = bus2.Text.Substring(3, 2);
                int minute22 = Convert.ToInt32(minute2);
                int sekunde22 = Convert.ToInt32(sekunde2);

                progress1.Maximum = (int)(minute1 * 60) + (int)sekunde1;
                progress2.Maximum = (int)(minute22 * 60) + (int)sekunde22;
            }
            else if (Onlyoneclick(listchekbox, 1))
            {
                Metodazabuseve(path_names[1]); timer.Start(); linija.Text = "236";
                string minute = bus1.Text.Substring(0, 2);
                string sekunde = bus1.Text.Substring(3, 2);
                int minute1 = Convert.ToInt32(minute);
                int sekunde1 = Convert.ToInt32(sekunde);

                string minute2 = bus2.Text.Substring(0, 2);
                string sekunde2 = bus2.Text.Substring(3, 2);
                int minute22 = Convert.ToInt32(minute2);
                int sekunde22 = Convert.ToInt32(sekunde2);

                progress1.Maximum = (int)(minute1 * 60) + (int)sekunde1;
                progress2.Maximum = (int)(minute22 * 60) + (int)sekunde22;
            }
            else if (Onlyoneclick(listchekbox, 2))
            {
                Metodazabuseve(path_names[2]); timer.Start(); linija.Text = "215";
                string minute = bus1.Text.Substring(0, 2);
                string sekunde = bus1.Text.Substring(3, 2);
                int minute1 = Convert.ToInt32(minute);
                int sekunde1 = Convert.ToInt32(sekunde);

                string minute2 = bus2.Text.Substring(0, 2);
                string sekunde2 = bus2.Text.Substring(3, 2);
                int minute22 = Convert.ToInt32(minute2);
                int sekunde22 = Convert.ToInt32(sekunde2);

                progress1.Maximum = (int)(minute1 * 60) + (int)sekunde1;
                progress2.Maximum = (int)(minute22 * 60) + (int)sekunde22;
            }
            else if (Onlyoneclick(listchekbox, 3))
            {
                Metodazabuseve(path_names[3]); timer.Start(); linija.Text = "215";
                string minute = bus1.Text.Substring(0, 2);
                string sekunde = bus1.Text.Substring(3, 2);
                int minute1 = Convert.ToInt32(minute);
                int sekunde1 = Convert.ToInt32(sekunde);

                string minute2 = bus2.Text.Substring(0, 2);
                string sekunde2 = bus2.Text.Substring(3, 2);
                int minute22 = Convert.ToInt32(minute2);
                int sekunde22 = Convert.ToInt32(sekunde2);

                progress1.Maximum = (int)(minute1 * 60) + (int)sekunde1;
                progress2.Maximum = (int)(minute22 * 60) + (int)sekunde22;
            }
            else if (Check1.IsChecked == false && Check2.IsChecked == false && Check3.IsChecked == false && Check4.IsChecked == false) { MessageBox.Show("Izaberite jednu rutu"); }
            else { MessageBox.Show("Greška, odaberite isključivo jednu rutu!"); }






        }

        private void onclick(object sender, EventArgs e)
        {
            if ((DateTime.Today.DayOfWeek != DayOfWeek.Saturday || DateTime.Today.DayOfWeek != DayOfWeek.Sunday)
                  && (DateTime.Now.TimeOfDay > new TimeSpan(06, 00, 00) && DateTime.Now.TimeOfDay < new TimeSpan(21, 00, 00)))
            {
                progress1.Value = progress1.Value + 1;
                progress2.Value += 1;
                Countdown(bus1);

                Countdown(bus2);
            }
        }
        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
        private bool Onlyoneclick(CheckBox[] itemselected, int id)
        {

            int itemcounter = 0;
            int falsecounter = 0;
            for (int i = 0; i < listchekbox.Length; i++)
            {
                if (itemselected[i].IsChecked == true && id == i)
                {
                    itemcounter++;
                }
                else if (itemselected[i].IsChecked == true) { falsecounter++; }
            }
            if (itemcounter == 1 && falsecounter == 0) { return true; }
            else { return false; }

        }
        public void Countdown(TextBlock tx)
        {

            int minute1;
            int sekunde1;
            string minute = tx.Text.Substring(0, 2);
            string sekunde = tx.Text.Substring(3, 2);
            bool res = int.TryParse(minute, out minute1);
            bool res1 = int.TryParse(sekunde, out sekunde1);

            if (res == true && res1 == true)
            {
                sekunde1 = sekunde1 - 1;
                if (sekunde1 < 0)
                {
                    minute1 = minute1 - 1;
                    sekunde1 = 59;

                }
                if (minute1 == 0 && sekunde1 == 0)
                {



                    MessageBox.Show("Autobus polazi!");
                    tx.Text = "Bus je krenuo!!!";
                    timer.Stop();
                    tx.Text = "00:00";
                }
                if (sekunde1 < 10) { tx.Text = minute1.ToString() + ":0" + sekunde1.ToString(); }
                else { tx.Text = minute1.ToString() + ":" + sekunde1.ToString(); }

                if (minute1 < 10) { tx.Text = "0" + minute1.ToString() + ":" + sekunde1.ToString(); }

                if (minute1 < 10 && sekunde1 < 10) { tx.Text = "0" + minute1.ToString() + ":0" + sekunde1.ToString(); }
            }
            else { MessageBox.Show("Doslo je do greške, pokušajte ponovo"); }

        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            timer.Stop();

            foreach (CheckBox c in listchekbox)
            {
                c.IsChecked = false;
            }
            bus1.Text = "";
            bus2.Text = "";
            linija.Text = "";
            progress1.Value = 0;
            progress2.Value = 0;
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
        private string Dani_tjedan(string dan)
        {
            dan = dan.Trim();
            string broj;
            switch (dan)
            {
                case "Monday":
                    broj = "Ponedjeljak";
                    break;

                case "Tuesday":
                    broj = "Utorak";
                    break;

                case "Wednesday":
                    broj = "Srijeda";
                    break;

                case "Thursday":
                    broj = "Četvrtak";
                    break;

                case "Friday":
                    broj = "Petak";
                    break;

                default:
                    broj = "Ne vozi vikendom";
                    break;

            }

            return broj;
        }
    }

}