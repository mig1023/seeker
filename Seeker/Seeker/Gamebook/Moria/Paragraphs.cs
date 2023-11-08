using System.Xml;
using System.Collections.Generic;
using Seeker.Game;

namespace Seeker.Gamebook.Moria
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Paragraph Get(int id, XmlNode xmlParagraph) =>
           base.Get(xmlParagraph);

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

            int count = int.Parse(xmlEnemy.Attributes["Count"].InnerText);
            string name = xmlEnemy.Attributes["Name"].InnerText;

            for (int i = 0; i < count; i++)
                enemies.Add(name);

            return enemies;
        }
    }
}
