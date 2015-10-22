using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EzEvade.Utils;
using SharpDX;

namespace EzEvade.Data
{
    public class ObjectTrackerInfo
    {
        public GameObject Obj;
        public Vector3 Position;
        public Vector3 Direction;
        public string Name;
        public int OwnerNetworkId;
        public bool UsePosition = false;
        public float Timestamp = 0;

        public ObjectTrackerInfo(GameObject obj)
        {
            this.Obj = obj;
            this.Name = obj.Name;
            this.Timestamp = EvadeUtils.TickCount;
        }

        public ObjectTrackerInfo(GameObject obj, string name)
        {
            this.Obj = obj;
            this.Name = name;
            this.Timestamp = EvadeUtils.TickCount;
        }

        public ObjectTrackerInfo(string name, Vector3 position)
        {
            this.Name = name;
            this.UsePosition = true;
            this.Position = position;

            this.Timestamp = EvadeUtils.TickCount;
        }
    }

    public static class ObjectTracker
    {
        public static Dictionary<int, ObjectTrackerInfo> ObjTracker = new Dictionary<int, ObjectTrackerInfo>();
        public static int ObjTrackerId = 0;

        static ObjectTracker()
        {
            Obj_AI_Minion.OnCreate += HiuCreate_ObjectTracker;
            //Obj_AI_Minion.OnCreate += HiuDelete_ObjectTracker;
        }

        public static void AddObjTrackerPosition(string name, Vector3 position, float timeExpires)
        {
            ObjTracker.Add(ObjTrackerId, new ObjectTrackerInfo(name, position));

            int trackerId = ObjTrackerId; //store the id for deletion
            DelayAction.Add((int) timeExpires, () => ObjTracker.Remove(ObjTrackerId));

            ObjTrackerId += 1;
        }

        private static void HiuCreate_ObjectTracker(GameObject obj, EventArgs args)
        {
            if (obj.IsEnemy && obj.Type == GameObjectType.obj_AI_Minion
                && !ObjectTracker.ObjTracker.ContainsKey(obj.NetworkId))
            {
                var minion = obj as Obj_AI_Minion;

                if (minion.BaseSkinName.Contains("testcube"))
                {
                    ObjectTracker.ObjTracker.Add(obj.NetworkId, new ObjectTrackerInfo(obj, "hiu"));
                    DelayAction.Add(250, () => ObjectTracker.ObjTracker.Remove(obj.NetworkId));
                }
            }
        }

        private static void HiuDelete_ObjectTracker(GameObject obj, EventArgs args)
        {
            if (ObjectTracker.ObjTracker.ContainsKey(obj.NetworkId))
            {
                ObjectTracker.ObjTracker.Remove(obj.NetworkId);
            }
        }

        public static Vector2 GetLastHiuOrientation()
        {
            var objList = ObjectTracker.ObjTracker.Values.Where(o => o.Name == "hiu");
            var sortedObjList = objList.OrderByDescending(o => o.Timestamp);

            if (sortedObjList.Count() >= 2)
            {
                var pos1 = sortedObjList.First().Obj.Position;
                var pos2 = sortedObjList.ElementAt(1).Obj.Position;

                return (pos2.To2D() - pos1.To2D()).Normalized();
            }

            return Vector2.Zero;
        }
    }
}