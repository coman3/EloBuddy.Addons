using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EloBuddy;
using Menu = EloBuddy.SDK.Menu.Menu;

namespace AdEvade.Draw
{
    public static class Debug
    {
        private static List<string> textToWrite = new List<string>();

        static Debug()
        {
            Drawing.OnEndScene += Drawing_OnEndScene;
        }

        public static int DrawCount = 10;
        private static void Drawing_OnEndScene(EventArgs args)
        {
            if (Config.Properties.GetData<bool>("DebugShow"))
            {
                if (textToWrite.Count > DrawCount)
                {
                    textToWrite.RemoveRange(0, 1);
                }
                for (int i = 0; i < textToWrite.Count; i++)
                {
                    Drawing.DrawText(10f, 10f + (textToWrite[i].Count(x => x == '\n')*12f) + (12f*i),
                        textToWrite[i].Contains('*') ? Color.Red : Color.Yellow, textToWrite[i]);
                }
            }

        }

        public static void PrintChat(object data)
        {
            if(Config.Properties.GetData<bool>("DebugShow"))
                Chat.Print(data);
        }
        public static void DrawTopLeft(object data)
        {
            if (Config.Properties.GetData<bool>("DebugShow"))
                   textToWrite.Add(data.ToString());
        }

        private static Form debugForm;
        private static float lastTickTime;

        public static bool DebugBool(this bool value, string key = "")
        {
            if(key == "")
                DrawTopLeft(value);
            else
                DrawTopLeft(key + " : " + value);
            return value;
        }
        public static void ShowValueForm()
        {
            if (debugForm == null || debugForm.IsDisposed)
            {
                debugForm = new Form();
                var list = new ListBox();
                debugForm.Controls.Add(list);
                list.Dock = DockStyle.Fill;
                debugForm.Text = "Debug Values";
                debugForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                Game.OnUpdate += delegate(EventArgs args)
                {
                    if (lastTickTime + 1 < Game.Time)
                    {
                        list.Items.Clear();
                        foreach (var o in Config.Properties.Data)
                        {
                            list.Items.Add("Data: " + o.Key + " : " + o.Value);
                        }
                        foreach (var o in Config.Properties.Spells)
                        {
                            list.Items.Add("Spells: " + o.Key + " : " + o.Value);
                        }
                        foreach (var o in Config.Properties.Keys)
                        {
                            list.Items.Add("Keys: " + o.Key + " : " + o.Value.CurrentValue);
                        }
                        foreach (var evadeSpell in Config.Properties.EvadeSpells)
                        {
                            list.Items.Add("EvadeSpell: " + evadeSpell.Key + " : " + evadeSpell.Value);
                        }
                        lastTickTime = Game.Time;
                    }
                    debugForm.Height = list.Items.Count*25 + 20;
                };
                debugForm.Show();
            }
        }
    }
}