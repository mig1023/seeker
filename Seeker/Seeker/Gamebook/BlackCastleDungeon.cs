using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Seeker.Game;

namespace Seeker.Gamebook
{
    class BlackCastleDungeon
    {
        public static int CONST = 7;

        public static Dictionary<int, Paragraph> Paragraphs = new Dictionary<int, Paragraph>
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
                    new Option { Destination = 86, Text = "По правой" },
                    new Option { Destination = 110, Text = "По левой" },
                }
            },
            [2] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 175, Text = "Поднимитесь на холм" },
                    new Option { Destination = 97, Text = "Пойдете дальше по дороге" },
            }
            },
            [3] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 211, Text = "Далее" },
                }
            },
            [4] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 416, Text = "Вернуться обратно и направиться к зданию в центре двора" },
                    new Option { Destination = 372, Text = "Заклятие Плавания" },
                    new Option { Destination = 103, Text = "Заклятие Левитации" },
                    new Option { Destination = 311, Text = "Заклятием Плавания и поплыть по течению реки" },
                }
            },
            [5] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 216, Text = "К острову" },
                    new Option { Destination = 517, Text = "На другой берег" },
                }
            },
            [6] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 420, Text = "Отправиться дальше" },
                }
            },
            [7] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 36, Text = "Заклятия Силы" },
                    new Option { Destination = 314, Text = "Заклятия Слабости" },
                    new Option { Destination = 112, Text = "Заклятия Огня" },
                    new Option { Destination = 183, Text = "Драться" },
                }
            },
            [8] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 118, Text = "К мосту" },
                }
            },
            [9] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 222, Text = "Далее" },
                }
            },
            [10] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 399, Text = "Войти" },
                    new Option { Destination = 325, Text = "По коридору" },
                }
            },
            [11] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 234, Text = "Далее" },
                }
            },
            [12] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 312, Text = "Построите плот" },
                    new Option { Destination = 201, Text = "Воспользуетесь заклятием Плавания" },
                    new Option { Destination = 425, Text = "Воспользуетесь заклятием Левитации" },
                }
            },
            [13] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 360, Text = "Пересечь дорогу и идти дальше в обход деревни" },
                    new Option { Destination = 184, Text = "Выйти на дорогу и пойти к деревне" },
                    new Option { Destination = 235, Text = "Выйти на дорогу и пойти от деревни" },
                }
            },
            [14] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 338, Text = "Вы удачливы" },
                    new Option { Destination = 404, Text = "Если нет, то ваше решение принято слишком поздно" },
                }
            },
            [15] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 417, Text = "Заклятие Силы" },
                    new Option { Destination = 228, Text = "Заклятие Слабости" },
                    new Option { Destination = 521, Text = "Заклятие Огня" },
                    new Option { Destination = 326, Text = "Драться без помощи магии" },
                }
            },
            [16] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 557, Text = "Воспользоваться заклятием Огня" },
                    new Option { Destination = 120, Text = "Если вы победите их, осмотреть домик" },
                    new Option { Destination = 416, Text = "Уйти и направиться к центральному строению" },
                }
            },
            [17] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 426, Text = "Отдадите им все золото" },
                    new Option { Destination = 109, Text = "Вы идете сражаться с волшебником и избавлять Зачарованный лес от нечисти" },
                    new Option { Destination = 339, Text = "Вы встречали по дороге кого-то из их знакомых" },
                }
            },
            [18] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 327, Text = "Заплатите ему" },
                    new Option { Destination = 217, Text = "Поищите в заплечном мешке подарок" },
                    new Option { Destination = 106, Text = "Вежливо поблагодарите и уйдете" },
                    new Option { Destination = 518, Text = "Лучше убить его и обыскать лавку" },
                }
            },
            [19] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 185, Text = "Пойдете дальше" },
                    new Option { Destination = 176, Text = "Зайдете в дом" },
                }
            },
            [20] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 340, Text = "Поискать убежища на ночь" },
                    new Option { Destination = 442, Text = "Пойти дальше" },
                }
            },
            [21] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 186, Text = "Воспользуетесь предложением и скажете, что ищете Черный замок" },
                    new Option { Destination = 202, Text = "Попросите проводить вас до ближайшего жилья" },
                    new Option { Destination = 229, Text = "Откажетесь и уйдете" },
                }
            },
            [22] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 427, Text = "Покинуть столовую либо через дверь в той же стене, где вход" },
                    new Option { Destination = 398, Text = "Покинуть через правую дверь" },
                    new Option { Destination = 206, Text = "Покинуть через левую дверь" },
                    new Option { Destination = 350, Text = "Покинуть через среднюю дверь" },
                }
            },
            [23] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 327, Text = "Заплатить ему" },
                    new Option { Destination = 217, Text = "Поищите в заплечном мешке подарок" },
                    new Option { Destination = 106, Text = "Вежливо поблагодарите и уйдете" },
                    new Option { Destination = 518, Text = "Лучше убить его и поискать, нет ли чего интересного в лавке" },
                }
            },
            [24] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 238, Text = "Далее" },
                }
            },
            [25] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 427, Text = "Через дверь в той же стене, где вход" },
                    new Option { Destination = 398, Text = "Через правую дверь" },
                    new Option { Destination = 206, Text = "Через левую дверь" },
                    new Option { Destination = 350, Text = "Через среднюю дверь" },
                }
            },
            [26] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 341, Text = "Далее" },
                }
            },
            [27] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 137, Text = "Дадите ей денег" },
                    new Option { Destination = 522, Text = "Будете драться" },
                }
            },
            [28] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 443, Text = "Он вполне доволен" },
                }
            },
            [29] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 235, Text = "Пойти по дороге" },
                    new Option { Destination = 184, Text = "Войти в деревню" },
                }
            },
            [30] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 10, Text = "Далее" },
                }
            },
            [31] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 48, Text = "Покажите шкура лисы" },
                    new Option { Destination = 428, Text = "Придется драться" },
                }
            },
            [32] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 207, Text = "Далее" },
                }
            },
            [33] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 143, Text = "Если хотите, можете попробовать убежать" },
                    new Option { Destination = 239, Text = "Гоблин убит" },
                }
            },
            [34] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 3, Text = "Далее" },
                }
            },
            [35] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 204, Text = "Отдохнуть" },
                    new Option { Destination = 342, Text = "Пойдете дальше" },
                }
            },
            [36] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 183, Text = "Далее" },
                }
            },
            [37] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 451, Text = "Заклятие Огня" },
                    new Option { Destination = 454, Text = "Дракон убит" },
                }
            },
            [38] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 221, Text = "Войти внутрь" },
                    new Option { Destination = 55, Text = "Пройти мимо" },
                }
            },
            [39] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 138, Text = "В дверь" },
                    new Option { Destination = 523, Text = "По лестнице вверх" },
                    new Option { Destination = 452, Text = "По лестнице вниз" },
                    new Option { Destination = 70, Text = "Подойдете к окну" },
                    new Option { Destination = 205, Text = "К картине" },
                }
            },
            [40] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 118, Text = "Далее" },
                }
            },
            [41] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 454, Text = "Далее" },
                }
            },
            [42] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 316, Text = "Далее" },
                }
            },
            [43] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 409, Text = "Далее" },
                }
            },
            [44] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 431, Text = "Поверите ему" },
                    new Option { Destination = 240, Text = "Осторожность никогда не помешает" },
                }
            },
            [45] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 345, Text = "Попросить книгу о самом волшебнике" },
                    new Option { Destination = 208, Text = "Попросить книгу о Принцессе" },
                    new Option { Destination = 139, Text = "Покинуть библиотеку" },
                }
            },
            [46] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 445, Text = "Направо" },
                    new Option { Destination = 524, Text = "Налево" },
                }
            },
            [47] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 101, Text = "К деревне?" },
                    new Option { Destination = 262, Text = "К лесу?" },
                }
            },
            [48] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 308, Text = "К строению в центре двора " },
                    new Option { Destination = 218, Text = "К сторожке" },
                    new Option { Destination = 116, Text = "К маленькому домику слева" },
                }
            },
            [49] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 14, Text = "Быстро вернетесь на перекресток и пойдете в другую сторону" },
                    new Option { Destination = 404, Text = "Останетесь на дороге" },
                }
            },
            [50] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 310, Text = "Скажете, что вы идете в Черный замок сражаться с волшебником" },
                    new Option { Destination = 418, Text = "Попытаетесь осторожно выведать у него что-нибудь еще, не называя себя" },
                    new Option { Destination = 179, Text = "Покинете деревню" },
                }
            },
            [51] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 21, Text = "Свернете к нему" },
                    new Option { Destination = 400, Text = "Поторопитесь к цели" },
                }
            },
            [52] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 373, Text = "Пойдете по тропинке" },
                    new Option { Destination = 121, Text = "Дальше по дороге" },
                }
            },
            [53] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 519, Text = "Попробуете с ними поговорить" },
                    new Option { Destination = 328, Text = "Будете драться" },
                }
            },
            [54] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 558, Text = "Если вы удачливы" },
                    new Option { Destination = 410, Text = "Заклятие Силы" },
                    new Option { Destination = 219, Text = "Заклятие Слабости" },
                    new Option { Destination = 189, Text = "Если вы победили" },
                }
            },
            [55] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 121, Text = "Направо" },
                    new Option { Destination = 188, Text = "Налево" },
                }
            },
            [56] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [57] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 181, Text = "Далее" },
                }
            },
            [58] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 40, Text = "Далее" },
                }
            },
            [59] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 240, Text = "Идти дальше вперед" },
                    new Option { Destination = 528, Text = "Свернуть с дороги" },
                }
            },
            [60] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 181, Text = "Далее" },
                }
            },
            [61] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 267, Text = "Достаньте перо павлина" },
                    new Option { Destination = 147, Text = "Кто сидит за следующей решеткой" },
                }
            },
            [62] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 457, Text = "Амулет" },
                    new Option { Destination = 156, Text = "Пояс" },
                    new Option { Destination = 367, Text = "Шкуру" },
                    new Option { Destination = 44, Text = "Отказаться" },
                }
            },
            [63] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 416, Text = "Далее" },
                }
            },
            [64] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 340, Text = "Поискать убежища на ночь" },
                    new Option { Destination = 442, Text = "Пойти вперед" },
                }
            },
            [65] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 468, Text = "Далее" },
                }
            },
            [66] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 46, Text = "Далее" },
                }
            },
            [67] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 98, Text = "Далее" },
                }
            },
            [68] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 160, Text = "Далее" },
                }
            },
            [69] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [70] = new Paragraph
            {
                Options = new List<Option>
              {
                    new Option { Destination = 39, Text = "Отойти от окна" },
                    new Option { Destination = 273, Text = "Воспользоваться заклятием Левитации" },
              }
            },
            [71] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 368, Text = "Правый" },
                    new Option { Destination = 482, Text = "Cредний" },
                    new Option { Destination = 536, Text = "Левый" },
                }
            },
            [72] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 340, Text = "Поискать убежище на ночь" },
                    new Option { Destination = 442, Text = "Пойти дальше" },
                }
            },
            [73] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 286, Text = "Налево" },
                    new Option { Destination = 470, Text = "Дальше" },
                }
            },
            [74] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 257, Text = "Далее" },
                }
            },
            [75] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 247, Text = "Драться" },
                }
            },
            [76] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 379, Text = "Выйти" },
                    new Option { Destination = 486, Text = "Поговорить" },
                }
            },
            [77] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 378, Text = "Уйти" },
                }
            },
            [78] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 497, Text = "Далее" },
                }
            },
            [79] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 325, Text = "Выбежать в следующую дверь" },
                }
            },
            [80] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 322, Text = "Открыть" },
                }
            },
            [81] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 51, Text = "Вернуться" },
                }
            },
            [82] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 280, Text = "Сражаться с волшебником" },
                    new Option { Destination = 384, Text = "Освобождать Принцессу" },
                    new Option { Destination = 492, Text = "Забрели в лес случайно" },
                    new Option { Destination = 563, Text = "Обнажить меч" },
                }
            },
            [83] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 247, Text = "Вернуться" },
                }
            },
            [84] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 547, Text = "Правую" },
                    new Option { Destination = 501, Text = "Левую" },
                }
            },
            [85] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 569, Text = "Сражайтесь" },
                }
            },
            [86] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 263, Text = "Налево" },
                    new Option { Destination = 403, Text = "Направо" },
                }
            },
            [87] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 331, Text = "Дальше в обход" },
                }
            },
            [88] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 177, Text = "Через кустарник" },
                    new Option { Destination = 212, Text = "К озеру" },
                }
            },
            [89] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 419, Text = "Поговорить с ними" },
                    new Option { Destination = 104, Text = "Предложить еду" },
                    new Option { Destination = 374, Text = "Драться" },
                }
            },
            [90] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Вернуться" },
                }
            },
            [91] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 379, Text = "Уйти" },
                }
            },
            [92] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 157, Text = "Говорить с жёнами" },
                    new Option { Destination = 255, Text = "Принять другое решение" },
                }
            },
            [93] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 301, Text = "Выйти в дверь" },
                }
            },
            [94] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 264, Text = "Войти" },
                }
            },
            [95] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 98, Text = "Уйти" },
                }
            },
            [96] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 241, Text = "Дальше по коридору" },
                }
            },
            [97] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 306, Text = "Удачлив" },
                }
            },
            [98] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 412, Text = "Далее" },
                }
            },
            [99] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [100] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 375, Text = "К реке" },
                    new Option { Destination = 424, Text = "К палаткам" },
                }
            },
            [101] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 405, Text = "Попить" },
                    new Option { Destination = 307, Text = "Наполнить флягу" },
                    new Option { Destination = 261, Text = "Пойти дальше" },
                }
            },
            [102] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 8, Text = "Далее" },
                }
            },
            [103] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 424, Text = "Далее" },
                }
            },
            [104] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 520, Text = "Далее" },
                }
            },
            [105] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 413, Text = "К острову" },
                    new Option { Destination = 425, Text = "На другой берег" },
                }
            },
            [106] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 329, Text = "Направо" },
                    new Option { Destination = 432, Text = "Налево" },
                    new Option { Destination = 20, Text = "Прямо" },
                }
            },
            [107] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 332, Text = "Поблагодарить и уйти" },
                    new Option { Destination = 209, Text = "Купить у него еду" },
                    new Option { Destination = 376, Text = "Попить воды" },
                    new Option { Destination = 15, Text = "Напасть на него" },
                }
            },
            [108] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 407, Text = "Далее" },
                }
            },
            [109] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 453, Text = "Удачлив" },
                    new Option { Destination = 242, Text = "Нет" },
                }
            },
            [110] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 302, Text = "Подойти и выяснить" },
                    new Option { Destination = 234, Text = "Оставите умирать" },
                }
            },
            [111] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 309, Text = "Вернуться" },
                }
            },
            [112] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 414, Text = "Далее" },
                }
            },
            [113] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 446, Text = "В правую" },
                    new Option { Destination = 330, Text = "В левую" },
                    new Option { Destination = 397, Text = "В противоположной стене" },
                }
            },
            [114] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [115] = new Paragraph
            {
                Options = new List<Option>
               {
                    new Option { Destination = 424, Text = "Далее" },
               }
            },
            [116] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 16, Text = "Войдете" },
                    new Option { Destination = 416, Text = "К центральному строению" },
                    new Option { Destination = 4, Text = "К реке" },
                }
            },
            [117] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 21, Text = "Войдете" },
                    new Option { Destination = 180, Text = "Направо" },
                    new Option { Destination = 334, Text = "Налево" },
                }
            },
            [118] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 190, Text = "Прямо к воротам" },
                    new Option { Destination = 236, Text = "Пойти налево в обход замка" },
                }
            },
            [119] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 529, Text = "Защищаться" },
                    new Option { Destination = 447, Text = "Поговорить с ней" },
                }
            },
            [120] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 433, Text = "Заглянете в сундук" },
                    new Option { Destination = 227, Text = "Исследуете люк" },
                    new Option { Destination = 416, Text = "Уйти" },
                }
            },
            [121] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 525, Text = "К дереву" },
                    new Option { Destination = 210, Text = "Дальше по дороге" },
                }
            },
            [122] = new Paragraph
            {
                Options = new List<Option>
                {
		    // !!
                    new Option { Destination = 48, Text = "Если вы пришли с параграфа 319, то отправляйтесь на 48," },
                    new Option { Destination = 100, Text = "Если же с параграфа 190, то на 100." },
                }
            },
            [123] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 46, Text = "Уйти" },
                    new Option { Destination = 335, Text = "Обшарить карманы убитых" },
                }
            },
            [124] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 327, Text = "Заплатите ему" },
                    new Option { Destination = 217, Text = "Поищите подарок" },
                    new Option { Destination = 106, Text = "Поблагодарите и уйдете" },
                    new Option { Destination = 518, Text = "Убить и осмотреть лавку" },
                }
            },
            [125] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 21, Text = "Войдете" },
                    new Option { Destination = 180, Text = "На тропинку направо, уходящую в лес" },
                    new Option { Destination = 334, Text = "Налево через сад" },
                }
            },
            [126] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 434, Text = "Далее" },
                }
            },
            [127] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 225, Text = "Далее" },
                }
            },
            [128] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 207, Text = "Далее" },
                }
            },
            [129] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 184, Text = "Идти в деревню" },
                    new Option { Destination = 223, Text = "Двинуться по лесу" },
                }
            },
            [130] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 39, Text = "Осмотреться" },
                }
            },
            [131] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 305, Text = "Далее" },
                }
            },
            [132] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 336, Text = "Сорвать его" },
                    new Option { Destination = 24, Text = "Выбираться с острова" },
                }
            },
            [133] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 225, Text = "Далее" },
                }
            },
            [134] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 454, Text = "Далее" },
                }
            },
            [135] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 422, Text = "Прямо" },
                    new Option { Destination = 435, Text = "Налево" },
                }
            },
            [136] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 337, Text = "Далее" },
                }
            },
            [137] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 458, Text = "Три" },
                    new Option { Destination = 369, Text = "Шесть" },
                    new Option { Destination = 274, Text = "Восемь" },
                    new Option { Destination = 522, Text = "Отгонять ее мечом" },
                }
            },
            [138] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 243, Text = "Войдете в комнату" },
                    new Option { Destination = 39, Text = "Другой выбор" },
                }
            },
            [139] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 530, Text = "Понюхать цветы" },
                    new Option { Destination = 471, Text = "Сорвать лук" },
                    new Option { Destination = 268, Text = "Сорвать огурец" },
                    new Option { Destination = 80, Text = "Пройти через комнату" },
                }
            },
            [140] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 322, Text = "Далее" },
                }
            },
            [141] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 409, Text = "Далее" },
                }
            },
            [142] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 443, Text = "Согласиться с предложением" },
                    new Option { Destination = 106, Text = "Отказаться и уйти" },
                }
            },
            [143] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 531, Text = "Далее" },
                }
            },
            [144] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 465, Text = "Далее" },
                }
            },
            [145] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 525, Text = "Свернете к дереву" },
                    new Option { Destination = 188, Text = "Пойдете дальше" },
                }
            },
            [146] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 428, Text = "Драться" },
                }
            },
            [147] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 460, Text = "Показать серебряный свисток" },
                    new Option { Destination = 348, Text = "Возвратиться и пойти налево" },
                    new Option { Destination = 537, Text = "Возвратиться и пойти обратно" },
                }
            },
            [148] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 464, Text = "Далее" },
                }
            },
            [149] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 98, Text = "Далее" },
                }
            },
            [150] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 269, Text = "Пройти до конца" },
                    new Option { Destination = 416, Text = "Вылезти назад и уйти" },
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
                    new Option { Destination = 472, Text = "С правой" },
                    new Option { Destination = 275, Text = "С левой" },
                }
            },
            [153] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 538, Text = "Заклятие Огня" },
                    new Option { Destination = 370, Text = "Заклятие Иллюзии" },
                }
            },
            [154] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 559, Text = "Направо" },
                    new Option { Destination = 181, Text = "Налево" },
                    new Option { Destination = 445, Text = "Прямо" },
                }
            },
            [155] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 461, Text = "Прямо" },
                    new Option { Destination = 250, Text = "Направо" },
                }
            },
            [156] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 44, Text = "Далее" },
                }
            },
            [157] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [158] = new Paragraph
            {
                Options = new List<Option>
               {
                    new Option { Destination = 484, Text = "Далее" },
               }
            },
            [159] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 540, Text = "Средний сундук" },
                    new Option { Destination = 380, Text = "Маленький сундук" },
                    new Option { Destination = 39, Text = "Вернетесь обратно" },
                }
            },
            [160] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 371, Text = "Напасть на него" },
                    new Option { Destination = 276, Text = "Пойдете дальше" },
                }
            },
            [161] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 316, Text = "Далее" },
                }
            },
            [162] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 611, Text = "Позвоните" },
                    new Option { Destination = 76, Text = "В другую дверь" },
                }
            },
            [163] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 98, Text = "Далее" },
                }
            },
            [164] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 487, Text = "Драться" },
                }
            },
            [165] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 560, Text = "Осмотреть шкаф" },
                    new Option { Destination = 288, Text = "Осмотреть карты на столе" },
                    new Option { Destination = 493, Text = "Сделано и то, и другое" },
                }
            },
            [166] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 350, Text = "Далее" },
                }
            },
            [167] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 100, Text = "Далее" },
                }
            },
            [168] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 241, Text = "Далее" },
                }
            },
            [169] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 617, Text = "Принцесса уже разбужена" },
                    new Option { Destination = 612, Text = "Обратить внимание на побежденного врага" },
                    new Option { Destination = 560, Text = "Подойти к шкафу" },
                    new Option { Destination = 288, Text = "Посмотреть карты на столе" },
                    new Option { Destination = 165, Text = "Подойти к зеркалу" },
                }
            },
            [170] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 594, Text = "Правую" },
                    new Option { Destination = 599, Text = "Левую" },
                }
            },
            [171] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 301, Text = "Выскользнуть" },
                }
            },
            [172] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 347, Text = "Далее" },
                }
            },
            [173] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 379, Text = "Далее" },
                }
            },
            [174] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [175] = new Paragraph
            {
                Options = new List<Option>
               {
                    new Option { Destination = 52, Text = "Далее" },
               }
            },
            [176] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 106, Text = "Уходить" },
                    new Option { Destination = 213, Text = "Поговорить с ним еще" },
                }
            },
            [177] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [178] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 406, Text = "Далее" },
                }
            },
            [179] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 99, Text = "По ней" },
                    new Option { Destination = 377, Text = "По дороге" },
                }
            },
            [180] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 400, Text = "Далее" },
                }
            },
            [181] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 123, Text = "Напасть первым" },
                    new Option { Destination = 17, Text = "Поговорить с ними" },
                }
            },
            [182] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 25, Text = "Осмотрите буфет" },
                    new Option { Destination = 224, Text = "Осмотрите люк" },
                    new Option { Destination = 22, Text = "Осмотрите трон" },
                    new Option { Destination = 427, Text = "Через дверь в той же стене, в которой был вход" },
                    new Option { Destination = 398, Text = "Через правую дверь" },
                    new Option { Destination = 206, Text = "Через левую дверь" },
                    new Option { Destination = 350, Text = "Через среднюю дверь" },
                }
            },
            [183] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 436, Text = "Далее" },
                }
            },
            [184] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 235, Text = "Уйти сразу" },
                    new Option { Destination = 351, Text = "Как лучше попасть в Черный замок" },
                    new Option { Destination = 448, Text = "Где можно купить еду" },
                    new Option { Destination = 526, Text = "Что находится поблизости от деревни" },
                }
            },
            [185] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 329, Text = "Направо" },
                    new Option { Destination = 432, Text = "Налево" },
                    new Option { Destination = 20, Text = "Прямо" },
                }
            },
            [186] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 456, Text = "Заплатите" },
                    new Option { Destination = 229, Text = "Откажетесь и уйдете" },
                }
            },
            [187] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 47, Text = "Далее" },
                }
            },
            [188] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 184, Text = "Налево" },
                    new Option { Destination = 235, Text = "Направо" },
                }
            },
            [189] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 19, Text = "Далее" },
                }
            },
            [190] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 245, Text = "Бродячий торговец" },
                    new Option { Destination = 449, Text = "Азартный игрок" },
                    new Option { Destination = 26, Text = "Собираетесь наняться служить в его армии" },
                    new Option { Destination = 341, Text = "Сражаться" },
                }
            },
            [191] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 532, Text = "Далее" },
                }
            },
            [192] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 437, Text = "Далее" },
                }
            },
            [193] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 230, Text = "За правую" },
                    new Option { Destination = 352, Text = "За левую" },
                }
            },
            [194] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 27, Text = "Перо павлина" },
                    new Option { Destination = 270, Text = "Серебряный браслет" },
                    new Option { Destination = 533, Text = "Белую стрелу" },
                    new Option { Destination = 137, Text = "Дать денег" },
                    new Option { Destination = 522, Text = "Срразиться" },
                }
            },
            [195] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [196] = new Paragraph
            {
                Options = new List<Option>
               {
                    new Option { Destination = 560, Text = "Шкаф" },
                    new Option { Destination = 288, Text = "Стол" },
                    new Option { Destination = 165, Text = "Зеркало" },
               }
            },
            [197] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 434, Text = "Далее" },
                }
            },
            [198] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 346, Text = "Далее" },
                }
            },
            [199] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 107, Text = "Войдете" },
                    new Option { Destination = 304, Text = "По тропинке направо" },
                }
            },
            [200] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 318, Text = "Осмотреть ларь в углу" },
                    new Option { Destination = 193, Text = "Сразу уйти" },
                }
            },
            [201] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 192, Text = "Далее" },
                }
            },
            [202] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 186, Text = "Попросите проводить" },
                    new Option { Destination = 229, Text = "Поблагодарите за обед и уйдете" },
                }
            },
            [203] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 440, Text = "Свернете" },
                    new Option { Destination = 140, Text = "Пойдете прямо" },
                }
            },
            [204] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 353, Text = "Далее" },
                }
            },
            [205] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 39, Text = "Далее" },
                }
            },
            [206] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [207] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 544, Text = "Далее" },
                }
            },
            [208] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 141, Text = "Далее" },
                }
            },
            [209] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 332, Text = "Далее" },
                }
            },
            [210] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 462, Text = "Далее" },
                }
            },
            [211] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 9, Text = "Заклятием Левитации" },
                    new Option { Destination = 313, Text = "Заклятием Плавания" },
                    new Option { Destination = 423, Text = "Пойти по тропинке" },
                }
            },
            [212] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 5, Text = "Заклятием Плавания" },
                    new Option { Destination = 105, Text = "Заклятием Левитации" },
                    new Option { Destination = 177, Text = "Вернуться и попробовать пройти через кустарник" },
                }
            },
            [213] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 18, Text = "Ищите Черный замок" },
                    new Option { Destination = 124, Text = "Идете сражаться с волшебником" },
                    new Option { Destination = 23, Text = "Будить Принцессу" },
                    new Option { Destination = 438, Text = "Гуляете по лесу" },
                }
            },
            [214] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 401, Text = "Сесть на коня" },
                    new Option { Destination = 378, Text = "Двинуться дальше" },
                }
            },
            [215] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 408, Text = "Далее" },
                }
            },
            [216] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 132, Text = "Далее" },
                }
            },
            [217] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 354, Text = "Белую стрелу" },
                    new Option { Destination = 28, Text = "Бриллиант" },
                    new Option { Destination = 142, Text = "Серебряный свисток" },
                    new Option { Destination = 106, Text = "Уйти" },
                }
            },
            [218] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 463, Text = "Удачлив" },
                    new Option { Destination = 539, Text = "Нет" },
                }
            },
            [219] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 189, Text = "Далее" },
                }
            },
            [220] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 548, Text = "Войдете в конюшню" },
                    new Option { Destination = 416, Text = "Пойдете к центральному зданию" },
                }
            },
            [221] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 535, Text = "Идете в Черный замок служить" },
                    new Option { Destination = 148, Text = "Сражаться с волшебником" },
                    new Option { Destination = 464, Text = "Освобождать Принцессу" },
                    new Option { Destination = 55, Text = "Зашли случайно" },
                }
            },
            [222] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 190, Text = "Есть волшебный пояс" },
                    new Option { Destination = 236, Text = "Направиться влево, в обход замка" },
                }
            },
            [223] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 2, Text = "Отправиться по дороге" },
                    new Option { Destination = 184, Text = "Войти в деревню" },
                    new Option { Destination = 29, Text = "Пересечь дорогу и продолжать идти" },
                }
            },
            [224] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 491, Text = "Воспользоваться подъёмником" },
                    new Option { Destination = 427, Text = "Уйти через дверь в той же стене" },
                    new Option { Destination = 398, Text = "Уйти через правую дверь" },
                    new Option { Destination = 206, Text = "Уйти через левую дверь" },
                    new Option { Destination = 350, Text = "Уйти через среднюю дверь" },
                }
            },
            [225] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 144, Text = "Есть шкура оленя" },
                    new Option { Destination = 465, Text = "Идти дальше" },
                }
            },
            [226] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 409, Text = "Далее" },
                }
            },
            [227] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 63, Text = "Медный ключик" },
                    new Option { Destination = 150, Text = "Кусок металла" },
                    new Option { Destination = 473, Text = "Фигурный ключ" },
                    new Option { Destination = 416, Text = "Покинуть домик" },
                }
            },
            [228] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 332, Text = "Уйти по дороге" },
                    new Option { Destination = 561, Text = "Переплыть озеро" },
                }
            },
            [229] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 180, Text = "Направо от входа в дом" },
                    new Option { Destination = 334, Text = "Налево, проходя через сад" },
                }
            },
            [230] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 549, Text = "Пойдете направо" },
                    new Option { Destination = 466, Text = "Пройти прямо, ни за что не держась" },
                }
            },
            [231] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 406, Text = "Далее" },
                }
            },
            [232] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 319, Text = "Далее" },
                }
            },
            [233] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [234] = new Paragraph
            {
                Options = new List<Option>
            {
                    new Option { Destination = 47, Text = "По тропинке" },
                    new Option { Destination = 82, Text = "По дороге" },
            }
            },
            [235] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 102, Text = "Далее" },
                }
            },
            [236] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 331, Text = "Пойти дальше" },
                    new Option { Destination = 87, Text = "Использовать заклятие Левитации" },
                }
            },
            [237] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 108, Text = "Подойдете посмотреть" },
                    new Option { Destination = 407, Text = "Пойдете дальше" },
                }
            },
            [238] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 99, Text = "Направо" },
                    new Option { Destination = 531, Text = "Налево" },
                }
            },
            [239] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 531, Text = "Свернуть на нее" },
                    new Option { Destination = 64, Text = "Пойти дальше" },
                }
            },
            [240] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 462, Text = "Пойти прямо" },
                    new Option { Destination = 145, Text = "Пойти налево" },
                }
            },
            [241] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 485, Text = "Далее" },
                }
            },
            [242] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 123, Text = "Далее" },
                }
            },
            [243] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 39, Text = "Уйти от греха подальше" },
                    new Option { Destination = 474, Text = "Осмотреть большой сундук" },
                    new Option { Destination = 540, Text = "Осмотреть средний сундук" },
                    new Option { Destination = 380, Text = "Осмотреть маленький сундук" },
                }
            },
            [244] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 19, Text = "Далее" },
                }
            },
            [245] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 100, Text = "Есть шкура лисы" },
                    new Option { Destination = 341, Text = "Сражаться" },
                }
            },
            [246] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 550, Text = "Скажете ему, что вам надо на 2-й этаж" },
                    new Option { Destination = 39, Text = "Уйти через противоположную дверь" },
                }
            },
            [247] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 83, Text = "Попытаться убежать" },
                    new Option { Destination = 381, Text = "Если вам удалось победить" },
                }
            },
            [248] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [249] = new Paragraph
            {
                Options = new List<Option>
               {
                    new Option { Destination = 562, Text = "Пригласили в замок и вы просто ошиблись дверью" },
                    new Option { Destination = 315, Text = "Вам поручили что-то передать ему" },
               }
            },
            [250] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 33, Text = "Далее },
                }
            },
            [251] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 191, Text = "Далее" },
                }
            },
            [252] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 564, Text = "Заклятие Слабости" },
                    new Option { Destination = 541, Text = "Заклятие Огня" },
                    new Option { Destination = 494, Text = "Сразитесь с ними" },
                    new Option { Destination = 364, Text = "Если есть меч Зеленого рыцаря" },
                }
            },
            [253] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 409, Text = "Далее" },
                }
            },
            [254] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 366, Text = "Далее" },
                }
            },
            [255] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 551, Text = "Подарить ей какой-нибудь подарок" },
                    new Option { Destination = 79, Text = "1 золотой" },
                    new Option { Destination = 382, Text = "4 золотых" },
                    new Option { Destination = 495, Text = "6 золотых" },
                    new Option { Destination = 198, Text = "Извиниться за вторжение" },
                }
            },
            [256] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 316, Text = "Далее" },
                }
            },
            [257] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 565, Text = "Знаете пароль," },
                    new Option { Destination = 264, Text = "Еси лев мёртв" },
                }
            },
            [258] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 560, Text = "Шкаф" },
                    new Option { Destination = 165, Text = "Зеркало" },
                    new Option { Destination = 288, Text = "Стол" },
                }
            },
            [259] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 48, Text = "Далее" },
                }
            },
            [260] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 116, Text = "К нему" },
                    new Option { Destination = 4, Text = "К реке" },
                    new Option { Destination = 416, Text = "К зданию в центре двора" },
                }
            },
            [261] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 50, Text = "Поговорить" },
                    new Option { Destination = 179, Text = "Пойти дальше" },
                }
            },
            [262] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 117, Text = "Направо" },
                    new Option { Destination = 303, Text = "Налево" },
                }
            },
            [263] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 54, Text = "Взобраться" },
                    new Option { Destination = 19, Text = "Пойти дальше" },
                }
            },
            [264] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 191, Text = "По лестнице вверх" },
                    new Option { Destination = 30, Text = "По лестнице вниз" },
                    new Option { Destination = 53, Text = "В дверь направо" },
                    new Option { Destination = 467, Text = ""В дверь налево" },
                }
            },
            [265] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 125, Text = "Далее" },
                }
            },
            [266] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 553, Text = "Далее" },
                }
            },
            [267] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 147, Text = "Далее" },
                }
            },
            [268] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 80, Text = "Далее" },
                }
            },
            [269] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 264, Text = "Далее" },
                }
            },
            [270] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 542, Text = "В дверь справа" },
                    new Option { Destination = 252, Text = "В дверь в противоположной от входа стене" },
                }
            },
            [271] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 341, Text = "Далее" },
                }
            },
            [272] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 480, Text = "Далее" },
                }
            },
            [273] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 39, Text = "Вернетесь обратно" },
                    new Option { Destination = 483, Text = "Влететь в то окно, которое выше" },
                    new Option { Destination = 566, Text = "Влететь в то окно, которое под ним" },
                }
            },
            [274] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 542, Text = "Повинуетесь и войдете" },
                    new Option { Destination = 522, Text = "Попробуете отогнать старуху мечом" },
                }
            },
            [275] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 279, Text = "Подойти к зеркалу" },
                    new Option { Destination = 385, Text = "Подойти к столикам" },
                }
            },
            [276] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [277] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 554, Text = "Подсвечник" },
                    new Option { Destination = 78, Text = "Перо павлина" },
                    new Option { Destination = 386, Text = "Серебряный сосуд" },
                    new Option { Destination = 429, Text = "Золотое ожерелье" },
                    new Option { Destination = 572, Text = "Если нет ни одного из этих предметов" },
                }
            },
            [278] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 409, Text = "Далее" },
                }
            },
            [279] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 509, Text = "Далее" },
                }
            },
            [280] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 555, Text = "Далее" },
                }
            },
            [281] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 547, Text = "Правую" },
                    new Option { Destination = 501, Text = "Левую" },
                }
            },
            [282] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 368, Text = "Правый" },
                    new Option { Destination = 536, Text = "Левый" },
                }
            },
            [283] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 387, Text = "Зеркальце" },
                    new Option { Destination = 69, Text = "Бронзовый свисток" },
                    new Option { Destination = 233, Text = "Золотое кольцо" },
                    new Option { Destination = 567, Text = "Сражаться" },
                }
            },
            [284] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 474, Text = "Открыть большой сундук" },
                    new Option { Destination = 380, Text = "Открыть маленький сундук" },
                    new Option { Destination = 39, Text = "Уйти от греха подальше" },
                }
            },
            [285] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 321, Text = "Далее" },
                }
            },
            [286] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 129, Text = "Далее" },
                }
            },
            [287] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 497, Text = "Далее" },
                }
            },
            [288] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 165, Text = "Подойти к зеркалу" },
                    new Option { Destination = 560, Text = "Осмотреть шкаф" },
                    new Option { Destination = 493, Text = "Если вы уже сделано и то, и другое" },
                }
            },
            [289] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 487, Text = "Далее" },
                }
            },
            [290] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 118, Text = "Далее" },
                }
            },
            [291] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 568, Text = "Меч" },
                    new Option { Destination = 498, Text = "Щит" },
                    new Option { Destination = 389, Text = "Выпьете жидкость из бутылочки" },
                    new Option { Destination = 300, Text = "Закрыть дверь и снова запереть ее" },
                }
            },
            [292] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 503, Text = "Выяснить, что ей нужно" },
                    new Option { Destination = 265, Text = "Сразиться с ней" },
                }
            },
            [293] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 169, Text = "Далее" },
                }
            },
            [294] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [295] = new Paragraph
            {
                Options = new List<Option>
             {
                    new Option { Destination = 379, Text = "Далее" },
             }
            },
            [296] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 255, Text = "Поговорить с женами мага" },
                    new Option { Destination = 325, Text = "Выйти из гарема" },
                }
            },
            [297] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 416, Text = "Далее" },
                }
            },
            [298] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 257, Text = "Далее" },
                }
            },
            [299] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 567, Text = "Далее" },
                }
            },
            [300] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 337, Text = "В ту, что перед вами" },
                    new Option { Destination = 595, Text = "в ту, что слева" },
                }
            },
            [301] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 170, Text = "Если у вас есть Оберег, то 170." },
                    new Option { Destination = 606, Text = "Если есть серебряный сосуд, то 606." },
                    new Option { Destination = 594, Text = "Вы пойдете в правую дверь" },
                    new Option { Destination = 599, Text = "Или в левую" },
                }
            },
            [302] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 11, Text = "Дадите умирающему напиться " },
                    new Option { Destination = 234, Text = "Откажете и пойдете дальше " },
                }
            },
            [303] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 99, Text = "Идти по ней дальше " },
                    new Option { Destination = 117, Text = "Вернуться на развилку и пойти направо " },
                }
            },
            [304] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 88, Text = "Пойдете дальше " },
                    new Option { Destination = 12, Text = "Cвернете к озеру " },
                }
            },
            [305] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 412, Text = "Вы ступаете на траву у края поляны " },
                }
            },
            [306] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 52, Text = "Вы выбираетесь и, благословляя счастливую Судьбу, отправляетесь дальше " },
                }
            },
            [307] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 261, Text = "Потом, внимательно глядя по сторонам, идете дальше по главной улице " },
                }
            },
            [308] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 416, Text = "К центральному строению? " },
                    new Option { Destination = 220, Text = "К низкому зданию справа? " },
                    new Option { Destination = 4, Text = "К реке? " },
                }
            },
            [309] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 111, Text = "Вы можете попытаться наложить заклятие Копии" },
                    new Option { Destination = 126, Text = "Если же решили драться с ним на мечах и победили его, то 126." },
                }
            },
            [310] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 179, Text = "Недоумевая, откуда старик знает про заклятия, вы благодарите его и отправляетесь дальше " },
                }
            },
            [311] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 115, Text = "Но догадались ли вы запастись еще одним таким заклятием? Если да, то 115." },
                }
            },
            [312] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 192, Text = "После этого благополучно пересекаете озеро, но на противоположном берегу вас ждет еще один сюрприз " },
                }
            },
            [313] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 222, Text = "Заклятие действует успешно, и вы без приключений переплываете реку " },
                }
            },
            [314] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 183, Text = "Вы — а не рыцари! Они попросту отразили заклятие, и оно попало в вас же! Теперь деритесь с ними, но уменьшите на 2 СИЛУ своего УДАРА " },
                }
            },
            [315] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 409, Text = "Вы пытаетесь сопротивляться, но один из Орков, подкравшись сзади, бьет своим мечом плашмя по голове(потеряйте 2 ВЫНОСЛИВОСТИ), и вы теряете сознание… " },
                }
            },
            [316] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 513, Text = "Если удается дважды ранить Дракона, то 513." },
                }
            },
            [317] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 127, Text = "Вы сделаете это" },
                    new Option { Destination = 225, Text = "Или откажетесь и посмотрите, кто сидит в соседней клетке" },
                }
            },
            [318] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 193, Text = "Но сейчас вам надо торопиться и уходить " },
                }
            },
            [319] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 31, Text = "Скажете, что вы бродячий торговец и идете в замок торговать" },
                    new Option { Destination = 439, Text = "Что вы азартный игрок и идете развлекать волшебника" },
                    new Option { Destination = 146, Text = "Или что вы собираетесь наняться в армию чародея" },
                }
            },
            [320] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 355, Text = "Рискнете предложить что-то в виде пропуска или атакуете его? Если первое, то 355," },
                    new Option { Destination = 543, Text = "Если второе, " },
                }
            },
            [321] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 75, Text = "Попробуете наложить какое-нибудь заклятие" },
                    new Option { Destination = 247, Text = "Или будете сражаться мечом" },
                }
            },
            [322] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 277, Text = "В какую дверь вы пойдете? Прямо" },
                    new Option { Destination = 556, Text = "Или направо" },
                }
            },
            [323] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 522, Text = "Что вы сделаете? Замахнетесь на нее мечом" },
                    new Option { Destination = 137, Text = "Дадите ей денег" },
                    new Option { Destination = 194, Text = "Или предложите какой-нибудь подарок" },
                }
            },
            [324] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 589, Text = "Первый ее вопрос: жив ли еще ее тюремщик, ненавистный Барлад Дэрт? Если волшебник жив, то 589," },
                    new Option { Destination = 617, Text = "Если же он мертв, то 617." },
                }
            },
            [325] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 162, Text = "Вы пойдете направо " },
                    new Option { Destination = 76, Text = "Налево " },
                }
            },
            [326] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 573, Text = "Если вы победили его, то 573." },
                    new Option { Destination = 504, Text = "Если хотите, во время боя можете покинуть таверну и бежать" },
                }
            },
            [327] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 443, Text = "Ваши деньги исчезают в бездонном кармане торговца " },
                }
            },
            [328] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 505, Text = "Если убили обоих врагов за 8 раундов атаки, то 505." },
                    new Option { Destination = 248, Text = "Если нет, то 248." },
                }
            },
            [329] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 33, Text = "Через некоторое время вы подходите к крепкому бревенчатому дому, тропинка заканчивается у его дверей " },
                }
            },
            [330] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 532, Text = "Если же нет, то текст не будет иметь отношение к происходящему, и вы пройдете через дверь, как обычно " },
                }
            },
            [331] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 305, Text = "Обойдя замок еще с одной стороны, вы видите, что тропинка раздваивается и можно либо немного углубиться в лес" },
                    new Option { Destination = 195, Text = "Либо продолжать идти прямо" },
                }
            },
            [332] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 250, Text = "Прямо " },
                    new Option { Destination = 461, Text = "Налево " },
                }
            },
            [333] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 149, Text = "С вами говорит сам домик! Ну что, поговорите с ним" },
                    new Option { Destination = 98, Text = "Или пойдете дальше по дорожке, ведущей от дома" },
                }
            },
            [334] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 99, Text = "Вы проходите через сад, и тропинка, петляя, уходит в лес " },
                }
            },
            [335] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 46, Text = "Можете взять с собой все, что считаете нужным, и идти дальше " },
                }
            },
            [336] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 24, Text = "Прибавьте себе 1 УДАЧУ, даже если вы еще ни разу не проверяли ее " },
                }
            },
            [337] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 613, Text = "Попробуете подкупить его" },
                    new Option { Destination = 249, Text = "Поговорить с ним" },
                    new Option { Destination = 65, Text = "Или будете драться" },
                }
            },
            [338] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 151, Text = "Наконец перед вами два пути: направо" },
                    new Option { Destination = 231, Text = "И прямо" },
                }
            },
            [339] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 123, Text = "Если же вы обманули их, то ваш обман непременно откроется, и лучше уж самому начать бой " },
                }
            },
            [340] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 509, Text = "Добавьте себе 3 ВЫНОСЛИВОСТИ и отправляйтесь в путь с рассветом " },
                }
            },
            [341] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 164, Text = "Вы можете воспользоваться заклятиями Силы" },
                    new Option { Destination = 289, Text = "Слабости" },
                    new Option { Destination = 506, Text = "Огня" },
                    new Option { Destination = 487, Text = "Или драться мечом" },
                }
            },
            [342] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 181, Text = "Встает солнце, и вы видите, что конец дороги близок " },
                }
            },
            [343] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 252, Text = "Но в комнате две двери, в какую из них пойти? Прямо" },
                    new Option { Destination = 542, Text = "Или направо" },
                }
            },
            [344] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 319, Text = "Куда вы направитесь: прямо к воротам замка, который виден из-за деревьев" },
                    new Option { Destination = 232, Text = "Или же налево по лесу, в обход его" },
                }
            },
            [345] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 141, Text = "Библиотекарь говорит, что попробует найти что-нибудь подходящее, и скрывается за полками " },
                }
            },
            [346] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 153, Text = "Хотите воспользоваться каким-нибудь заклятием, чтобы выбраться отсюда" },
                    new Option { Destination = 68, Text = "Или посмотрите, что будет дальше" },
                }
            },
            [347] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [348] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 544, Text = "Вы спускаетесь по ней " },
                }
            },
            [349] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 615, Text = "Пойдете за ней" },
                    new Option { Destination = 305, Text = "Пойдете по тропинке" },
                    new Option { Destination = 210, Text = "Или вернетесь на дорогу" },
                }
            },
            [350] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 545, Text = "Никакой стражи поблизости не видно, и вы можете либо попробовать взять один из сосудов с алтаря(тогда какой — серебряный " },
                    new Option { Destination = 253, Text = "Или стеклянный — 253)," },
                    new Option { Destination = 39, Text = "Либо выйти в противоположную дверь " },
                }
            },
            [351] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 574, Text = "Примете его предложение" },
                    new Option { Destination = 235, Text = "Или откажетесь и покинете деревню по одной из дорог: на юг" },
                    new Option { Destination = 2, Text = "Или на запад" },
                }
            },
            [352] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 322, Text = "Придется подниматься до конца " },
                }
            },
            [353] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 154, Text = "Вы пойдете по тропинке направо" },
                    new Option { Destination = 237, Text = "Налево" },
                    new Option { Destination = 181, Text = "Или пойдете по дороге прямо и посмотрите, куда она вас приведет" },
                }
            },
            [354] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 327, Text = "Так что вам придется либо платить деньги" },
                    new Option { Destination = 518, Text = "Либо драться с ним" },
                    new Option { Destination = 106, Text = "Либо уходить" },
                }
            },
            [355] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 233, Text = "Золотое кольцо? " },
                    new Option { Destination = 69, Text = "Бронзовый свисток? " },
                    new Option { Destination = 168, Text = "Четки? " },
                    new Option { Destination = 543, Text = "Если у вас нет ни одного из этих предметов, вам придется драться " },
                }
            },
            [356] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 207, Text = "Теперь уходите " },
                }
            },
            [357] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 316, Text = "Заклятие действует, и вы можете прибавлять 2 к своей СИЛЕ УДАРА, когда будете сражаться " },
                }
            },
            [358] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 510, Text = "Куда вы пойдете: направо" },
                    new Option { Destination = 214, Text = "Налево" },
                    new Option { Destination = 38, Text = "Или прямо" },
                }
            },
            [359] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [360] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 2, Text = "Теперь вам уже придется либо пойти по ней" },
                    new Option { Destination = 184, Text = "Либо все-таки передумать и войти в деревню" },
                }
            },
            [361] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [362] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 241, Text = "Она не заперта " },
                }
            },
            [363] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 241, Text = "Вы же идете дальше по коридору, пока он не заканчивается невысокой дверью " },
                }
            },
            [364] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 480, Text = "Дверь справа от вас ведет в следующую комнату " },
                }
            },
            [365] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 543, Text = "Вычеркните выбранное вами заклятие, вернитесь на 543" },
                }
            },
            [366] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 416, Text = "Теперь цитадель высится слева от вас, можете обойти ее вокруг и поискать вход" },
                    new Option { Destination = 116, Text = "Вы можете сначала подойти к нему" },
                }
            },
            [367] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 44, Text = "Новая дорога уводит вас вглубь леса " },
                }
            },
            [368] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [369] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 542, Text = "Пойдете в эту дверь" },
                    new Option { Destination = 252, Text = "Или в ту, что прямо" },
                }
            },
            [370] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 538, Text = "Хотите наложить заклятие Огня" },
                    new Option { Destination = 68, Text = "Или наберетесь терпения" },
                }
            },
            [371] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 276, Text = "Придется покорно идти дальше " },
                }
            },
            [372] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 424, Text = "Вы используете заклятие Плавания и незамеченным перебираетесь на другой берег реки " },
                }
            },
            [373] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 221, Text = "Вы можете либо войти в хижину" },
                    new Option { Destination = 121, Text = "Либо вернуться на дорогу и пойти по ней дальше" },
                }
            },
            [374] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 34, Text = "Если вам удалось победить их, то 34." },
                }
            },
            [375] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 254, Text = "Вам придется использовать заклятия: либо Плавания" },
                    new Option { Destination = 508, Text = "Либо Левитации" },
                    new Option { Destination = 311, Text = "Если хотите, можете, используя заклятие Плавания, попробовать проплыть по течению под замок, надеясь попасть в подземелье незамеченным" },
                    new Option { Destination = 424, Text = "Если же у вас нет ни одного из этих заклятий или вы не хотите их использовать, идите к палаткам" },
                }
            },
            [376] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 155, Text = "Засыпая, вы опускаетесь на пол таверны " },
                }
            },
            [377] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 197, Text = "Спрячетесь и посмотрите, кто едет вам навстречу" },
                    new Option { Destination = 309, Text = "Или предпочтете поговорить со всадником и останетесь посередине дороги" },
                }
            },
            [378] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 415, Text = "Спуска с моста нет, так что вы можете либо попытаться спрыгнуть" },
                    new Option { Destination = 129, Text = "Либо идти дальше" },
                }
            },
            [379] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 544, Text = "Приходится спускаться до конца " },
                }
            },
            [380] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 474, Text = "Вам приходится просто выхватывать из текста отдельные фразы, которые можно понять: «Меня охраняют Зеленые рыцари», «…дешь ко мне, захвати с собой белую стрелу», «…плохо и страшно…», «не присылай на верную смерть своих лю…», «…авная сила в волшебном перстне с рубином…» Теперь, если вы еще этого не сделали, можете посмотреть два оставшихся сундука: большой" },
                    new Option { Destination = 540, Text = "Или средний" },
                    new Option { Destination = 39, Text = "Или же вернуться в большую залу" },
                }
            },
            [381] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 84, Text = "Вы направитесь к саркофагу со статуей" },
                    new Option { Destination = 281, Text = "К саркофагу с крестом" },
                    new Option { Destination = 496, Text = "К могиле" },
                    new Option { Destination = 547, Text = "Или уйдете через одну из двух дверей в левой стене: правую" },
                    new Option { Destination = 501, Text = "Или левую" },
                }
            },
            [382] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 575, Text = "Если вы согласны на это, то скажите им, хотите ли пойти наверх" },
                    new Option { Destination = 511, Text = "Или вниз" },
                    new Option { Destination = 325, Text = "Или же можете просто выйти из гарема и идти дальше по коридору" },
                }
            },
            [383] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 576, Text = "Клубочек сворачивает на тропинку, ведущую направо" },
                    new Option { Destination = 353, Text = "Пойдете за ним или предпочтете вернуться на 353" },
                }
            },
            [384] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 555, Text = "Но не слишком ли опрометчив ваш ответ? " },
                }
            },
            [385] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 502, Text = "Но рассмотреть рисунки как следует вам не дают " },
                }
            },
            [386] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 572, Text = "«В этом сосуде таится смерть, слышится голос,— и ты посмел предложить его нам!» " },
                }
            },
            [387] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 241, Text = "Вы поворачиваете его " },
                }
            },
            [388] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 577, Text = "Что же делать теперь? Если хотите, то можете выхватить меч и попробовать убить мага" },
                    new Option { Destination = 293, Text = "Иначе вам придется просто стоять и смотреть, что будет дальше " },
                }
            },
            [389] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 300, Text = "Теперь же закройте дверь и снова заприте ее " },
                }
            },
            [390] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 569, Text = "Когда перейдете на 569" },
                }
            },
            [391] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 502, Text = "Вы укололись настолько больно, что оказались не в силах сдержать крик " },
                }
            },
            [392] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [393] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 291, Text = "Есть ли у вас медный ключик? Если да, то достаньте его и откройте дверь" },
                    new Option { Destination = 337, Text = "Если же нет, то попробуйте открыть или ту дверь, что за вами" },
                    new Option { Destination = 595, Text = "Или же ту, что слева от вас" },
                }
            },
            [394] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 294, Text = "Что вы хотите ей подарить? Бронзовый свисток" },
                    new Option { Destination = 596, Text = "Зеркальце" },
                    new Option { Destination = 171, Text = "Или флакончик духов" },
                    new Option { Destination = 93, Text = "Если вам нечего дарить, то придется с ней сразиться" },
                }
            },
            [395] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 567, Text = "Он обратил ваше заклятие на себя, и вам придется теперь прибавлять 2 к его СИЛЕ УДАРА " },
                }
            },
            [396] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 100, Text = "Подозрительно оглядев с ног до головы, стража пропускает вас через ворота " },
                }
            },
            [397] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 130, Text = "Выйдете в переднюю дверь" },
                    new Option { Destination = 512, Text = "В правую" },
                    new Option { Destination = 315, Text = "Или попробуете с ним поговорить" },
                }
            },
            [398] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 39, Text = "Она пуста, и из нее ведет только одна дверь — вперед " },
                }
            },
            [399] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Пройдете в нее, ничего не сказав" },
                    new Option { Destination = 255, Text = "Или попытаетесь поговорить с ними" },
                }
            },
            [400] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 514, Text = "Пойдете, куда указывает стрелка" },
                    new Option { Destination = 35, Text = "Или не будете сворачивать" },
                }
            },
            [401] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 546, Text = "Если вы удачливы, то 546," },
                    new Option { Destination = 77, Text = "Если нет " },
                }
            },
            [402] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 547, Text = "Потеряйте 1 ВЫНОСЛИВОСТЬ и уходите в правую" },
                    new Option { Destination = 501, Text = "Или левую" },
                }
            },
            [403] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 199, Text = "Вы пойдете туда" },
                    new Option { Destination = 244, Text = "Или свернете налево" },
                }
            },
            [404] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 183, Text = "Теперь надо мгновенно решать, что делать: выхватить меч и драться" },
                    new Option { Destination = 36, Text = "Попробовать наложить заклятие Силы" },
                    new Option { Destination = 334, Text = "Слабости" },
                    new Option { Destination = 7, Text = "Копии" },
                    new Option { Destination = 112, Text = "Огня" },
                }
            },
            [405] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 307, Text = "Теперь можете наполнить флягу" },
                    new Option { Destination = 261, Text = "Или отправиться дальше" },
                }
            },
            [406] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 98, Text = "Пойдете прямо " },
                    new Option { Destination = 515, Text = "Пойдете к ульям " },
                }
            },
            [407] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 181, Text = "Может быть, это та дорога, которая шла от перекрестка прямо? Не исключено, но возвращаться назад ни к чему, и вы решаете, что такая дорога наверняка должна вести к какому-нибудь жилью, а, быть может, и к самому Черному замку " },
                }
            },
            [408] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 131, Text = "Выпьете содержимое" },
                    new Option { Destination = 305, Text = "Или не будете рисковать и уйдете по тропинке, ведущей от дерева в лес" },
                    new Option { Destination = 210, Text = "Или же вернетесь на дорогу" },
                }
            },
            [409] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 200, Text = "Вы попробуете использовать заклятие Иллюзии" },
                    new Option { Destination = 114, Text = "Огня" },
                    new Option { Destination = 56, Text = "Или у вас нет ни того, ни другого" },
                }
            },
            [410] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 54, Text = "Вернитесь на 54" },
                }
            },
            [411] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 578, Text = "Хотите попробовать еще и овощи" },
                    new Option { Destination = 80, Text = "Или покинете комнату через дверь в правой стене" },
                }
            },
            [412] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 316, Text = "Вы можете либо биться с ним" },
                    new Option { Destination = 37, Text = "Либо использовать заклятия — Огня" },
                    new Option { Destination = 357, Text = "Силы" },
                    new Option { Destination = 256, Text = "Или Слабости" },
                    new Option { Destination = 516, Text = "А может быть, лучше предложить миролюбивому Дракону подарок" },
                }
            },
            [413] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 132, Text = "Заклятие переносит вас на маленький остров, и вы оглядываетесь по сторонам " },
                }
            },
            [414] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 338, Text = "Решив не рисковать, вы возвращаетесь к развилке и идете направо " },
                }
            },
            [415] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 57, Text = "Если вы удачливы, то 57," },
                    new Option { Destination = 579, Text = "Если нет " },
                }
            },
            [416] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 424, Text = "Военный лагерь врага! Может быть, стоит пойти туда и попытаться разузнать что-нибудь о замке" },
                    new Option { Destination = 257, Text = "Или лучше не терять времени и попробовать войти" },
                }
            },
            [417] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 326, Text = "Прибавьте 2 к вашей СИЛЕ УДАРА и сражайтесь с Водяным" },
                }
            },
            [418] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 179, Text = "Отчаявшись что-либо узнать, вы покидаете деревню " },
                }
            },
            [419] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 104, Text = "Дадите им поесть" },
                    new Option { Destination = 374, Text = "Или откажете" },
                }
            },
            [420] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 38, Text = "Направо " },
                    new Option { Destination = 214, Text = "Прямо " },
                }
            },
            [421] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [422] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 317, Text = "Что вы скажете: что у вас есть кольцо" },
                    new Option { Destination = 133, Text = "Или что у вас его нет" },
                }
            },
            [423] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 58, Text = "Наложите заклятие Силы" },
                    new Option { Destination = 580, Text = "Слабости" },
                    new Option { Destination = 290, Text = "Огня" },
                    new Option { Destination = 40, Text = "Или будете драться с ними" },
                }
            },
            [424] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 582, Text = "Вы можете подсесть к одному из костров и поговорить с Орками" },
                    new Option { Destination = 74, Text = "Или же решить, что лучше уж попытаться проникнуть в сердце замка" },
                }
            },
            [425] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 614, Text = "Теперь скорее накладывайте еще одно заклятие Левитации или Плавания" },
                    new Option { Destination = 581, Text = "Иначе упадете в воду" },
                }
            },
            [426] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 123, Text = "«Мы и так возьмем все твое золото! Наверно, в твоем мешке есть что-то более ценное, раз ты так легко готов расстаться с ним!» Вы понимаете, что пощады ждать не приходится и решаете драться " },
                }
            },
            [427] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 583, Text = "Есть ли у вас золотой свисток? Если да, то 583," },
                    new Option { Destination = 597, Text = "Если же нет, то можете попробовать или выломать ее" },
                    new Option { Destination = 398, Text = "Или выйти в другую дверь: правую" },
                    new Option { Destination = 206, Text = "Левую" },
                    new Option { Destination = 350, Text = "Или среднюю" },
                }
            },
            [428] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 499, Text = "Вы можете воспользоваться заклятиями Огня" },
                    new Option { Destination = 85, Text = "Силы" },
                    new Option { Destination = 390, Text = "Слабости" },
                    new Option { Destination = 569, Text = "Или же просто сражаться мечом" },
                }
            },
            [429] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 301, Text = "Дверь оказывается совсем не там, где вы думали, и вы с облегчением открываете ее " },
                }
            },
            [430] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 400, Text = "Вернитесь на 400" },
                }
            },
            [431] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 59, Text = "Если вы удачливы, то 59," },
                }
            },
            [432] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 421, Text = "Вот опять налево отходит тропинка, и вы снова в раздумье: свернуть на нее" },
                    new Option { Destination = 73, Text = "Или продолжать идти прямо" },
                }
            },
            [433] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 500, Text = "Рискнете выпить ее" },
                    new Option { Destination = 416, Text = "Или предпочтете уйти" },
                }
            },
            [434] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 584, Text = "Куда же все-таки надумаете пойти: налево" },
                    new Option { Destination = 60, Text = "Или направо" },
                }
            },
            [435] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 544, Text = "Подойдя к двери, ведущей на лестницу, вы обнаруживаете, что она не заперта, и спускаетесь вниз " },
                }
            },
            [436] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 338, Text = "После этого вы решаете вернуться на развилку и пойти направо " },
                }
            },
            [437] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 531, Text = "Вы изберете путь направо" },
                    new Option { Destination = 72, Text = "Или налево" },
                }
            },
            [438] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 106, Text = "Вы понимаете, что он не поверил вам, но теперь не остается ничего другого, кроме как уйти " },
                }
            },
            [439] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 259, Text = "Скажете, что вы играете в карты" },
                    new Option { Destination = 585, Text = "Или в кости" },
                }
            },
            [440] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 348, Text = "Вы пойдете направо" },
                    new Option { Destination = 61, Text = "Или налево" },
                }
            },
            [441] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 378, Text = "К утру выходите на какую-то дорогу, но понятия не имеете, куда идти " },
                }
            },
            [442] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 119, Text = "К утру выходите на поляну, которую во всех направлениях пересекает множество следов крупных животных " },
                }
            },
            [443] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 106, Text = "Больше он ничего не знает, и вам остается только распрощаться и покинуть лавку " },
                }
            },
            [444] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 502, Text = "Внезапный шум, который с каждой секундой усиливается, заставляет насторожиться " },
                }
            },
            [445] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 102, Text = "Но внезапно дорога резко поворачивает, и деревья расступаются " },
                }
            },
            [446] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 328, Text = "Несколько раз вы дергаете за ручку двери, но безуспешно, и понимаете, что незамеченным пройти не удастся " },
                }
            },
            [447] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 62, Text = "Если оно у вас есть и вы хотите истратить его на медвежонка, то 62." },
                    new Option { Destination = 529, Text = "Если же нет или не хотите, то вы обманули надежды медведицы, и вам придется драться с ней " },
                }
            },
            [448] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 235, Text = "Сделайте необходимые покупки и покиньте деревню по одной из дорог: на юг" },
                    new Option { Destination = 2, Text = "Или на запад" },
                }
            },
            [449] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 396, Text = "Скажете, что вы играете в карты" },
                    new Option { Destination = 271, Text = "Или в кости" },
                }
            },
            [450] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 292, Text = "Вы можете либо свернуть по тропинке налево (292" },
                    new Option { Destination = 51, Text = "), либо пойти прямо" },
                    new Option { Destination = 586, Text = "Либо попробовать свернуть направо, если вам кажется, что так вы скорее достигнете цели" },
                }
            },
            [451] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 454, Text = "Вам повезло: неутомимый страж мог причинить вам немало неприятностей " },
                }
            },
            [452] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 10, Text = "Она заканчивается дверью, которую вы и открываете " },
                }
            },
            [453] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 46, Text = "А вам надо решать, куда направиться дальше, ведь вы так растерялись, что забыли спросить их об этом " },
                }
            },
            [454] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 319, Text = "Пойдете прямо к воротам" },
                    new Option { Destination = 232, Text = "Или налево по лесу в обход замка, надеясь проникнуть в него более легко" },
                }
            },
            [455] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 141, Text = "«Сейчас я принесу то, что вам нужно», — говорит он и скрывается за стеллажами " },
                }
            },
            [456] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [457] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 44, Text = "Поблагодарив медведицу, вы вылезаете из берлоги и идете дальше по дороге, уходящей с поляны в лес " },
                }
            },
            [458] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 252, Text = "В какую из них вы пойдете: в противоположную от входа" },
                    new Option { Destination = 542, Text = "Или в ту, что справа" },
                }
            },
            [459] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 399, Text = "Вернитесь на 399" },
                    new Option { Destination = 325, Text = "Теперь решайте: пройдете через комнату и пойдете дальше по коридору, ни слова не сказав и надеясь, что Зеленый рыцарь имеет право это сделать" },
                    new Option { Destination = 255, Text = "Или поговорите с женщинами" },
                }
            },
            [460] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 587, Text = "Сделаете это" },
                    new Option { Destination = 537, Text = "Или откажетесь и пойдете дальше — в этом случае придется вернуться обратно на развилку и выбрать, двинетесь ли вы направо" },
                    new Option { Destination = 348, Text = "Или налево" },
                }
            },
            [461] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 340, Text = "Хотите поискать себе убежище на ночь" },
                    new Option { Destination = 442, Text = "Или продолжите свой путь при свете луны" },
                }
            },
            [462] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 178, Text = "Если вы голодны, то можете попытаться поймать его и восстановить свои силы" },
                    new Option { Destination = 333, Text = "Если же нет, продолжайте свой путь" },
                }
            },
            [463] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 158, Text = "Хотите войти в сторожку и иметь дело с остальными" },
                    new Option { Destination = 308, Text = "Или не будете лучше искушать судьбу и направитесь к высокому зданию в центре двора" },
                }
            },
            [464] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 600, Text = "Если согласны на это, то 600," },
                    new Option { Destination = 55, Text = "Если же нет, то поблагодарите, возьмите шкуру оленя и уходите " },
                }
            },
            [465] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 140, Text = "Вы быстро проходите мимо них и выходите в узкий коридор " },
                }
            },
            [466] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 352, Text = "Стена продолжается, и вы идете по ней дальше " },
                }
            },
            [467] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 182, Text = "Вы попадаете в маленькую проходную комнатку и открываете следующую дверь " },
                }
            },
            [468] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 39, Text = "Вы же, не дожидаясь, пока стража сообразит, в чем дело, выбегаете в дверь за его столом и плотно прикрываете ее за собой " },
                }
            },
            [469] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 257, Text = "После этого придется пробиваться силой — вернитесь на 257" },
                }
            },
            [470] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 221, Text = "Войдете внутрь" },
                    new Option { Destination = 55, Text = "Или пройдете мимо и направитесь по тропинке в лес" },
                }
            },
            [471] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 80, Text = "После этого вы выходите из комнаты-огорода " },
                }
            },
            [472] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 588, Text = "Если удачливы, то 588," },
                    new Option { Destination = 391, Text = "Если же нет, то 391." },
                }
            },
            [473] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 416, Text = "Вы покидаете домик " },
                }
            },
            [474] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 159, Text = "Хотите еще раз попробовать поднять крышку" },
                    new Option { Destination = 540, Text = "Или займетесь одним из двух других сундуков — средним" },
                    new Option { Destination = 380, Text = "Или маленьким" },
                    new Option { Destination = 39, Text = "А может быть, лучше уйти" },
                }
            },
            [475] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 617, Text = "Если волшебник еще жив, то 589, если же мертв, то 617." },
                }
            },
            [476] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 347, Text = "Заклятие действует, но… " },
                }
            },
            [477] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 176, Text = "Загляните еще раз в параграф 176" },
                    new Option { Destination = 106, Text = "Теперь уходите " },
                }
            },
            [478] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 320, Text = "Пойдете по нему" },
                    new Option { Destination = 534, Text = "Или вернетесь на 534" },
                }
            },
            [479] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 590, Text = "Откроете крышку" },
                    new Option { Destination = 392, Text = "Или достаточно того, что вы как следует напугали стража и теперь можете идти дальше" },
                }
            },
            [480] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 45, Text = "Какую книгу вы попросите: о Зеленых рыцарях" },
                    new Option { Destination = 345, Text = "О самом волшебнике" },
                    new Option { Destination = 208, Text = "О Принцессе" },
                    new Option { Destination = 591, Text = "Или скажете, что вы передумали и уйдете из библиотеки" },
                }
            },
            [481] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 60, Text = "Но куда идти дальше? Продвигаться наугад по бездорожью очень тяжело (потеряйте 2 ВЫНОСЛИВОСТИ), но в конце концов вы выходите на какую-то дорогу " },
                }
            },
            [482] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 241, Text = "Вскоре он упирается в дверь, на которой вы видите герб Волшебника: Черный замок на белом поле " },
                }
            },
            [483] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 39, Text = "Если у вас есть еще одно заклятие Левитации, используйте его либо для того, чтобы вернуться обратно" },
                    new Option { Destination = 566, Text = "Либо для того, чтобы влететь в окно немного ниже" },
                }
            },
            [484] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 308, Text = "Возьмите все, что понравилось, и идите к зданию в центре двора " },
                }
            },
            [485] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 472, Text = "Справа " },
                    new Option { Destination = 275, Text = "Слева " },
                }
            },
            [486] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 570, Text = "Он не прочь поболтать с вами, но подумали ли вы, о чем вы собираетесь с ним говорить? Спросите про сундук в углу" },
                    new Option { Destination = 91, Text = "Попросите накормить вас" },
                    new Option { Destination = 295, Text = "Или расспросите о замке" },
                }
            },
            [487] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 100, Text = "Если после всего этого вы еще остались живы, то можете переступить через тела своих поверженных противников и войти в ворота " },
                }
            },
            [488] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 454, Text = "Дракону дорога память о друге, и он пропускает вас, взлетая с поляны и скрываясь за облаками " },
                }
            },
            [489] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 501, Text = "Откроете правую" },
                    new Option { Destination = 547, Text = "Или левую" },
                }
            },
            [490] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 255, Text = "Однако понимая, что вы хотели сделать подарок, но незнакомы с местными обычаями, она уходит в свою комнату, а вы можете вернуться на 255" },
                }
            },
            [491] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 571, Text = "Если вы удачливы, то 571," },
                    new Option { Destination = 593, Text = "Если же нет " },
                }
            },
            [492] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 151, Text = "Вам ничего не остается, как встать, вытряхнуть муравьев из одежды и пойти по ней " },
                }
            },
            [493] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [494] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [495] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 325, Text = "Теперь же, выслушав жен мага, вы выходите из гарема и идете по новому коридору " },
                }
            },
            [496] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 402, Text = "Если вы решили взять копье, то 402," },
                    new Option { Destination = 547, Text = "Если что-то из оставшегося, то кладите вещи в заплечный мешок и решайте, через какую дверь лучше выйти: через правую" },
                    new Option { Destination = 501, Text = "Или левую" },
                }
            },
            [497] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 572, Text = "Ты пожалеешь об этом» " },
                }
            },
            [498] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 300, Text = "Теперь закройте дверь кладовки и снова заприте ее " },
                }
            },
            [499] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 48, Text = "Если вы победили стража, можете войти в ворота " },
                }
            },
            [500] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 416, Text = "Прибавьте себе 6 ВЫНОСЛИВОСТЕЙ и уходите из домика " },
                }
            },
            [501] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 534, Text = "За ней широкая лестница с удобными деревянными перилами, которая ведет вниз " },
                }
            },
            [502] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [503] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 125, Text = "Ваш путь свободен " },
                }
            },
            [504] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 332, Text = "Однако вскоре понимаете, что Водяной вряд ли станет преследовать вас, и переходите на шаг " },
                }
            },
            [505] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 337, Text = "Какую дверь откроете: ту, что перед вами" },
                    new Option { Destination = 393, Text = "Ту, что справа от вас" },
                    new Option { Destination = 595, Text = "Или ту, что слева" },
                }
            },
            [506] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 100, Text = "Вы же, не дожидаясь, пока они надумают вернуться, входите в ворота " },
                }
            },
            [507] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 598, Text = "Пойдете по ней" },
                    new Option { Destination = 501, Text = "Или заглянете за левую дверь" },
                }
            },
            [508] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 366, Text = "Оставшись незамеченным, можете двигаться дальше " },
                }
            },
            [509] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 119, Text = "Вы решаете пересечь поляну, видя, что с другой ее стороны дорога продолжается " },
                }
            },
            [510] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 38, Text = "Куда же теперь направитесь: направо" },
                    new Option { Destination = 214, Text = "Или прямо" },
                }
            },
            [511] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 544, Text = "Через два пролета дверь, но вам не удается ее открыть, и спускаться приходится до конца " },
                }
            },
            [512] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 409, Text = "Пробуете сопротивляться, но сзади кто-то наносит сильный удар по голове (потеряйте 2 ВЫНОСЛИВОСТИ), и вы теряете сознание… " },
                }
            },
            [513] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 41, Text = "Воспользуетесь передышкой и наложите заклятие Огня" },
                    new Option { Destination = 476, Text = "Силы" },
                    new Option { Destination = 172, Text = "Слабости" },
                    new Option { Destination = 347, Text = "Копии или будете продолжать драться" },
                    new Option { Destination = 601, Text = "Можете, если есть желание, попробовать убежать" },
                }
            },
            [514] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 358, Text = "Хотите отдохнуть под одним из деревьев рядом с дорогой" },
                    new Option { Destination = 441, Text = "Или пойдете дальше" },
                }
            },
            [515] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 98, Text = "Потеряйте 8 ВЫНОСЛИВОСТЕЙ и, если еще живы, возвращайтесь на дорогу " },
                }
            },
            [516] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 134, Text = "Коготь дракона? " },
                    new Option { Destination = 42, Text = "Бриллиант? " },
                    new Option { Destination = 161, Text = "Гребень? " },
                    new Option { Destination = 488, Text = "Перо аиста? " },
                    new Option { Destination = 316, Text = "Если же вам нечего ему предложить, придется драться " },
                }
            },
            [517] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 359, Text = "Хотите узнать, питаются ли они людьми" },
                    new Option { Destination = 105, Text = "Или воспользуетесь заклятием Левитации" },
                }
            },
            [518] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 477, Text = "Если вы убили его, то 477." },
                }
            },
            [519] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 278, Text = "Что вы спросите у них? Как пройти к Принцессе" },
                    new Option { Destination = 43, Text = "Как пройти к Барладу Дэрту" },
                    new Option { Destination = 136, Text = "Или как пройти к Начальнику стражи" },
                }
            },
            [520] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 3, Text = "Теперь же отправляйтесь дальше " },
                }
            },
            [521] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 326, Text = "Теперь вам ничего не остается, как сражаться с ним мечом " },
                }
            },
            [522] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 252, Text = "Вы пойдете в дверь, которая перед вами" },
                    new Option { Destination = 542, Text = "Или в ту, что справа" },
                }
            },
            [523] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 39, Text = "Теперь вернитесь на 39" },
                }
            },
            [524] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 89, Text = "Остаются две: тропинка прямо" },
                    new Option { Destination = 616, Text = "И дорога направо" },
                }
            },
            [525] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 349, Text = "Если у вас есть банан, чтобы умилостивить Обезьяну, то 349," },
                    new Option { Destination = 215, Text = "Если же нет, то вам придется драться с ней, и победитель воспользуется тенью " },
                }
            },
            [526] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 235, Text = "Информация странна и непонятна: может быть, он принимает вас за кого-то другого? Но так или иначе все же надо решать, куда идти — на юг" },
                    new Option { Destination = 2, Text = "Или на запад" },
                }
            },
            [527] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 182, Text = "Теперь пройдите через маленькую проходную комнату, куда вы попали, и откройте следующую дверь " },
                }
            },
            [528] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 240, Text = "Потом отправляйтесь в путь, ведь и так потеряно слишком много времени " },
                }
            },
            [529] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 44, Text = "Если вы победили медведицу, то выбираетесь из берлоги и идете дальше по дороге, которая уходит в глубь леса " },
                }
            },
            [530] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 409, Text = "Вы чувствуете, как кружится голова, и теряете сознание… " },
                }
            },
            [531] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 49, Text = "Пойдете по дороге прямо" },
                    new Option { Destination = 338, Text = "Или налево" },
                }
            },
            [532] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 323, Text = "Если вы справились с охраной, то можете открыть дверь, которую они охраняли " },
                }
            },
            [533] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 137, Text = "Мжете теперь либо попробовать откупиться" },
                    new Option { Destination = 522, Text = "Либо вынуть меч и драться" },
                }
            },
            [534] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 71, Text = "Если у вас есть белая стрела, вы можете попробовать открыть дверь с соответствующим рисунком" },
                    new Option { Destination = 362, Text = "Если есть бляха с золотым орлом, сделайте то же самое" },
                    new Option { Destination = 478, Text = "Но у вас все же остается выбор: войти в правую дверь" },
                    new Option { Destination = 603, Text = "Левую" },
                    new Option { Destination = 282, Text = "Или в среднюю" },
                }
            },
            [535] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 55, Text = "Вы понимаете, что ничего не добьетесь от него, и уходите по той самой тропинке, про которую спрашивали " },
                }
            },
            [536] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 363, Text = "Если у вас есть Оберег, то 363," },
                    new Option { Destination = 479, Text = "Если есть серебряный сосуд, то 479." },
                    new Option { Destination = 283, Text = "Иначе вам придется либо подумать о том, что может служить пропуском" },
                    new Option { Destination = 567, Text = "Либо атаковать" },
                }
            },
            [537] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 140, Text = "Вы возвращаетесь к коридору, из которого пришли, и сворачиваете направо, подальше от темницы " },
                }
            },
            [538] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 370, Text = "Теперь можете или попробовать заклятие Иллюзии" },
                    new Option { Destination = 68, Text = "Или же спокойно ждите, чтобудет дальше" },
                }
            },
            [539] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 484, Text = "Вы же можете зайти в сторожку и посмотреть, нет ли там чего-нибудь для вас полезного" },
                    new Option { Destination = 308, Text = "А можете решить, что лучше сразу пойти к зданию в центре двора" },
                }
            },
            [540] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 284, Text = "Есть ли у вас фигурный ключ? Если да, то вам удастся его открыть" },
                    new Option { Destination = 474, Text = "Если нет, то лучше заняться большим" },
                    new Option { Destination = 380, Text = "Маленьким" },
                    new Option { Destination = 39, Text = "Или вернуться в залу, откуда вы пришли" },
                }
            },
            [541] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 364, Text = "Есть ли у вас меч Зеленого рыцаря? Если да, то 364," },
                    new Option { Destination = 494, Text = "Если нет " },
                }
            },
            [542] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 411, Text = "Что вас больше привлекает: мясо" },
                    new Option { Destination = 578, Text = "Или овощи" },
                    new Option { Destination = 80, Text = "Если вы не рискуете есть чьи-то припасы, выходите через дверь в правой стене" },
                }
            },
            [543] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 365, Text = "Вы можете наложить какое-нибудь заклятие" },
                    new Option { Destination = 241, Text = "Если вам удалось победить рыцаря, то идите по коридору дальше, пока не дойдете до двери " },
                }
            },
            [544] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 285, Text = "Какую из них вы откроете: правую" },
                    new Option { Destination = 489, Text = "Или левую" },
                }
            },
            [545] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 253, Text = "Что вы сделаете: возьмете стеклянный сосуд" },
                    new Option { Destination = 39, Text = "Или же выйдете из комнаты" },
                }
            },
            [546] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 181, Text = "Оглянувшись вокруг, пытаетесь понять, куда же вы попали " },
                }
            },
            [547] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 501, Text = "Если вам есть чем открыть дверь, сделайте это, иначе придется идти в другую дверь " },
                }
            },
            [548] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 297, Text = "Погладите его" },
                    new Option { Destination = 416, Text = "Или уйдете из конюшни, пока не вернулись конюхи" },
                }
            },
            [549] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 285, Text = "Какую из них вы откроете: правую" },
                    new Option { Destination = 489, Text = "Или левую" },
                }
            },
            [550] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 607, Text = "Начальник стражи выходит вместе с вами в комнату, где сидят Орки, и личным ключом отпирает дверь справа " },
                }
            },
            [551] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 92, Text = "Зеркальце? " },
                    new Option { Destination = 296, Text = "Гребень? " },
                    new Option { Destination = 490, Text = "Оберег? " },
                    new Option { Destination = 255, Text = "Если у вас нет ни одного из этих предметов, то вернитесь на 255" },
                }
            },
            [552] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 323, Text = "Предъявляете пропуск, и Гоблины беспрепятственно пропускают вас " },
                }
            },
            [553] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 388, Text = "Если вы дали Гарпии зеркальце или убили ее, то 388." },
                }
            },
            [554] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 301, Text = "Если хотите поменять одежду, то сделайте это быстро — и выходите, пока духи не передумали " },
                }
            },
            [555] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 450, Text = "С этим вы прощаетесь с Лесовичком и отправляетесь дальше " },
                }
            },
            [556] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 394, Text = "Это Женщина-вампир, и надо решать: попробуете откупиться от нее каким-нибудь подарком" },
                    new Option { Destination = 93, Text = "Или достанете меч и будете биться" },
                }
            },
            [557] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 120, Text = "Теперь можете откинуть каменные ставни и осмотреть домик" },
                    new Option { Destination = 416, Text = "Или уйти, если боитесь, что мыши поднимут тревогу" },
                }
            },
            [558] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 189, Text = "Если вы победили, то 189." },
                }
            },
            [559] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 184, Text = "Но — решайте: идти в деревню" },
                    new Option { Destination = 13, Text = "Или пойти по тропинке, которая, не выходя из леса, идет левей деревни" },
                }
            },
            [560] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 165, Text = "Теперь обратите внимание на зеркало" },
                    new Option { Destination = 288, Text = "Или на карты на столе" },
                    new Option { Destination = 493, Text = "Если вы уже осмотрели и то, и другое, то 493." },
                }
            },
            [561] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 192, Text = "Вы без приключений переплываете озеро, но на берегу ждет еще один сюрприз " },
                }
            },
            [562] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 613, Text = "Вам надо либо быстро предложить ему деньги" },
                    new Option { Destination = 65, Text = "Либо нападать первым" },
                }
            },
            [563] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 450, Text = "После этого вы можете взять у него из кармана серебряный свисток и отправиться по дороге дальше " },
                }
            },
            [564] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 480, Text = "Вы быстро проскальзываете между ними и выходите в дверь справа " },
                }
            },
            [565] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 94, Text = "Щит тьмы? " },
                    new Option { Destination = 469, Text = "Демоны и сила? " },
                    new Option { Destination = 298, Text = "Меч и верность? " },
                }
            },
            [566] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 556, Text = "Если хотите, то можете войти в правую" },
                    new Option { Destination = 277, Text = "Можете в ту, что напротив вас" },
                    new Option { Destination = 483, Text = "Однако если у вас есть еще в запасе заклятия Левитации, то вы можете их использовать, чтобы посмотреть, что находится за окном над вами" },
                    new Option { Destination = 39, Text = "Или вернуться в залу, откуда вы начали свое воздушное путешествие" },
                }
            },
            [567] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 395, Text = "Вы можете воспользоваться заклятием Силы" },
                    new Option { Destination = 299, Text = "Слабости" },
                    new Option { Destination = 96, Text = "Огня" },
                    new Option { Destination = 241, Text = "Если вы победили врага, то путь вперед свободен, и вы бежите дальше по коридору, пока дорогу не преграждает дверь " },
                }
            },
            [568] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 300, Text = "Оставьте свой меч в кладовой вместо того, который берете с собой, и заприте дверь " },
                }
            },
            [569] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 48, Text = "Если вы еще остались живы после битвы, то можете проскользнуть в ворота, оставив стражу лежать на дороге " },
                }
            },
            [570] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 604, Text = "Теперь другого выхода у вас нет: если вы не убьете всех троих, они поднимут тревогу " },
                }
            },
            [571] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 76, Text = "Вы изо всех сил налегаете на нее " },
                }
            },
            [572] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [573] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 561, Text = "Сядете в нее и попробуете переплыть озеро" },
                    new Option { Destination = 332, Text = "Или пойдете по дороге, ведущей от таверны в глубь леса" },
                }
            },
            [574] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [575] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 323, Text = "Вы отаетесь в одиночестве " },
                }
            },
            [576] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 559, Text = "Пойдете по ней направо" },
                    new Option { Destination = 181, Text = "Налево" },
                    new Option { Destination = 445, Text = "Или прямо? Если вы пойдете прямо, то тропинка вскоре выведет вас на другую дорогу" },
                }
            },
            [577] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 293, Text = "«Ты слишком подл, чтобы оценить мое великодушие и терпение, говорит маг, — и будешь наказан за это» " },
                }
            },
            [578] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 411, Text = "Хотите теперь попробовать мясо, если вы этого еще не делали" },
                    new Option { Destination = 80, Text = "Или поторопитесь уйти из комнаты через дверь в ее правой стене" },
                }
            },
            [579] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [580] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 118, Text = "Когда вы добьете своих врагов, можете забрать у них бронзовый свисток и медный ключик, которые им больше не понадобятся, и перейти реку " },
                }
            },
            [581] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [582] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 74, Text = "Вы благодарите собеседников, а что сделаете потом? Воспользуетесь информацией и отправитесь ко входу в центр цитадели" },
                    new Option { Destination = 174, Text = "Или поговорите с ними еще, надеясь узнать что-нибудь не менее ценное" },
                }
            },
            [583] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 607, Text = "Пойдете по ней" },
                    new Option { Destination = 398, Text = "Или выйдете в одну из других дверей: правую" },
                    new Option { Destination = 206, Text = "Левую" },
                    new Option { Destination = 350, Text = "Или среднюю" },
                }
            },
            [584] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 616, Text = "Куда же направитесь вы? Прямо" },
                    new Option { Destination = 89, Text = "Налево" },
                }
            },
            [585] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 428, Text = "Ответ не слишком удачен: теперь придется пробиваться силой " },
                }
            },
            [586] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 608, Text = "Поговорите с ними" },
                    new Option { Destination = 6, Text = "Или решите, что проще расчистить себе путь мечом, не вступая в лишние разговоры" },
                }
            },
            [587] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 348, Text = "Но остальные камеры-клетки пусты, и вам приходится вернуться на развилку и либо свернуть налево" },
                    new Option { Destination = 537, Text = "Либо вернуться направо" },
                }
            },
            [588] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 275, Text = "Потеряйте 6 ВЫНОСЛИВОСТЕЙ за укол волшебным шипом и обойдите кровать с другой стороны " },
                }
            },
            [589] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 266, Text = "Вы просите ее подождать и направляетесь к зеркалу " },
                }
            },
            [590] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 241, Text = "За углом коридора вас ожидает прочная дубовая дверь " },
                }
            },
            [591] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 139, Text = "Вы же проходите через библиотеку, тщательно стараясь ничего не задеть, и, открыв дверь, попадаете в следующую комнату " },
                }
            },
            [592] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 323, Text = "Вы открываете дверь за их спинами " },
                }
            },
            [593] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 409, Text = "По пути задеваете за что-то головой (потеряйте 3 ВЫНОСЛИВОСТИ) — и теряете сознание… " },
                }
            },
            [594] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 553, Text = "Вы смело подходите кдвери, быстро открываете ее и переступаете порог " },
                }
            },
            [595] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 610, Text = "Если вы сломали дверь, то 610," },
                    new Option { Destination = 393, Text = "А если решили бросить эту затею, то можете (если этого еще не делали) попробовать открыть дверь за вашей спиной" },
                    new Option { Destination = 337, Text = "Или справа от вас" },
                }
            },
            [596] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 93, Text = "Кажется, вам все же придется сразиться с ней " },
                }
            },
            [597] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 350, Text = "Вам все же придется выбрать одну из оставшихся дверей: прямо" },
                    new Option { Destination = 398, Text = "Справа" },
                    new Option { Destination = 206, Text = "Или слева" },
                }
            },
            [598] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 501, Text = "Если у вас есть в запасе заклятие Левитации, то вы еще сумеете вернуться обратно и попробовать выйти через другую дверь" },
                }
            },
            [599] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [600] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 55, Text = "Теперь можете поблагодарить старичка, взять шкуру оленя и продолжить свой путь по тропинке, которая начинается у его дома " },
                }
            },
            [601] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 232, Text = "Если остались живы, то скрывайтесь в лесу, где Дракон не может вас преследовать " },
                }
            },
            [602] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 169, Text = "Ваш противник замертво падает со стула " },
                }
            },
            [603] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [604] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 173, Text = "Теперь можете или заглянуть в сундук" },
                    new Option { Destination = 379, Text = "Или побыстрее уйти из кухни" },
                }
            },
            [605] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 169, Text = "Вам придется лишь быть довольным действием волшебного предмета " },
                }
            },
            [606] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 594, Text = "Войдете в правую" },
                    new Option { Destination = 599, Text = "Или в левую" },
                }
            },
            [607] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 323, Text = "Поднявшись, видите еще одну дверь, но на этот раз она оказывается незапертой " },
                }
            },
            [608] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 214, Text = "Куда пойдете: прямо" },
                    new Option { Destination = 38, Text = "Или направо" },
                }
            },
            [609] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [610] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 607, Text = "Хотите подняться по ней" },
                    new Option { Destination = 393, Text = "Или попробуете (если еще не пытались это сделать) открыть две другие двери: за вашей спиной" },
                    new Option { Destination = 337, Text = "Или справа от вас" },
                }
            },
            [611] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 128, Text = "Не спрашивая, кто вы, он любезно предлагает отведать вина — красного" },
                    new Option { Destination = 32, Text = "Или белого" },
                    new Option { Destination = 226, Text = "А может быть, эля" },
                    new Option { Destination = 356, Text = "Примете его предолжение или будете драться с ним" },
                }
            },
            [612] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 196, Text = "На них два перстня: с рубином" },
                    new Option { Destination = 258, Text = "И изумрудом" },
                    new Option { Destination = 169, Text = "Возьмете один из них или вернетесь на 169" },
                }
            },
            [613] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 246, Text = "Согласитесь на его условия" },
                    new Option { Destination = 65, Text = "Или всеже будете драться" },
                }
            },
            [614] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 192, Text = "Но передохнуть вам не удается " },
                }
            },
            [615] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 232, Text = "Вы пробираетесь за Обезьяной по лесу, пока она не выводит вас на какую-то дорожку и исчезает в чаще " },
                }
            },
            [616] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [617] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
        };
    }
}
