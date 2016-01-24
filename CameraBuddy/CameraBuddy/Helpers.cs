using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace CameraBuddy
{
    public static class Helpers
    {
        public static float MaxDistance(this IEnumerable<Obj_AI_Base> entitys, Vector3 basePos, out Obj_AI_Base furthestEntity)
        {
            var list = entitys.ToList();
            var maxDist = 0f;
            furthestEntity = null;
            foreach (var entity in list)
            {
                var dist = entity.Distance(basePos);
                if(dist <= maxDist) continue;

                maxDist = dist;
                furthestEntity = entity;
            }
            return maxDist;
        }
        public static Vector3 AveragePosition(this IEnumerable<Obj_AI_Base> entitys)
        {
            var list = entitys.ToList();
            var posx = list.Average(x => x.Position.X);
            var posy = list.Average(x => x.Position.Y);
            var posz = list.Average(x => x.Position.Z);

            return new Vector3(posx, posy, posz);
        }
    }
}