using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Fpz_simulation
{
    public partial class Dvorane : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((DateTime.Now.TimeOfDay < new TimeSpan(07, 59, 00)) || (DateTime.Now.TimeOfDay > new TimeSpan(20, 01, 00)))// zas sa andom ne funkcionira
            {

                Response.Write("Dnevni raspored još nije počeo");
                Button1.Enabled = false;
               
            }
            ListBox1.Items.Clear();
            ListBox2.Items.Clear();
            ListBox3.Items.Clear();
            ListBox4.Items.Clear();

        }
        
        List<string> zadnja_lista;
        string[] array_fin;
        string[] zpolje;
        TimeSpan tm_const1 = new TimeSpan(08, 00, 00); 
        TimeSpan tm_const2 = new TimeSpan(20, 00, 00);

        int inner_counter = 0;

        SqlCommand cmd;
        SqlConnection con;
        SqlDataReader dr;
        SqlDataReader dr1;

        TimeSpan[][] jagged_array1; // pocetak_nastave 
        TimeSpan[][] jagged_array2; // kraj_nastave    

        int index_counter = 0;
        int main_index = 0;
       

        List<string> imena_Lista = new List<string>();
        List<string> Konacan_dodatak = new List<string>();
        private bool IsWeekDayOrHour()
        {
            if (DateTime.Today.DayOfWeek == DayOfWeek.Saturday || (DateTime.Now.TimeOfDay < new TimeSpan(06, 00, 00) && DateTime.Now.TimeOfDay > new TimeSpan(21, 00, 00)))

            { return false; }
            else { return true; }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

            if (IsWeekDayOrHour())
            {
                con = new SqlConnection("Data Source=pc3490ierf43;Initial Catalog=Fakultet_pz;Integrated Security=True");
                con.Open();


                /*za duljinu polja*/
                cmd = new SqlCommand(@"SELECT COUNT(Nastava.Dvoranaa_ID) from nastava,Tjedan  
                                                     WHERE Nastava.Tjedan_ID=Tjedan.ID and Tjedan.ID=@dantjedanbroj
                                                     GROUP BY Nastava.Dvoranaa_ID", con);

                cmd.Parameters.Add(new SqlParameter("dantjedanbroj", (int)System.DateTime.Now.DayOfWeek));


                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        main_index = main_index + dr.FieldCount; // 

                    }
                    dr.Close();
                    jagged_array1 = new TimeSpan[main_index][];


                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        jagged_array1[index_counter] = new TimeSpan[dr.GetInt32(0)];  // u jagged arrayu kolko svaki array ima duljinu
                        index_counter++;


                    }
                    // SVE DOBRO DO OVDJE PROVJERILI 01.07.2014. !
                    index_counter = 0;
                    dr.Close();


                }
                else { }

                cmd = new SqlCommand(@"SELECT  Nastava.Pocetak_nastave,Nastava.Kraj_nastave,Dvorana.Dvorana_ime FROM Nastava,Tjedan,Dvorana
                                   WHERE Nastava.Tjedan_ID=Tjedan.ID and Nastava.Dvoranaa_ID=Dvorana.ID and  Tjedan.ID=@dantjedanbroj
                                   ORDER BY Nastava.Dvoranaa_ID,Pocetak_nastave,Kraj_nastave", con);

                cmd.Parameters.Add(new SqlParameter("dantjedanbroj", (int)System.DateTime.Now.DayOfWeek));
                dr = cmd.ExecuteReader();

                jagged_array2 = new TimeSpan[jagged_array1.Length][];
             


                for (int z = 0; z < jagged_array1.Length; z++)
                {
                    jagged_array2[z] = new TimeSpan[jagged_array1[z].Length];
                   

                }
                imena_Lista.Clear();
                while (dr.Read())  // napuniti polje 
                {

                    for (int i = 0; i < jagged_array1.Length; i++)
                    {

                        for (int j = 0; j < jagged_array1[i].Length; j++)
                        {
                            jagged_array1[i][j] = dr.GetTimeSpan(0);
                            
                            jagged_array2[i][j] = dr.GetTimeSpan(1);
                            string storage = dr.GetString(2);
                            imena_Lista.Add(storage);
                            dr.Read();
                        }
                       

                    }
                   
                    break; //nije break bezveze vec je on tu da ti se ne ovaj .read() ne poremeti!

                }
              
                dr.Close();
                con.Close();
                array_fin = imena_Lista.ToArray();
                Konacan_dodatak.Clear();
              // SVE U REDU DO OVDJE APSOLUTNO SVE!!----------------------------------------
                for (int i = 0; i < jagged_array1.Length; i++)//3 // za modifikaciju odnosno  slobodna/ne
                {


                    for (int j = 0; j < jagged_array1[i].Length; j++)//1
                    {



                        if (jagged_array1[i].Length == 1) // kasnie se pobrini ak nema nis znaci za nula(0)
                        {
                            if ((DateTime.Now.TimeOfDay < jagged_array1[i][j] || DateTime.Now.TimeOfDay > jagged_array2[i][j]))
                            {



                                Konacan_dodatak.Add(array_fin[inner_counter]);

                            }
                        }


                        else
                        {
                            if (j < jagged_array1[i].Length - 1) 
                            {


                                if ((DateTime.Now.TimeOfDay > jagged_array2[i][j] &&   // Uvjet za međuvrijeme! ukoliko postoji rupa za tu dvoranu
                                     DateTime.Now.TimeOfDay < jagged_array1[i][j + 1]))
                                {

                                    Konacan_dodatak.Add(array_fin[inner_counter]);


                                }
                                else if (DateTime.Now.TimeOfDay > jagged_array2[i][jagged_array2[i].Length - 1] && j == jagged_array1[i].Length - 2
                                    || (DateTime.Now.TimeOfDay < jagged_array1[i][0])) // al sad fkt briem da je ok ali fkt fkt!!!
                                {
                                    Konacan_dodatak.Add(array_fin[inner_counter]);

                                }
                            }
                        }
                        inner_counter++;
                    }

                } inner_counter = 0;


                //tu smo stali sa java android!
                Konacan_dodatak = Konacan_dodatak.Distinct().ToList(); //ili stavi break u if i else if!
                


                //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                zpolje = Konacan_dodatak.ToArray();
                if (Konacan_dodatak.Count != 0)
                {
                    for (int i = 0; i < zpolje.Length; i++)
                    {


                        con = new SqlConnection("Data Source=pc3490ierf43;Initial Catalog=Fakultet_pz;Integrated Security=True");
                        con.Open();



                        cmd = new SqlCommand(@"SELECT  Objekt.Naziv,Lokacija.Ime FROM Objekt,Dvorana,Lokacija
                                   WHERE Objekt.ID=Dvorana.ObjektID and Lokacija.ID=Dvorana.LokacijaID
                                  and Dvorana.Dvorana_ime=@moba", con);

                        cmd.Parameters.Add(new SqlParameter("moba", zpolje[i]));
                        zadnja_lista = new List<string>();



                        dr1 = cmd.ExecuteReader();

                        if (dr1.HasRows)
                        {
                            while (dr1.Read())
                            {
                                string naziv_objekta = dr1.GetString(0);
                                switch (naziv_objekta)
                                {

                                    case "Objekt 69":
                                        ListBox1.Items.Add(zpolje[i]);
                                        break;
                                    case "Objekt 70":
                                        ListBox2.Items.Add(zpolje[i]);
                                        break;
                                    case "Objekt 71":
                                        ListBox3.Items.Add(zpolje[i]);
                                        break;
                                    case "Vukeliceva 0":
                                        ListBox4.Items.Add(zpolje[i]);
                                        break;
                                    default:
                                        break;

                                }

                            }




                        }
                        else { Response.Write("No rows"); }
                        dr1.Close();





                    }
                }
                else
                {
                    Response.Write("GRESKA");
                }
            }
            else { Response.Write("NERADNI DAN NA FAKULTETU!"); }

        }
    }
}