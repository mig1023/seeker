using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.OctopusIsland
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Paragraph Get(int id, XmlNode xmlParagraph) => base.Get(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            action.WoundsToWin = Xml.IntParse(xmlAction["WoundsToWin"]);
            action.DinnerHitpointsBonus = Xml.IntParse(xmlAction["Dinner"]);
            action.ReturnedStuffs = Xml.BoolParse(xmlAction["ReturnedStuffs"]);

            if (xmlAction["Enemies"] != null)
            {
                action.Enemies = new List<Character>();

                foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemies/Enemy"))
                    action.Enemies.Add(EnemyParse(xmlEnemy));
            }

            return action;
        }

        public override Option OptionParse(XmlNode xmlOption)
        {
            Option option = OptionsTemplateWithoutLink(xmlOption);

            if (int.TryParse(xmlOption.Attributes["Link"].Value, out int _))
                option.Link = Xml.IntParse(xmlOption.Attributes["Link"]);
            else
            {
                List<string> link = xmlOption.Attributes["Link"].Value.Split(',').ToList<string>();
                option.Link = int.Parse(link[random.Next(link.Count())]);
            }

            return option;
        }

        private static Character EnemyParse(XmlNode xmlEnemy) => new Character
        {
            Name = Xml.StringParse(xmlEnemy.Attributes["Name"]),
            Skill = Xml.IntParse(xmlEnemy.Attributes["Skill"]),
            Hitpoint = Xml.IntParse(xmlEnemy.Attributes["Hitpoint"]),
        };

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
            Xml.ModificationParse(xmlModification, new Modification());
    }
}
