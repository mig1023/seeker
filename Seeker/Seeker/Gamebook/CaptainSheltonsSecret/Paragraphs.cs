using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;


namespace Seeker.Gamebook.CaptainSheltonsSecret
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Paragraph Get(int id, XmlNode xmlParagraph) => GetTemplate(xmlParagraph); 

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            action.RoundsToWin = Xml.IntParse(xmlAction["RoundsToWin"]);
            action.WoundsToWin = Xml.IntParse(xmlAction["WoundsToWin"]);
            action.DamageToWin = Xml.IntParse(xmlAction["DamageToWin"]);
            action.MasteryPenalty = Xml.IntParse(xmlAction["MasteryPenalty"]);
            action.GroupFight = Xml.BoolParse(xmlAction["GroupFight"]);

            if (xmlAction["Allies"] != null)
            {
                action.Allies = new List<Character>();

                foreach (XmlNode xmlAlly in xmlAction.SelectNodes("Allies/Ally"))
                    action.Allies.Add(EnemyParse(xmlAlly));
            }

            if (xmlAction["Enemies"] != null)
            {
                action.Enemies = new List<Character>();

                foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemies/Enemy"))
                    action.Enemies.Add(EnemyParse(xmlEnemy));
            }

            return action;
        }

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
            Xml.ModificationParse(xmlModification, new Modification());

        private static Character EnemyParse(XmlNode xmlEnemy)
        {
            Character enemy = new Character
            {
                Name = Xml.StringParse(xmlEnemy.Attributes["Name"]),
                MaxMastery = Xml.IntParse(xmlEnemy.Attributes["Mastery"]),
                MaxEndurance = Xml.IntParse(xmlEnemy.Attributes["Endurance"]),
            };

            enemy.Mastery = enemy.MaxMastery;
            enemy.Endurance = enemy.MaxEndurance;

            return enemy;
        }
    }
}
