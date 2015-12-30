package com.esuniceesunic.fpzinfo;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.List;

import org.joda.time.DateTime;
import org.joda.time.LocalTime;
import org.joda.time.Period;
import org.joda.time.format.PeriodFormatter;
import org.joda.time.format.PeriodFormatterBuilder;

import android.support.v7.app.ActionBarActivity;
import android.graphics.Color;
import android.os.Bundle;
import android.os.CountDownTimer;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.ProgressBar;
import android.widget.RadioButton;
import android.widget.TextView;
import android.widget.Toast;

public class Autobus_zet extends ActionBarActivity {

	  String[] path_names;
      String[] array;
      CountDownTimer ctd;
      int t2_Totalseconds;
      int t1_Totalseconds;
      TextView bus1;
      TextView bus2;
      TextView linija;
      Period pr;
      Boolean Counter;
      DateTime dt5;
      TextView dan;
      TextView datum;
      ProgressBar firstBar;
      ProgressBar secondBar;
      int konstantnovrijeme;
      int konstantnovrijeme2;
       
      
     
     public void Metodazabuseve(String path)
     {
    	 List<LocalTime>lista=new ArrayList<LocalTime>();
    	 LocalTime[] timespan = null;
         String line="";
         Dtime dt2 = new Dtime();

         try
         {
        	     InputStream json=getAssets().open(path);
                 BufferedReader str = new BufferedReader(new InputStreamReader(json, "UTF-8"));
                 

                     while ((line = str.readLine())  != null)
                     {

                      
                         if (!line.contains("-"))
                         {
                             array = line.split(";");
                             timespan = new LocalTime[array.length];
                             for (int i = 0; i < array.length; i++)
                             {
                                 timespan[i] = new LocalTime(dt2.GetHours(array[i]), dt2.GetMinutes(array[i]), 0);
                                 lista.add(timespan[i]);

                             }
                              

                         }

                     }
                     timespan= new LocalTime[lista.size()];
                     timespan=lista.toArray(timespan);
                     //---------------------------------------------2.dio
                   
                     
                     for (int i = 0; i < timespan.length; i++) 
                     {
                         if (timespan[i].compareTo(LocalTime.now())>0)
                         {
                        	 t1_Totalseconds= (timespan[i].getMillisOfDay())-(LocalTime.now().getMillisOfDay());
                        	 PeriodFormatter fmt = new PeriodFormatterBuilder()
                             .printZeroAlways()
                             .minimumPrintedDigits(2)
                             .appendMinutes()
                             .appendSeparator(":")
                             .printZeroAlways()
                             .minimumPrintedDigits(2)
                             .appendSeconds()
                             .toFormatter();
                             Period period = new Period(t1_Totalseconds);
                             konstantnovrijeme=t1_Totalseconds;
                             String now=fmt.print(period);
                             bus1.setText(now);
                             
                        	 
                        	 if(timespan.length-1==i)
                             {
                            	 t2_Totalseconds=0;
                             }
                             else
                             {
                            	t2_Totalseconds= (timespan[i+1].getMillisOfDay())-(LocalTime.now().getMillisOfDay());
                            	PeriodFormatter fmt1 = new PeriodFormatterBuilder()
                                .printZeroAlways()
                                .minimumPrintedDigits(2)
                                .appendMinutes()
                                .appendSeparator(":")
                                .printZeroAlways()
                                .minimumPrintedDigits(2)
                                .appendSeconds()
                                .toFormatter();
                                Period period1 = new Period(t2_Totalseconds);
                                konstantnovrijeme2=t2_Totalseconds;
                                String now1=fmt1.print(period1);
                                bus2.setText(now1);
                           	 
                             }
                             break; 
                         }
                         else
                         {
                        	 
                        	 bus1.setText("else");
                        	 
                         }
                     }
               
            str.close();
         }
         catch (Exception ef)
         {
           
         }


     }
	
	
     RadioButton rb1;
     RadioButton rb2;
     RadioButton rb3;
     RadioButton rb4;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		try {
			
			super.onCreate(savedInstanceState);
			setContentView(R.layout.activity_autobus_zet);
			bus1=(TextView)findViewById(R.id.textView8);
			bus2=(TextView)findViewById(R.id.textView9);
			linija=(TextView)findViewById(R.id.textView11);
			datum=(TextView)findViewById(R.id.textView3);
			dan=(TextView)findViewById(R.id.textView4);
			firstBar=(ProgressBar)findViewById(R.id.progressBar1);
			secondBar=(ProgressBar)findViewById(R.id.progressBar2);
			
			dt5=new DateTime();
			datum.setText(DateTime.now().toString().substring(0,10));
			dan.setText(String.valueOf(dt5.getDayOfWeek()));
			  Counter=false;
			  path_names = new String[4];
	           
	            path_names[0]="Caviceva_Kampus.txt";
	            path_names[1]="Kampus_Caviceva.txt";
	            path_names[2]="Kampus_Kvatric.txt";
	            path_names[3]="Kvatric_Kampus.txt";
	           

	            rb1 = (RadioButton) findViewById(R.id.radio0);
	            rb2 = (RadioButton) findViewById(R.id.radio1);
	            rb3 = (RadioButton) findViewById(R.id.radio2);
	            rb4 = (RadioButton) findViewById(R.id.radio3);
	            }
			catch(Exception d){Toast.makeText(getApplicationContext(), d.toString(),
					   Toast.LENGTH_LONG).show();}
	}
	public void Click_me(View view)
    {
		secondBar.setProgress(0);
		firstBar.setProgress(0);
		  if (LocalTime.now().compareTo(new LocalTime(06,45,00))>0 && LocalTime.now().compareTo(new LocalTime(20,00,00))<0)
		  {
			  
		  if (rb1.isChecked()){if(Counter!=false){ctd.cancel();}Metodazabuseve(path_names[0]);linija.setText("236");}
		  if (rb2.isChecked()){if(Counter!=false){ctd.cancel();}Metodazabuseve(path_names[1]);linija.setText("236");}
		  if (rb3.isChecked()){if(Counter!=false){ctd.cancel();}Metodazabuseve(path_names[2]);linija.setText("215");}
		  if (rb4.isChecked()){if(Counter!=false){ctd.cancel();}Metodazabuseve(path_names[3]);linija.setText("215");}
		  firstBar.setMax(t1_Totalseconds/1000);
		  secondBar.setMax(t1_Totalseconds/1000);
		  Counter=true;
		  
		 
		  
		  ctd=new CountDownTimer(konstantnovrijeme, 1000) {
             
	            public void onTick(long millisUntilFinished)
	            {
	            	
	            
	              Countdown(bus1);
	              int p = firstBar.getProgress();
                  p += 1;
                  firstBar.setProgress(p);
                  secondBar.setProgress(p);
	              Countdown(bus2);
          	    
          	}

	            public void onFinish() 
	            {
	            	
	            	Toast.makeText(getApplicationContext(), "Autobus kreæe!",
	            			   Toast.LENGTH_LONG).show();
	            }
	         
	        }.start();
		  }
		  else{Toast.makeText(getApplicationContext(), "",
				   Toast.LENGTH_LONG).show();}
	      
    }
  public void Zaustavi_click( View view)
  {
 	 ctd.cancel(); 
 	 bus1.setText("00:00");
 	 bus2.setText("00:00");
 	 linija.setText("");
  }
  public void Countdown(TextView tx)
  {
          int minute1;
          int sekunde1;
          String minute = tx.getText().toString().substring(0,2);
          String sekunde = tx.getText().toString().substring(3, 5);
          minute1=Integer.parseInt(minute);
          sekunde1=Integer.parseInt(sekunde);
          
              sekunde1 = sekunde1 - 1;
              if (sekunde1 < 0)
              {
                  minute1 = minute1 - 1;
                  sekunde1 = 59;

              }
              if (minute1 < 0)
              {
             	 ctd.cancel();
                  tx.setText("Bus je krenuo");
                  
              }
              if (sekunde1 < 10) { tx.setText(String.valueOf(minute1) + ":0" + String.valueOf(sekunde1));  }
              else { tx.setText(String.valueOf(minute1) + ":" + String.valueOf(sekunde1)); }

              if (minute1 < 10) { tx.setText("0"+String.valueOf(minute1) + ":" + String.valueOf(sekunde1)); }

              if (minute1 < 10 && sekunde1 < 10) { tx.setText("0"+String.valueOf(minute1) + ":0" + String.valueOf(sekunde1)); }
    }
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.autobus_zet, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		// Handle action bar item clicks here. The action bar will
		// automatically handle clicks on the Home/Up button, so long
		// as you specify a parent activity in AndroidManifest.xml.
		int id = item.getItemId();
		if (id == R.id.action_settings) {
			return true;
		}
		return super.onOptionsItemSelected(item);
	}
}
