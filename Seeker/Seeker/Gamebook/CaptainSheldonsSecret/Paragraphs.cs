using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Seeker.Game;

namespace Seeker.Gamebook.CaptainSheldonsSecret
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
                    new Option { Destination = 151, Text = "Выйдете в море со штурманом" },
                    new Option { Destination = 230, Text = "Пойдете нанимать капитана" },
                    new Option { Destination = 83, Text = "Отклоните оба предложения" },
                }
            },
            [2] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 522, Text = "Попытаетесь убить ее" },
                    new Option { Destination = 156, Text = "Дадите поесть" },
                    new Option { Destination = 403, Text = "Отгоните ее рукой" },
                }
            },
            [3] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 354, Text = "Поскорее выбраться из леса" },
                    new Option { Destination = 242, Text = "Поплывите дальше" },
                }
            },
            [4] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 125, Text = "Далее" },
                }
            },
            [5] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 380, Text = "Уплывете" },
                    new Option { Destination = 409, Text = "Узнаете, кто они такие и что в сундуках" },
                }
            },
            [6] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 524, Text = "Далее" },
                }
            },
            [7] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 164, Text = "Попробуете открыть" },
                    new Option { Destination = 359, Text = "Поднимитесь наверх" },
                }
            },
            [8] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 159, Text = "Выяснить, что это было" },
                    new Option { Destination = 446, Text = "Поплывете в другую сторону" },
                }
            },
            [9] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 530, Text = "Далее" },
                }
            },
            [10] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [11] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 615, Text = "Если вам удалось сразить его" },
                    new Option { Destination = 184, Text = "Если решили покинуть поле боя" },
                }
            },
            [12] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 251, Text = "Последуете за ней" },
                    new Option { Destination = 360, Text = "Попытаетесь покинуть лес" },
                    new Option { Destination = 152, Text = "Поплывете в другую сторону" },
                }
            },
            [13] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 16, Text = "Второй" },
                    new Option { Destination = 233, Text = "Третий" },
                    new Option { Destination = 165, Text = "Четвертый" },
                    new Option { Destination = 418, Text = "Пятый" },
                    new Option { Destination = 395, Text = "Шестой" },
                    new Option { Destination = 535, Text = "Седьмой" },
                    new Option { Destination = 100, Text = "Восьмой" },
                    new Option { Destination = 84, Text = "Девятый" },
                    new Option { Destination = 352, Text = "Десятый" },
                    new Option { Destination = 282, Text = "Одиннадцатый" },
                    new Option { Destination = 456, Text = "Двенадцатый" },
                    new Option { Destination = 500, Text = "Тринадцатый" },
                }
            },
            [14] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 364, Text = "В каюту штурмана" },
                    new Option { Destination = 411, Text = "В трюм" },
                    new Option { Destination = 8, Text = "Покинете корабль" },
                }
            },
            [15] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 419, Text = "Далее" },
                }
            },
            [16] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 166, Text = "Примете предложение" },
                    new Option { Destination = 252, Text = "Скажете, что торопитесь" },
                }
            },
            [17] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 531, Text = "Предложите ему деньги" },
                    new Option { Destination = 365, Text = "Постараетесь обойтись без его помощи" },
                }
            },
            [18] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 167, Text = "Догоните корабль Шелтона и попроситесь на борт" },
                    new Option { Destination = 410, Text = "Продолжать путешествие на своей шхуне" },
                }
            },
            [19] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 52, Text = "Поплыть в левый тоннель" },
                    new Option { Destination = 253, Text = "Поплыть в средний тоннель" },
                    new Option { Destination = 361, Text = "Проплыть сквозь одну из картин" },
                }
            },
            [20] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 536, Text = "К подводному лесу" },
                    new Option { Destination = 267, Text = "К невысокой скале" },
                    new Option { Destination = 381, Text = "Проплыть между лесом и скалой" },
                }
            },
            [21] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 420, Text = "Солонина" },
                    new Option { Destination = 186, Text = "Вино в большой бочке" },
                    new Option { Destination = 434, Text = "Окорока" },
                    new Option { Destination = 15, Text = "Продолжите путешествие" },
                }
            },
            [22] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 8, Text = "Далее" },
                }
            },
            [23] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 254, Text = "Наденете его на палец" },
                    new Option { Destination = 345, Text = "Через тот, что справа" },
                    new Option { Destination = 71, Text = "Через тот, что прямо перед вами" },
                }
            },
            [24] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 523, Text = "Далее" },
                }
            },
            [25] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 382, Text = "Далее" },
                }
            },
            [26] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 255, Text = "Путешествовать вместе с Джоном" },
                    new Option { Destination = 421, Text = "Расстанетесь" },
                }
            },
            [27] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 537, Text = "Далее" },
                }
            },
            [28] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 389, Text = "Далее" },
                }
            },
            [29] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 448, Text = "Если Рыба-еж победила" },
                    new Option { Destination = 268, Text = "Если она мертва" },
                }
            },
            [30] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 549, Text = "Далее" },
                }
            },
            [31] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 188, Text = "Прямо" },
                    new Option { Destination = 435, Text = "Свернуть направо" },
                }
            },
            [32] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 326, Text = "Далее" },
                }
            },
            [33] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 406, Text = "Уменьшили силу своего противника до восьми" },
                    new Option { Destination = 362, Text = "Акула сделала то же самое" },
                }
            },
            [34] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 374, Text = "Далее" },
                }
            },
            [35] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 353, Text = "Далее" },
                }
            },
            [36] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 269, Text = "К большому подводному пастбищу" },
                    new Option { Destination = 189, Text = "В открытое море" },
                }
            },
            [37] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 540, Text = "Далее" },
                }
            },
            [38] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 374, Text = "Далее" },
                }
            },
            [39] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 294, Text = "Далее" },
                }
            },
            [40] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 202, Text = "Поговорить с Карликом" },
                    new Option { Destination = 501, Text = "Отправитесь дальше" },
                }
            },
            [41] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 182, Text = "Далее" },
                }
            },
            [42] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 468, Text = "Покормить рыбок" },
                    new Option { Destination = 516, Text = "Отправиться дальше" },
                }
            },
            [43] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 557, Text = "Число меньше ловкости" },
                    new Option { Destination = 327, Text = "Число больше или равно" },
                }
            },
            [44] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 19, Text = "Далее" },
                }
            },
            [45] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 501, Text = "Далее" },
                }
            },
            [46] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 208, Text = "Далее" },
                }
            },
            [47] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 578, Text = "Взять его с собой" },
                    new Option { Destination = 32, Text = "Плывёте дальше" },
                }
            },
            [48] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 628, Text = "Попытаться убить ее" },
                    new Option { Destination = 328, Text = "Плыть дальше" },
                }
            },
            [49] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 188, Text = "Направо" },
                    new Option { Destination = 422, Text = "Куда ведет следующий проход" },
                }
            },
            [50] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 555, Text = "Далее" },
                }
            },
            [51] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 176, Text = "Но решение за вами: нагоните корвет и подниметесь на него, оставив вашу шхуну" },
                    new Option { Destination = 4, Text = ", или будете продолжать путь" },
                }
            },
            [52] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 241, Text = "Свернете в него" },
                    new Option { Destination = 160, Text = "или поплывете дальше прямо" },
                }
            },
            [53] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 532, Text = "За деревом, которое служило жилищем Рыбе-хамелеону, виднеется невысокая скала, к которой и можете направиться" },
                    new Option { Destination = 404, Text = ". Или же решите не испытывать больше судьбу и поплывете к выходу из подводного леса, который виднеется неподалеку" },
                }
            },
            [54] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 190, Text = "Попробуете теперь правую стену" },
                    new Option { Destination = 528, Text = ", ту, что перед вами" },
                    new Option { Destination = 10, Text = ", или все же повернете обратно" },
                }
            },
            [55] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 270, Text = "Если удачливы, то 270" },
                    new Option { Destination = 538, Text = ", если же нет — 538" },
                }
            },
            [56] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 382, Text = "К нему и направляетесь — 382" },
                }
            },
            [57] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [58] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 437, Text = "Иначе она лишь грустно улыбнется и исчезнет — 437" },
                }
            },
            [59] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 15, Text = "Приходится снять латы и покинуть старую башню —15" },
                }
            },
            [60] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 140, Text = "Если в первой группе победил капитан, а оба ваши противника еще живы, то 140" },
                    new Option { Destination = 488, Text = ", если они мертвы, — 488" },
                    new Option { Destination = 334, Text = ", если же остался только один, — 334" },
                    new Option { Destination = 198, Text = "Если все враги мертвы, а капитан пал в бою, то 198" },
                }
            },
            [61] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 324, Text = "Вы покидаете остров, проведя на нем час, — 324" },
                }
            },
            [62] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 402, Text = "Вы направляетесь к развалинам, что видны неподалеку, надеясь там в безопасности отдохнуть — 402" },
                }
            },
            [63] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 217, Text = "Если вы убили их, то 217" },
                }
            },
            [64] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 489, Text = "Хотите спуститься и посмотреть, что это блестит" },
                    new Option { Destination = 126, Text = ", или поплывете дальше" },
                }
            },
            [65] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 214, Text = "Кроме того, если хотите, возьмите немного мяса с собой (добавьте 1 еду) — 214" },
                }
            },
            [66] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 148, Text = "Если хотите помочь ему, то 148" },
                    new Option { Destination = 198, Text = "Если ваш союзник перебил всех пиратов, то он исчезает" },
                    new Option { Destination = 583, Text = ", если же нет, сражаться с ними дальше придется вам" },
                }
            },
            [67] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 13, Text = "Вокруг —13" },
                    new Option { Destination = 123, Text = "Если по пути вы не встречали на этот счет никаких указаний, то выбор будет зависеть только от вашей интуиции —123" },
                }
            },
            [68] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 611, Text = "Выскакиваете в коридор, но в эту минуту корабль начинает сильно качать, не удержавшись на ногах, вы с размаху падаете на пол (потеряйте 4 ВЫНОСЛИВОСТИ) — 611" },
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
                    new Option { Destination = 399, Text = "Тогда вы обещаете спасти их позже, когда найдете Неведомое и разгадаете его тайну, а пока спешите уплыть — 399" },
                }
            },
            [71] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 616, Text = "Если с вами золотой щит, то 616" },
                    new Option { Destination = 422, Text = "Если убили его, то 422" },
                }
            },
            [72] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 256, Text = "Если удачливы, то 256" },
                    new Option { Destination = 383, Text = ", если же нет — 383" },
                }
            },
            [73] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 429, Text = "Если вам удалось отомстить, то 429" },
                }
            },
            [74] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 338, Text = "Теперь, если вы еще этого не сделали, можете надеть пояс" },
                    new Option { Destination = 584, Text = "или взять раковину" },
                    new Option { Destination = 508, Text = ". Если же нет желания продолжать эксперименты, уплывайте прочь — 508" },
                }
            },
            [75] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 145, Text = "Примете приглашение и последуете за коралловыми рыбками" },
                    new Option { Destination = 516, Text = "или поплывете дальше сами" },
                }
            },
            [76] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 569, Text = "Проведя на острове 40 минут, вы покидаете его — 569" },
                }
            },
            [77] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 241, Text = "Если выпало 1–6, то победили вы" },
                    new Option { Destination = 160, Text = "Тогда плывите прямо" },
                }
            },
            [78] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [79] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 269, Text = "Потом ведьма указывает на появившееся вдалеке большое подводное пастбище и говорит, что это и есть ваша цель, — 269" },
                }
            },
            [80] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 589, Text = "Если моряк победил, а оба ваши противника еще живы, то 589" },
                    new Option { Destination = 625, Text = ", если же в живых остался только один, то 625" },
                    new Option { Destination = 198, Text = "Если все враги мертвы, то 198" },
                }
            },
            [81] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 328, Text = "Прибавьте 1 УДАЧУ — 328" },
                }
            },
            [82] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 595, Text = "Рискнете подплыть подкрепиться" },
                    new Option { Destination = 214, Text = "или проплывете мимо" },
                }
            },
            [83] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 400, Text = "Спросите у словоохотливого хозяина дорогу и пойдете искать Джона" },
                    new Option { Destination = 524, Text = "или решите, что лучше вернуться на свою шхуну" },
                }
            },
            [84] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 157, Text = "Если у вас есть гладкий камешек, то лучше сразу попробовать показать его моряку, если нет, — думайте, о чем будете с ним разговаривать —157" },
                }
            },
            [85] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 248, Text = "На какой остров из тех, где вы еще не были, поплывете теперь? На первый" },
                    new Option { Destination = 233, Text = ", третий" },
                    new Option { Destination = 165, Text = ", четвертый" },
                    new Option { Destination = 418, Text = ", пятый" },
                    new Option { Destination = 395, Text = ", шестой" },
                    new Option { Destination = 535, Text = ", седьмой" },
                    new Option { Destination = 100, Text = ", восьмой" },
                    new Option { Destination = 84, Text = ", девятый" },
                    new Option { Destination = 352, Text = ", десятый" },
                    new Option { Destination = 282, Text = ", одиннадцатый" },
                    new Option { Destination = 456, Text = ", двенадцатый" },
                    new Option { Destination = 500, Text = "или тринадцатый" },
                }
            },
            [86] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 271, Text = "Если вы победили врага и при этом случайно остались живы, то 271" },
                }
            },
            [87] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 384, Text = "Можно направиться к невысокому подводному зданию справа от вас" },
                    new Option { Destination = 539, Text = ", поплыть в противоположную сторону" },
                    new Option { Destination = 32, Text = "или вообще не менять направления" },
                }
            },
            [88] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 192, Text = "Если вы убили его, то 192" },
                }
            },
            [89] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 447, Text = "Рыба-собака выводит вас к высокой скале и исчезает — 447" },
                }
            },
            [90] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 253, Text = "Куда вы поплывете: прямо" },
                    new Option { Destination = 19, Text = ", направо" },
                    new Option { Destination = 52, Text = "или налево" },
                }
            },
            [91] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 436, Text = "'Что же ты хочешь за это? — спрашивает Дух. — Удачи? Совета?' Так что же вы попросите: совета" },
                    new Option { Destination = 385, Text = "или удачи" },
                }
            },
            [92] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 540, Text = "Вы провели на острове ровно полчаса — 540" },
                }
            },
            [93] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 272, Text = "Если у вас есть трезубец, то 272" },
                    new Option { Destination = 608, Text = ", если же нет, то 608" },
                }
            },
            [94] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 90, Text = "Хотите направиться туда" },
                    new Option { Destination = 523, Text = "или поплывете куда глаза глядят" },
                }
            },
            [95] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 57, Text = "Хотите вернуться как можно скорее в Грейкейп я не испытывать больше судьбу" },
                    new Option { Destination = 387, Text = "или смело направитесь вперед" },
                }
            },
            [96] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 193, Text = "Дверь медленно открывается —193" },
                }
            },
            [97] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 324, Text = "После этого выбираетесь из логова Крылатого льва и покидаете остров, проведя на нем 35 минут — 324" },
                }
            },
            [98] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 422, Text = "Если хотите взять кольцо с собой, наденьте его на палец, если же нет, — можете незаметно выбросить: сейчас за вами никто не наблюдает — 422" },
                }
            },
            [99] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 457, Text = "Если вы встречали капитана Шелтона, то 457" },
                    new Option { Destination = 249, Text = ", если нет — 249" },
                }
            },
            [100] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 555, Text = "Если удается убить его, то с острова уплываете, потратив на его посещение 40 минут, — 555" },
                }
            },
            [101] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 450, Text = "Можете заговорить с ним" },
                    new Option { Destination = 286, Text = ", убить его, надеясь на интересную добычу" },
                    new Option { Destination = 501, Text = ", или просто проплыть мимо и отправиться дальше" },
                }
            },
            [102] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 26, Text = "Вы с Джоном пытаетесь отвязать одну из шлюпок, но в это время огромная волна накрывает палубу, и вы оказываетесь в воде далеко от тонущего корабля — 26" },
                }
            },
            [103] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 451, Text = "Но как могут водоросли высохнуть под водой? Хотите приблизиться и внимательнее осмотреть это место" },
                    new Option { Destination = 189, Text = "? Если нет, то можете либо просто плыть дальше" },
                    new Option { Destination = 269, Text = ", либо заинтересоваться большим подводным лугом слева от вас" },
                }
            },
            [104] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 262, Text = "Ну, а если не можете этого сделать, — вступайте в схватку сами — 262" },
                }
            },
            [105] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 502, Text = "Если у вас есть опознавательный знак и вы хотите показать его водяным, то 502" },
                    new Option { Destination = 438, Text = ". Если же у вас есть трезубец, — 438" },
                    new Option { Destination = 312, Text = ". В том случае, если у вас нет ни того, ни другого, — 312" },
                }
            },
            [106] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 361, Text = "Вернитесь на 361" },
                    new Option { Destination = 621, Text = "Через первую — 621" },
                    new Option { Destination = 412, Text = ", вторую — 412" },
                    new Option { Destination = 556, Text = ", третью — 556" },
                    new Option { Destination = 306, Text = ", четвертую — 306" },
                    new Option { Destination = 469, Text = ", пятую — 469" },
                    new Option { Destination = 39, Text = ", шестую — 39" },
                    new Option { Destination = 209, Text = ", седьмую — 209" },
                }
            },
            [107] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 618, Text = "Если вы удачливы, то 618" },
                    new Option { Destination = 203, Text = ", если нет, — 203" },
                }
            },
            [108] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 447, Text = "Ленивый кальмар не станет преследовать, а предпочтет дождаться в засаде более неповоротливую добычу — 447" },
                }
            },
            [109] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [110] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 369, Text = "Одержав победу, Рыба-молот уплывает, вильнув на прощание хвостом, — 369" },
                }
            },
            [111] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 289, Text = "Вы прекрасно умеете плавать, но чувствуете, что какая-то сила неумолимо тянет на дно, и вот уже вода смыкается над вашей головой — 289" },
                }
            },
            [112] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 3, Text = "Но сейчас вы безоружны, поэтому во время боя уменьшайте свою СИЛУ УДАРА на 3" },
                    new Option { Destination = 11, Text = "Однако, если у вас есть какое-либо другое оружие, можете его использовать (в этом случае ваша СИЛА УДАРА останется прежней) -11" },
                }
            },
            [113] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 217, Text = "Если ваши надежды оправдались, то Носорог исчезает" },
                    new Option { Destination = 63, Text = ", если же нет, то дальше придется сражаться самому" },
                }
            },
            [114] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 88, Text = "Убьете его, чтобы он не предупредил Неведомое о том, что вы его разыскиваете (или же просто для того, чтобы завладеть лодкой)" },
                    new Option { Destination = 365, Text = ", или распрощаетесь с ним и отправитесь дальше" },
                }
            },
            [115] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 90, Text = "Теперь вернитесь на 90" },
                }
            },
            [116] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 307, Text = "Карлик говорит, что может предложить по крайней мере четыре рыбки: Рыбу-меч" },
                    new Option { Destination = 452, Text = ", Рыбу-пилу" },
                    new Option { Destination = 503, Text = ", Рыбу-луну" },
                    new Option { Destination = 40, Text = "и Рыбу-ежа" },
                    new Option { Destination = 286, Text = "Можете также попробовать убить его, чтобы завладеть всем бесплатно" },
                    new Option { Destination = 501, Text = ", или отправиться дальше" },
                }
            },
            [117] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 512, Text = "Постараетесь удержаться внизу, зацепившись за атолл" },
                    new Option { Destination = 484, Text = ", или всплывете на поверхность моря" },
                }
            },
            [118] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 549, Text = "Прежде чем вы успеваете спросить, о каком пастбище идет речь и чем оно может помочь, Дух исчезает — 549" },
                }
            },
            [119] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 234, Text = "Рыба отказывается от преследования, и вскоре перед вами открывается скопление прекрасных подводных цветов — 234" },
                }
            },
            [120] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 612, Text = "А пока — плывите дальше — 612" },
                }
            },
            [121] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 398, Text = "Быстро уплываете прочь от Морского черта и радуетесь, что удалось избежать боя, — 398" },
                }
            },
            [122] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 41, Text = "Можете еще немного посидеть в кресле и отдохнуть (восстановите 6 ВЫНОСЛИВОСТЕЙ), после чего все-таки придется уходить — время дорого — 41" },
                }
            },
            [123] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 248, Text = "На какой остров вы поплывете для начала? На первый" },
                    new Option { Destination = 16, Text = ", второй" },
                    new Option { Destination = 233, Text = ", третий" },
                    new Option { Destination = 165, Text = ", четвертый" },
                    new Option { Destination = 418, Text = ", пятый" },
                    new Option { Destination = 395, Text = ", шестой" },
                    new Option { Destination = 535, Text = ", седьмой" },
                    new Option { Destination = 100, Text = ", восьмой" },
                    new Option { Destination = 84, Text = ", девятый" },
                    new Option { Destination = 352, Text = ", десятый" },
                    new Option { Destination = 282, Text = ", одиннадцатый" },
                    new Option { Destination = 456, Text = ", двенадцатый" },
                    new Option { Destination = 500, Text = "или тринадцатый" },
                }
            },
            [124] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 501, Text = "Не дослушав его до конца, вы плывете дальше — 501" },
                }
            },
            [125] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 169, Text = "Так что решать вам: предпочтете сами исследовать пещеру" },
                    new Option { Destination = 24, Text = "или отправитесь в открытое море" },
                }
            },
            [126] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 42, Text = "Попробуете с ними заговорить" },
                    new Option { Destination = 468, Text = ", покормите, бросив 1 еду" },
                    new Option { Destination = 516, Text = ", или поплывете дальше, минуя риф" },
                }
            },
            [127] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 258, Text = "Теперь же пора покидать остров, вы и так потеряли на нем полчаса — 258" },
                }
            },
            [128] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 236, Text = "Поэтому можете спокойно уплыть из залы по коридору, ведущему дальше, прихватив с собой трезубец, — 236" },
                }
            },
            [129] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 204, Text = "Теперь ваш путь лежит дальше — 204" },
                }
            },
            [130] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [131] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 472, Text = "Если вы добили его, то 472" },
                }
            },
            [132] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 559, Text = "К сожалению, жребий выпал одному из ваших соперников — 559" },
                }
            },
            [133] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 600, Text = "Если хотите ему помочь, то 600" },
                    new Option { Destination = 198, Text = "Если лев победил, он улетает, сопровождаемый вашим благодарным взглядом" },
                    new Option { Destination = 583, Text = ", если же пираты убили его, в бой придется вступить вам" },
                }
            },
            [134] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 189, Text = "Теперь, когда опасность миновала, можете отправляться дальше —189" },
                }
            },
            [135] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 336, Text = "Куда вы поплывете? Направо от скалы" },
                    new Option { Destination = 566, Text = "или налево" },
                }
            },
            [136] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 85, Text = "После этого прощаетесь с Принцем и покидаете остров, проведя на нем 45 минут, — 85" },
                }
            },
            [137] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "В случае удачи поблагодарите Рыцаря-водяного, и он уплывает —198" },
                }
            },
            [138] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 476, Text = "После чего величественно поднимается в воздух и скрывается в вышине — 476" },
                }
            },
            [139] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 338, Text = "Но что вы возьмете кроме этого? Золотой пояс" },
                    new Option { Destination = 584, Text = "? Раковину" },
                    new Option { Destination = 74, Text = "? Примерите браслет" },
                    new Option { Destination = 508, Text = "? Если же ни одна из этих вещей вас не привлекает, отправляйтесь дальше" },
                }
            },
            [140] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Если все пираты убиты, то 198" },
                    new Option { Destination = 488, Text = "Но если при этом капитан остался жив, — 488" },
                }
            },
            [141] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 56, Text = "Через несколько минут вы следуете за ним" },
                }
            },
            [142] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 511, Text = "Конечно, против пистолета вы бессильны, и не остается ничего другого, как подчиниться, — 511" },
                }
            },
            [143] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 208, Text = "В ту же минуту Лев с рычанием прыгает вперед — 208" },
                }
            },
            [144] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 590, Text = "Вдруг это отсвечивает чешуя какой-нибудь притаившейся рыбы? Захотите посмотреть, что там блестит" },
                    new Option { Destination = 343, Text = ", или предпочтете не рисковать и отправитесь дальше" },
                }
            },
            [145] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 447, Text = "Эти маленькие очаровательные существа вызывают у вас такое большое доверие, что, когда они исчезают, становится даже немного грустно, — 447" },
                }
            },
            [146] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 78, Text = "Если он уменьшил вашу ВЫНОСЛИВОСТЬ до 4, то 78" },
                    new Option { Destination = 597, Text = ". Если же вы убили его, — 597" },
                }
            },
            [147] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 594, Text = "Если хотите помочь ему победить врагов, то 594" },
                    new Option { Destination = 198, Text = "Рыцарь побеждает пиратов и вновь исчезает" },
                    new Option { Destination = 583, Text = ". Если он погиб, придется вступить в схватку с врагами самому" },
                }
            },
            [148] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Носорог исчезнет" },
                    new Option { Destination = 598, Text = "При этом если бой продолжается с обоими пиратами, то 598" },
                    new Option { Destination = 491, Text = ", а если только с одним, то 491" },
                }
            },
            [149] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 2, Text = "Поскольку в отличие от вас лев прекрасно ориентируется в темноте, то во время всего сражения придется уменьшить на 2" },
                    new Option { Destination = 324, Text = "а нем 35 минут" },
                    new Option { Destination = 61, Text = ". Или же, соблюдая меры предосторожности, доберитесь до логова льва, осмотрите его и воспользуйтесь плодами своей победы" },
                }
            },
            [150] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 341, Text = "Если есть амулет, то 341" },
                    new Option { Destination = 348, Text = ", если где-то по дороге вы случайно подобрали ржавый меч, то 348" },
                    new Option { Destination = 609, Text = ", если же нет ни того, ни другого, — 609" },
                }
            },
            [151] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 405, Text = "Так что же? Нагоните корабль капитана Шелтона" },
                    new Option { Destination = 237, Text = "или будете продолжать свой путь" },
                }
            },
            [152] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 53, Text = "Если вы убили ее, то 53" },
                }
            },
            [153] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 3, Text = "Плыть приходится так быстро, что не замечаете, как оказались на опушке огромного подводного леса, необыкновенно похожего на настоящий, — 3" },
                }
            },
            [154] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 358, Text = "Можете не выяснять, кто этобыл, и покинуть остров, проведя на нем ровно полчаса" },
                    new Option { Destination = 525, Text = ", или попробуйте догнать этого человека" },
                }
            },
            [155] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 355, Text = "Если удалось убить врага, — 355" },
                }
            },
            [156] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 243, Text = "Теперь же рыбка уплывает, а вы продолжаете свой путь — 243" },
                }
            },
            [157] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 530, Text = "Теперь можете либо покинуть остров и негостеприимного хозяина, потеряв 20 минут" },
                    new Option { Destination = 9, Text = ", либо попытаться расспросить моряка о близлежащих островах" },
                }
            },
            [158] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 413, Text = "Если удачливы, то 413" },
                    new Option { Destination = 533, Text = ", если же нет, — 533" },
                }
            },
            [159] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 406, Text = "Если смогли уменьшить ВЫНОСЛИВОСТЬ противника до 8, то 406" },
                    new Option { Destination = 362, Text = ". Если акула уменьшила вашу ВЫНОСЛИВОСТЬ до 8, то 362" },
                }
            },
            [160] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 72, Text = "Если хотите, можете быстро прошмыгнуть в один из тоннелей и попробовать скрыться" },
                    new Option { Destination = 244, Text = "или же вступайте с ними в разговор" },
                }
            },
            [161] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [162] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 257, Text = "Будете дальше ощупывать стены" },
                    new Option { Destination = 10, Text = "или повернете обратно" },
                }
            },
            [163] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 585, Text = "Хотите поплыть в каюту капитана" },
                    new Option { Destination = 411, Text = ", в трюм" },
                    new Option { Destination = 364, Text = "или в каюту штурмана" },
                }
            },
            [164] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 21, Text = "Попробуете посильнее оттолкнуться от противоположной стены и открыть ее" },
                    new Option { Destination = 359, Text = "или поднимитесь вверх по лестнице" },
                }
            },
            [165] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 258, Text = "В противном случае вы потеряли на острове полчаса и покидаете его ни с чем" },
                    new Option { Destination = 624, Text = ". Однако есть еще одна возможность: приблизиться к Филину и попытаться познакомиться с ним поближе" },
                }
            },
            [166] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 85, Text = "Если у вас есть жемчужины, то отдайте их потомку древних королей, если же нет, то поблагодарите его за гостеприимство и покиньте остров, проведя на нем 45 минут, — 85" },
                }
            },
            [167] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 366, Text = "Ночью впервые за несколько дней вы можете спокойно уснуть — 366" },
                }
            },
            [168] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 259, Text = "Хотите проплыть так, чтобы можно было разглядеть их поближе" },
                    new Option { Destination = 423, Text = ", или не будете рисковать и направитесь в открытое море" },
                }
            },
            [169] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 2, Text = "К счастью, деньги были при вас, и они не потерялись в этой суматохе, а в карманах найдется и немного еды (на 2" },
                    new Option { Destination = 90, Text = "Теперь — ко входу в пещеру" },
                }
            },
            [170] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 22, Text = "В случае победы можете спокойно осмотреть каюту капитана — 22" },
                }
            },
            [171] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 224, Text = "Если хотите помочь воину, то 224" },
                    new Option { Destination = 198, Text = "После уничтожения врагов воин исчезает" },
                    new Option { Destination = 583, Text = ". Если он гибнет, то драться придется вам" },
                }
            },
            [172] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 8, Text = "После этого покидаете затонувший корабль и плывете дальше — 8" },
                }
            },
            [173] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 15, Text = "Теперь самое время покинуть старую башню и отправиться дальше —15" },
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
                    new Option { Destination = 240, Text = "Можете последовать за ними, надеясь, что они выведут к жилью" },
                    new Option { Destination = 407, Text = ", или покинуть остров, потеряв на нем полчаса" },
                }
            },
            [176] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 357, Text = "Отдав команде шхуны приказ следовать за 'Гермесом', вы впервые за два дня ложитесь спать, ощущая себя в безопасности, — 357" },
                }
            },
            [177] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 273, Text = "Когда 'нужный час' настанет, прибавьте 88 к номеру параграфа, на котором будете находиться, — 273" },
                }
            },
            [178] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 5, Text = "Направитесь к кораблю и все же попробуете заговорить с этими людьми" },
                    new Option { Destination = 526, Text = "или поплывете в другую сторону" },
                }
            },
            [179] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 623, Text = "Можете взять одну из следующих вещей: большой алмаз" },
                    new Option { Destination = 23, Text = ", серебряное блюдо" },
                    new Option { Destination = 260, Text = ", золотой щит" },
                    new Option { Destination = 424, Text = ", ржавый меч" },
                    new Option { Destination = 71, Text = "— или, не беря ничего, поплыть в один из двух других тоннелей: в тот, что перед вами" },
                    new Option { Destination = 345, Text = ", или в тот, что справа от вас" },
                }
            },
            [180] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 217, Text = "Когда битва будет закончена, — 217" },
                }
            },
            [181] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 541, Text = "Если вам удалось победить противника, то 541" },
                }
            },
            [182] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 86, Text = "Направитесь туда" },
                    new Option { Destination = 274, Text = "или решите, что лучше проплыть стороной" },
                }
            },
            [183] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 29, Text = "Если же в вашем распоряжении Рыба-еж, то 29" },
                }
            },
            [184] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 168, Text = "Вы покидаете поле боя, еще раз обжегшись о листья растения и потеряв еще 4 ВЫНОСЛИВОСТИ, — 168" },
                }
            },
            [185] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 414, Text = "Если вам удалось их убить, то 414" },
                }
            },
            [186] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 439, Text = "Восстановите себе 5 ВЫНОСЛИВОСТЕЙ и решайте — попить еще" },
                    new Option { Destination = 15, Text = "или оставить башню и двинуться дальше" },
                }
            },
            [187] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 35, Text = "Рана болезненна, и вам не слишком-то хочется продолжать в том же духе — 35" },
                }
            },
            [188] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 504, Text = "Один из тоннелей сворачивает налево" },
                    new Option { Destination = 291, Text = ", другой идет направо" },
                }
            },
            [189] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 313, Text = "Но кто мог направить ее вам на помощь? Поговорите с ней" },
                    new Option { Destination = 613, Text = "или атакуете, обнажив меч" },
                }
            },
            [190] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 54, Text = "Остаются еще две — левая" },
                    new Option { Destination = 528, Text = "и та, что перед вами" },
                    new Option { Destination = 10, Text = ". Но, быть может, лучше повернуть обратно" },
                }
            },
            [191] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 308, Text = "Если вы удачливы, то 308" },
                    new Option { Destination = 453, Text = ", если же нет, — 453" },
                }
            },
            [192] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 2, Text = "Добыча невелика — 2" },
                    new Option { Destination = 365, Text = "Но выбор сделан, и теперь ничего не остается, кроме как отправиться дальше, — 365" },
                }
            },
            [193] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 292, Text = "Вы попадаете в сухую просторную комнату, в которой жарко и душно от множества свечей, горящих вдоль стен в изящных бронзовых канделябрах, — 292" },
                }
            },
            [194] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 217, Text = "Если рыцарь победил, то он салютует вам и исчезает" },
                    new Option { Destination = 63, Text = ", если же нет, то придется сражаться дальше самому" },
                }
            },
            [195] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 98, Text = "славы" },
                    new Option { Destination = 626, Text = ", богатства" },
                    new Option { Destination = 505, Text = ", совета" },
                }
            },
            [196] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 274, Text = "Теперь же ваш путь лежит дальше: над огромной подводной равниной" },
                    new Option { Destination = 310, Text = "или через заросли гигантских древовидных растений" },
                }
            },
            [197] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 304, Text = "Теперь можете плыть дальше: исследовать впадину" },
                    new Option { Destination = 231, Text = ", обогнуть ее с левой стороны, направившись к одинокой скале неподалеку" },
                    new Option { Destination = 15, Text = ", или обогнуть ее с правой стороны, заинтересовавшись развалинами какого-то замка в полукилометре от вас" },
                }
            },
            [198] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 293, Text = "В противном случае остается уповать только на остроту меча — 293" },
                }
            },
            [199] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 353, Text = "Не остается ничего более оригинального, кроме как направиться к развалинам, которые виднеются неподалеку, — 353" },
                }
            },
            [200] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 87, Text = "Вы берете бутылку с собой, рассчитывая при случае отправить ее по адресу, — 87" },
                }
            },
            [201] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 454, Text = "Если удачливы, то 454" },
                    new Option { Destination = 506, Text = ", если нет, — 506" },
                }
            },
            [202] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 45, Text = "Если у вас есть золотой перстень, то 45" },
                    new Option { Destination = 501, Text = ", если же нет, остается только поблагодарить это странное существо и распрощаться с ним — 501" },
                }
            },
            [203] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 441, Text = "После нескольких бесплодных попыток вы решаете оставить его в покое и отправиться дальше — 441" },
                }
            },
            [204] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 153, Text = "Если хотите попробовать уплыть от него, то 153" },
                    new Option { Destination = 473, Text = "Если вы убили его, то 473" },
                }
            },
            [205] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 560, Text = "Попробуете варево, которое он предложил" },
                    new Option { Destination = 339, Text = ", или откажетесь" },
                }
            },
            [206] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 178, Text = "Направитесь к нему" },
                    new Option { Destination = 48, Text = "или нет" },
                }
            },
            [207] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 569, Text = "Вернетесь обратно и покинете остров, проведя на нем 25 минут" },
                    new Option { Destination = 76, Text = ", или спуститесь к реке напиться и посмотреть, нет ли там чего-нибудь интересного или полезного" },
                }
            },
            [208] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 332, Text = "Попытаетесь быстро выбежать из пещеры, чтобы избежать сражения хотя бы в темноте" },
                    new Option { Destination = 149, Text = ", или примете бой" },
                }
            },
            [209] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 572, Text = "Если у вас выпало 1, 2 или 3, то 572" },
                    new Option { Destination = 141, Text = ", если же 4,5 или 6, то 141" },
                }
            },
            [210] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 1, Text = "(Кроме того, в этом случае не надо уменьшать на 1" },
                    new Option { Destination = 49, Text = "УДАЧУ на момент проверки.) Не найдя больше ничего интересного, вы покидаете библиотеку — 49" },
                }
            },
            [211] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 234, Text = "Если вы убили ее, то можете плыть дальше: либо к красивым морским цветам впереди" },
                    new Option { Destination = 48, Text = ", либо просто в открытое море" },
                }
            },
            [212] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 509, Text = "Так что придется покинуть остров, на этот раз проведя на нем 45 минут, — 509" },
                }
            },
            [213] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 13, Text = "(Для этого вычтите 363 из номера параграфа, на котором будете находиться.) Затем вы, не теряя времени, покидаете остров, проведя на нем всего 20 минут, — 13" },
                }
            },
            [214] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 189, Text = "Путь дальше свободен, и что-то подсказывает вам, что близок конец подводных приключений, — 189" },
                }
            },
            [215] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 490, Text = "Вдруг Морской конек останавливается так резко и внезапно, что вы перелетаете через его голову и видите, как он мгновенно исчезает, — 490" },
                }
            },
            [216] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 274, Text = "Теперь, когда вам понадобится позвать Дельфина, прибавьте 532 к номеру параграфа, на котором будет находиться, и ваш знакомый, благодаря вам избежавший зубов акулы, тотчас появится Теперь же ваш путь лежит дальше: над огромной подводной равниной" },
                    new Option { Destination = 310, Text = "или через заросли гигантских древовидных растений" },
                }
            },
            [217] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 331, Text = "Если у вас есть амулет, то 331" },
                    new Option { Destination = 150, Text = ". Если же нет, но вам удалось уменьшить выносливость врага до 12, то 150" },
                }
            },
            [218] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 217, Text = "Если посланец Принца победил, то он исчезает" },
                    new Option { Destination = 63, Text = ", иначе же продолжать бой придется вам" },
                }
            },
            [219] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 189, Text = "Теперь можете подкрепиться (восстановите 6 ВЫНОСЛИВОСТЕЙ), немного мяса взять с собой (в качестве 1 еды) и плыть дальше —189" },
                }
            },
            [220] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 500, Text = "Ваше время истекло, и неведомая сила заставляет направиться к центральному, тринадцатому острову — 500" },
                }
            },
            [221] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 592, Text = "Она отпугивает рыб, и они в ужасе уплывают — 592" },
                }
            },
            [222] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 136, Text = "'Я — сын Короля…' Ну что ж, этих объяснений вам вполне достаточно —136" },
                }
            },
            [223] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 269, Text = "Когда водоворот столь же внезапно успокаивается, вы видите перед собой обширное подводное пастбище, к которому и направляетесь, — 269" },
                }
            },
            [224] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 449, Text = "Если посланник Принца победил, а оба ваших противника еще живы, то 449" },
                    new Option { Destination = 607, Text = ", если же жив только один, то 607" },
                    new Option { Destination = 198, Text = "Если же произошло невероятное и все пираты уничтожены, то воин исчезнет —198" },
                }
            },
            [225] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 302, Text = "Если у вас и в самом деле есть раковина, то воспользуйтесь ею, а если нет, отправляйтесь дальше — 302" },
                }
            },
            [226] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Если враги убиты, а Морской рыцарь еще жив, то он исчезает -198" },
                }
            },
            [227] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Если после вашей победы Крылатый лев остается живым, то он улетает — 198" },
                }
            },
            [228] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 524, Text = "Делать нечего — вы покидаете его дом, решив возглавить команду самостоятельно и отправиться в море без спутника, — 524" },
                }
            },
            [229] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 284, Text = "Если вы передумали, сражайтесь — 284" },
                }
            },
            [230] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 351, Text = "Последуете за ним до конца" },
                    new Option { Destination = 6, Text = "или поблагодарите и, сказав, что передумали, уйдете" },
                }
            },
            [231] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 155, Text = "Попробуете заплыть внутрь и посмотреть, нет ли там чего-нибудь интересного" },
                    new Option { Destination = 402, Text = ", или направитесь прочь от пещеры к необычному строению, отдаленно напоминающему подводную башню" },
                    new Option { Destination = 527, Text = ", или к тому месту, где на дне видны странные песчаные холмики" },
                }
            },
            [232] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 161, Text = "Решайтесь: попытаетесь сделать это" },
                    new Option { Destination = 20, Text = "или будете продолжать путешествие под водой" },
                }
            },
            [233] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 358, Text = "Теперь можете либо покинуть остров, потеряв на нем ровно полчаса" },
                    new Option { Destination = 525, Text = ", либо попробовать догнать этого человека" },
                }
            },
            [234] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 408, Text = "Так что же? Попробуете дотянуться рукой" },
                    new Option { Destination = 576, Text = "? Мечом" },
                    new Option { Destination = 11, Text = "? Сначала сразитесь с цветами" },
                    new Option { Destination = 168, Text = "? Или решите не рисковать и поплывете дальше" },
                }
            },
            [235] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [236] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 363, Text = "Если у вас есть большой алмаз или опознавательный знак, скорее предъявите их, иначе придется вступить в бой, — 363" },
                }
            },
            [237] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 57, Text = "Понимая, что и 'Гермес' постигла участь его предшественников, можете либо повернуть назад, решив, что Неведомое, которое потопило огромный корвет, без труда справится с вашим суденышком" },
                    new Option { Destination = 263, Text = ", либо устремиться вперед, надеясь найти разгадку тайны" },
                }
            },
            [238] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Когда бой будет закончен (если, конечно, он закончится в вашу пользу), то 198" },
                }
            },
            [239] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 359, Text = "Но вот перед вами лестница: можете либо подняться по ней вверх — на второй этаж и далее к самому верху развалившегося укрепления" },
                    new Option { Destination = 7, Text = ", либо спуститься вниз, в подвал" },
                }
            },
            [240] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 407, Text = "Разочарованный, вы покидаете остров, проведя на нем 45 минут, — 407" },
                }
            },
            [241] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 528, Text = "Если удачливы, то 528" },
                    new Option { Destination = 162, Text = ", если нет, — 162" },
                }
            },
            [242] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 389, Text = "Сразитесь с ней" },
                    new Option { Destination = 12, Text = "или попробуете приручить, накормив (если, конечно, у вас есть чем)" },
                }
            },
            [243] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 371, Text = "Хотите спуститься и посмотреть, что это" },
                    new Option { Destination = 87, Text = ", или поплывете дальше, решив не искать приключений на свою голову" },
                }
            },
            [244] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 158, Text = "Если вам удалось убить противника за пять раундов атаки, то вы можете либо снова попытаться бежать" },
                    new Option { Destination = 414, Text = "Если вы убили всех Рыцарей-водяных, то 414" },
                }
            },
            [245] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 169, Text = "Так что решать вам: предпочтете сами исследовать пещеру" },
                    new Option { Destination = 24, Text = "или отправитесь в открытое море" },
                }
            },
            [246] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 390, Text = "Итак, придется либо поплатиться деньгами за свою скрытность и заплатить ему 10 золотых" },
                    new Option { Destination = 524, Text = ", либо руководить командой в одиночку" },
                }
            },
            [247] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 542, Text = "Так куда же вы попросите себя доставить: к сокровищам" },
                    new Option { Destination = 415, Text = ", за добрым советом" },
                    new Option { Destination = 114, Text = ", к тому, кто топит корабли" },
                    new Option { Destination = 365, Text = ", или откажетесь от его услуг" },
                    new Option { Destination = 88, Text = "? А может быть, лучше убить старика и самому воспользоваться лодкой" },
                }
            },
            [248] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 13, Text = "На бой у вас ушло полчаса, если для победы хватило пяти раундов атаки, или 45 минут, если больше, — 13" },
                }
            },
            [249] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 629, Text = "Теперь многое становится более понятно, но все же не до конца — 629" },
                }
            },
            [250] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 3, Text = "Решив не искать на свою голову лишних приключений, вы вскоре оказываетесь на опушке огромного подводного леса, чрезвычайно похожего на настоящий, — 3" },
                }
            },
            [251] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 89, Text = "Но может быть, наоборот, она заманивает в ловушку? Если хотите плыть за ней дальше, покинув лес, то 89" },
                    new Option { Destination = 152, Text = ". Если же нет, то откажитесь следовать за ней, и она уплывет, а вы продолжите прогулку по лесу —152" },
                }
            },
            [252] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 85, Text = "Если же нет, то придется покинуть остров, проведя яа нем ровно полчаса, — 85" },
                }
            },
            [253] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 427, Text = "Можете либо атаковать его первым и прорываться с боем" },
                    new Option { Destination = 391, Text = "Но что тогда вы ему скажете: откроете правду и попросите помочь в поисках Неведомого, которое потопило ваш корабль" },
                    new Option { Destination = 543, Text = ", или ответите, что специально пришли сюда, чтобы найти подводного Короля" },
                }
            },
            [254] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 71, Text = "Теперь самое время покинуть сокровищницу, но через какой коридор это сделать? Через тот, что прямо перед вами" },
                    new Option { Destination = 345, Text = ", или через тот, что справа от вас" },
                }
            },
            [255] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 90, Text = "Поплывете внутрь пещеры" },
                    new Option { Destination = 523, Text = "или в открытое море" },
                }
            },
            [256] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 345, Text = "Но можно попробовать либо проскользнуть в ближайший к вам выход" },
                    new Option { Destination = 241, Text = "Но в этом случае, чтобы скрыться от преследователей, придется свернуть в боковое ответвление" },
                }
            },
            [257] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 528, Text = "Тогда какую стену вы попробуете на ощупь первой: ту, что перед вами" },
                    new Option { Destination = 54, Text = ", левую" },
                    new Option { Destination = 190, Text = "или правую" },
                }
            },
            [258] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 248, Text = "На какой остров из тех, где вы еще не были, хотите отправиться теперь? На первый" },
                    new Option { Destination = 16, Text = ", второй" },
                    new Option { Destination = 233, Text = ", третий" },
                    new Option { Destination = 418, Text = ", пятый" },
                    new Option { Destination = 395, Text = ", шестой" },
                    new Option { Destination = 535, Text = ", седьмой" },
                    new Option { Destination = 100, Text = ", восьмой" },
                    new Option { Destination = 84, Text = ", девятый" },
                    new Option { Destination = 352, Text = ", десятый" },
                    new Option { Destination = 282, Text = ", одиннадцатый" },
                    new Option { Destination = 456, Text = ", двенадцатый" },
                    new Option { Destination = 500, Text = "или тринадцатый" },
                }
            },
            [259] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 428, Text = "Предложите ему еду (но тогда сколько: 1" },
                    new Option { Destination = 91, Text = ", 2" },
                    new Option { Destination = 392, Text = "); деньги: 2 золотых" },
                    new Option { Destination = 544, Text = ", 4 золотых" },
                    new Option { Destination = 30, Text = ", а может быть, что-то из вещей, которые с вами" },
                }
            },
            [260] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 71, Text = "Вы берете золотой щит и покидаете сокровищницу: можете уплыть через тоннель, начинающийся в противоположной стене зала" },
                    new Option { Destination = 345, Text = ", или через тот, который виден в правой стене" },
                }
            },
            [261] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 382, Text = "Но делать нечего, и вы направляетесь к коралловому рифу, который виднеется неподалеку, — 382" },
                }
            },
            [262] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 429, Text = "В случае победы над ним — 429" },
                }
            },
            [263] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 55, Text = "Спасти корабль уже невозможно: он начинает медленно погружаться в море — 55" },
                }
            },
            [264] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 195, Text = "Достаточно только пожелать увидеть Короля, и через несколько минут двери королевских покоев распахиваются перед вами —195" },
                }
            },
            [265] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 394, Text = "Попробуете проникнуть во фрегат и выяснить, что произошло, или поплывете дальше один? Если первое, то 394" },
                    new Option { Destination = 8, Text = ", если второе, — 8" },
                }
            },
            [266] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 241, Text = "Но как найти механизм, который приведет ее в действие? Ваш спутник считает, что надо попробовать ощупать стены — какой-нибудь выступ вполне может оказаться секретным рычагом — 241" },
                }
            },
            [267] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 610, Text = "Рискнете проплыть этим тоннелем" },
                    new Option { Destination = 536, Text = ", направитесь к подводному лесу" },
                    new Option { Destination = 381, Text = "или постараетесь проскользнуть между лесом и скалами" },
                }
            },
            [268] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 369, Text = "Если вы убили своего врага, то 369" },
                }
            },
            [269] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 107, Text = "Если хотите, можете попытаться оседлать его" },
                    new Option { Destination = 441, Text = ", если же нет — отправляйтесь дальше" },
                }
            },
            [270] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 94, Text = "Опустившись на дно, понимаете, что всплыть не удастся, и решаете смириться, отправившись на поиски Неведомого в подводном царстве, благо судьба предоставляет такую возможность — 94" },
                }
            },
            [271] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 196, Text = "Если вы уже находили статуэтку сидящего человечка, то теперь самое время достать ее, если же нет, то 196" },
                }
            },
            [272] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 269, Text = "С интересом осмотревшись, вы видите вокруг совершенно незнакомые места, а впереди большой подводный луг, к которому, немного поразмыслив, и решаете направиться — 269" },
                }
            },
            [273] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 248, Text = "На какой остров из тех, конечно, где вы еще не успели побывать, лежит ваш путь теперь? На первый" },
                    new Option { Destination = 16, Text = ", второй" },
                    new Option { Destination = 165, Text = ", четвертый" },
                    new Option { Destination = 418, Text = ", пятый" },
                    new Option { Destination = 395, Text = ", шестой" },
                    new Option { Destination = 535, Text = ", седьмой" },
                    new Option { Destination = 100, Text = ", восьмой" },
                    new Option { Destination = 84, Text = ", девятый" },
                    new Option { Destination = 352, Text = ", десятый" },
                    new Option { Destination = 282, Text = ", одиннадцатый" },
                    new Option { Destination = 456, Text = ", двенадцатый" },
                    new Option { Destination = 500, Text = "или тринадцатый" },
                }
            },
            [274] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 507, Text = "Ничего хорошего на ее морде не написано, и скорее всего лучшим выходом будет вступить в бой, не дожидаясь, пока она нападет первая" },
                    new Option { Destination = 314, Text = ". Хотя, быть может, вы попробуете покормить ее, бросив в воду 1 еду" },
                }
            },
            [275] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 120, Text = "Покормите ее, бросив в воду 1 еду" },
                    new Option { Destination = 561, Text = ", убьете" },
                    new Option { Destination = 612, Text = "или просто поплывете дальше, не обращая внимания на назойливую рыбку" },
                }
            },
            [276] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 197, Text = "Если удачливы, то 197" },
                    new Option { Destination = 442, Text = ", если нет, — 442" },
                }
            },
            [277] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 43, Text = "Начнете с правого" },
                    new Option { Destination = 474, Text = "или с левого" },
                    new Option { Destination = 508, Text = "? Конечно, у вас всегда остается возможность уплыть, не дотрагиваясь до них" },
                }
            },
            [278] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 253, Text = "Теперь вернитесь на 253" },
                }
            },
            [279] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 458, Text = "Похоже на тайник, но как его открыть? Если у вас есть Рыба-пила, то проблема решена" },
                    new Option { Destination = 396, Text = "Делать нечего — придется уплывать из леса ни с чем — 396" },
                }
            },
            [280] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 365, Text = "Когда же спрашиваете про Неведомое, то лодочник просто не понимает, о чем вы говорите, и, пожав плечами, уплывает — 365" },
                }
            },
            [281] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 402, Text = "Теперь же прибавьте себе 2 УДАЧИ и отправляйтесь дальше: к развалинам, которые видны неподалеку" },
                    new Option { Destination = 103, Text = ", или в открытое море" },
                }
            },
            [282] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 509, Text = "Но до нее не так уж близко, поэтому решайте: покинете остров, проведя на нем 10 минут" },
                    new Option { Destination = 459, Text = ", или направитесь к хижине, надеясь, что там может жить тот, кто будет в силах вам помочь" },
                }
            },
            [283] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 8, Text = "Необычное указание продолжает занимать ваши мысли все время, пока вы выбираетесь из затонувшего корабля и решаете, куда плыть дальше, — 8" },
                }
            },
            [284] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 229, Text = "Как это ни печально, союзников у вас нет, но, может быть, хотите позвать Солнечную рыбу" },
                    new Option { Destination = 198, Text = "Если вы чудом победили, то 198" },
                }
            },
            [285] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 121, Text = "Можете попробовать скрыться от злобной рыбки — 121" },
                    new Option { Destination = 315, Text = ". Если же вам удалось уменьшить ВЫНОСЛИВОСТЬ Морского черта до 2, то 315" },
                }
            },
            [286] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 501, Text = "Теперь не остается ничего другого, как отправляться дальше, — 501" },
                }
            },
            [287] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 622, Text = "В ту же секунду раздается угрожающее рычание льва — 622" },
                }
            },
            [288] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 195, Text = "В конце приемной он распахивает высокую золотую дверь, и вы переплываете порог —195" },
                }
            },
            [289] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 94, Text = "Тем более что в подводном царстве гораздо больше шансов отыскать Неведомое — 94" },
                }
            },
            [290] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 577, Text = "Хотите помочь ему" },
                    new Option { Destination = 480, Text = "или предоставите Водяному сражаться с пиратами в одиночку" },
                }
            },
            [291] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 316, Text = "Постучите" },
                    new Option { Destination = 205, Text = ", откроете ее без стука" },
                    new Option { Destination = 504, Text = "или вернетесь на развилку и изберете другой тоннель" },
                }
            },
            [292] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 122, Text = "Хотите сесть в него" },
                    new Option { Destination = 562, Text = ", устроиться на полу, чтобы отдохнуть" },
                    new Option { Destination = 41, Text = ", или решите, что здесь нет ничего интересного, а время дорого, и уйдете" },
                }
            },
            [293] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 5, Text = "Если удается отрубить все три его головы (одна голова — 5" },
                    new Option { Destination = 476, Text = "ВЫНОСЛИВОСТЕЙ), то 476" },
                }
            },
            [294] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 510, Text = "Если вам повезло, то 510" },
                    new Option { Destination = 579, Text = ", если же нет, — 579" },
                }
            },
            [295] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 526, Text = "Следуете за ней около получаса и так привыкаете видеть эту забавную рыбу впереди себя, что и не замечаете, как она исчезает, — 526" },
                }
            },
            [296] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 558, Text = "Вы не понимаете язык рыб, поэтому остается либо покормить коралловых рыбок, кинув им 1 еду" },
                    new Option { Destination = 462, Text = ", либо отправиться дальше, минуя риф" },
                    new Option { Destination = 516, Text = "или огибая его" },
                }
            },
            [297] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 123, Text = "Однако первый остров можете выбрать сами —123" },
                }
            },
            [298] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 317, Text = "Спасение может быть только в том, чтобы успеть отвязать шлюпку, — 317" },
                }
            },
            [299] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 511, Text = "Пойдете с ним" },
                    new Option { Destination = 142, Text = "или окажете сопротивление" },
                }
            },
            [300] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 463, Text = "Если удачливы, то 463" },
                    new Option { Destination = 563, Text = ", если нет, — 563" },
                }
            },
            [301] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 210, Text = "Так что же: рискнете произнести заклятие" },
                    new Option { Destination = 49, Text = "или покинете библиотеку" },
                }
            },
            [302] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 512, Text = "Постараетесь удержаться внизу, схватившись за одну из лопастей мельницы" },
                    new Option { Destination = 484, Text = ", или всплывете на поверхность" },
                }
            },
            [303] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 124, Text = "Если хотите умилостивить его (и есть чем), то можете попытаться предложить серебряное блюдо" },
                    new Option { Destination = 45, Text = ", золотой перстень" },
                    new Option { Destination = 627, Text = "или сушеного краба" },
                    new Option { Destination = 501, Text = ". Иначе оскорбленный Карлик не станет больше с вами разговаривать, и останется только отправиться дальше несолоно хлебавши" },
                }
            },
            [304] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 565, Text = "Спуститесь во впадину и подплывете к сундукам" },
                    new Option { Destination = 231, Text = "или предпочтете обогнуть ее и направиться к одинокой скале, стоящей неподалеку" },
                }
            },
            [305] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 464, Text = "Тем не менее ведьма согласна помочь и спрашивает, хотите ли вы в обмен на ее услуги расстаться с 10 золотыми? Если да, то 464" },
                    new Option { Destination = 614, Text = ", если же не хотите или у вас просто нет таких денег, то 614" },
                }
            },
            [306] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 545, Text = "Через несколько минут вы следуете за ним" },
                }
            },
            [307] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 501, Text = "Прибавьте себе 1 МАСТЕРСТВО (даже если для этого придется превысить изначальное), поблагодарите Карлика и отправляйтесь дальше" },
                    new Option { Destination = 202, Text = ". Или хотите поинтересоваться, нет ли у него в запасе еще чего-нибудь столь же полезного" },
                }
            },
            [308] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 513, Text = "Но прежде чем это сделать, вспомните, не случалось ли вам атаковать Карлика" },
                    new Option { Destination = 46, Text = ", лодочника" },
                    new Option { Destination = 143, Text = ", стража у входа в подводное королевство" },
                    new Option { Destination = 603, Text = "или Солнечную рыбу" },
                    new Option { Destination = 379, Text = "? А быть может, сразу двоих? Тогда кого: стража и Карлика" },
                    new Option { Destination = 287, Text = ", Карлика и лодочника" },
                    new Option { Destination = 342, Text = ", лодочника и Солнечную рыбу" },
                    new Option { Destination = 368, Text = "или рыбу и Водяного" },
                    new Option { Destination = 97, Text = "? Ну, а если вы умудрились не напасть ни на одно из этих существ, то 97" },
                }
            },
            [309] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [310] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 479, Text = "Хотите попробовать убить его, чтобы подкрепиться" },
                    new Option { Destination = 566, Text = ", или не тронете его и поплывете дальше" },
                }
            },
            [311] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 211, Text = "Если знаете рыбий язык, то лучше попробовать объясниться на нем, иначе придется ее убить, — 211" },
                }
            },
            [312] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 521, Text = "В том случае, если Водяной убит за пять раундов атаки, то 521" },
                    new Option { Destination = 130, Text = ", если нет, — 130" },
                }
            },
            [313] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 58, Text = "Если у вас есть золотой пояс и вы хотите вернуть его Русалке — сделайте это, если же нет, то 58" },
                }
            },
            [314] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 32, Text = "Теперь вы можете продолжить свой путь — 32" },
                }
            },
            [315] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 567, Text = "Пощадите его" },
                    new Option { Destination = 131, Text = "или предпочтете не вступать с врагами ни в какие сделки и сражаться до конца" },
                }
            },
            [316] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 205, Text = "Откроете ее" },
                    new Option { Destination = 504, Text = "или лучше вернетесь на развилку и поплывете в другой тоннель" },
                }
            },
            [317] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [318] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 304, Text = "Но уже через несколько секунд она снова чиста и прозрачна, Дух течения куда-то исчез, а перед вами в морском дне видна обширная глубокая впадина — 304" },
                }
            },
            [319] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 269, Text = "После этого решайте, куда отправиться дальше: к большому подводному лугу слева" },
                    new Option { Destination = 189, Text = "или в открытое море" },
                }
            },
            [320] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 396, Text = "Теперь же до конца леса уже недалеко, и вы плывете к его опушке — 396" },
                }
            },
            [321] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 566, Text = "Куда вы направитесь, покинув пещеру ведьмы? Налево" },
                    new Option { Destination = 336, Text = "или направо" },
                }
            },
            [322] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [323] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 132, Text = "Если на нем выпало 1 или 2, то 132" },
                    new Option { Destination = 485, Text = ", если 3 или 4, — 485" },
                }
            },
            [324] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 248, Text = "Если в вашем распоряжении осталось более 15 минут, решайте, на какой из еще не исследованных островов отправитесь: на первый" },
                    new Option { Destination = 16, Text = ", второй" },
                    new Option { Destination = 233, Text = ", третий" },
                    new Option { Destination = 165, Text = ", четвертый" },
                    new Option { Destination = 418, Text = ", пятый" },
                    new Option { Destination = 395, Text = ", шестой" },
                    new Option { Destination = 100, Text = ", восьмой" },
                    new Option { Destination = 84, Text = ", девятый" },
                    new Option { Destination = 352, Text = ", десятый" },
                    new Option { Destination = 282, Text = ", одиннадцатый" },
                    new Option { Destination = 456, Text = ", двенадцатый" },
                    new Option { Destination = 500, Text = "или на тринадцатый" },
                }
            },
            [325] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 269, Text = "Когда движение прекращается, перед вами открывается обширный подводный луг, к которому и направляетесь, — 269" },
                }
            },
            [326] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 466, Text = "Возьмете его с собой" },
                    new Option { Destination = 539, Text = "или выбросите и продолжите свой путь" },
                }
            },
            [327] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 474, Text = "Если хотите, можете попытать счастья со вторым" },
                    new Option { Destination = 508, Text = ", а если уже имели с ним дело или предпочитаете не тратить время на бесплодные попытки, отправляйтесь дальше" },
                }
            },
            [328] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 234, Text = "Куда вы направитесь: к скоплению замечательных подводных цветов слева от вас" },
                    new Option { Destination = 447, Text = ", прямо" },
                    new Option { Destination = 526, Text = "или направо" },
                }
            },
            [329] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 217, Text = "В случае удачного исхода боя — 217" },
                }
            },
            [330] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 429, Text = "Если ваши друзья победили Кондора, то они могут лететь по своим делам — 429" },
                    new Option { Destination = 570, Text = ", если же он убил их, в бой придется вступить вам — 570" },
                }
            },
            [331] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 217, Text = "Насколько честно с вашей стороны будет прибегнуть к помощи магии? Если считаете, что противник слишком подл, чтобы можно было ему верить, и хотите применить волшебство, то сделайте это, если же нет — вернитесь на 217" },
                }
            },
            [332] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 574, Text = "Если удачливы, то 574" },
                    new Option { Destination = 483, Text = ", если нет, то 483" },
                }
            },
            [333] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 32, Text = "Потеряйте 1 УДАЧУ — 32" },
                }
            },
            [334] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 488, Text = "Если капитан остается жив после того, как пират побежден, то 488" },
                    new Option { Destination = 198, Text = ", если же нет, — 198" },
                }
            },
            [335] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 308, Text = "Неожиданно Рыба-фонарик гаснет — 308" },
                }
            },
            [336] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [337] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 63, Text = "Если Водяной погиб, то бой принимать вам" },
                    new Option { Destination = 217, Text = ", если же он перебил врагов, то уплывает прочь" },
                }
            },
            [338] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 584, Text = "Теперь (если этого еще не делали) можете взять с собой раковину" },
                    new Option { Destination = 74, Text = ", примерить бронзовый браслет" },
                    new Option { Destination = 508, Text = "или оставить сундуки в покое и отправиться дальше" },
                }
            },
            [339] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 504, Text = "Если удастся убить его за четыре раунда атаки, то лучше поскорее убраться из кухни, вернувшись в коридор, по которому вы приплыли" },
                    new Option { Destination = 601, Text = ", а если нет, то 601" },
                }
            },
            [340] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Но решив, что вы могущественный волшебник, умеющий по своему желанию убивать на любом расстоянии, он в ужасе бросается в море, надеясь, добраться вплавь до своего корабля, — 198" },
                }
            },
            [341] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 609, Text = "Амулет при вас, но стоит ли прибегнуть к таящейся в нем силе? Настал ли уже тот момент, о котором говорила ведьма? Если считаете, что да, то воспользуйтесь им, если же нет, то 609" },
                    new Option { Destination = 348, Text = ". А может быть, у вас с собой ржавый меч — 348" },
                }
            },
            [342] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 622, Text = "И в ту же секунду раздается угрожающее рычание льва — 622" },
                }
            },
            [343] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 304, Text = "Если хотите, то можете исследовать ее" },
                    new Option { Destination = 15, Text = ", а если нет, то обогните ее либо справа, заинтересовавшись развалинами какого-то замка в полукилометре от вас" },
                    new Option { Destination = 231, Text = ", либо слева, направившись к одинокой скале неподалеку" },
                }
            },
            [344] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 493, Text = "Если хотите взять их с собой, то 493" },
                    new Option { Destination = 234, Text = "Если же нет, то плывите дальше: либо к красивым морским цветам впереди" },
                    new Option { Destination = 48, Text = ", либо в открытое море" },
                }
            },
            [345] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 31, Text = "Хотите плыть дальше прямо" },
                    new Option { Destination = 602, Text = ", или лучше свернуть направо" },
                    new Option { Destination = 105, Text = "или налево" },
                }
            },
            [346] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 99, Text = "Стоит его достать, как удар грома сотрясает остров, — 99" },
                }
            },
            [347] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 67, Text = "Хотите исследовать пещеру" },
                    new Option { Destination = 2, Text = "или поплывете дальше прямо" },
                    new Option { Destination = 146, Text = "или направо от скалы" },
                }
            },
            [348] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 609, Text = "Но может быть, время для него еще не настало" },
                    new Option { Destination = 341, Text = "или у вас при себе амулет" },
                }
            },
            [349] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 497, Text = "Поплывете за ней" },
                    new Option { Destination = 604, Text = "или откажетесь" },
                }
            },
            [350] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Если все враги мертвы, то моряк исчезает" },
                    new Option { Destination = 583, Text = ", если же ваш союзник погиб, то 583" },
                }
            },
            [351] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 51, Text = "Заплатите требуемые деньги" },
                    new Option { Destination = 228, Text = "или решите, что это слишком дорого, и уйдете" },
                }
            },
            [352] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 401, Text = "Отправитесь на исследование острова" },
                    new Option { Destination = 407, Text = "или покинете его, потратив всего 10 минут" },
                }
            },
            [353] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 163, Text = "Попытаетесь проникнуть в затонувший фрегат" },
                    new Option { Destination = 8, Text = "или решите не рисковать и поплывете дальше" },
                }
            },
            [354] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 232, Text = "Стоит поторопиться покинуть место, внушающее немало опасений, и вскоре перед вами вновь открытое море — 232" },
                }
            },
            [355] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 402, Text = "Покинув место битвы, вы торопитесь подальше от него: к непонятному строению, напоминающему подводную башню" },
                    new Option { Destination = 527, Text = ", или же к любопытному участку песчаного дна неподалеку, на котором видны какие-то непонятные холмики" },
                }
            },
            [356] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 99, Text = "Удар грома потрясает остров — 99" },
                }
            },
            [357] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 245, Text = "Вместе с Мейстоном судорожно пытаетесь освободить от крепления спасательную шлюпку, но в это время огромная волна смывает вас в море — 245" },
                }
            },
            [358] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 248, Text = "На какой остров из тех, где вы еще не были, направитесь теперь? На первый" },
                    new Option { Destination = 16, Text = ", второй" },
                    new Option { Destination = 165, Text = ", четвертый" },
                    new Option { Destination = 418, Text = ", пятый" },
                    new Option { Destination = 395, Text = ", шестой" },
                    new Option { Destination = 535, Text = ", седьмой" },
                    new Option { Destination = 100, Text = ", восьмой" },
                    new Option { Destination = 84, Text = ", девятый" },
                    new Option { Destination = 352, Text = ", десятый" },
                    new Option { Destination = 282, Text = ", одиннадцатый" },
                    new Option { Destination = 456, Text = ", двенадцатый" },
                    new Option { Destination = 500, Text = "или тринадцатый" },
                }
            },
            [359] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 416, Text = "Откроете ее" },
                    new Option { Destination = 372, Text = "или продолжите подъем" },
                }
            },
            [360] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 526, Text = "Вы вздыхаете с облегчением и направляетесь дальше, радуясь, что он не стал вас удерживать, — 526" },
                }
            },
            [361] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 417, Text = "Вам предстоит выбор, через какую из картин проплыть: через первую" },
                    new Option { Destination = 25, Text = ", вторую" },
                    new Option { Destination = 261, Text = ", третью" },
                    new Option { Destination = 545, Text = ", четвертую" },
                    new Option { Destination = 199, Text = ", пятую" },
                    new Option { Destination = 294, Text = ", шестую" },
                    new Option { Destination = 56, Text = "или седьмую" },
                }
            },
            [362] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 582, Text = "Акула уже атакует его, и вам надо как можно быстрее решать: кинетесь ей наперерез и, несмотря на раны, продолжите бой, чтобы защитить Дельфина" },
                    new Option { Destination = 232, Text = ", или поблагодарите судьбу за то, что остались живы, и поплывете дальше" },
                }
            },
            [363] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [364] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 283, Text = "Если это произошло, то 283" },
                    new Option { Destination = 8, Text = ", если нет, — в любую минуту можете прекратить бесплодные попытки, покинуть затонувший корабль и плыть дальше — 8" },
                }
            },
            [365] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 235, Text = "Можете направиться либо в какой-то странный провал в морском дне слева от вас" },
                    new Option { Destination = 353, Text = ", либо к непонятным развалинам прямо перед вами" },
                    new Option { Destination = 430, Text = ", либо просто в открытое море" },
                }
            },
            [366] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 26, Text = "Вы даже не успеваете предупредить своего товарища, как вас обоих смывает с палубы и уносит в море далеко от 'Гермеса', — 26" },
                }
            },
            [367] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 232, Text = "А пока ваш путь лежит дальше вперед — 232" },
                }
            },
            [368] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 622, Text = "В ту же секунду раздается угрожающее рычание льва — 622" },
                }
            },
            [369] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 104, Text = "Если можете позвать на помощь Грифа или Филина, сделайте это, если обоих, то 104" },
                    new Option { Destination = 262, Text = ". Если же нет, то придется либо прибегнуть к помощи Крылатого льва, либо вступить в схватку самому" },
                }
            },
            [370] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [371] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 431, Text = "Вскроете бутылку и прочтете послание" },
                    new Option { Destination = 200, Text = "или возьмете ее с собой" },
                    new Option { Destination = 87, Text = "? Можете также оставить свою находку там, где она лежала, и плыть дальше" },
                }
            },
            [372] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 27, Text = "Хотите посмотреть в подзорную трубу (либо наверх через толщу воды — 27" },
                    new Option { Destination = 546, Text = ", либо вниз, на панораму, открывающуюся перед вами, — 546" },
                    new Option { Destination = 537, Text = ") или покинете башню и, проплыв над разрушенной крепостной стеной, двинетесь дальше" },
                }
            },
            [373] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 263, Text = "Направите шхуну к нему и постараетесь как можно быстрее добраться до места катастрофы" },
                    new Option { Destination = 57, Text = "или повернете обратно в порт" },
                }
            },
            [374] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 31, Text = "Можете поплыть направо" },
                    new Option { Destination = 105, Text = ", прямо" },
                    new Option { Destination = 443, Text = "или налево" },
                }
            },
            [375] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 295, Text = "Последуете за ней" },
                    new Option { Destination = 204, Text = "или не поверите хитрой рыбке и поплывете дальше: прямо от того места, где вы оказались" },
                    new Option { Destination = 514, Text = ", или налево" },
                }
            },
            [376] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 68, Text = "Примете его предложение и отправите свою шхуну обратно в порт" },
                    new Option { Destination = 432, Text = "или откажетесь и, вернувшись на 'Святую Елену', продолжите плавание самостоятельно" },
                }
            },
            [377] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 168, Text = "Теперь отправляйтесь дальше —168" },
                }
            },
            [378] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 163, Text = "Вы согласны с ним, но кто отправится внутрь? Если вы, то 163" },
                    new Option { Destination = 265, Text = ", если Джон, то 265" },
                }
            },
            [379] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 622, Text = "И в ту же секунду раздается угрожающее рычание льва — 622" },
                }
            },
            [380] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 526, Text = "Может быть, и правильно, что вы решили плыть дальше, — 526" },
                }
            },
            [381] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 547, Text = "Попробуете проплыть через него" },
                    new Option { Destination = 86, Text = "или свернете к небольшой уютной впадине, заросшей темно-зелеными водорослями" },
                }
            },
            [382] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 296, Text = "Попробуете с ними заговорить" },
                    new Option { Destination = 558, Text = ", покормите их, бросив 1 еду" },
                    new Option { Destination = 462, Text = ", или, поплывете дальше? Но куда: минуя риф" },
                    new Option { Destination = 516, Text = "или огибая его" },
                }
            },
            [383] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 244, Text = "Увы, вам не повезло — прекрасная идея побыстрее скрыться пришла в голову слишком поздно: один из Водяных уже перед вами — 244" },
                }
            },
            [384] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 568, Text = "Если у вас есть бутылка с посланием, которую надо отправить, то 568" },
                    new Option { Destination = 32, Text = ". Если же нет, то на почте делать нечего и придется отправляться дальше — 32" },
                }
            },
            [385] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 549, Text = "Помимо этого, можете восстановить свою УДАЧУ до начального уровня и плыть дальше — 549" },
                }
            },
            [386] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 106, Text = "Можете плыть вместе через одну и ту же картину" },
                    new Option { Destination = 444, Text = ", либо разделиться" },
                }
            },
            [387] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 111, Text = "Если удачливы, то 111" },
                    new Option { Destination = 298, Text = ", если нет, то 298" },
                }
            },
            [388] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 369, Text = "В случае победы над акулой — 369" },
                }
            },
            [389] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 360, Text = "Если вы убили ее, то можете либо поторопиться и покинуть негостеприимный лес" },
                    new Option { Destination = 152, Text = ", либо направиться дальше: к группе деревьев неподалеку" },
                    new Option { Destination = 550, Text = "или к подводному лугу справа от вас" },
                }
            },
            [390] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 18, Text = "На следующее утро, еще до восхода солнца, Джон уже ждет в порту, и ваша шхуна 'Святая Елена' выходит в море —18" },
                }
            },
            [391] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 34, Text = "После этого он отплывает в сторону — можете отправляться дальше — 34" },
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
                    new Option { Destination = 108, Text = "Если вы удачливы, то 108" },
                    new Option { Destination = 445, Text = ", если нет, то 445" },
                }
            },
            [394] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 8, Text = "Поторопитесь покинуть корабль и поплывете дальше один" },
                    new Option { Destination = 585, Text = "или все же осмотрите отсеки: каюту капитана" },
                    new Option { Destination = 364, Text = ", каюту штурмана" },
                    new Option { Destination = 411, Text = "или трюм" },
                }
            },
            [395] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 299, Text = "Подождете неизвестно чего" },
                    new Option { Destination = 465, Text = "или покинете остров, проведя на нем 10 минут" },
                }
            },
            [396] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 86, Text = "Но куда направиться теперь? Прямо перед вами на морском дне видна небольшая уютная впадина, поросшая темно-зелеными водорослями" },
                    new Option { Destination = 575, Text = ", а справа вдалеке — невысокая скала" },
                }
            },
            [397] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 36, Text = "Если он мертв, то 36" },
                }
            },
            [398] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 119, Text = "Если хотите, можете попытаться бежать" },
                    new Option { Destination = 551, Text = "Если удалось ее победить, то 551" },
                }
            },
            [399] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 526, Text = "Если удачливы, то 526" },
                    new Option { Destination = 109, Text = ", если нет, то 109" },
                }
            },
            [400] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 529, Text = "Расскажете ему о цели путешествия" },
                    new Option { Destination = 246, Text = "или постараетесь прельстить деньгами, сохранив все в тайне" },
                }
            },
            [401] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 175, Text = "Обогнете озеро и подойдете к оленям" },
                    new Option { Destination = 407, Text = "или покинете остров, потеряв на нем 25 минут" },
                }
            },
            [402] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 239, Text = "Если хотите, можете вплыть внутрь и исследовать остатки некогда грозной и могучей цитадели" },
                    new Option { Destination = 15, Text = ". Или направитесь прочь, решив, что посещение замка принесет больше опасностей, чем выгод" },
                }
            },
            [403] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 243, Text = "Вы отгоняете Солнечную рыбу рукой, и она послушно уплывает прочь: можете продолжать путешествие — 243" },
                }
            },
            [404] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 69, Text = "К тому же над лугом медленно плывет тихая приятная музыка, но откуда она может доноситься? Приблизитесь, чтобы посмотреть на цветы и послушать музыку" },
                    new Option { Destination = 396, Text = ", или предпочтете не терять времени и скорее выбраться из кораллового леса" },
                }
            },
            [405] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 174, Text = "Вы можете остаться с Шелтоном и отправить ваше судно в Грейкейп" },
                    new Option { Destination = 373, Text = "или же поблагодарить капитана и, вернувшись на 'Святую Елену', продолжить путешествие" },
                }
            },
            [406] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 582, Text = "Акула уже атакует его, и вам надо как можно быстрее решать: кинуться ей наперерез и продолжить бой, чтобы защитить Дельфина" },
                    new Option { Destination = 232, Text = ", или воспользоваться тем, что она оставила вас, и поплыть дальше" },
                }
            },
            [407] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 248, Text = "На какой остров из тех, где вы еще не были, хотите направиться теперь? Первый" },
                    new Option { Destination = 16, Text = ", второй" },
                    new Option { Destination = 233, Text = ", третий" },
                    new Option { Destination = 165, Text = ", четвертый" },
                    new Option { Destination = 418, Text = ", пятый" },
                    new Option { Destination = 395, Text = ", шестой" },
                    new Option { Destination = 535, Text = ", седьмой" },
                    new Option { Destination = 100, Text = ", восьмой" },
                    new Option { Destination = 84, Text = ", девятый" },
                    new Option { Destination = 282, Text = ", одиннадцатый" },
                    new Option { Destination = 456, Text = ", двенадцатый" },
                    new Option { Destination = 500, Text = "или тринадцатый" },
                }
            },
            [408] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 234, Text = "Потеряйте 2 ВЫНОСЛИВОСТИ и вернитесь на 234" },
                }
            },
            [409] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 70, Text = "Если он убит, то можете приблизиться к кораблю" },
                    new Option { Destination = 380, Text = ". Но, быть может, лучше уплыть от греха подальше" },
                }
            },
            [410] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 370, Text = "Может быть, стоит, пока не поздно, вернуться в порт" },
                    new Option { Destination = 102, Text = "? Или все же решите плыть дальше навстречу Неведомому" },
                }
            },
            [411] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 172, Text = "Постараетесь побыстрее уйти из трюма, чтобы не погибнуть от нападения неизвестного существа, которое наверняка прекрасно видит в темноте" },
                    new Option { Destination = 619, Text = ", или смело пойдете дальше" },
                }
            },
            [412] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 25, Text = "Вы торопитесь следом" },
                }
            },
            [413] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 241, Text = "Бросаетесь снова в тот тоннель, из которого появились, и на этот раз свернете в боковое ответвление, чтобы вас не нашли" },
                    new Option { Destination = 345, Text = ", или выскользнете в ближайший от вас выход из зала" },
                }
            },
            [414] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 179, Text = "Теперь можете спокойно оглядеться" },
                    new Option { Destination = 71, Text = ". Но не лучше ли поторопиться уплыть, пока на шум драки не подоспело подкрепление? Тогда какой из выходов вы выберете: тот, что прямо перед вами" },
                    new Option { Destination = 345, Text = ", или тот, что рядом" },
                }
            },
            [415] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 247, Text = "Вы платите старику требуемые деньги (если же денег нет, то придется вернуться на 247" },
                    new Option { Destination = 3, Text = "В конце концов, он уплывает прочь, высадив вас на краю огромного подводного леса, под своды которого вы и вступаете, — 3" },
                }
            },
            [416] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 173, Text = "Что вы выберете? Возьмете топор" },
                    new Option { Destination = 300, Text = ", арбалет" },
                    new Option { Destination = 59, Text = "или наденете доспехи" },
                }
            },
            [417] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 375, Text = "Волшебная дверь наделяет даром понимания языка рыб! Однако кто знает, всех ли? Поэтому, когда захотите воспользоваться приобретенным умением, прибавьте 33 к номеру параграфа, на котором будете находиться, и, если язык встречной рыбы понятен вам, сможете попытаться с ней поговорить — 375" },
                }
            },
            [418] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 477, Text = "Если уже встречались с ним, то 477" },
                    new Option { Destination = 37, Text = ", если же нет, то 37" },
                }
            },
            [419] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 397, Text = "Если хотите рискнуть, то 397" },
                    new Option { Destination = 189, Text = ", а если поплывете дальше, — 189" },
                }
            },
            [420] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 15, Text = "В таком случае запишите ее на свой листок как 1 еду и, покинув башню, продолжайте путешествие —15" },
                }
            },
            [421] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 90, Text = "Но куда направиться теперь? Если хотите, можете исследовать пещеру, вход в которую виден слева" },
                    new Option { Destination = 523, Text = ", или просто поплыть куда глаза глядят, надеясь, что удача найдет вас сама" },
                }
            },
            [422] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 398, Text = "Покидаете обиталище водяных, но куда теперь лежит ваш путь? Направо" },
                    new Option { Destination = 285, Text = "или налево" },
                }
            },
            [423] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 276, Text = "Попробуете скрыться от Кита, надеясь, что он вас не заметил" },
                    new Option { Destination = 93, Text = ", или же смело двинетесь ему навстречу" },
                }
            },
            [424] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 254, Text = "Если хотите, можете надеть его на палец" },
                    new Option { Destination = 345, Text = ", если же нет, то решайте, по какому подводному коридору поплывете дальше: по тому, что справа" },
                    new Option { Destination = 71, Text = ", или по тому, что перед вами" },
                }
            },
            [425] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 110, Text = "Хорошо, если Рыба-молот победила, тогда 110" },
                    new Option { Destination = 268, Text = ". Если же она мертва, придется вступить в бой вам — 268" },
                }
            },
            [426] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 94, Text = "Но еще не все потеряно, ведь в подводном царстве гораздо больше шансов найти разгадку таинственного исчезновения кораблей, — 94" },
                }
            },
            [427] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 38, Text = "Если убили его, то 38" },
                }
            },
            [428] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 549, Text = "Но путь дальше свободен — шаловливый Дух исчезает — 549" },
                }
            },
            [429] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 284, Text = "Если же их нет, — сражайтесь в одиночку — 284" },
                }
            },
            [430] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 353, Text = "Если победили его, то дальше путь лежит к странным развалинам, виднеющимся неподалеку, — 353" },
                }
            },
            [431] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 87, Text = "После этого можете отправляться дальше — 87" },
                }
            },
            [432] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 111, Text = "Если вы удачливы, то 111" },
                    new Option { Destination = 298, Text = ", если же нет, — 298" },
                }
            },
            [433] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 402, Text = "Куда теперь лежит ваш путь? К развалинам, которые видны неподалеку слева" },
                    new Option { Destination = 103, Text = ", или в открытое море" },
                }
            },
            [434] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 15, Text = "Вы выплевываете то, что откусили (однако какую-то часть все же успели проглотить, теперь вам нехорошо — потеряйте 4 ВЫНОСЛИВОСТИ), и, не рискуя отведать что-нибудь еще, покидаете башню —15" },
                }
            },
            [435] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 301, Text = "Приблизитесь к ним, чтобы узнать, что лежит в ящичках" },
                    new Option { Destination = 188, Text = ", поплывете обратно, свернув на развилке направо" },
                    new Option { Destination = 422, Text = ", или пересечете пещеру и узнаете, куда коридор ведет дальше" },
                }
            },
            [436] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 118, Text = "Если есть, то 118" },
                    new Option { Destination = 318, Text = ", если нет, то 318" },
                }
            },
            [437] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 117, Text = "Поплывете направо, где виднеется что-то похожее на подводное поселение" },
                    new Option { Destination = 302, Text = ", или налево, где вы заметили какое-то сооружение, напоминающее мельницу" },
                }
            },
            [438] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 502, Text = "Может быть, лучше все же воспользоваться опознавательным знаком" },
                    new Option { Destination = 518, Text = "? Или предпочтете сделать вид, что приплыли в этот зал специально для того, чтобы принять участие в состязании" },
                }
            },
            [439] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [440] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [441] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 117, Text = "Справа от вас виднеется что-то похожее на подводное поселение" },
                    new Option { Destination = 302, Text = ". А слева — нелепое сооружение, очень напоминающее мельницу" },
                }
            },
            [442] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 93, Text = "Решив, что лучше встретить опасность лицом к лицу, вы разворачиваетесь и, приготовив меч, устремляетесь вперед — 93" },
                }
            },
            [443] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 201, Text = "Если вам удалось убить его за пять раундов атаки, то можете либо попытаться бежать" },
                    new Option { Destination = 414, Text = "Если вы победили их, то 414" },
                }
            },
            [444] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 361, Text = "Но, быть может, вам еще суждено с ним, встретиться? Вернитесь на 361" },
                }
            },
            [445] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 181, Text = "Остается одно: бой —181" },
                }
            },
            [446] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 519, Text = "Поплывете к нему" },
                    new Option { Destination = 250, Text = ", или огромный подводный лес поблизости кажется вам более привлекательным" },
                }
            },
            [447] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 2, Text = "Если же нет, то, миновав скалу, можете продолжить свой путь — либо прямо" },
                    new Option { Destination = 146, Text = ", либо направо от нее" },
                }
            },
            [448] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 369, Text = "А ваши испытания далеко не закончены — 369" },
                }
            },
            [449] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Когда вы благополучно покончите со всеми пиратами, — 198" },
                }
            },
            [450] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 116, Text = "Согласитесь на это и спросите, каких именно рыбок он предлагает" },
                    new Option { Destination = 303, Text = ", откажетесь и попробуете поговорить с ним еще" },
                    new Option { Destination = 501, Text = "или отправитесь дальше" },
                }
            },
            [451] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 319, Text = "Если вы все же победили его, то 319" },
                }
            },
            [452] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 202, Text = "Хотите еще поговорить с Карликом" },
                    new Option { Destination = 501, Text = "или поблагодарите его и отправитесь дальше" },
                }
            },
            [453] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [454] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 105, Text = "Добравшись до развилки, сворачиваете налево —105" },
                }
            },
            [455] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 353, Text = "Теперь направляетесь к странным развалинам, которые видны неподалеку, — 353" },
                }
            },
            [456] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 569, Text = "Хотите покинуть остров, потеряв на нем всего 10 минут" },
                    new Option { Destination = 207, Text = ", или подниметесь на ближайший холм, чтобы осмотреть окрестности" },
                }
            },
            [457] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 629, Text = "Неужели он и есть таинственное Неведомое? Но почему же тогда погиб его собственный корвет — 629" },
                }
            },
            [458] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 320, Text = "Хотите взять его с собой (ведь оно тоже займет одно место в ваших карманах)" },
                    new Option { Destination = 396, Text = "или разочарованно поплывете прочь из леса, опушка которого уже видна" },
                }
            },
            [459] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 212, Text = "Хотите осмотреть ее, надеясь найти что-нибудь полезное" },
                    new Option { Destination = 509, Text = ", или покинете остров, проведя на нем 30 минут" },
                }
            },
            [460] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 530, Text = "Теперь же самое время покинуть остров, проведя на нем 20 минут, — 530" },
                }
            },
            [461] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [462] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 552, Text = "Хотите спуститься и посмотреть, что это блестит" },
                    new Option { Destination = 204, Text = ", или поплывете дальше" },
                }
            },
            [463] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 15, Text = "Вы решаете оставить проклятый арбалет там, где он лежал, и покинуть башню —15" },
                }
            },
            [464] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 471, Text = "Если у вас есть сушеный краб и вы хотите отдать его ведьме, то 471" },
                    new Option { Destination = 321, Text = ", если же нет, останется только распрощаться с хозяйкой и покинуть комнату — 321" },
                }
            },
            [465] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 248, Text = "На какой остров из тех, где вы еще не были, направитесь теперь? На первый" },
                    new Option { Destination = 16, Text = ", второй" },
                    new Option { Destination = 233, Text = ", третий" },
                    new Option { Destination = 165, Text = ", четвертый" },
                    new Option { Destination = 418, Text = ", пятый" },
                    new Option { Destination = 535, Text = ", седьмой" },
                    new Option { Destination = 100, Text = ", восьмой" },
                    new Option { Destination = 84, Text = ", девятый" },
                    new Option { Destination = 352, Text = ", десятый" },
                    new Option { Destination = 282, Text = ", одиннадцатый" },
                    new Option { Destination = 456, Text = ", двенадцатый" },
                    new Option { Destination = 500, Text = "или тринадцатый" },
                }
            },
            [466] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 539, Text = "После этого продолжаете свой путь — 539" },
                }
            },
            [467] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 553, Text = "Вернете перстень" },
                    new Option { Destination = 286, Text = "или будете драться, ведь теперь Карлик просто так вас не отпустит" },
                }
            },
            [468] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 516, Text = "Миновав риф, отправляетесь дальше — 516" },
                }
            },
            [469] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 199, Text = "Через картину первым проплываете вы" },
                }
            },
            [470] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 429, Text = "Если победил Филин, то он улетает" },
                    new Option { Destination = 570, Text = ". Если же ваш союзник мертв, придется в бой вступать вам" },
                }
            },
            [471] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 79, Text = "Если скажете, что да, — 79" },
                    new Option { Destination = 349, Text = ", если ответите, что нет, — 349" },
                }
            },
            [472] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 586, Text = "Хотите посмотреть, что это такое" },
                    new Option { Destination = 423, Text = ", или поплывете в другую сторону" },
                }
            },
            [473] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 159, Text = "Хотите направиться туда и выяснить, кто это был" },
                    new Option { Destination = 250, Text = ", или поплывете в другую сторону" },
                }
            },
            [474] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 139, Text = "Если вам повезло, — 139" },
                    new Option { Destination = 43, Text = ", если нет, — можете либо перейти к правому сундуку (если вы этого еще не делали)" },
                    new Option { Destination = 508, Text = ", либо уплыть прочь" },
                }
            },
            [475] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 305, Text = "Протиснувшись в узкий проход, проделанный прямо в скале и напоминающий чью-то нору, попадаете в огромную комнату — 305" },
                }
            },
            [476] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 329, Text = "Иначе либо обратитесь к Солнечной рыбе, либо сражайтесь сами — 329" },
                }
            },
            [477] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 92, Text = "Если у вас есть кольцо с трезубцем, то 92" },
                    new Option { Destination = 571, Text = ", если же нет, — 571" },
                }
            },
            [478] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [479] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 65, Text = "Если удачливы, то 65" },
                    new Option { Destination = 580, Text = ", если нет, — 580" },
                }
            },
            [480] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Если он победит их, может с чистой совестью уплыть домой" },
                    new Option { Destination = 583, Text = ", а если нет, — в бой вступать вам" },
                }
            },
            [481] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 422, Text = "В конце концов, вы выбираете направление, которое кажется правильным, исходя из этого делаете выбор на очередной развилке — 422" },
                }
            },
            [482] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 593, Text = "Поверите ему и отдадите изумруды" },
                    new Option { Destination = 322, Text = "или притворитесь, что пошутили и ни о чем подобном не слышали? Но офицер настаивает, что вы должны были их найти" },
                }
            },
            [483] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [484] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 461, Text = "Вы свободны, но горизонт чист, вокруг не видно ни корабля, ни острова — 461" },
                }
            },
            [485] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 195, Text = "Стремление побеседовать с самим Королем? Ну, что ж, желание победителя — закон, и через несколько минут двери королевских покоев распахиваются перед вами —195" },
                }
            },
            [486] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 488, Text = "В случае победы капитана над пиратами — 488" },
                    new Option { Destination = 583, Text = ", если же они его убили, то дальше сражаться придется вам" },
                }
            },
            [487] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 80, Text = "Хотите помочь ему" },
                    new Option { Destination = 350, Text = ", или пусть сражается один" },
                }
            },
            [488] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 478, Text = "Когда же вы твердо решили, что Ричард больше не будет сражаться за вас, — 478" },
                    new Option { Destination = 198, Text = "Теперь сражение продолжается — 198" },
                }
            },
            [489] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 605, Text = "Хотите надеть его" },
                    new Option { Destination = 126, Text = "или оставите лежать, где лежал, и поплывете дальше" },
                }
            },
            [490] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 591, Text = "Если у вас есть амулет, то 591" },
                    new Option { Destination = 592, Text = "Если обе рыбины мертвы, то 592" },
                }
            },
            [491] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Если вы убили своего противника, а Носорог еще жив, то он исчезает —198" },
                }
            },
            [492] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 67, Text = "Хотите исследовать пещеру" },
                    new Option { Destination = 302, Text = "или поплывете дальше — к необычному строению, отдаленно напоминающему мельницу" },
                }
            },
            [493] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 234, Text = "Но куда вы направитесь теперь: к красивым морским цветам впереди" },
                    new Option { Destination = 48, Text = "или в открытое море" },
                }
            },
            [494] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 429, Text = "Гриф улетит, если победит Кондора" },
                    new Option { Destination = 570, Text = ". Если же он потерпит поражение, то в бой придется вступать вам" },
                }
            },
            [495] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 105, Text = "Извинившись, вернитесь обратно на развилку, чтобы на этот раз свернуть налево" },
                    new Option { Destination = 31, Text = "или проплыть прямо" },
                }
            },
            [496] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 67, Text = "Хотите исследовать пещеру" },
                    new Option { Destination = 302, Text = "или поплывете дальше к какому-то строению, отдаленно напоминающему мельницу" },
                }
            },
            [497] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 304, Text = "Ведьма приводит вас на край какой-то глубокой обширной впадины и исчезает — 304" },
                }
            },
            [498] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 577, Text = "Теперь бой продолжаете вы, Рыцарь-водяной и ваш оставшийся в живых противник (МАСТЕРСТВО и ВЫНОСЛИВОСТЬ посмотрите на 577" },
                    new Option { Destination = 198, Text = "Если победа за вами, а рыцарь жив, то он уплывает —198" },
                }
            },
            [499] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 594, Text = "Далее бой продолжаете вы, оставшийся в живых пират и Морской рыцарь (ВЫНОСЛИВОСТЬ и МАСТЕРСТВО двух последних можете посмотреть на 594" },
                    new Option { Destination = 198, Text = "Если победа осталась за вами и рыцарь жив, он исчезает —198" },
                }
            },
            [500] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 183, Text = "Невольно сделав шаг назад и чуть не упав в море, достаете меч и готовитесь к сражению не на жизнь, а на смерть, понимая, что эта битва будет решающей и сейчас на карту поставлено все —183" },
                }
            },
            [501] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 279, Text = "Приблизитесь к нему" },
                    new Option { Destination = 396, Text = "или направитесь к выходу из леса, виднеющемуся далеко впереди" },
                }
            },
            [502] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 312, Text = "Если он и в самом деле у вас есть, то предъявите его, если же нет, — 312" },
                }
            },
            [503] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 202, Text = "Хотите еще поговорить с Карликом" },
                    new Option { Destination = 501, Text = "или поблагодарите его и отправитесь дальше" },
                }
            },
            [504] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 236, Text = "Коридор, по которому вы плыли, продолжается прямо" },
                    new Option { Destination = 422, Text = ", а еще один отходит направо" },
                }
            },
            [505] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 422, Text = "После этого Король делает знак одному из приближенных, и тот через потайную дверцу выводит вас в какой-то коридор, ведущий прочь от королевских покоев, — 422" },
                }
            },
            [506] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 443, Text = "Так что ничего не остается, кроме как продолжать бой, — 443" },
                }
            },
            [507] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 32, Text = "Если удалось победить ее, можете продолжать путь — 32" },
                }
            },
            [508] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 231, Text = "Выбор падает на одинокую угрюмую скалу, возвышающуюся неподалеку, и вы плывете прямо к ней — 231" },
                }
            },
            [509] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 248, Text = "На какой остров из тех, где вы еще не были, хотите направиться теперь? На первый" },
                    new Option { Destination = 16, Text = ", второй" },
                    new Option { Destination = 233, Text = ", третий" },
                    new Option { Destination = 165, Text = ", четвертый" },
                    new Option { Destination = 418, Text = ", пятый" },
                    new Option { Destination = 395, Text = ", шестой" },
                    new Option { Destination = 535, Text = ", седьмой" },
                    new Option { Destination = 100, Text = ", восьмой" },
                    new Option { Destination = 84, Text = ", девятый" },
                    new Option { Destination = 352, Text = ", десятый" },
                    new Option { Destination = 456, Text = ", двенадцатый" },
                    new Option { Destination = 500, Text = "или тринадцатый" },
                }
            },
            [510] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [511] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 322, Text = "Если же нет, то 322" },
                }
            },
            [512] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [513] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 208, Text = "Вы ничего не успеваете понять, как лев с рычанием кидается вперед, — 208" },
                }
            },
            [514] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 126, Text = "Поплывете к нему" },
                    new Option { Destination = 462, Text = ", в противоположную от него сторону" },
                    new Option { Destination = 206, Text = "или не хотите менять направление" },
                }
            },
            [515] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Тот в ярости щелкает пальцами, и пираты исчезают —198" },
                }
            },
            [516] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 311, Text = "Если хотите попробовать уговорить ее не делать этого, то 311" },
                    new Option { Destination = 211, Text = ", если же предпочитаете убить ее, чтобы расчистить путь, то 211" },
                }
            },
            [517] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 60, Text = "Хотите помочь ему" },
                    new Option { Destination = 486, Text = "или предоставите Мейстону сражаться с пиратами в одиночку" },
                }
            },
            [518] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 323, Text = "Если выпавшее число меньше вашего МАСТЕРСТВА, то 323" },
                    new Option { Destination = 128, Text = "; если же больше или равно, то 128" },
                }
            },
            [519] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 153, Text = "Можете попробовать уплыть от него, тогда 153" },
                    new Option { Destination = 64, Text = ". Если же вы его убили, — 64" },
                }
            },
            [520] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 397, Text = "Хотите приблизиться к ней и пообедать (говорят, моллюски очень неплохи на вкус)" },
                    new Option { Destination = 269, Text = "или поплывете дальше" },
                }
            },
            [521] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 481, Text = "Если удачливы, то 481" },
                    new Option { Destination = 130, Text = ", если нет, — 130" },
                }
            },
            [522] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 243, Text = "Ну, что ж, дело сделано, теперь отправляйтесь дальше — 243" },
                }
            },
            [523] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 17, Text = "Попробуете поговорить со стариком" },
                    new Option { Destination = 581, Text = ", решите узнать, куда он может отвезти" },
                    new Option { Destination = 247, Text = ", назовете дорогу сами" },
                    new Option { Destination = 88, Text = "или убьете его, чтобы захватить лодку и посмотреть, что в ней" },
                }
            },
            [524] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 376, Text = "Можете либо нагнать его и поговорить с капитаном" },
                    new Option { Destination = 95, Text = ", либо продолжать плыть, не меняя скорости и не выставляя дополнительных парусов" },
                }
            },
            [525] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 358, Text = "Ничего не остается, кроме как отправиться восвояси: вы и так провели на острове целый час — 358" },
                }
            },
            [526] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 393, Text = "Битва будет нелегкой, однако, если хотите, попробуйте быстро развернуться и скрыться от него" },
                    new Option { Destination = 181, Text = ". Если же нет, — тогда в бой" },
                }
            },
            [527] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 281, Text = "Так что же: выполните просьбу погибших моряков" },
                    new Option { Destination = 433, Text = "или проплывете мимо" },
                }
            },
            [528] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 353, Text = "Путь дальше свободен, и вы устремляетесь в открытое море к каким-то непонятным развалинам, которые виднеются впереди, — 353" },
                }
            },
            [529] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 18, Text = "7 золотых для него достаточно, и вы договариваетесь встретиться на следующее утро в порту, чтобы выйти в море, — 18" },
                }
            },
            [530] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 248, Text = "На какой из островов, где вы еще не были, будет лежать ваш путь? На первый" },
                    new Option { Destination = 16, Text = ", второй" },
                    new Option { Destination = 233, Text = ", третий" },
                    new Option { Destination = 165, Text = ", четвертый" },
                    new Option { Destination = 418, Text = ", пятый" },
                    new Option { Destination = 395, Text = ", шестой" },
                    new Option { Destination = 535, Text = ", седьмой" },
                    new Option { Destination = 100, Text = ", восьмой" },
                    new Option { Destination = 352, Text = ", десятый" },
                    new Option { Destination = 282, Text = ", одиннадцатый" },
                    new Option { Destination = 456, Text = ", двенадцатый" },
                    new Option { Destination = 500, Text = "или тринадцатый" },
                }
            },
            [531] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 280, Text = "Отдадите ему деньги, если они есть" },
                    new Option { Destination = 365, Text = ", или решите, что это слишком дорого, и, отпустив его, поплывете дальше" },
                }
            },
            [532] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 182, Text = "Если вы догадались, что значит МВК, то посмотрите параграф с соответствующим номером, если же нет, — придется оставить дверцу в покое и плыть дальше —182" },
                }
            },
            [533] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 414, Text = "При благополучном для вас исходе схватки — 414" },
                }
            },
            [534] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 63, Text = "В случае гибели моряка сражение придется продолжить самому" },
                    new Option { Destination = 217, Text = ", если же он перебил врагов, то исчезнет, кивнув на прощание и не дожидаясь вашей благодарности" },
                }
            },
            [535] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 191, Text = "Если же рыбки нет, то 191" },
                    new Option { Destination = 324, Text = ". Ну, а если предпочтете убраться с острова подобру-поздорову, не выясняя отношений со львом, то сделайте это (потеряв всего 20 минут) — 324" },
                }
            },
            [536] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 279, Text = "Приблизитесь к нему" },
                    new Option { Destination = 396, Text = "или направитесь к выходу из леса, виднеющемуся далеко впереди" },
                }
            },
            [537] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 397, Text = "Если хотите рискнуть, то 397" },
                    new Option { Destination = 189, Text = ", если же поплывете дальше, то 189" },
                }
            },
            [538] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [539] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 214, Text = "Если повержен и этот противник, то 214" },
                }
            },
            [540] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 248, Text = "На какой остров из тех, где вы еще не были, хотите направиться теперь? На первый" },
                    new Option { Destination = 16, Text = ", второй" },
                    new Option { Destination = 233, Text = ", третий" },
                    new Option { Destination = 165, Text = ", четвертый" },
                    new Option { Destination = 395, Text = ", шестой" },
                    new Option { Destination = 535, Text = ", седьмой" },
                    new Option { Destination = 100, Text = ", восьмой" },
                    new Option { Destination = 84, Text = ", девятый" },
                    new Option { Destination = 352, Text = ", десятый" },
                    new Option { Destination = 282, Text = ", одиннадцатый" },
                    new Option { Destination = 456, Text = ", двенадцатый" },
                    new Option { Destination = 500, Text = "или на тринадцатый" },
                }
            },
            [541] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 447, Text = "Но, слава Богу, остались в живых и теперь можете плыть дальше — 447" },
                }
            },
            [542] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 304, Text = "В конце концов, старик высаживает вас и уплывает — 304" },
                }
            },
            [543] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 34, Text = "Водяной хмурится, но отплывает в сторону, — можете плыть дальше (потеряйте 1 УДАЧУ) — 34" },
                }
            },
            [544] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 436, Text = "Вместо этого он спрашивает, что вы хотели бы получить от него: совет" },
                    new Option { Destination = 620, Text = "или удачу" },
                }
            },
            [545] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 277, Text = "Если остались живы, а свирепые рыбы повержены, то 277" },
                }
            },
            [546] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 537, Text = "Больше ничего интересного увидеть не удается, и вы покидаете башню и, проплыв высоко над остатками бывшей крепостной стены, двигаетесь дальше — 537" },
                }
            },
            [547] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [548] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 365, Text = "Послушаете его и отправитесь дальше" },
                    new Option { Destination = 523, Text = "или поступите по-своему (в этом случае вернитесь на 523" },
                }
            },
            [549] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 275, Text = "Направитесь туда, чтобы посмотреть, что это такое" },
                    new Option { Destination = 15, Text = ", или поторопитесь вперед к руинам, которые кажутся остатками какого-то древнего замка" },
                }
            },
            [550] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 69, Text = "Приблизитесь, чтобы посмотреть на цветы и послушать музыку" },
                    new Option { Destination = 396, Text = ", или отправитесь дальше — либо к выходу из леса" },
                    new Option { Destination = 101, Text = ", либо в глубь его" },
                }
            },
            [551] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 382, Text = "Куда же теперь? К симпатичному коралловому рифу справа от вас" },
                    new Option { Destination = 234, Text = "или к скоплению прекрасных подводных цветов слева" },
                }
            },
            [552] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 129, Text = "Хотите надеть красивый обруч на голову" },
                    new Option { Destination = 617, Text = "или оставите лежать где лежал" },
                }
            },
            [553] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 501, Text = "Теперь остается только в свою очередь попрощаться с Карликом и двинуться дальше —501" },
                }
            },
            [554] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 236, Text = "Путь лежит дальше по этому коридору — 236" },
                }
            },
            [555] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 248, Text = "На какой остров из тех, где вы еще не были, направитесь теперь? На первый" },
                    new Option { Destination = 16, Text = ", второй" },
                    new Option { Destination = 233, Text = ", третий" },
                    new Option { Destination = 165, Text = ", четвертый" },
                    new Option { Destination = 418, Text = ", пятый" },
                    new Option { Destination = 395, Text = ", шестой" },
                    new Option { Destination = 535, Text = ", седьмой" },
                    new Option { Destination = 84, Text = ", девятый" },
                    new Option { Destination = 352, Text = ", десятый" },
                    new Option { Destination = 282, Text = ", одиннадцатый" },
                    new Option { Destination = 456, Text = ", двенадцатый" },
                    new Option { Destination = 500, Text = "или тринадцатый" },
                }
            },
            [556] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 261, Text = "После этого вы проплываете через картину сами" },
                }
            },
            [557] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 474, Text = "Теперь, если хотите, займитесь вторым сундуком" },
                    new Option { Destination = 508, Text = ", а если нет — плывите дальше" },
                }
            },
            [558] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 462, Text = "А вы отправляетесь дальше либо минуя риф" },
                    new Option { Destination = 516, Text = ", либо огибая его" },
                }
            },
            [559] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 236, Text = "Вы же покидаете зал, где происходило состязание, через противоположный тоннель и плывете дальше — 236" },
                }
            },
            [560] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 504, Text = "Прикрыв дверь кухни, возвращаетесь в тоннель, из которого приплыли, и сворачиваете направо — 504" },
                }
            },
            [561] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 612, Text = "После этого, вытерев меч о ближайшие водоросли, можете плыть дальше — 612" },
                }
            },
            [562] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 41, Text = "Восстановите 4 ВЫНОСЛИВОСТИ и уходите — время дорого — 41" },
                }
            },
            [563] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [564] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 478, Text = "Тогда — 478" },
                    new Option { Destination = 476, Text = "Если же воины убили его, то можете вернуться на 476" },
                    new Option { Destination = 329, Text = "Если же на вашей стороне больше сражаться некому, то придется принять удар на себя" },
                }
            },
            [565] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 62, Text = "Когда ваша ВЫНОСЛИВОСТЬ уменьшится до 4, перейдите на 62" },
                    new Option { Destination = 277, Text = ". Если же вы победили морских хищниц и при этом остались живы, то 277" },
                }
            },
            [566] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 134, Text = "Не правда ли, не самая приятная встреча под водой? Хотите попытаться скрыться от нее? Тогда 134" },
                    new Option { Destination = 82, Text = "Теперь — 82" },
                }
            },
            [567] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 472, Text = "Оставляете его залечивать раны и плывете дальше — 472" },
                }
            },
            [568] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 333, Text = "Если вы уже открывали бутылку, то 333" },
                    new Option { Destination = 47, Text = ", если нет, — 47" },
                }
            },
            [569] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 248, Text = "На какой остров из тех, где вы еще не были, отправитесь теперь? На первый" },
                    new Option { Destination = 16, Text = ", второй" },
                    new Option { Destination = 233, Text = ", третий" },
                    new Option { Destination = 165, Text = ", четвертый" },
                    new Option { Destination = 418, Text = ", пятый" },
                    new Option { Destination = 395, Text = ", шестой" },
                    new Option { Destination = 535, Text = ", седьмой" },
                    new Option { Destination = 100, Text = ", восьмой" },
                    new Option { Destination = 84, Text = ", девятый" },
                    new Option { Destination = 352, Text = ", десятый" },
                    new Option { Destination = 282, Text = ", одиннадцатый" },
                    new Option { Destination = 500, Text = "или тринадцатый" },
                }
            },
            [570] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 429, Text = "Если он мертв, то 429" },
                }
            },
            [571] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 540, Text = "Ничего другого не остается, как покинуть остров, проведя на нем полчаса, — 540" },
                }
            },
            [572] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 56, Text = "Первым проплываете через картину с изображением грифа" },
                }
            },
            [573] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 559, Text = "К сожалению, победил один из ваших соперников — 559" },
                }
            },
            [574] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 324, Text = "Если удалось выйти из схватки победителем, можете либо покинуть остров, проведя на нем 40 минут" },
                    new Option { Destination = 61, Text = ", либо, соблюдая все меры предосторожности, вернуться в логово Льва и осмотреть его" },
                }
            },
            [575] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 475, Text = "Хотите посмотреть, что там находится" },
                    new Option { Destination = 135, Text = ", или не станете рисковать" },
                }
            },
            [576] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 377, Text = "Если удачливы, то 377" },
                    new Option { Destination = 112, Text = ", если нет, — 112" },
                }
            },
            [577] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 137, Text = "В том случае, если Рыцарь победит, а ваши противники еще живы, то 137" },
                    new Option { Destination = 498, Text = ". Если остался в живых только один, — 498" },
                    new Option { Destination = 198, Text = "Если все враги мертвы, то 198" },
                }
            },
            [578] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 32, Text = "(Если он когда-нибудь понадобится, прибавьте 376 к номеру параграфа, на котором будете в этот момент находиться.) После этого благодарите Морскую звезду, извиняетесь за забывчивость и плывете дальше — 32" },
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
                    new Option { Destination = 219, Text = "Если у вас есть трезубец, то 219" },
                    new Option { Destination = 189, Text = "Однако найти ответ на эту задачку невозможно, и вы плывете дальше —189" },
                }
            },
            [581] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 365, Text = "Удивленно пожимаете плечами и продолжаете путь — 365" },
                }
            },
            [582] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 367, Text = "Однако если вы убили ее, то 367" },
                }
            },
            [583] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Если удалось их перебить, то 198" },
                }
            },
            [584] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 338, Text = "Теперь, если вы еще не брали золотой пояс" },
                    new Option { Destination = 74, Text = "или браслет" },
                    new Option { Destination = 508, Text = ", можете это сделать, иначе придется плыть дальше" },
                }
            },
            [585] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 14, Text = "Что вы сделаете? Попытаетесь быстро захлопнуть дверь" },
                    new Option { Destination = 170, Text = "или примете вызов" },
                }
            },
            [586] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 144, Text = "Хотите приблизиться" },
                    new Option { Destination = 336, Text = "или предпочтете миновать его стороной" },
                }
            },
            [587] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 217, Text = "А сияние медленно гаснет и исчезает — 217" },
                }
            },
            [588] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [589] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Если после боя моряк остается жив, то он исчезает —198" },
                }
            },
            [590] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 343, Text = "Возьмите их с собой и отправляйтесь дальше — 343" },
                }
            },
            [591] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 490, Text = "Если хотите изменить свое решение, вернитесь на 490" },
                }
            },
            [592] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 225, Text = "Если есть раковина, то 225" },
                    new Option { Destination = 302, Text = ". Если нет ни того, ни другого, ваш путь лежит налево, где виднеется что-то очень похожее на мельницу, — 302" },
                }
            },
            [593] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 465, Text = "После, этого офицер с солдатами садится в шлюпку и отплывает, а вы покидаете остров, проведя на нем полчаса, — 465" },
                }
            },
            [594] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Если произошло невероятное и все враги мертвы, — 198" },
                    new Option { Destination = 226, Text = "Если же ваш союзник убил пирата, то может прийти вам на помощь (при этом, если живы оба ваши врага, — 226" },
                    new Option { Destination = 499, Text = ", а если только один, — 499" },
                }
            },
            [595] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 214, Text = "Восстановите 3 ВЫНОСЛИВОСТИ и отправляйтесь дальше — 214" },
                }
            },
            [596] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 99, Text = "Когда ВЫНОСЛИВОСТЬ вашего противника уменьшена до 2, удар грома потрясает остров — 99" },
                }
            },
            [597] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [598] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Если все пираты мертвы, а Носорог еще жив, то он исчезает -198" },
                }
            },
            [599] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 123, Text = "Теперь выберите остров, на который отправитесь —123" },
                }
            },
            [600] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 227, Text = "Тогда 227" },
                    new Option { Destination = 238, Text = ", если у вас остался один враг, или 238" },
                    new Option { Destination = 198, Text = "Ну, а в том случае, когда все враги будут уничтожены, а Леи останется жив, то он улетает — 198" },
                }
            },
            [601] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [602] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 105, Text = "Ничего другого не остается, кроме как вернуться на развилку и выбрать один из двух других тоннелей: прямо" },
                    new Option { Destination = 31, Text = "или направо" },
                }
            },
            [603] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 208, Text = "И откуда только оно взялось? Не успеваете ничего понять, как лев с рычанием бросается вперед, — 208" },
                }
            },
            [604] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 336, Text = "Куда теперь лежит ваш путь? Направо от скалы" },
                    new Option { Destination = 566, Text = "или налево" },
                }
            },
            [605] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 126, Text = "А теперь пора двигаться дальше —126" },
                }
            },
            [606] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 592, Text = "Но исчезает и сам амулет, ведь его можно использовать только один раз, — 592" },
                }
            },
            [607] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Когда последний враг будет убит — 198" },
                }
            },
            [608] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
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
                    new Option { Destination = 305, Text = "Смело плывете по тоннелю, но в конце его попадаете в очень странную комнату — 305" },
                }
            },
            [611] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 426, Text = "Если удачливы, то 426" },
                    new Option { Destination = 588, Text = ", если нет, — 588" },
                }
            },
            [612] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 520, Text = "Поплывете дальше в этом направлении" },
                    new Option { Destination = 243, Text = "или в противоположную сторону" },
                }
            },
            [613] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 437, Text = "Несколько минут она забавляется игрой, а потом пропадает совсем — 437" },
                }
            },
            [614] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 471, Text = "Если он есть и его не жалко отдать старухе, то 471" },
                    new Option { Destination = 321, Text = ". Если же нет, то не остается ничего другого, как попрощаться и уплыть из комнаты, — 321" },
                }
            },
            [615] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 168, Text = "Теперь можете отправляться дальше —168" },
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
                    new Option { Destination = 204, Text = "К тому же диадема могла оказаться волшебной, а кто знает, доброй или злой магией она наделена, — 204" },
                }
            },
            [618] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 441, Text = "Спрыгнете с него и поплывете дальше сами по себе" },
                    new Option { Destination = 215, Text = "или посмотрите, куда он вас привезет" },
                }
            },
            [619] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 8, Text = "Теперь самое время покинуть корабль и плыть дальше — 8" },
                }
            },
            [620] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [621] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 417, Text = "Вы торопитесь вслед" },
                }
            },
            [622] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [623] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 71, Text = "Теперь можете уплыть по любому из двух тоннелей: по тому, что впереди" },
                    new Option { Destination = 345, Text = ", или по тому, что справа от вас" },
                }
            },
            [624] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 258, Text = "Остается одно: покинуть остров, бессмысленно проведя на нем 40 минут, — 258" },
                }
            },
            [625] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Если битва закончилась вашей победой, — 198" },
                }
            },
            [626] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 422, Text = "Вежливо поблагодарив Короля, торопитесь вслед за одним из придворных к невысокой боковой дверце, открывающейся в новый коридор — 422" },
                }
            },
            [627] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 501, Text = "Течение тут же уносит отвергнутый подарок, а Карлик просто возмущен: как можно было предложить ему такую гадость! Приходится оставить капризное существо и двинуться дальше — 501" },
                }
            },
            [628] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 328, Text = "Закончив трапезу, плывете дальше — 328" },
                }
            },
            [629] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
        };
    }
}
