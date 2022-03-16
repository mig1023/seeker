using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.Cyberpunk
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;
        private static Random rand = new Random();

        public string Stat { get; set; }

        public override List<string> Status() => new List<string>
        {
            String.Format("Планирование: {0}", protagonist.Planning),
            String.Format("Подготовка: {0}", protagonist.Preparation),
            String.Format("Везение: {0}", protagonist.Luck),
        };

        public override List<string> Representer()
        {
            if (String.IsNullOrEmpty(Text))
            {
                string line = "Проверка: ";

                foreach (string stat in Stat.Split(','))
                    line += String.Format("{0} + ", Constants.CharactersParams()[stat.Trim()]);

                return new List<string> { line.TrimEnd(' ', '+') };
            }
            else
                return new List<string> { Text };
        }
            
        public override bool CheckOnlyIf(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains(">") || option.Contains("<"))
            {
                int level = Game.Services.LevelParse(option);

                if (option.Contains("ПЛАНИРОВАНИЕ <=") && (level < protagonist.Planning))
                    return false;

                else if (option.Contains("ПОДГОТОВКА <=") && (level < protagonist.Preparation))
                    return false;

                else if (option.Contains("ВЕЗЕНИЕ <=") && (level < protagonist.Luck))
                    return false;

                return true;
            }
            else
            {
                return CheckOnlyIfTrigger(option);
            }
        }

        private int PercentageDice() => rand.Next(100) + 1;

        public List<string> Test()
        {
            List<string> test = new List<string>();

            int paramsLevel = 0;

            string[] stats = Stat.Split(',');

            foreach (string stat in stats)
            {
                int param = GetProperty(protagonist, stat.Trim());
                paramsLevel += param;

                test.Add(String.Format("{0}: {1}", Constants.CharactersParams()[stat.Trim()], param));
            }

            test.Add(String.Format("ИТОГО: {0}", paramsLevel));

            int dice = PercentageDice();

            test.Add(String.Format("BOLD|Бросок кубика: {0}", dice));

            bool result = dice <= paramsLevel;
            string comparison = Game.Services.Сomparison(dice, paramsLevel);

            test.Add(String.Format("Бросок {0} суммы параметров, равной {1}", comparison, paramsLevel));

            test.Add(Result(result, "УСПЕХ|НЕУДАЧА"));

            return test;
        }
    }
}
