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
            Drawing.OnDraw += Drawing_OnDraw;
            
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            foreach (var enemy in EntityManager.Heroes.Enemies)
            {
                // If enemy is on screen, or the cursor is arround the posistion of the enemy
                if (enemy.Position.IsOnScreen() || enemy.Position.Distance(Game.CursorPos) < 2000)
                    Line.DrawLine(Color.Gray, 2, enemy.Path);
            }
        }

    

       
    }
}
