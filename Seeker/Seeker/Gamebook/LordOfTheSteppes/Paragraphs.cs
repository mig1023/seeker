using System;
using System.Collections.Generic;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.LordOfTheSteppes
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Paragraph Get(int id, XmlNode xmlParagraph) => base.Get(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            foreach (string param in Constants.GetActionParams())
                SetProperty(action, param, xmlAction);

            action.SpecialTechnique = SpecialTechniquesParse(xmlAction["SpecialTechnique"]);

            if (xmlAction["Benefit"] != null)
            {
                action.BenefitList = new List<Abstract.IModification>();

                foreach (XmlNode bonefit in xmlAction.SelectNodes("Benefit"))
                    action.BenefitList.Add(ModificationParse(bonefit));
            }

            bool falaleyHelp = Xml.BoolParse(xmlAction["FalaleyHelp"]) && Data.Triggers.Contains("Фалалей поможет");

            if ((xmlAction["Allies"] != null) || falaleyHelp)
            {
                action.Allies = new List<Character> { new Character { Name = Character.Protagonist.Name } };
                action.GroupFight = true;
            }

            if (xmlAction["Allies"] != null)
                foreach (XmlNode xmlAlly in xmlAction.SelectNodes("Allies/Ally"))
                    action.Allies.Add(CharacterParse(xmlAlly, null));

            if (falaleyHelp)
                action.Allies.Add(Falaley());

            if (xmlAction["Enemies"] != null)
            {
                action.Enemies = new List<Character>();

                foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemies/Enemy"))
                    action.Enemies.Add(CharacterParse(xmlEnemy, action));

                if (action.Enemies.Count > 1)
                    action.GroupFight = true;
            }

            return action;
        }

        public override Option OptionParse(XmlNode xmlOption) => OptionParseWithDo(xmlOption, new Modification());

        private static Character Falaley()
        {
            Character character = new Character
            {
                Name = "Фалалей",
                MaxAttack = 9,
                MaxEndurance = 14,
                MaxDefence = 15,
                MaxInitiative = 10,
                SpecialTechnique = new List<Character.SpecialTechniques>(),
            };

            character.Attack = character.MaxAttack;
            character.Endurance = character.MaxEndurance;
            character.Defence = character.MaxDefence;
            character.Initiative = character.MaxInitiative;

            character.SpecialTechnique.Add(Character.SpecialTechniques.PowerfulStrike);

            return character;
        }

        private static Character CharacterParse(XmlNode xmlNode, Actions action)
        {
            Character character = new Character
            {
                Name = Xml.StringParse(xmlNode.Attributes["Name"]),
                MaxAttack = Xml.IntParse(xmlNode.Attributes["Attack"]),
                MaxEndurance = Xml.IntParse(xmlNode.Attributes["Endurance"]),
                MaxDefence = Xml.IntParse(xmlNode.Attributes["Defence"]),
                MaxInitiative = Xml.IntParse(xmlNode.Attributes["Initiative"]),
                SpecialTechnique = new List<Character.SpecialTechniques>(),
            };

            if ((action != null) && action.StoneGuard)
            {
                character.MaxAttack = Character.Protagonist.MaxAttack;
                character.MaxDefence = Character.Protagonist.MaxDefence;
                character.MaxInitiative = Character.Protagonist.MaxInitiative;
            }

            string specialTechniques = Xml.StringParse(xmlNode.Attributes["SpecialTechnique"]);

            foreach (string specialTechnique in specialTechniques.Split(','))
                character.SpecialTechnique.Add(SpecialTechniquesParse(specialTechnique));

            character.Attack = character.MaxAttack;
            character.Endurance = character.MaxEndurance;
            character.Defence = character.MaxDefence;
            character.Initiative = character.MaxInitiative;

            return character;
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

        public override Abstract.IModification ModificationParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return null;

            Modification modification = new Modification
            {
                Name = Xml.StringParse(xmlNode.Attributes["Name"]),
                Value = Xml.IntParse(xmlNode.Attributes["Value"]),
                ValueString = Xml.StringParse(xmlNode.Attributes["ValueString"]),
                Restore = Xml.BoolParse(xmlNode.Attributes["Restore"]),
                Empty = Xml.BoolParse(xmlNode.Attributes["Empty"]),
            };

            return modification;
        }
    }
}
