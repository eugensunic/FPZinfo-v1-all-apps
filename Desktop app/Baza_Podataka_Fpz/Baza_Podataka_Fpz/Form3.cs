using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Baza_Podataka_Fpz
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e) // busevi 
        {
           
           
        }
        double total_seconds1 = 0;
        double total_seconds2 = 0;
        int combobox_index1 = 0;
        int combobox_index2 = 0;
        string[] path_names = new string[4] { @"C:\Users\eugen\Desktop\C# projects\Baza_Podataka_Fpz\Baza_Podataka_Fpz\bin\Debug\Kampus_Caviceva.txt", 
                                              @"C:\Users\eugen\Desktop\C# projects\Baza_Podataka_Fpz\Baza_Podataka_Fpz\bin\Debug\Caviceva_Kampus.txt",
                                              @"C:\Users\eugen\Desktop\C# projects\Baza_Podataka_Fpz\Baza_Podataka_Fpz\bin\Debug\Kampus_Kvatric.txt",
                                              @"C:\Users\eugen\Desktop\C# projects\Baza_Podataka_Fpz\Baza_Podataka_Fpz\bin\Debug\Kvatric_Kampus.txt"};
        List<TimeSpan> lista = new List<TimeSpan>();
        string[] array;
        TimeSpan[] timespan;

        public void Metodazabuseve(string path)
        {
            Dtime dt2 = new Dtime();
            
            try
            {
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
              

            }
            catch (Exception ef) { MessageBox.Show(ef.ToString() + " No path found"); }
            // moras postaviti if uvjet ali moras ocitati zadnji sat i prvi sat pa na temleju njih sutra!
            for (int i = 0; i < timespan.Length; i++) // do 20:00h funkcionira.
            {
                if (timespan[i] > DateTime.Now.TimeOfDay)
                {
                    TimeSpan t1 = timespan[i].Subtract(DateTime.Now.TimeOfDay);
                    TimeSpan t2 = timespan[i + 1].Subtract(DateTime.Now.TimeOfDay);

                    total_seconds1 = t1.TotalSeconds;
                    total_seconds2 = t2.TotalSeconds;
                    
                    label16.Text = t1.ToString(@"mm\:ss");
                    label15.Text = t2.ToString(@"mm\:ss");
                   
                    break; //nema potrebe za dalnjim loopanjem
                }
            }
            timer1.Start();

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            if (combobox_index1 == combobox_index2)
            {
                MessageBox.Show("Linija nemoze prometovati s jedne lokacije na istu lokaciju");
                timer1.Stop();
                label9.Text = "------";
            }
            else
            {
                if (combobox_index1 == 0 && combobox_index2 == 2)
                {
                    Metodazabuseve(path_names[0]);
                    label9.Text = "236";
                }
                else if (combobox_index1 == 2 && combobox_index2 == 0)
                {
                    Metodazabuseve(path_names[1]);
                    label9.Text = "236";
                }
                else if (combobox_index1 == 1 && combobox_index2 == 0)
                {
                    Metodazabuseve(path_names[3]);
                    label9.Text = "215";
                }
                else
                {
                    Metodazabuseve(path_names[2]);
                    label9.Text = "215";
                }
            }
            
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            timer1.Stop();
            label16.Text = "00:00";
            label15.Text = "00:00";
            combobox_index1 = comboBox1.SelectedIndex;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            timer1.Stop();
            label16.Text = "00:00";
            label15.Text = "00:00";
            combobox_index2 = comboBox2.SelectedIndex;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            button1.Click += button1_Click;  // s lamdama...
            button1_Click(this, new EventArgs());
            
        }
    }
}
