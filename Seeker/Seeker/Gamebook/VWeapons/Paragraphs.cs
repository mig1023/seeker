using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;

namespace Seeker.Gamebook.VWeapons
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Game.Paragraph Get(int id, XmlNode xmlParagraph) => GetTemplate(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            action.Dogfight = Game.Xml.BoolParse(xmlAction["Dogfight"]);

            if (xmlAction["Enemies"] != null)
            {
                action.Enemies = new List<Character>();

                foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemies/Enemy"))
                {
                    Character enemy = new Character
                    {
                        Name = Game.Xml.StringParse(xmlEnemy.Attributes["Name"]),
                        Hitpoints = Game.Xml.IntParse(xmlEnemy.Attributes["Hitpoints"]),
                        Accuracy = Game.Xml.IntParse(xmlEnemy.Attributes["Accuracy"]),
                        First = Game.Xml.BoolParse(xmlEnemy.Attributes["First"]),
                        WithoutCartridges = Game.Xml.BoolParse(xmlEnemy.Attributes["WithoutCartridges"]),
                        Animal = Game.Xml.BoolParse(xmlEnemy.Attributes["Animal"]),
                    };

                    if (enemy.WithoutCartridges || enemy.Animal)
                        enemy.Cartridges = 0;
                    else
                        enemy.Cartridges = 8;

                    action.Enemies.Add(enemy);
                }
            }

            return action;
        }
    }
}
