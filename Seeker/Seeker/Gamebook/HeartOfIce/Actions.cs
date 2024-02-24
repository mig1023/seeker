using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.HeartOfIce
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public string Untrigger { get; set; }
        public string Skill { get; set; }
        public bool Choice { get; set; }
        public bool Sell { get; set; }
        public bool SellIfAvailable { get; set; }
        public string SellType { get; set; }
        public bool Split { get; set; }

        public override List<string> Representer()
        {
            if (Price > 0)
            {
                string money = Game.Services.CoinsNoun(Price, "скад", "скада", "скадов");
                return new List<string> { $"{Head}, {Price} скад{money}" };
            }
            else if (!String.IsNullOrEmpty(Head))
            {
                return new List<string> { Head };
            }
            else if (!String.IsNullOrEmpty(Skill))
            {
                return new List<string> { Skill };
            }
            else
            {
                return new List<string>();
            }
        }

        public override List<string> Status()
        {
            List<string> statusLines = new List<string>
            {
                $"Здоровье: {Character.Protagonist.Life}/{Character.Protagonist.MaxLife}",
                $"Деньги: {Character.Protagonist.Money}",
            };

            if (Character.Protagonist.Food > 0)
                statusLines.Add($"Еда: {Character.Protagonist.Food}");

            if (Character.Protagonist.Shots > 0)
                statusLines.Add($"Выстрелов: {Character.Protagonist.Shots}");

            return statusLines;
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(Character.Protagonist.Life, out toEndParagraph, out toEndText);

        public List<string> Get()
        {
            if (Choice)
                Character.Protagonist.Chosen = true;

            if (!String.IsNullOrEmpty(Skill))
            {
                Character.Protagonist.Skills.Add(Skill);
                Character.Protagonist.SkillsValue -= 1;
            }

            if ((Price > 0) && (Character.Protagonist.Money >= Price))
            {
                Character.Protagonist.Money += ((Sell || SellIfAvailable) ? Price : (Price * -1));
                Game.Option.Trigger(Untrigger, remove: true);

                if (SellIfAvailable && (SellType == "Пистолет"))
                    Character.Protagonist.Shots = 0;

                if (SellIfAvailable && (SellType == "Еда"))
                    Character.Protagonist.Food -= 1;
            }

            if (Split)
                Character.Protagonist.Split += 1;

            if (((Price > 0) || Split) && !Multiple)
                Used = true;

            if (BenefitList != null)
            {
                foreach (Modification modification in BenefitList)
                    modification.Do();
            }

            return new List<string> { "RELOAD" };
        }

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            bool disabledBySkills = (!String.IsNullOrEmpty(Skill) &&
                ((Character.Protagonist.SkillsValue <= 0) || Character.Protagonist.Skills.Contains(Skill)));

            bool disbledByChoice = (Choice && Character.Protagonist.Chosen);
            bool disabledByPrice = (Price > 0) && (Character.Protagonist.Money < Price);
            bool disabledBySplit = Split && (Character.Protagonist.Split >= 2);
            bool disabledByAvailable = SellIfAvailable && !Available();

            return !(disbledByChoice || disabledBySkills || disabledByPrice || disabledBySplit || disabledByAvailable || Used);
        }

        public bool Available()
        {
            if (!String.IsNullOrEmpty(Untrigger))
            {
                return Game.Option.IsTriggered(Untrigger);
            }
            else if (SellType == "Пистолет")
            {
                return Character.Protagonist.Shots > 0;
            }
            else if (SellType == "Еда")
            {
                return Character.Protagonist.Food > 0;
            }

            return false;
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
                    if (Character.Protagonist.Skills.Contains(oneOption.Trim()))
                        return true;

                    if (Game.Option.IsTriggered(oneOption.Trim()))
                        return true;
                }

                return false;
            }
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains("="))
                    {
                        if (oneOption.Contains("ВЫСТРЕЛОВ >=") && (int.Parse(oneOption.Split('=')[1]) > Character.Protagonist.Shots))
                            return false;

                        if (oneOption.Contains("ДЕНЬГИ >=") && (int.Parse(oneOption.Split('=')[1]) > Character.Protagonist.Money))
                            return false;

                        else if (oneOption.Contains("ЕДА >=") && (int.Parse(oneOption.Split('=')[1]) > Character.Protagonist.Food))
                            return false;
                    }
                    else if (!Game.Option.IsTriggered(oneOption.Trim()) && !Character.Protagonist.Skills.Contains(oneOption.Trim()))
                        return false;
                }

                return true;
            }
        }
    }
}
