using System.Drawing;
using System.Linq;
using CameraBuddy.Camera;
using CameraBuddy.Spectate.Situation;
using EloBuddy;
using EloBuddy.SDK;
using Player = EloBuddy.Player;

namespace CameraBuddy.Spectate
{
    public class Spectator
    {
        public CameraState CameraState { get; set; }
        private bool Started { get; set; }
        public Spectator()
        {
            CameraState = new CameraState();
            EloBuddy.Game.OnUpdate += Game_OnUpdate;
            Drawing.OnDraw += Drawing_OnDraw;
        }

        public void Stop()
        {
            if(!Started) return;
            EloBuddy.Game.OnUpdate -= Game_OnUpdate;
            Drawing.OnDraw -= Drawing_OnDraw;
            Started = false;
        }


        public void Start()
        {
            if(Started) return;
            EloBuddy.Game.OnUpdate += Game_OnUpdate;
            Drawing.OnDraw += Drawing_OnDraw;
            Started = true;
        }
        private void Drawing_OnDraw(System.EventArgs args)
        {
            if (Player.Instance.IsHPBarRendered) return;
            Drawing.DrawText(Drawing.Width / 2 - 200, 20, Color.Red, "Spectating", 30);
            CameraState.Set();
        }

        private void Game_OnUpdate(System.EventArgs args)
        {
            if(Player.Instance.IsHPBarRendered) return;
            if (Buildings.Ally.Nexus.HealthPercent < 0.4)
            {
                if (Heros.Enemy.Heroes.Any(
                        x => x.Distance(Buildings.Ally.Nexus) <= x.AttackRange + Buildings.Ally.Nexus.BoundingRadius))
                {
                    //Ally Nexus almost dead / being attacked
                    CameraState.Position = Buildings.Ally.Nexus.Position.To2D();
                    return;
                }
            }
            if (Buildings.Enemy.Nexus.HealthPercent < 0.4)
            {
                if (Heros.Ally.Heroes.Any(
                        x => x.Distance(Buildings.Enemy.Nexus) <= x.AttackRange + Buildings.Enemy.Nexus.BoundingRadius))
                {
                    //Enemy Nexus almost dead / being attacked
                    CameraState.Position = Buildings.Enemy.Nexus.Position.To2D();
                    return;
                }
            }
            foreach (var objAiTurret in Buildings.Ally.Turrents)
            {
                if (objAiTurret.HealthPercent <= 0.30)
                {
                    //Separated for possibility of more logic
                    var atackingHeroes = Heros.Enemy.Heroes.Any(
                        x => x.Position.Distance(objAiTurret) <= x.AttackRange + objAiTurret.BoundingRadius);
                    if (atackingHeroes)
                    {
                        //Your turrent is being attacked and almost dead, try and focus here.
                        CameraState.Position = objAiTurret.Position.To2D();
                        return;
                    }
                }
            }
            foreach (var objAiTurret in Buildings.Enemy.Turrents)
            {
                if (objAiTurret.HealthPercent <= 0.30)
                {
                    //Separated for possibility of more logic
                    var atackingHeroes = Heros.Ally.Heroes.Any(
                        x => x.Position.Distance(objAiTurret) <= x.AttackRange + objAiTurret.BoundingRadius);
                    if (atackingHeroes)
                    {
                        //Turrent is being attacked and almost dead, try and focus here.
                        CameraState.Position = objAiTurret.Position.To2D();
                        return;
                    }
                }
            }

            //TODO: Add more logic
            var heroGroupInfo = Heros.AreGrouped(HeroType.Enemy, 3);
            if (heroGroupInfo.Result)
            {
                //Enemy heroes are grouped...
                CameraState.Position = heroGroupInfo.AveragePosistion.To2D();
                return;
            }

            heroGroupInfo = Heros.AreGrouped(HeroType.Ally, 3);
            if (heroGroupInfo.Result)
            {
                //Ally heroes are grouped...
                CameraState.Position = heroGroupInfo.AveragePosistion.To2D();
                return;
            }
        }
    }
}