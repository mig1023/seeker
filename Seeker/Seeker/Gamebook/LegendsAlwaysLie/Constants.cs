using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.LegendsAlwaysLie
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#911",
            [ButtonTypes.Action] = "#ba2020",
            [ButtonTypes.Option] = "#cc8888",
            [ButtonTypes.Continue] = "#cc8888",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#f5e7e5",
            [ColorTypes.StatusBar] = "#870808",
            [ColorTypes.AdditionalStatus] = "#b70b0b",
            [ColorTypes.AdditionalFont] = "#ffffff",
            [ColorTypes.BookColor] = "#4c0000",
        };

        public override Output.Interface.TextFontSize GetFontSize() => Output.Interface.TextFontSize.little;

        public override double? GetLineHeight() => 1.20;

        public static List<int> GetParagraphsWithoutStaticsButtons() => new List<int> { 0, 687, 714, 715, 701, 702 };

        public override List<int> GetParagraphsWithoutStatuses() => new List<int> { 0, 714, 715, 716 };

        public static List<string> GetActionParams() => new List<string>
        {
            "ConneryAttacks", "ReactionWounds", "ReactionRound", "ReactionHit", "Dices", "DiceBonus", "OnlyRounds",
            "RoundsToWin", "AttackWounds", "Disabled", "IncrementWounds", "GolemFight", "GolemFight"
        };

        public static List<string> GetModsParams() => new List<string>
        {
            "Name", "Value", "WizardWoundsPenalty", "ThrowerWoundsPenalty", "Empty", "Init"
        };

        public override string GetDescription() => "Вы – молодой ведьмак, чудом выживш" +
            "ий в кровавой мясорубке, в которую вылилось ваше самое первое задание. Оз" +
            "лобленному, разочаровавшемуся в своих наставниках юноше предлагают неслых" +
            "анную авантюру: отправиться в безумный, граничащий с самоубийством поход " +
            "на самый край света. И предожение это делает не кто иной, как легендарный" +
            " Коннери из Таннендока.\n\nЗачем он намерен отправиться на плато горных в" +
            "еликанов? Что он найдет в этом забытом богами краю? И что удастся обрести" +
            " вам?";

        public static Links GetLinks() => new Links
        {
            Protagonist = Character.Protagonist.Init,
            CheckOnlyIf = Actions.StaticInstance.CheckOnlyIf,
            Paragraphs = Paragraphs.StaticInstance,
            Actions = Actions.StaticInstance,
            Constants = StaticInstance,
            Save = Character.Protagonist.Save,
            Load = Character.Protagonist.Load,
        };
    }
}
