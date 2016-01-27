using System;
using System.Collections.Generic;
using System.Linq;
using CameraBuddy.Spectate.Data;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace CameraBuddy.Spectate.Situation
{
    public static class Minions
    {
        public static Dictionary<Lane, Vector3> ColisionPoints { get; private set; }

        static Minions()
        {
            ColisionPoints = new Dictionary<Lane, Vector3>();
            GameObject.OnCreate += GameObject_OnCreate;
        }

        private static void GameObject_OnCreate(GameObject sender, EventArgs args)
        {
            if (sender.Type == GameObjectType.MissileClient)
            {
                var missile = (MissileClient)sender;
                if (missile.Target.Type == GameObjectType.obj_AI_Minion &&
                    missile.SpellCaster.Type == GameObjectType.obj_AI_Minion)
                {
                    ColisionPoints[missile.Position.InWhatLane()] = missile.Position;
                }
            }
        }
        public static class Ally
        {
            public static List<Obj_AI_Minion> AllMinions { get {return EntityManager.MinionsAndMonsters.AlliedMinions.ToList(); } }

            ///<summary>
            /// Gets all nearby minions 
            /// </summary>
            /// <param name="range">When range is -1, it will use the players auto attack range + minion bounding radius</param>
            public static List<Obj_AI_Minion> GetNearbyMinions(float range = -1)
            {
                if (range == -1) range = Player.AttackRange;
                return AllMinions.Where(x => x.Position.IsInRange(Player.Posistion, range + x.BoundingRadius)).ToList();
            }
            
        }

        public static class Enemy
        {
            public static List<Obj_AI_Minion> AllMinions { get { return EntityManager.MinionsAndMonsters.EnemyMinions.ToList(); } }
            ///<summary>
            /// When range is -1, it will use the players auto attack range + minion bounding radius
            /// </summary>
            public static List<Obj_AI_Minion> GetNearbyMinions(float range = -1)
            {
                if (range == -1) range = Player.AttackRange;
                return AllMinions.Where(x => x.Position.IsInRange(Player.Posistion, range + x.BoundingRadius)).ToList();
            }
        }

        public static Vector3 GetMinionBlockedPoint(this AIHeroClient enemyHero, float extraDistance)
        {
            var minions = enemyHero.Team == Player.EnemyTeam ? Ally.AllMinions.Where(x => x.Position.InWhatRegion() == Player.RegionIn).ToList() : Enemy.GetNearbyMinions(); // Hero is enemy team, so return my minions, else return enemy minions.
            Vector3 safePoint;
            if (minions.Count > 0)
            {
                float totalX = minions.Select(x => x.Position.X).Sum();
                float totalY = minions.Select(y => y.Position.Y).Sum();
                float averageX = totalX/minions.Count;
                float averageY = totalY/minions.Count;
                var newPoint = new Vector2(averageX, averageY).To3D(); // Point within minions 

                var heroPos = enemyHero.Position;
                safePoint = heroPos.Extend(newPoint, newPoint.Distance(heroPos) + extraDistance).To3D();
            }
            else
            {
                safePoint =  Player.Posistion;
                //TODO: safe distance from enemy, or put that in new function?
            }
            return safePoint;
        }
    }
}