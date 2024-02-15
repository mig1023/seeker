using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.LegendsAlwaysLie
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        public static Actions GetInstance() => StaticInstance;
        private static Character protagonist = Character.Protagonist;

        public enum FoodSharingType { KeepMyself, ToHim, FiftyFifty };

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

        public Abstract.IModification Damage { get; set; }

        public Character.SpecializationType? Specialization { get; set; }


        public override List<string> Representer()
        {
            if (Price > 0)
            {
                string gold = Game.Services.CoinsNoun(Price, "золотой", "золотых", "золотых");
                return new List<string> { $"{Head}, {Price} {gold}" };
            }
            else if (!String.IsNullOrEmpty(Head))
            {
                return new List<string> { Head };
            }

            List<string> enemies = new List<string>();

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                enemies.Add($"{enemy.Name}\nсила {enemy.Strength}  жизни {enemy.Hitpoints}");

            return enemies;
        }

        public override List<string> Status() => new List<string>
        {
            $"Жизни Коннери: {protagonist.ConneryHitpoints}/30",
            $"Доверие Коннери: {protagonist.ConneryTrust}",
        };

        public override List<string> AdditionalStatus() => new List<string>
        {
            $"Сила: {protagonist.Strength}",
            $"Жизни: {protagonist.Hitpoints}/30",
            $"Заклинаний: {protagonist.Magicpoints}",
            $"Золото: {protagonist.Gold}",
        };

        public override List<string> StaticButtons()
        {
            List<string> staticButtons = new List<string> { };

            int currentId = Game.Data.CurrentParagraphID;
            Abstract.IConstants consants = Game.Data.Constants;
            bool withoutStatic = consants.GetParagraphsWithoutStaticsButtons().Contains(currentId);

            if (withoutStatic)
                return staticButtons;

            bool healingSpell = (protagonist.Magicpoints > 0) && !Game.Option.IsTriggered("HealingSpellLost");

            if (healingSpell && !Game.Buttons.ExistsInParagraph(actionText: "Вылечить"))
            {
                if (protagonist.Hitpoints < 30)
                    staticButtons.Add("ЛЕЧИЛКА");

                if ((protagonist.ConneryHitpoints < 30) && (protagonist.Hitpoints > 2))
                    staticButtons.Add("ЛЕЧИЛКА ДЛЯ КОННЕРИ");
            }

            if ((protagonist.Elixir > 0) && (protagonist.Hitpoints < 30))
                staticButtons.Add("ЭЛИКСИР");

            return staticButtons;
        }

        public override bool StaticAction(string action)
        {
            if (action == "ЛЕЧИЛКА")
            {
                protagonist.Hitpoints += 6;
                protagonist.Magicpoints -= 1;
                
                return true;
            }
            else if (action == "ЛЕЧИЛКА ДЛЯ КОННЕРИ")
            {
                protagonist.ConneryHitpoints += 8;
                protagonist.Magicpoints -= 1;

                InjuriesBySpells();

                return true;
            }
            else if ((action == "ЭЛИКСИР") && (protagonist.Hitpoints < 30))
            {
                protagonist.Hitpoints = 30;
                protagonist.Elixir -= 1;

                return true;
            }

            return false;
        }

        public static void InjuriesBySpells() =>
            protagonist.Hitpoints -= (protagonist.Specialization == Character.SpecializationType.Wizard ? 1 : 2);

        public List<string> Reaction()
        {
            List<string> reaction = new List<string>();

            bool goodReaction = Reactions.Good(ref reaction);

            if (!goodReaction && Game.Option.IsTriggered("WardSave"))
            {
                reaction.Add("GOOD|Вы не смогли среагировать, но духи земли и камня не забывают добра: они помогли вам!");
                goodReaction = true;
                Game.Option.Trigger("WardSave", remove: true);
            }

            reaction.Add(Result(goodReaction, "СРЕАГИРОВАЛИ|НЕ СРЕАГИРОВАЛИ"));

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

            if (protagonist.Hitpoints <= 0)
            {
                toEndText = Output.Constants.GAMEOVER_TEXT;
            }
            else if (protagonist.ConneryHitpoints <= 0)
            {
                toEndText = "Коннери погиб, ваше путешествие окончено";
            }
            else if (protagonist.ConneryTrust <= 0)
            {
                toEndText = "Коннери потерял к вам всякое доверие, ваше путешествие окончено";
            }
            else
            {
                return false;
            }

            return true;
        }

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            bool bySpecButton = (Specialization != null) &&
                (protagonist.Specialization != Character.SpecializationType.Nope);

            bool byPrice = (Price > 0) && (protagonist.Gold < Price);
            bool byCureSprain = (Type == "CureSprain") && (protagonist.Magicpoints <= 0);

            bool byAlreadyDecided = (FoodSharing != null) && Game.Option.IsTriggered("FoodSharing");
            bool byFootwraps = ((Type == "FootwrapsDeadlyReplacement") ||
                (Type == "FootwrapsReplacement")) && protagonist.Footwraps <= 0;

            return !(bySpecButton || byPrice || byCureSprain || byAlreadyDecided || byFootwraps || Disabled);
        }

        public List<string> Get()
        {
            if ((Specialization != null) && (protagonist.Specialization == Character.SpecializationType.Nope))
            {
                protagonist.Specialization = Specialization ?? Character.SpecializationType.Nope;

                if (Specialization == Character.SpecializationType.Warrior)
                {
                    protagonist.Strength += 2;
                    protagonist.Magicpoints = 2;
                }
                else if (Specialization == Character.SpecializationType.Wizard)
                {
                    protagonist.Magicpoints = 5;
                }
                else
                {
                    protagonist.Strength += 1;
                    protagonist.Magicpoints = 3;
                }
            }
            else if ((Price > 0) && (protagonist.Gold >= Price))
            {
                protagonist.Gold -= Price;
            }

            return new List<string> { "RELOAD" };
        }

        public List<string> Sell()
        {
            Disabled = true;
            protagonist.Gold += Price;             

            return new List<string> { "RELOAD" };
        }

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains(";"))
            {
                string[] options = option.Split(';');

                int optionMustBe = int.Parse(options[0]);
                int optionCount = options.Where(x => Game.Option.IsTriggered(x.Trim())).Count();

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
                        int level = Game.Services.LevelParse(oneOption);

                        if (orLogic && oneOption.Contains("ЗОЛОТО >=") && (level <= protagonist.Gold))
                            return true;

                        if (oneOption.Contains("ЗОЛОТО >=") && (level > protagonist.Gold))
                            return false;

                        if (oneOption.Contains("ЗАКЛЯТИЙ >") && (level >= protagonist.Magicpoints))
                            return false;

                        if (oneOption.Contains("ЭЛИКСИР >") && (level >= protagonist.Elixir))
                            return false;

                        if (oneOption.Contains("ЗАКЛЯТИЙ (!воин) >") && ((level >= protagonist.Magicpoints) ||
                                (protagonist.Specialization == Character.SpecializationType.Warrior)))
                            return false;

                        if (oneOption.Contains("ВРЕМЯ ДЛЯ ЧТЕНИЯ >") && (level >= protagonist.TimeForReading))
                            return false;

                        if (oneOption.Contains("ДОВЕРИЕ >") && (level >= protagonist.ConneryTrust))
                            return false;
                        
                        if (oneOption.Contains("ДОВЕРИЕ <=") && (level < protagonist.ConneryTrust))
                            return false;

                        if (option.Contains("ВОИН") || option.Contains("МАГ") || option.Contains("МЕТАТЕЛЬ"))
                        {
                            string type = option.Replace("!", String.Empty);
                            Character.SpecializationType spec = Constants.GetSpecializationType()[type];

                            bool specialization = option.Contains("!") ?
                                (protagonist.Specialization != spec) : (protagonist.Specialization == spec);

                            return specialization;
                        }
                    }
                    else if (oneOption.Contains("!"))
                    {
                        if (Game.Option.IsTriggered(oneOption.Replace("!", String.Empty).Trim()))
                            return false;
                    }
                    else if (orLogic && Game.Option.IsTriggered(oneOption.Trim()))
                    {
                        return true;
                    }
                    else if (!orLogic && !Game.Option.IsTriggered(oneOption.Trim()))
                    {
                        return false;
                    }
                }

                return !orLogic;
            }
        }

        public List<string> DiceCheck()
        {
            List<string> diceCheck = new List<string> { };

            int dice = Game.Dice.Roll();
            
            diceCheck.Add($"На кубикe выпало: {Game.Dice.Symbol(dice)}");
            diceCheck.Add(dice % 2 == 0 ? "BIG|ЧЁТНОЕ ЧИСЛО!" : "BIG|НЕЧЁТНОЕ ЧИСЛО!");

            return diceCheck;
        }

        public List<string> MushroomsForConnery()
        {
            if (protagonist.ConneryTrust >= 6)
            {
                protagonist.ConneryHitpoints += 3;
                return new List<string> { "BIG|GOOD|Коннери хмыкнул и съел :)" };
            }
            else
            {
                return new List<string> { "BIG|BAD|Коннери отказался :(" };
            }
        }

        public List<string> FootwrapsReplacement()
        {
            Game.Option.Trigger("Legs", remove: true);

            return new List<string> { "BIG|GOOD|Вы успешно поменяли портянки :)" };
        }

        public List<string> FootwrapsDeadlyReplacement()
        {
            protagonist.Hitpoints += (Game.Option.IsTriggered("Legs") ? 4 : 2);

            return new List<string> { "BIG|GOOD|Вы успешно поменяли портянки :)" };
        }

        public List<string> CureSprain()
        {
            protagonist.Strength += 1;
            protagonist.Magicpoints -= 1;

            return new List<string> { "BIG|GOOD|Вы успешно вылечили растяжение" };
        }

        public List<string> ShareFood()
        {
            Game.Option.Trigger("FoodIsDivided");

            if (FoodSharing == FoodSharingType.KeepMyself)
            {
                protagonist.Hitpoints += 5;
            }
            else if (FoodSharing == FoodSharingType.ToHim)
            {
                protagonist.ConneryHitpoints += 5;
            }
            else
            {
                protagonist.Hitpoints += 3;
                protagonist.ConneryHitpoints += 3;
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
                diceCheck.Add($"На {i} выпало: {Game.Dice.Symbol(dice)}");
            }

            if (DiceBonus != 0)
            {
                dices += DiceBonus;
                diceCheck.Add($"Добавляем {DiceBonus} по условию");
            }

            protagonist.Hitpoints -= dices;

            diceCheck.Add($"BIG|BAD|Вы потеряли жизней: {dices}");

            return diceCheck;
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            int round = 1, golemRound = 4, incrementWounds = 2;
            bool warriorFight = (protagonist.Specialization == Character.SpecializationType.Warrior);
            bool poisonBlade = false;

            if (Game.Option.IsTriggered("PoisonBlade"))
            {
                poisonBlade = true;
                Game.Option.Trigger("PoisonBlade", remove: true);
            }

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            while (true)
            {
                fight.Add($"HEAD|BOLD|Раунд: {round}");

                if (!GolemFight && (protagonist.Specialization == Character.SpecializationType.Thrower) && (round == 1))
                {
                    fight.Add("BOLD|Вы бросаете метательные ножи");

                    FightEnemies[0].Hitpoints -= 3;

                    fight.Add($"GOOD|{FightEnemies[0].Name} ранен метательными ножами и потерял 3 жизни");
                    fight.Add(String.Empty);

                    if (Fights.EnemyLost(FightEnemies, ref fight))
                        return fight;
                }

                if (!String.IsNullOrEmpty(ReactionRound))
                {
                    string[] wounds = ReactionRound.Split(',');
                    fight.Add($"BOLD|{wounds[1].TrimStart()} пытаются нанести вам дополнительный урон");

                    if (!Reactions.Good(ref fight))
                    {
                        int wound = int.Parse(wounds[0]);
                        protagonist.Hitpoints -= wound;

                        fight.Add($"BAD|{wounds[1].TrimStart()} нанесли дополнительный урон: {wound}");

                        if (protagonist.Hitpoints <= 0)
                            return Fights.Lost(fight);
                    }
                    else
                    {
                        fight.Add($"BOLD|{wounds[1].TrimStart()} не преуспели");
                    }
                        
                    fight.Add(String.Empty);
                }

                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.Hitpoints <= 0)
                        continue;

                    fight.Add($"{enemy.Name} (жизни: {enemy.Hitpoints})");

                    if (!String.IsNullOrEmpty(ConneryAttacks))
                    {
                        string[] bonus = ConneryAttacks.Split(',');

                        if ((bonus.Length < 2) || (round > int.Parse(bonus[1])))
                        {
                            int conneryAttack = int.Parse(bonus[0]);

                            enemy.Hitpoints -= conneryAttack;

                            fight.Add($"GOOD|{enemy.Name} ранен атакой Коннери (-{conneryAttack})");

                            if (Fights.EnemyLost(FightEnemies, ref fight, connery: true))
                                return fight;
                        }
                    }

                    Game.Dice.DoubleRoll(out int firstProtagonistRoll, out int secondProtagonistRoll);
                    int protagonistHitStrength = firstProtagonistRoll + secondProtagonistRoll + protagonist.Strength;

                    fight.Add($"Ваш удар: " +
                        $"{Game.Dice.Symbol(firstProtagonistRoll)} + " +
                        $"{Game.Dice.Symbol(secondProtagonistRoll)} + " +
                        $"{protagonist.Strength} = {protagonistHitStrength}");

                    Game.Dice.DoubleRoll(out int firstEnemyRoll, out int secondEnemyRoll);
                    int enemyHitStrength = firstEnemyRoll + secondEnemyRoll + enemy.Strength;

                    fight.Add($"Его удар: " +
                        $"{Game.Dice.Symbol(firstEnemyRoll)} + " +
                        $"{Game.Dice.Symbol(secondEnemyRoll)} + " +
                        $"{enemy.Strength} = {enemyHitStrength}");

                    bool zombieWound = false;

                    if (ZombieFight && (protagonistHitStrength > enemyHitStrength))
                    {
                        int dice = Game.Dice.Roll();
                        zombieWound = dice % 2 == 0;
                        string odd = zombieWound ? "чёт" : "нечет";
                        fight.Add($"Бросок на пробитие: {Game.Dice.Symbol(dice)} - {odd}");

                        if (warriorFight)
                            zombieWound = true;
                    }

                    bool doubleRoll = firstProtagonistRoll == secondProtagonistRoll;
                    bool lightningLunge = (warriorFight && doubleRoll && !Game.Option.IsTriggered("EvilEye"));

                    if (ZombieFight && (protagonistHitStrength > enemyHitStrength) && !zombieWound)
                    {
                        fight.Add("BOLD|Вы не смогли пробить до кости");
                    }
                    else if (GolemFight && (protagonistHitStrength > enemyHitStrength))
                    {
                        fight.Add("BOLD|Вы отбили все атаки");
                    }
                    else if (warriorFight && (firstProtagonistRoll == secondProtagonistRoll) && (firstProtagonistRoll == 6))
                    {
                        fight.Add("BOLD|Вы сделали 'Крыло ястреба'!");

                        if (enemy.Hitpoints > 6)
                        {
                            enemy.Hitpoints /= 2;
                            fight.Add($"GOOD|{enemy.Name} ранен на половину своих жизней");
                        }
                        else
                        {
                            enemy.Hitpoints = 0;
                            fight.Add($"GOOD|{enemy.Name} убит наповал");

                            if (Fights.EnemyLost(FightEnemies, ref fight))
                                return fight;
                        }
                    }

                    else if ((protagonistHitStrength > enemyHitStrength) || lightningLunge)
                    {
                        if (lightningLunge)
                            fight.Add("BOLD|Вы сделали 'Молниеносный выпад'!");

                        fight.Add($"GOOD|{enemy.Name} ранен");

                        if (String.IsNullOrEmpty(ReactionHit))
                        {
                            int wound = (AttackWounds > 0 ? AttackWounds : 2);

                            if (poisonBlade)
                            {
                                wound += 2;
                                fight.Add($"Вы нанесли урон ядом: {wound}");
                            }

                            enemy.Hitpoints -= wound;
                        }
                        else
                        {
                            string[] wounds = ReactionHit.Split('-');
                            int wound = int.Parse(Reactions.Good(ref fight) ? wounds[0] : wounds[1]);

                            enemy.Hitpoints -= wound;

                            fight.Add($"Вы нанесли урон: {wound}");
                        }
                        
                        if (Fights.EnemyLost(FightEnemies, ref fight))
                            return fight;
                    }
                    else if (protagonistHitStrength < enemyHitStrength)
                    {
                        fight.Add($"BAD|{enemy.Name} ранил вас");

                        if (!String.IsNullOrEmpty(ReactionWounds))
                        {
                            string[] wounds = ReactionWounds.Split('-');
                            int wound = int.Parse(Reactions.Good(ref fight) ? wounds[0] : wounds[1]);

                            protagonist.Hitpoints -= wound;

                            fight.Add($"{enemy.Name} нанёс урон: {wound}");
                        }
                        else if (IncrementWounds)
                        {
                            fight.Add($"{enemy.Name} нанёс урон: {incrementWounds}");

                            protagonist.Hitpoints -= incrementWounds;

                            incrementWounds += 1;
                        }
                        else
                        {
                            protagonist.Hitpoints -= 2;
                        }

                        if (protagonist.Hitpoints <= 0)
                            return Fights.Lost(fight);
                    }
                    else
                    {
                        fight.Add("BOLD|Ничья в раунде");
                    }

                    if (GolemFight && (golemRound > 0))
                    {
                        golemRound -= 1;
                    }
                    else if (GolemFight)
                    {
                        golemRound = 4;

                        fight.Add("BOLD|Коннери улучил момент для удара");

                        if (!Reactions.Good(ref fight))
                        {
                            fight.Add("BAD|Вы не смогли прикрыть Коннери и он ранен");
                            protagonist.ConneryHitpoints -= 2;

                            if (protagonist.ConneryHitpoints <= 0)
                                return Fights.Lost(fight);
                        }
                        else
                        {
                            fight.Add($"GOOD|{enemy.Name} ранен атакой Коннери");
                            enemy.Hitpoints -= 1;

                            if (Fights.EnemyLost(FightEnemies, ref fight, connery: true))
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
                        return Fights.Lost(fight);
                    }

                    fight.Add(String.Empty);
                }

                round += 1;
            }
        }
    }
}
