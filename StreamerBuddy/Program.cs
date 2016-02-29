using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;
using SharpDX.Direct3D9;
using Color = System.Drawing.Color;
using Line = EloBuddy.SDK.Rendering.Line;
using Sprite = EloBuddy.SDK.Rendering.Sprite;

namespace StreamerBuddy
{
    class Program
    {
        static void Main(string[] args)
        {
            Drawing.OnEndScene += Drawing_OnDraw;
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            Chat.OnMessage += Chat_OnMessage;
            Chat.OnClientSideMessage += Chat_OnClientSideMessage;
        }

        private static void Chat_OnClientSideMessage(ChatClientSideMessageEventArgs args)
        {
            args.Message =  StripSummonerNames(args.Message);
        }


        private static void Chat_OnMessage(AIHeroClient sender, ChatMessageEventArgs args)
        {
            args.Message = StripSummonerNames(args.Message);
        }
        private static void Drawing_OnDraw(EventArgs args)
        {
            if (Loading.IsLoadingComplete)
            {
                Line.DrawLine(Color.Black, 20, new Vector2(0, Drawing.Height / 2.5f), new Vector2(Drawing.Width, Drawing.Height / 2.5f));
                Line.DrawLine(Color.Black, 20, new Vector2(0, Drawing.Height -  Drawing.Height / 11.5f), new Vector2(Drawing.Width, Drawing.Height - Drawing.Height / 11.5f));
            }
        }

        private static string StripSummonerNames(string value)
        {
            foreach (var hero in EntityManager.Heroes.AllHeroes)
            {
                value = value.Replace(hero.Name, CoverName(hero.Name));
            }
            return value;
        }

        private static string CoverName(string name)
        {
            var coverstring = name;
            for (int l = name.Length < 8 ? 1 : 3; l < name.Length - (name.Length < 6 ? 1 : 3); l++)
            {
                coverstring = coverstring.Remove(l, 1);
                coverstring = coverstring.Insert(l, "*");
            }
            return coverstring;
        }
    }
}
