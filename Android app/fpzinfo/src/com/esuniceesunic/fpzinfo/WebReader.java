package com.esuniceesunic.fpzinfo;

import android.support.v7.app.ActionBarActivity;
import android.content.Context;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

public class WebReader extends ActionBarActivity {
	 ListView listbox0;
	 TextView prikazjelovnika;
	   
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_web_reader);
		
		listbox0=(ListView)findViewById(R.id.listView1);
		prikazjelovnika=(TextView)findViewById(R.id.jelovnik);
		
	}
	 public boolean isOnline() {
	        ConnectivityManager cm =
	            (ConnectivityManager) getSystemService(Context.CONNECTIVITY_SERVICE);
	        NetworkInfo netInfo = cm.getActiveNetworkInfo();
	        if (netInfo != null && netInfo.isConnectedOrConnecting()) {
	            return true;
	        }
	        return false;
	    }
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.web_reader, menu);
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
	public void Glavno_jelo(View view)
	{
		
		 HtmlReader task = new HtmlReader(listbox0,this,"GLAVNA JELA","PRILO",3);
	     task.execute(new String[] { "http://www.sczg.unizg.hr/prehrana/restorani/zuk-borongaj/" });

		
	}
	public void Mesni_menu(View view)
	{
		
		 HtmlReader task = new HtmlReader(listbox0,this,"MENU-MESNI:","MENU-VEGETARIJANSKI",1);
	     task.execute(new String[] { "http://www.sczg.unizg.hr/prehrana/restorani/zuk-borongaj/" });

		
	}
	public void Veg_jelo(View view)
	{
		
		 HtmlReader task = new HtmlReader(listbox0,this,"MENU-VEGETARIJANSKI","GLAVNA JELA",2);
	     task.execute(new String[] { "http://www.sczg.unizg.hr/prehrana/restorani/zuk-borongaj/" });

		
	}
	public void Prilog_Click(View view)
	{
		
		 HtmlReader task = new HtmlReader(listbox0,this,"PRILO","Normativ",0);
	     task.execute(new String[] { "http://www.sczg.unizg.hr/prehrana/restorani/zuk-borongaj/" });

		
	}
}
