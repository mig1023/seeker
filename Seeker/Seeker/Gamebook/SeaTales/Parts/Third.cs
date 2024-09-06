using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.SeaTales.Parts
{
    class Third : IParts
    {
        public List<string> Status() => null;

        public List<string> AdditionalStatus() => new List<string>
        {
            $"Героизм: {Character.Protagonist.Heroism}",
            $"Алкоголизм: {Character.Protagonist.Alcoholism}",
            $"Авантюризм: {Character.Protagonist.Adventurism}",
            $"Вдохновение: {Character.Protagonist.Inspiration}",
            $"Плавание: {Character.Protagonist.Travel}",
            $"Репутация: {Character.Protagonist.Reputation}",
        };

        public List<string> Representer(Actions action)
        {
            string dices = GetTestLevel(out string _);
            List<int> _ = action.GetTragetDices(dices, out string dicesLine);

            return new List<string> { $"ПРОВЕРКА\nНужно выбросить {dicesLine}" };
        }

        public void GetLastSelected(out int id, out string selected)
        {
            List<string> lastSelected = Character.Protagonist.LastSelectedOption
                .Split('%')
                .ToList();

            id = int.Parse(lastSelected[0]);
            selected = lastSelected[1];
        }

        public bool Availability(string option)
        {
            if (String.IsNullOrEmpty(Character.Protagonist.LastSelectedOption))
            {
                return true;
            }
            else
            {
                GetLastSelected(out int id, out string selected);

                if (id != Game.Data.CurrentParagraphID)
                {
                    return true;
                }
                else
                {
                    return selected == option;
                }
            }
        }

        public bool IsButtonEnabled(Actions action)
        {
            if (action.Type != "RandomOption")
            {
                return true;
            }
            else if (String.IsNullOrEmpty(Character.Protagonist.LastSelectedOption))
            {
                return true;
            }
            else
            {
                GetLastSelected(out int id, out string _);
                return id != Game.Data.CurrentParagraphID;
            }
        }

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;

            if (Character.Protagonist.Alcoholism >= 10)
            {
                toEndParagraph = 720;
                toEndText = "Алкоголизм зашкалил...";
            }
            else if (Character.Protagonist.Heroism >= 110)
            {
                toEndParagraph = 940;
                toEndText = "Героизм зашкалил!!";
            }
            else if (Character.Protagonist.Adventurism <= 0)
            {
                toEndParagraph = 1050;
                toEndText = "Авантюризм зашкалил...";
            }
            else if (Character.Protagonist.Adventurism >= 10)
            {
                toEndParagraph = 830;
                toEndText = "Авантюризм зашкалил!!";
            }
            else if (Character.Protagonist.Travel >= 100)
            {
                toEndParagraph = 755;
                toEndText = "Плавание достигло своей цели!";
            }
            else if ((Character.Protagonist.Travel <= -100) && !Game.Option.IsTriggered("Параграф 888"))
            {
                toEndParagraph = 888;
                toEndText = "Плавание занесло куда-то не туда...";
            }
            else if (Character.Protagonist.Reputation <= -100)
            {
                toEndParagraph = 1160;
                toEndText = "Репутация зашкалила...";
            }
            else
            {
                toEndText = String.Empty;
                return false;
            }

            return true;
        }

        private string GetTestLevel(out string heroism)
        {
            heroism = "выше ";

            if (Character.Protagonist.Heroism > 100)
            {
                heroism += "100";
                return "2,3,4,5,6";
            }
            else if (Character.Protagonist.Heroism > 75)
            {
                heroism += "75";
                return "3,4,5,6";
            }
            else if (Character.Protagonist.Heroism > 50)
            {
                heroism += "50";
                return "4,5,6";
            }
            else if (Character.Protagonist.Heroism > 25)
            {
                heroism += "25";
                return "5,6";
            }
            else
            {
                heroism = "не превышает 25";
                return "6";
            }
        }

        private void AdditionalEffects(Actions action, ref List<string> test, string line, bool good)
        {
            if (String.IsNullOrEmpty(line))
                return;

            List<string> effects = line
                .Split(';')
                .Select(x => x.Trim())
                .ToList();

            foreach (string effect in effects)
            {
                if (effect.Contains(':'))
                {
                    string color = good ? "GOOD" : "BAD";
                    string[] element = effect.Split(':');
                    string property = element[0].Trim();
                    string value = element[1].Trim();
                    int currentValue = action.GetProperty(Character.Protagonist, property);
                    int bonus = int.Parse(value);

                    action.SetProperty(Character.Protagonist, property, currentValue + bonus);
                    test.Add($"{color}|{Constants.Properties[property]} {value}");
                }
                else
                {
                    test.Add(String.Empty);
                    test.Add($"{effect}");
                }
            }
        }

        public List<string> RandomOption()
        {
            List<string> options = Game.Option.GetTexts();

            string selected = options[Game.Dice.Roll() - 1];
            Character.Protagonist.LastSelectedOption = $"{Game.Data.CurrentParagraphID}%{selected}";

            return new List<string> { "RELOAD" };
        }

        public List<string> Test(Actions action)
        {
            List<string> test = new List<string>();

            string dices = GetTestLevel(out string heroism);
            List<int> targetDices = action.GetTragetDices(dices, out string dicesLine);

            test.Add($"Нужно выдержать проверку!");
            test.Add($"BOLD|Ввиду того, что Героизм {heroism} нужно выбросить {dicesLine}");

            int dice = Game.Dice.Roll();
            test.Add($"BIG|Бросаем кубик: {Game.Dice.Symbol(dice)}");

            if (targetDices.Contains(dice))
            {
                test.Add("BIG|GOOD|BOLD|Проверка УСПЕШНО пройдена! :)");
                AdditionalEffects(action, ref test, action.Success, good: true);

                Game.Buttons.Disable(String.Join(",", Constants.FailButtons));
            }
            else
            {
                test.Add("BIG|BAD|BOLD|Проверка ПРОВАЛЕНА :(");
                AdditionalEffects(action, ref test, action.Fail, good: false);

                Game.Buttons.Disable(String.Join(",", Constants.SuccessButtons));
            }

            return test;
        }
    }
}
