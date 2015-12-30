package com.esuniceesunic.fpzinfo;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.URL;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import org.apache.http.HttpResponse;

import android.content.Context;
import android.os.AsyncTask;
import android.widget.ArrayAdapter;
import android.widget.ListView;

public class HtmlReader extends AsyncTask<String, Void, List<String> >
{
	
	String concatenate="";
	ListView listbox0;
	Context ct;
	String firstparameter="";
	String secondparameter="";
	int id;

	public HtmlReader(ListView lt, Context ct1,String parameter1, String parameter2,int id1) 
	{
		
       listbox0=lt;
       ct=ct1;
       firstparameter=parameter1;
       secondparameter=parameter2;
       id=id1;
    }
	
	
	HttpResponse response;
	
	@Override
	protected List<String>doInBackground(String...params) 
	{
	
		List <String> vraceno;
		vraceno=Read_Html_data(params[0],firstparameter,secondparameter);
		return vraceno;
		
		
		
		
		// Generiraj_menu("PRILO", ListBox4, "Normativ");
        
        
	}
	
	
	protected void onPostExecute(List <String> lista)
	{
		
		 ArrayAdapter<String> adapter = new ArrayAdapter<String>(ct,android.R.layout.simple_list_item_1, lista);
		   listbox0.setAdapter(adapter);
     
    }
	private List<String> Read_Html_data(String httpstring, String menu,String finalword)
	{
		String []stockArr;
		List<String>items=new ArrayList<String>();
		char []array=null;
        int position1 = 0;
        int position2 = 0;
        String maindata = "r";
		String concatenate="";
		String line = null;
		try
		{
		   URL address = new URL(httpstring) ;
	        InputStream is = address.openStream() ;
	        InputStreamReader isr = new InputStreamReader(is) ;
	        BufferedReader reader = new BufferedReader(isr) ;
	        items.add("proso");
	        while ((line = reader.readLine()) != null)
	        {
	        	
	        	if (line.contains("<p>") && line.contains("<em>")
	                    && line.contains("<strong>") && line.contains(menu))// MENU-MESNI
	                {
	        		  do
	        		  { 
	        			  maindata=reader.readLine();
	        			  array=maindata.toCharArray();
	        			  for (int i = 0; i < maindata.length(); i++)
	                        {
	                            if (array[i] == '<' && array[i + 1] == 'p' && array[i + 2] == '>') //START POSITION FOR INNER TEXT
	                            {
	                                position1 = i + 3;


	                            }
	                            else if (array[i] == '<' && array[i + 1] == '/') //END POSITION FOR INNER TEXT
	                            {
	                                position2 = i;
	                            }
	                         }
	                            //--------------------------------------------------------------------
	                            for (int j = position1; j < position2; j++)
	                            {
	                                concatenate = concatenate + array[j];

	                            }
	                            
	                            items.add(concatenate);
	                            concatenate="";
	                            
	                        
	        			  
	        		  
	                }while (!maindata.contains(finalword));
	        		   
	        		
	        }
	       
		}
	        }
		catch(Exception d){}
		   stockArr = new String[items.size()];
		   stockArr = items.toArray(stockArr);
		  for (int i = 0; i < items.size(); i++)
          {
             if (stockArr[i].contains("<") || stockArr[i].contains(">"))
              {
                  stockArr[i] = "";
              }
          }
		  if (id == 1 || id == 2)
          {
              int counter = 1;
              for (int i = 0; i < stockArr.length; i++)
              {
                  if (stockArr[i] != "")
                  {
                      stockArr[i] = String.valueOf(counter) + ". " + stockArr[i];

                  }
                  counter++;


              }
          }
		  return Arrays.asList(stockArr);
       

     }


	
	
	}
	
	
