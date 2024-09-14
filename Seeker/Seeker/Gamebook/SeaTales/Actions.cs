using Seeker.Gamebook.SeaTales.Parts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.SeaTales
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public string Dices { get; set; }

        public int Throws { get; set; }

        public int Heat { get; set; }

        public string Level { get; set; }

        public string Success { get; set; }

        public string Fail { get; set; }

        private static Dictionary<int, IParts> Parts = new Dictionary<int, IParts>();

        public IParts NewPart(int part)
        {
            switch(part)
            {
                case 1:
                    return new First();
                case 2:
                    return new Second();
                case 3:
                    return new Third();
                case 4:
                    return new Fourth();
            }

            return null;
        }

        public IParts GetPart()
        {
            var part = Constants.StoryPart();

            if (!Parts.ContainsKey(part))
                Parts[part] = NewPart(part);

            return Parts[part];
        }

        public override List<string> Status() => GetPart().Status();

        public override List<string> AdditionalStatus() => GetPart().AdditionalStatus();

        public List<int> GetTragetDices(string dices, out string dicesLine)
        {
            var targets = dices
                .Split(',')
                .Select(x => int.Parse(x))
                .ToList();

            dicesLine = String.Empty;

            if (targets.Count < 2)
            {
                dicesLine = Game.Dice.Symbol(targets[0]);
            }
            else
            {
                for (int i = 0; i < targets.Count; i += 1)
                {
                    if (i > 0)
                        dicesLine += i == targets.Count - 1 ? " или " : ", ";

                    dicesLine += Game.Dice.Symbol(targets[i]);
                }
            }

            return targets;
        }

        public override List<string> Representer() =>
            GetPart().Representer(this);

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GetPart().GameOver(out toEndParagraph, out toEndText);

        public List<string> RandomOption() =>
            GetPart().RandomOption();

        public List<string> Test() => GetPart().Test(this);
    }
}
