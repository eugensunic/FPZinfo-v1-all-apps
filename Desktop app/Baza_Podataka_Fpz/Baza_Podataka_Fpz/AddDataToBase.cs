using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Net;
using System.Xml;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
    

namespace Baza_Podataka_Fpz
{
    class AddDataToBase //insert mainly, update,delete, webclient class usage
    {
        
        public void Xmlpodaci_nastavnik()   // USING REGEX,, i kod updatova na fpz serveru ovo funkcionira.
        {
            int counter = 0;
            String html;
            WebClient web = new WebClient();
            char[] alpha = "ABCČĆDĐEFGHIJKLMNOPRSŠTUVZŽ".ToCharArray();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.UTF8;    // za sve chaatactere
            XmlWriter xml = XmlWriter.Create("fpz_nastavnici.xml", settings);
            xml.WriteStartDocument();
            xml.WriteStartElement("Profesor");

            for (int i = 0; i < alpha.Length; i++)
            {
                
                if (alpha[i] == 'Ć') 
                { 
                     html = web.DownloadString("http://www.fpz.unizg.hr/Ustrojstvo.asp?izbID=7&L=" +"%C8" );
                }
                else if (alpha[i] == 'Č')
                {
                     html = web.DownloadString("http://www.fpz.unizg.hr/Ustrojstvo.asp?izbID=7&L=" + "%C6");
                }
                else if (alpha[i] == 'Š')
                {
                     html = web.DownloadString("http://www.fpz.unizg.hr/Ustrojstvo.asp?izbID=7&L=" + "%8A");
                }
                else if (alpha[i] == 'Ž')
                {
                    html = web.DownloadString("http://www.fpz.unizg.hr/Ustrojstvo.asp?izbID=7&L=" + "%8E");
                }
                else 
                {
                    html = web.DownloadString("http://www.fpz.unizg.hr/Ustrojstvo.asp?izbID=7&L=" + alpha[i]);
                }

                MatchCollection m1 = Regex.Matches(html, @"<strong>(.*?)</strong>", RegexOptions.Singleline);// whole collection
                xml.WriteStartElement("Osobe");
                

                foreach (Match m in m1)
                {
                    
                    if (m.Groups[1].ToString() != "Smotra Sveučilišta 2013")
                    {
                        xml.WriteElementString("Name", m.Value.ToString());
                        
                        counter++;
                    }
                }
                xml.WriteEndElement();

            }
            xml.WriteStartElement("UkupanBrojNastavnika");
            xml.WriteElementString("Broj_n", counter.ToString());
            xml.WriteEndElement();
            xml.WriteEndElement();
            xml.WriteEndDocument();
            xml.Close();
           //xml dispose ne jer ce se nakon zatvaranja dokumenta rijesiti resourca odnosno rijesti ce se fajla!!!
        }
        //variables

        int position1;
        int position2;
        string concatenate = "";

        //variables

        private void Nastavnici_fpzwebiste()  //vidit kaj je brze gornja methoda ili donja methoda
        {
            WebClient web = new WebClient();
            string s = web.DownloadString("http://www.fpz.unizg.hr/Ustrojstvo.asp?izbID=7&L=Š");
            string input = "";



            using (StreamWriter str = File.CreateText("bam.txt"))
            {

                str.Write(s);
            }

            using (StreamReader strr = new StreamReader("bam.txt"))
            {

                while (!strr.EndOfStream)
                {
                    input = strr.ReadLine();

                    if (input.Contains("<strong>") && (!input.Contains("Smotra Sveučilišta 2013")))
                    {
                        for (int i = 0; i < input.Length; i++) // nemoj tu micati sigurnije je ovak
                        {
                            if (input[i] == '<' && input[i + 1] == 's')
                            {
                                position1 = i + 8;


                            }
                            else if (input[i] == '>' && input[i - 1] == 'g')
                            {
                                position2 = i - 8;
                            }
                        }

                        for (int j = position1; j < position2; j++)
                        {
                            concatenate += input[j];

                        }
                       
                        concatenate = string.Empty;
                    }




                }


            }

        }
        public void DownloadPdf() 
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadFile("http://zet.hr/media/155423/236.pdf",@"C:\\Users\\eugen\\Desktop");
            }

        }
   


        string getname;
        string getlastname;
        const string  htmltag="<strong>";
        int ID_counter = 1;
        XmlDocument xml;
        SqlConnection con1;

        public void Insert_pic_into_db() 
        {

        }
        public void Insert_into_db() 
        {
            int htmltaglenght = htmltag.Length;
            xml = new XmlDocument();
            try
            {
                xml.Load("fpz_nastavnici.xml");
            }
            catch (Exception e) { MessageBox.Show(e.ToString()); }
            con1 = new SqlConnection("Data Source=pc3490ierf43;Initial Catalog=Fakultet_pz;Integrated Security=True");
            con1.Open();


            foreach (XmlNode node in xml.SelectNodes("Profesor/Osobe/Name")) // PAZITI tu na velika i mala slova!!
            {
                int space1 = node.InnerText.IndexOf(' ');
               
                int space2 = node.InnerText.IndexOf(' ', space1 + 1);  //
               
               
                if (space2!=-1) // ako ima dva spaca (logika ok)
                {
                    getname = node.InnerText.Substring(space2, (node.InnerText.IndexOf('/') - 1) -space2);  
                }

                else  //OK
                {
                    getname = node.InnerText.Substring((node.InnerText.IndexOf(' ')+1), (node.InnerText.IndexOf('/')-2) -node.InnerText.IndexOf(' '));  
                }

                getlastname = node.InnerText.Substring(htmltag.Length,node.InnerText.IndexOf(' ')-htmltag.Length); // vadi van prezime odnosno prvi substring u full-namu

               


                try
                {
                    using (SqlCommand cmd1 = new SqlCommand(@"
                                                INSERT INTO NASTAVNIK (Nastavnik.ID,Nastavnik.ime,Nastavnik.prezime,Nastavnik.email) 
                                                Values(@ID, @Ime, @Prezime,@eMAIL);", con1))
                    {
                        cmd1.Parameters.Add(new SqlParameter("ID",ID_counter));  //int counter
                        cmd1.Parameters.Add(new SqlParameter("Ime",getname));    //ime 
                        cmd1.Parameters.Add(new SqlParameter("Prezime",getlastname));//prezime
                        cmd1.Parameters.Add(new SqlParameter("eMAIL", getname.ToLower()+"."+getlastname.ToLower()+"@fpz.hr")); // email
                        cmd1.ExecuteNonQuery();

                    }
                    ID_counter++;





                }

                catch (SqlException e) { MessageBox.Show(e.ToString()+"Greska_sql"); }
                
               

            }
            MessageBox.Show("Uspjeh!");
            con1.Close();
            }
        public byte[] imagebt;
        public void Insert_Image()  //byting and 
        {

            FileStream fs = new FileStream(@"C:\Users\eugen\Desktop\Dragan Perakovic2013-03-24.jpg", FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            imagebt = br.ReadBytes((int)fs.Length);
           
            MessageBox.Show(imagebt.Length.ToString()); // see if data is really present.
            using (SqlConnection con3=new SqlConnection("Data Source=pc3490ierf43;Initial Catalog=Fakultet_pz;Integrated Security=True"))
            {
                con3.Open();
                using (SqlCommand cmd = new SqlCommand(@"UPDATE Nastavnik
                                                       SET NASTAVNIK.slika=@image1
                                                       WHERE Nastavnik.ID=84", con3))
                {
                    cmd.Parameters.AddWithValue("image1", imagebt);
                    cmd.ExecuteNonQuery();
                }
            }
                    
        }
       
        public Image img 
        { 
          get;
          set;
        }

        public Image ReadWrite_Image(string ime,string prezime)  // ovu metodu napravi return metodu koja vraca type image ili koristi ref keyword
        {
           
            using (SqlConnection con4 = new SqlConnection("Data Source=pc3490ierf43;Initial Catalog=Fakultet_pz;Integrated Security=True"))
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
                                return new Bitmap(@"C:\Users\eugen\Desktop\C# projects\Baza_Podataka_Fpz\Baza_Podataka_Fpz\bin\Debug\none.jpg");
                                
                            }
                          
                           
                           else 
                           {
                               byte [] bt = (byte[])dr["slika"];
                               MemoryStream ms = new MemoryStream(bt);
                               img = Image.FromStream(ms);
                               
                           }
                        }
                        return img;
                    }
                    else
                    {
                        MessageBox.Show("No rows in table, nema slike");
                        return null;
                    }
                }
            }
            }
        }
        public Image Image_Resize(Image img) // for the groupbox ofcourse..., nemoj se glupiat tu samo... uzmi odma vriejdsoti
        {
            return  (Image)new Bitmap(img, new Size(134, 142));
        }

    }
}

