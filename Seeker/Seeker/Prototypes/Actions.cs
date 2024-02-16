using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Seeker.Game;

namespace Seeker.Prototypes
{
    class Actions
    {
        public static Constants StaticInstance = new Constants();
        public static Constants GetInstance() => StaticInstance;

        public string Type { get; set; }
        public string Button { get; set; }
        public List<Output.Text> Texts { get; set; }
        public string Trigger { get; set; }
        public string Head { get; set; }
        public bool Used { get; set; }
        public Option Option { get; set; }
        public int Price { get; set; }
        public bool Multiple { get; set; }

        public Abstract.IModification Benefit { get; set; }
        public List<Abstract.IModification> BenefitList { get; set; }

        public virtual List<string> Do(out bool reload, string action = "", bool trigger = false)
        {
            List<string> actionResult = new List<string>();

            if (trigger)
                Option.Trigger(Trigger);

            string actionName = (String.IsNullOrEmpty(action) ? Type : action);

            try
            {
                actionResult = this.GetType().InvokeMember(
                    actionName, System.Reflection.BindingFlags.InvokeMethod, null, this, null) as List<string>;
            }
            catch (Exception ex)
            {
                actionResult = new List<string> { "BIG|BOLD|Ошибка action-кода книги:", $"BIG|{ex.Message}" };
            }

            reload = (actionResult.Count >= 1) && (actionResult[0] == "RELOAD");

            return actionResult;
        }

        public virtual List<string> Representer() =>
            new List<string> { };

        public virtual string ButtonText()
        {
            if (!String.IsNullOrEmpty(Button))
                return Button;

            Dictionary<string, string> texts = Data.Constants.ButtonText();
            return texts.ContainsKey(Type) ? texts[Type] : String.Empty;
        }
                        
        public virtual List<string> Status() =>
            null;

        public virtual List<string> AdditionalStatus() =>
            null;

        public virtual List<string> StaticButtons() =>
            new List<string> { };

        public virtual bool StaticAction(string action) =>
            false;

        public virtual bool Availability(string option) =>
            true;

        public static bool AvailabilityTrigger(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains("!"))
            {
                return !Option.IsTriggered(option.Replace("!", String.Empty).Trim());
            }
            else
            {
                return Option.IsTriggered(option);
            }
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
            toEndText = Output.Constants.GAMEOVER_TEXT;

            return param <= 0;
        }

        public virtual bool GameOverBy(bool param, out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = Output.Constants.GAMEOVER_TEXT;

            return param;
        }

        public virtual List<string> ChangeProtagonistParam(string stat,
            Character protagonist, string limithName, bool decrease = false)
        {
            SetProperty(protagonist, stat, GetProperty(protagonist, stat) + (decrease ? -1 : 1));

            if (!String.IsNullOrEmpty(limithName))
                SetProperty(protagonist, limithName, GetProperty(protagonist, limithName) + (decrease ? 1 : -1));

            return new List<string> { "RELOAD" };
        }

        public virtual bool IsButtonEnabled(bool secondButton = false) =>
            true;

        public virtual bool IsHealingEnabled() =>
            false;

        public virtual void UseHealing(int healingLevel) =>
            Services.DoNothing();

        public virtual string TextByOptions(string option) =>
            String.Empty;
        
        public virtual List<string> TextByProperties(XmlNode text) =>
            null;

        public List<string> SimpleDice() =>
            new List<string> { $"BIG|Кубик: {Dice.Symbol(Dice.Roll())}" };

        public int GetProperty(object protagonist, string property) =>
            (int)protagonist.GetType().GetProperty(property).GetValue(protagonist, null);

        public void SetProperty(object protagonist, string property, int value) =>
            protagonist.GetType().GetProperty(property).SetValue(protagonist, value);

        public string Result(bool resultOk, string options)
        {
            List<string> result = options.Split('|').ToList();
            return resultOk ? $"BIG|GOOD|{result[0]} :)" : $"BIG|BAD|{result[1]} :(";
        }
    }
}
