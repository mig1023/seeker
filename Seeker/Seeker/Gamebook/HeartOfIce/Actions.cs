using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.HeartOfIce
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protogonist = Character.Protagonist;

        public string RemoveTrigger { get; set; }
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
                string money = Game.Other.CoinsNoun(Price, "скад", "скада", "скадов");
                return new List<string> { String.Format("{0}, {1} скад{2}", Text, Price, money) };
            }
            else if (!String.IsNullOrEmpty(Text))
                return new List<string> { Text };

            else if (!String.IsNullOrEmpty(Skill))
                return new List<string> { Skill };

            else
                return new List<string>();
        }

        public override List<string> Status()
        {
            List<string> statusLines = new List<string>
            {
                String.Format("Здоровье: {0}", protogonist),
                String.Format("Деньги: {0}", protogonist),
            };

            if (protogonist.Food > 0)
                statusLines.Add(String.Format("Еда: {0}", protogonist.Food));

            if (protogonist.Shots > 0)
                statusLines.Add(String.Format("Выстрелов: {0}", protogonist.Shots));

            return statusLines;
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(protogonist.Life, out toEndParagraph, out toEndText);

        public List<string> Get()
        {
            if (Choice)
                protogonist.Chosen = true;

            if (!String.IsNullOrEmpty(Skill))
            {
                protogonist.Skills.Add(Skill);
                protogonist.SkillsValue -= 1;
            }

            if ((Price > 0) && (protogonist.Money >= Price))
            {
                protogonist.Money += ((Sell || SellIfAvailable) ? Price : (Price * -1));
                Game.Option.Trigger(RemoveTrigger, remove: true);

                if (SellIfAvailable && (SellType == "Пистолет"))
                    protogonist.Shots = 0;

                if (SellIfAvailable && (SellType == "Еда"))
                    protogonist.Food -= 1;
            }

            if (Split)
                protogonist.Split += 1;

            if (((Price > 0) || Split) && !Multiple)
                Used = true;

            if (BenefitList != null)
                foreach (Modification modification in BenefitList)
                    modification.Do();

            return new List<string> { "RELOAD" };
        }

        public override bool IsButtonEnabled()
        {
            bool disabledBySkills = (!String.IsNullOrEmpty(Skill) &&
                ((protogonist.SkillsValue <= 0) || protogonist.Skills.Contains(Skill)));

            bool disbledByChoice = (Choice && protogonist.Chosen);
            bool disabledByPrice = (Price > 0) && (protogonist.Money < Price);
            bool disabledBySplit = Split && (protogonist.Split >= 2);
            bool disabledByAvailable = SellIfAvailable && !Available();

            return !(disbledByChoice || disabledBySkills || disabledByPrice || disabledBySplit || disabledByAvailable || Used);
        }

        public bool Available()
        {
            if (!String.IsNullOrEmpty(RemoveTrigger))
                return Game.Data.Triggers.Contains(RemoveTrigger);

            else if (SellType == "Пистолет")
                return protogonist.Shots > 0;

            else if (SellType == "Еда")
                return protogonist.Food > 0;

            return false;
        }

        public override bool CheckOnlyIf(string option)
        {
            if (option.Contains("|"))
            {
                foreach (string oneOption in option.Split('|'))
                {
                    if (protogonist.Skills.Contains(oneOption.Trim()))
                        return true;

                    if (Game.Data.Triggers.Contains(oneOption.Trim()))
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
                        if (oneOption.Contains("ВЫСТРЕЛОВ >=") && (int.Parse(oneOption.Split('=')[1]) > protogonist.Shots))
                            return false;

                        if (oneOption.Contains("ДЕНЬГИ >=") && (int.Parse(oneOption.Split('=')[1]) > protogonist.Money))
                            return false;

                        else if (oneOption.Contains("ЕДА >=") && (int.Parse(oneOption.Split('=')[1]) > protogonist.Food))
                            return false;
                    }
                    else if (!Game.Data.Triggers.Contains(oneOption.Trim()) && !protogonist.Skills.Contains(oneOption.Trim()))
                        return false;
                }

                return true;
            }
        }
    }
}
