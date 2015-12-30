using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baza_Podataka_Fpz
{
    class Dtime
    {
        public Dtime()
        {

        }
        public Dtime(int hour, int minutes)
        {
            this.Hour = hour;
            this.Minutes = minutes;
        }

        public int Hour
        {
            get;
            set;
        }
        public int Minutes
        {
            get;
            set;
        }

        public int GetHours(string z)  
        {
           
            z = z.Substring(0, 2);     // kasnie tryparse
            int a = int.Parse(z);
            return a;
        }
        public int GetMinutes(string z)
        {
            z = z.Substring(3, 2);
            int a = int.Parse(z);
            return a;
        }
    }
}
