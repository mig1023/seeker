using System;
using System.Collections.Generic;

namespace Seeker.Prototypes
{
    class Actions
    {
        public string Name { get; set; }
        public string Button { get; set; }
        public string Aftertext { get; set; }
        public List<Output.Text> Aftertexts { get; set; }
        public string Trigger { get; set; }
        public string Text { get; set; }
        public bool Used { get; set; }
        public Game.Option Option { get; set; }
        public int Price { get; set; }
        public bool Multiple { get; set; }

        public Abstract.IModification Benefit { get; set; }
        public List<Abstract.IModification> BenefitList { get; set; }

        public virtual List<string> Do(out bool reload, string action = "", bool trigger = false)
        {
            if (trigger)
                Game.Option.Trigger(Trigger);

            string actionName = (String.IsNullOrEmpty(action) ? Name : action);

            List<string> actionResult = this.GetType().InvokeMember(actionName, System.Reflection.BindingFlags.InvokeMethod,
                null, this, null) as List<string>;

            reload = (actionResult.Count >= 1) && (actionResult[0] == "RELOAD");

            return actionResult;
        }

        public virtual List<string> Representer() => new List<string> { };

        public virtual List<string> Status() => null;

        public virtual List<string> AdditionalStatus() => null;

        public virtual List<string> StaticButtons() => new List<string> { };

        public virtual bool StaticAction(string action) => false;

        public virtual bool CheckOnlyIf(string option) => true;

        public static bool CheckOnlyIfTrigger(string option)
        {
            if (option.Contains("!"))
                return !Game.Option.IsTriggered(option.Replace("!", String.Empty).Trim());
            else
                return Game.Option.IsTriggered(option);
        }

        public virtual bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = String.Empty;

            return false;
        }

        public virtual bool GameOverBy(int param, out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = "Начать сначала";

            return param <= 0;
        }

        public virtual bool IsButtonEnabled() => true;

        public virtual bool IsHealingEnabled() => false;

        public virtual void UseHealing(int healingLevel) => Game.Other.DoNothing();

        public virtual string TextByOptions(string option) => String.Empty;

        public int GetProperty(object protagonist, string property) =>
            (int)protagonist.GetType().GetProperty(property).GetValue(protagonist, null);

        public void SetProperty(object protagonist, string property, int value) =>
            protagonist.GetType().GetProperty(property).SetValue(protagonist, value);

        public string Result(bool resultOk, string options) =>
            (resultOk ? String.Format("BIG|GOOD|{0} :)", options.Split('|')[0]) : String.Format("BIG|BAD|{0} :(", options.Split('|')[1]));
    }
}
