using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Prototypes
{
    class Actions
    {
        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string Aftertext { get; set; }
        public string Trigger { get; set; }


        public virtual List<string> Do(out bool reload, string action = "", bool trigger = false)
        {
            if (trigger)
                Game.Option.Trigger(Trigger);

            string actionName = (String.IsNullOrEmpty(action) ? ActionName : action);

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

        public virtual bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = String.Empty;

            return false;
        }

        public virtual bool IsButtonEnabled() => true;

        public virtual bool IsHealingEnabled() => false;

        public virtual void UseHealing(int healingLevel) => Game.Other.DoNothing();

        public virtual string TextByOptions(string option) => String.Empty;
    }
}
