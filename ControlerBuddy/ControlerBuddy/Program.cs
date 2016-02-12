using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlerBuddy.WindowsHooks;
using EloBuddy;
using EloBuddy.SDK.Events;
using SharpDX.XInput;

namespace ControlerBuddy
{
    class Program
    {
        static void Main()
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            Controller controler;
            if (ControllerManager.TryGetControler(out controler))
            {
                ControllerManager.SetControler(controler);
                MenuManger.Initialize();
                Movement.Initialize();
                Camera.Initialize();
                Mouse.Lock();

                //SpellCasting.Initialize(SpellCasting.LoadSpells(Player.Instance.Hero));
            }
            else
            {
                Chat.Print("Controller Not Found. Canceled loading, press F5 to reload after a controller is connected.", Color.Red);
            }
        }
    }
}
