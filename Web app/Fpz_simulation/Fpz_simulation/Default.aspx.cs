using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Fpz_simulation
{
    public partial class Index : System.Web.UI.Page
    {
        enum Gender
        {
            Male,
            Female,
            Unknown

        };
        private static Gender Value { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {


            }
        }
        [System.Web.Script.Services.ScriptMethodAttribute()]
        [WebMethod]
        public static List<string> GetCity(string prefixText)
        {
            DataTable dt = new DataTable();
            string constr = "workstation id=Fakultet.mssql.somee.com;packet size=4096;user id=marko78087808_SQLLogin_1;pwd=cejbhg7ats;data source=Fakultet.mssql.somee.com;persist security info=False;initial catalog=Fakultet";
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT Predmet.Naziv From Predmet WHERE Predmet.Naziv LIKE @City+'%'", con);
            cmd.Parameters.AddWithValue("City", prefixText);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            List<string> Nastavnik = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Nastavnik.Add(dt.Rows[i][0].ToString());
            }
            con.Close();
            return Nastavnik;
        }
        protected void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Image1.Visible = true;
            Label10.Text = Getsurname(ListBox2.SelectedItem.ToString());
            Label11.Text = Getfirstname(ListBox2.SelectedItem.ToString());



            Label12.Text = GetDatabaseString(@"SELECT Nastavnik.email FROM Nastavnik      
                                                WHERE Nastavnik.prezime=@prezime AND 
                                                Nastavnik.ime=@ime");



            Label13.Text = GetDatabaseString(@"SELECT Nastavnik.Titula FROM Nastavnik
                                            WHERE Nastavnik.ime=@ime AND 
                                            Nastavnik.prezime=@prezime");

            string objekt = GetDatabaseString(@"SELECT Objekt.Naziv FROM Nastavnik,soba,Objekt
                                          WHERE Nastavnik.sobaID=Soba.ID AND soba.ObjektID=Objekt.ID AND
                                          Nastavnik.ime=@ime and Nastavnik.prezime=@prezime");



            Label14.Text = GetDatabaseString(@"SELECT SOBA.Broj FROM Soba,Nastavnik      
                                            WHERE Soba.ID=Nastavnik.sobaID AND                        
                                            Nastavnik.ime=@ime AND
                                            Nastavnik.prezime=@prezime") + " " + objekt;

            Label15.Text = GetDatabaseString(@"SELECT Soba.Kat FROM Soba,Nastavnik
                                            WHERE soba.ID=Nastavnik.sobaID AND 
                                            Nastavnik.ime=@ime AND
                                            Nastavnik.prezime=@prezime");

            Konzultacije(@"SELECT tjedan_nastavnik.Pocetak_konz,tjedan_nastavnik.Kraj_konz,Tjedan.Dan From tjedan_nastavnik,Nastavnik,Tjedan
                                         where Nastavnik.ID=tjedan_nastavnik.Nastavnik_ID and tjedan_nastavnik.TjedanID=Tjedan.ID and
                                         nastavnik.ime=@ime AND Nastavnik.prezime=@prezime");

            ReadWrite_Image(Getfirstname(ListBox2.SelectedItem.ToString()), Getsurname(ListBox2.SelectedItem.ToString()));



            if (Nazocnost_profesora() == true)
            {
                Label18.Text = "Nazocan";

                if (Value == Gender.Male)
                {
                    Image1.ImageUrl = "~/Images/male_available.png";

                }

                if (Value == Gender.Female)
                {
                    Image1.ImageUrl = "~/Images/female_available.png";
                }


            }

            else if (Nazocnost_profesora() == false)
            {
                Image1.ImageUrl = "~/Images/unavailable.png";
                Label18.Text = "Nenazocan";
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ListBox2.Items.Clear();

            string ime_prezime = "";

            try
            {
                using (SqlConnection con = new SqlConnection(@"workstation id=Fakultet.mssql.somee.com;packet size=4096;user id=marko78087808_SQLLogin_1;pwd=cejbhg7ats;data source=Fakultet.mssql.somee.com;persist security info=False;initial catalog=Fakultet"))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(@"SELECT Nastavnik.ime,Nastavnik.prezime 
                          FROM Nastavnik,Predmet,Nastavnik_Predmet
                          WHERE Nastavnik.ID=Nastavnik_Predmet.NastavnikID AND
                          Nastavnik_Predmet.PredmetID=Predmet.ID AND
                          Predmet.Naziv=@Predmet_Box1", con))
                    {
                        cmd.Parameters.Add(new SqlParameter("Predmet_Box1", TextBox1.Text));
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {


                                    ime_prezime = dr[1].ToString().Trim() + " " + dr[0].ToString().Trim();
                                    ListBox2.Items.Add(ime_prezime);
                                    ime_prezime = "";
                                }
                            }
                            else
                            {
                                Response.Write("Error");
                            }
                        }
                    }


                }


            }
            catch (Exception t) { Response.Write(t.ToString()); }
        }
        private string Getsurname(string initialstring) // sa lamdom
        {
            return initialstring.Substring(0, initialstring.IndexOf(' '));


        }

        private string Getfirstname(string initialstring) // sa lamdom
        {

            return initialstring.Substring(initialstring.IndexOf(' ') + 1, initialstring.Length - (initialstring.IndexOf(' ') + 1));


        }
        private string GetDatabaseString(string pattern)  // General mehtods for strings
        {
            string dohvat_podatka = "";


            using (SqlConnection con = new SqlConnection("workstation id=Fakultet.mssql.somee.com;packet size=4096;user id=marko78087808_SQLLogin_1;pwd=cejbhg7ats;data source=Fakultet.mssql.somee.com;persist security info=False;initial catalog=Fakultet"))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(pattern, con))
                {
                    cmd.Parameters.Add(new SqlParameter("prezime", Getsurname(ListBox2.SelectedItem.ToString())));
                    cmd.Parameters.Add(new SqlParameter("ime", Getfirstname(ListBox2.SelectedItem.ToString())));
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                dohvat_podatka = dr[0].ToString();
                            }
                        }
                        else
                        {
                            Response.Write("No rows in table");
                        }
                    }


                }

            }
            return dohvat_podatka;
        }
        private void Konzultacije(string pattern) // nema smisla returnat to -- voidaj...
        {



            using (SqlConnection con = new SqlConnection("workstation id=Fakultet.mssql.somee.com;packet size=4096;user id=marko78087808_SQLLogin_1;pwd=cejbhg7ats;data source=Fakultet.mssql.somee.com;persist security info=False;initial catalog=Fakultet"))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(pattern, con))
                {
                    cmd.Parameters.Add(new SqlParameter("prezime", Getsurname(ListBox2.SelectedItem.ToString())));
                    cmd.Parameters.Add(new SqlParameter("ime", Getfirstname(ListBox2.SelectedItem.ToString())));
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Label16.Text = dr.GetString(2).Trim() + ":" + "  " + dr[0].ToString() + " - " + dr[1].ToString();
                                dr.Read();
                                Label17.Text = dr.GetString(2).Trim() + ":" + "   " + dr[0].ToString() + " - " + dr[1].ToString();


                            }
                        }
                        else
                        {
                            Response.Write("none");
                        }


                    }



                }


            }
        }
        private bool Nazocnost_profesora()
        {


            TimeSpan tr = new TimeSpan(11, 11, 11);
            TimeSpan tr2 = new TimeSpan(12, 12, 12);
            if (tr > tr2) { }
            SqlConnection con = con = new SqlConnection("workstation id=Fakultet.mssql.somee.com;packet size=4096;user id=marko78087808_SQLLogin_1;pwd=cejbhg7ats;data source=Fakultet.mssql.somee.com;persist security info=False;initial catalog=Fakultet  ");
            con.Open();
            Dtime dt = new Dtime();

            SqlCommand cmd = new SqlCommand
             (@" SELECT tjedan_nastavnik.Pocetak_konz, tjedan_nastavnik.Kraj_konz,Tjedan.ID,Gender.naziv_spola  FROM 
                tjedan_nastavnik,Nastavnik,Tjedan,Gender
                WHERE Nastavnik.ID=tjedan_nastavnik.Nastavnik_ID AND
                tjedan_nastavnik.TjedanID=Tjedan.ID AND Nastavnik.GenderID=Gender.Spol_ID AND
                Nastavnik.ime=@fname AND
                Nastavnik.prezime=@lname", con);

            cmd.Parameters.Add(new SqlParameter("fname", Getfirstname(ListBox2.SelectedItem.ToString())));
            cmd.Parameters.Add(new SqlParameter("lname", Getsurname(ListBox2.SelectedItem.ToString())));


            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ViewState["c"] = dr[0].ToString();
                ViewState["d"] = dr[1].ToString();
                ViewState["dan_broj"] = dr.GetInt32(2);
                ViewState["Spol"] = dr.GetString(3);

                if ((int)ViewState["dan_broj"] == (int)DateTime.Now.DayOfWeek)   // da izbjegnem polje i strukturiziranje nepotrebno
                {
                    break;
                }


            }

            string spol = ViewState["Spol"].ToString().Trim();
            if (spol == "Male")
            {
                Value = Gender.Male;
            }

            if (spol == "Female")
            {
                Value = Gender.Female;
            }
            con.Close();



            TimeSpan ts1 = new TimeSpan(dt.GetHours(ViewState["c"].ToString()), dt.GetMinutes(ViewState["c"].ToString()), 00);
           
            TimeSpan ts2 = new TimeSpan(dt.GetHours(ViewState["d"].ToString()), dt.GetMinutes(ViewState["d"].ToString()), 00);

            string g = DateTime.Now.ToShortTimeString();  //? kakve su ovo brie




            // pitaj profa jel bi brze radio program ako bi stavi ogranicenje na h tipa od 08 00 do 20 00 ili ne bi imalo bas neku ulogu...
            if (DateTime.Now.TimeOfDay.CompareTo(ts1) == 1 &&
               ts2.CompareTo(DateTime.Now.TimeOfDay) == 1 &&
               (int)ViewState["dan_broj"] == (int)DateTime.Now.DayOfWeek)    // ovo tu ne radi jel ovaj populira u petlji samo zadnji red!!!!
            {

                return true;
            }


            else
            {

                return false;
            }



        }
        private int Dani_tjedan(string dan)  // moze i ENUM  da domeniziras to ali ok
        {
            dan = dan.Trim();  // za svaki slucaj
            int broj;
            switch (dan)
            {
                case "poned":
                    broj = 1;
                    break;

                case "utorak":
                    broj = 2;
                    break;

                case "srijeda":
                    broj = 3;
                    break;

                case "cetvrtak":
                    broj = 4;
                    break;

                case "petak":
                    broj = 5;
                    break;

                default:
                    broj = -1;
                    break;

            }
            return broj;
        }
        public void PutImage(string img_source)  // SVE STIMA samo oduzmi od cjele forme do groupboxa nadi interval
        {
            if (string.IsNullOrEmpty(img_source))
            {
                Response.Write("No path");
            }

            else
            {
                try
                {
                    System.Drawing.Image img = System.Drawing.Image.FromFile(img_source);
                    Bitmap bm = new Bitmap(img, 32, 32);

                    Bitmap oCanvas = new Bitmap(200, 150);
                    Graphics g = Graphics.FromImage(oCanvas);
                    g.Clear(Color.White);
                    g.DrawEllipse(Pens.Red, 10, 10, 150, 100);

                    // Now, we only need to send it // to the client
                    Response.ContentType = "image/jpeg";
                    oCanvas.Save(Response.OutputStream, ImageFormat.Jpeg);
                    Response.End();

                    // Cleanup
                    g.Dispose();
                    oCanvas.Dispose();

                }
                catch (Exception e) { Response.Write(e.ToString()); }

            }
        }
        public void ReadWrite_Image(string ime, string prezime)  
        {
            string base64 = "";
            using (SqlConnection con4 = new SqlConnection("workstation id=Fakultet.mssql.somee.com;packet size=4096;user id=marko78087808_SQLLogin_1;pwd=cejbhg7ats;data source=Fakultet.mssql.somee.com;persist security info=False;initial catalog=Fakultet"))
            {
                con4.Open();
                using (SqlCommand cmd = new SqlCommand(@"SELECT Nastavnik.slika FROM Nastavnik
                                                        WHERE Nastavnik.ime=@ime and Nastavnik.prezime=@prezime", con4))
                {
                    cmd.Parameters.AddWithValue("ime", ime);
                    cmd.Parameters.AddWithValue("prezime", prezime);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                if (dr.IsDBNull(0))
                                {
                                    Image2.ImageUrl = "~/Images/none.jpg";

                                }


                                else
                                {
                                    byte[] bt = (byte[])dr["slika"];
                                    base64 = Convert.ToBase64String(bt, 0, bt.Length);
                                    Image2.ImageUrl = "data:image/png;base64," + base64;



                                }
                            }

                        }
                        else
                        {
                            Image2.ImageUrl = "~/Images/none.jpg";

                        }
                    }
                }
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            ListBox2.Items.Clear();
        }
    }
}