using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.YounglingTournament
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#c0ac6c",
            [ButtonTypes.Continue] = "#d6c078",
            [ButtonTypes.Action] = "#c0a23b",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.StatusBar] = "#e8d17e",
            [ColorTypes.StatusFont] = "#000000",
            [ColorTypes.ActionBox] = "#e6d9ae",
        };

        public override bool ShowDisabledOption() => true;

        public static List<string> GetActionParams() => new List<string>
        {
            "Level", "AccuracyBonus", "HeroHitpointsLimith", "EnemyHitpointsLimith", "HeroRoundWin", "EnemyRoundWin"
        };

        public static List<string> GetEnemyParams() => new List<string>
        {
            "Name", "MaxHitpoints", "Accuracy", "Shield", "Firepower", "Skill", "Rang"
        };

        public static Dictionary<Character.SwordTypes, string> SwordSkillsNames() => new Dictionary<Character.SwordTypes, string>
        {
            [Character.SwordTypes.Decisiveness] = "Решительности",
            [Character.SwordTypes.Elasticity] = "Эластичности",
            [Character.SwordTypes.Rivalry] = "Соперничества",
            [Character.SwordTypes.Perseverance] = "Настойчивости",
            [Character.SwordTypes.Aggressiveness] = "Агрессивности",
            [Character.SwordTypes.Confidence] = "Уверенности",
            [Character.SwordTypes.Vaapad] = "Ваапад",
            [Character.SwordTypes.JarKai] = "Джар-Кай",
        };

        public override string GetDescription() => "Сегодня вам исполнилось 13 лет. Это — предельный возраст для юнлинга, если до окончания этого года вас не возьмет на обучение один из мастеров-джедаев, вам придется или покинуть Орден, или перейти в Корпус Обслуживания.\n\nМысли о переводе заставляют вас поморщиться — они неприятны.\n\nНадо собраться.Сегодня — финальные Испытания Юнлингов, которые помогут вам осуществить мечту… Да, именно так — помогут.Никаких сомнений. Вы достойны… Ну, и другие тоже.\n\nДаже эта девчонка, Ливия Умай, достойна.Теперь достойна. Почему именно ее выбрал в падаваны Сеет Лиарт? Да, она хорошо проявила себя на прошлогодних Испытаниях, и выполнила все требования, но то же самое сделали и вы.И другие юнлинги тоже. Разве что Даммар Ви не справился, но у него будут еще две попытки, в этом и в следующем году.\n\nА Сеет Лиарт выбрал ее.Вы даже слышали краем уха, что он говорил джедаю-наставнику Кано Туру о том, что Ливия — самый многообещающий юнлинг в клане.\n\nМногообещающий! Вы плотно сжали челюсти, борясь с горечью, которую вызвало даже воспоминание о тех словах.Интересно, куда смотрел мастер-джедай? Эта девчонка ведь и фехтует-то с трудом.Только и может, что предметы швырять, да про свои видения рассказывать.\n\nМысли прерывает сигнал таймера — пора собираться.\n\nВы одеваетесь в одежды юнлинга (К1), прикалываете булавку на плечо — микромультитул (ММТ), берете свой тренировочный световой меч, и направляетесь в Большой зал.Там начнутся ваши Испытания, и там вы обретете своего учителя. Только так. Ни тени сомнения.";

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
