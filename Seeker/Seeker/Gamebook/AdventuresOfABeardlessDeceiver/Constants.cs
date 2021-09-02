using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.AdventuresOfABeardlessDeceiver
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#5da130",
            [ButtonTypes.Action] = "#339933",
            [ButtonTypes.Option] = "#8dbd6e",
            [ButtonTypes.Continue] = "#8dbd6e",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#dbeadc",
            [ColorTypes.ActionBox] = "#7cb281",
            [ColorTypes.StatusBar] = "#005100",
            [ColorTypes.AdditionalStatus] = "#99b999",
        };

        public static List<int> GetParagraphsWithoutStaticsButtons() => new List<int> { 0, 30, 60, 90 };

        public override List<int> GetParagraphsWithoutStatuses() => new List<int> { 0, 205 };

        public static Dictionary<string, string> StatNames = new Dictionary<string, string>
        {
            ["Strength"] = "силы",
            ["Skill"] = "ловкости",
            ["Wisdom"] = "мудрости",
            ["Cunning"] = "хитрости",
            ["Oratory"] = "красноречия",
            ["Popularity"] = "популярности",
        };

        public static List<string> GetActionParams() => new List<string>
        {
            "Stat", "StatStep", "Level", "GreatKhanSpecialCheck", "GuessBonus", "TablesGame"
        };

        public static List<string> GetModsParams() => new List<string>
        {
            "Name", "Value", "Empty", "Init"
        };

        public override string GetDescription() => "Алдар Косе (в переводе с тюркских языков Безбородый Обманщик) – герой сказок и анекдотов кочевых народов Центральной Азии. Великий хитрец и ловкач, он всегда стоит на страже справедливости, защищая бедный люд от произвола богачей и чиновников.\n\nВ этой книге Алдар Косе должен выполнить важное поручение главы своего рода.И вы можете ему в этом помочь. Вместе с героем вы будете путешествовать по Великой степи, территории Казахского ханства и других государств XVII века, выбирая дорогу на свой вкус.Различные состязания по верховой езде и джигитовке, конкурсы степных поэтов и музыкантов, загадки мудрецов, борьба и прочие испытания – всё это ждёт вас на страницах данной книги.";

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
