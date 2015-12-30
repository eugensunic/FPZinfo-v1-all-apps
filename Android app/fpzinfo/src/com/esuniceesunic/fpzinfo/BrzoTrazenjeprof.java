package com.esuniceesunic.fpzinfo;

import java.util.ArrayList;

import org.kobjects.base64.Base64;
import org.ksoap2.SoapEnvelope;
import org.ksoap2.serialization.SoapObject;
import org.ksoap2.serialization.SoapPrimitive;
import org.ksoap2.serialization.SoapSerializationEnvelope;
import org.ksoap2.transport.HttpTransportSE;

import com.esuniceesunic.fpzinfo.Glavnastranica.SoapTaskAutoComplete;
import com.esuniceesunic.fpzinfo.Glavnastranica.SoapTaskKonzOboje;
import com.esuniceesunic.fpzinfo.Glavnastranica.SoapTaskNazocnostProfesora;
import com.esuniceesunic.fpzinfo.Glavnastranica.SoapTaskPopulacijaSrodnih;
import com.esuniceesunic.fpzinfo.Glavnastranica.SoapTaskkat;
import com.esuniceesunic.fpzinfo.Glavnastranica.SoapTaskobjekt;
import com.esuniceesunic.fpzinfo.Glavnastranica.SoapTaskslika;
import com.esuniceesunic.fpzinfo.Glavnastranica.SoapTasksoba;

import android.support.v7.app.ActionBarActivity;
import android.app.ProgressDialog;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.AutoCompleteTextView;
import android.widget.ImageView;
import android.widget.Spinner;
import android.widget.TextView;


public class BrzoTrazenjeprof extends ActionBarActivity {

	public class SoapTasksoba extends AsyncTask<Void, Void, Void> {
		  private final ProgressDialog dialog = new ProgressDialog(BrzoTrazenjeprof.this);
		  protected void onPreExecute() 
		  {

		   this.dialog.setMessage("dohvaæanje podataka...");
		   this.dialog.show();

		  }

		  @Override
		  protected Void doInBackground(Void... arg0) 
		  {
		   try{
		    sobapublic_string=WebServiceCallExample("SELECT SOBA.Broj FROM Soba,Nastavnik WHERE Soba.ID=Nastavnik.sobaID AND Nastavnik.ime=@ime AND Nastavnik.prezime=@prezime",name,surname);
		    
		   }catch (Exception e){
		    e.printStackTrace();
		   }
		   return null;
		  }

		  protected void onPostExecute(Void result) 
		  {
		
		   soba.setText(sobapublic_string);
		   if (this.dialog.isShowing()){
		    this.dialog.dismiss();
		   }
		  }

		 }
	public class SoapTaskkat extends AsyncTask<Void, Void, Void> {
		  private final ProgressDialog dialog = new ProgressDialog(BrzoTrazenjeprof.this);
		  protected void onPreExecute() 
		  {

		   this.dialog.setMessage("dohvaæanje podataka...");
		   this.dialog.show();

		  }

		  @Override
		  protected Void doInBackground(Void... arg0) 
		  {
		   try{
		    katpublic_string=WebServiceCallExample("SELECT SOBA.Kat FROM Soba,Nastavnik WHERE Soba.ID=Nastavnik.sobaID AND Nastavnik.ime=@ime AND Nastavnik.prezime=@prezime",name,surname);
		    
		   }catch (Exception e){
		    e.printStackTrace();
		   }
		   return null;
		  }

		  protected void onPostExecute(Void result) 
		  {
		
		   kat.setText(katpublic_string);
		   if (this.dialog.isShowing()){
		    this.dialog.dismiss();
		   }
		  }

		 }
	public class SoapTaskKonzOboje extends AsyncTask<Void, Void, Void> {
		  private final ProgressDialog dialog = new ProgressDialog(BrzoTrazenjeprof.this);
		  protected void onPreExecute() 
		  {

		   this.dialog.setMessage("dohvaæanje podataka...");
		   this.dialog.show();

		  }

		  @Override
		  protected Void doInBackground(Void... arg0) 
		  {
		   try{
		    WebServiceCallKonzultacije("SELECT tjedan_nastavnik.Pocetak_konz,tjedan_nastavnik.Kraj_konz,Tjedan.Dan From tjedan_nastavnik,Nastavnik,Tjedan where Nastavnik.ID=tjedan_nastavnik.Nastavnik_ID and tjedan_nastavnik.TjedanID=Tjedan.ID and nastavnik.ime=@ime AND Nastavnik.prezime=@prezime",name,surname);
		    
		   }catch (Exception e){
		    e.printStackTrace();
		   }
		   return null;
		  }

		  protected void onPostExecute(Void result) 
		  {
		    konzultacije1.setText(konzultacijeprve);
		    konzultacije2.setText(konzultacijedruge);
		 
		   if (this.dialog.isShowing()){
		    this.dialog.dismiss();
		   }
		  }

		 }
	public class SoapTaskNazocnostProfesora extends AsyncTask<Void, Void, Void> {
		  private final ProgressDialog dialog = new ProgressDialog(BrzoTrazenjeprof.this);
		  protected void onPreExecute() 
		  {

		   this.dialog.setMessage("dohvaæanje podataka...");
		   this.dialog.show();

		  }

		  @Override
		  protected Void doInBackground(Void... arg0) 
		  {
		   try{
		    WebServiceCallNazocnost(name,surname);
		    
		   }catch (Exception e){
		    e.printStackTrace();
		   }
		   return null;
		  }

		  protected void onPostExecute(Void result) 
		  {
		
		  if(truefalse=="true" || truefalse=="1"){stanje.setText("Nazocan");}
		  else{stanje.setText("Nedostupan");}
		   if (this.dialog.isShowing()){
		    this.dialog.dismiss();
		   }
		  }

		 }
	public class SoapTaskobjekt extends AsyncTask<Void, Void, Void> {
		  private final ProgressDialog dialog = new ProgressDialog(BrzoTrazenjeprof.this);
		  protected void onPreExecute() 
		  {

		   this.dialog.setMessage("dohvaæanje podataka...");
		   this.dialog.show();

		  }

		  @Override
		  protected Void doInBackground(Void... arg0) 
		  {
		   try{
		    objektpublic_string=WebServiceCallExample("SELECT Objekt.Naziv FROM Nastavnik,soba,Objekt WHERE Nastavnik.sobaID=Soba.ID AND soba.ObjektID=Objekt.ID AND Nastavnik.ime=@ime and Nastavnik.prezime=@prezime",name,surname);
		    
		   }catch (Exception e){
		    e.printStackTrace();
		   }
		   return null;
		  }

		  protected void onPostExecute(Void result) 
		  {
		
		 objekt.setText(objektpublic_string);
		   if (this.dialog.isShowing()){
		    this.dialog.dismiss();
		   }
		  }

		 }
	public class SoapTaskslika extends AsyncTask<Void, Void, Void> {
		  private final ProgressDialog dialog = new ProgressDialog(BrzoTrazenjeprof.this);
		  protected void onPreExecute() 
		  {

		   this.dialog.setMessage("dohvaæanje podataka...");
		   this.dialog.show();

		  }

		  @Override
		  protected Void doInBackground(Void... arg0) 
		  {
		   try{
			   WebServiceCallSlika(name,surname);
		    
		   }catch (Exception e){
		    e.printStackTrace();
		   }
		   return null;
		  }

		  protected void onPostExecute(Void result) 
		  {
			 
			  
			 if (slikabajtovi!=null){
			  Bitmap bmp=BitmapFactory.decodeByteArray(slikabajtovi,0,slikabajtovi.length);
			  slika_profesora.setImageBitmap(bmp);}
			 else{slika_profesora.setImageResource(R.drawable.none);}
		   if (this.dialog.isShowing()){
		    this.dialog.dismiss();
		   }
		  }

		 }
	public class SoapTaskAutoComplete extends AsyncTask<Void, Void, Void> {
		  private final ProgressDialog dialog = new ProgressDialog(BrzoTrazenjeprof.this);
		  protected void onPreExecute() 
		  {

		   this.dialog.setMessage("dohvaæanje podataka...");
		   this.dialog.show();

		  }

		  @Override
		  protected Void doInBackground(Void... arg0) 
		  {
		   try{
			   
			   WebServiceCallNastavnik();
		   }catch (Exception e){
		    e.printStackTrace();
		   }
		   return null;
		  }

		  protected void onPostExecute(Void result) 
		  {
			  autoComplete1.setAdapter(arrayAdapter);
	          autoComplete1.setThreshold(1);
		   if (this.dialog.isShowing()){
		    this.dialog.dismiss();
		   }
		  }

		 }
	 AutoCompleteTextView autoComplete1;
	
		TextView ime;
		TextView prezime;
		TextView email;
		TextView soba;
		TextView kat;
		TextView objekt;
		TextView konzultacije1;
		TextView konzultacije2;
		TextView speckonz;
		
	    TextView stanje;
	   
	    ImageView slika_profesora;
	    
	    String sobapublic_string;
	    String katpublic_string;
	    String objektpublic_string;
	    String konzultacijeprve;
	    String konzultacijedruge;
	    String truefalse;
	    ArrayAdapter<String> arrayAdapter;
	  
	    String name;
	    String surname;
	    
	    byte[] slikabajtovi;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_brzo_trazenjeprof);
		autoComplete1=(AutoCompleteTextView)findViewById(R.id.autoCompleteTextView1);
		ime=(TextView)findViewById(R.id.textView11);
		prezime=(TextView)findViewById(R.id.textView12);
		email=(TextView)findViewById(R.id.textView13);
		soba=(TextView)findViewById(R.id.textView14);
		kat=(TextView)findViewById(R.id.textView15);
		objekt=(TextView)findViewById(R.id.textView16);
		konzultacije1=(TextView)findViewById(R.id.textView17);
		konzultacije2=(TextView)findViewById(R.id.textView18);
		speckonz=(TextView)findViewById(R.id.textView19);
		slika_profesora=(ImageView)findViewById(R.id.imageView1);
		stanje=(TextView)findViewById(R.id.textView21);
		autoComplete1.setText("");
		speckonz.setText("Nema");
		autoComplete1.setText("Unesite nastavnika");
		slika_profesora.setImageResource(R.drawable.whiteimage);
		
		  new SoapTaskAutoComplete().execute();
	}
	private String Getsurname(String initialstring)  // PREZIME
    {
        
    	  return initialstring.substring(initialstring.indexOf(' ')).trim();

    }
  
    private String Getfirstname(String initialstring) // IME
    {

      
        return initialstring.substring(0, initialstring.indexOf(' ')).trim();


    }
	 public void Button_Clicked(View view) //spinner
	    {
			
		 ime.setText(Getsurname(autoComplete1.getText().toString()));
         prezime.setText(Getfirstname(autoComplete1.getText().toString()));
         name=Getsurname(autoComplete1.getText().toString().trim());//prema ovom se ravnaj
         surname=Getfirstname(autoComplete1.getText().toString().trim());//prema ovom se ravnaj
         email.setText(name.toLowerCase()+"."+surname.toLowerCase()+"@fpz.hr");
       
         
	 	    new SoapTasksoba().execute();
	 	    new SoapTaskkat().execute();
	 	    new SoapTaskobjekt().execute();
	 	    new SoapTaskKonzOboje().execute();
	 	    new SoapTaskNazocnostProfesora().execute();
		    new SoapTaskslika().execute();
			
		}
	 public String WebServiceCallExample(String pattern,String name,String lname) {
		 String soba_string="";
		 final String METHOD_NAME = "GetDatabaseString";


		   final String NAMESPACE = "http://tempuri.org/";


		   final String URL ="http://fpzinfo.somee.com/WcfService3/WcfService3/Service1.svc"; //96.43.137.45


		  final String SOAP_ACTION = "http://tempuri.org/IService1/GetDatabaseString";
		  
	  SoapObject Request = new SoapObject(NAMESPACE, METHOD_NAME);
	   Request.addProperty("pattern",pattern );
	   Request.addProperty("name", name);
	   Request.addProperty("lastname", lname);
	  /* Set the web service envelope */
	  SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(SoapEnvelope.VER11);
	  
	  envelope.dotNet = true;
	  envelope.setOutputSoapObject(Request);
	 
	  HttpTransportSE androidHttpTransport = new HttpTransportSE(URL);
	  try{
	   androidHttpTransport.call(SOAP_ACTION, envelope);
	 
	   SoapPrimitive sp = (SoapPrimitive) envelope.getResponse();

	   
	  
	    soba_string = sp.toString();
	  
	  }catch (Exception e){
	   e.printStackTrace();
	  }
	return soba_string;
	 }
	 public void WebServiceCallKonzultacije(String pattern,String imee,String prezimee) {
		
		 final String METHOD_NAME = "Konzultacije";


		   final String NAMESPACE = "http://tempuri.org/";


		   final String URL ="http://fpzinfo.somee.com/WcfService3/WcfService3/Service1.svc"; //96.43.137.45


		  final String SOAP_ACTION = "http://tempuri.org/IService1/Konzultacije";
		  
	  SoapObject Request = new SoapObject(NAMESPACE, METHOD_NAME);
	   Request.addProperty("pattern",pattern );
	   Request.addProperty("ime", imee);
	   Request.addProperty("prezime", prezimee);
	  /* Set the web service envelope */
	  SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(SoapEnvelope.VER11);
	  
	  envelope.dotNet = true;
	  envelope.setOutputSoapObject(Request);
	 
	  HttpTransportSE androidHttpTransport = new HttpTransportSE(URL);
	  try{
	   androidHttpTransport.call(SOAP_ACTION, envelope);
	 
	   SoapObject result = (SoapObject) envelope.bodyIn;
	   SoapObject detail = (SoapObject) result.getProperty("Konzultacije"+ "Result");
	   ArrayList<String> listic = new ArrayList<String>();
		for (int i = 0; i <detail.getPropertyCount(); i++)
		{
			String str = detail.getProperty(i).toString();
			listic.add(str);
		}
		
			konzultacijedruge=listic.get(1).toString().trim();
			konzultacijeprve=listic.get(0).toString().trim();
	  }catch (Exception e){
	   e.printStackTrace();
	  }
	 }
	 public void WebServiceCallNazocnost(String ime,String prezime) {
		   final String METHOD_NAME = "Nazocnost_profesora";


		   final String NAMESPACE = "http://tempuri.org/";


		   final String URL ="http://fpzinfo.somee.com/WcfService3/WcfService3/Service1.svc"; //96.43.137.45


		  final String SOAP_ACTION = "http://tempuri.org/IService1/Nazocnost_profesora";
		  
	  SoapObject Request = new SoapObject(NAMESPACE, METHOD_NAME);
	  
	   Request.addProperty("ime", ime);
	   Request.addProperty("prezime", prezime);
	  /* Set the web service envelope */
	  SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(SoapEnvelope.VER11);
	  
	  envelope.dotNet = true;
	  envelope.setOutputSoapObject(Request);
	 
	  HttpTransportSE androidHttpTransport = new HttpTransportSE(URL);
	  try{
	   androidHttpTransport.call(SOAP_ACTION, envelope);
	   //SoapObject response = (SoapObject) envelope.getResponse();
	  

	   SoapObject result = (SoapObject) envelope.getResponse();
	     

	   
	  truefalse=result.toString();
	  }catch (Exception e){
	   e.printStackTrace();
	  }
	 }
	 public void WebServiceCallSlika(String ime,String prezime) {
		   final String METHOD_NAME = "ReadWrite_Image";


		   final String NAMESPACE = "http://tempuri.org/";


		   final String URL ="http://fpzinfo.somee.com/WcfService3/WcfService3/Service1.svc"; //96.43.137.45


		  final String SOAP_ACTION = "http://tempuri.org/IService1/ReadWrite_Image";
		  
	  SoapObject Request = new SoapObject(NAMESPACE, METHOD_NAME);

	   Request.addProperty("ime", ime);
	   Request.addProperty("prezime", prezime);
	  /* Set the web service envelope */
	  SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(SoapEnvelope.VER11);
	  
	  envelope.dotNet = true;
	  envelope.setOutputSoapObject(Request);
	 
	  HttpTransportSE androidHttpTransport = new HttpTransportSE(URL);
	  try{
	   androidHttpTransport.call(SOAP_ACTION, envelope);
	   SoapPrimitive  resultsRequestSOAP = (SoapPrimitive) envelope.getResponse();
        String result = resultsRequestSOAP.toString();
	  if (result.length()!=0 || result!=null)
	  {
	   slikabajtovi= Base64.decode(result.toString());
	   }
	  else {slikabajtovi=null;}
	  
	  
	  
	  }catch (Exception e){
	   e.printStackTrace();
	  }
	 }
	 public void WebServiceCallNastavnik() {
			
		 final String METHOD_NAME = "GetNastavnikFromDatabase";


		   final String NAMESPACE = "http://tempuri.org/";


		   final String URL ="http://fpzinfo.somee.com/WcfService3/WcfService3/Service1.svc"; //96.43.137.45


		  final String SOAP_ACTION = "http://tempuri.org/IService1/GetNastavnikFromDatabase";
		  
	  SoapObject Request = new SoapObject(NAMESPACE, METHOD_NAME);
	  
	  /* Set the web service envelope */
	  SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(SoapEnvelope.VER11);
	  
	  envelope.dotNet = true;
	  envelope.setOutputSoapObject(Request);
	 
	  HttpTransportSE androidHttpTransport = new HttpTransportSE(URL);
	  try{
	   androidHttpTransport.call(SOAP_ACTION, envelope);
	 
	   SoapObject result = (SoapObject) envelope.bodyIn;
	   SoapObject detail = (SoapObject) result.getProperty("GetNastavnikFromDatabase"+ "Result");
	   ArrayList<String> listic = new ArrayList<String>();
		for (int i = 0; i <detail.getPropertyCount(); i++)
		{
			String str = detail.getProperty(i).toString();
			listic.add(str);
		}
		  arrayAdapter = new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, listic);
         
	  }catch (Exception e){
	   e.printStackTrace();
	  }
	 }
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.brzo_trazenjeprof, menu);
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
