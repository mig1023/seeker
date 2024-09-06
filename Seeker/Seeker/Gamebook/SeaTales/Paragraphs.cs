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
            foreach (XmlNode xmlModification in xmlParagraph.SelectNodes("Modifications/*"))
            {
                if (xmlModification.Name != "Redirect")
                    continue;

                var currendId = Game.Data.CurrentParagraphID;

                int count = Game.Data.Path
                    .Where(x => x == currendId.ToString())
                    .Count();

                if (count == 0)
                    continue;

                string redirect = xmlModification.Attributes["Value"]?.InnerText ?? null;
                id = redirect == null ? id + 1 : int.Parse(redirect);

                Game.Data.CurrentParagraphID = id;
                xmlParagraph = Game.Data.XmlParagraphs[id];
                return Get(id, xmlParagraph);
            }

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
            var modifications = new List<Abstract.IModification>();

            foreach (XmlNode optionMod in xmlOption.SelectNodes("*"))
            {
                if (!optionMod.Name.StartsWith("Text"))
                    modifications.Add(new Modification());
            }

            Option option = OptionParseWithDo(xmlOption, modifications);

            if (ThisIsGameover(xmlOption))
            {
                option.Goto = GetGoto(xmlOption);
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
