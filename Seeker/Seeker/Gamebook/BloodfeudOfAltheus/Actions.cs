using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.BloodfeudOfAltheus
{
    class Actions : Abstract.IActions
    {
        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string Aftertext { get; set; }
        public string Trigger { get; set; }
        public int Dices { get; set; }


        public List<string> Do(out bool reload, string action = "", bool trigger = false)
        {
            if (trigger)
                Game.Option.Trigger(Trigger);

            string actionName = (String.IsNullOrEmpty(action) ? ActionName : action);
            List<string> actionResult = typeof(Actions).GetMethod(actionName).Invoke(this, new object[] { }) as List<string>;

            reload = (actionResult.Count >= 1) && (actionResult[0] == "RELOAD");

            return actionResult;
        }

        public List<string> Representer() => new List<string> { };

        public List<string> Status()
        {
            List<string> statusLines = new List<string>
            {
                String.Format("Сила: {0}", Character.Protagonist.Strength),
                String.Format("Защита: {0}", Character.Protagonist.Defence),
                String.Format("Слава: {0}", Character.Protagonist.Glory),
                String.Format("Позор: {0}", Character.Protagonist.Shame),
            };

            return statusLines;
        }

        public List<string> AdditionalStatus()
        {
            List<string> statusLines = new List<string>();

            Dictionary<int, string> healthLine = new Dictionary<int, string>
            {
                [0] = "мёртв", 
                [1] = "тяжело ранен", 
                [2] = "ранен", 
                [3] = "здоров", 
            };

            statusLines.Add(String.Format("Здоровье: {0}", healthLine[Character.Protagonist.Health]));
            statusLines.Add(String.Format("Оружие: {0}", Character.Protagonist.WeaponName));
            statusLines.Add(String.Format("Покровитель: {0}", Character.Protagonist.Patron));

            if (statusLines.Count <= 0)
                return null;

            return statusLines;
        }

        public List<string> StaticButtons() => new List<string> { };

        public bool StaticAction(string action) => false;

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = String.Empty;

            return false;
        }

        public bool IsButtonEnabled() => true;

        public static bool CheckOnlyIf(string option) => true;
    }
}
