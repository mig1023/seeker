﻿using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Seeker.Game;


namespace Seeker.Gamebook.StringOfWorlds
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Paragraph paragraph = ParagraphTemplate(xmlParagraph);

            Constants.RandomColor();

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/Option"))
            {
                Option option = OptionsTemplateWithoutDestination(xmlOption);

                if (xmlOption.Attributes["Destination"].Value == "Gate")
                {
                    if (Character.Protagonist.GateCode <= 0)
                        continue;
                    else
                    {
                        foreach (XmlNode xmlModification in xmlParagraph.SelectNodes("Modifications/Modification"))
                            if (xmlModification.Attributes["Name"].Value == "GateCode")
                                Character.Protagonist.GateCode += Xml.IntParse(xmlModification.Attributes["Value"]);
                        
                        option.Destination = Character.Protagonist.GateCode;
                    }
                }
                else if (int.TryParse(xmlOption.Attributes["Destination"].Value, out int _))
                    option.Destination = Xml.IntParse(xmlOption.Attributes["Destination"]);
                else
                {
                    List<string> destinations = xmlOption.Attributes["Destination"].Value.Split(',').ToList<string>();
                    option.Destination = int.Parse(destinations[random.Next(destinations.Count())]);
                }

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

            foreach (string param in Constants.GetActionParams())
                SetProperty(action, param, xmlAction);

            if (xmlAction["Enemies"] != null)
            {
                action.Enemies = new List<Character>();

                foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemies/Enemy"))
                {
                    Character enemy = new Character();

                    foreach (string param in Constants.GetEnemyParams())
                        SetPropertyByAttr(enemy, param, xmlEnemy);

                    enemy.Skill = enemy.MaxSkill;
                    enemy.Strength = enemy.MaxStrength;

                    action.Enemies.Add(enemy);
                }
            }

            if (xmlAction["Benefit"] != null)
                action.Benefit = Xml.ModificationParse(xmlAction["Benefit"], new Modification());

            return action;
        }
    }
}
