using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.CreatureOfHavoc
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

                XmlNode optionMod = xmlOption.SelectSingleNode("Modification");

                if (optionMod != null)
                    option.Do.Add(Xml.ModificationParse(optionMod, new Modification()));

                paragraph.Options.Add(option);
            }

            foreach (XmlNode xmlAction in xmlParagraph.SelectNodes("Actions/*"))
                paragraph.Actions.Add(ActionParse(xmlAction));

            foreach (XmlNode xmlModification in xmlParagraph.SelectNodes("Modifications/*"))
                paragraph.Modification.Add(ModificationParse(xmlModification));

            return paragraph;
        }

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            foreach (string param in GetProperties(action))
                SetProperty(action, param, xmlAction);

            if (xmlAction["Enemy"] != null)
            {
                action.Enemies = new List<Character> { EnemyParse(xmlAction["Enemy"]) };
            }
            else if (xmlAction["Enemies"] != null)
            {
                action.Enemies = new List<Character>();

                foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemies/Enemy"))
                    action.Enemies.Add(EnemyParse(xmlEnemy));
            }

            return action;
        }

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
            (Abstract.IModification)base.ModificationParse(xmlModification, new Modification());

        private Character EnemyParse(XmlNode xmlEnemy)
        {
            Character enemy = new Character();

            foreach (string param in GetProperties(enemy))
                SetPropertyByAttr(enemy, param, xmlEnemy, maxPrefix: true);

            enemy.Mastery = enemy.MaxMastery;
            enemy.Endurance = enemy.MaxEndurance;

            return enemy;
        }
    }
}
