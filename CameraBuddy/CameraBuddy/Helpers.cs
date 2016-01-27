using System.Collections.Generic;
using System.Linq;
using CameraBuddy.Game;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using GameObject = CameraBuddy.Game.GameObject;

namespace CameraBuddy
{
    public static class Helpers
    {
        public static float MaxDistance(this IEnumerable<GameObject> entitys, Vector3 basePos, out GameObject furthestEntity)
        {
            var list = entitys.ToList();
            var maxDist = 0f;
            furthestEntity = null;
            foreach (var entity in list)
            {
                var dist = entity.GetPosistion().Distance(basePos);
                if(dist <= maxDist) continue;

                maxDist = dist;
                furthestEntity = entity;
            }
            return maxDist;
        }
        public static Vector3 AveragePosition(this IEnumerable<GameObject> entitys)
        {
            var list = entitys.ToList();
            var posx = list.Average(x => x.GetPosistion().X);
            var posy = list.Average(x => x.GetPosistion().Y);
            var posz = list.Average(x => x.GetPosistion().Z);

            return new Vector3(posx, posy, posz);
        }
        public static Vector3 OffsetTowardHero(this Vector3 pos, GameObject gameObject, float distance = 10)
        {
            return pos.Extend(gameObject.GetPosistion(), distance).To3D((int)pos.Z);
        }
    }
}