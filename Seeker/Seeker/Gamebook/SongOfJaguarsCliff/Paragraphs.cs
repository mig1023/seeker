using Seeker.Game;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Seeker.Gamebook.SongOfJaguarsCliff
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public override Paragraph Get(int id, XmlNode xmlParagraph) =>
            base.Get(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            foreach (string param in GetProperties(action))
                SetProperty(action, param, xmlAction);

            if (action.Type == "Fight")
            {
                action.Enemies = new List<Character>();

                foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemy"))
                    action.Enemies.Add(EnemyParse(xmlEnemy));
            }
            else if (action.Type == "Option")
            {
                action.Option = OptionParse(xmlAction);
            }

            action.Benefit = Xml.ModificationParse(xmlAction["Benefit"], new Modification());

            return action;
        }

        public override Option OptionParse(XmlNode xmlOption) =>
            OptionParseWithDo(xmlOption, new Modification());

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
            Xml.ModificationParse(xmlModification, new Modification());

        private Character EnemyParse(XmlNode xmlEnemy)
        {
            Character enemy = new Character();

            foreach (string param in GetProperties(enemy))
            {
                if ((param != "Weapons") && (param != "Priority"))
                    SetPropertyByAttr(enemy, param, xmlEnemy, maxPrefix: true);
            }

            enemy.Weapons = new List<Weapon>();
            
            foreach (string weapon in xmlEnemy.Attributes["Weapons"].Value.Split(';'))
            {
                enemy.Weapons.Add(new Weapon(weapon));
            }

            string priority = xmlEnemy.Attributes["Priority"].Value;

            if (priority.Contains(";"))
            {
                string[] priorities = priority.Split(';');
                enemy.Priority = Xml.IntParse(priorities[0]);
                enemy.PriorityComment = priorities[1].Trim();
            }
            else
            {
                enemy.Priority = Xml.IntParse(priority);
            }
            
            enemy.CurrentWeapon = null;

            if ((enemy.Name == "Питер Кларк") && Game.Option.IsTriggered("Кларк"))
            {
                enemy.Hitpoints = 5;
            }

            return enemy;
        }
    }
}
