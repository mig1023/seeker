using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.LandOfUnwaryBears
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#9e003a",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.BookColor] = "#9e003a",
        };


        public override string GetFont() => "RobotoFont";

        public override Output.Interface.TextFontSize GetFontSize() => Output.Interface.TextFontSize.small;

        public override string GetDescription() => "Голос Леонида Володарского за кадром: «Американский самолёт-невидимка мирно пролетал над необъятными просторами России. Секретный агент никого не трогал и просто выполнял задание ЦРУ по совершению государственного переворота в этой, как ему казалось, отсталой варварской стране. Но в этот самый миг вражеский трактор встретил его шквалом зенитного огня, вынуждая катапультироваться. Как позднее выяснили звёздно-полосатые орлы в Пентагоне, никто не метил в него специально: просто пьяному русскому мужику захотелось вдруг пострелять по Луне из зенитных орудий…».";

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
