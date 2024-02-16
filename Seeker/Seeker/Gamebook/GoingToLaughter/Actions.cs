using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.GoingToLaughter
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public new static Actions StaticInstance = new Actions();
        public new static Actions GetInstance() => StaticInstance;
        private static Character protagonist = Character.Protagonist;

        public bool Advantage { get; set; }
        public bool Disadvantage { get; set; }

        public string Target { get; set; }
        public int Dices { get; set; }
        public int DiceBonus { get; set; }
        public int ResultBonus { get; set; }
        public bool DiceOfDice { get; set; }

        public List<string> Get()
        {
            if (Advantage)
            {
                protagonist.Advantages.Add(this.Button);
                protagonist.Balance += 1;
            }
            else if (Disadvantage)
            {
                protagonist.Disadvantages.Add(this.Button);
                protagonist.Balance -= 1;
            }

            return new List<string> { "RELOAD" };
        }

        public override List<string> Status() => new List<string>
        {
            $"Героизм: {protagonist.Heroism}",
            $"Злодеяние: {protagonist.Villainy}",
            $"Шутовство: {protagonist.Buffoonery}",
            $"Вдохновение: {protagonist.Inspiration}",
        };

        public override string ButtonText()
        {
            if (Type == "DiceValues")
            {
                return "Кинуть кубик" + (DiceOfDice || (Dices > 0) ? "и" : String.Empty);
            }
            else
            {
                return Button;
            }
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText)
        {
            if (protagonist.Buffoonery <= 0)
            {
                toEndParagraph = 1392;
                toEndText = "Это уже другая история...";

                return true;
            }
            else
            {
                toEndParagraph = 0;
                toEndText = String.Empty;

                return false;
            }
        }

        private bool Incompatible(string disadvantage)
        {
            if (!Constants.IncompatiblesDisadvantages.ContainsKey(disadvantage))
                return false;

            string incompatibles = Constants.IncompatiblesDisadvantages[disadvantage];

            foreach (string incompatible in incompatibles.Split(','))
            {
                bool isAdvantages = protagonist.Advantages.Contains(incompatible.Trim());
                bool idDisadvantages = protagonist.Disadvantages.Contains(incompatible.Trim());

                if (isAdvantages || idDisadvantages)
                    return true;
            }

            return false;
        }
            
        public override bool IsButtonEnabled(bool secondButton = false)
        {
            if (Advantage && protagonist.Advantages.Contains(this.Button))
            {
                return false;
            }
            else if (Disadvantage && (protagonist.Balance == 0))
            {
                return false;
            }
            else if (Disadvantage && Incompatible(this.Button))
            {
                return false;
            }
            else if (Disadvantage && protagonist.Disadvantages.Contains(this.Button))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains("|"))
            {
                foreach (string oneOption in option.Split('|'))
                {
                    if (protagonist.Advantages.Contains(oneOption.Trim()) || protagonist.Disadvantages.Contains(oneOption.Trim()))
                        return true;
                }

                return false;
            }
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    List<string> advantages = protagonist.Disadvantages;
                    List<string> disadvantages = protagonist.Disadvantages;

                    if (oneOption.Contains(">") || oneOption.Contains("<"))
                    {
                        int level = Game.Services.LevelParse(oneOption);

                        if (oneOption.Contains("БАЛАНС <=") && (level < protagonist.Balance))
                            return false;

                        if (oneOption.Contains("ГЕРОИЗМ >=") && (level > protagonist.Heroism))
                            return false;

                        if (oneOption.Contains("ЗЛОДЕЙСТВО >=") && (level > protagonist.Villainy))
                            return false;

                        if (oneOption.Contains("ЗЛОДЕЙСТВО <") && (level <= protagonist.Villainy))
                            return false;

                        if (oneOption.Contains("ПРЕДЛОЖЕНИЕ >=") && (level > protagonist.AbubakarOffer))
                            return false;

                        if (oneOption.Contains("ПРЕДЛОЖЕНИЕ <") && (level <= protagonist.AbubakarOffer))
                            return false;
                    }
                    else if (oneOption.Contains("!"))
                    {
                        if (disadvantages.Contains(oneOption.Trim().Replace("!", String.Empty).Trim()))
                            return false;
                    }
                    else if (!advantages.Contains(oneOption.Trim()) && !disadvantages.Contains(oneOption.Trim()))
                    {
                        return false;
                    }
                };

                return true;
            }
        }

        public List<string> HeroismCheck()
        {
            List<string> luckCheck = new List<string> { $"Уровень героизма: {protagonist.Heroism}." };

            if (protagonist.Heroism >= 5)
            {
                luckCheck.Add("В броске даже нет необходимости!");
                luckCheck.Add("BIG|GOOD|УСПЕХ :)");

                return luckCheck;
            }
            else
            {
                int level = 6 - protagonist.Heroism;
                int dice = Game.Dice.Roll();

                luckCheck.Add($"Для прохождения проверки нужно выбросить {level} или больше.");
                luckCheck.Add($"Бросок: {Game.Dice.Symbol(dice)}");
                luckCheck.Add(Result(dice >= level, "УСПЕХ|НЕУДАЧА"));

                return luckCheck;
            }
        }

        public List<string> DiceValues()
        {
            List<string> diceValues = new List<string> { };

            int dicesResult = 0;
            int dicesCount = (Dices > 0 ? Dices : 1);

            if (DiceOfDice)
            {
                dicesCount = Game.Dice.Roll();
                diceValues.Add($"Количество кубиков: {Game.Dice.Symbol(dicesCount)}");
                diceValues.Add(String.Empty);
            }

            for (int i = 1; i <= dicesCount; i++)
            {
                string bonus = String.Empty;
                int dice = Game.Dice.Roll();
                dicesResult += dice;

                if (DiceBonus > 0)
                {
                    dicesResult += DiceBonus;
                    bonus = $" + {DiceBonus} по условию";
                }

                diceValues.Add($"На {i} выпало: {Game.Dice.Symbol(dice)}{bonus}");
            }

            if (ResultBonus > 0)
            {
                dicesResult += ResultBonus;
                diceValues.Add($" +{ResultBonus} по условию");
            }

            SetProperty(protagonist, Target, (GetProperty(protagonist, Target) + dicesResult));

            diceValues.Add($"BIG|BOLD|Вы добавили +{dicesResult} к {Constants.ParamNames[Target]}");

            return diceValues;
        }
    }
}
