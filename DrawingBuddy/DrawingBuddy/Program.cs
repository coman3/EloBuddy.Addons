using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;
using Color = System.Drawing.Color;

namespace DrawingBuddy
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Wait till the loading screen has passed
            Loading.OnLoadingComplete += OnLoadingComplete;
        }

        private static void OnLoadingComplete(EventArgs args)
        {
            AIHeroClient.OnDeath += Player_OnDeath;
            Game.OnUpdate += Game_OnUpdate;
            Console.WriteLine("Loaded!");
            
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (Player.Instance.IsDead)
            {
                Console.WriteLine("OnDeath");
                unsafe
                {
                    byte* f = (byte*)(Player.Instance.MemoryAddress + 0x118);
                    *f = 0;
                    Console.WriteLine("Setting...");
                }
                Game.OnUpdate -= Game_OnUpdate;
            }
        }

        private static void Player_OnDeath(Obj_AI_Base sender, OnHeroDeathEventArgs args)
        {

        }

        

    

       
    }
}
