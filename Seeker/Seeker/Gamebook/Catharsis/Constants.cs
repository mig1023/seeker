using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.Catharsis
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#51514b",
            [ButtonTypes.Option] = "#858581",
            [ButtonTypes.Action] = "#939393",
            [ButtonTypes.Continue] = "#a9a9a6",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#cdcdcd",
            [ColorTypes.StatusBar] = "#b8b8b8",
            [ColorTypes.StatusFont] = "#000000",
            [ColorTypes.AdditionalStatus] = "#bfbfbf",
            [ColorTypes.ActionBox] = "#b8b8b8",
        };

        public static Dictionary<string, int> GetStartValues() => new Dictionary<string, int>
        {
            ["Fight"] = 10,
            ["Accuracy"] = 10,
            ["Stealth"] = 3,
        };

        public override List<int> GetParagraphsWithoutStatuses() => new List<int> { 0, 401, 402 };

        public override bool ShowDisabledOption() => true;

        public override string GetDescription() => "Полицейский «мустанг» отделился от общего потока воздушного транспорта и, быстро снижаясь, устремился к наземной дороге. Огромный плакат, под которым он пролетел, предупреждал неоновыми огнями: «Внимание! Вы перемещаетесь в Нижний Город! Передвижение по воздуху запрещено!» Первый ярус города построили еще до массового распространения летающих машин. Передвигаться здесь можно только по узким улочкам.\n\nСопла плазменного двигателя выплюнули излишки энергии, уменьшая тягу.Электрические разряды коснулись земли, подхватив и закружив в воздухе старые газеты. «Мустанг» коснулся дорожного полотна колесами и, не останавливаясь, продолжил путь как обычный автомобиль.Дорога свободна – очень мало осталось машин, передвигающихся по земле – немногие обитатели Нижнего Города могут себе их позволить. Маршал взялся за руль и переключил управление в ручной режим. Его исин, Горацио, вышел на связь с полицейским участком. – Три-Кей-Двенадцать приступил к патрулированию, – ровным голосом с металлическими интонациями доложил маршал.\n\nЛицо стража скрывала маска защитного шлема. Его внутренняя сторона представляла собой экран с панорамным обзором.На монитор в режиме онлайн выводились данные о совершаемых в этом секторе города преступлениях.\n\n– Добро пожаловать в трущобы рая! – донесся из рации веселый голос диспетчера. – Не скучной тебе смены дуболом, удачи!\n\n«Дуболомами» называли всех маршалов из-за того, что их тело на восемьдесят процентов состояло из имплантов.Маршалы на это прозвище не обижались. Многие считали, что они вообще не в состоянии обижаться или радоваться.Способность к эмоциональным переживаниям маршалы теряли после посвящения в стражи. Во всяком случае, так заявляли представители корпорации «Сайберкорп».\n\nМаршал вдавил педаль газа, увеличивая скорость машины.Очередное дежурство началось.";

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
