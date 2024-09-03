using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.SeaTales
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public override Paragraph Get(int id, XmlNode xmlParagraph)
        {
            if (Constants.StoryPart() == 1)
            {
                if (Data.CurrentParagraphID > 1)
                    Character.Protagonist.Heat -= 1;
            }
            else if (Constants.StoryPart() == 2)
            {
                if (Character.Protagonist.NeedCredibilityCheck)
                {
                    Character.Protagonist.Nonsense -= 1;
                    Character.Protagonist.NeedCredibilityCheck = false;
                }
                
                if (xmlParagraph.SelectNodes("Actions").Count > 0)
                {
                    Character.Protagonist.NeedCredibilityCheck = true;
                }
            }

            return base.Get(xmlParagraph);
        }

        public override Option OptionParse(XmlNode xmlOption)
        {
            Option option = OptionsTemplateWithoutGoto(xmlOption);

            if (ThisIsGameover(xmlOption))
            {
                option.Goto = GetGoto(xmlOption);
            }
            else if (xmlOption.Attributes["Goto"].Value == "Next")
            {
                option.Goto = Game.Data.CurrentParagraphID + 1;
            }
            else if (int.TryParse(xmlOption.Attributes["Goto"].Value, out int _))
            {
                option.Goto = Xml.IntParse(xmlOption.Attributes["Goto"]);
            }
            else
            {
                List<string> link = xmlOption.Attributes["Goto"].Value
                    .Split(',')
                    .Select(x => x.Trim())
                    .ToList<string>();

                option.Goto = int.Parse(link[random.Next(link.Count())]);
            }

            if (Character.Protagonist.NeedCredibilityCheck && String.IsNullOrEmpty(option.Text))
            {
                option.Text = "Пропустить";
            }

            return option;
        }

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            foreach (string param in GetProperties(action))
                SetProperty(action, param, xmlAction);

            action.Dices = xmlAction.Attributes["Dices"]?.InnerText ?? String.Empty;
            action.Throws = Xml.IntParse(xmlAction.Attributes["Throws"]);
            action.Heat = Xml.IntParse(xmlAction.Attributes["Heat"]);
            action.Level = xmlAction.Attributes["Level"]?.InnerText ?? String.Empty;
            action.Success = xmlAction.Attributes["Success"]?.InnerText ?? String.Empty;

            if (action.Type == "Option")
                action.Option = OptionParse(xmlAction);

            return action;
        }

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
            (Abstract.IModification)base.ModificationParse(xmlModification, new Modification());
    }
}
