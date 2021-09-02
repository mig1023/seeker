using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.HeartOfIce
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#4a6e9c",
            [ButtonTypes.Option] = "#6e8baf",
            [ButtonTypes.Action] = "#6e8baf",
            [ButtonTypes.Continue] = "#99adc7",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#b6c5d7",
            [ColorTypes.Font] = "#2c425d",
            [ColorTypes.ActionBox] = "#a5b6cd",
            [ColorTypes.StatusBar] = "#5c7ca5",
        };

        public override List<int> GetParagraphsWithoutStatuses() => new List<int> { 0, 454, 455, 456, 457 };

        public static List<string> GetActionParams() => new List<string>
        {
            "Skill", "RemoveTrigger", "SellType", "Choice", "Sell", "SellIfAvailable", "Split"
        };

        public override bool ShowDisabledOption() => true;

        public override string GetDescription() => "В этом приключении все, как в книгах-играх, но с одним отличием: в них нет случайности. Выживете вы или умрете – зависит от Ваших решений, а не от броска кубика.\n\nЧереда ужасных событий отбросила человечество на грань вымирания.Население стремительно уменьшается и сейчас только несколько миллионов человек рассеяны по всему миру, главным образом в городах, где все еще есть возможность искуственно производить еду.\n\nНаступил 2300 год.Богатеи все так же держатся в стороне, вымучено развлекаясь в ожидании конца.В трущобах бедняков процветают болезни.Земля между городами находится под покровом снега и льда.Никто не надеется, что человечество продержится еще столетие.Это настоящий 'конец истории'.";

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
