using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.HostagesOfPirateAdmiral
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public override Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Paragraph paragraph = ParagraphTemplate(xmlParagraph);

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/*"))
            {
                Option option = OptionsTemplateWithoutGoto(xmlOption);

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
                    List<string> link = xmlOption.Attributes["Goto"].Value.Split(',').ToList<string>();
                    option.Goto = int.Parse(link[random.Next(link.Count())]);
                }

                paragraph.Options.Add(option);
            }

            foreach (XmlNode xmlAction in xmlParagraph.SelectNodes("Actions/*"))
                paragraph.Actions.Add(ActionParse(xmlAction));

            foreach (XmlNode xmlModification in xmlParagraph.SelectNodes("Modifications/*"))
                paragraph.Modification.Add(Xml.ModificationParse(xmlModification, new Modification()));

            return paragraph;
        }

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            foreach (string param in GetProperties(action))
                SetProperty(action, param, xmlAction);

            if (xmlAction["Enemy"] != null)
            {
                action.Enemies = new List<Character> { ParseEnemy(xmlAction["Enemy"]) };
            }
            else if (xmlAction["Enemies"] != null)
            {
                action.Enemies = new List<Character>();

                foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemies/Enemy"))
                    action.Enemies.Add(ParseEnemy(xmlEnemy));
            }

            if (xmlAction["Benefit"] != null)
                action.Benefit = Xml.ModificationParse(xmlAction["Benefit"], new Modification());

            if (action.Type == "Option")
                action.Option = OptionParse(xmlAction);

            return action;
        }

        private Character ParseEnemy(XmlNode xmlEnemy)
        {
            Character enemy = new Character();

            foreach (string param in GetProperties(enemy))
                SetPropertyByAttr(enemy, param, xmlEnemy, maxPrefix: true);

            enemy.Skill = enemy.MaxSkill;
            enemy.Strength = enemy.MaxStrength;

            return enemy;
        }
    }
}