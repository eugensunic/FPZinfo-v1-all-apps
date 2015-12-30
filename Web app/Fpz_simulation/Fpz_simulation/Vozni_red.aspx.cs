using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Resources;

namespace Fpz_simulation
{
    public partial class Vozni_red : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if ((DateTime.Now.TimeOfDay < new TimeSpan(06, 45, 00)) || (DateTime.Now.TimeOfDay > new TimeSpan(21, 00, 00)))// zas sa andom ne funkcionira
                {
                    
                    Response.Write("Linije ne prometuju u ovo vrijeme");
                    Button1.Enabled = false;

                }
                else if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday) { Button1.Enabled = false; Response.Write("Linije ne prometuju vikendom!"); }

                DropDownList2.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                DropDownList3.Items.Insert(0, new ListItem(String.Empty, String.Empty));

                DropDownList2.Items.Add(new ListItem("Caviceva", "1"));
                DropDownList2.Items.Add(new ListItem("Kvaternikov trg", "2"));
                DropDownList2.Items.Add(new ListItem("Kampus", "3"));
                DropDownList3.Items.Add(new ListItem("Caviceva", "1"));
                DropDownList3.Items.Add(new ListItem("Kvaternikov trg", "2"));
                DropDownList3.Items.Add(new ListItem("Kampus", "3"));
                
            }
        }
        double total_seconds1 = 0;

        double total_seconds2 = 0;

        string[] path_names = new string[4] { "Text_File\\Caviceva_Kampus.txt", 
                                              "Text_File\\Kampus_Caviceva.txt",
                                              "Text_File\\Kampus_Kvatric.txt",
                                              "Text_File\\Kvatric_Kampus.txt"};
        List<TimeSpan> lista = new List<TimeSpan>();
        string[] array;
        TimeSpan[] timespan;
        TimeSpan t2;
        public void Metodazabuseve(string path)
        {

            Dtime dt2 = new Dtime();

            try
            {
                if (DateTime.Today.DayOfWeek != DayOfWeek.Saturday || DateTime.Today.DayOfWeek != DayOfWeek.Sunday 
                    || (DateTime.Now.TimeOfDay> new TimeSpan(06,00,00)  && DateTime.Now.TimeOfDay<new TimeSpan(21,00,00)))
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

                               

                            }

                        }
                    }
                    timespan = lista.ToArray();




                    for (int i = 0; i < timespan.Length; i++) 
                    {
                        if (timespan[i] > DateTime.Now.TimeOfDay)
                        {
                            TimeSpan t1 = timespan[i].Subtract(DateTime.Now.TimeOfDay);
                            if (i == timespan.Length - 1) { t2 = new TimeSpan(0, 0, 0); }
                            else { t2 = timespan[i + 1].Subtract(DateTime.Now.TimeOfDay); }

                            total_seconds1 = t1.TotalSeconds;
                            total_seconds2 = t2.TotalSeconds;

                            Label16.Text = t1.ToString(@"mm\:ss");
                            Label17.Text = t2.ToString(@"mm\:ss");

                            break; 
                        }
                    }
                }
                else 
                {
                    Response.Write("NO BUS AVAILABLE AT THIS MOMEMNT !!!!");
                }
            }
            catch (Exception ef)
            {
                Response.Write(ef.ToString());
            }


        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            if (DropDownList2.SelectedValue == "1" && DropDownList3.SelectedValue == "3")// caviceva_kampus [0]
            {
                Label11.Text = "236";
                string z = Server.MapPath("Text_File\\Caviceva_Kampus.txt"); // radi!!!
                Metodazabuseve(z);
            }
            if (DropDownList2.SelectedValue == "3" && DropDownList3.SelectedValue == "1")// kampus caviceva[1]
            {
                Label11.Text = "236";
                Metodazabuseve(Server.MapPath(path_names[1]));
            }
            if (DropDownList2.SelectedValue == "3" && DropDownList3.SelectedValue == "2") // kampus_kvatric[2]
            {
                Label11.Text = "215";
                Metodazabuseve(Server.MapPath(path_names[2]));
            }

            if (DropDownList2.SelectedValue == "2" && DropDownList3.SelectedValue == "3")// kvatric_kampus[3]
            {
                Label11.Text = "215";
                Metodazabuseve(Server.MapPath(path_names[3]));
            }
            if ((DropDownList2.SelectedValue == "1" && DropDownList3.SelectedValue == "1") ||
                 (DropDownList2.SelectedValue == "2" && DropDownList3.SelectedValue == "2") ||
                 (DropDownList2.SelectedValue == "3" && DropDownList3.SelectedValue == "3"))// nedozvoljene kombinacije
            {
                Label16.Text = "mm:ss";
                Label17.Text = "mm:ss";
                Label11.Text = "Nepostojeća linija";
                Response.Write("Forbbiden combination");
            }
           
           
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}