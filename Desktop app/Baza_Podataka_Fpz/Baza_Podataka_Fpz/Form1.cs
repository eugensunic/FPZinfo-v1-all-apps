using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Net;

using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

using iTextSharp.text.pdf.parser;

namespace Baza_Podataka_Fpz
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // CLASS VARIABLES;

        #region 

        AutoCompleteStringCollection coll;
        SqlConnection con;
        SqlDataReader dr;
        SqlCommand cmd;
        Form3 f3;
        Form2 f2;
        Gender value;
        AddDataToBase addata;
        TimeSpan ts1;// za zelenog covijeka
        TimeSpan ts2; // za zelena svjetla

        #endregion
        enum Gender
        {
            Male,
            Female,
            Unknown
           
        };

        //VARIABLES;
        int dan_broj;
        int dan_tjedan;
        string spol="";
        string c = "";
        string d = "";
        string objekt = "";
        string prvo_vrijeme= "";
        string drugo_vrijeme = "";
        string dan_izbaze = "";
        
        

        #region     
        
        string ime_prezime = string.Empty;
        List<string> populacija_nastavnika;

        #endregion
        AddDataToBase adb3 = new AddDataToBase();
        private void Form1_Load(object sender, EventArgs e)
        {
            AutoComplete();

           
            
           
           
            //UNOS svih nastavnika u debug file 
            
           
        }
        void AutoComplete()
        {
            textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            coll = new AutoCompleteStringCollection();
            try
            {
                using (con = new SqlConnection("workstation id=Fakultet.mssql.somee.com;packet size=4096;user id=marko78087808_SQLLogin_1;pwd=cejbhg7ats;data source=Fakultet.mssql.somee.com;persist security info=False;initial catalog=Fakultet"))
                {
                    con.Open();
                    using (cmd = new SqlCommand("SELECT Predmet.Naziv FROM Predmet", con))
                    using (dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                coll.Add(dr["naziv"].ToString());
                            }
                        }
                        else
                        {
                            MessageBox.Show("No rows in table");
                        }
                    }


                }
                textBox1.AutoCompleteCustomSource = coll;
            }
            catch (SqlException e) { MessageBox.Show(e.ToString()); }
           
        }

        private void button1_Click(object sender, EventArgs e)  // POTVRDI
        {
            AddDataToBase adt = new AddDataToBase();
           
            populacija_nastavnika = new List<string>();
            try
            {
                using (con = new SqlConnection(@"Data Source=pc3490ierf43;
                                                Initial Catalog=Fakultet_pz;
                                                Integrated Security=True"))
                {
                    con.Open();

                    using (cmd = new SqlCommand(@"SELECT Nastavnik.ime,Nastavnik.prezime 
                          FROM Nastavnik,Predmet,Nastavnik_Predmet
                          WHERE Nastavnik.ID=Nastavnik_Predmet.NastavnikID AND
                          Nastavnik_Predmet.PredmetID=Predmet.ID AND
                          Predmet.Naziv=@Predmet_Box1", con))
                    {
                        cmd.Parameters.Add(new SqlParameter("Predmet_Box1", textBox1.Text));
                        using (dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {


                                    ime_prezime = dr[1].ToString().Trim() +" "+ dr[0].ToString().Trim();
                                    populacija_nastavnika.Add(ime_prezime);
                                    ime_prezime = "";
                                }
                            }
                            else
                            {
                                MessageBox.Show("No rows in table");
                            }
                        }
                    }


                }
                listBox1.DataSource = populacija_nastavnika;
               
            }
            catch (SqlException ex) 
            {
                MessageBox.Show(ex.ToString()); 
            }
        }
        AddDataToBase adb;
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)  // LISTBOX
        {
          
            label16.Text = Getsurname(listBox1.SelectedItem.ToString());
            label17.Text = Getfirstname(listBox1.SelectedItem.ToString());

        

            label15.Text = GetDatabaseString(@"SELECT Nastavnik.email FROM Nastavnik      
                                                WHERE Nastavnik.prezime=@prezime AND 
                                                Nastavnik.ime=@ime");



            label14.Text = GetDatabaseString(@"SELECT Nastavnik.Titula FROM Nastavnik
                                            WHERE Nastavnik.ime=@ime AND 
                                            Nastavnik.prezime=@prezime");

             objekt = GetDatabaseString(@"SELECT Objekt.Naziv FROM Nastavnik,soba,Objekt
                                          WHERE Nastavnik.sobaID=Soba.ID AND soba.ObjektID=Objekt.ID AND
                                          Nastavnik.ime=@ime and Nastavnik.prezime=@prezime");
           

           
           label13.Text = GetDatabaseString(@"SELECT SOBA.Broj FROM Soba,Nastavnik      
                                            WHERE Soba.ID=Nastavnik.sobaID AND                        
                                            Nastavnik.ime=@ime AND
                                            Nastavnik.prezime=@prezime") + " " +objekt;

           label20.Text = GetDatabaseString(@"SELECT Soba.Kat FROM Soba,Nastavnik
                                            WHERE soba.ID=Nastavnik.sobaID AND 
                                            Nastavnik.ime=@ime AND
                                            Nastavnik.prezime=@prezime");

           Konzultacije(@"select tjedan_nastavnik.Pocetak_konz,tjedan_nastavnik.Kraj_konz,Tjedan.Dan From tjedan_nastavnik,Nastavnik,Tjedan
                                         where Nastavnik.ID=tjedan_nastavnik.Nastavnik_ID and tjedan_nastavnik.TjedanID=Tjedan.ID and
                                         nastavnik.ime=@ime AND Nastavnik.prezime=@prezime");
           adb = new AddDataToBase();
          adb.img= adb.ReadWrite_Image(Getfirstname(listBox1.SelectedItem.ToString()),Getsurname(listBox1.SelectedItem.ToString()));
       
          pictureBox1.Image = adb.Image_Resize(adb.img);
           

            
            if (Nazocnost_profesora() == true)
            {
                label11.Text = "Nazocan";
                if (value==Gender.Male)
                {
                    PutImage(@"C:\Users\eugen\Desktop\C# projects\Baza_Podataka_Fpz\Baza_Podataka_Fpz\bin\Debug\male_available.png");

                }

                if (value==Gender.Female)  
                {
                     PutImage(@"C:\Users\eugen\Desktop\C# projects\Baza_Podataka_Fpz\Baza_Podataka_Fpz\bin\Debug\female_available.png");
                }
                
            }

            else if (Nazocnost_profesora() == false)
            {
                if (value==Gender.Male)
                {
                    label11.Text = "Nema ga";
                }
                if (value==Gender.Female)
                {
                    label11.Text = "Nema je";
                }
               
                PutImage(@"C:\Users\eugen\Desktop\C# projects\Baza_Podataka_Fpz\Baza_Podataka_Fpz\bin\Debug\unavailable.png");


            }
        }
        

        private string Getsurname(string initialstring) // sa lamdom
        {
            return initialstring.Substring(0, initialstring.IndexOf(' '));
            
            
        }

        private string Getfirstname(string initialstring) // sa lamdom
        {
            
            return  initialstring.Substring(initialstring.IndexOf(' ')+1,initialstring.Length-(initialstring.IndexOf(' ')+1));
           
          
        }

        

        private bool Nazocnost_profesora() 
        {
           
            
       
            con = con = new SqlConnection("Data Source=pc3490ierf43;Initial Catalog=Fakultet_pz;Integrated Security=True");
            con.Open();
            Dtime dt = new Dtime();

            cmd = new SqlCommand
            (@" SELECT tjedan_nastavnik.Pocetak_konz, tjedan_nastavnik.Kraj_konz,Tjedan.ID,Gender.naziv_spola  FROM 
                tjedan_nastavnik,Nastavnik,Tjedan,Gender
                WHERE Nastavnik.ID=tjedan_nastavnik.Nastavnik_ID AND
                tjedan_nastavnik.TjedanID=Tjedan.ID AND Nastavnik.GenderID=Gender.Spol_ID AND
                Nastavnik.ime=@fname AND
                Nastavnik.prezime=@lname", con);

            cmd.Parameters.Add(new SqlParameter("fname", Getfirstname(listBox1.SelectedItem.ToString())));
            cmd.Parameters.Add(new SqlParameter("lname", Getsurname(listBox1.SelectedItem.ToString())));
           

            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                c = dr[0].ToString();
                d = dr[1].ToString();
                dan_broj = dr.GetInt32(2);
                spol = dr.GetString(3);

                if (dan_broj == (int)DateTime.Now.DayOfWeek)   // da izbjegnem polje i strukturiziranje nepotrebno
                {
                    break;
                }

               
            }

            spol = spol.Trim();  
            if (spol == "Male")
            {
                value = Gender.Male;
            }

            if (spol == "Female")
            {
                value = Gender.Female;
            }
            con.Close();
           
          
            
                 ts1 = new TimeSpan(dt.GetHours(c), dt.GetMinutes(c), 00);

                 ts2 = new TimeSpan(dt.GetHours(d), dt.GetMinutes(d), 00);
                
                string g = DateTime.Now.ToShortTimeString();  //? kakve su ovo brie




                // pitaj profa jel bi brze radio program ako bi stavi ogranicenje na h tipa od 08 00 do 20 00 ili ne bi imalo bas neku ulogu...
                if (DateTime.Now.TimeOfDay.CompareTo(ts1) == 1 &&
                   ts2.CompareTo(DateTime.Now.TimeOfDay) == 1 &&
                   dan_broj == (int)DateTime.Now.DayOfWeek)    // ovo tu ne radi jel ovaj populira u petlji samo zadnji red!!!!
                {

                    return true;
                }

                
                else
                {

                    return false;
                }
            
            
           
        }
        private  int Dani_tjedan(string dan)  // moze i ENUM  da domeniziras to ali ok
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
                    broj=5;
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
                MessageBox.Show("No path"); 
            }

            else
            {
                try
                { 
                System.Drawing.Image img = System.Drawing.Image.FromFile(img_source);
                Bitmap bm = new Bitmap(img, 32, 32);
                Graphics paper2 = groupBox1.CreateGraphics();
                paper2.Clear(groupBox1.BackColor);
                paper2.DrawImage(bm, new Point(390, 30));
                }
                catch (Exception e) { MessageBox.Show(e.ToString()); }
                
            }
        }
        private string GetDatabaseString(string pattern)  // General mehtods for strings
        {
            string dohvat_podatka = "";


            using (con = new SqlConnection("Data Source=pc3490ierf43;Initial Catalog=Fakultet_pz;Integrated Security=True"))
            {
                con.Open();
                using (cmd = new SqlCommand(pattern, con))
                {
                    cmd.Parameters.Add(new SqlParameter("prezime", Getsurname(listBox1.SelectedItem.ToString())));
                    cmd.Parameters.Add(new SqlParameter("ime", Getfirstname(listBox1.SelectedItem.ToString())));
                    using (dr = cmd.ExecuteReader())
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
                            MessageBox.Show("No rows in table");
                        }
                    }


                }

            }
            return dohvat_podatka;
        }
        private void Konzultacije(string pattern) // nema smisla returnat to -- voidaj...
        {
            


            using (con = new SqlConnection("Data Source=pc3490ierf43;Initial Catalog=Fakultet_pz;Integrated Security=True"))
            {
                con.Open();
                using (cmd = new SqlCommand(pattern, con))
                {
                    cmd.Parameters.Add(new SqlParameter("prezime", Getsurname(listBox1.SelectedItem.ToString())));
                    cmd.Parameters.Add(new SqlParameter("ime", Getfirstname(listBox1.SelectedItem.ToString())));
                    using (dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                prvo_vrijeme=dr.GetString(2).Trim()+":"+"  "+dr[0].ToString()+ " - "+ dr[1].ToString();
                                dr.Read();
                                drugo_vrijeme = dr.GetString(2).Trim() + ":" +"   "+dr[0].ToString() + " - " + dr[1].ToString();
                               
                                
                            }
                        }
                        else
                        {
                            MessageBox.Show("No rows in table");
                        }
                    }


                }
                label12.Text = prvo_vrijeme;
                label18.Text = drugo_vrijeme;
                

            }
           
          
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)  // ne treba mi ovo ali ne brisati zbog greski
        {
            
        }
        private void DownloadPdf()
        {
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    webClient.DownloadFile("http://zet.hr/media/155423/236.pdf", @"C:\\Users\\eugen\\Desktop\\myfile1.pdf");
                    webClient.DownloadFile("http://zet.hr/media/99990/215j.pdf", @"C:\\Users\\eugen\\Desktop\\myfile2.pdf");
                }
                catch (WebException e) { MessageBox.Show(e.ToString()); }
            }

        }
        private void ReadPDF_File()  // ZA buseve, premjesti kasnie ali ok
        {
            
        }
     
        private void dvoraneToolStripMenuItem_Click(object sender, EventArgs e) //DVORANA TOOLSTRIP
        {

            f2 = new Form2();
            f2.Show();
        }

        private void kalendarToolStripMenuItem_Click(object sender, EventArgs e) //KALENDAR TOOLSTRIP
        {
            // nista za sad ali raditi custom calendar
        }

        private void zetLinijeToolStripMenuItem_Click(object sender, EventArgs e) // ZET LINIJE TOOLSTRIP
        {
            f3 = new Form3();
            f3.Show();

        }

        private void closeallToolStripMenuItem_Click(object sender, EventArgs e) // Close all TOOLSTRIP
        {
            this.Close();
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void rasporedSatiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 fm4 = new Form4();
            fm4.Show();
        }
        
    }
}
