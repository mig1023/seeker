using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.GoingToLaughter
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
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
                Character.Protagonist.Advantages.Add(this.Button);
                Character.Protagonist.Balance += 1;
            }
            else if (Disadvantage)
            {
                Character.Protagonist.Disadvantages.Add(this.Button);
                Character.Protagonist.Balance -= 1;
            }

            return new List<string> { "RELOAD" };
        }

        public override List<string> Status() => new List<string>
        {
            $"Героизм: {Character.Protagonist.Heroism}",
            $"Злодеяние: {Character.Protagonist.Villainy}",
            $"Шутовство: {Character.Protagonist.Buffoonery}",
            $"Вдохновение: {Character.Protagonist.Inspiration}",
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
            if (Character.Protagonist.Buffoonery <= 0)
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
                bool isAdvantages = Character.Protagonist.Advantages.Contains(incompatible.Trim());
                bool idDisadvantages = Character.Protagonist.Disadvantages.Contains(incompatible.Trim());

                if (isAdvantages || idDisadvantages)
                    return true;
            }

            return false;
        }
            
        public override bool IsButtonEnabled(bool secondButton = false)
        {
            if (Advantage && Character.Protagonist.Advantages.Contains(this.Button))
            {
                return false;
            }
            else if (Disadvantage && (Character.Protagonist.Balance == 0))
            {
                return false;
            }
            else if (Disadvantage && Incompatible(this.Button))
            {
                return false;
            }
            else if (Disadvantage && Character.Protagonist.Disadvantages.Contains(this.Button))
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
                    if (Character.Protagonist.Advantages.Contains(oneOption.Trim()) || Character.Protagonist.Disadvantages.Contains(oneOption.Trim()))
                        return true;
                }

                return false;
            }
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    List<string> advantages = Character.Protagonist.Disadvantages;
                    List<string> disadvantages = Character.Protagonist.Disadvantages;

                    if (oneOption.Contains(">") || oneOption.Contains("<"))
                    {
                        int level = Game.Services.LevelParse(oneOption);

                        if (oneOption.Contains("БАЛАНС <=") && (level < Character.Protagonist.Balance))
                            return false;

                        if (oneOption.Contains("ГЕРОИЗМ >=") && (level > Character.Protagonist.Heroism))
                            return false;

                        if (oneOption.Contains("ЗЛОДЕЙСТВО >=") && (level > Character.Protagonist.Villainy))
                            return false;

                        if (oneOption.Contains("ЗЛОДЕЙСТВО <") && (level <= Character.Protagonist.Villainy))
                            return false;

                        if (oneOption.Contains("ПРЕДЛОЖЕНИЕ >=") && (level > Character.Protagonist.AbubakarOffer))
                            return false;

                        if (oneOption.Contains("ПРЕДЛОЖЕНИЕ <") && (level <= Character.Protagonist.AbubakarOffer))
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
            List<string> luckCheck = new List<string> { $"Уровень героизма: {Character.Protagonist.Heroism}." };

            if (Character.Protagonist.Heroism >= 5)
            {
                luckCheck.Add("В броске даже нет необходимости!");
                luckCheck.Add("BIG|GOOD|УСПЕХ :)");

                Game.Buttons.Disable("Бросок провален");

                return luckCheck;
            }
            else
            {
                int level = 6 - Character.Protagonist.Heroism;
                int dice = Game.Dice.Roll();
                bool okResult = dice >= level;

                luckCheck.Add($"Для прохождения проверки нужно выбросить {level} или больше.");
                luckCheck.Add($"Бросок: {Game.Dice.Symbol(dice)}");
                luckCheck.Add(Result(okResult, "УСПЕХ", "НЕУДАЧА"));

                Game.Buttons.Disable(okResult, "Бросок успешен", "Бросок провален");

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

            SetProperty(Character.Protagonist, Target, (GetProperty(Character.Protagonist, Target) + dicesResult));

            diceValues.Add($"BIG|BOLD|Вы добавили +{dicesResult} к {Constants.ParamNames[Target]}");

            return diceValues;
        }
    }
}
