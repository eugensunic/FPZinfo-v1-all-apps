package com.esuniceesunic.fpzinfo;

import android.support.v7.app.ActionBarActivity;
import android.content.Context;
import android.content.Intent;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Toast;


public class MainActivity extends ActionBarActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        if (isOnline()==false)
        {
        	Toast.makeText(getApplicationContext(), "Otvorite Internet vezu!",
 				   Toast.LENGTH_LONG).show();
        }
     
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
        getMenuInflater().inflate(R.menu.main, menu);
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
    public void Vozni_red(View view)
    {
    	Intent myIntent = new Intent(this, Autobus_zet.class);
    	
    	startActivity(myIntent);
    }
 public void Menza(View view)
    {
    	Intent myIntent = new Intent(this, WebReader.class);
    	
    	startActivity(myIntent);
    }
 public void app_info(View view)//kalendar
    {
    	Intent myIntent = new Intent(this, Kalendarfpz.class);
    	
    	startActivity(myIntent);
    }
 public void Slobodne_ucionice(View view)
    {
    	Intent myIntent = new Intent(this, Slobodneucionice.class);
    	
    	startActivity(myIntent);
    }
 public void Profesor_info(View view)
    {
    	Intent myIntent = new Intent(this, Glavnastranica.class);
    	
    	startActivity(myIntent);
    }
 public void Brzo_pretrazivanje(View view)
    {
    	Intent myIntent = new Intent(this, BrzoTrazenjeprof.class);
    	
    	startActivity(myIntent);
    }

}
