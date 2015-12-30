using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Windows.Forms;

namespace Baza_Podataka_Fpz
{
    class Dvorane_tab
    {

        string[] array_fin;
        TimeSpan lastelmarray;
        string[] zpolje;
        TimeSpan tm_const1 = new TimeSpan(08, 00, 00); // aha...
        TimeSpan tm_const2 = new TimeSpan(20, 00, 00);

        int inner_counter = 0;

        SqlCommand cmd;
        SqlConnection con;
        SqlDataReader dr;

        TimeSpan[][] jagged_array1; // pocetak_nastave 0
        TimeSpan[][] jagged_array2; // kraj_nastave    1

        int index_counter = 0;
        int main_index = 0;
        int dantjedan = 4;

        List<string> imena_Lista = new List<string>();
        List<string> Konacan_dodatak = new List<string>();


        public void Dvorane()
        {
            con = new SqlConnection("Data Source=pc3490ierf43;Initial Catalog=Fakultet_pz;Integrated Security=True");
            con.Open();


            /*za duljinu polja*/
            cmd = new SqlCommand(@"select count(Nastava.Dvoranaa_ID) from nastava,Tjedan  
                                                     WHERE Nastava.Tjedann_ID=Tjedan.ID and Tjedan.ID=2
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
                index_counter = 0;
                dr.Close();


            }
            else { MessageBox.Show("table has now rows "); }

            cmd = new SqlCommand(@"SELECT  Nastava.Pocetak_nastave,Nastava.Kraj_nastave,Dvorana.Dvorana_ime FROM Nastava,Tjedan,Dvorana
                                   WHERE Nastava.Tjedann_ID=Tjedan.ID and Nastava.Dvoranaa_ID=Dvorana.ID and  Tjedan.ID=2
                                   ORDER BY Nastava.Dvoranaa_ID,Pocetak_nastave,Kraj_nastave", con);

            cmd.Parameters.Add(new SqlParameter("dantjedanbroj", (int)System.DateTime.Now.DayOfWeek));
            dr = cmd.ExecuteReader();

            jagged_array2 = new TimeSpan[jagged_array1.Length][];


            for (int z = 0; z < jagged_array1.Length; z++)
            {
                jagged_array2[z] = new TimeSpan[jagged_array1[z].Length];

            }

            while (dr.Read())  // napuniti polje 
            {

                for (int i = 0; i < jagged_array1.Length; i++)//3  ,, 4
                {

                    for (int j = 0; j < jagged_array1[i].Length; j++)//1
                    {
                        jagged_array1[i][j] = dr.GetTimeSpan(0);
                        
                        jagged_array2[i][j] = dr.GetTimeSpan(1);
                        imena_Lista.Add(dr.GetString(2));
                        


                        dr.Read();
                    }

                }


                break;

            }
            

            dr.Close();
            con.Close();
           
            array_fin = imena_Lista.ToArray();
            Konacan_dodatak.Clear();

            for (int i = 0; i < jagged_array1.Length; i++)//3 // za modifikaciju odnosno  slobodna/ne
            {
                
               
                for (int j = 0; j < jagged_array1[i].Length; j++)//1
                {
                    

                   
                    if (jagged_array1[i].Length == 1) // kasnie se pobrini ak nema nis znaci za nula(0)
                    {
                        if (!(DateTime.Now.TimeOfDay < jagged_array2[i][j]  && 
                              DateTime.Now.TimeOfDay > jagged_array1[i][j]) &&
                              dantjedan == 2) 
                        {

                            
                            
                              Konacan_dodatak.Add(array_fin[inner_counter]);
                             
                        }
                    }
                       

                    else
                    {
                        if (j < jagged_array1[i].Length-1 )  
                        {
                            
                           
                            if ((DateTime.Now.TimeOfDay > jagged_array2[i][j] && 
                                 DateTime.Now.TimeOfDay < jagged_array1[i][j + 1]) 
                                 && dantjedan ==2) 
                            {

                                
                                                                                           // ili ak ne zelis gore onda if (j==jagged_array1[i].length-2)
                            Konacan_dodatak.Add(array_fin[inner_counter]);
                                
                            }
                            else if (DateTime.Now.TimeOfDay > jagged_array2[i][jagged_array2[i].Length - 1] && j == jagged_array1[i].Length - 2
                                || (DateTime.Now.TimeOfDay<jagged_array1[i][0])) // al sad fkt briem da je ok ali fkt fkt!!!
                            {
                                Konacan_dodatak.Add(array_fin[inner_counter]);





                            }
                        }
                    }
                    inner_counter++;
                }

            } inner_counter = 0;
             // dobro sredili smo da to nemora ali zast to ne radi¨!!!!!
            foreach (var item in Konacan_dodatak)
            {
                MessageBox.Show(item.ToString());
            }
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            zpolje=Konacan_dodatak.ToArray();
            if (Konacan_dodatak.Count!=0)
            {
                for (int i = 0; i < zpolje.Length; i++)
			    {


                    con = new SqlConnection("Data Source=pc3490ierf43;Initial Catalog=Fakultet_pz;Integrated Security=True");
                    con.Open();


           
            cmd = new SqlCommand(@"SELECT Objekt.Naziv,Lokacija.Ime FROM Objekt,Dvorana,Lokacija
                                   WHERE Objekt.ID=Dvorana.ObjektID and Lokacija.ID=Dvorana.LokacijaID
                                  and Dvorana.Dvorana_ime='@soba'", con);

            cmd.Parameters.Add(new SqlParameter("soba", zpolje[i]));


            dr = cmd.ExecuteReader();

            
                while (dr.Read())
                {
                    string naziv_objekta = dr.GetString(0);
                    switch (naziv_objekta) 
                    { 
                        case "Objekt69":
                           //LISTBOX List.add
                            break;
                        case "Objekt70":

                            break;
                        case "Objekt 71":

                            break;
                        case "Vukeliceva0":

                            break;
                        default: 
                            break;

                    }

                }
                dr.Close();
                con.Close();
                con.Dispose();
           
              
            

                }
            }
            else
            {
                MessageBox.Show("no rows, sve sobe na svim lokacijama su zauzete!");
            }
         }
    }
}
