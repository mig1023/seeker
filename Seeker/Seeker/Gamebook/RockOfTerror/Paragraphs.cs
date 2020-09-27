using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Seeker.Game;

namespace Seeker.Gamebook.RockOfTerror
{
    class Paragraphs : Interfaces.IParagraphs
    {
        public Game.Paragraph Get(int id)
        {
            Paragraph source = Paragraph[id];

            Game.Paragraph paragraph = new Game.Paragraph();

            if (source.Options != null)
                paragraph.Options = new List<Option>(source.Options);

            if (source.Actions != null)
                paragraph.Actions = new List<Interfaces.IActions>(source.Actions);

            if (source.Modification != null)
                paragraph.Modification = new List<Interfaces.IModification>(source.Modification);

            paragraph.Trigger = source.Trigger;

            return paragraph;
        }

        private static Dictionary<int, Paragraph> Paragraph = new Dictionary<int, Paragraph>
        {
            [0] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 1, Text = "В путь!" },
                }
            },
            [1] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 51, Text = "Нужно делать первый выбор пойдете по правой тропе (на северо-восток) (51" },
                    new Option { Destination = 66, Text = ") или по левой (на северо-запад) (66" },
                }
            },
            [2] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [3] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 52, Text = "Если прошло более 11-ти часов то (52" },
                    new Option { Destination = 79, Text = "), иначе (79" },
                }
            },
            [4] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 97, Text = "Далее" },
                }
            },
            [5] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 34, Text = "Можно пойти по песчаной отмели направо, на восток (34" },
                    new Option { Destination = 84, Text = "), или все же хотите рискнуть и переплыть реку (84" },
                }
            },
            [6] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 43, Text = "Далее" },
                }
            },
            [7] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 64, Text = "Далее" },
                }
            },
            [8] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 88, Text = "Если хотите сойти с тропы и поискать источник звука (88" },
                    new Option { Destination = 24, Text = "Если нет, то продолжайте идти по тропе (24" },
                }
            },
            [9] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 44, Text = "Далее" },
                }
            },
            [10] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 45, Text = "Одна идет на восток (45" },
                    new Option { Destination = 58, Text = ") и, кажется, никак не приблизит вас к Шрекенштейну, а вторая на север (58" },
                    new Option { Destination = 59, Text = "Если жажда оказалась сильнее, и вы хотите попить (59" },
                }
            },
            [11] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [12] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 46, Text = "Но стоит ли идти по ней, когда жить вам осталось совсем недолго? Возможно, стоит поискать морозник вокруг на болоте (46" },
                    new Option { Destination = 58, Text = "), или все же пойдете по тропе дальше, надеясь найти морозник по дороге (58" },
                }
            },
            [13] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 33, Text = "Далее" },
                }
            },
            [14] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 78, Text = "Если хотите попытаться спасти его, прыгнув за ним в воду, то (78" },
                    new Option { Destination = 29, Text = "), если нет (29" },
                }
            },
            [15] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 2, Text = "Далее" },
                }
            },
            [16] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 49, Text = "Далее" },
                }
            },
            [17] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 98, Text = "Поспешите по туннелю вперед (98" },
                    new Option { Destination = 11, Text = ") или попытаетесь спуститься по лестнице, навстречу страшным звукам (11" },
                }
            },
            [18] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 97, Text = "Далее" },
                }
            },
            [19] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 44, Text = "Далее" },
                }
            },
            [20] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 64, Text = "Далее" },
                }
            },
            [21] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [22] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 16, Text = "Хотите осмотреть усыпальницы (16" },
                    new Option { Destination = 49, Text = ")? Или не будете тревожить покой мертвых и поспешите покинуть это место и вернуться на перекресток (49" },
                }
            },
            [23] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 77, Text = "Продолжите упрямо идти по тропинке (77" },
                    new Option { Destination = 4, Text = "), или сойдете с тропы и пойдете через болото прямо по направлению к Шрекенштейну (4" },
                }
            },
            [24] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [25] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 58, Text = "Далее" },
                }
            },
            [26] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 94, Text = "Хотите посмотреть, что скрывается под накидкой (94" },
                    new Option { Destination = 63, Text = "Бесшумно закроете люк и покинете дом (63" },
                }
            },
            [27] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 15, Text = "Откажитесь выступить в роли палача (15" },
                    new Option { Destination = 95, Text = ") или бросите факел в костер (95" },
                }
            },
            [28] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 85, Text = "Далее" },
                }
            },
            [29] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 32, Text = "Далее" },
                }
            },
            [30] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 21, Text = "Далее" },
                }
            },
            [31] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 56, Text = "Далее" },
                }
            },
            [32] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 13, Text = "Показалось? Хотите сойти с тропы и пойти в направлении, откуда, вам показалось, исходил странный писк (13" },
                    new Option { Destination = 33, Text = "), или не будет задерживаться, и продолжите идти по тропе (33" },
                }
            },
            [33] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 8, Text = "Далее" },
                }
            },
            [34] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 57, Text = "Далее" },
                }
            },
            [35] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [36] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [37] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 21, Text = "Далее" },
                }
            },
            [38] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 58, Text = "Далее" },
                }
            },
            [39] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 47, Text = "Справа видны темные силуэты деревьев, и вы подумываете: не пойти ли дальше пешком? Если хотите причалить и сойти на берег (47" },
                    new Option { Destination = 99, Text = "Если хотите потратить драгоценное время и силы, рискнуть и попытаться подплыть к неясному силуэту (99" },
                    new Option { Destination = 96, Text = "), или же поторопитесь к берегу, ведь вы уже проплыли середину реки, и осталось немного (96" },
                }
            },
            [40] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 11, Text = "Далее" },
                }
            },
            [41] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 87, Text = "Далее" },
                }
            },
            [42] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 2, Text = "Далее" },
                }
            },
            [43] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 57, Text = "Далее" },
                }
            },
            [44] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 3, Text = "Пойдете искать тропу с восточного склона (3" },
                    new Option { Destination = 92, Text = ")? Или с западного (92" },
                }
            },
            [45] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 56, Text = "Далее" },
                }
            },
            [46] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [47] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 5, Text = "Далее" },
                }
            },
            [48] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 61, Text = "Попробуете помочь этим людям (61" },
                    new Option { Destination = 43, Text = ")? Или оставите всё как есть и покинете дом, ведь у вас и так мало времени (43" },
                }
            },
            [49] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 80, Text = "Далее" },
                }
            },
            [50] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 2, Text = "Далее" },
                }
            },
            [51] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 68, Text = "Если хотите сменить направление и пойти на север (68" },
                    new Option { Destination = 80, Text = "), иначе продолжайте идти на северо-восток (80" },
                }
            },
            [52] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Далее" },
                }
            },
            [53] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 91, Text = "Далее" },
                }
            },
            [54] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 18, Text = "Далее" },
                }
            },
            [55] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 21, Text = "Далее" },
                }
            },
            [56] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 48, Text = "Постучите в дверь? (тогда, если прошло более четырех часов, то (48" },
                    new Option { Destination = 6, Text = ") иначе (6" },
                    new Option { Destination = 43, Text = ")), или решите не терять время и пойдете дальше (43" },
                }
            },
            [57] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 14, Text = "Далее" },
                }
            },
            [58] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 39, Text = "Тропа уводит вдоль заводи вправо (39" },
                    new Option { Destination = 9, Text = "), но при желании можно попытаться обогнуть её слева, пробираясь вдоль берега через кустарник (9" },
                }
            },
            [59] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 90, Text = "Вдоволь напившись и ополоснув лицо и руки, вы решаете, по какой тропе пойти дальше? На восток (90" },
                    new Option { Destination = 38, Text = "), на север (38" },
                }
            },
            [60] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 44, Text = "Далее" },
                }
            },
            [61] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 43, Text = "Далее" },
                }
            },
            [62] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 11, Text = "Далее" },
                }
            },
            [63] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 18, Text = "Далее" },
                }
            },
            [64] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 10, Text = "Один путь ведет на северо-восток (10" },
                    new Option { Destination = 58, Text = "), другой на северо-запад (58" },
                }
            },
            [65] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 17, Text = "Далее" },
                }
            },
            [66] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 23, Text = "Продолжите путь на северо-запад (23" },
                    new Option { Destination = 86, Text = "), или свернете на север (86" },
                }
            },
            [67] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 70, Text = "Далее" },
                }
            },
            [68] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 49, Text = "Других дорог, кроме тропы, по которой вы пришли, нет, и вы думаете о том, что бы вернуться на перекресток и выбрать другой путь (благо перекресток в 15 минутах ходьбы) (49" },
                    new Option { Destination = 22, Text = "Но можно попробовать осмотреть склеп, кто знает, какие тайны он хранит (22" },
                }
            },
            [69] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 32, Text = "Далее" },
                }
            },
            [70] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Далее" },
                }
            },
            [71] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 88, Text = "Хотите пойти по следам (88" },
                    new Option { Destination = 44, Text = ")? Или не будете менять направление, гора Шрекенштейн уже совсем рядом (44" },
                }
            },
            [72] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 98, Text = "Поспешите по туннелю вперед (98" },
                    new Option { Destination = 11, Text = "), или попытаетесь спуститься по лестнице, навстречу страшным звукам (11" },
                }
            },
            [73] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 43, Text = "Далее" },
                }
            },
            [74] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 71, Text = "Далее" },
                }
            },
            [75] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 50, Text = "Расскажите инквизитору правду (50" },
                    new Option { Destination = 27, Text = ") или соврете и скажите, что вы простой напуганный лесоруб, который убегал от зверя (27" },
                }
            },
            [76] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 41, Text = "Если смело решите идти дальше, то (41" },
                    new Option { Destination = 31, Text = "), если нет, то вам придется вернуться на перекресток и выбрать другой путь (31" },
                }
            },
            [77] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 26, Text = "Если хотите потратить впустую еще больше времени и осмотреть дом (26" },
                    new Option { Destination = 18, Text = "), или же направитесь через болота напрямик к горе Шрекенштейн (18" },
                }
            },
            [78] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 32, Text = "Далее" },
                }
            },
            [79] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 67, Text = "Поможете воину (67" },
                    new Option { Destination = 70, Text = ")? Или будете прорываться дальше к центру поляны (70" },
                }
            },
            [80] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 56, Text = "Пойдете дальше на северо-восток (по правой тропе) (56" },
                    new Option { Destination = 76, Text = ") или свернете на северо-запад (по левой тропе) (76" },
                }
            },
            [81] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 40, Text = "Надо брать книгу и выбираться (40" },
                    new Option { Destination = 93, Text = "Но можно остаться и все же открыть книгу (93" },
                }
            },
            [82] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 21, Text = "Далее" },
                }
            },
            [83] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [84] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [85] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 8, Text = "Далее" },
                }
            },
            [86] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 58, Text = "Если продолжите идти по тропе на север (где зеленый туман рассеивается) (58" },
                    new Option { Destination = 41, Text = "), или все же хотите рискнуть и свернуть на северо-восток (41" },
                }
            },
            [87] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 7, Text = "С кем бы вы хотели поговорить: с инквизитором (7" },
                    new Option { Destination = 20, Text = ") с демонологом (20" },
                }
            },
            [88] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 75, Text = "Далее" },
                }
            },
            [89] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 71, Text = "Далее" },
                }
            },
            [90] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 56, Text = "Далее" },
                }
            },
            [91] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 85, Text = "Далее" },
                }
            },
            [92] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 3, Text = "Далее" },
                }
            },
            [93] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 11, Text = "Далее" },
                }
            },
            [94] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 83, Text = "Снова посмотрите в зеркало (83" },
                    new Option { Destination = 54, Text = ")? Или броситесь в люк и поскорее покинете страшное место (54" },
                }
            },
            [95] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 44, Text = "Далее" },
                }
            },
            [96] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 71, Text = "Далее" },
                }
            },
            [97] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 12, Text = "Пойдете через поляну со змеями (12" },
                    new Option { Destination = 35, Text = "), или попытаетесь обойти препятствие по болоту (35" },
                }
            },
            [98] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 62, Text = "Поспешите в проход слева (62" },
                    new Option { Destination = 81, Text = ") или справа (81" },
                }
            },
            [99] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 89, Text = "Задержитесь и похороните несчастного, чтобы тело его больше не служило пищей крылатым падальщикам (89" },
                    new Option { Destination = 71, Text = ")? Или не будете тратить время и, так как другого пути нет, отправитесь к горе напрямик, через лес (71" },
                }
            },
            [100] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
        };
    }
}
