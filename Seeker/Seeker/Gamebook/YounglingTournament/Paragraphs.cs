using System.Collections.Generic;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.YounglingTournament
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Paragraph paragraph = ParagraphTemplate(xmlParagraph);

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/*"))
            {
                Option option = OptionsTemplateWithoutDestination(xmlOption);

                if (xmlOption.Attributes["Destination"].Value == "Back")
                    option.Destination = Character.Protagonist.WayBack;
                else
                    option.Destination = Xml.IntParse(xmlOption.Attributes["Destination"]);

                if (xmlOption.Attributes["Do"] != null)
                    option.Do = Xml.ModificationParse(xmlOption, new Modification(), name: "Do");

                paragraph.Options.Add(option);
            }

            foreach (XmlNode xmlAction in xmlParagraph.SelectNodes("Actions/Action"))
                paragraph.Actions.Add(ActionParse(xmlAction));

            foreach (XmlNode xmlModification in xmlParagraph.SelectNodes("Modifications/Modification"))
                paragraph.Modification.Add(Xml.ModificationParse(xmlModification, new Modification()));

            return paragraph;
        }

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            foreach (string param in GetProperties(action))
                SetProperty(action, param, xmlAction);

            if (xmlAction["Enemies"] != null)
            {
                action.Enemies = new List<Character>();

                foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemies/Enemy"))
                {
                    Character enemy = new Character();

                    foreach (string param in GetProperties(enemy))
                        SetPropertyByAttr(enemy, param, xmlEnemy, maxPrefix: true);

                    enemy.Hitpoints = enemy.MaxHitpoints;

                    action.Enemies.Add(enemy);
                }
            }

            if (xmlAction["Benefit"] != null)
                action.Benefit = Xml.ModificationParse(xmlAction["Benefit"], new Modification());

            if (action.Type == "Option")
                action.Option = OptionInActionParse(xmlAction);

            return action;
        }

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
            Xml.ModificationParse(xmlModification, new Modification());
    }
}
