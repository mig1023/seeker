using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;

namespace Seeker.Gamebook.LordOfTheSteppes
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Game.Paragraph Get(int id, XmlNode xmlParagraph) => GetTemplate(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = new Actions
            {
                ActionName = Game.Xml.StringParse(xmlAction["ActionName"]),
                ButtonName = Game.Xml.StringParse(xmlAction["ButtonName"]),
                Aftertext = Game.Xml.StringParse(xmlAction["Aftertext"]),
                Trigger = Game.Xml.StringParse(xmlAction["Trigger"]),
                Text = Game.Xml.StringParse(xmlAction["Text"]),
                Stat = Game.Xml.StringParse(xmlAction["Stat"]),
                StatStep = Game.Xml.IntParse(xmlAction["StatStep"]),
                RoundsToWin = Game.Xml.IntParse(xmlAction["RoundsToWin"]),
                WoundsToWin = Game.Xml.IntParse(xmlAction["WoundsToWin"]),
                Coherence = Game.Xml.IntParse(xmlAction["Coherence"]),
                Dices = Game.Xml.IntParse(xmlAction["Dices"]),
                Price = Game.Xml.IntParse(xmlAction["Price"]),
                Multiple = Game.Xml.BoolParse(xmlAction["Multiple"]),
                NotToDeath = Game.Xml.BoolParse(xmlAction["NotToDeath"]),
                Odd = Game.Xml.BoolParse(xmlAction["Odd"]),
                Initiative = Game.Xml.BoolParse(xmlAction["Initiative"]),
                StoneGuard = Game.Xml.BoolParse(xmlAction["StoneGuard"]),
                SpecialTechnique = SpecialTechniquesParse(xmlAction["SpecialTechnique"]),
            };

            if (xmlAction["Benefit"] != null)
            {
                action.BenefitList = new List<Abstract.IModification>();

                foreach (XmlNode bonefit in xmlAction.SelectNodes("Benefit"))
                    action.BenefitList.Add(ModificationParse(bonefit));
            }

            bool falaleyHelp = Game.Xml.BoolParse(xmlAction["FalaleyHelp"]) && Game.Data.Triggers.Contains("Фалалей поможет");

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
                Name = Game.Xml.StringParse(xmlNode.Attributes["Name"]),
                MaxAttack = Game.Xml.IntParse(xmlNode.Attributes["Attack"]),
                MaxEndurance = Game.Xml.IntParse(xmlNode.Attributes["Endurance"]),
                MaxDefence = Game.Xml.IntParse(xmlNode.Attributes["Defence"]),
                MaxInitiative = Game.Xml.IntParse(xmlNode.Attributes["Initiative"]),
                SpecialTechnique = new List<Character.SpecialTechniques>(),
            };

            if ((action != null) && action.StoneGuard)
            {
                character.MaxAttack = Character.Protagonist.MaxAttack;
                character.MaxDefence = Character.Protagonist.MaxDefence;
                character.MaxInitiative = Character.Protagonist.MaxInitiative;
            }

            string specialTechniques = Game.Xml.StringParse(xmlNode.Attributes["SpecialTechnique"]);

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
                Name = Game.Xml.StringParse(xmlNode.Attributes["Name"]),
                Value = Game.Xml.IntParse(xmlNode.Attributes["Value"]),
                ValueString = Game.Xml.StringParse(xmlNode.Attributes["ValueString"]),
                Restore = Game.Xml.BoolParse(xmlNode.Attributes["Restore"]),
                Empty = Game.Xml.BoolParse(xmlNode.Attributes["Empty"]),
            };

            return modification;
        }
    }
}
