using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Seeker.Game;

namespace Seeker.Gamebook.BlackCastleDungeon
{
    class Paragraphs : Interfaces.IParagraphs
    {
        public Paragraph Get(int id)
        {
            return Paragraph[id];
        }

        private static Dictionary<int, Paragraph> Paragraph = new Dictionary<int, Paragraph>
        {
            [0] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 1, Text = "В путь!" },
                    new Option { Destination = 618, Text = "Правила и инструкции" },
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
                Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
                        new Character
                        {
                            Name = "ПЕРВЫЙ ДРОВОСЕК",
                            Mastery = 5,
                            Endurance = 4,
                        },
                        new Character
                        {
                            Name = "ВТОРОЙ ДРОВОСЕК",
                            Mastery = 6,
                            Endurance = 7,
                        }
                    },
                },
		
		Modification = new Modification
                {
                    Name = "Luck",
                    Value = -1,
                },

                Options = new List<Option>
                {
                    new Option { Destination = 420, Text = "Отправиться дальше" },
                }
            },
            [7] = new Paragraph
            {
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -2,
                },
		
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
                OpenOption = "PirateTreasure",

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
                Action = new Actions
                {
                    ActionName = "Luck",
                    ButtonName = "Проверить удачу",
                },

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
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
                        new Character
                        {
                            Name = "ПЕРВАЯ ЛЕТУЧАЯ МЫШЬ",
                            Mastery = 6,
                            Endurance = 8,
                        },
                        new Character
                        {
                            Name = "ВТОРАЯ ЛЕТУЧАЯ МЫШЬ",
                            Mastery = 5,
                            Endurance = 7,
                        }
			new Character
                        {
                            Name = "ТРЕТЬЯ ЛЕТУЧАЯ МЫШЬ",
                            Mastery = 5,
                            Endurance = 6,
                        }
                    },
                },

		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -1,
                },		
		    
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
                    new Option { Destination = 109, Text = "Вы идете сражаться с волшебником" },
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
                    new Option { Destination = 427, Text = "Через дверь в той же стене, где вход" },
                    new Option { Destination = 398, Text = "Через правую дверь" },
                    new Option { Destination = 206, Text = "Через левую дверь" },
                    new Option { Destination = 350, Text = "Через среднюю дверь" },
                }
            },
            [23] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 327, Text = "Заплатить ему" },
                    new Option { Destination = 217, Text = "Поищите в заплечном мешке подарок" },
                    new Option { Destination = 106, Text = "Вежливо поблагодарите и уйдете" },
                    new Option { Destination = 518, Text = "Лучше убить его" },
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
		Modification = new Modification
                {
                    Name = "Mastery",
                    Value = -1,
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 207, Text = "Далее" },
                }
            },
            [33] = new Paragraph
            {
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
                        new Character
                        {
                            Name = "ОРК",
                            Mastery = 6,
                            Endurance = 8,
                        },
			new Character
                        {
                            Name = "ГОБЛИН",
                            Mastery = 7,
                            Endurance = 5,
                        },
                    },
                },
		    
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
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
                        new Character
                        {
                            Name = "ДРАКОН",
                            Mastery = 9,
                            Endurance = 4,
                        },
                    },
                },
		
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -2,
                },
		    
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
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
                        new Character
                        {
                            Name = "ПЕРВЫЙ ГОБЛИН",
                            Mastery = 6,
                            Endurance = 9,
                        },
			new Character
                        {
                            Name = "ВТОРОЙ ГОБЛИН",
                            Mastery = 7,
                            Endurance = 5,
                        },
                    },
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 118, Text = "Далее" },
                }
            },
            [41] = new Paragraph
            {
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
                        new Character
                        {
                            Name = "ДРАКОН",
                            Mastery = 9,
                            Endurance = 4,
                        },
                    },
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 454, Text = "Далее" },
                }
            },
            [42] = new Paragraph
            {
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -2,
                },
		
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
                    new Option { Destination = 101, Text = "К деревне" },
                    new Option { Destination = 262, Text = "К лесу" },
                    new Option { Destination = 187, Text = "Пойти поискать клад", OnlyIf = "PirateTreasure" },
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
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
                        new Character
                        {
                            Name = "ГИГАНТСКИЙ ПАУК",
                            Mastery = 8,
                            Endurance = 8,
                        },
                    },
                },
		
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
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
                        new Character
                        {
                            Name = "НАЧАЛЬНИК СТРАЖИ",
                            Mastery = 9,
                            Endurance = 5,
                        },
                    },
                },
		
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
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -1,
                },
		
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
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -2,
                },
		
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
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
                        new Character
                        {
                            Name = "ЖЕНЩИНА-ВАМПИР",
                            Mastery = 11,
                            Endurance = 14,
                        },
                    },
                },
		
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
		Action = new Actions
                {
                    ActionName = "Luck",
                    ButtonName = "Проверить удачу",
                },
		
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
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
                        new Character
                        {
                            Name = "ГОБЛИН",
                            Mastery = 8,
                            Endurance = 9,
                        },
                    },
                },
		
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
		Action = new Actions
                {
                    ActionName = "Luck",
                    ButtonName = "Проверить удачу",
                },
		
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
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -2,
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 309, Text = "Вернуться" },
                }
            },
            [112] = new Paragraph
            {
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
                        new Character
                        {
                            Name = "ЗЕЛЕНЫЙ РЫЦАРЬ",
                            Mastery = 10,
                            Endurance = 10,
                        },
                    },
                },
		
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
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -2,
                },
		
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
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
                        new Character
                        {
                            Name = "ПЕРВЫЙ РАЗБОЙНИК",
                            Mastery = 6,
                            Endurance = 4,
                        },
			new Character
                        {
                            Name = "ВТОРОЙ РАЗБОЙНИК",
                            Mastery = 7,
                            Endurance = 8,
                        },
			new Character
                        {
                            Name = "ТРЕТИЙ РАЗБОЙНИК",
                            Mastery = 5,
                            Endurance = 5,
                        },
                    },
                },
		    
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
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -2,
                },
		
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
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -2,
                },
		
		// -1 Mastery
		
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
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -2,
                },
		
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
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
                        new Character
                        {
                            Name = "ПЕРВЫЙ ЗЕЛЕНЫЙ РЫЦАРЬ",
                            Mastery = 10,
                            Endurance = 10,
                        },
			new Character
                        {
                            Name = "ВТОРОЙ ЗЕЛЕНЫЙ РЫЦАРЬ",
                            Mastery = 10,
                            Endurance = 10,
                        },
                    },
                },
		
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
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -1,
                },
		    
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
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
                        new Character
                        {
                            Name = "ГОБЛИН",
                            Mastery = 7,
                            Endurance = 9,
                        },
                    },
                },
		
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
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -2,
                },
		
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
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -3,
                },
		
		// -1 Mastery
		
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
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
                        new Character
                        {
                            Name = "ОБЕЗЬЯНА",
                            Mastery = 9,
                            Endurance = 14,
                        },
                    },
                },
		
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
		Action = new Actions
                {
                    ActionName = "Luck",
                    ButtonName = "Проверить удачу",
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 463, Text = "Удачлив" },
                    new Option { Destination = 539, Text = "Нет" },
                }
            },
            [219] = new Paragraph
            {
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
                        new Character
                        {
                            Name = "ГИГАНТСКИЙ ПАУК",
                            Mastery = 5,
                            Endurance = 8,
                        },
                    },
                },
		
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
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -1,
                },
		
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
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -2,
                },
		
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
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
                        new Character
                        {
                            Name = "ПРИЗРАК",
                            Mastery = 10,
                            Endurance = 9,
                        },
                    },
                },
		
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
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
                        new Character
                        {
                            Name = "ПАУК",
                            Mastery = 8,
                            Endurance = 8,
                        },
                    },
                },
		
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
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
                        new Character
                        {
                            Name = "ДУХ МЕРТВЫХ",
                            Mastery = 10,
                            Endurance = 12,
                        },
                    },
                },  
		    
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
                    new Option { Destination = 33, Text = "Далее" },
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
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
                        new Character
                        {
                            Name = "ЛЕВ",
                            Mastery = 9,
                            Endurance = 15,
                        },
                    },
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 565, Text = "Знаете пароль" },
                    new Option { Destination = 264, Text = "Если лев мёртв" },
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
		Modification = new Modification
                {
                    Name = "Luck",
                    Value = -1,
                },
		    
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
                    new Option { Destination = 467, Text = "В дверь налево" },
                }
            },
            [265] = new Paragraph
            {
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
                        new Character
                        {
                            Name = "ГИЕНА",
                            Mastery = 6,
                            Endurance = 6,
                        },
                    },
                },
		    
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
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -4,
                },
		
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
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
                        new Character
                        {
                            Name = "ГОБЛИН",
                            Mastery = 6,
                            Endurance = 9,
                        },
                    },
                },
		
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
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
                        new Character
                        {
                            Name = "БАРЛАД ДЭРТ",
                            Mastery = 14,
                            Endurance = 12,
                        },
                    },
                },
		
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
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -2,
                },
		
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
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
                        new Character
                        {
                            Name = "ПЕРВЫЙ РЫЦАРЬ",
                            Mastery = 10,
                            Endurance = 10,
                        },
			new Character
                        {
                            Name = "ВТОРОЙ РЫЦАРЬ",
                            Mastery = 10,
                            Endurance = 10,
                        },
			new Character
                        {
                            Name = "ТРЕТИЙ РЫЦАРЬ",
                            Mastery = 10,
                            Endurance = 10,
                        },
			new Character
                        {
                            Name = "КАПИТАН РЫЦАРЕЙ",
                            Mastery = 12,
                            Endurance = 12,
                        },
                    },
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 170, Text = "Есть Оберег" },
                    new Option { Destination = 606, Text = "Есть серебряный сосуд" },
                    new Option { Destination = 594, Text = "В правую дверь" },
                    new Option { Destination = 599, Text = "В левую дверь" },
                }
            },
            [302] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 11, Text = "Дадите напиться" },
                    new Option { Destination = 234, Text = "Откажете" },
                }
            },
            [303] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 99, Text = "Идти дальше" },
                    new Option { Destination = 117, Text = "Вернуться" },
                }
            },
            [304] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 88, Text = "Пойдете дальше" },
                    new Option { Destination = 12, Text = "Cвернете к озеру" },
                }
            },
            [305] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 412, Text = "Далее" },
                }
            },
            [306] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 52, Text = "Далее" },
                }
            },
            [307] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 261, Text = "Далее" },
                }
            },
            [308] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 416, Text = "К центральному строению" },
                    new Option { Destination = 220, Text = "К низкому зданию" },
                    new Option { Destination = 4, Text = "К реке" },
                }
            },
            [309] = new Paragraph
            {
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
                        new Character
                        {
                            Name = "ЗЕЛЕНЫЙ РЫЦАРЬ",
                            Mastery = 10,
                            Endurance = 10,
                        },
		    },
                },
		
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -3,
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 111, Text = "Наложить заклятие Копии" },
                    new Option { Destination = 126, Text = "Если победили" },
                }
            },
            [310] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 179, Text = "Далее" },
                }
            },
            [311] = new Paragraph
            {
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -1,
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 115, Text = "Да, догадались" },
                }
            },
            [312] = new Paragraph
            {
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -4,
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 192, Text = "Далее" },
                }
            },
            [313] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 222, Text = "Далее" },
                }
            },
            [314] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 183, Text = "Далее" },
                }
            },
            [315] = new Paragraph
            {
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -2,
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 409, Text = "Далее" },
                }
            },
            [316] = new Paragraph
            {
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
                        new Character
                        {
                            Name = "ДРАКОН",
                            Mastery = 12,
                            Endurance = 8,
                        },
		    },
                },
		
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -4,
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 513, Text = "Удалось" },
                }
            },
            [317] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 127, Text = "Сделаете это" },
                    new Option { Destination = 225, Text = "Откажетесь и посмотрите" },
                }
            },
            [318] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 193, Text = "Далее" },
                }
            },
            [319] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 31, Text = "Бродячий торговец" },
                    new Option { Destination = 439, Text = "Азартный игрок" },
                    new Option { Destination = 146, Text = "Собираетесь наняться в армию чародея" },
                }
            },
            [320] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 355, Text = "Рискнете предложить что-то в виде пропуска" },
                    new Option { Destination = 543, Text = "Драться" },
                }
            },
            [321] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 75, Text = "Попробуете наложить заклятие" },
                    new Option { Destination = 247, Text = "Сражаться" },
                }
            },
            [322] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 277, Text = "Прямо" },
                    new Option { Destination = 556, Text = "Направо" },
                }
            },
            [323] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 522, Text = "Замахнетесь" },
                    new Option { Destination = 137, Text = "Дадите денег" },
                    new Option { Destination = 194, Text = "Предложите подарок" },
                }
            },
            [324] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 589, Text = "Если волшебник жив" },
                    new Option { Destination = 617, Text = "Если же он мертв" },
                }
            },
            [325] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 162, Text = "Направо" },
                    new Option { Destination = 76, Text = "Налево" },
                }
            },
            [326] = new Paragraph
            {
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
                        new Character
                        {
                            Name = "ВОДЯНОЙ",
                            Mastery = 7,
                            Endurance = 7,
                        },
		    },
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 573, Text = "Если вы победили" },
                    new Option { Destination = 504, Text = "Покинуть таверну и бежать" },
                }
            },
            [327] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 443, Text = "Далее" },
                }
            },
            [328] = new Paragraph
            {
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
                        new Character
                        {
                            Name = "ПЕРВЫЙ ОРК",
                            Mastery = 8,
                            Endurance = 5,
                        },
			new Character
                        {
                            Name = "ВТОРОЙ ОРК",
                            Mastery = 7,
                            Endurance = 7,
                        },
		    },
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 505, Text = "Если убили обоих врагов" },
                    new Option { Destination = 248, Text = "Если они живы" },
                }
            },
            [329] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 33, Text = "Далее" },
                }
            },
            [330] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 532, Text = "Далее" },
                }
            },
            [331] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 305, Text = "Углубиться в лес" },
                    new Option { Destination = 195, Text = "Продолжать идти прямо" },
                }
            },
            [332] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 250, Text = "Прямо" },
                    new Option { Destination = 461, Text = "Налево" },
                }
            },
            [333] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 149, Text = "Ппоговорить с ним" },
                    new Option { Destination = 98, Text = "Пойти дальше по дорожке" },
                }
            },
            [334] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 99, Text = "Далее" },
                }
            },
            [335] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 46, Text = "Далее" },
                }
            },
            [336] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 24, Text = "Далее" },
                }
            },
            [337] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 613, Text = "Подкупить его" },
                    new Option { Destination = 249, Text = "Поговорить с ним" },
                    new Option { Destination = 65, Text = "Драться" },
                }
            },
            [338] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 151, Text = "Направо" },
                    new Option { Destination = 231, Text = "Прямо" },
                }
            },
            [339] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 123, Text = "Далее" },
                }
            },
            [340] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 509, Text = "Далее" },
                }
            },
            [341] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 164, Text = "Заклятие Силы" },
                    new Option { Destination = 289, Text = "Заклятие Слабости" },
                    new Option { Destination = 506, Text = "Заклятие Огня" },
                    new Option { Destination = 487, Text = "Драться" },
                }
            },
            [342] = new Paragraph
            {
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -3,
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 181, Text = "Далее" },
                }
            },
            [343] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 252, Text = "Прямо" },
                    new Option { Destination = 542, Text = "Направо" },
                }
            },
            [344] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 319, Text = "Прямо к воротам замка" },
                    new Option { Destination = 232, Text = "В обход" },
                }
            },
            [345] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 141, Text = "Далее" },
                }
            },
            [346] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 153, Text = "Воспользоваться заклятием" },
                    new Option { Destination = 68, Text = "Посмотреть, что будет дальше" },
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
                    new Option { Destination = 544, Text = "Далее" },
                }
            },
            [349] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 615, Text = "Пойдете за ней" },
                    new Option { Destination = 305, Text = "Пойдете по тропинке" },
                    new Option { Destination = 210, Text = "Вернетесь на дорогу" },
                }
            },
            [350] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 545, Text = "Взять серебряный сосуд" },
                    new Option { Destination = 253, Text = "Взять стеклянный сосуд" },
                    new Option { Destination = 39, Text = "Выйти в противоположную дверь " },
                }
            },
            [351] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 574, Text = "Принять предложение" },
                    new Option { Destination = 235, Text = "Отказаться и покинуть деревню на юг" },
                    new Option { Destination = 2, Text = "Отказаться и покинуть деревню на запад" },
                }
            },
            [352] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 322, Text = "Далее" },
                }
            },
            [353] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 154, Text = "По тропинке направо" },
                    new Option { Destination = 237, Text = "По тропинке налево" },
                    new Option { Destination = 181, Text = "По дороге прямо" },
                }
            },
            [354] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 327, Text = "Заплатить деньги" },
                    new Option { Destination = 518, Text = "Драться" },
                    new Option { Destination = 106, Text = "Уйти" },
                }
            },
            [355] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 233, Text = "Золотое кольцо" },
                    new Option { Destination = 69, Text = "Бронзовый свисток" },
                    new Option { Destination = 168, Text = "Четки" },
                    new Option { Destination = 543, Text = "Драться" },
                }
            },
            [356] = new Paragraph
            {
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
                        new Character
                        {
                            Name = "ТРОЛЛЬ",
                            Mastery = 9,
                            Endurance = 14,
                        },
		    },
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 207, Text = "Далее" },
                }
            },
            [357] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 316, Text = "Далее" },
                }
            },
            [358] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 510, Text = "Направо" },
                    new Option { Destination = 214, Text = "Налево" },
                    new Option { Destination = 38, Text = "Прямо" },
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
                    new Option { Destination = 2, Text = "Пойти по другой дороге" },
                    new Option { Destination = 184, Text = "Войти в деревню" },
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
                    new Option { Destination = 241, Text = "Далее" },
                }
            },
            [363] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 241, Text = "Далее" },
                }
            },
            [364] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 480, Text = "Далее" },
                }
            },
            [365] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 543, Text = "Далее" },
                }
            },
            [366] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 416, Text = "Обойти ее вокруг и поискать вход" },
                    new Option { Destination = 116, Text = "К заброшенному домику" },
                }
            },
            [367] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 44, Text = "Далее" },
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
                    new Option { Destination = 542, Text = "В эту дверь" },
                    new Option { Destination = 252, Text = "В ту, что прямо" },
                }
            },
            [370] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 538, Text = "Заклятие Огня" },
                    new Option { Destination = 68, Text = "Ждать" },
                }
            },
            [371] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 276, Text = "Далее" },
                }
            },
            [372] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 424, Text = "Далее" },
                }
            },
            [373] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 221, Text = "Войти в хижину" },
                    new Option { Destination = 121, Text = "Вернуться на дорогу" },
                }
            },
            [374] = new Paragraph
            {
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
                        new Character
                        {
                            Name = "ПЕРВЫЙ ЧЕЛОВЕК",
                            Mastery = 8,
                            Endurance = 5,
                        },
			new Character
                        {
                            Name = "ВТОРОЙ ЧЕЛОВЕК",
                            Mastery = 6,
                            Endurance = 9,
                        },
		    },
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 34, Text = "Далее" },
                }
            },
            [375] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 254, Text = "Заклятье Плавания" },
                    new Option { Destination = 508, Text = "Заклятье Левитации" },
                    new Option { Destination = 311, Text = "Попробовать проплыть по течению под замок" },
                    new Option { Destination = 424, Text = "К палаткам" },
                }
            },
            [376] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 155, Text = "Далее" },
                }
            },
            [377] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 197, Text = "Спрятаться" },
                    new Option { Destination = 309, Text = "Поговорить со всадником" },
                }
            },
            [378] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 415, Text = "Спрыгнуть" },
                    new Option { Destination = 129, Text = "Идти дальше" },
                }
            },
            [379] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 544, Text = "Далее" },
                }
            },
            [380] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 474, Text = "Посмотреть большой сундук" },
                    new Option { Destination = 540, Text = "Посмотреть средний сундук" },
                    new Option { Destination = 39, Text = "Вернуться в большую залу" },
                }
            },
            [381] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 84, Text = "К саркофагу со статуей" },
                    new Option { Destination = 281, Text = "К саркофагу с крестом" },
                    new Option { Destination = 496, Text = "К могиле" },
                    new Option { Destination = 547, Text = "Уйти через правую дверь" },
                    new Option { Destination = 501, Text = "Уйти через левую дверь" },
                }
            },
            [382] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 575, Text = "Хотите пойти наверх" },
                    new Option { Destination = 511, Text = "Хотите пойти вниз" },
                    new Option { Destination = 325, Text = "Выйти из гарема" },
                }
            },
            [383] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 576, Text = "Пойти за ним" },
                    new Option { Destination = 353, Text = "Вернуться и выбрать" },
                }
            },
            [384] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 555, Text = "Далее" },
                }
            },
            [385] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 502, Text = "Далее" },
                }
            },
            [386] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 572, Text = "Далее" },
                }
            },
            [387] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 241, Text = "Далее" },
                }
            },
            [388] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 577, Text = "Выхватить меч" },
                    new Option { Destination = 293, Text = "Стоять и смотреть" },
                }
            },
            [389] = new Paragraph
            {
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -3,
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 300, Text = "Далее" },
                }
            },
            [390] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 569, Text = "Далее" },
                }
            },
            [391] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 502, Text = "Далее" },
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
                    new Option { Destination = 291, Text = "Есть медный ключик" },
                    new Option { Destination = 337, Text = "Попробуете открыть дверь за вами" },
                    new Option { Destination = 595, Text = "Попробуете открыть дверь слева" },
                }
            },
            [394] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 294, Text = "Бронзовый свисток" },
                    new Option { Destination = 596, Text = "Зеркальце" },
                    new Option { Destination = 171, Text = "Флакончик духов" },
                    new Option { Destination = 93, Text = "Сражаться" },
                }
            },
            [395] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 567, Text = "Далее" },
                }
            },
            [396] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 100, Text = "Далее" },
                }
            },
            [397] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 130, Text = "В переднюю дверь" },
                    new Option { Destination = 512, Text = "В правую дверь" },
                    new Option { Destination = 315, Text = "Поговорить" },
                }
            },
            [398] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 39, Text = "Далее" },
                }
            },
            [399] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Пройдете, ничего не сказав" },
                    new Option { Destination = 255, Text = "Попытаетесь поговорить" },
                }
            },
            [400] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 514, Text = "Пойдете, куда указывает стрелка" },
                    new Option { Destination = 35, Text = "Не будете сворачивать" },
                }
            },
            [401] = new Paragraph
            {
		Action = new Actions
                {
                    ActionName = "Luck",
                    ButtonName = "Проверить удачу",
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 546, Text = "Удачлив" },
                    new Option { Destination = 77, Text = "Нет " },
                }
            },
            [402] = new Paragraph
            {
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -1,
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 547, Text = "В правую" },
                    new Option { Destination = 501, Text = "В левую" },
                }
            },
            [403] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 199, Text = "Пойдете к таверне" },
                    new Option { Destination = 244, Text = "Свернете налево" },
                }
            },
            [404] = new Paragraph
            {
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -3,
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 183, Text = "Выхватить меч" },
                    new Option { Destination = 36, Text = "Заклятие Силы" },
                    new Option { Destination = 334, Text = "Заклятие Слабости" },
                    new Option { Destination = 7, Text = "Заклятие Копии" },
                    new Option { Destination = 112, Text = "Заклятие Огня" },
                }
            },
            [405] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 307, Text = "Наполнить флягу" },
                    new Option { Destination = 261, Text = "Отправиться дальше" },
                }
            },
            [406] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 98, Text = "Прямо" },
                    new Option { Destination = 515, Text = "К ульям" },
                }
            },
            [407] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 181, Text = "Далее" },
                }
            },
            [408] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 131, Text = "Выпьете" },
                    new Option { Destination = 305, Text = "Уйдете по тропинке" },
                    new Option { Destination = 210, Text = "Вернетесь на дорогу" },
                }
            },
            [409] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 200, Text = "Заклятие Иллюзии" },
                    new Option { Destination = 114, Text = "Заклятие Огня" },
                    new Option { Destination = 56, Text = "Ннет ни того, ни другого" },
                }
            },
            [410] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 54, Text = "Далее" },
                }
            },
            [411] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 578, Text = "Попробовать овощи" },
                    new Option { Destination = 80, Text = "Покинете комнату" },
                }
            },
            [412] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 316, Text = "Биться" },
                    new Option { Destination = 37, Text = "Заклятье Огня" },
                    new Option { Destination = 357, Text = "Заклятье Силы" },
                    new Option { Destination = 256, Text = "Заклятье Слабости" },
                    new Option { Destination = 516, Text = "Предложить подарок" },
                }
            },
            [413] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 132, Text = "Далее" },
                }
            },
            [414] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 338, Text = "Далее" },
                }
            },
            [415] = new Paragraph
            {
		Action = new Actions
                {
                    ActionName = "Luck",
                    ButtonName = "Проверить удачу",
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 57, Text = "Удачлив" },
                    new Option { Destination = 579, Text = "Нет" },
                }
            },
            [416] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 424, Text = "Попытаться разузнать что-нибудь о замке" },
                    new Option { Destination = 257, Text = "Попробовать войти" },
                }
            },
            [417] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 326, Text = "Далее" },
                }
            },
            [418] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 179, Text = "Далее" },
                }
            },
            [419] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 104, Text = "Дадите им поесть" },
                    new Option { Destination = 374, Text = "Откажете" },
                }
            },
            [420] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 38, Text = "Направо" },
                    new Option { Destination = 214, Text = "Прямо" },
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
                    new Option { Destination = 317, Text = "У вас есть кольцо" },
                    new Option { Destination = 133, Text = "У вас нет кольца" },
                }
            },
            [423] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 58, Text = "Заклятье Силы" },
                    new Option { Destination = 580, Text = "Заклятье Слабости" },
                    new Option { Destination = 290, Text = "Заклятье Огня" },
                    new Option { Destination = 40, Text = "Драться с ними" },
                }
            },
            [424] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 582, Text = "Поговорить с Орками" },
                    new Option { Destination = 74, Text = "Попытаться проникнуть в сердце замка" },
                }
            },
            [425] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 614, Text = "Наложить еще одно заклятие Левитации или Плавания" },
                    new Option { Destination = 581, Text = "Упасть в воду" },
                }
            },
            [426] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 123, Text = "Далее" },
                }
            },
            [427] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 583, Text = "Золотой свисток" },
                    new Option { Destination = 597, Text = "Выломать ее" },
                    new Option { Destination = 398, Text = "Выйти в правую дверь" },
                    new Option { Destination = 206, Text = "Выйти в левую дверь" },
                    new Option { Destination = 350, Text = "Выйти в среднюю дверь" },
                }
            },
            [428] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 499, Text = "Заклятье Огня" },
                    new Option { Destination = 85, Text = "Заклятье Силы" },
                    new Option { Destination = 390, Text = "Заклятье Слабости" },
                    new Option { Destination = 569, Text = "Сражаться мечом" },
                }
            },
            [429] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 301, Text = "Далее" },
                }
            },
            [430] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 400, Text = "Далее" },
                }
            },
            [431] = new Paragraph
            {
		Action = new Actions
                {
                    ActionName = "Luck",
                    ButtonName = "Проверить удачу",
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 59, Text = "Удачлив" },
		    new Option { Destination = 0, Text = "Нет" },
                }
            },
            [432] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 421, Text = "Cвернуть" },
                    new Option { Destination = 73, Text = "Идти прямо" },
                }
            },
            [433] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 500, Text = "Выпить ее" },
                    new Option { Destination = 416, Text = "Уйти" },
                }
            },
            [434] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 584, Text = "Налево" },
                    new Option { Destination = 60, Text = "Направо" },
                }
            },
            [435] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 544, Text = "Далее" },
                }
            },
            [436] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 338, Text = "Направо" },
                }
            },
            [437] = new Paragraph
            {
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -2,
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 531, Text = "Направо" },
                    new Option { Destination = 72, Text = "Налево" },
                }
            },
            [438] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 106, Text = "Далее" },
                }
            },
            [439] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 259, Text = "В карты" },
                    new Option { Destination = 585, Text = "В кости" },
                }
            },
            [440] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 348, Text = "Направо" },
                    new Option { Destination = 61, Text = "Налево" },
                }
            },
            [441] = new Paragraph
            {
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -3,
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 378, Text = "Далее" },
                }
            },
            [442] = new Paragraph
            {
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -2,
                },
		    
                Options = new List<Option>
                {
                    new Option { Destination = 119, Text = "Далее" },
                }
            },
            [443] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 106, Text = "Далее" },
                }
            },
            [444] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 502, Text = "Далее" },
                }
            },
            [445] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 102, Text = "Далее" },
                }
            },
            [446] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 328, Text = "Далее" },
                }
            },
            [447] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 62, Text = "Исцелить медвежонка" },
                    new Option { Destination = 529, Text = "Драться с медведицей" },
                }
            },
            [448] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 235, Text = "На юг" },
                    new Option { Destination = 2, Text = "На запад" },
                }
            },
            [449] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 396, Text = "В карты" },
                    new Option { Destination = 271, Text = "В кости" },
                }
            },
            [450] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 292, Text = "Налево" },
                    new Option { Destination = 51, Text = "Прямо" },
                    new Option { Destination = 586, Text = "Направо" },
                }
            },
            [451] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 454, Text = "Далее" },
                }
            },
            [452] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 10, Text = "Далее" },
                }
            },
            [453] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 46, Text = "Далее" },
                }
            },
            [454] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 319, Text = "Прямо к воротам" },
                    new Option { Destination = 232, Text = "В обход замка" },
                }
            },
            [455] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 141, Text = "Далее" },
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
                    new Option { Destination = 44, Text = "Далее" },
                }
            },
            [458] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 252, Text = "В противоположную от входа" },
                    new Option { Destination = 542, Text = "В ту, что справа" },
                }
            },
            [459] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 399, Text = "Вернитесь" },
                    new Option { Destination = 325, Text = "Дальше по коридору" },
                    new Option { Destination = 255, Text = "Поговорите с женщинами" },
                }
            },
            [460] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 587, Text = "Сделаете это" },
                    new Option { Destination = 537, Text = "Дальше направо" },
                    new Option { Destination = 348, Text = "Дальше налево" },
                }
            },
            [461] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 340, Text = "Искать убежище на ночь" },
                    new Option { Destination = 442, Text = "Продолжите путь" },
                }
            },
            [462] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 178, Text = "Попытаться поймать" },
                    new Option { Destination = 333, Text = "Продолжить путь" },
                }
            },
            [463] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 158, Text = "Войти в сторожку" },
                    new Option { Destination = 308, Text = "К высокому зданию в центре двора" },
                }
            },
            [464] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 600, Text = "Согласны" },
                    new Option { Destination = 55, Text = "Уйти" },
                }
            },
            [465] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 140, Text = "Далее" },
                }
            },
            [466] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 352, Text = "Далее" },
                }
            },
            [467] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 182, Text = "Далее" },
                }
            },
            [468] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 39, Text = "Далее" },
                }
            },
            [469] = new Paragraph
            {
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -2,
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 257, Text = "Далее" },
                }
            },
            [470] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 221, Text = "Войте внутрь" },
                    new Option { Destination = 55, Text = "Пройти мимо" },
                }
            },
            [471] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 80, Text = "Далее" },
                }
            },
            [472] = new Paragraph
            {
		Action = new Actions
                {
                    ActionName = "Luck",
                    ButtonName = "Проверить удачу",
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 588, Text = "Удачлив" },
                    new Option { Destination = 391, Text = "Нет" },
                }
            },
            [473] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 416, Text = "Далее" },
                }
            },
            [474] = new Paragraph
            {
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -2,
                },
		
		// -1 Mastery
		
                Options = new List<Option>
                {
                    new Option { Destination = 159, Text = "Еще раз попробовать" },
                    new Option { Destination = 540, Text = "Заняться средним сундуком" },
                    new Option { Destination = 380, Text = "Заняться маленьким сундуком" },
                    new Option { Destination = 39, Text = "Лучше уйти" },
                }
            },
            [475] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 617, Text = "Далее" },
                }
            },
            [476] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 347, Text = "Далее" },
                }
            },
            [477] = new Paragraph
            {
		Modification = new Modification
                {
                    Name = "Luck",
                    Value = -1,
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 106, Text = "Далее" },
                }
            },
            [478] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 320, Text = "Пойдете по нему" },
                    new Option { Destination = 534, Text = "Вернетесь" },
                }
            },
            [479] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 590, Text = "Откроете крышку" },
                    new Option { Destination = 392, Text = "Идти дальше" },
                }
            },
            [480] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 45, Text = "О Зеленых рыцарях" },
                    new Option { Destination = 345, Text = "О самом волшебнике" },
                    new Option { Destination = 208, Text = "О Принцессе" },
                    new Option { Destination = 591, Text = "Уйти из библиотеки" },
                }
            },
            [481] = new Paragraph
            {
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -2,
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 60, Text = "Далее" },
                }
            },
            [482] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 241, Text = "Далее" },
                }
            },
            [483] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 39, Text = "Вернуться обратно" },
                    new Option { Destination = 566, Text = "В окно немного ниже" },
		    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [484] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 308, Text = "Далее" },
                }
            },
            [485] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 472, Text = "Справа" },
                    new Option { Destination = 275, Text = "Слева" },
                }
            },
            [486] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 570, Text = "Спросить про сундук" },
                    new Option { Destination = 91, Text = "Попросить накормить вас" },
                    new Option { Destination = 295, Text = "Расспросить о замке" },
                }
            },
            [487] = new Paragraph
            {
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
                        new Character
                        {
                            Name = "ПЕРВЫЙ ОРК",
                            Mastery = 10,
                            Endurance = 6,
                        },
			new Character
                        {
                            Name = "ВТОРОЙ ОРК",
                            Mastery = 7,
                            Endurance = 7,
                        },
			new Character
                        {
                            Name = "ТРЕТИЙ ОРК",
                            Mastery = 7,
                            Endurance = 7,
                        },
		    },
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 100, Text = "Далее" },
                }
            },
            [488] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 454, Text = "Далее" },
                }
            },
            [489] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 501, Text = "Правую" },
                    new Option { Destination = 547, Text = "Левую" },
                }
            },
            [490] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 255, Text = "Далее" },
                }
            },
            [491] = new Paragraph
            {
		Action = new Actions
                {
                    ActionName = "Luck",
                    ButtonName = "Проверить удачу",
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 571, Text = "Удачлив" },
                    new Option { Destination = 593, Text = "Нет" },
                }
            },
            [492] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 151, Text = "Далее" },
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
                    new Option { Destination = 325, Text = "Далее" },
                }
            },
            [496] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 402, Text = "Взять копье" },
                    new Option { Destination = 547, Text = "Выйти через правую" },
                    new Option { Destination = 501, Text = "Выйти через левую" },
                }
            },
            [497] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 572, Text = "Далее" },
                }
            },
            [498] = new Paragraph
            {
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
			new Character
                        {
                            Name = "ОРК",
                            Mastery = 7,
                            Endurance = 7,
                        },
		    },
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 300, Text = "Далее" },
                }
            },
            [499] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 48, Text = "Далее" },
                }
            },
            [500] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 416, Text = "Далее" },
                }
            },
            [501] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 534, Text = "Далее" },
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
                    new Option { Destination = 125, Text = "Далее" },
                }
            },
            [504] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 332, Text = "Далее" },
                }
            },
            [505] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 337, Text = "Ту, что перед вами" },
                    new Option { Destination = 393, Text = "Ту, что справа от вас" },
                    new Option { Destination = 595, Text = "Ту, что слева" },
                }
            },
            [506] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 100, Text = "Далее" },
                }
            },
            [507] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 598, Text = "Пойти по ней" },
                    new Option { Destination = 501, Text = "Заглянуть за левую дверь" },
                }
            },
            [508] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 366, Text = "Далее" },
                }
            },
            [509] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 119, Text = "Далее" },
                }
            },
            [510] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 38, Text = "Нправо" },
                    new Option { Destination = 214, Text = "Прямо" },
                }
            },
            [511] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 544, Text = "Далее" },
                }
            },
            [512] = new Paragraph
            {
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -2,
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 409, Text = "Далее" },
                }
            },
            [513] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 41, Text = "Заклятье Огня" },
                    new Option { Destination = 476, Text = "Заклятье Силы" },
                    new Option { Destination = 172, Text = "Заклятье Слабости" },
                    new Option { Destination = 347, Text = "Продолжать драться" },
                    new Option { Destination = 601, Text = "Попробовать убежать" },
                }
            },
            [514] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 358, Text = "Отдохнуть" },
                    new Option { Destination = 441, Text = "Пойти дальше" },
                }
            },
            [515] = new Paragraph
            {
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -8,
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 98, Text = "Далее" },
                }
            },
            [516] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 134, Text = "Коготь дракона" },
                    new Option { Destination = 42, Text = "Бриллиант" },
                    new Option { Destination = 161, Text = "Гребень" },
                    new Option { Destination = 488, Text = "Перо аиста" },
                    new Option { Destination = 316, Text = "Драться" },
                }
            },
            [517] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 359, Text = "Подождать" },
                    new Option { Destination = 105, Text = "Заклятье Левитации" },
                }
            },
            [518] = new Paragraph
            {
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
			new Character
                        {
                            Name = "ТОРГОВЕЦ",
                            Mastery = 6,
                            Endurance = 12,
                        },
		    },
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 477, Text = "Далее" },
                }
            },
            [519] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 278, Text = "Как пройти к Принцессе" },
                    new Option { Destination = 43, Text = "Как пройти к Барладу Дэрту" },
                    new Option { Destination = 136, Text = "Как пройти к Начальнику стражи" },
                }
            },
            [520] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 3, Text = "Далее" },
                }
            },
            [521] = new Paragraph
            {
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -2,
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 326, Text = "Далее" },
                }
            },
            [522] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 252, Text = "В дверь, которая перед вами" },
                    new Option { Destination = 542, Text = "В ту дверь, что справа" },
                }
            },
            [523] = new Paragraph
            {
                Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -6,
                },

                Options = new List<Option>
                {
                    new Option { Destination = 39, Text = "Далее" },
                    new Option { Destination = 523, Text = "ЕЩЁ РАЗ" },
                    new Option { Destination = 523, Text = "ЕЩЁ РАЗ РАЗ РАЗ" },
                }
            },
            [524] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 89, Text = "Тропинка прямо" },
                    new Option { Destination = 616, Text = "Дорога направо" },
                }
            },
            [525] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 349, Text = "Есть банан" },
                    new Option { Destination = 215, Text = "Драться с ней" },
                }
            },
            [526] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 235, Text = "На юг" },
                    new Option { Destination = 2, Text = "На запад" },
                }
            },
            [527] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 182, Text = "Далее" },
                }
            },
            [528] = new Paragraph
            {
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
			new Character
                        {
                            Name = "ОБОРОТЕНЬ",
                            Mastery = 10,
                            Endurance = 10,
                        },
		    },
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 240, Text = "Далее" },
                }
            },
            [529] = new Paragraph
            {
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
			new Character
                        {
                            Name = "МЕДВЕДИЦА",
                            Mastery = 8,
                            Endurance = 10,
                        },
		    },
                },
		    
                Options = new List<Option>
                {
                    new Option { Destination = 44, Text = "Далее" },
                }
            },
            [530] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 409, Text = "Далее" },
                }
            },
            [531] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 49, Text = "Прямо" },
                    new Option { Destination = 338, Text = "Налево" },
                }
            },
            [532] = new Paragraph
            {
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
			new Character
                        {
                            Name = "ПЕРВЫЙ ГОБЛИН",
                            Mastery = 8,
                            Endurance = 10,
                        },
			new Character
                        {
                            Name = "ВТОРОЙ ГОБЛИН",
                            Mastery = 6,
                            Endurance = 8,
                        },
		    },
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 323, Text = "Далее" },
                }
            },
            [533] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 137, Text = "Попробовать откупиться" },
                    new Option { Destination = 522, Text = "Драться" },
                }
            },
            [534] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 71, Text = "Есть белая стрела" },
                    new Option { Destination = 362, Text = "Есть бляха с золотым орлом" },
                    new Option { Destination = 478, Text = "Войти в правую дверь" },
                    new Option { Destination = 603, Text = "Войти левую дверь" },
                    new Option { Destination = 282, Text = "Войти в среднюю дверь" },
                }
            },
            [535] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 55, Text = "Далее" },
                }
            },
            [536] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 363, Text = "Есть Оберег" },
                    new Option { Destination = 479, Text = "Есть серебряный сосуд" },
                    new Option { Destination = 283, Text = "Подумать" },
                    new Option { Destination = 567, Text = "Атаковать" },
                }
            },
            [537] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 140, Text = "Далее" },
                }
            },
            [538] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 370, Text = "Заклятье Иллюзии" },
                    new Option { Destination = 68, Text = "Спокойно ждать" },
                }
            },
            [539] = new Paragraph
            {
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
			new Character
                        {
                            Name = "ГОБЛИН",
                            Mastery = 4,
                            Endurance = 7,
                        },
			new Character
                        {
                            Name = "ВТОРОЙ ГОБЛИН",
                            Mastery = 8,
                            Endurance = 9,
                        },
		    },
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 484, Text = "Зайти в сторожку" },
                    new Option { Destination = 308, Text = "К зданию в центре двора" },
                }
            },
            [540] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 284, Text = "Есть фигурный ключ" },
                    new Option { Destination = 474, Text = "Заняться большим сундуком" },
                    new Option { Destination = 380, Text = "Заняться маленьким сундуком" },
                    new Option { Destination = 39, Text = "Вернуться в залу" },
                }
            },
            [541] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 364, Text = "Есть меч Зеленого рыцаря" },
                    new Option { Destination = 494, Text = "Нет меча" },
                }
            },
            [542] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 411, Text = "Мясо" },
                    new Option { Destination = 578, Text = "Овощи" },
                    new Option { Destination = 80, Text = "Выходите через дверь" },
                }
            },
            [543] = new Paragraph
            {
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
			new Character
                        {
                            Name = "ЗЕЛЕНЫЙ РЫЦАРЬ",
                            Mastery = 11,
                            Endurance = 14,
                        },
		    },
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 365, Text = "Использовать аклятие" },
                    new Option { Destination = 241, Text = "По коридору дальше" },
                }
            },
            [544] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 285, Text = "Правую" },
                    new Option { Destination = 489, Text = "Левую" },
                }
            },
            [545] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 253, Text = "Взять стеклянный сосуд" },
                    new Option { Destination = 39, Text = "Выйти из комнаты" },
                }
            },
            [546] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 181, Text = "Далее" },
                }
            },
            [547] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 501, Text = "Далее" },
                }
            },
            [548] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 297, Text = "Погладить" },
                    new Option { Destination = 416, Text = "Уйти" },
                }
            },
            [549] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 285, Text = "Правую" },
                    new Option { Destination = 489, Text = "Левую" },
                }
            },
            [550] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 607, Text = "Далее" },
                }
            },
            [551] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 92, Text = "Зеркальце" },
                    new Option { Destination = 296, Text = "Гребень" },
                    new Option { Destination = 490, Text = "Оберег" },
                    new Option { Destination = 255, Text = "Придумать что-то другое" },
                }
            },
            [552] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 323, Text = "Далее" },
                }
            },
            [553] = new Paragraph
            {
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
			new Character
                        {
                            Name = "ГАРПИЯ",
                            Mastery = 10,
                            Endurance = 12,
                        },
		    },
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 388, Text = "Далее" },
                }
            },
            [554] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 301, Text = "Далее" },
                }
            },
            [555] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 450, Text = "Далее" },
                }
            },
            [556] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 394, Text = "Попробовать откупиться" },
                    new Option { Destination = 93, Text = "Достать меч" },
                }
            },
            [557] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 120, Text = "Осмотреть домик" },
                    new Option { Destination = 416, Text = "Уйти" },
                }
            },
            [558] = new Paragraph
            {
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
			new Character
                        {
                            Name = "ГИГАНТСКИЙ ПАУК",
                            Mastery = 8,
                            Endurance = 8,
                        },
		    },
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 189, Text = "Далее" },
                }
            },
            [559] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 184, Text = "Идти в деревню" },
                    new Option { Destination = 13, Text = "Пойти по тропинке" },
                }
            },
            [560] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 165, Text = "Зеркало" },
                    new Option { Destination = 288, Text = "Карты на столе" },
                    new Option { Destination = 493, Text = "Если вы уже осмотрели и то, и другое" },
                }
            },
            [561] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 192, Text = "Далее" },
                }
            },
            [562] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 613, Text = "Предложить ему деньги" },
                    new Option { Destination = 65, Text = "Напасть" },
                }
            },
            [563] = new Paragraph
            {
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
			new Character
                        {
                            Name = "ЛЕСОВИЧОК",
                            Mastery = 6,
                            Endurance = 8,
                        },
		    },
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 450, Text = "Далее" },
                }
            },
            [564] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 480, Text = "Далее" },
                }
            },
            [565] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 94, Text = "Щит тьмы" },
                    new Option { Destination = 469, Text = "Демоны и сила" },
                    new Option { Destination = 298, Text = "Меч и верность" },
                }
            },
            [566] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 556, Text = "В правую" },
                    new Option { Destination = 277, Text = "В ту, что напротив" },
                    new Option { Destination = 483, Text = "Заклятье Левитации" },
                    new Option { Destination = 39, Text = "Вернуться в залу" },
                }
            },
            [567] = new Paragraph
            {
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
			new Character
                        {
                            Name = "ЗЕЛЕНЫЙ РЫЦАРЬ",
                            Mastery = 10,
                            Endurance = 10,
                        },
		    },
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 395, Text = "Заклятье Силы" },
                    new Option { Destination = 299, Text = "Заклятье Слабости" },
                    new Option { Destination = 96, Text = "Заклятье Огня" },
                    new Option { Destination = 241, Text = "Если вы победили врага" },
                }
            },
            [568] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 300, Text = "Далее" },
                }
            },
            [569] = new Paragraph
            {
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
			new Character
                        {
                            Name = "ПЕРВЫЙ ОРК",
                            Mastery = 10,
                            Endurance = 6,
                        },
			new Character
                        {
                            Name = "ВТОРОЙ ОРК",
                            Mastery = 7,
                            Endurance = 7,
                        },
			new Character
                        {
                            Name = "ТРЕТИЙ ОРК",
                            Mastery = 7,
                            Endurance = 7,
                        },
		    },
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 48, Text = "Далее" },
                }
            },
            [570] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 604, Text = "Далее" },
                }
            },
            [571] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 76, Text = "Далее" },
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
                    new Option { Destination = 561, Text = "Попробовать переплыть" },
                    new Option { Destination = 332, Text = "Пойдете по дороге" },
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
                    new Option { Destination = 323, Text = "Далее" },
                }
            },
            [576] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 559, Text = "Направо" },
                    new Option { Destination = 181, Text = "Налево" },
                    new Option { Destination = 445, Text = "Прямо" },
                }
            },
            [577] = new Paragraph
            {
		Modification = new Modification
                {
                    Name = "Mastery",
                    Value = -1,
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 293, Text = "Далее" },
                }
            },
            [578] = new Paragraph
            {
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -2,
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 411, Text = "Попробовать мясо" },
                    new Option { Destination = 80, Text = "Уйти" },
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
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
			new Character
                        {
                            Name = "ПЕРВЫЙ ГОБЛИН",
                            Mastery = 4,
                            Endurance = 9,
                        },
			new Character
                        {
                            Name = "ВТОРОЙ ГОБЛИН",
                            Mastery = 7,
                            Endurance = 5,
                        },
		    },
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 118, Text = "Далее" },
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
                    new Option { Destination = 74, Text = "Ко входу в центр цитадели" },
                    new Option { Destination = 174, Text = "Поговорите с ними еще" },
                }
            },
            [583] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 607, Text = "По лестнице вверх" },
                    new Option { Destination = 398, Text = "В правую дверь" },
                    new Option { Destination = 206, Text = "В левую дверь" },
                    new Option { Destination = 350, Text = "В среднюю дверь" },
                }
            },
            [584] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 616, Text = "Прямо" },
                    new Option { Destination = 89, Text = "Налево" },
                }
            },
            [585] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 428, Text = "Далее" },
                }
            },
            [586] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 608, Text = "Поговорите с ними" },
                    new Option { Destination = 6, Text = "Расчистить путь мечом" },
                }
            },
            [587] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 348, Text = "Свернуть налево" },
                    new Option { Destination = 537, Text = "Вернуться направо" },
                }
            },
            [588] = new Paragraph
            {
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -6,
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 275, Text = "Далее" },
                }
            },
            [589] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 266, Text = "Далее" },
                }
            },
            [590] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 241, Text = "Далее" },
                }
            },
            [591] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 139, Text = "Далее" },
                }
            },
            [592] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 323, Text = "Далее" },
                }
            },
            [593] = new Paragraph
            {
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -3,
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 409, Text = "Далее" },
                }
            },
            [594] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 553, Text = "Далее" },
                }
            },
            [595] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 610, Text = "Дверь сломана" },
                    new Option { Destination = 393, Text = "Попробовать открыть дверь за вашей спиной" },
                    new Option { Destination = 337, Text = "Попробовать открыть дверь справа" },
                }
            },
            [596] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 93, Text = "Далее" },
                }
            },
            [597] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 350, Text = "Прямо" },
                    new Option { Destination = 398, Text = "Справа" },
                    new Option { Destination = 206, Text = "Слева" },
                }
            },
            [598] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 501, Text = "Далее" },
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
                    new Option { Destination = 55, Text = "Далее" },
                }
            },
            [601] = new Paragraph
            {
		Modification = new Modification
                {
                    Name = "Endurance",
                    Value = -6,
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 232, Text = "Далее" },
                }
            },
            [602] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 169, Text = "Далее" },
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
		Action = new Actions
                {
                    ActionName = "Fight",
                    ButtonName = "Сражаться",

                    Enemies = new List<Character>
                    {
			new Character
                        {
                            Name = "ПОВАР",
                            Mastery = 8,
                            Endurance = 10,
                        },
		    },
                },
		
                Options = new List<Option>
                {
                    new Option { Destination = 173, Text = "Заглянуть в сундук" },
                    new Option { Destination = 379, Text = "Уйти" },
                }
            },
            [605] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 169, Text = "Далее" },
                }
            },
            [606] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 594, Text = "Правую" },
                    new Option { Destination = 599, Text = "Левую" },
                }
            },
            [607] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 323, Text = "Далее" },
                }
            },
            [608] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 214, Text = "Прямо" },
                    new Option { Destination = 38, Text = "Направо" },
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
                    new Option { Destination = 607, Text = "Подняться по ней" },
                    new Option { Destination = 393, Text = "Попробуете открыть дверь за вашей спиной" },
                    new Option { Destination = 337, Text = "Попробуете открыть дверь справа от вас" },
                }
            },
            [611] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 128, Text = "Красного вина" },
                    new Option { Destination = 32, Text = "Белого вина" },
                    new Option { Destination = 226, Text = "Эля" },
                    new Option { Destination = 356, Text = "Драться" },
                }
            },
            [612] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 196, Text = "С рубином" },
                    new Option { Destination = 258, Text = "И изумрудом" },
                    new Option { Destination = 169, Text = "Осмотреть кабинет" },
                }
            },
            [613] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 246, Text = "Согласиться" },
                    new Option { Destination = 65, Text = "Драться" },
                }
            },
            [614] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 192, Text = "Далее" },
                }
            },
            [615] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 232, Text = "Далее" },
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
            [618] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Далее" },
                }
            },
        };
    }
}
