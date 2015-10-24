using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using System.IO;
using System.Net;
using System.Threading;

namespace DualScreenBuddy
{
    class Program
    {
        private static string LocalMapPath =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/DualScreenBuddy/MapData/";


        public static bool IsGameRunning;
        public static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
            Game.OnEnd += Game_OnEnd;
        }

        private static void Game_OnEnd(GameEndEventArgs args)
        {
            IsGameRunning = false;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            IsGameRunning = true;
            Chat.Print("Loaded!");
            var form = new MainForm();
            var screen = Screen.AllScreens.First(x => !x.Primary);
            //form.TopMost = true;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Left = screen.Bounds.Left;
            form.Top = screen.Bounds.Top;
            form.StartPosition = FormStartPosition.Manual;
            form.Size = screen.Bounds.Size;
            form.Show();
            //FormThread = new Thread(new System.Threading.ThreadStart(ThreadProc));
            //FormThread.Start();
        }
        public static void ThreadProc()
        {
            //if (Screen.AllScreens.Length > 1)
            {

                //while (IsGameRunning)
                {
                    Application.DoEvents();
                }
            }
        }
    }
}

