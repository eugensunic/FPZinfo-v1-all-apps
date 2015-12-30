package com.esuniceesunic.fpzinfo;

import java.util.ArrayList;

import org.ksoap2.SoapEnvelope;
import org.ksoap2.serialization.SoapObject;
import org.ksoap2.serialization.SoapSerializationEnvelope;
import org.ksoap2.transport.HttpTransportSE;

import com.esuniceesunic.fpzinfo.Glavnastranica.SoapTaskPopulacijaSrodnih;

import android.support.v7.app.ActionBarActivity;
import android.app.ProgressDialog;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.Toast;

public class Slobodneucionice extends ActionBarActivity {

	ArrayAdapter<String> arrayAdapter;
	ListView listbox;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_slobodneucionice);
		listbox=(ListView)findViewById(R.id.listBox1);
		
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.slobodneucionice, menu);
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
	 public void WebServiceCallDvorane() {
			
		 final String METHOD_NAME = "GetAvailableRoom";


		   final String NAMESPACE = "http://tempuri.org/";


		   final String URL ="http://fpzinfo.somee.com/WcfService3/WcfService3/Service1.svc"; //96.43.137.45


		  final String SOAP_ACTION = "http://tempuri.org/IService1/GetAvailableRoom";
		  
	  SoapObject Request = new SoapObject(NAMESPACE, METHOD_NAME);
	  
	  /* Set the web service envelope */
	  SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(SoapEnvelope.VER11);
	  
	  envelope.dotNet = true;
	  envelope.setOutputSoapObject(Request);
	 
	  HttpTransportSE androidHttpTransport = new HttpTransportSE(URL);
	  try{
	   androidHttpTransport.call(SOAP_ACTION, envelope);
	 
	   SoapObject result = (SoapObject) envelope.bodyIn;
	   SoapObject detail = (SoapObject) result.getProperty("GetAvailableRoom"+ "Result");
	   ArrayList<String> listic = new ArrayList<String>();
	   if (detail.getPropertyCount()!=0)
	   {
		for (int i = 0; i <detail.getPropertyCount(); i++)
		 {
			String str = detail.getProperty(i).toString();
			listic.add(str);
		 }
	   
		  arrayAdapter = new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, listic);
	   }
	   else{arrayAdapter=null;}
	  }catch (Exception e){
	   e.printStackTrace();
	  }
	 }
	 
	 public class SoapTasksDvorane extends AsyncTask<Void, Void, Void> {
		  private final ProgressDialog dialog = new ProgressDialog(Slobodneucionice.this);
		  protected void onPreExecute() 
		  {

		   this.dialog.setMessage("dohvaæanje podataka...");
		   this.dialog.show();

		  }

		  @Override
		  protected Void doInBackground(Void... arg0) 
		  {
		   try{
		    WebServiceCallDvorane();
		   
		    
		   }catch (Exception e){
		    e.printStackTrace();
		   }
		   return null;
		  }

		  protected void onPostExecute(Void result) 
		  {
			 
		   listbox.setAdapter(arrayAdapter);
		   if (this.dialog.isShowing()){
		    this.dialog.dismiss();
		   }
		  }

		 }
	 public void ClickButton(View view) //spinner
	    {
			
		  new SoapTasksDvorane().execute();
			
		}
}
