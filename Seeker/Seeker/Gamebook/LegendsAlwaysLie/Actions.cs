using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.LegendsAlwaysLie
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();

        public enum FoodSharingType { KeepMyself, ToHim, FiftyFifty };

        public int Price { get; set; }
        public string Text { get; set; }
        public bool Disabled { get; set; } 

        public List<Character> Enemies { get; set; }
        public string ConneryAttacks { get; set; }
        public int OnlyRounds { get; set; }
        public int RoundsToWin { get; set; }
        public int AttackWounds { get; set; }
        public string ReactionWounds { get; set; }
        public bool IncrementWounds { get; set; }
        public string ReactionRound { get; set; }
        public string ReactionHit { get; set; }
        public bool GolemFight { get; set; }
        public bool ZombieFight { get; set; }

        public int Dices { get; set; }
        public int DiceBonus { get; set; }
        public FoodSharingType? FoodSharing { get; set; }

        public Abstract.IModification Benefit { get; set; }
        public Abstract.IModification Damage { get; set; }

        public Character.SpecializationType? Specialization { get; set; }


        public override List<string> Representer()
        {
            if (!String.IsNullOrEmpty(Text))
                return new List<string> { Text };

            List<string> enemies = new List<string>();

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                enemies.Add(String.Format("{0}\nсила {1}  жизни {2}", enemy.Name, enemy.Strength, enemy.Hitpoints));

            return enemies;
        }

        public override List<string> Status() => new List<string>
        {
            String.Format("Сила: {0}", Character.Protagonist.Strength),
            String.Format("Жизни: {0}", Character.Protagonist.Hitpoints),
            String.Format("Заклинаний: {0}", Character.Protagonist.Magicpoints),
            String.Format("Золото: {0}", Character.Protagonist.Gold),
        };

        public override List<string> AdditionalStatus() => new List<string>
        {
            String.Format("Жизни Коннери: {0}", Character.Protagonist.ConneryHitpoints),
            String.Format("Доверие Коннери: {0}", Character.Protagonist.ConneryTrust),
        };

        public override List<string> StaticButtons()
        {
            List<string> staticButtons = new List<string> { };

            if (Constants.GetParagraphsWithoutStaticsButtons().Contains(Game.Data.CurrentParagraphID))
                return staticButtons;

            bool healingSpell = (Character.Protagonist.Magicpoints > 0) && (Character.Protagonist.HealingSpellLost <= 0);

            if (healingSpell && !Game.Checks.ExistsInParagraph(actionText: "Вылечить"))
            {
                if (Character.Protagonist.Hitpoints < 30)
                    staticButtons.Add("ЛЕЧИЛКА");

                if ((Character.Protagonist.ConneryHitpoints < 30) && (Character.Protagonist.Hitpoints > 2))
                    staticButtons.Add("ЛЕЧИЛКА ДЛЯ КОННЕРИ");
            }

            if ((Character.Protagonist.Elixir > 0) && (Character.Protagonist.Hitpoints < 30))
                staticButtons.Add("ЭЛИКСИР");

            return staticButtons;
        }

        public override bool StaticAction(string action)
        {
            if (action == "ЛЕЧИЛКА")
            {
                Character.Protagonist.Hitpoints += 6;
                Character.Protagonist.Magicpoints -= 1;
                
                return true;
            }
            else if (action == "ЛЕЧИЛКА ДЛЯ КОННЕРИ")
            {
                Character.Protagonist.ConneryHitpoints += 8;
                Character.Protagonist.Magicpoints -= 1;

                InjuriesBySpells();

                return true;
            }
            else if ((action == "ЭЛИКСИР") && (Character.Protagonist.Hitpoints < 30))
            {
                Character.Protagonist.Hitpoints = 30;
                Character.Protagonist.Elixir -= 1;

                return true;
            }

            return false;
        }

        public static void InjuriesBySpells() =>
            Character.Protagonist.Hitpoints -= (Character.Protagonist.Specialization == Character.SpecializationType.Wizard ? 1 : 2);

        private bool GoodReaction(ref List<string> reaction)
        {
            int reactionLevel = (int)Math.Floor((double)Character.Protagonist.Hitpoints / 5);
            reaction.Add(String.Format("Уровнь реакции: {0} / 5 = {1}", Character.Protagonist.Hitpoints, reactionLevel));

            if (Game.Data.Triggers.Contains("EvilEye"))
            {
                reactionLevel -= 1;
                reaction.Add(String.Format("Из-за сглаза уровнь реакции снижается на единицу: {0}", reactionLevel));
            }

            int reactionDice = Game.Dice.Roll();
            bool goodReaction = reactionDice <= reactionLevel;
            reaction.Add(String.Format("Реакция: {0} {1} {2}", Game.Dice.Symbol(reactionDice), (goodReaction ? "<=" : ">"), reactionLevel));

            return goodReaction;
        }

        public List<string> Reaction()
        {
            List<string> reaction = new List<string>();

            bool goodReaction = GoodReaction(ref reaction);

            reaction.Add(goodReaction ? "BIG|GOOD|СРЕАГИРОВАЛИ :)" : "BIG|BAD|НЕ СРЕАГИРОВАЛИ :(");

            if (goodReaction && (Benefit != null))
                Benefit.Do();

            if (!goodReaction && (Damage != null))
                Damage.Do();

            return reaction;
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = String.Empty;

            if (Character.Protagonist.Hitpoints <= 0)
                toEndText = "Начать сначала";

            else if (Character.Protagonist.ConneryHitpoints <= 0)
                toEndText = "Коннери погиб, ваше путешествие окончено";

            else if (Character.Protagonist.ConneryTrust <= 0)
                toEndText = "Коннери потерял к вам всякое доверие, ваше путешествие окончено";

            else
                return false;

            return true;
        }

        public override bool IsButtonEnabled()
        {
            bool bySpecButton = (Specialization != null) && (Character.Protagonist.Specialization != Character.SpecializationType.Nope);
            bool byPrice = (Price > 0) && (Character.Protagonist.Gold < Price);
            bool byCureSprain = (ActionName == "CureSprain") && (Character.Protagonist.Magicpoints <= 0);

            bool byAlreadyDecided = (FoodSharing != null) && Character.Protagonist.FoodIsDivided;
            bool byFootwraps = ((ActionName == "FootwrapsDeadlyReplacement") ||
                (ActionName == "FootwrapsReplacement")) && Character.Protagonist.Footwraps <= 0;

            return !(bySpecButton || byPrice || byCureSprain || byAlreadyDecided || byFootwraps || Disabled);
        }

        public List<string> Get()
        {
            if ((Specialization != null) && (Character.Protagonist.Specialization == Character.SpecializationType.Nope))
            {
                Character.Protagonist.Specialization = Specialization ?? Character.SpecializationType.Nope;

                if (Specialization == Character.SpecializationType.Warrior)
                {
                    Character.Protagonist.Strength += 2;
                    Character.Protagonist.Magicpoints = 2;
                }

                else if (Specialization == Character.SpecializationType.Wizard)
                    Character.Protagonist.Magicpoints = 5;

                else
                {
                    Character.Protagonist.Strength += 1;
                    Character.Protagonist.Magicpoints = 3;
                }
            }

            else if ((Price > 0) && (Character.Protagonist.Gold >= Price))
                Character.Protagonist.Gold -= Price;

            return new List<string> { "RELOAD" };
        }

        public List<string> Sell()
        {
            Disabled = true;
            Character.Protagonist.Gold += Price;             

            return new List<string> { "RELOAD" };
        }

        public override bool CheckOnlyIf(string option)
        {
            if (option.Contains(";"))
            {
                string[] options = option.Split(';');

                int optionMustBe = int.Parse(options[0]);
                int optionCount = options.Where(x => Game.Data.Triggers.Contains(x.Trim())).Count();

                return (optionCount >= optionMustBe);
            }
            else
            {
                string[] options = option.Split('|', ',');

                bool orLogic = option.Contains("|");

                foreach (string oneOption in options)
                {
                    if (oneOption.Contains(">") || oneOption.Contains("<"))
                    {
                        int level = Game.Other.LevelParse(oneOption);

                        if (orLogic && oneOption.Contains("ЗОЛОТО >=") && (level <= Character.Protagonist.Gold))
                            return true;

                        if (oneOption.Contains("ЗОЛОТО >=") && (level > Character.Protagonist.Gold))
                            return false;

                        if (oneOption.Contains("ЗАКЛЯТИЙ >") && (level >= Character.Protagonist.Magicpoints))
                            return false;

                        if (oneOption.Contains("ЭЛИКСИР >") && (level >= Character.Protagonist.Elixir))
                            return false;

                        if (oneOption.Contains("ЗАКЛЯТИЙ (!воин) >") && ((level >= Character.Protagonist.Magicpoints) ||
                                (Character.Protagonist.Specialization == Character.SpecializationType.Warrior)))
                            return false;

                        if (oneOption.Contains("ВРЕМЯ ДЛЯ ЧТЕНИЯ >") && (level >= Character.Protagonist.TimeForReading))
                            return false;

                        if (oneOption.Contains("ДОВЕРИЕ >") && (level >= Character.Protagonist.ConneryTrust))
                            return false;
                        
                        if (oneOption.Contains("ДОВЕРИЕ <=") && (level < Character.Protagonist.ConneryTrust))
                            return false;
                    }
                    else if (oneOption.Contains("!"))
                    {
                        if (Game.Data.Triggers.Contains(oneOption.Replace("!", String.Empty).Trim()))
                            return false;
                    }

                    else if (orLogic && Game.Data.Triggers.Contains(oneOption.Trim()))
                        return true;

                    else if (!orLogic && !Game.Data.Triggers.Contains(oneOption.Trim()))
                        return false;
                }

                return (orLogic ? false : true);
            }
        }

        public List<string> DiceCheck()
        {
            List<string> diceCheck = new List<string> { };

            int dice = Game.Dice.Roll();
            
            diceCheck.Add(String.Format("На кубикe выпало: {0}", Game.Dice.Symbol(dice)));
            diceCheck.Add(dice % 2 == 0 ? "BIG|ЧЁТНОЕ ЧИСЛО!" : "BIG|НЕЧЁТНОЕ ЧИСЛО!");

            return diceCheck;
        }

        public List<string> MushroomsForConnery()
        {
            if (Character.Protagonist.ConneryTrust >= 6)
            {
                Character.Protagonist.ConneryHitpoints += 3;
                return new List<string> { "BIG|GOOD|Коннери хмыкнул и съел :)" };
            }
            else
                return new List<string> { "BIG|BAD|Коннери отказался :(" };
        }

        public List<string> FootwrapsReplacement()
        {
            Game.Option.Trigger("Legs", remove: true);

            return new List<string> { "BIG|GOOD|Вы успешно поменяли портянки :)" };
        }

        public List<string> FootwrapsDeadlyReplacement()
        {
            Character.Protagonist.Hitpoints += (Game.Data.Triggers.Contains("Legs") ? 4 : 2);

            return new List<string> { "BIG|GOOD|Вы успешно поменяли портянки :)" };
        }

        public List<string> CureSprain()
        {
            Character.Protagonist.Strength += 1;
            Character.Protagonist.Magicpoints -= 1;

            return new List<string> { "BIG|GOOD|Вы успешно вылечили растяжение" };
        }

        public List<string> ShareFood()
        {
            Character.Protagonist.FoodIsDivided = true;

            if (FoodSharing == FoodSharingType.KeepMyself)
                Character.Protagonist.Hitpoints += 5;

            else if (FoodSharing == FoodSharingType.ToHim)
                Character.Protagonist.ConneryHitpoints += 5;

            else
            {
                Character.Protagonist.Hitpoints += 3;
                Character.Protagonist.ConneryHitpoints += 3;
            }

            return new List<string> { "RELOAD" };
        }

        public List<string> DiceWounds()
        {
            List<string> diceCheck = new List<string> { };

            int dicesCount = (Dices == 0 ? 1 : Dices);
            int dices = 0;
            
            for (int i = 1; i <= dicesCount; i++)
            {
                int dice = Game.Dice.Roll();
                dices += dice;
                diceCheck.Add(String.Format("На {0} выпало: {1}", i, Game.Dice.Symbol(dice)));
            }

            if (DiceBonus != 0)
            {
                dices += DiceBonus;
                diceCheck.Add(String.Format("Добавляем {0} по условию", DiceBonus));
            }

            Character.Protagonist.Hitpoints -= dices;

            diceCheck.Add(String.Format("BIG|BAD|Вы потеряли жизней: {0}", dices));

            return diceCheck;
        }

        private bool EnemyLostFight(List<Character> FightEnemies, ref List<string> fight, bool connery = false)
        {
            if (FightEnemies.Where(x => x.Hitpoints > 0).Count() > 0)
                return false;
            else
            {
                fight.Add(String.Empty);

                if (connery)
                    fight.Add("BIG|GOOD|Коннери его добил, вы ПОБЕДИЛИ :)");
                else
                    fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");

                return true;
            }
        }

        private List<string> LostFight(List<string> fight)
        {
            fight.Add(String.Empty);
            fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");

            return fight;
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            int round = 1, golemRound = 4, incrementWounds = 2;
            bool warriorFight = (Character.Protagonist.Specialization == Character.SpecializationType.Warrior);
            bool poisonBlade = false;

            if (Character.Protagonist.PoisonBlade > 0)
            {
                poisonBlade = true;
                Character.Protagonist.PoisonBlade = 0;
            }

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            while (true)
            {
                fight.Add(String.Format("HEAD|Раунд: {0}", round));

                if (!GolemFight && (Character.Protagonist.Specialization == Character.SpecializationType.Thrower))
                {
                    fight.Add("BOLD|Вы бросаете метательные ножи");

                    FightEnemies[0].Hitpoints -= 3;

                    fight.Add(String.Format("GOOD|{0} ранен метательными ножами и потерял 3 жизни", FightEnemies[0].Name));
                    fight.Add(String.Empty);
                }

                if (!String.IsNullOrEmpty(ReactionRound))
                {
                    string[] wounds = ReactionRound.Split(',');
                    fight.Add(String.Format("BOLD|{0} пытаются нанести вам дополнительный урон", wounds[1].TrimStart()));

                    if (!GoodReaction(ref fight))
                    {
                        int wound = int.Parse(wounds[0]);
                        Character.Protagonist.Hitpoints -= wound;

                        fight.Add(String.Format("BAD|{0} нанесли дополнительный урон: {1}", wounds[1].TrimStart(), wound));

                        if (Character.Protagonist.Hitpoints <= 0)
                            return LostFight(fight);
                    }
                    else
                        fight.Add(String.Format("BOLD|{0} не преуспели", wounds[1].TrimStart()));

                    fight.Add(String.Empty);
                }

                foreach (Character enemy in FightEnemies.Where(x => x.Hitpoints > 0))
                {

                    fight.Add(String.Format("{0} (жизни: {1})", enemy.Name, enemy.Hitpoints));

                    if (!String.IsNullOrEmpty(ConneryAttacks))
                    {
                        string[] bonus = ConneryAttacks.Split(',');

                        if ((bonus.Length < 2) || (round > int.Parse(bonus[1])))
                        {
                            int conneryAttack = int.Parse(bonus[0]);

                            enemy.Hitpoints -= conneryAttack;

                            fight.Add(String.Format("GOOD|{0} ранен атакой Коннери", enemy.Name, conneryAttack));

                            if (EnemyLostFight(FightEnemies, ref fight, connery: true))
                                return fight;
                        }
                    }

                    int firstHeroRoll = Game.Dice.Roll();
                    int secondHeroRoll = Game.Dice.Roll();
                    int heroHitStrength = firstHeroRoll + secondHeroRoll + Character.Protagonist.Strength;

                    fight.Add(String.Format(
                        "Ваш удар: {0} + {1} + {2} = {3}",
                        Game.Dice.Symbol(firstHeroRoll), Game.Dice.Symbol(secondHeroRoll),
                        Character.Protagonist.Strength, heroHitStrength));

                    int firstEnemyRoll = Game.Dice.Roll();
                    int secondEnemyRoll = Game.Dice.Roll();
                    int enemyHitStrength = firstEnemyRoll + secondEnemyRoll + enemy.Strength;

                    fight.Add(String.Format(
                            "Его удар: {0} + {1} + {2} = {3}",
                            Game.Dice.Symbol(firstEnemyRoll), Game.Dice.Symbol(secondEnemyRoll),
                            enemy.Strength, enemyHitStrength));

                    bool zombieWound = false;

                    if (ZombieFight && (heroHitStrength > enemyHitStrength))
                    {
                        int dice = Game.Dice.Roll();
                        zombieWound = dice % 2 == 0;
                        fight.Add(String.Format("Бросок на пробитие: {0} - {1}", Game.Dice.Symbol(dice), (zombieWound ? "чёт" : "нечет")));

                        if (warriorFight)
                            zombieWound = true;
                    }

                    bool lightningLunge = (warriorFight && (firstHeroRoll == secondHeroRoll) && !Game.Data.Triggers.Contains("EvilEye"));

                    if (ZombieFight && (heroHitStrength > enemyHitStrength) && !zombieWound)
                        fight.Add("BOLD|Вы не смогли пробить до кости");

                    else if (GolemFight && (heroHitStrength > enemyHitStrength))
                        fight.Add("BOLD|Вы отбили все атаки");

                    else if (warriorFight && (firstHeroRoll == secondHeroRoll) && (firstHeroRoll == 6))
                    {
                        fight.Add("BOLD|Вы сделали 'Крыло ястреба'!");

                        if (enemy.Hitpoints > 6)
                        {
                            enemy.Hitpoints /= 2;
                            fight.Add(String.Format("GOOD|{0} ранен на половину своих жизней", enemy.Name));
                        }
                        else
                        {
                            enemy.Hitpoints = 0;
                            fight.Add(String.Format("GOOD|{0} убит наповал", enemy.Name));

                            if (EnemyLostFight(FightEnemies, ref fight))
                                return fight;
                        }
                    }

                    else if ((heroHitStrength > enemyHitStrength) || lightningLunge)
                    {
                        if (lightningLunge)
                            fight.Add("BOLD|Вы сделали 'Молниеносный выпад'!");

                        fight.Add(String.Format("GOOD|{0} ранен", enemy.Name));

                        if (String.IsNullOrEmpty(ReactionHit))
                        {
                            int wound = (AttackWounds > 0 ? AttackWounds : 2);

                            if (poisonBlade)
                            {
                                wound += 2;
                                fight.Add(String.Format("Вы нанесли урон ядом: {0}", wound));
                            }

                            enemy.Hitpoints -= wound;
                        }
                        else
                        {
                            string[] wounds = ReactionHit.Split('-');
                            int wound = int.Parse(GoodReaction(ref fight) ? wounds[0] : wounds[1]);

                            enemy.Hitpoints -= wound;

                            fight.Add(String.Format("Вы нанесли урон: {0}", wound));
                        }
                        
                        if (EnemyLostFight(FightEnemies, ref fight))
                            return fight;
                    }
                    else if (heroHitStrength < enemyHitStrength)
                    {
                        fight.Add(String.Format("BAD|{0} ранил вас", enemy.Name));

                        if (!String.IsNullOrEmpty(ReactionWounds))
                        {
                            string[] wounds = ReactionWounds.Split('-');
                            int wound = int.Parse(GoodReaction(ref fight) ? wounds[0] : wounds[1]);

                            Character.Protagonist.Hitpoints -= wound;

                            fight.Add(String.Format("{0} нанёс урон: {1}", enemy.Name, wound));
                        }
                        else if (IncrementWounds)
                        {
                            fight.Add(String.Format("{0} нанёс урон: {1}", enemy.Name, incrementWounds));

                            Character.Protagonist.Hitpoints -= incrementWounds;

                            incrementWounds += 1;
                        }
                        else
                            Character.Protagonist.Hitpoints -= 2;

                        if (Character.Protagonist.Hitpoints <= 0)
                            return LostFight(fight);
                    }
                    else
                        fight.Add("BOLD|Ничья в раунде");

                    if (GolemFight && (golemRound > 0))
                        golemRound -= 1;

                    else if (GolemFight)
                    {
                        golemRound = 4;

                        fight.Add("BOLD|Коннери улучил момент для удара");

                        if (!GoodReaction(ref fight))
                        {
                            fight.Add("BAD|Вы не смогли прикрыть Коннери и он ранен");
                            Character.Protagonist.ConneryHitpoints -= 2;

                            if (Character.Protagonist.ConneryHitpoints <= 0)
                                return LostFight(fight);
                        }
                        else
                        {
                            fight.Add(String.Format("GOOD|{0} ранен атакой Коннери", enemy.Name));
                            enemy.Hitpoints -= 1;

                            if (EnemyLostFight(FightEnemies, ref fight, connery: true))
                                return fight;
                        }
                    }

                    if ((OnlyRounds > 0) && (OnlyRounds <= round))
                    {
                        fight.Add("BOLD|Отведённые на бой раунды истекли.");
                        return fight;
                    }

                    if ((RoundsToWin > 0) && (RoundsToWin <= round))
                    {
                        fight.Add("BAD|Отведённые на победу раунды истекли.");
                        return LostFight(fight);
                    }

                    fight.Add(String.Empty);
                }

                round += 1;
            }
        }
    }
}
