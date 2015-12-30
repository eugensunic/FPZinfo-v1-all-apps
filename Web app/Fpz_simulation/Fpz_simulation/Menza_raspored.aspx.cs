using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;

namespace Fpz_simulation
{
    public partial class Menza_raspored : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if ((DateTime.Now.TimeOfDay < new TimeSpan(06, 45, 00)) || (DateTime.Now.TimeOfDay > new TimeSpan(16, 00, 00)))// zas sa andom ne funkcionira
            {

                Response.Write("Menza ne radi u ovo vrijeme");
                Button1.Enabled = false;

            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday) { Response.Write("Menza ne radi vikendom"); Button1.Enabled = false; }
            else if (DateTime.Now.TimeOfDay > new TimeSpan(06, 46, 00) || DateTime.Now.TimeOfDay < new TimeSpan(09, 45, 00))
            { Response.Write("Jelovnik nije ažuriran još"); Button1.Enabled = false; }
            ListBox1.Items.Clear();
            ListBox2.Items.Clear();
            ListBox3.Items.Clear();
            ListBox4.Items.Clear();
        }
        public void Generiraj_menu(string menu, ListBox lb, string final_word)
        {

            List<string> items = new List<string>();
            string concatenate = "";
            int position1 = 0;
            int position2 = 0;
            string maindata = "r";
            string read_data = string.Empty;
            WebRequest req = WebRequest.Create("http://www.sczg.unizg.hr/prehrana/restorani/zuk-borongaj/");
            WebResponse res = req.GetResponse();
            StreamReader sr = new StreamReader(res.GetResponseStream());
            while (!sr.EndOfStream)
            {
                read_data = sr.ReadLine();

                if (read_data.Contains("<p>") && read_data.Contains("<em>")
                    && read_data.Contains("<strong>") && read_data.Contains(menu))// MENU-MESNI
                {



                    do
                    {
                        maindata = sr.ReadLine();
                        for (int i = 0; i < maindata.Length; i++)
                        {
                            if (maindata[i] == '<' && maindata[i + 1] == 'p' && maindata[i + 2] == '>') //START POSITION FOR INNER TEXT
                           {
                                position1 = i + 3;


                            }
                            else if (maindata[i] == '<' && maindata[i + 1] == '/') //END POSITION FOR INNER TEXT
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
                    foreach (var item in items)
                    {
                        lb.Items.Add(item);
                    }
                }





            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Generiraj_menu("PRILO", ListBox4, "Normativ");
            Generiraj_menu("MENU-MESNI:", ListBox1, "MENU-VEGETARIJANSKI");
            Generiraj_menu("GLAVNA JELA", ListBox3, "PRILO");
            Generiraj_menu("MENU-VEGETARIJANSKI", ListBox2, "GLAVNA JELA");
        }// zavrsetak public metode!
    }
}