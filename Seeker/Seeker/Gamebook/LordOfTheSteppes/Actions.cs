﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.LordOfTheSteppes
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public string Stat { get; set; }
        public int StatStep { get; set; }
        public Character.SpecialTechniques SpecialTechnique { get; set; }

        public int Dices { get; set; }
        public bool Odd { get; set; }
        public bool Initiative { get; set; }

        public List<Character> Allies { get; set; }
        public List<Character> Enemies { get; set; }
        public int RoundsToWin { get; set; }
        public int WoundsToWin { get; set; }
        public bool GroupFight { get; set; }
        public bool NotToDeath { get; set; }
        public int Coherence { get; set; }
        public bool StoneGuard { get; set; }

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (!String.IsNullOrEmpty(Stat))
            {
                int diff = GetProperty(protagonist, Stat) - Constants.GetStartValues[Stat];
                string diffLine = diff > 0 ? $" (+{diff})" : String.Empty;

                return new List<string> { $"{Head}{diffLine}" };
            }
            else if (Price > 0)
            {
                string coins = Game.Services.CoinsNoun(Price, "монета", "монеты", "монет");
                return new List<string> { $"{Head}, {Price} {coins}" };
            }
            else if (!String.IsNullOrEmpty(Head) || (Type == "Get"))
            {
                return new List<string> { Head };
            }
            else if (Enemies == null)
            {
                return enemies;
            }

            if ((Allies != null) && GroupFight)
            {
                foreach (Character ally in Allies)
                {
                    if (ally.Name == protagonist.Name)
                    {
                        enemies.Add($"Вы\n" +
                            $"нападение {protagonist.Attack}  " +
                            $"защита {protagonist.Defence}  " +
                            $"жизнь {protagonist.Endurance}  " +
                            $"инициатива {protagonist.Initiative}" +
                            $"{protagonist.GetSpecialTechniques()}");
                    }
                    else
                    {
                        enemies.Add(String.Format("{0}\nнападение {1}  защита {2}  жизнь {3}  инициатива {4}{5}",
                            ally.Name, ally.Attack, ally.Defence, ally.Endurance, ally.Initiative, ally.GetSpecialTechniques()));
                    }
                }

                enemies.Add("SPLITTER|против");
            }

            foreach (Character enemy in Enemies)
                enemies.Add(String.Format("{0}\nнападение {1}  защита {2}  жизнь {3}  инициатива {4}{5}",
                    enemy.Name, enemy.Attack, enemy.Defence, enemy.Endurance, enemy.Initiative, enemy.GetSpecialTechniques()));

            return enemies;
        }

        public override string ButtonText()
        {
            if (!String.IsNullOrEmpty(Button))
                return Button;

            switch (Type)
            {
                case "DiceCheck":
                    return "Кинуть кубик" + (Dices > 0 ? "и" : String.Empty);

                case "Fight":
                    return "Сражаться";

                case "Get":
                    return "Купить";

                default:
                    return Button;
            }
        }

        public override List<string> Status() => new List<string>
        {
            String.Format("Жизнь: {0}/{1}", protagonist.Endurance, protagonist.MaxEndurance),
            String.Format("Монеты: {0}", protagonist.Coins),
        };

        public override List<string> AdditionalStatus() => new List<string>
        {
            String.Format("Нападение: {0}", protagonist.Attack),
            String.Format("Защита: {0}", protagonist.Defence),
            String.Format("Инициатива: {0}", protagonist.Initiative),
        };

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(protagonist.Endurance, out toEndParagraph, out toEndText);

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            bool disabledByChoosed = (SpecialTechnique != Character.SpecialTechniques.Nope) &&
                (protagonist.SpecialTechnique.Count > 0);

            bool disabledByBonusesRemove = !String.IsNullOrEmpty(Stat) &&
                ((GetProperty(protagonist, Stat) - Constants.GetStartValues[Stat]) <= 0) && secondButton;

            bool disabledByBonusesAdd = (!String.IsNullOrEmpty(Stat)) && (protagonist.Bonuses <= 0) && !secondButton;
            bool disabledByPrice = (Price > 0) && (protagonist.Coins < Price);
            bool disabledByUsed = ((Price > 0) ||(Price < 0)) && Used;

            return !(disabledByChoosed || disabledByBonusesAdd || disabledByBonusesRemove || disabledByPrice || disabledByUsed);
        }

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (protagonist.SpecialTechnique.Where(x => option == x.ToString()).Count() > 0)
            {
                return true;
            }
            else if (option.Contains("|"))
            {
                return option.Split('|').Where(x => Game.Option.IsTriggered(x.Trim())).Count() > 0;
            }
            else
            {
                if (option.Contains(">") || option.Contains("<"))
                    return !(option.Contains("МОНЕТ >=") && (int.Parse(option.Split('=')[1]) > protagonist.Coins));
                else
                    return Game.Option.IsTriggered(option);
            }
        }

        public List<string> Get()
        {
            if ((SpecialTechnique != Character.SpecialTechniques.Nope) && (protagonist.SpecialTechnique.Count == 0))
            {
                protagonist.SpecialTechnique.Add(SpecialTechnique);
            }
            else if ((StatStep > 0) && (protagonist.Bonuses >= 0))
            {
                ParamChange();
            }
            else if (((Price > 0) || (Price < 0)) && (protagonist.Coins >= Price))
            {
                protagonist.Coins -= Price;

                if (!Multiple)
                    Used = true;

                if (BenefitList != null)
                {
                    foreach (Modification modification in BenefitList)
                        modification.Do();
                }
            }

            return new List<string> { "RELOAD" };
        }

        public List<string> Decrease() => ParamChange(decrease: true);

        private List<string> ParamChange(bool decrease = false)
        {
            int currentStat = GetProperty(protagonist, Stat);

            currentStat += StatStep * (decrease ? -1 : 1);

            SetProperty(protagonist, "Max" + Stat, currentStat);
            SetProperty(protagonist, Stat, currentStat);

            protagonist.Bonuses += (decrease ? 1 : -1);

            return new List<string> { "RELOAD" };
        }

        public List<string> FunnyFight()
        {
            List<string> fight = new List<string>();

            const int ENEMY_STRENGTH = 6;

            int protagonistWounds = 0, enemyWounds = 0;

            while (true)
            {
                Game.Dice.DoubleRoll(out int firstRoll, out int secondRoll);
                int protagonistStrength = firstRoll + secondRoll + protagonist.Attack;

                fight.Add(String.Format("Ваша сила удара: {0} + {1} + {2} = {3}",
                    Game.Dice.Symbol(firstRoll), Game.Dice.Symbol(secondRoll), protagonist.Attack, protagonistStrength));

                Game.Dice.DoubleRoll(out firstRoll, out secondRoll);
                int enemyStrength = firstRoll + secondRoll + ENEMY_STRENGTH;

                fight.Add(String.Format("Cила удара удальца: {0} + {1} + {2} = {3}",
                    Game.Dice.Symbol(firstRoll), Game.Dice.Symbol(secondRoll), ENEMY_STRENGTH, enemyStrength));

                if (protagonistStrength > enemyStrength)
                {
                    fight.Add("GOOD|Вы нанесли ему удар");

                    enemyWounds += 1;

                    if (enemyWounds >= 3)
                    {
                        fight.Add(String.Empty);
                        fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");
                        return fight;
                    }
                }
                else if (protagonistStrength < enemyStrength)
                {
                    fight.Add("BAD|Он нанёс вам удар");

                    protagonistWounds += 1;

                    if (protagonistWounds >= 4)
                    {
                        fight.Add(String.Empty);
                        fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");
                        return fight;
                    }
                }
                else
                    fight.Add("Вы парировали удары друг друга");

                fight.Add(String.Empty);
            }
        }

        public List<string> GameOfDice()
        {
            if (protagonist.Coins < 5)
                return new List<string> { "BAD|У вас не достаточно денег, чтобы играть..." };

            List<string> diceGame = new List<string> { };

            int myResult, enemyResult;

            do
            {
                Game.Dice.DoubleRoll(out int hisFirstDice, out int hisSecondDice);
                enemyResult = hisFirstDice + hisSecondDice + 4;

                diceGame.Add(String.Format("Он бросил: {0} + {1} = {2}",
                    Game.Dice.Symbol(hisFirstDice), Game.Dice.Symbol(hisSecondDice), enemyResult));

                Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
                myResult = firstDice + secondDice;

                diceGame.Add(String.Format("Вы бросили: {0} + {1} = {2}",
                    Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), myResult));

                diceGame.Add(String.Empty);
            }
            while (myResult == enemyResult);

            if (myResult > enemyResult)
            {
                diceGame.Add("BIG|GOOD|ВЫ ВЫИГРАЛИ 5 МОНЕТ:)");
                protagonist.Coins += 5;
            }
            else
            {
                diceGame.Add("BIG|BAD|ПРОИГРАЛИ 5 МОНЕТ :(");
                protagonist.Coins -= 5;
            }

            return diceGame;
        }

        public List<string> DiceCheck()
        {
            List<string> diceCheck = new List<string> { };

            int firstDice = Game.Dice.Roll();
            int dicesResult = firstDice;

            string size = (Odd ? String.Empty : "BIG|");

            if (Dices == 1)
            {
                diceCheck.Add(String.Format("{0}На кубикe выпало: {1}", size, Game.Dice.Symbol(firstDice)));
            }
            else
            {
                int secondDice = Game.Dice.Roll();
                dicesResult += secondDice + (Initiative ? protagonist.Initiative : 0);
                string initLine = (Initiative ? String.Format(" + {0} Инициатива", protagonist.Initiative) : String.Empty);

                diceCheck.Add(String.Format("{0}На кубиках выпало: {1} + {2}{3} = {4}",
                    size, Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), initLine, dicesResult));
            }

            if (Odd)
                diceCheck.Add(dicesResult % 2 == 0 ? "BIG|ЧЁТНОЕ ЧИСЛО!" : "BIG|НЕЧЁТНОЕ ЧИСЛО!");

            return diceCheck;
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            int iProtagonist, iEnemy, round = 1, enemyWounds = 0;
            string protagonistLine, enemyLine, iTemplate = "{0} (инициатива {1})";

            List<Character> FightAllies = new List<Character>();
            List<Character> FightEnemies = new List<Character>();
            List<Character> FightAll = new List<Character>();
            List<Character> FightOrder = null;
            Dictionary<string, int> WoundsCount = new Dictionary<string, int>();
            Dictionary<string, List<int>> AttackStory = new Dictionary<string, List<int>>();

            foreach (Character enemy in Enemies)
            {
                Character enemyClone = enemy.Clone();
                FightEnemies.Add(enemyClone);
                FightAll.Add(enemyClone);
            };

            if (Allies == null)
            {
                FightAllies.Add(protagonist);
                FightAll.Add(protagonist);
            }
            else
            {
                foreach (Character ally in Allies)
                {
                    if (ally.Name == protagonist.Name)
                    {
                        FightAllies.Add(protagonist);
                        FightAll.Add(protagonist);
                    }
                    else
                    {
                        Character allyClone = ally.Clone();
                        FightAllies.Add(allyClone);
                        FightAll.Add(allyClone);
                    }
                }
            }

            foreach (Character fighter in FightAll)
            {
                WoundsCount[fighter.Name] = 0;
                AttackStory[fighter.Name] = new List<int>();
            }

            fight.Add("ОЧЕРЁДНОСТЬ УДАРОВ:");

            if (GroupFight)
            {
                FightOrder = FightAll.OrderByDescending(o => o.Initiative).ToList();

                foreach (Character fighter in FightOrder)
                    fight.Add(String.Format(iTemplate, fighter.Name, fighter.Initiative));
            }
            else
            {
                bool firstStrike = protagonist.SpecialTechnique.Contains(Character.SpecialTechniques.FirstStrike);
                bool enemyFirstStrike = FightEnemies[0].SpecialTechnique.Contains(Character.SpecialTechniques.FirstStrike);
                bool enemyIgnoreFirstStrike = FightEnemies[0].SpecialTechnique.Contains(Character.SpecialTechniques.IgnoreFirstStrike);

                FightOrder = new List<Character>();

                do
                {
                    iProtagonist = Services.InitiativeAndDices(protagonist, out protagonistLine);
                    iEnemy = Services.InitiativeAndDices(FightEnemies[0], out enemyLine);
                }
                while (iProtagonist == iEnemy);

                FightOrder.Add(protagonist);

                if (firstStrike && !enemyFirstStrike && !enemyIgnoreFirstStrike)
                    Services.OutputInitiative(ref fight, FightEnemies, FightOrder, iTemplate, protagonistLine, enemyLine, special: true);

                else if (!firstStrike && enemyFirstStrike)
                    Services.OutputInitiative(ref fight, FightEnemies, FightOrder, iTemplate, protagonistLine, enemyLine, reverse: true, special: true);

                else if (iProtagonist > iEnemy)
                    Services.OutputInitiative(ref fight, FightEnemies, FightOrder, iTemplate, protagonistLine, enemyLine);

                else
                    Services.OutputInitiative(ref fight, FightEnemies, FightOrder, iTemplate, protagonistLine, enemyLine, reverse: true);
            }

            fight.Add(String.Empty);

            while (true)
            {
                fight.Add(String.Format("HEAD|BOLD|Раунд: {0}", round));

                int coherenceIndex = 0;

                protagonist.FightStyle = Services.ChooseFightStyle(ref fight, AttackStory, FightEnemies);

                foreach (Character fighter in FightOrder)
                {
                    if (fighter.Endurance <= 0)
                        continue;

                    Character enemy = Services.FindEnemy(fighter, FightAllies, FightEnemies);

                    if (enemy == null)
                        continue;

                    fight.Add(String.Empty);

                    if (GroupFight)
                        fight.Add(String.Format("BOLD|{0} выбрает противника для атаки: {1}", fighter.Name, enemy.Name));
                    else
                        fight.Add(String.Format("BOLD|{0} атакует", fighter.Name));

                    enemyWounds += Services.Attack(fighter, enemy, ref fight, FightAllies, ref WoundsCount, ref AttackStory, round,
                        coherenceIndex, Coherence, out bool reactionSuccess);

                    if (fighter.SpecialTechnique.Contains(Character.SpecialTechniques.TwoBlades))
                    {
                        fight.Add("Дополнительная атака (особый приём):");

                        if (reactionSuccess)
                            fight.Add(String.Format("{0}|Уклонение от атаки благодаря Реакции (особый приём)",
                                (FightAllies.Contains(enemy) ? "GOOD" : "BAD")));
                        else
                            enemyWounds += Services.Attack(fighter, enemy, ref fight, FightAllies, ref WoundsCount, ref AttackStory, round,
                                coherenceIndex, Coherence, out bool _, supplAttack: true);
                    }

                    if (FightEnemies.Contains(fighter))
                        coherenceIndex += 1;
                }

                bool enemyLost = FightEnemies.Where(x => (x.Endurance > 0) || (x.MaxEndurance == 0)).Count() == 0;

                if (enemyLost || ((WoundsToWin > 0) && (WoundsToWin <= enemyWounds)))
                {
                    fight.Add(String.Empty);
                    fight.Add("BIG|GOOD|ВЫ ПОБЕДИЛИ :)");
                    return fight;
                }

                bool allyLost = FightAllies.Where(x => x.Endurance > 0).Count() == 0;

                if (allyLost)
                {
                    fight.Add(String.Empty);
                    fight.Add("BIG|BAD|ВЫ ПРОИГРАЛИ :(");

                    if (NotToDeath)
                        protagonist.Endurance += 1;

                    return fight;
                }

                if ((RoundsToWin > 0) && (RoundsToWin <= round))
                {
                    fight.Add(String.Empty);
                    fight.Add("BAD|Отведённые на победу раунды истекли.");
                    fight.Add("BIG|BAD|ВЫ ПРОИГРАЛИ :(");
                    return fight;
                }

                fight.Add(String.Empty);

                round += 1;
            }
        }

        public override bool IsHealingEnabled() =>
            protagonist.Endurance < protagonist.MaxEndurance;

        public override void UseHealing(int healingLevel)
        {
            if (healingLevel == -1)
                protagonist.Endurance = protagonist.MaxEndurance;
            else
                protagonist.Endurance += healingLevel;
        }
    }
}
