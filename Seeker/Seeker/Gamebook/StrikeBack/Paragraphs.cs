using System;
using System.Collections.Generic;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.StrikeBack
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public override Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Paragraph paragraph = ParagraphTemplate(xmlParagraph);

            XmlNode transformation = xmlParagraph.SelectSingleNode("Transformation");

            if (transformation != null)
                paragraph.Modification.Add(ModificationParse(transformation));

            return base.Get(xmlParagraph, paragraph);
        }

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            foreach (string param in GetProperties(action))
                SetProperty(action, param, xmlAction);

            action.SpecialTechniques = SpecialTechniquesListParse(xmlAction["SpecialTechnique"]);

            if (xmlAction["Ally"] != null)
            {
                action.Allies = new List<Character> { CharacterParse(xmlAction["Ally"], null) };
            }
            else if (xmlAction["Allies"] != null)
            {
                action.GroupFight = true;
                action.Allies = new List<Character>();

                foreach (XmlNode xmlAlly in xmlAction.SelectNodes("Allies/Ally"))
                    action.Allies.Add(CharacterParse(xmlAlly, null));
            }

            if (xmlAction["Enemy"] != null)
            {
                action.Enemies = new List<Character> { CharacterParse(xmlAction["Enemy"], action) };
            }
            else if (xmlAction["Enemies"] != null)
            {
                action.Enemies = new List<Character>();

                foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemies/Enemy"))
                    action.Enemies.Add(CharacterParse(xmlEnemy, action));

                if (!action.GroupFight && (action.Enemies.Count > 1))
                    action.GroupFight = true;
            }

            if (action.Type == "Option")
                action.Option = OptionParse(xmlAction);

            return action;
        }

        private Character CharacterParse(XmlNode xmlNode, Actions action)
        {
            Character character = new Character();

            foreach (string param in GetProperties(character))
                SetPropertyByAttr(character, param, xmlNode, maxPrefix: true);

            character.Attack = character.MaxAttack;
            character.Endurance = character.MaxEndurance;
            character.Defence = character.MaxDefence;
            character.SpecialTechnique = SpecialTechniquesListParse(xmlNode.Attributes["SpecialTechnique"]);

            return character;
        }

        private static List<Character.SpecialTechniques> SpecialTechniquesListParse(XmlNode xmlNode)
        {
            List<Character.SpecialTechniques> specialTechniques = new List<Character.SpecialTechniques>();

            string techniquesLine = Xml.StringParse(xmlNode);

            foreach (string specialTechnique in techniquesLine.Split(','))
                specialTechniques.Add(SpecialTechniquesParse(specialTechnique));

            return specialTechniques;
        }

        private static Character.SpecialTechniques SpecialTechniquesParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return Character.SpecialTechniques.Nope;

            return SpecialTechniquesParse(xmlNode.InnerText);
        }

        private static Character.SpecialTechniques SpecialTechniquesParse(string xmlLine)
        {
            bool success = Enum.TryParse(xmlLine, out Character.SpecialTechniques value);
            return (success ? value : Character.SpecialTechniques.Nope);
        }

        public override Option OptionParse(XmlNode xmlOption) =>
            OptionParseWithDo(xmlOption, new Modification());

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
            (Abstract.IModification)base.ModificationParse(xmlModification, new Modification());
    }
}
