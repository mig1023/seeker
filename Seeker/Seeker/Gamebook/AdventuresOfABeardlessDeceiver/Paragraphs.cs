using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Seeker.Game;

namespace Seeker.Gamebook.AdventuresOfABeardlessDeceiver
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

            paragraph.OpenOption = source.OpenOption;

            return paragraph;
        }

        private static Dictionary<int, Paragraph> Paragraph = new Dictionary<int, Paragraph>
        {
            [1] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Поднять золотую монету" },
                    new Option { Destination = 60, Text = "Поднять бронзовый знак Тенгри" },
                    new Option { Destination = 90, Text = "Поднять сверток с одеждой" },
                }
            },
            [2] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 3, Text = "Далее" },
                }
            },
            [3] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 92, Text = "В случае успеха" },
                    new Option { Destination = 59, Text = "В случае провала" },
                }
            },
            [4] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 5, Text = "Далее" },
                }
            },
            [5] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 156, Text = "посетить базар и купить там что-нибудь полезное" },
                    new Option { Destination = 164, Text = "или пообщаться с соплеменниками" },
                }
            },
            [6] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 7, Text = "Далее" },
                }
            },
            [7] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 98, Text = "На северо-запад к кипчакам" },
                    new Option { Destination = 162, Text = "На запад к оазису" },
                    new Option { Destination = 38, Text = "На север к Иртышу" },
                }
            },
            [8] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 9, Text = "Далее" },
                }
            },
            [9] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 10, Text = "Далее" },
                }
            },
            [10] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 28, Text = "В случае успеха" },
                    new Option { Destination = 50, Text = "В случае провала" },
                }
            },
            [11] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 12, Text = "Далее" },
                }
            },
            [12] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 13, Text = "Далее" },
                }
            },
            [13] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 14, Text = "Далее" },
                }
            },
            [14] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 15, Text = "Далее" },
                }
            },
            [15] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 38, Text = "Вдоль левого берега Иртыша к ойратским землям" },
                    new Option { Destination = 68, Text = "Переправиться на безлюдное правобережье" },
                    new Option { Destination = 98, Text = "В степь к кочевью кипчаков" },
                }
            },
            [16] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 17, Text = "Далее" },
                }
            },
            [17] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 18, Text = "Далее" },
                }
            },
            [18] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 32, Text = "Если у Алдара донской рысак, то он с лёгкостью совершит длинный прыжок. Ведь казаки южнорусских степей обучают своих коней и не таким приёмам. В этом случае" },
                    new Option { Destination = 54, Text = "Иначе, можно взять немного левее" },
                    new Option { Destination = 89, Text = "В случае успеха" },
                    new Option { Destination = 129, Text = "В случае провала" },
                }
            },
            [19] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 177, Text = "Если Алдар Косе заручился поддержкой охраны оазиса" },
                    new Option { Destination = 194, Text = "Если он знает слабое место персидского торговца" },
                    new Option { Destination = 67, Text = "В случае успеха - 185. В случае провала" },
                    new Option { Destination = 8, Text = "Либо же, не обращая внимания ни на что, продолжить путь на северо-запад к караван-сараю" },
                }
            },
            [20] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 80, Text = "В случае успеха" },
                    new Option { Destination = 50, Text = "В случае провала" },
                }
            },
            [21] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 22, Text = "Далее" },
                }
            },
            [22] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 86, Text = "В случае успеха" },
                    new Option { Destination = 97, Text = "В случае провала" },
                }
            },
            [23] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [24] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 63, Text = "Если у Алдара Косе есть праздничная одежда, и он за время путешествия сменил коня (на арабского скакуна, донского рысака или ахалтекинца), откликнуться на вызов" },
                    new Option { Destination = 135, Text = "Если же у нашего героя чего-то не хватает, быстро купить нужное (одежду или скакуна) на базаре неподалёку. Главное, чтобы хватило денег" },
                    new Option { Destination = 27, Text = "Отказаться от участия в этом соревновании" },
                }
            },
            [25] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 56, Text = "Согласиться посетить поселение ойратов и поговорить с их предводителем" },
                    new Option { Destination = 140, Text = "Отказаться и продолжить путь дальше вдоль Иртыша" },
                }
            },
            [26] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 27, Text = "Далее" },
                }
            },
            [27] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 28, Text = "Далее" },
                }
            },
            [28] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 29, Text = "Далее" },
                }
            },
            [29] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Далее" },
                }
            },
            [30] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 45, Text = "В случае успеха" },
                    new Option { Destination = 35, Text = "В случае провала" },
                }
            },
            [31] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 136, Text = "Попроситься на приём к караван-баши (этот вариант доступен только, если имеется праздничная одежда)" },
                    new Option { Destination = 144, Text = "Принять приглашение погонщиков посидеть у костра" },
                    new Option { Destination = 154, Text = "Послушать, что рассказывает бородач с ослом" },
                }
            },
            [32] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 33, Text = "Далее" },
                }
            },
            [33] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 100, Text = "В случае успеха" },
                    new Option { Destination = 50, Text = "В случае провала" },
                }
            },
            [34] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 35, Text = "Далее" },
                }
            },
            [35] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 36, Text = "Далее" },
                }
            },
            [36] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 199, Text = "Если обе проверки успешны" },
                    new Option { Destination = 176, Text = "Если хотя бы одна из них провалена" },
                }
            },
            [37] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 24, Text = "Заплатить 25 ТАНЬГА и переодеться (отметить праздничную одежду в списке имущества). Тогда Алдара пропустят" },
                    new Option { Destination = 24, Text = "Если у Алдара есть ханский перстень, показать его воинам. Они обязаны будут пропустить человека несмотря на его вид" },
                    new Option { Destination = 112, Text = "В случае успеха" },
                    new Option { Destination = 83, Text = "В случае провала" },
                }
            },
            [38] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 113, Text = "Вперёд к кочевью ойратов" },
                    new Option { Destination = 133, Text = "Избежать встречи, обогнув стоянку за холмами" },
                }
            },
            [39] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 40, Text = "Далее" },
                }
            },
            [40] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 149, Text = "Если обе проверки удачны" },
                    new Option { Destination = 139, Text = "Если только одна из них удачна (неважно какая)" },
                    new Option { Destination = 159, Text = "Если обе проверки провалены" },
                }
            },
            [41] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 42, Text = "Далее" },
                }
            },
            [42] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 180, Text = "Вызвать Темир-батыра на курес, казахскую борьбу" },
                    new Option { Destination = 36, Text = "Если у Алдара есть домбра, бросить вызов на музыкальный поединок" },
                    new Option { Destination = 192, Text = "Если у нашего героя есть бутыль арака, попытаться хитростью опоить бандита" },
                    new Option { Destination = 161, Text = "Если же нужных предметов нет и не хочется пока связываться с Темир-батыром, направиться к ханской юрте для выполнения задания" },
                }
            },
            [43] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 44, Text = "Далее" },
                }
            },
            [44] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 45, Text = "Далее" },
                }
            },
            [45] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 46, Text = "Далее" },
                }
            },
            [46] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 47, Text = "Далее" },
                }
            },
            [47] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 88, Text = "В случае успеха" },
                    new Option { Destination = 188, Text = "В случае провала" },
                }
            },
            [48] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 49, Text = "Далее" },
                }
            },
            [49] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 187, Text = "Заплатить требуемую сумму. Тогда Алдара пропустят, предварительно отобрав оружие" },
                    new Option { Destination = 83, Text = "Если же таких денег нет" },
                }
            },
            [50] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 51, Text = "Далее" },
                }
            },
            [51] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 52, Text = "Далее" },
                }
            },
            [52] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 53, Text = "Далее" },
                }
            },
            [53] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 3, Text = "Река" },
                    new Option { Destination = 134, Text = "Горы" },
                }
            },
            [54] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 55, Text = "Далее" },
                }
            },
            [55] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [56] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 71, Text = "Если Алдар Косе сам спас мальчика на реке, то" },
                    new Option { Destination = 91, Text = "Если же нет, но он прибыл к ойратам в качестве гостя, то" },
                    new Option { Destination = 111, Text = "А в том случае, если он прибыл как пленник" },
                }
            },
            [57] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 58, Text = "Далее" },
                }
            },
            [58] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 59, Text = "Далее" },
                }
            },
            [59] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 60, Text = "Далее" },
                }
            },
            [60] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 75, Text = "В случае успеха" },
                    new Option { Destination = 65, Text = "В случае провала" },
                }
            },
            [61] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 2, Text = "Не взирая ни на что, рассказать правду о задании Алдара" },
                    new Option { Destination = 101, Text = "Уклончиво ответить, что Алдар всего лишь бедный кочевник, зарабатывающий на жизнь там, где подвернётся возможность" },
                }
            },
            [62] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 162, Text = "К оазису" },
                    new Option { Destination = 98, Text = "В кипчакское кочевье" },
                }
            },
            [63] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 64, Text = "Далее" },
                }
            },
            [64] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 65, Text = "Далее" },
                }
            },
            [65] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 66, Text = "Далее" },
                }
            },
            [66] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 163, Text = "Если Алдару удалось получить комнату в караван-сарае" },
                    new Option { Destination = 172, Text = "Если же он будет спать во внутреннем дворе у костра" },
                }
            },
            [67] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 68, Text = "Далее" },
                }
            },
            [68] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 201, Text = "Двигаться и дальше вдоль реки" },
                    new Option { Destination = 103, Text = "Срезать путь по прямой, углубившись в лес" },
                }
            },
            [69] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 70, Text = "Далее" },
                }
            },
            [70] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 71, Text = "Далее" },
                }
            },
            [71] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 2, Text = "Не взирая ни на что, рассказать правду о задании Алдара" },
                    new Option { Destination = 14, Text = "Уклончиво ответить, что Алдар всего лишь бедный кочевник, зарабатывающий на жизнь там, где подвернется возможность" },
                }
            },
            [72] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 73, Text = "Далее" },
                }
            },
            [73] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 196, Text = "В случае успеха" },
                    new Option { Destination = 186, Text = "В случае провала" },
                }
            },
            [74] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 197, Text = "В случае успеха" },
                    new Option { Destination = 4, Text = "В случае провала" },
                }
            },
            [75] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 76, Text = "Далее" },
                }
            },
            [76] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 106, Text = "Если у Алдара арабский скакун, то он натренирован не бояться змей, которых много в пустынях аравийского полуострова. В этом случае" },
                    new Option { Destination = 119, Text = "В случае успеха" },
                    new Option { Destination = 141, Text = "В случае провала" },
                }
            },
            [77] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 78, Text = "Далее" },
                }
            },
            [78] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 40, Text = "Если у Алдара есть такие деньги и желание, то можно сделать ставку и сыграть в нарды" },
                    new Option { Destination = 128, Text = "Или можно просто посмотреть, как другие играют" },
                }
            },
            [79] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 66, Text = "Посидеть в чайхане и поговорить с торговцами" },
                    new Option { Destination = 87, Text = "Пойти на приём к местному беку" },
                    new Option { Destination = 126, Text = "Посмотреть на танцовщиц" },
                }
            },
            [80] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 81, Text = "Далее" },
                }
            },
            [81] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 151, Text = "- А какую награду ты желаешь себе, Алдар Косе? Проси всего, что хочешь! Если имя Сауле знакомо Алдару Косе" },
                    new Option { Destination = 204, Text = "Если оно ни о чём не говорит" },
                }
            },
            [82] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 114, Text = "Сделать выбор в пользу медового напитка" },
                    new Option { Destination = 147, Text = "Попросить собеседников налить полугар" },
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
                    new Option { Destination = 85, Text = "Далее" },
                }
            },
            [85] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 86, Text = "Далее" },
                }
            },
            [86] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 87, Text = "Далее" },
                }
            },
            [87] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 88, Text = "Далее" },
                }
            },
            [88] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 89, Text = "Далее" },
                }
            },
            [89] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 90, Text = "Далее" },
                }
            },
            [90] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 105, Text = "В случае успеха" },
                    new Option { Destination = 95, Text = "В случае провала" },
                }
            },
            [91] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 92, Text = "Далее" },
                }
            },
            [92] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 93, Text = "Далее" },
                }
            },
            [93] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 94, Text = "Далее" },
                }
            },
            [94] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 95, Text = "Далее" },
                }
            },
            [95] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 96, Text = "Далее" },
                }
            },
            [96] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 97, Text = "Далее" },
                }
            },
            [97] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 98, Text = "Далее" },
                }
            },
            [98] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 158, Text = "Попроситься на приём к Джанибек-батыру" },
                    new Option { Destination = 131, Text = "Поучаствовать в ат-омырауластыру" },
                    new Option { Destination = 47, Text = "Заговорить с бандитами и обвинить их в разбойном нападении" },
                }
            },
            [99] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 100, Text = "Далее" },
                }
            },
            [100] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 101, Text = "Далее" },
                }
            },
            [101] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 102, Text = "Далее" },
                }
            },
            [102] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 103, Text = "Далее" },
                }
            },
            [103] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 99, Text = "Отказаться от испытания и продолжить путь вдоль реки" },
                    new Option { Destination = 190, Text = "Показать силушку немереную" },
                    new Option { Destination = 203, Text = "Показать удаль молодецкую" },
                    new Option { Destination = 53, Text = "Показать острый ум" },
                }
            },
            [104] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 105, Text = "Далее" },
                }
            },
            [105] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 106, Text = "Далее" },
                }
            },
            [106] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 96, Text = "Если ЕДИНИЦЫ ВРЕМЕНИ уменьшились до нуля" },
                    new Option { Destination = 58, Text = "Если счёт всё ещё больше нуля" },
                }
            },
            [107] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 78, Text = "Пообщаться с казахскими воинами - 23. Посмотреть на товары" },
                    new Option { Destination = 124, Text = "Просто побродить по оазису, наблюдая за его обитателями" },
                }
            },
            [108] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 109, Text = "Далее" },
                }
            },
            [109] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 66, Text = "Посидеть в чайхане и поговорить с торговцами" },
                    new Option { Destination = 87, Text = "Пойти на приём к местному беку" },
                    new Option { Destination = 126, Text = "Посмотреть на танцовщиц" },
                    new Option { Destination = 138, Text = "Поиграть в нарды" },
                }
            },
            [110] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 111, Text = "Далее" },
                }
            },
            [111] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 112, Text = "Далее" },
                }
            },
            [112] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 113, Text = "Далее" },
                }
            },
            [113] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 10, Text = "Пуститься вплавь на коне" },
                    new Option { Destination = 20, Text = "Пуститься вплавь без коня" },
                    new Option { Destination = 33, Text = "Использовать верёвку, перебросив один конец мальчику" },
                }
            },
            [114] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 145, Text = "Если Лихарев обещал подарить коня, то" },
                    new Option { Destination = 175, Text = "Если же нет, то покинуть крепость" },
                }
            },
            [115] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 44, Text = "В случае успеха" },
                    new Option { Destination = 55, Text = "В случае провала" },
                }
            },
            [116] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [117] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 118, Text = "Далее" },
                }
            },
            [118] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 119, Text = "Далее" },
                }
            },
            [119] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 96, Text = "Если ЕДИНИЦЫ ВРЕМЕНИ уменьшились до нуля" },
                    new Option { Destination = 58, Text = "Если счёт всё ещё больше нуля" },
                }
            },
            [120] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 5, Text = "На северо-запад в кочевье найманов" },
                    new Option { Destination = 202, Text = "На запад к караванному пути" },
                    new Option { Destination = 160, Text = "На север к реке Иртыш" },
                }
            },
            [121] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 122, Text = "Далее" },
                }
            },
            [122] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 123, Text = "Далее" },
                }
            },
            [123] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 182, Text = "Если у Алдара Косе жеребец-ахалтекинец, то проблем не будет. Ведь туркмены за многие столетия вывели эту породу лошадей специально для плавного хода. В этом случае перейти на" },
                    new Option { Destination = 153, Text = "В случае успеха - 169. В случае провала" },
                }
            },
            [124] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 125, Text = "Далее" },
                }
            },
            [125] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 126, Text = "Далее" },
                }
            },
            [126] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 51, Text = "Бросить в блюдо 5 ТАНЬГА" },
                    new Option { Destination = 195, Text = "Бросить в блюдо 15 ТАНЬГА" },
                    new Option { Destination = 163, Text = "Если Алдару удалось получить комнату в караван-сарае" },
                    new Option { Destination = 172, Text = "Если же он будет спать во внутреннем дворе у костра" },
                }
            },
            [127] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 128, Text = "Далее" },
                }
            },
            [128] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 129, Text = "Далее" },
                }
            },
            [129] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 130, Text = "Далее" },
                }
            },
            [130] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 43, Text = "Если у Алдара Косе записано «наставление Афанасия», то" },
                    new Option { Destination = 108, Text = "Если же нет ни того ни другого, тогда ПРОВЕРКА МУДРОСТИ, уровень - 11 В случае успеха - 181. В случае провала" },
                }
            },
            [131] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 125, Text = "Если у Алдара Косе есть 10 ТАНЬГА и желание, принять вызов" },
                    new Option { Destination = 168, Text = "Иначе покинуть кочевье кипчаков" },
                }
            },
            [132] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 9, Text = "Если СЛАВА АКЫНА равна 6 или больше" },
                    new Option { Destination = 52, Text = "Если она достигает значения от 3 до 5" },
                    new Option { Destination = 117, Text = "Если же этот параметр равняется 1 или 2" },
                    new Option { Destination = 191, Text = "А если СЛАВА АКЫНА осталась на нуле или даже уменьшилась" },
                }
            },
            [133] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 183, Text = "Сдаться на милость победителя" },
                    new Option { Destination = 193, Text = "Попытаться обхитрить воина" },
                    new Option { Destination = 173, Text = "Вступить с ним в борьбу" },
                }
            },
            [134] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 135, Text = "Далее" },
                }
            },
            [135] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 136, Text = "Далее" },
                }
            },
            [136] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 166, Text = "В случае успеха" },
                    new Option { Destination = 184, Text = "В случае провала" },
                }
            },
            [137] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [138] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 29, Text = "Если обе проверки удачны" },
                    new Option { Destination = 39, Text = "Если только одна из них удачна (неважно какая)" },
                    new Option { Destination = 79, Text = "Если обе проверки провалены" },
                }
            },
            [139] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 140, Text = "Далее" },
                }
            },
            [140] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 141, Text = "Далее" },
                }
            },
            [141] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 142, Text = "Далее" },
                }
            },
            [142] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 143, Text = "Далее" },
                }
            },
            [143] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [144] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 145, Text = "Далее" },
                }
            },
            [145] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 146, Text = "Далее" },
                }
            },
            [146] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 56, Text = "Согласиться посетить поселение ойратов и поговорить с их предводителем" },
                    new Option { Destination = 140, Text = "Отказаться и продолжить путь дальше вдоль Иртыша" },
                }
            },
            [147] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 145, Text = "Если Лихарев обещал подарить скакуна, то" },
                    new Option { Destination = 175, Text = "Если же нет, покинуть крепость" },
                }
            },
            [148] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 48, Text = "В случае успеха" },
                    new Option { Destination = 69, Text = "В случае провала" },
                }
            },
            [149] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 150, Text = "Далее" },
                }
            },
            [150] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [151] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [152] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 153, Text = "Далее" },
                }
            },
            [153] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 154, Text = "Далее" },
                }
            },
            [154] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 155, Text = "Далее" },
                }
            },
            [155] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 24, Text = "Если у кочевника есть праздничная одежда, то он переодевается и его беспрепятственно пропускают" },
                    new Option { Destination = 37, Text = "Если одежды нет" },
                }
            },
            [156] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 74, Text = "Принять участие в айтысе (необходима домбра)" },
                    new Option { Destination = 94, Text = "Отказаться от участия и просто посмотреть на соревнование" },
                }
            },
            [157] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 158, Text = "Далее" },
                }
            },
            [158] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 159, Text = "Далее" },
                }
            },
            [159] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 160, Text = "Далее" },
                }
            },
            [160] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 152, Text = "Использовать огонь из костра" },
                    new Option { Destination = 170, Text = "Использовать лук и стрелы" },
                }
            },
            [161] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 6, Text = "Если у Алдара есть какой-то из предметов, который можно предъявить в качестве пропуска: ханский перстень, письмо Гулинши-багатура, сообщение из бухарского каравана или записка Джанибека" },
                    new Option { Destination = 16, Text = "Если наш герой только что сумел посрамить Темир-батыра (неважно как, в поединке или с помощью арака) или ПОПУЛЯРНОСТЬ Алдара Косе равняется 10 или больше" },
                    new Option { Destination = 49, Text = "Иначе придётся посулить денег охране" },
                    new Option { Destination = 83, Text = "Если ни один из указанных вариантов не подходит" },
                }
            },
            [162] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 163, Text = "Далее" },
                }
            },
            [163] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 164, Text = "Далее" },
                }
            },
            [164] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 165, Text = "Далее" },
                }
            },
            [165] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 130, Text = "Если у Алдара Косе есть зелёный камень, то" },
                    new Option { Destination = 157, Text = "Если же нет, то" },
                }
            },
            [166] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 167, Text = "Далее" },
                }
            },
            [167] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 168, Text = "Далее" },
                }
            },
            [168] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 140, Text = "На север к русской крепости" },
                    new Option { Destination = 8, Text = "На запад к караван-сараю" },
                }
            },
            [169] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 170, Text = "Далее" },
                }
            },
            [170] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 142, Text = "В случае успеха" },
                    new Option { Destination = 122, Text = "В случае провала" },
                }
            },
            [171] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 172, Text = "Далее" },
                }
            },
            [172] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 173, Text = "Далее" },
                }
            },
            [173] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 85, Text = "Если обе проверки успешны" },
                    new Option { Destination = 25, Text = "Если только одна из них успешна (неважно какая)" },
                    new Option { Destination = 183, Text = "Если обе проверки провалились" },
                }
            },
            [174] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 42, Text = "Если ПОПУЛЯРНОСТЬ Алдара Косе равняется 6 или больше" },
                    new Option { Destination = 11, Text = "Если же ПОПУЛЯРНОСТЬ меньше 6" },
                }
            },
            [175] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 176, Text = "Далее" },
                }
            },
            [176] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 177, Text = "Далее" },
                }
            },
            [177] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 178, Text = "Далее" },
                }
            },
            [178] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 179, Text = "Далее" },
                }
            },
            [179] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 180, Text = "Далее" },
                }
            },
            [180] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 13, Text = "Если обе проверки успешны" },
                    new Option { Destination = 171, Text = "Если хотя бы одна из них провалена" },
                }
            },
            [181] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 182, Text = "Далее" },
                }
            },
            [182] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 183, Text = "Далее" },
                }
            },
            [183] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 184, Text = "Далее" },
                }
            },
            [184] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 185, Text = "Далее" },
                }
            },
            [185] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 186, Text = "Далее" },
                }
            },
            [186] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 187, Text = "Далее" },
                }
            },
            [187] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 102, Text = "Если у Алдара Косе есть какое-нибудь доказательство в подтверждение своих слов (письмо Гулинши-багатура, записка Джанибека или сообщение из бухарского каравана), предъявить его" },
                    new Option { Destination = 81, Text = "В случае успеха" },
                    new Option { Destination = 116, Text = "В случае провала" },
                }
            },
            [188] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 189, Text = "Далее" },
                }
            },
            [189] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 190, Text = "Далее" },
                }
            },
            [190] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 127, Text = "В случае успеха" },
                    new Option { Destination = 72, Text = "В случае провала" },
                }
            },
            [191] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 192, Text = "Далее" },
                }
            },
            [192] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 193, Text = "Далее" },
                }
            },
            [193] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 146, Text = "В случае успеха" },
                    new Option { Destination = 183, Text = "В случае провала" },
                }
            },
            [194] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 195, Text = "Далее" },
                }
            },
            [195] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 196, Text = "Далее" },
                }
            },
            [196] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 197, Text = "Далее" },
                }
            },
            [197] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Далее" },
                }
            },
            [198] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [199] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 200, Text = "Далее" },
                }
            },
            [200] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 201, Text = "Далее" },
                }
            },
            [201] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [202] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 203, Text = "Далее" },
                }
            },
            [203] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 110, Text = "В случае успеха" },
                    new Option { Destination = 200, Text = "В случае провала" },
                }
            },
            [204] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
        };
    }
}
