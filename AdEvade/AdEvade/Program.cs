using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK.Events;

namespace AdEvade
{
    class Program
    {
        //public static AdEvade Evade;
        static void Main(string[] args)
        {
            //Evade = new AdEvade();

            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            Chat.Print("<font color='#ff0000'>Development for this version of AdEvade has been discontinued. Please wait for the rework to be released, and use EvadePlus or Evade# in the meantime. Thanks.</font>");
        }
    }
}
