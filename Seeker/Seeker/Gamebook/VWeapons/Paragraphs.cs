using System.Collections.Generic;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.VWeapons
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Paragraph Get(int id, XmlNode xmlParagraph) => GetTemplate(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            action.Dogfight = Xml.BoolParse(xmlAction["Dogfight"]);
            action.DamagedWeapon = Xml.BoolParse(xmlAction["DamagedWeapon"]);
            action.Value = Xml.IntParse(xmlAction["Value"]);
            action.Time = Xml.IntParse(xmlAction["Time"]);

            if (xmlAction["Enemies"] != null)
            {
                action.Enemies = new List<Character>();

                foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemies/Enemy"))
                {
                    Character enemy = new Character
                    {
                        Name = Xml.StringParse(xmlEnemy.Attributes["Name"]),
                        Hitpoints = Xml.IntParse(xmlEnemy.Attributes["Hitpoints"]),
                        Accuracy = Xml.IntParse(xmlEnemy.Attributes["Accuracy"]),
                        First = Xml.BoolParse(xmlEnemy.Attributes["First"]),
                        WithoutCartridges = Xml.BoolParse(xmlEnemy.Attributes["WithoutCartridges"]),
                        Animal = Xml.BoolParse(xmlEnemy.Attributes["Animal"]),
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

        public override Option OptionParse(XmlNode xmlOption) => OptionParseWithDo(xmlOption, new Modification());

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
            Xml.ModificationParse(xmlModification, new Modification());
    }
}
