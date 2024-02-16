using System.Xml;
using System.Collections.Generic;
using Seeker.Game;

namespace Seeker.Gamebook.Moria
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public new static Paragraphs StaticInstance = new Paragraphs();
        public new static Paragraphs GetInstance() => StaticInstance;

        public override Paragraph Get(int id, XmlNode xmlParagraph)
        {
            if (Character.Protagonist.MagicPause > 0)
                Character.Protagonist.MagicPause -= 1;

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
                action.Enemies = EnemyParse(xmlAction["Enemy"]);
            }

            if (action.Type == "Option")
            {
                action.Option = OptionParse(xmlAction);
            }

            return action;
        }

        public override Option OptionParse(XmlNode xmlOption) =>
            OptionParseWithDo(xmlOption, new Modification());

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
            Xml.ModificationParse(xmlModification, new Modification());

        private List<string> EnemyParse(XmlNode xmlEnemy)
        {
            List<string> enemies = new List<string>();

            int count = 0;

            if (xmlEnemy.Attributes["Count"].InnerText == "RANDOM")
                count = 12 - Dice.Roll(dices: 2);
            else
                count = int.Parse(xmlEnemy.Attributes["Count"].InnerText);

            string name = xmlEnemy.Attributes["Name"].InnerText;

            for (int i = 0; i < count; i++)
                enemies.Add(name);

            return enemies;
        }
    }
}
