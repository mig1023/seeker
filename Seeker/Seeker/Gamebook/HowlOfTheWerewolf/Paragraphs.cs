using System;
using System.Collections.Generic;
using System.Xml;

using Seeker.Game;

namespace Seeker.Gamebook.HowlOfTheWerewolf
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public override Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Paragraph paragraph = ParagraphTemplate(xmlParagraph);

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/*"))
            {
                Option option = OptionsTemplateWithoutGoto(xmlOption);

                if (ThisIsGameover(xmlOption) || ThisIsBack(xmlOption))
                {
                    option.Goto = GetGoto(xmlOption, wayBack: Character.Protagonist.WayBack);
                }
                else
                {
                    option.Goto = Xml.IntParse(xmlOption.Attributes["Goto"]);
                }

                XmlNode optionMod = xmlOption.SelectSingleNode("Modification");

                if (optionMod != null)
                    option.Do.Add(Xml.ModificationParse(optionMod, new Modification()));

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

            action.Specificity = SpecificsParse(xmlAction["Specificity"]);

            if (xmlAction["Benefit"] != null)
            {
                action.BenefitList = new List<Abstract.IModification>();

                foreach (XmlNode bonefit in xmlAction.SelectNodes("Benefit"))
                    action.BenefitList.Add(Xml.ModificationParse(bonefit, new Modification()));
            }

            if (xmlAction["Enemy"] != null)
            {
                Character enemy = EnemyParse(xmlAction["Enemy"]);

                if (Xml.BoolParse(xmlAction["RandomEnemyCount"]))
                {
                    EnemyMultiplier(Dice.Roll(), ref action, enemy);
                }
                else
                {
                    action.Enemies = new List<Character> { enemy };
                }
            }
            else if(xmlAction["Enemies"] != null)
            {
                action.Enemies = new List<Character>();

                foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemies/Enemy"))
                    action.Enemies.Add(EnemyParse(xmlEnemy));
            }

            return action;
        }

        private Character EnemyParse(XmlNode xmlEnemy)
        {
            Character enemy = new Character();

            foreach (string param in GetProperties(enemy))
                SetPropertyByAttr(enemy, param, xmlEnemy, maxPrefix: true);

            enemy.Mastery = enemy.MaxMastery;
            enemy.Endurance = enemy.MaxEndurance;

            return enemy;
        }

        public static void EnemyMultiplier(int count, ref Actions action, Character enemy)
        {
            string name = enemy.Name;

            for (int i = 0; i < count; i++)
            {
                enemy.Name = Constants.GetCountName[i + 1] + " " + name;
                action.Enemies.Add(enemy.Clone());
            }
        }

        private static Actions.Specifics SpecificsParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return Actions.Specifics.Nope;

            bool success = Enum.TryParse(xmlNode.InnerText, out Actions.Specifics value);

            return (success ? value : Actions.Specifics.Nope);
        }
    }
}
