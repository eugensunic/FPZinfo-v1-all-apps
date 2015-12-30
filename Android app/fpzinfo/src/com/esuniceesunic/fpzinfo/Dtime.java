package com.esuniceesunic.fpzinfo;

public class Dtime 
{
	 public Dtime(){}
     public int GetHours(String z)
     {

         z = z.substring(0, 2);     
         int a = Integer.parseInt(z);
         return a;
     }
     public int GetMinutes(String z)
     {
         z = z.substring(3, 5);
         int a = Integer.parseInt(z);
         return a;
     }
}
