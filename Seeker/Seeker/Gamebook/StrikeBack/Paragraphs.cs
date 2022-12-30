using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.StrikeBack
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

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

            if (xmlAction["Allies"] != null)
            {
                action.GroupFight = true;
                action.Allies = new List<Character>();

                foreach (XmlNode xmlAlly in xmlAction.SelectNodes("Allies/Ally"))
                    action.Allies.Add(CharacterParse(xmlAlly, null));
            }
                
            if (xmlAction["Enemies"] != null)
            {
                action.Enemies = new List<Character>();

                foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemies/Enemy"))
                    action.Enemies.Add(CharacterParse(xmlEnemy, action));

                if (!action.GroupFight && (action.Enemies.Count > 1))
                    action.GroupFight = true;
            }

            if (action.Type == "Option")
                action.Option = OptionParse(xmlAction["Option"]);

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

            return character;
        }

        public override Option OptionParse(XmlNode xmlOption) =>
            OptionParseWithDo(xmlOption, new Modification());

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
            (Abstract.IModification)base.ModificationParse(xmlModification, new Modification());
    }
}
