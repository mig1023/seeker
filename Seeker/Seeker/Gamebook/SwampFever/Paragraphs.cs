using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Seeker.Game;

namespace Seeker.Gamebook.SwampFever
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
                    new Option { Destination = 4, Text = "Далее" },
                }
            },
            [2] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 126, Text = "Рискнёте сразиться с монстром" },
                    new Option { Destination = 100, Text = "или предпочтёте ретироваться" },
                }
            },
            [3] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [4] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 12, Text = "Примете предложение служебного бота" },
                    new Option { Destination = 27, Text = "Или откажитесь, учитывая то, что время полёта будет длиться около тридцати дней" },
                }
            },
            [5] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 85, Text = "Если вы согласны, то покидаете харвестер" },
                    new Option { Destination = 29, Text = "В противном случае" },
                }
            },
            [6] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 96, Text = "Если есть метка №2" },
                    new Option { Destination = 20, Text = "Иначе" },
                }
            },
            [7] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 180, Text = "Какое направление выберете? На север - Пухлый Солончак" },
                    new Option { Destination = 150, Text = "на юго-запад - Грибная Чаща" },
                    new Option { Destination = 128, Text = "на юг - Гнилотопь" },
                    new Option { Destination = 106, Text = "на восток - Кристаллесье" },
                }
            },
            [8] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 93, Text = "Далее" },
                }
            },
            [09] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 75, Text = "Согласитесь" },
                    new Option { Destination = 90, Text = "Откажитесь" },
                }
            },
            [10] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 102, Text = "Впереди путь раздваивается, и вы движетесь строго по первой" },
                    new Option { Destination = 88, Text = "или второй дороге" },
                }
            },
            [11] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 124, Text = "Рискнёте покинуть харвестер, чтобы его почистить" },
                    new Option { Destination = 136, Text = "Либо, включив огнемёт, вы помчитесь вперёд" },
                }
            },
            [12] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 54, Text = "Далее" },
                }
            },
            [13] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [14] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 51, Text = "Куда двинетесь дальше? На север - Тимение Искупления" },
                    new Option { Destination = 56, Text = "на запад - Павший Колосс" },
                    new Option { Destination = 46, Text = "на юг - Запределье" },
                    new Option { Destination = 131, Text = "на восток - Край Ветров" },
                }
            },
            [15] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 103, Text = "Далее" },
                }
            },
            [16] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Далее" },
                }
            },
            [17] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 138, Text = "Далее" },
                }
            },
            [18] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 3, Text = "Если вы хотите покинуть Стигию, то можете оставаться в гостинице, ожидая своего фрегата" },
                    new Option { Destination = 30, Text = "В ином случае вы или временно тут проживаете (пропуск хода), или покидаете гостиницу" },
                }
            },
            [19] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 3, Text = "Далее" },
                }
            },
            [20] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 96, Text = "Вы будете ехать дальше" },
                    new Option { Destination = 140, Text = "или остановитесь, чтобы рискнуть наладить с яти контакт" },
                }
            },
            [21] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 116, Text = "Примите странное угощение" },
                    new Option { Destination = 132, Text = "или откажитесь" },
                }
            },
            [22] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 111, Text = "Если вы прошли испытание за 4 броска кубика" },
                    new Option { Destination = 65, Text = "В противном случае трос обрывается, и ялик скрывается в мутной пучине" },
                }
            },
            [23] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 56, Text = "Куда вы отсюда направитесь? На север - Павший Колосс" },
                    new Option { Destination = 39, Text = "на запад - Свалка" },
                    new Option { Destination = 64, Text = "на юг - Тихий Омут" },
                    new Option { Destination = 46, Text = "на восток - Запределье" },
                }
            },
            [24] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 74, Text = "Далее" },
                }
            },
            [25] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Далее" },
                }
            },
            [26] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 61, Text = "Вы можете подъехать к термитнику" },
                    new Option { Destination = 142, Text = "выстрелить в яму" },
                    new Option { Destination = 48, Text = "или вернуться на основную дорогу" },
                }
            },
            [27] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 54, Text = "Далее" },
                }
            },
            [28] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 109, Text = "Рискнёте расплавить лёд, чтобы извлечь ценное растение" },
                    new Option { Destination = 147, Text = "Или нет" },
                }
            },
            [29] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 41, Text = "Далее" },
                }
            },
            [30] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 18, Text = "Итак, куда держите путь? Гостиница" },
                    new Option { Destination = 34, Text = "Элеватор" },
                    new Option { Destination = 40, Text = "Техцентр" },
                    new Option { Destination = 25, Text = "Обсерватория" },
                    new Option { Destination = 45, Text = "Покинете Порт Грёз" },
                }
            },
            [31] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 52, Text = "Если есть метка № 19" },
                    new Option { Destination = 68, Text = "В противном случае" },
                }
            },
            [32] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 16, Text = "Будете помогать ликвидировать возгорание" },
                    new Option { Destination = 98, Text = "или нет" },
                }
            },
            [33] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 151, Text = "Проверка успешно пройдена" },
                    new Option { Destination = 71, Text = "Или провалена" },
                }
            },
            [34] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Далее" },
                }
            },
            [35] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 53, Text = "Далее" },
                }
            },
            [36] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 112, Text = "Далее" },
                }
            },
            [37] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 15, Text = "Если вы избежите участи быть съеденным и одолеете врага" },
                    new Option { Destination = 103, Text = "При маловероятной ничьей чудище само отступит" },
                }
            },
            [38] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 95, Text = "Рискнёте туда заехать" },
                    new Option { Destination = 114, Text = "Или нет" },
                }
            },
            [39] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 8, Text = "Если есть метка № 3" },
                    new Option { Destination = 129, Text = "В противном случае" },
                }
            },
            [40] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Далее" },
                }
            },
            [41] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 87, Text = "Куда поедете дальше? На северо-запад -Чертоги Холода" },
                    new Option { Destination = 91, Text = "на запад - Тернистая Лощина" },
                    new Option { Destination = 63, Text = "на юг - Клейкое Озеро" },
                    new Option { Destination = 131, Text = "на юго-восток - Край Ветров" },
                }
            },
            [42] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 83, Text = "При наличии метки № 20" },
                    new Option { Destination = 32, Text = "или № 21" },
                    new Option { Destination = 165, Text = "или №22 " },
                    new Option { Destination = 55, Text = "или № 23" },
                    new Option { Destination = 113, Text = "При отсутствии меток" },
                }
            },
            [43] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 3, Text = "Далее" },
                }
            },
            [44] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 69, Text = "Итак, испытание завершилось победой" },
                    new Option { Destination = 144, Text = "поражением" },
                }
            },
            [45] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 125, Text = "На севере находится Медная Заводь" },
                    new Option { Destination = 6, Text = "на западе - Старый Тракт" },
                    new Option { Destination = 39, Text = "на юге - Свалка" },
                    new Option { Destination = 56, Text = "на востоке - Павший Колосс" },
                }
            },
            [46] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 135, Text = "Развернётесь, чтобы выстрелить?" },
                    new Option { Destination = 81, Text = "Или попробуете скрыться" },
                }
            },
            [47] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 73, Text = "Далее" },
                }
            },
            [48] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 115, Text = "Куда направитесь? На северо-восток - Органная Пещера" },
                    new Option { Destination = 60, Text = "на юго-запад - Мегапровал" },
                    new Option { Destination = 143, Text = "на юг - Биполярный Кочкарник" },
                    new Option { Destination = 128, Text = "на восток - Гнилотопь" },
                }
            },
            [49] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 159, Text = "Куда направитесь теперь? На север - Роща Инвазии" },
                    new Option { Destination = 115, Text = "на запад - Органная Пещера" },
                    new Option { Destination = 125, Text = "на юг - Медная Заводь" },
                    new Option { Destination = 87, Text = "на восток - Чертоги Холода" },
                }
            },
            [50] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 139, Text = "Вы можете предупредить гидронейца об опасности" },
                    new Option { Destination = 110, Text = "выстрелить в Чернохвоста" },
                    new Option { Destination = 70, Text = "или не вмешиваться в ход событий" },
                }
            },
            [51] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 5, Text = "Если есть метка № 8" },
                    new Option { Destination = 104, Text = "Иначе" },
                }
            },
            [52] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 39, Text = "Куда теперь направитесь? На север - Свалка" },
                    new Option { Destination = 145, Text = "на запад - Гремучая Долина" },
                    new Option { Destination = 166, Text = "на юг - Гигросаванна" },
                    new Option { Destination = 64, Text = "на восток - Тихий Омут" },
                }
            },
            [53] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 51, Text = "Какое направление выберете? На северо-запад - Тимение Искупления" },
                    new Option { Destination = 63, Text = "на запад - Клейкое Озеро" },
                    new Option { Destination = 46, Text = "на юго-запад - Запределье" },
                }
            },
            [54] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Далее" },
                }
            },
            [55] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Далее" },
                }
            },
            [56] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 127, Text = "Если метка № 1 отсутствует" },
                    new Option { Destination = 112, Text = "Если метка № 1 активна" },
                }
            },
            [57] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 141, Text = "Как поступите? Попытаетесь отговорить жечь ценное растение" },
                    new Option { Destination = 105, Text = "атакуете безумца" },
                    new Option { Destination = 23, Text = "или просто проедете мимо" },
                }
            },
            [58] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 93, Text = "Далее" },
                }
            },
            [59] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 106, Text = "Куда двинетесь дальше? На север - Кристаллесье" },
                    new Option { Destination = 128, Text = "на запад - Гнилотопь" },
                    new Option { Destination = 42, Text = "на юг - Порт Грёз" },
                    new Option { Destination = 91, Text = "на восток - Тернистая Лощина" },
                }
            },
            [60] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 86, Text = "Если есть метка № 14" },
                    new Option { Destination = 134, Text = "Иначе" },
                }
            },
            [61] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 48, Text = "Далее" },
                }
            },
            [62] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 132, Text = "Откажитесь" },
                    new Option { Destination = 21, Text = "Согласитесь" },
                }
            },
            [63] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 14, Text = "Если есть метка № 4" },
                    new Option { Destination = 148, Text = "Иначе" },
                }
            },
            [64] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 73, Text = "Если есть метка № 17" },
                    new Option { Destination = 121, Text = "Иначе" },
                }
            },
            [65] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 115, Text = "Куда вы отсюда отправитесь? На север - Органная Пещера" },
                    new Option { Destination = 150, Text = "на запад - Грибная Чаща" },
                    new Option { Destination = 6, Text = "на юг - Старый Тракт" },
                    new Option { Destination = 125, Text = "на восток - Медная Заводь" },
                }
            },
            [66] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 71, Text = "Далее" },
                }
            },
            [67] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 45, Text = "Далее" },
                }
            },
            [68] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 130, Text = "При победе перейдите" },
                    new Option { Destination = 52, Text = "При ничейном исходе у вас появится выбор: продолжить бой или ретироваться" },
                }
            },
            [69] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 6, Text = "Куда направитесь дальше? На север - Старый Тракт" },
                    new Option { Destination = 123, Text = "на запад - Спектральные Кущи" },
                    new Option { Destination = 145, Text = "на юг -Гремучая Долина" },
                    new Option { Destination = 39, Text = "на восток - Свалка" },
                }
            },
            [70] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 7, Text = "Далее" },
                }
            },
            [71] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [72] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 96, Text = "Далее" },
                }
            },
            [73] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 119, Text = "Куда теперь двинетесь? На север - Рыжая Мшара" },
                    new Option { Destination = 31, Text = "на запад - Чаруса Чудес" },
                    new Option { Destination = 172, Text = "на юг - Пчелиные Марши" },
                    new Option { Destination = 46, Text = "северо-восток -Запределье" },
                }
            },
            [74] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 143, Text = "Куда направитесь дальше? На север - Биполярный Кочкарник" },
                    new Option { Destination = 60, Text = "на северо-запад - Мегапровал" },
                    new Option { Destination = 145, Text = "на юго-восток - Гремучая Долина" },
                    new Option { Destination = 44, Text = "на восток - Сернистая Падь" },
                }
            },
            [75] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 90, Text = "Далее" },
                }
            },
            [76] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 103, Text = "Далее" },
                }
            },
            [77] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 146, Text = "Далее" },
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
                    new Option { Destination = 108, Text = "Если победа за вами" },
                    new Option { Destination = 14, Text = "При ничьей вы можете ретироваться" },
                }
            },
            [80] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 112, Text = "Далее" },
                }
            },
            [81] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 63, Text = "Куда держите путь? На север - Клейкое Озеро" },
                    new Option { Destination = 119, Text = "на запад - Рыжая Мшара" },
                    new Option { Destination = 64, Text = "на юго-запад - Тихий Омут" },
                    new Option { Destination = 131, Text = "на северо-восток - Край Ветров" },
                }
            },
            [82] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 3, Text = "Далее" },
                }
            },
            [83] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 67, Text = "Вы можете покинуть поселение" },
                    new Option { Destination = 122, Text = "или присоединиться к протестующим" },
                }
            },
            [84] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 23, Text = "Далее" },
                }
            },
            [85] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 3, Text = "Далее" },
                }
            },
            [86] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 150, Text = "Куда дальше направитесь? На северо-восток - Грибная Чаща" },
                    new Option { Destination = 143, Text = "на восток - Биполярный Кочкарник" },
                    new Option { Destination = 123, Text = "на юго-восток - Спектральные Кущи" },
                }
            },
            [87] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 147, Text = "Если есть метка № 9" },
                    new Option { Destination = 28, Text = "Иначе" },
                }
            },
            [88] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 24, Text = "Если вы победите" },
                    new Option { Destination = 74, Text = "При ничьей можете ретироваться" },
                }
            },
            [89] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 93, Text = "Далее" },
                }
            },
            [90] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Далее" },
                }
            },
            [91] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 118, Text = "Если есть метка № 5" },
                    new Option { Destination = 11, Text = "Иначе" },
                }
            },
            [92] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 132, Text = "Далее" },
                }
            },
            [93] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 42, Text = "Куда держите путь? На север - Порт Грёз" },
                    new Option { Destination = 44, Text = "на запад -Сернистая Падь" },
                    new Option { Destination = 31, Text = "на юг - Чаруса Чудес" },
                    new Option { Destination = 119, Text = "на восток - Рыжая Мшара" },
                }
            },
            [94] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 22, Text = "Попробуете вытащить собственность компании" },
                    new Option { Destination = 65, Text = "или проедете мимо" },
                }
            },
            [95] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 114, Text = "Далее" },
                }
            },
            [96] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 128, Text = "На север - Гнилотопь" },
                    new Option { Destination = 143, Text = "на запад - Биполярный Кочкарник" },
                    new Option { Destination = 44, Text = "на юг - Сернистая Падь" },
                    new Option { Destination = 42, Text = "на восток - Порт Грёз" },
                }
            },
            [97] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 73, Text = "Далее" },
                }
            },
            [98] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 45, Text = "Далее" },
                }
            },
            [99] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 96, Text = "Далее" },
                }
            },
            [100] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 86, Text = "Далее" },
                }
            },
            [101] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 43, Text = "Если проверка успешно пройдена, то вам удалось уйти в сторону" },
                    new Option { Destination = 82, Text = "Корпус харвестера сгорает, а вы, находясь в капсуле водителя, теряете сознание" },
                }
            },
            [102] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 74, Text = "Далее" },
                }
            },
            [103] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 150, Text = "Где дальше будете месить грязь? На севере - Грибная Чаща" },
                    new Option { Destination = 60, Text = "на западе - Мегапровал" },
                    new Option { Destination = 123, Text = "на юге - Спектральные Кущи" },
                    new Option { Destination = 6, Text = "на востоке - Старый Тракт" },
                }
            },
            [104] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 62, Text = "Вы можете подъехать к тени ближе" },
                    new Option { Destination = 41, Text = "или ретироваться" },
                }
            },
            [105] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 23, Text = "Далее" },
                }
            },
            [106] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 49, Text = "Выберите один из возможных путей для движения: А" },
                    new Option { Destination = 33, Text = "В" },
                    new Option { Destination = 66, Text = "С" },
                }
            },
            [107] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 89, Text = "Если бой завершится ничьёй, то вы можете прекратить огонь и подождать дальнейших действий робота" },
                    new Option { Destination = 58, Text = "Если победите вы" },
                    new Option { Destination = 19, Text = "В противном случае" },
                }
            },
            [108] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 14, Text = "Далее" },
                }
            },
            [109] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 147, Text = "Далее" },
                }
            },
            [110] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 77, Text = "Рука тянется уничтожить паразита, но из какого оружия? Огнемёт" },
                    new Option { Destination = 120, Text = "или Плазмопушка" },
                }
            },
            [111] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 65, Text = "Далее" },
                }
            },
            [112] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 91, Text = "Куда направитесь дальше? На север - Тернистая Лощина" },
                    new Option { Destination = 42, Text = "на запад - Порт Грёз" },
                    new Option { Destination = 119, Text = "на юг - Рыжая Мшара" },
                    new Option { Destination = 63, Text = "на восток - Клейкое Озеро" },
                }
            },
            [113] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 9, Text = "'Да, стигон есть!'" },
                    new Option { Destination = 149, Text = "'Нет, я еду пустым'" },
                }
            },
            [114] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 44, Text = "Куда повернёте? На север - Сернистая Падь" },
                    new Option { Destination = 123, Text = "на северо-запад - Спектральные Кущи" },
                    new Option { Destination = 167, Text = "на юг - Дикий Зыбун" },
                    new Option { Destination = 31, Text = "на восток - Чаруса Чудес" },
                }
            },
            [115] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 7, Text = "Если есть метка № 10" },
                    new Option { Destination = 50, Text = "В противном случае" },
                }
            },
            [116] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 92, Text = "Проверка пройдена" },
                    new Option { Destination = 78, Text = "Или провалена" },
                }
            },
            [117] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 26, Text = "Последуете за ними" },
                    new Option { Destination = 48, Text = "или поедете дальше по тропе" },
                }
            },
            [118] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 87, Text = "На север - Чертоги Холода" },
                    new Option { Destination = 125, Text = "на запад - Медная Заводь" },
                    new Option { Destination = 56, Text = "на юг -Павший Колосс" },
                    new Option { Destination = 51, Text = "на восток - Тимение Искупления" },
                }
            },
            [119] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 23, Text = "Если есть метка № 6" },
                    new Option { Destination = 57, Text = "Иначе" },
                }
            },
            [120] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 7, Text = "Далее" },
                }
            },
            [121] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 137, Text = "'20-17-15-9-6-10'" },
                    new Option { Destination = 97, Text = "'20-18-11-8-6-10'" },
                }
            },
            [122] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Далее" },
                }
            },
            [123] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 74, Text = "Если есть метка № 15" },
                    new Option { Destination = 10, Text = "Иначе" },
                }
            },
            [124] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 152, Text = "Проверка завершилась успехом" },
                    new Option { Destination = 71, Text = "или провалом" },
                }
            },
            [125] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 17, Text = "При наличии метки № 13" },
                    new Option { Destination = 59, Text = "Иначе" },
                }
            },
            [126] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 86, Text = "После победы запишите метку № 14" },
                    new Option { Destination = 100, Text = "При ничьей возникнет выбор: продолжить схватку или отступить" },
                    new Option { Destination = 100, Text = "При ничьей возникнет выбор: продолжить схватку или отступить" },
                }
            },
            [127] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 80, Text = "Вы можете наблюдать за памятником из-за кустов" },
                    new Option { Destination = 36, Text = "или подъехать к статуе вплотную, чтобы исследовать ухо" },
                }
            },
            [128] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 65, Text = "Если есть метка № 7" },
                    new Option { Destination = 94, Text = "Иначе" },
                }
            },
            [129] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 107, Text = "Начнёте отстреливаться" },
                    new Option { Destination = 89, Text = "или будете ждать своей участи" },
                }
            },
            [130] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 52, Text = "Далее" },
                }
            },
            [131] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 53, Text = "Если есть метка № 16" },
                    new Option { Destination = 35, Text = "Иначе" },
                }
            },
            [132] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 41, Text = "Далее" },
                }
            },
            [133] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 37, Text = "Первому" },
                    new Option { Destination = 76, Text = "Второму" },
                }
            },
            [134] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 154, Text = "Если есть метка № 12" },
                    new Option { Destination = 2, Text = "Если нет" },
                    new Option { Destination = 2, Text = "Если нет" },
                }
            },
            [135] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 63, Text = "Где продолжите свои странствия? На севере - Клейкое Озеро" },
                    new Option { Destination = 119, Text = "на западе - Рыжая Мшара" },
                    new Option { Destination = 64, Text = "на юго-западе -Тихий Омут" },
                    new Option { Destination = 131, Text = "на северо-востоке - Край Ветров" },
                }
            },
            [136] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 118, Text = "Далее" },
                }
            },
            [137] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 47, Text = "Если вы одолеете крикливую тварь" },
                    new Option { Destination = 73, Text = "При ничейном исходе боя монстр отступит" },
                }
            },
            [138] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 59, Text = "Далее" },
                }
            },
            [139] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 70, Text = "Далее" },
                }
            },
            [140] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 72, Text = "Возможно, что изобразив средство контроля над рабами, вы напугаете и заставите дикаря повиноваться" },
                    new Option { Destination = 99, Text = "Яти любят, когда что-то сияет" },
                }
            },
            [141] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 84, Text = "Далее" },
                }
            },
            [142] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 82, Text = "Далее" },
                }
            },
            [143] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 103, Text = "Если есть метка № 11" },
                    new Option { Destination = 133, Text = "Иначе" },
                }
            },
            [144] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 69, Text = "Проверка завершилась успехом" },
                    new Option { Destination = 13, Text = "или провалом" },
                }
            },
            [145] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 114, Text = "Если есть метка № 18" },
                    new Option { Destination = 38, Text = "Иначе" },
                }
            },
            [146] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 7, Text = "Далее" },
                }
            },
            [147] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 160, Text = "Куда двинетесь теперь? На север - Разноцветье" },
                    new Option { Destination = 106, Text = "запад - Кристаллесье" },
                    new Option { Destination = 91, Text = "на юг - Тернистая Лощина" },
                    new Option { Destination = 51, Text = "на юго-восток - Тимение Искупления" },
                }
            },
            [148] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 79, Text = "Вы притормозите" },
                    new Option { Destination = 153, Text = "или ускоритесь" },
                }
            },
            [149] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Далее" },
                }
            },
            [150] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 117, Text = "Если есть метка № 5" },
                    new Option { Destination = 48, Text = "Иначе" },
                }
            },
            [151] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 49, Text = "Далее" },
                }
            },
            [152] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 118, Text = "Далее" },
                }
            },
            [153] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 14, Text = "Далее" },
                }
            },
            [154] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 86, Text = "Далее" },
                }
            },
            [155] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 182, Text = "Далее" },
                }
            },
            [156] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 181, Text = "Рискнёте покинуть харвестер, чтобы тщательнее искать несчастного" },
                    new Option { Destination = 31, Text = "Или продолжите путь как ни в чём не бывало? При втором варианте решайте, куда именно направитесь? На север - Чаруса Чудес" },
                    new Option { Destination = 167, Text = "На запад - Дикий Зыбун" },
                    new Option { Destination = 172, Text = "На восток - Пчелиные Марши" },
                }
            },
            [157] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 175, Text = "Если победа за вами" },
                    new Option { Destination = 169, Text = "Ретироваться" },
                    new Option { Destination = 13, Text = "При поражении животное забросает машину огненными шарами" },
                }
            },
            [158] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Далее" },
                }
            },
            [159] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 183, Text = "Если есть метка № 26" },
                    new Option { Destination = 185, Text = "Иначе" },
                }
            },
            [160] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 170, Text = "Если есть метка № 28" },
                    new Option { Destination = 196, Text = "Иначе" },
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
                    new Option { Destination = 31, Text = "На север - Чаруса Чудес" },
                    new Option { Destination = 167, Text = "На запад - Дикий Зыбун" },
                    new Option { Destination = 172, Text = "На восток - Пчелиные Марши" },
                }
            },
            [163] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 188, Text = "Больше «Чемиод»" },
                    new Option { Destination = 190, Text = "Больше «Ноплюче»" },
                }
            },
            [164] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 183, Text = "Далее" },
                }
            },
            [165] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 176, Text = "Изъявите желание участвовать в состязании" },
                    new Option { Destination = 158, Text = "Или предпочтёте стоять в стороне" },
                }
            },
            [166] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 171, Text = "Если есть № 24" },
                    new Option { Destination = 200, Text = "Иначе" },
                }
            },
            [167] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Если есть метка № 27" },
                    new Option { Destination = 177, Text = "Иначе" },
                }
            },
            [168] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 174, Text = "Согласитесь на игру" },
                    new Option { Destination = 178, Text = "Попробуете взять 'своё' силой" },
                    new Option { Destination = 194, Text = "Проедете мимо" },
                }
            },
            [169] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 31, Text = "На севере - Чаруса Чудес" },
                    new Option { Destination = 167, Text = "На западе - Дикий Зыбун" },
                    new Option { Destination = 172, Text = "На востоке - Пчелиные Марши" },
                }
            },
            [170] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 159, Text = "На запад - Роща Инвазии" },
                    new Option { Destination = 87, Text = "На юг - Чертоги Холода" },
                }
            },
            [171] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 156, Text = "Если есть № 25" },
                    new Option { Destination = 179, Text = "Иначе" },
                }
            },
            [172] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 182, Text = "Если есть метка № 30" },
                    new Option { Destination = 199, Text = "Иначе" },
                }
            },
            [173] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 183, Text = "Далее" },
                }
            },
            [174] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 163, Text = "Вторая 'А'" },
                    new Option { Destination = 197, Text = "Вторая 'О'" },
                }
            },
            [175] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 31, Text = "Где нынче будете колесить? На севере - Чаруса Чудес" },
                    new Option { Destination = 167, Text = "На западе - Дикий Зыбун" },
                    new Option { Destination = 172, Text = "На востоке - Пчелиные Марши" },
                }
            },
            [176] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 193, Text = "Если вы сделали три шага назад и победили" },
                    new Option { Destination = 187, Text = "Если вы сделали три шага вперёд и проиграли" },
                }
            },
            [177] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 184, Text = "Примете с ними бой" },
                    new Option { Destination = 161, Text = "Или спрячетесь в кустах" },
                }
            },
            [178] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 194, Text = "Далее" },
                    new Option { Destination = 194, Text = "При ничьей можете отступить" },
                }
            },
            [179] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 195, Text = "Агрегат с квадратом" },
                    new Option { Destination = 162, Text = "Агрегат с треугольником" },
                }
            },
            [180] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 194, Text = "Если есть метка № 29" },
                    new Option { Destination = 168, Text = "Иначе" },
                }
            },
            [181] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 191, Text = "Успех" },
                    new Option { Destination = 186, Text = "Провал" },
                }
            },
            [182] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 64, Text = "На север - Тихий Омут" },
                    new Option { Destination = 166, Text = "На запад - Гигросаванна" },
                }
            },
            [183] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 180, Text = "Куда отправитесь теперь? На запад - Пухлый Солончак" },
                    new Option { Destination = 106, Text = "На юг - Кристаллесье" },
                    new Option { Destination = 160, Text = "На восток - Разноцветье" },
                }
            },
            [184] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [185] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 173, Text = "'Я пришёл с миром'" },
                    new Option { Destination = 164, Text = "'Я тебя понимаю'" },
                }
            },
            [186] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [187] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Далее" },
                }
            },
            [188] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 194, Text = "Далее" },
                }
            },
            [189] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 170, Text = "Далее" },
                }
            },
            [190] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 194, Text = "Далее" },
                }
            },
            [191] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 31, Text = "Где дальше будете странствовать? На севере - Чаруса Чудес" },
                    new Option { Destination = 167, Text = "на западе - Дикий Зыбун" },
                    new Option { Destination = 172, Text = "на востоке - Пчелиные Марши" },
                }
            },
            [192] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Если вы находитесь в точке с чётной цифрой, то птицы пробегают мимо" },
                    new Option { Destination = 184, Text = "Харвестер заметен со стороны" },
                }
            },
            [193] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Далее" },
                }
            },
            [194] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 115, Text = "На юг - Органная Пещера" },
                    new Option { Destination = 159, Text = "или на восток - Роща Инвазии" },
                }
            },
            [195] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 186, Text = "Далее" },
                }
            },
            [196] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 189, Text = "Пуститесь в погоню за Стигиносцем" },
                    new Option { Destination = 170, Text = "или не станете этого делать" },
                }
            },
            [197] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 194, Text = "Согласитесь и поедете дальше" },
                    new Option { Destination = 178, Text = "Или рискнёте отобрать стигон" },
                }
            },
            [198] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 145, Text = "На север - Гремучая Долина" },
                    new Option { Destination = 166, Text = "или на восток - Гигросаванна" },
                }
            },
            [199] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 155, Text = "Рискнёте ли забрать гнездо" },
                    new Option { Destination = 182, Text = "или решите не трогать его" },
                }
            },
            [200] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 157, Text = "Проучите усатого наглеца" },
                    new Option { Destination = 169, Text = "Предпочтёте не связываться" },
                }
            },

        };
    }
}
