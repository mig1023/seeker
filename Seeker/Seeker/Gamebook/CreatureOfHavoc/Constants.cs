using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.CreatureOfHavoc
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#145334",
            [ButtonTypes.Action] = "#104229",
            [ButtonTypes.Option] = "#42755c",
            [ButtonTypes.Continue] = "#7d9188",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#c9d7d0",
            [ColorTypes.StatusBar] = "#0e3b24",
            [ColorTypes.BookColor] = "#145334",
        };

        public static List<string> GetActionParams() => new List<string>
        {
            "WoundsToWin", "RoundsToWin", "RoundsToFight", "Ophidiotaur", "ManicBeast", "GiantHornet"
        };

        public static List<string> GetEnemyParams() => new List<string>
        {
            "Name", "MaxMastery", "MaxEndurance"
        };

        public override string GetDescription() => "Зло затаилось в Долине Троллей! Вот-вот чёрный колдун Жаррадан Марр овладеет секретами эльфийской магии, которые сделают его всемогущим. Ничто тогда не защитит мирную Аллансию от порабощения легионами Хаоса.\n\nНо Разрушителю нет до этого дела, - им правят животные инстинкты, вечный голод и слепая ярость. Бессловесный монстр не способен понять чувств, мыслей и поступков других существ. Он создан для кровавой схватки, и плоть поверженного врага для него слаще мёда. Это свирепое чудовище - ТЫ! Но у тебя есть шанс научиться контролировать свою звериную натуру - ты сможешь познать себя и свою судьбу, наполнить смыслом и великим предназначением свое некогда бесцельное существование! Всё, что надо для этого сделать, - просто уцелеть в ужасающем приключении!";

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
