using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EzEvade.Config;
using EzEvade.Data;
using EzEvade.Utils;
using SharpDX;
using SpellData = EzEvade.Data.SpellData;

namespace EzEvade
{
    public class SpecialSpellEventArgs : EventArgs
    {
        public bool NoProcess { get; set; }
    }

    public class SpellDetector
    {
        //public delegate void OnCreateSpellHandler(Spell spell);
        //public static event OnCreateSpellHandler OnCreateSpell;

        public delegate void OnProcessDetectedSpellsHandler();

        public static event OnProcessDetectedSpellsHandler OnProcessDetectedSpells;

        public delegate void OnProcessSpecialSpellHandler(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args,
            SpellData spellData, SpecialSpellEventArgs specialSpellArgs);

        public static event OnProcessSpecialSpellHandler OnProcessSpecialSpell;

        //public static event OnDeleteSpellHandler OnDeleteSpell;

        public static Dictionary<int, Spell> Spells = new Dictionary<int, Spell>();
        public static Dictionary<int, Spell> DrawSpells = new Dictionary<int, Spell>();
        public static Dictionary<int, Spell> DetectedSpells = new Dictionary<int, Spell>();

        public static Dictionary<string, IChampionPlugin> ChampionPlugins = new Dictionary<string, IChampionPlugin>();

        public static Dictionary<string, string> ChanneledSpells = new Dictionary<string, string>();

        public static Dictionary<string, SpellData> OnProcessSpells = new Dictionary<string, SpellData>();
        public static Dictionary<string, SpellData> OnMissileSpells = new Dictionary<string, SpellData>();

        public static Dictionary<string, SpellData> WindupSpells = new Dictionary<string, SpellData>();

        private static int _spellIdCount = 0;

        private static AIHeroClient MyHero
        {
            get { return ObjectManager.Player; }
        }

        public static float LastCheckTime = 0;
        public static float LastCheckSpellCollisionTime = 0;

        public static Menu Menu;
        public static Menu SpellMenu;

        public SpellDetector(Menu mainMenu)
        {
            GameObject.OnCreate += SpellMissile_OnCreate;
            GameObject.OnDelete += SpellMissile_OnDelete;

            //GameObject.OnCreate += SpellMissile_OnCreateOld;
            //GameObject.OnDelete += SpellMissile_OnDeleteOld;

            Obj_AI_Base.OnProcessSpellCast += Game_ProcessSpell;

            Game.OnUpdate += Game_OnGameUpdate;

            Menu = mainMenu;

            SpellMenu = Menu.AddSubMenu("Spells", "Spells");

            LoadSpellDictionary();
            InitChannelSpells();
        }

        private void SpellMissile_OnCreate(GameObject obj, EventArgs args)
        {
            if (obj.GetType() != typeof (MissileClient) || !((MissileClient) obj).IsValidMissile())
                return;

            var missile = (MissileClient) obj;

            SpellData spellData;

            if (missile.SpellCaster != null && missile.SpellCaster.Team != MyHero.Team &&
                missile.SData.Name != null && OnMissileSpells.TryGetValue(missile.SData.Name, out spellData)
                && missile.StartPosition != null && missile.EndPosition != null)
            {

                if (missile.StartPosition.Distance(MyHero.Position) < spellData.Range + 1000)
                {
                    var hero = missile.SpellCaster;

                    if (hero.IsVisible)
                    {
                        if (spellData.UsePackets)
                        {
                            CreateSpellData(hero, missile.StartPosition, missile.EndPosition, spellData, obj);
                            return;
                        }

                        foreach (KeyValuePair<int, Spell> entry in Spells)
                        {
                            Spell spell = entry.Value;

                            var dir = (missile.EndPosition.To2D() - missile.StartPosition.To2D()).Normalized();

                            if (spell.Info.MissileName == missile.SData.Name
                                && spell.HeroId == missile.SpellCaster.NetworkId
                                && dir.AngleBetween(spell.Direction) < 10)
                            {

                                if (spell.Info.IsThreeWay == false
                                    && spell.Info.IsSpecial == false)
                                {
                                    spell.SpellObject = obj;

                                    /*if(spell.spellType == SpellType.Line)
                                    {
                                        if (missile.SData.LineWidth != spell.info.radius)
                                        {
                                            Console.WriteLine("Wrong radius " + spell.info.spellName + ": "
                                            + spell.info.radius + " vs " + missile.SData.LineWidth);
                                        }

                                        if (missile.SData.MissileSpeed != spell.info.projectileSpeed)
                                        {
                                            Console.WriteLine("Wrong speed " + spell.info.spellName + ": "
                                            + spell.info.projectileSpeed + " vs " + missile.SData.MissileSpeed);
                                        }
                                        
                                    }*/

                                    //var acquisitionTime = EvadeUtils.TickCount - spell.startTime;
                                    //Console.WriteLine("AcquiredTime: " + acquisitionTime);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Config.Config.GetData<bool>("DodgeFOWSpells"))
                        {
                            CreateSpellData(hero, missile.StartPosition, missile.EndPosition, spellData, obj);
                        }
                    }
                }
            }
        }

        private void SpellMissile_OnDelete(GameObject obj, EventArgs args)
        {
            if (obj.GetType() != typeof (MissileClient) || !((MissileClient) obj).IsValidMissile())
                return;

            var missile = (MissileClient) obj;
            //SpellData spellData;

            foreach (var spell in Spells.Values.ToList().Where(
                s => (s.SpellObject != null && s.SpellObject.NetworkId == obj.NetworkId))) //isAlive
            {
                //Console.WriteLine("Distance: " + obj.Position.Distance(myHero.Position));

                DelayAction.Add(1, () => DeleteSpell(spell.SpellId));
            }
        }

        public void RemoveNonDangerousSpells()
        {
            foreach (var spell in Spells.Values.ToList().Where(
                s => (s.GetSpellDangerLevel() < 3)))
            {
                DelayAction.Add(1, () => DeleteSpell(spell.SpellId));
            }
        }

        private void Game_ProcessSpell(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args)
        {
            try
            {
                /*var castTime2 = (hero.Spellbook.CastTime - Game.Time) * 1000;
                if (castTime2 > 0)
                {
                    Console.WriteLine(args.SData.Name + ": " + castTime2);
                }*/

                SpellData spellData;

                if (hero.Team != MyHero.Team && OnProcessSpells.TryGetValue(args.SData.Name, out spellData))
                {
                    if (spellData.UsePackets == false)
                    {
                        var specialSpellArgs = new SpecialSpellEventArgs();
                        if (OnProcessSpecialSpell != null)
                        {
                            OnProcessSpecialSpell(hero, args, spellData, specialSpellArgs);
                        }

                        if (specialSpellArgs.NoProcess == false && spellData.NoProcess == false)
                        {
                            CreateSpellData(hero, hero.ServerPosition, args.End, spellData, null);

                            /*if (spellData.spellType == SpellType.Line)
                            {
                                var castTime = (hero.Spellbook.CastTime - Game.Time) * 1000;

                                if (Math.Abs(castTime - spellData.spellDelay) > 5)
                                {
                                    Console.WriteLine("Wrong delay " + spellData.spellName + ": "
                                        + spellData.spellDelay + " vs " + castTime);
                                }
                            }*/

                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void CreateSpellData(Obj_AI_Base hero, Vector3 spellStartPos, Vector3 spellEndPos,
            SpellData spellData, GameObject obj = null, float extraEndTick = 0.0f, bool processSpell = true,
            SpellType spellType = SpellType.None, bool checkEndExplosion = true, float spellRadius = 0)
        {
            if (checkEndExplosion && spellData.HasEndExplosion)
            {
                CreateSpellData(hero, spellStartPos, spellEndPos,
                    spellData, obj, extraEndTick, false,
                    spellData.SpellType, false);

                CreateSpellData(hero, spellStartPos, spellEndPos,
                    spellData, obj, extraEndTick, true,
                    SpellType.Circular, false);

                return;
            }
            if (spellStartPos.Distance(MyHero.Position) < spellData.Range + 1000)
            {
                Vector2 startPosition = spellStartPos.To2D();
                Vector2 endPosition = spellEndPos.To2D();
                Vector2 direction = (endPosition - startPosition).Normalized();
                float endTick = 0;

                if (spellType == SpellType.None)
                {
                    spellType = spellData.SpellType;
                }

                if (spellData.FixedRange) //for diana q
                {
                    if (endPosition.Distance(startPosition) > spellData.Range)
                    {
                        //var heroCastPos = hero.ServerPosition.To2D();
                        //direction = (endPosition - heroCastPos).Normalized();
                        endPosition = startPosition + direction*spellData.Range;
                    }
                }
                if (spellType == SpellType.Line)
                {
                    endTick = spellData.SpellDelay + (spellData.Range/spellData.ProjectileSpeed)*1000;
                    endPosition = startPosition + direction*spellData.Range;

                    if (spellData.UseEndPosition)
                    {
                        var range = spellEndPos.To2D().Distance(spellStartPos.To2D());
                        endTick = spellData.SpellDelay + (range/spellData.ProjectileSpeed)*1000;
                        endPosition = spellEndPos.To2D();
                    }

                    if (obj != null)
                        endTick -= spellData.SpellDelay;
                }
                else if (spellType == SpellType.Circular)
                {
                    endTick = spellData.SpellDelay;

                    if (spellData.ProjectileSpeed == 0)
                    {
                        endPosition = hero.ServerPosition.To2D();
                    }
                    else if (spellData.ProjectileSpeed > 0)
                    {
                        if (spellData.SpellType == SpellType.Line &&
                            spellData.HasEndExplosion &&
                            spellData.UseEndPosition == false)
                        {
                            endPosition = startPosition + direction*spellData.Range;
                        }

                        endTick = endTick + 1000*startPosition.Distance(endPosition)/spellData.ProjectileSpeed;
                    }
                }
                else if (spellType == SpellType.Arc)
                {
                    endTick = endTick + 1000*startPosition.Distance(endPosition)/spellData.ProjectileSpeed;

                    if (obj != null)
                        endTick -= spellData.SpellDelay;
                }
                else if (spellType == SpellType.Cone)
                {
                    return;
                }
                else
                {
                    return;
                }
                endTick += extraEndTick;
                Spell newSpell = new Spell();
                newSpell.StartTime = EvadeUtils.TickCount;
                newSpell.EndTime = EvadeUtils.TickCount + endTick;
                newSpell.StartPos = startPosition;
                newSpell.EndPos = endPosition;
                newSpell.Height = spellEndPos.Z + spellData.ExtraDrawHeight;
                newSpell.Direction = direction;
                newSpell.HeroId = hero.NetworkId;
                newSpell.Info = spellData;
                newSpell.SpellType = spellType;
                newSpell.Radius = spellRadius > 0 ? spellRadius : newSpell.GetSpellRadius();
                if (obj != null)
                {
                    newSpell.SpellObject = obj;
                    newSpell.ProjectileId = obj.NetworkId;
                }
                int spellId = CreateSpell(newSpell, processSpell);
                DelayAction.Add((int) (endTick + spellData.ExtraEndTime), () => DeleteSpell(spellId));
            }
        }

        private void Game_OnGameUpdate(EventArgs args)
        {
            UpdateSpells();

            if (EvadeUtils.TickCount - LastCheckSpellCollisionTime > 100)
            {
                CheckSpellCollision();
                LastCheckSpellCollisionTime = EvadeUtils.TickCount;
            }

            if (EvadeUtils.TickCount - LastCheckTime > 1)
            {
                //CheckCasterDead();                
                CheckSpellEndTime();
                AddDetectedSpells();
                LastCheckTime = EvadeUtils.TickCount;
            }
        }

        public static void UpdateSpells()
        {
            foreach (var spell in DetectedSpells.Values)
            {
                spell.UpdateSpellInfo();
            }
        }

        private void CheckSpellEndTime()
        {
            foreach (KeyValuePair<int, Spell> entry in DetectedSpells)
            {
                Spell spell = entry.Value;

                foreach (var hero in EntityManager.Heroes.Enemies)
                {
                    if (hero.IsDead && spell.HeroId == hero.NetworkId)
                    {
                        if (spell.SpellObject == null)
                            DelayAction.Add(1, () => DeleteSpell(entry.Key));
                    }
                }

                if (spell.EndTime + spell.Info.ExtraEndTime < EvadeUtils.TickCount
                    || CanHeroWalkIntoSpell(spell) == false)
                {
                    DelayAction.Add(1, () => DeleteSpell(entry.Key));
                }
            }
        }

        private static void CheckSpellCollision()
        {
            if (Config.Config.GetData<bool>("CheckSpellCollision") == false)
            {
                return;
            }

            foreach (KeyValuePair<int, Spell> entry in DetectedSpells)
            {
                Spell spell = entry.Value;

                var collisionObject = spell.CheckSpellCollision();

                if (collisionObject != null)
                {
                    spell.PredictedEndPos = spell.GetSpellProjection(collisionObject.ServerPosition.To2D());

                    if (spell.CurrentSpellPosition.Distance(collisionObject.ServerPosition)
                        < collisionObject.BoundingRadius + spell.Radius)
                    {
                        DelayAction.Add(1, () => DeleteSpell(entry.Key));
                    }
                }
            }
        }

        public static bool CanHeroWalkIntoSpell(Spell spell)
        {
            if (Config.Config.GetData<bool>("AdvancedSpellDetection"))
            {
                Vector2 heroPos = MyHero.Position.To2D();
                var extraDist = MyHero.Distance(GameData.HeroInfo.ServerPos2D);

                if (spell.SpellType == SpellType.Line)
                {
                    var walkRadius = GameData.HeroInfo.MoveSpeed*(spell.EndTime - EvadeUtils.TickCount)/1000 +
                                     GameData.HeroInfo.BoundingRadius + spell.Info.Radius + extraDist + 10;
                    var spellPos = spell.CurrentSpellPosition;
                    var spellEndPos = spell.GetSpellEndPosition();

                    var projection = heroPos.ProjectOn(spellPos, spellEndPos);

                    return projection.SegmentPoint.Distance(heroPos) <= walkRadius;
                }
                else if (spell.SpellType == SpellType.Circular)
                {
                    var walkRadius = GameData.HeroInfo.MoveSpeed*(spell.EndTime - EvadeUtils.TickCount)/1000 +
                                     GameData.HeroInfo.BoundingRadius + spell.Info.Radius + extraDist + 10;

                    if (heroPos.Distance(spell.EndPos) < walkRadius)
                    {
                        return true;
                    }

                }
                else if (spell.SpellType == SpellType.Arc)
                {
                    var spellRange = spell.StartPos.Distance(spell.EndPos);
                    var midPoint = spell.StartPos + spell.Direction*(spellRange/2);
                    var arcRadius = spell.Info.Radius*(1 + spellRange/100);

                    var walkRadius = GameData.HeroInfo.MoveSpeed*(spell.EndTime - EvadeUtils.TickCount)/1000 +
                                     GameData.HeroInfo.BoundingRadius + arcRadius + extraDist + 10;

                    if (heroPos.Distance(midPoint) < walkRadius)
                    {
                        return true;
                    }

                }

                return false;
            }


            return true;
        }

        private static void AddDetectedSpells()
        {
            bool spellAdded = false;

            foreach (KeyValuePair<int, Spell> entry in DetectedSpells)
            {
                Spell spell = entry.Value;

                float evadeTime, spellHitTime;
                spell.CanHeroEvade(MyHero, out evadeTime, out spellHitTime);

                spell.SpellHitTime = spellHitTime;
                spell.EvadeTime = evadeTime;

                var extraDelay = Game.Ping + Config.Config.GetData<int>("ExtraPingBuffer");

                if (spell.SpellHitTime - extraDelay < 1500 && CanHeroWalkIntoSpell(spell))
                    //if(true)
                {
                    Spell newSpell = spell;
                    int spellId = spell.SpellId;

                    if (!DrawSpells.ContainsKey(spell.SpellId))
                    {
                        DrawSpells.Add(spellId, newSpell);
                    }

                    //var spellFlyTime = Evade.GetTickCount - spell.startTime;
                    if (spellHitTime < Config.Config.GetData<int>("SpellDetectionTime"))
                    {
                        continue;
                    }

                    if (EvadeUtils.TickCount - spell.StartTime < Config.Config.GetData<int>("ReactionTime"))
                    {
                        continue;
                    }

                    var dodgeInterval = Config.Config.GetData<int>("DodgeInterval");
                    if (AdEvade.LastPosInfo != null && dodgeInterval > 0)
                    {
                        var timeElapsed = EvadeUtils.TickCount - AdEvade.LastPosInfo.Timestamp;

                        if (dodgeInterval > timeElapsed)
                        {
                            //var delay = dodgeInterval - timeElapsed;
                            //DelayAction.Add((int)delay, () => SpellDetector_OnProcessDetectedSpells());
                            continue;
                        }
                    }

                    if (!Spells.ContainsKey(spell.SpellId))
                    {
                        if (!(Config.Config.GetData<bool>("DodgeDangerous") && newSpell.GetSpellDangerLevel() < 3)
                            && Config.Config.GetSpell(newSpell.Info.SpellName).Dodge)
                        {
                            if (newSpell.SpellType == SpellType.Circular
                                && !Config.Config.GetData<bool>("DodgeCircularSpells"))
                            {
                                //return spellID;
                                continue;
                            }

                            Spells.Add(spellId, newSpell);

                            spellAdded = true;
                        }
                    }

                    if (Config.Config.GetData<bool>("CheckSpellCollision") && spell.PredictedEndPos != Vector2.Zero)
                    {
                        spellAdded = false;
                    }
                }
            }

            if (spellAdded && OnProcessDetectedSpells != null)
            {
                OnProcessDetectedSpells();
            }
        }

        private static int CreateSpell(Spell newSpell, bool processSpell = true)
        {
            int spellId = _spellIdCount++;
            newSpell.SpellId = spellId;

            newSpell.UpdateSpellInfo();
            DetectedSpells.Add(spellId, newSpell);

            if (processSpell)
            {
                CheckSpellCollision();
                AddDetectedSpells();
            }

            return spellId;
        }

        public static void DeleteSpell(int spellId)
        {
            Spells.Remove(spellId);
            DrawSpells.Remove(spellId);
            DetectedSpells.Remove(spellId);
        }

        public static int GetCurrentSpellId()
        {
            return _spellIdCount;
        }

        public static List<int> GetSpellList()
        {
            List<int> spellList = new List<int>();

            foreach (KeyValuePair<int, Spell> entry in SpellDetector.Spells)
            {
                Spell spell = entry.Value;
                spellList.Add(spell.SpellId);
            }

            return spellList;
        }

        public static int GetHighestDetectedSpellId()
        {
            int highest = 0;

            foreach (var spell in Spells)
            {
                highest = Math.Max(highest, spell.Key);
            }

            return highest;
        }

        public static float GetLowestEvadeTime(out Spell lowestSpell)
        {
            float lowest = float.MaxValue;
            lowestSpell = null;

            foreach (KeyValuePair<int, Spell> entry in SpellDetector.Spells)
            {
                Spell spell = entry.Value;

                if (spell.SpellHitTime != float.MinValue)
                {
                    //Console.WriteLine("spellhittime: " + spell.spellHitTime);
                    lowest = Math.Min(lowest, (spell.SpellHitTime - spell.EvadeTime));
                    lowestSpell = spell;
                }
            }

            return lowest;
        }

        public static Spell GetMostDangerousSpell(bool hasProjectile = false)
        {
            int maxDanger = 0;
            Spell maxDangerSpell = null;

            foreach (Spell spell in SpellDetector.Spells.Values)
            {
                if (!hasProjectile || (spell.Info.ProjectileSpeed > 0 && spell.Info.ProjectileSpeed != float.MaxValue))
                {
                    var dangerlevel = spell.Dangerlevel;

                    if (dangerlevel > maxDanger)
                    {
                        maxDanger = dangerlevel;
                        maxDangerSpell = spell;
                    }
                }
            }

            return maxDangerSpell;
        }

        public static void InitChannelSpells()
        {

            ChanneledSpells["Drain"] = "FiddleSticks";
            ChanneledSpells["Crowstorm"] = "FiddleSticks";
            ChanneledSpells["KatarinaR"] = "Katarina";
            ChanneledSpells["AbsoluteZero"] = "Nunu";
            ChanneledSpells["GalioIdolOfDurand"] = "Galio";
            ChanneledSpells["MissFortuneBulletTime"] = "MissFortune";
            ChanneledSpells["Meditate"] = "MasterYi";
            ChanneledSpells["NetherGrasp"] = "Malzahar";
            ChanneledSpells["ReapTheWhirlwind"] = "Janna";
            ChanneledSpells["KarthusFallenOne"] = "Karthus";
            ChanneledSpells["KarthusFallenOne2"] = "Karthus";
            ChanneledSpells["VelkozR"] = "Velkoz";
            ChanneledSpells["XerathLocusOfPower2"] = "Xerath";
            ChanneledSpells["ZacE"] = "Zac";
            ChanneledSpells["Pantheon_Heartseeker"] = "Pantheon";

            ChanneledSpells["OdinRecall"] = "AllChampions";
            ChanneledSpells["Recall"] = "AllChampions";

        }

        public static void LoadDummySpell(SpellData spell)
        {
            string menuName = spell.CharName + " (" + spell.SpellKey + ") Settings";

            var enableSpell = !spell.DefaultOff;

            var spellConfig = new SpellConfigControl(SpellMenu, menuName, spell, enableSpell);
            spellConfig.AddToMenu();

            Config.Config.Spells.Add(spell.Name, spell.GetSpellConfig(spellConfig));
        }

        //Credits to Kurisu
        public static object NewInstance(Type type)
        {
            var target = type.GetConstructor(Type.EmptyTypes);
            var dynamic = new DynamicMethod(string.Empty, type, new Type[0], target.DeclaringType);
            var il = dynamic.GetILGenerator();

            il.DeclareLocal(target.DeclaringType);
            il.Emit(OpCodes.Newobj, target);
            il.Emit(OpCodes.Stloc_0);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ret);

            var method = (Func<object>) dynamic.CreateDelegate(typeof (Func<object>));
            return method();
        }

        private void LoadSpecialSpell(SpellData spell)
        {
            if (ChampionPlugins.ContainsKey(spell.CharName))
            {
                ChampionPlugins[spell.CharName].LoadSpecialSpell(spell);
            }

            ChampionPlugins["AllChampions"].LoadSpecialSpell(spell);
        }

        private void LoadSpecialSpellPlugins()
        {
            ChampionPlugins.Add("AllChampions", new Data.SpecialSpells.AllChampions());

            foreach (var hero in EntityManager.Heroes.Enemies)
            {
                var championPlugin = Assembly
                    .GetExecutingAssembly()
                    .GetTypes(
                    )
                    .FirstOrDefault(t => t.IsClass && t.Namespace == "EzEvade.Data.SpecialSpells"
                                         && t.Name == hero.ChampionName);

                if (championPlugin != null)
                {
                    if (!ChampionPlugins.ContainsKey(hero.ChampionName))
                    {
                        ChampionPlugins.Add(hero.ChampionName,
                            (IChampionPlugin) NewInstance(championPlugin));
                    }
                }
            }
        }

        private void LoadSpellDictionary()
        {
            LoadSpecialSpellPlugins();

            foreach (var hero in ObjectManager.Get<AIHeroClient>())
            {
                if (hero.IsMe)
                {
                    foreach (var spell in SpellWindupDatabase.Spells.Where(
                        s => (s.CharName == hero.ChampionName)))
                    {
                        if (!WindupSpells.ContainsKey(spell.SpellName))
                        {
                            WindupSpells.Add(spell.SpellName, spell);
                        }
                    }
                }

                if (hero.Team != MyHero.Team)
                {
                    foreach (var spell in SpellDatabase.Spells.Where(
                        s => (s.CharName == hero.ChampionName) || (s.CharName == "AllChampions")))
                    {
                        //Console.WriteLine(spell.spellName); 

                        if (!(spell.SpellType == SpellType.Circular
                              || spell.SpellType == SpellType.Line
                              || spell.SpellType == SpellType.Arc))
                            continue;

                        if (spell.CharName == "AllChampions")
                        {
                            SpellSlot slot = hero.GetSpellSlotFromName(spell.SpellName);
                            if (slot == SpellSlot.Unknown)
                            {
                                continue;
                            }
                        }

                        if (!OnProcessSpells.ContainsKey(spell.SpellName))
                        {
                            if (spell.MissileName == "")
                                spell.MissileName = spell.SpellName;

                            OnProcessSpells.Add(spell.SpellName, spell);
                            OnMissileSpells.Add(spell.MissileName, spell);

                            if (spell.ExtraSpellNames != null)
                            {
                                foreach (string spellName in spell.ExtraSpellNames)
                                {
                                    OnProcessSpells.Add(spellName, spell);
                                }
                            }

                            if (spell.ExtraMissileNames != null)
                            {
                                foreach (string spellName in spell.ExtraMissileNames)
                                {
                                    OnMissileSpells.Add(spellName, spell);
                                }
                            }

                            LoadSpecialSpell(spell);
                            if (!Config.Config.Spells.Any(x => x.Key == spell.SpellName))
                            {
                                string menuName = spell.CharName + " (" + spell.SpellKey + ") Settings";
                                var enableSpell = !spell.DefaultOff;
                                var spellConfig = new SpellConfigControl(SpellMenu, menuName, spell, enableSpell);
                                spellConfig.AddToMenu();

                                Config.Config.SetSpell(spell.SpellName, spell.GetSpellConfig(spellConfig));
                            }
                        }

                    }
                }

            }
        }

    }
}
