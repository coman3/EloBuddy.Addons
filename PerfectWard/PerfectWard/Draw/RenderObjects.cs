using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using PerfectWard.Config;
using SharpDX;

namespace PerfectWard.Draw
{
    public abstract class RenderObject
    {
        public bool Visable = true;
        abstract public void Draw();
        abstract public Vector3 GetScreenPos();
    }

    public static class RenderObjects
    {
        private static readonly List<RenderObject> Objects = new List<RenderObject>();
        private static readonly DateTime AssemblyLoadTime = DateTime.Now;

        public static float TickCount
        {
            get
            {
                return (int)DateTime.Now.Subtract(AssemblyLoadTime).TotalMilliseconds;
            }
        }
        static RenderObjects()
        {
            Drawing.OnDraw += Drawing_OnDraw;
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (!Properties.GetData<bool>("Enable")) return;
            Render();
        }

        private static void Render()
        {
            foreach (RenderObject obj in Objects.Where(x => x.Visable))
            {
                if(obj.GetScreenPos().IsOnScreen() || obj.GetType() == typeof(RenderLine))
                    obj.Draw(); //weird after draw
            }
        }

        public static void Add(RenderObject obj)
        {
            Objects.Add(obj);
        }

        public static bool IsNearPlayer(this Vector3 location, float radius)
        {
            return Player.Instance.Position.IsInRange(location, radius);
        }
    }
}