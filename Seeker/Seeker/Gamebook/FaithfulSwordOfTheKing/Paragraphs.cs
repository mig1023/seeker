﻿using System;
using System.Collections.Generic;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.FaithfulSwordOfTheKing
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

            action.MeritalArt = MeritalArtsParse(xmlAction["MeritalArt"]);

            if (action.Name == "Option")
                action.Option = OptionParse(xmlAction["Option"]);

            if (xmlAction["Benefit"] != null)
            {
                action.BenefitList = new List<Abstract.IModification>();

                foreach (XmlNode bonefit in xmlAction.SelectNodes("Benefit"))
                    action.BenefitList.Add(ModificationParse(bonefit));
            }

            if (xmlAction["Enemies"] != null)
            {
                action.Enemies = new List<Character>();

                foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemies/Enemy"))
                    action.Enemies.Add(EnemyParse(xmlEnemy));
            }

            return action;
        }

        public override Option OptionParse(XmlNode xmlOption) => OptionParseWithDo(xmlOption, new Modification());

        private static Character.MeritalArts MeritalArtsParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return Character.MeritalArts.Nope;

            bool success = Enum.TryParse(xmlNode.InnerText, out Character.MeritalArts value);

            return (success ? value : Character.MeritalArts.Nope);
        }

        private Character EnemyParse(XmlNode xmlEnemy)
        {
            Character enemy = new Character();

            foreach (string param in Constants.GetEnemyParams())
                SetPropertyByAttr(enemy, param, xmlEnemy);

            enemy.Skill = enemy.MaxSkill;
            enemy.Strength = enemy.MaxStrength;

            return enemy;
        }

        public override Abstract.IModification ModificationParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return null;

            Modification modification = new Modification();

            foreach (string param in Constants.GetModsParams())
                SetPropertyByAttr(modification, param, xmlNode, maxPrefix: true);

            return modification;
        }
    }
}
