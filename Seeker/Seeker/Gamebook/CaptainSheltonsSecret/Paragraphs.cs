using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Seeker.Game;

namespace Seeker.Gamebook.CaptainSheltonsSecret
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
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "РАСТЕНИЕ-УБИЙЦА",
                                Skill = 8,
                                Strength = 14,
                            },
                        },
                    },
                },

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
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "МОРСКОЙ ДРАКОНЧИК",
                                Skill = 9,
                                Strength = 8,
                                SeaArmour = true,
                            },
                        },
                    },
                },

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
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Пусть сражаются",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            new Character
                            {
                                Name = "РЫБА-ЕЖ",
                                Skill = 7,
                                Strength = 12,
                            },
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЯТНИСТАЯ АКУЛА",
                                Skill = 10,
                                Strength = 10,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 448, Text = "Рыба-еж победила" },
                    new Option { Destination = 268, Text = "Она мертва" },
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
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "БРЫЗГУН",
                                Skill = 8,
                                Strength = 9,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 326, Text = "Далее" },
                }
            },
            [33] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "АКУЛА",
                                Skill = 10,
                                Strength = 16,
                            },
                        },
                    },
                },

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
                    new Option { Destination = 176, Text = "Нагонать корвет" },
                    new Option { Destination = 4, Text = "Продолжить путь" },
                }
            },
            [52] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 241, Text = "Свернете в него" },
                    new Option { Destination = 160, Text = "Поплывете дальше" },
                }
            },
            [53] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 532, Text = "К невысокой скале" },
                    new Option { Destination = 404, Text = "К выходу из подводного леса" },
                }
            },
            [54] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 190, Text = "Правую стену" },
                    new Option { Destination = 528, Text = "Стену перед вами" },
                    new Option { Destination = 10, Text = "Повернете обратно" },
                }
            },
            [55] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 270, Text = "Удачливы" },
                    new Option { Destination = 538, Text = "Нет" },
                }
            },
            [56] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 382, Text = "Далее" },
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
                    new Option { Destination = 437, Text = "Далее" },
                }
            },
            [59] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 15, Text = "Далее" },
                }
            },
            [60] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Пусть сражаются",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            new Character
                            {
                                Name = "КАПИТАН",
                                Skill = 10,
                                Strength = 14,
                            },
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },

                        Aftertext = "Вторая парая:",
                    },
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВТОРОЙ ПИРАТ",
                                Skill = 9,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ТРЕТИЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },

                        Aftertext = "Если вы убили его",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 140, Text = "Победил капитан" },
                    new Option { Destination = 488, Text = "Они оба мертвы" },
                    new Option { Destination = 334, Text = "Остался только один" },
                    new Option { Destination = 198, Text = "Все враги мертвы, а капитан пал в бою" },
                }
            },
            [61] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 324, Text = "Далее" },
                }
            },
            [62] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 402, Text = "Далее" },
                }
            },
            [63] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ВОИН",
                                Skill = 10,
                                Strength = 10,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ ВОИН",
                                Skill = 10,
                                Strength = 10,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 217, Text = "Далее" },
                }
            },
            [64] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 489, Text = "Спуститься и посмотреть" },
                    new Option { Destination = 126, Text = "Поплывете дальше" },
                }
            },
            [65] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 214, Text = "Далее" },
                }
            },
            [66] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            new Character
                            {
                                Name = "НОСОРОГ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ ПИРАТ",
                                Skill = 9,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ТРЕТИЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 148, Text = "Помочь ему" },
                    new Option { Destination = 198, Text = "Если союзник перебил всех пиратов" },
                    new Option { Destination = 583, Text = "Сражаться придется вам" },
                }
            },
            [67] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 123, Text = "Выбрать остров" },
                }
            },
            [68] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 611, Text = "Далее" },
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
                    new Option { Destination = 399, Text = "Далее" },
                }
            },
            [71] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "РЫЦАРЬ-ВОДЯНОЙ",
                                Skill = 9,
                                Strength = 12,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 616, Text = "С вами золотой щит" },
                    new Option { Destination = 422, Text = "Убили его" },
                }
            },
            [72] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 256, Text = "Удачливы" },
                    new Option { Destination = 383, Text = "Нет" },
                }
            },
            [73] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "КОНДОР",
                                Skill = 9,
                                Strength = 9,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 429, Text = "Далее" },
                }
            },
            [74] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 338, Text = "Надеть пояс" },
                    new Option { Destination = 584, Text = "Взять раковину" },
                    new Option { Destination = 508, Text = "Уплыть прочь" },
                }
            },
            [75] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 145, Text = "Примете приглашение" },
                    new Option { Destination = 516, Text = "Поплывете дальше сами" },
                }
            },
            [76] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 569, Text = "Далее" },
                }
            },
            [77] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 241, Text = "Победили вы" },
                    new Option { Destination = 160, Text = "Плыть прямо" },
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
                    new Option { Destination = 269, Text = "Далее" },
                }
            },
            [80] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Пусть сражаются",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            new Character
                            {
                                Name = "МОРЯК",
                                Skill = 10,
                                Strength = 12,
                            },
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },

                        Aftertext = "И вторая группа, в которой сражаетесь вы:",
                    },
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВТОРОЙ ПИРАТ",
                                Skill = 9,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ТРЕТИЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },

                        Aftertext = "Если вы убили своих противников, а моряк еще нет, то можете помочь ему. Порядок боя определите сами: либо кидать кубики сначала за одну группу, потом за другую, либо попеременно",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 589, Text = "Моряк победил" },
                    new Option { Destination = 625, Text = "В живых остался только один" },
                    new Option { Destination = 198, Text = "Все враги мертвы" },
                }
            },
            [81] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 328, Text = "Далее" },
                }
            },
            [82] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 595, Text = "Подплыть подкрепиться" },
                    new Option { Destination = 214, Text = "Проплывете мимо" },
                }
            },
            [83] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 400, Text = "Пойдете искать Джона" },
                    new Option { Destination = 524, Text = "Вернуться на свою шхуну" },
                }
            },
            [84] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 157, Text = "Далее" },
                }
            },
            [85] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 248, Text = "Первый" },
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
            [86] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "СПРУТ",
                                Skill = 11,
                                Strength = 15,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 271, Text = "Далее" },
                }
            },
            [87] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 384, Text = "К невысокому подводному зданию" },
                    new Option { Destination = 539, Text = "Поплыть в противоположную сторону" },
                    new Option { Destination = 32, Text = "Не менять направления" },
                }
            },
            [88] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "СТАРИК-ЛОДОЧНИК",
                                Skill = 6,
                                Strength = 10,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 192, Text = "Далее" },
                }
            },
            [89] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 447, Text = "Далее" },
                }
            },
            [90] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 253, Text = "Прямо" },
                    new Option { Destination = 19, Text = "Направо" },
                    new Option { Destination = 52, Text = "Налево" },
                }
            },
            [91] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 436, Text = "Совета" },
                    new Option { Destination = 385, Text = "Удачи" },
                }
            },
            [92] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 540, Text = "Далее" },
                }
            },
            [93] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 272, Text = "У вас есть трезубец" },
                    new Option { Destination = 608, Text = "Далее" },
                }
            },
            [94] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 90, Text = "Направиться туда" },
                    new Option { Destination = 523, Text = "Поплывете куда глаза глядят" },
                }
            },
            [95] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 57, Text = "Вернуться в Грейкейп" },
                    new Option { Destination = 387, Text = "Направитесь вперед" },
                }
            },
            [96] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 193, Text = "Далее" },
                }
            },
            [97] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 324, Text = "Далее" },
                }
            },
            [98] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 422, Text = "Далее" },
                }
            },
            [99] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 457, Text = "Встречали капитана Шелтона" },
                    new Option { Destination = 249, Text = "Далее" },
                }
            },
            [100] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ГРИФ",
                                Skill = 8,
                                Strength = 10,
                            },
                        },

                        Aftertext = "Если удается убить его, то с острова уплываете, потратив на его посещение 40 минут."
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 555, Text = "Далее" },
                }
            },
            [101] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 450, Text = "Заговорить с ним" },
                    new Option { Destination = 286, Text = "Убить его" },
                    new Option { Destination = 501, Text = "Проплыть мимо" },
                }
            },
            [102] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 26, Text = "Далее" },
                }
            },
            [103] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 451, Text = "Осмотреть это место" },
                    new Option { Destination = 189, Text = "Плыть дальше" },
                    new Option { Destination = 269, Text = "Заинтересоваться большим подводным лугом" },
                }
            },
            [104] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 262, Text = "Далее" },
                }
            },
            [105] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 502, Text = "Есть опознавательный знак" },
                    new Option { Destination = 438, Text = "Есть трезубец" },
                    new Option { Destination = 312, Text = "Далее" },
                }
            },
            [106] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 361, Text = "Вернетесь" },
                    new Option { Destination = 621, Text = "Первую" },
                    new Option { Destination = 412, Text = "Вторую" },
                    new Option { Destination = 556, Text = "Третью" },
                    new Option { Destination = 306, Text = "Четвертую" },
                    new Option { Destination = 469, Text = "Пятую" },
                    new Option { Destination = 39, Text = "Шестую" },
                    new Option { Destination = 209, Text = "Седьмую" },
                }
            },
            [107] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 618, Text = "Удачливы" },
                    new Option { Destination = 203, Text = "Нет" },
                }
            },
            [108] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 447, Text = "Далее" },
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
                    new Option { Destination = 369, Text = "Далее" },
                }
            },
            [111] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 289, Text = "Далее" },
                }
            },
            [112] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 3, Text = "Вы безоружны" },
                    new Option { Destination = 11, Text = "У вас есть какое-либо другое оружие" },
                }
            },
            [113] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Пусть сражаются",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            new Character
                            {
                                Name = "НОСОРОГ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ВОИН",
                                Skill = 10,
                                Strength = 10,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ ВОИН",
                                Skill = 10,
                                Strength = 10,
                            },
                        },

                        Aftertext = "Если ваши надежды оправдались, то Носорог исчезает. Если же нет, то дальше придется сражаться самому."
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 217, Text = "Надежды оправдались" },
                    new Option { Destination = 63, Text = "Сражаться" },
                }
            },
            [114] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 88, Text = "Убьете его" },
                    new Option { Destination = 365, Text = "Распрощаетесь и отправитесь дальше" },
                }
            },
            [115] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 90, Text = "Далее" },
                }
            },
            [116] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 307, Text = "Рыбу-меч" },
                    new Option { Destination = 452, Text = "Рыбу-пилу" },
                    new Option { Destination = 503, Text = "Рыбу-луну" },
                    new Option { Destination = 40, Text = "Рыбу-ежа" },
                    new Option { Destination = 286, Text = "Убить его" },
                    new Option { Destination = 501, Text = "Отправиться дальше" },
                }
            },
            [117] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 512, Text = "Удержаться внизу" },
                    new Option { Destination = 484, Text = "Всплывете на поверхность моря" },
                }
            },
            [118] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 549, Text = "Далее" },
                }
            },
            [119] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 234, Text = "Далее" },
                }
            },
            [120] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 612, Text = "Далее" },
                }
            },
            [121] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 398, Text = "Далее" },
                }
            },
            [122] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 41, Text = "Далее" },
                }
            },
            [123] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 248, Text = "Первый" },
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
            [124] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 501, Text = "Далее" },
                }
            },
            [125] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 169, Text = "Исследовать пещеру" },
                    new Option { Destination = 24, Text = "Отправитесь в открытое море" },
                }
            },
            [126] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 42, Text = "Заговорите" },
                    new Option { Destination = 468, Text = "Покормите" },
                    new Option { Destination = 516, Text = "Поплывете дальше" },
                }
            },
            [127] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 258, Text = "Далее" },
                }
            },
            [128] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 236, Text = "Далее" },
                }
            },
            [129] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 204, Text = "Далее" },
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
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "МОРСКОЙ ЧЕРТ",
                                Skill = 9,
                                Strength = 2,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 472, Text = "Далее" },
                }
            },
            [132] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 559, Text = "Далее" },
                }
            },
            [133] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Пусть сражаются",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            new Character
                            {
                                Name = "КРЫЛАТЫЙ ЛЕВ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ ПИРАТ",
                                Skill = 9,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ТРЕТИЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },

                        Aftertext = "Если лев победил, он улетает, сопровождаемый вашим благодарным взглядом. Если же пираты убили его, в бой придется вступить вам."
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 600, Text = "Помочь ему" },
                    new Option { Destination = 198, Text = "Если лев победил" },
                    new Option { Destination = 583, Text = "Если же пираты убили его" },
                }
            },
            [134] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 189, Text = "Далее" },
                }
            },
            [135] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 336, Text = "Направо" },
                    new Option { Destination = 566, Text = "Налево" },
                }
            },
            [136] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 85, Text = "Далее" },
                }
            },
            [137] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ТРЕТИЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },

                        Aftertext = "А также:",
                    },
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Пусть сражаются",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            new Character
                            {
                                Name = "РЫЦАРЬ-ВОДЯНОЙ",
                                Skill = 10,
                                Strength = 9,
                            },
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВТОРОЙ ПИРАТ",
                                Skill = 9,
                                Strength = 8,
                            },
                        },

                        Aftertext = "Если кто-то побеждает в одной из пар, то может помочь другу. В случае удачи поблагодарите Рыцаря-водяного, и он уплывает.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Далее" },
                }
            },
            [138] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 476, Text = "Далее" },
                }
            },
            [139] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 338, Text = "Золотой пояс" },
                    new Option { Destination = 584, Text = "Раковин" },
                    new Option { Destination = 74, Text = "Браслет" },
                    new Option { Destination = 508, Text = "Отправляйтесь дальше" },
                }
            },
            [140] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Пусть сражаются",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            new Character
                            {
                                Name = "КАПИТАН",
                                Skill = 10,
                                Strength = 14,
                            },
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВТОРОЙ ПИРАТ",
                                Skill = 9,
                                Strength = 8,
                            },
                        },

                        Aftertext = "Вторая парая:",
                    },
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ТРЕТИЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 488, Text = "Капитан остался жив" },
                    new Option { Destination = 198, Text = "Пираты убиты" },
                }
            },
            [141] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 56, Text = "Далее" },
                }
            },
            [142] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 511, Text = "Далее" },
                }
            },
            [143] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 208, Text = "Далее" },
                }
            },
            [144] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 590, Text = "Посмотреть" },
                    new Option { Destination = 343, Text = "Отправиться дальше" },
                }
            },
            [145] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 447, Text = "Далее" },
                }
            },
            [146] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ГИГАНТСКИЙ КРАБ",
                                Skill = 9,
                                Strength = 14,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 78, Text = "Он уменьшил вашу силу" },
                    new Option { Destination = 597, Text = "Вы убили его" },
                }
            },
            [147] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Пусть сражаются",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            new Character
                            {
                                Name = "МОРСКОЙ РЫЦАРЬ",
                                Skill = 11,
                                Strength = 12,
                            },
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ ПИРАТ",
                                Skill = 9,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ТРЕТИЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 594, Text = "Помочь" },
                    new Option { Destination = 198, Text = "Рыцарь побеждает" },
                    new Option { Destination = 583, Text = "Рыцарь погиб" },
                }
            },
            [148] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Пусть сражаются",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            new Character
                            {
                                Name = "НОСОРОГ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },

                        },
                    },
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВТОРОЙ ПИРАТ",
                                Skill = 9,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ТРЕТИЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Если вы победили" },
                    new Option { Destination = 598, Text = "Бой продолжается с обоими пиратами" },
                    new Option { Destination = 491, Text = "Бой продолжается с одним пиратом" },
                }
            },
            [149] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",
                        SkillPenalty = 2,

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "КРЫЛАТЫЙ ЛЕВ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 324, Text = "Покинуть остров" },
                    new Option { Destination = 61, Text = "Осмотреть логово" },
                }
            },
            [150] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 341, Text = "Есть амулет" },
                    new Option { Destination = 348, Text = "Есть ржавый меч" },
                    new Option { Destination = 609, Text = "Далее" },
                }
            },
            [151] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 405, Text = "Нагоните корабль капитана Шелтона" },
                    new Option { Destination = 237, Text = "Продолжать свой путь" },
                }
            },
            [152] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "РЫБА-ХАМЕЛЕОН",
                                Skill = 11,
                                Strength = 2,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 53, Text = "Далее" },
                }
            },
            [153] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 3, Text = "Далее" },
                }
            },
            [154] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 358, Text = "Покинуть остров" },
                    new Option { Destination = 525, Text = "Попробуйте догнать" },
                }
            },
            [155] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "МОРСКАЯ ЗМЕЯ",
                                Skill = 10,
                                Strength = 14,
                                ExtendedDamage = 1,
                                SkillDamage = 1,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 355, Text = "Далее" },
                }
            },
            [156] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 243, Text = "Далее" },
                }
            },
            [157] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 530, Text = "Покинуть остров" },
                    new Option { Destination = 9, Text = "Попытаться расспросить моряка" },
                }
            },
            [158] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 413, Text = "Удачливы" },
                    new Option { Destination = 533, Text = "Нет" },
                }
            },
            [159] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",
                        DamageToWin = 8,

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "АКУЛА",
                                Skill = 10,
                                Strength = 16,
                            },
                        },

                        Aftertext = "Сможете ли вы уменьшить силу своего противника до 8 или акула уменьшит вашу?"
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 406, Text = "Смогли уменьшить силу акулы" },
                    new Option { Destination = 362, Text = "Акула уменьшила вашу силу" },
                }
            },
            [160] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 72, Text = "Прошмыгнуть" },
                    new Option { Destination = 244, Text = "Вступить с ними в разговор" },
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
                    new Option { Destination = 257, Text = "Дальше ощупывать стены" },
                    new Option { Destination = 10, Text = "Повернете обратно" },
                }
            },
            [163] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 585, Text = "В каюту капитана" },
                    new Option { Destination = 411, Text = "В трюм" },
                    new Option { Destination = 364, Text = "В каюту штурмана" },
                }
            },
            [164] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 21, Text = "Оттолкнуться от противоположной стены" },
                    new Option { Destination = 359, Text = "Подняться вверх по лестнице" },
                }
            },
            [165] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 624, Text = "Приблизиться к Филину" },
                    new Option { Destination = 258, Text = "Далее" },
                }
            },
            [166] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 85, Text = "Далее" },
                }
            },
            [167] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 366, Text = "Далее" },
                }
            },
            [168] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 259, Text = "Разглядеть их поближе" },
                    new Option { Destination = 423, Text = "В открытое море" },
                }
            },
            [169] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 90, Text = "Далее" },
                }
            },
            [170] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "РЫБА",
                                Skill = 9,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 22, Text = "Далее" },
                }
            },
            [171] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Пусть сражаются",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВОИН",
                                Skill = 11,
                                Strength = 14,
                            },
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ ПИРАТ",
                                Skill = 9,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ТРЕТИЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 224, Text = "Помочь воину" },
                    new Option { Destination = 198, Text = "Воин победил" },
                    new Option { Destination = 583, Text = "Воин погиб" },
                }
            },
            [172] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 8, Text = "Далее" },
                }
            },
            [173] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 15, Text = "Далее" },
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
                    new Option { Destination = 240, Text = "Последовать за ними" },
                    new Option { Destination = 407, Text = "Покинуть остров" },
                }
            },
            [176] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 357, Text = "Далее" },
                }
            },
            [177] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 273, Text = "Далее" },
                }
            },
            [178] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 5, Text = "К кораблю" },
                    new Option { Destination = 526, Text = "В другую сторону" },
                }
            },
            [179] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 623, Text = "Большой алмаз" },
                    new Option { Destination = 23, Text = "Серебряное блюдо" },
                    new Option { Destination = 260, Text = "Золотой щит" },
                    new Option { Destination = 424, Text = "Ржавый меч" },
                    new Option { Destination = 71, Text = "В тоннель перед вами" },
                    new Option { Destination = 345, Text = "В тоннель справа" },
                }
            },
            [180] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Пусть сражаются",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            new Character
                            {
                                Name = "КРЫЛАТЫЙ ЛЕВ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВОИН",
                                Skill = 10,
                                Strength = 10,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 217, Text = "Далее" },
                }
            },
            [181] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ГИГАНТСКИЙ КАЛЬМАР",
                                Skill = 11,
                                Strength = 14,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 541, Text = "Далее" },
                }
            },
            [182] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 86, Text = "Направиться туда" },
                    new Option { Destination = 274, Text = "Проплыть стороной" },
                }
            },
            [183] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 29, Text = "Далее" },
                }
            },
            [184] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 168, Text = "Далее" },
                }
            },
            [185] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 414, Text = "Далее" },
                }
            },
            [186] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 439, Text = "Попить еще" },
                    new Option { Destination = 15, Text = "Двинуться дальше" },
                }
            },
            [187] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 35, Text = "Далее" },
                }
            },
            [188] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 504, Text = "Налево" },
                    new Option { Destination = 291, Text = "Направо" },
                }
            },
            [189] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 313, Text = "Поговорить с ней" },
                    new Option { Destination = 613, Text = "Атакуете" },
                }
            },
            [190] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 54, Text = "Левая стена" },
                    new Option { Destination = 528, Text = "Стена перед вами" },
                    new Option { Destination = 10, Text = "Повернуть обратно" },
                }
            },
            [191] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 308, Text = "Удачливы" },
                    new Option { Destination = 453, Text = "Нет" },
                }
            },
            [192] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 365, Text = "Далее" },
                }
            },
            [193] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 292, Text = "Далее" },
                }
            },
            [194] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Пусть сражаются",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ЛАТНИК",
                                Skill = 11,
                                Strength = 12,
                            },
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ВОИН",
                                Skill = 10,
                                Strength = 10,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ ВОИН",
                                Skill = 10,
                                Strength = 10,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 217, Text = "Рыцарь победил" },
                    new Option { Destination = 63, Text = "Сражаться" },
                }
            },
            [195] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 98, Text = "Славы" },
                    new Option { Destination = 626, Text = "Богатства" },
                    new Option { Destination = 505, Text = "Совета" },
                }
            },
            [196] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 274, Text = "Над огромной подводной равниной" },
                    new Option { Destination = 310, Text = "Через заросли" },
                }
            },
            [197] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 304, Text = "Исследовать впадину" },
                    new Option { Destination = 231, Text = "Обогнуть ее с левой стороны" },
                    new Option { Destination = 15, Text = "Обогнуть ее с правой стороны" },
                }
            },
            [198] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 293, Text = "Далее" },
                }
            },
            [199] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 353, Text = "Далее" },
                }
            },
            [200] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 87, Text = "Далее" },
                }
            },
            [201] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 454, Text = "Удачливы" },
                    new Option { Destination = 506, Text = "Нет" },
                }
            },
            [202] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 45, Text = "Есть золотой перстень" },
                    new Option { Destination = 501, Text = "Поблагодарить и распрощаться" },
                }
            },
            [203] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 441, Text = "Далее" },
                }
            },
            [204] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ЭЛЕКТРИЧЕСКИЙ СКАТ",
                                Skill = 8,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 473, Text = "Убили его" },
                    new Option { Destination = 153, Text = "Попробовать уплыть" },
                }
            },
            [205] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 560, Text = "Попробуете" },
                    new Option { Destination = 339, Text = "Откажетесь" },
                }
            },
            [206] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 178, Text = "Направитесь к нему" },
                    new Option { Destination = 48, Text = "Проплывёте мимо" },
                }
            },
            [207] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 569, Text = "Вернетесь обратно и покинете остров" },
                    new Option { Destination = 76, Text = "Спуститесь к реке напиться" },
                }
            },
            [208] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 332, Text = "Попытаетесь быстро выбежать" },
                    new Option { Destination = 149, Text = "Примете бой" },
                }
            },
            [209] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 572, Text = "Выпало 1, 2 или 3" },
                    new Option { Destination = 141, Text = "Выпало 4, 5 или 6" },
                }
            },
            [210] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 49, Text = "Далее" },
                }
            },
            [211] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "РЫБА-ВОРЧУН",
                                Skill = 6,
                                Strength = 7,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 234, Text = "К красивым морским цветам" },
                    new Option { Destination = 48, Text = "В открытое море" },
                }
            },
            [212] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 509, Text = "Далее" },
                }
            },
            [213] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 13, Text = "Далее" },
                }
            },
            [214] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 189, Text = "Далее" },
                }
            },
            [215] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 490, Text = "Далее" },
                }
            },
            [216] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 274, Text = "Над огромной подводной равниной" },
                    new Option { Destination = 310, Text = "Через заросли гигантских древовидных растений" },
                }
            },
            [217] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",
                        DamageToWin = 12,

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВЕЛИКАН",
                                Skill = 12,
                                Strength = 24,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 331, Text = "Есть амулет" },
                    new Option { Destination = 150, Text = "Удалось уменьшить силу врага" },
                }
            },
            [218] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Пусть сражаются",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВОИН ПРИНЦА",
                                Skill = 11,
                                Strength = 14,
                            },
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ВОИН",
                                Skill = 10,
                                Strength = 10,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ ВОИН",
                                Skill = 10,
                                Strength = 10,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 217, Text = "Посланец Принца победил" },
                    new Option { Destination = 63, Text = "Бой придется продолжать" },
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
                    new Option { Destination = 500, Text = "Далее" },
                }
            },
            [221] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 592, Text = "Далее" },
                }
            },
            [222] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 136, Text = "Далее" },
                }
            },
            [223] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 269, Text = "Далее" },
                }
            },
            [224] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВТОРОЙ ПИРАТ",
                                Skill = 9,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ТРЕТИЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },

                        Aftertext = "А во второй:",
                    },
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Пусть сражаются",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВОИН",
                                Skill = 11,
                                Strength = 14,
                            },
                            new Character
                            {
                                Name = "ПЕРВЫЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 449, Text = "Посланник Принца победил, а оба ваших противника еще живы" },
                    new Option { Destination = 607, Text = "Жив только один" },
                    new Option { Destination = 198, Text = "Все пираты уничтожены" },
                }
            },
            [225] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 302, Text = "Далее" },
                }
            },
            [226] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ТРЕТИЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },

                        Aftertext = "А также:",
                    },
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Пусть сражаются",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "МОРСКОЙ РЫЦАРЬ",
                                Skill = 11,
                                Strength = 12,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ ПИРАТ",
                                Skill = 9,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Далее" },
                }
            },
            [227] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Далее" },
                }
            },
            [228] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 524, Text = "Далее" },
                }
            },
            [229] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 284, Text = "Далее" },
                }
            },
            [230] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 351, Text = "Последуете за ним" },
                    new Option { Destination = 6, Text = "Поблагодарите и уйдете" },
                }
            },
            [231] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 155, Text = "Попробуете заплыть внутрь" },
                    new Option { Destination = 402, Text = "Прочь от пещеры к необычному строению" },
                    new Option { Destination = 527, Text = "К тому месту, где на дне видны странные песчаные холмики" },
                }
            },
            [232] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 161, Text = "Попытаетесь сделать это" },
                    new Option { Destination = 20, Text = "Будете продолжать путешествие под водой" },
                }
            },
            [233] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 358, Text = "Покинуть остров" },
                    new Option { Destination = 525, Text = "Попробовать догнать этого человека" },
                }
            },
            [234] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 408, Text = "Дотянуться рукой" },
                    new Option { Destination = 576, Text = "Дотянуться мечом" },
                    new Option { Destination = 11, Text = "Сразитесь с цветами" },
                    new Option { Destination = 168, Text = "Поплывете дальше" },
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
                    new Option { Destination = 363, Text = "Далее" },
                }
            },
            [237] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 57, Text = "Повернуть назад" },
                    new Option { Destination = 263, Text = "Устремиться вперед" },
                }
            },
            [238] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ТРЕТИЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },

                        Aftertext = "А также:",
                    },
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Пусть сражаются",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "КРЫЛАТЫЙ ЛЕВ",
                                Skill = 10,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ ПИРАТ",
                                Skill = 9,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Далее" },
                }
            },
            [239] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 359, Text = "Подняться вверх" },
                    new Option { Destination = 7, Text = "Спуститься вниз" },
                }
            },
            [240] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 407, Text = "Далее" },
                }
            },
            [241] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 528, Text = "Удачливы" },
                    new Option { Destination = 162, Text = "Нет" },
                }
            },
            [242] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 389, Text = "Сразитесь с ней" },
                    new Option { Destination = 12, Text = "Попробуете приручить, накормив" },
                }
            },
            [243] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 371, Text = "Спуститься и посмотреть" },
                    new Option { Destination = 87, Text = "Поплывете дальше" },
                }
            },
            [244] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться со старшим",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "СТАРШИЙ РЫЦАРЬ-ВОДЯНОЙ",
                                Skill = 9,
                                Strength = 10,
                            },
                        },

                        Aftertext = "Если вам удалось убить противника за пять раундов атаки, то вы можете либо снова попытаться бежать, либо сразиться с двумя оставшимися (теперь уже одновременно). Если же через пять раундов атаки ваш враг еще жив, два других просто присоединяются к нему.",
                    },

                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться с остальными",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ РЫЦАРЬ-ВОДЯНОЙ",
                                Skill = 8,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ РЫЦАРЬ-ВОДЯНОЙ",
                                Skill = 7,
                                Strength = 9,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 158, Text = "Попытаться бежать" },
                    new Option { Destination = 414, Text = "Вы убили всех Рыцарей-водяных" },
                }
            },
            [245] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 169, Text = "Исследовать пещеру" },
                    new Option { Destination = 24, Text = "Отправиться в открытое море" },
                }
            },
            [246] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 390, Text = "Заплатить ему 10 золотых" },
                    new Option { Destination = 524, Text = "Руководить командой в одиночку" },
                }
            },
            [247] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 542, Text = "К сокровищам" },
                    new Option { Destination = 415, Text = "За добрым советом" },
                    new Option { Destination = 114, Text = "К тому, кто топит корабли" },
                    new Option { Destination = 365, Text = "Откажетесь от его услуг" },
                    new Option { Destination = 88, Text = "Убить старика" },
                }
            },
            [248] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "НОСОРОГ",
                                Skill = 10,
                                Strength = 10,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 13, Text = "Далее" },
                }
            },
            [249] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 629, Text = "Далее" },
                }
            },
            [250] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 3, Text = "Далее" },
                }
            },
            [251] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 89, Text = "Плыть за ней дальше" },
                    new Option { Destination = 152, Text = "Отказаться" },
                }
            },
            [252] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 85, Text = "Далее" },
                }
            },
            [253] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 427, Text = "Атаковать его первым" },
                    new Option { Destination = 391, Text = "Откроете правду и попросите помочь в поисках Неведомого" },
                    new Option { Destination = 543, Text = "Ответите, что пришли, чтобы найти подводного Короля" },
                }
            },
            [254] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 71, Text = "Через тот, что прямо перед вами" },
                    new Option { Destination = 345, Text = "Через тот, что справа от вас" },
                }
            },
            [255] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 90, Text = "Внутрь пещеры" },
                    new Option { Destination = 523, Text = "В открытое море" },
                }
            },
            [256] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 345, Text = "Проскользнуть в ближайший к вам выход" },
                    new Option { Destination = 241, Text = "Свернуть в боковое ответвление" },
                }
            },
            [257] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 528, Text = "Ту, что перед вами" },
                    new Option { Destination = 54, Text = "Ту, что слева" },
                    new Option { Destination = 190, Text = "Ту, что справа" },
                }
            },
            [258] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 248, Text = "Первый" },
                    new Option { Destination = 16, Text = "Второй" },
                    new Option { Destination = 233, Text = "Третий" },
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
            [259] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 428, Text = "Одну порцию еду" },
                    new Option { Destination = 91, Text = "Две порции еды" },
                    new Option { Destination = 392, Text = "Два золотых" },
                    new Option { Destination = 544, Text = "Четыре золотых" },
                    new Option { Destination = 30, Text = "Что-то из вещей" },
                }
            },
            [260] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 71, Text = "Через тоннель, начинающийся в противоположной стене зала" },
                    new Option { Destination = 345, Text = "Через тот, который виден в правой стене" },
                }
            },
            [261] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 382, Text = "Далее" },
                }
            },
            [262] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "КОНДОР",
                                Skill = 9,
                                Strength = 9,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 429, Text = "Далее" },
                }
            },
            [263] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 55, Text = "Далее" },
                }
            },
            [264] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 195, Text = "Далее" },
                }
            },
            [265] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 394, Text = "Попробуете проникнуть во фрегат" },
                    new Option { Destination = 8, Text = "Поплывете дальше один" },
                }
            },
            [266] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 241, Text = "Далее" },
                }
            },
            [267] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 610, Text = "Проплыть этим тоннелем" },
                    new Option { Destination = 536, Text = "К подводному лесу" },
                    new Option { Destination = 381, Text = "Проскользнуть между лесом и скалами" },
                }
            },
            [268] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЯТНИСТАЯ АКУЛА",
                                Skill = 10,
                                Strength = 10,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 369, Text = "Далее" },
                }
            },
            [269] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 107, Text = "Попытаться оседлать его" },
                    new Option { Destination = 441, Text = "Отправиться дальше" },
                }
            },
            [270] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 94, Text = "Далее" },
                }
            },
            [271] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 196, Text = "Далее" },
                }
            },
            [272] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 269, Text = "Далее" },
                }
            },
            [273] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 248, Text = "Первый" },
                    new Option { Destination = 16, Text = "Второй" },
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
            [274] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 507, Text = "Вступить в бой" },
                    new Option { Destination = 314, Text = "Попробуете покормить" },
                }
            },
            [275] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 120, Text = "Покормите ее" },
                    new Option { Destination = 561, Text = "Убьете её" },
                    new Option { Destination = 612, Text = "Просто поплывете дальше" },
                }
            },
            [276] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 197, Text = "Удачливы" },
                    new Option { Destination = 442, Text = "Нет" },
                }
            },
            [277] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 43, Text = "С правого" },
                    new Option { Destination = 474, Text = "С левого" },
                    new Option { Destination = 508, Text = "Уплыть, не дотрагиваясь" },
                }
            },
            [278] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 253, Text = "Далее" },
                }
            },
            [279] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 458, Text = "Есть Рыба-пила" },
                    new Option { Destination = 396, Text = "Уплыть из леса" },
                }
            },
            [280] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 365, Text = "Далее" },
                }
            },
            [281] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 402, Text = "К развалинам" },
                    new Option { Destination = 103, Text = "В открытое море" },
                }
            },
            [282] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 509, Text = "Покинуть остров" },
                    new Option { Destination = 459, Text = "Направиться к хижине" },
                }
            },
            [283] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 8, Text = "Далее" },
                }
            },
            [284] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ ПИРАТ",
                                Skill = 9,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ТРЕТИЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 229, Text = "Позвать Солнечную рыбу" },
                    new Option { Destination = 198, Text = "Вы победили" },
                }
            },
            [285] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "МОРСКОЙ ЧЕРТ",
                                Skill = 9,
                                Strength = 10,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 121, Text = "Попробовать скрыться" },
                    new Option { Destination = 315, Text = "Удалось уменьшить силу Морского черта до 2" },
                }
            },
            [286] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 501, Text = "Далее" },
                }
            },
            [287] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 622, Text = "Далее" },
                }
            },
            [288] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 195, Text = "Далее" },
                }
            },
            [289] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 94, Text = "Далее" },
                }
            },
            [290] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 577, Text = "Помочь ему" },
                    new Option { Destination = 480, Text = "Пусть сражается в одиночку" },
                }
            },
            [291] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 316, Text = "Постучать" },
                    new Option { Destination = 205, Text = "Открыть без стука" },
                    new Option { Destination = 504, Text = "Вернуться на развилку" },
                }
            },
            [292] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 122, Text = "Сесть в него" },
                    new Option { Destination = 562, Text = "Устроиться на полу" },
                    new Option { Destination = 41, Text = "Уйти" },
                }
            },
            [293] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ДРАКОН",
                                Skill = 11,
                                Strength = 15,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 476, Text = "Далее" },
                }
            },
            [294] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 510, Text = "Удачливы" },
                    new Option { Destination = 579, Text = "Нет" },
                }
            },
            [295] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 526, Text = "Далее" },
                }
            },
            [296] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 558, Text = "Покормить коралловых рыбок" },
                    new Option { Destination = 462, Text = "Отправиться дальше, минуя риф" },
                    new Option { Destination = 516, Text = "Обогнуть его" },
                }
            },
            [297] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 123, Text = "Далее" },
                }
            },
            [298] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 317, Text = "Далее" },
                }
            },
            [299] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 511, Text = "Пойти с ним" },
                    new Option { Destination = 142, Text = "Оказать сопротивление" },
                }
            },
            [300] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 463, Text = "Удачливы" },
                    new Option { Destination = 563, Text = "Нет" },
                }
            },
            [301] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 210, Text = "Произнести заклятие" },
                    new Option { Destination = 49, Text = "Покинуть библиотеку" },
                }
            },
            [302] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 512, Text = "Постараться удержаться внизу" },
                    new Option { Destination = 484, Text = "Всплыть на поверхность" },
                }
            },
            [303] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 124, Text = "Серебряное блюдо" },
                    new Option { Destination = 45, Text = "Золотой перстень" },
                    new Option { Destination = 627, Text = "Сушеного краба" },
                    new Option { Destination = 501, Text = "Отправиться дальше" },
                }
            },
            [304] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 565, Text = "К сундукам" },
                    new Option { Destination = 231, Text = "К одинокой скале" },
                }
            },
            [305] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 464, Text = "Хотите расстаться с 10 золотыми" },
                    new Option { Destination = 614, Text = "Не хотите" },
                }
            },
            [306] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 545, Text = "Далее" },
                }
            },
            [307] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 501, Text = "Отправиться дальше" },
                    new Option { Destination = 202, Text = "Поинтересоваться, нет ли у него еще чего-нибудь полезного" },
                }
            },
            [308] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 513, Text = "Карлика" },
                    new Option { Destination = 46, Text = "Лодочника" },
                    new Option { Destination = 143, Text = "Стража у входа в королевство" },
                    new Option { Destination = 603, Text = "Солнечную рыбу" },
                    new Option { Destination = 379, Text = "Стража и Карлика" },
                    new Option { Destination = 287, Text = "Карлика и лодочника" },
                    new Option { Destination = 342, Text = "Лодочника и Солнечную рыбу" },
                    new Option { Destination = 368, Text = "Солнечную рыбу и Водяного" },
                    new Option { Destination = 97, Text = "Вы не нападали ни на одно из них" },
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
                    new Option { Destination = 479, Text = "Убить его" },
                    new Option { Destination = 566, Text = "Поплывете дальше" },
                }
            },
            [311] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 211, Text = "Далее" },
                }
            },
            [312] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",
                        RoundsToWin = 5,

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВОДЯНОЙ",
                                Skill = 9,
                                Strength = 10,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 521, Text = "Водяной убит за пять раундов атаки" },
                    new Option { Destination = 130, Text = "Водяной не убит" },
                }
            },
            [313] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 58, Text = "Далее" },
                }
            },
            [314] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 32, Text = "Далее" },
                }
            },
            [315] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 567, Text = "Пощадите его" },
                    new Option { Destination = 131, Text = "Будете сражаться до конца" },
                }
            },
            [316] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 205, Text = "Откроете ее" },
                    new Option { Destination = 504, Text = "Вернетесь на развилку" },
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
                    new Option { Destination = 304, Text = "Далее" },
                }
            },
            [319] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 269, Text = "К большому подводному лугу слева" },
                    new Option { Destination = 189, Text = "В открытое море" },
                }
            },
            [320] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 396, Text = "Далее" },
                }
            },
            [321] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 566, Text = "Налево" },
                    new Option { Destination = 336, Text = "Направо" },
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
                    new Option { Destination = 132, Text = "На нем выпало 1 или 2" },
                    new Option { Destination = 485, Text = "На неём выпало 3 или 4" },
                }
            },
            [324] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 248, Text = "Первый" },
                    new Option { Destination = 16, Text = "Второй" },
                    new Option { Destination = 233, Text = "Третий" },
                    new Option { Destination = 165, Text = "Четвертый" },
                    new Option { Destination = 418, Text = "Пятый" },
                    new Option { Destination = 395, Text = "Шестой" },
                    new Option { Destination = 100, Text = "Восьмой" },
                    new Option { Destination = 84, Text = "Девятый" },
                    new Option { Destination = 352, Text = "Десятый" },
                    new Option { Destination = 282, Text = "Одиннадцатый" },
                    new Option { Destination = 456, Text = "Двенадцатый" },
                    new Option { Destination = 500, Text = "Тринадцатый" },
                }
            },
            [325] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 269, Text = "Далее" },
                }
            },
            [326] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 466, Text = "Возьмете его с собой" },
                    new Option { Destination = 539, Text = "Выбросите и продолжите путь" },
                }
            },
            [327] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 474, Text = "Попытать счастья со вторым" },
                    new Option { Destination = 508, Text = "Отправиться дальше" },
                }
            },
            [328] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 234, Text = "К скоплению цветов слева от вас" },
                    new Option { Destination = 447, Text = "Прямо" },
                    new Option { Destination = 526, Text = "Направо" },
                }
            },
            [329] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ВОИН",
                                Skill = 10,
                                Strength = 10,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ ВОИН",
                                Skill = 10,
                                Strength = 10,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 217, Text = "Далее" },
                }
            },
            [330] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Пусть сражаются",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ГРИФ",
                                Skill = 8,
                                Strength = 10,
                            },
                            new Character
                            {
                                Name = "ФИЛИН",
                                Skill = 9,
                                Strength = 9,
                            },
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "КОНДОР",
                                Skill = 9,
                                Strength = 9,
                            },
                        },

                        Aftertext = "Если ваши друзья победили Кондора, то они могут лететь по своим делам, если же он убил их, в бой придется вступить вам."
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 429, Text = "Они победили Кондора" },
                    new Option { Destination = 570, Text = "Кондор убил их" },
                }
            },
            [331] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 217, Text = "Далее" },
                }
            },
            [332] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 574, Text = "Удачливы" },
                    new Option { Destination = 483, Text = "Нет" },
                }
            },
            [333] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 32, Text = "Далее" },
                }
            },
            [334] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 488, Text = "Капитан остался жив после того, как пират побежден" },
                    new Option { Destination = 198, Text = "Капитан мёртв" },
                }
            },
            [335] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 308, Text = "Далее" },
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
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Пусть сражаются",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            new Character
                            {
                                Name = "РЫЦАРЬ-ВОДЯНОЙ",
                                Skill = 10,
                                Strength = 9,
                            },
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ВОИН",
                                Skill = 10,
                                Strength = 10,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ ВОИН",
                                Skill = 10,
                                Strength = 10,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 63, Text = "Водяной погиб, бой принимать вам" },
                    new Option { Destination = 217, Text = "Водяной перебил врагов" },
                }
            },
            [338] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 584, Text = "Взять с собой раковину" },
                    new Option { Destination = 74, Text = "Взять с бронзовый браслет" },
                    new Option { Destination = 508, Text = "Отправиться дальше" },
                }
            },
            [339] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",
                        RoundsToWin = 4,

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПОВАР-ВОДЯНОЙ",
                                Skill = 8,
                                Strength = 8,
                            },
                        },

                        Aftertext = "Если удастся убить его за четыре раунда атаки, то лучше поскорее убраться из кухни, вернувшись в коридор, по которому вы приплыли, а если нет, то у вас возникнут проблемы."
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 504, Text = "Удалось убить за четыре раунда атаки" },
                    new Option { Destination = 601, Text = "Не удалось" },
                }
            },
            [340] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Далее" },
                }
            },
            [341] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 609, Text = "Далее" },
                    new Option { Destination = 348, Text = "Есть с собой ржавый меч" },
                }
            },
            [342] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 622, Text = "Далее" },
                }
            },
            [343] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 304, Text = "Исследовать ее" },
                    new Option { Destination = 15, Text = "Обогнуть справа" },
                    new Option { Destination = 231, Text = "Обогнуть слева" },
                }
            },
            [344] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 493, Text = "Взять их с собой" },
                    new Option { Destination = 234, Text = "К красивым морским цветам впереди" },
                    new Option { Destination = 48, Text = "В открытое море" },
                }
            },
            [345] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 31, Text = "Плыть дальше прямо" },
                    new Option { Destination = 602, Text = "Свернуть направо" },
                    new Option { Destination = 105, Text = "Свернуть налево" },
                }
            },
            [346] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 99, Text = "Далее" },
                }
            },
            [347] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 67, Text = "Исследовать пещеру" },
                    new Option { Destination = 2, Text = "Плыть дальше прямо" },
                    new Option { Destination = 146, Text = "Плыть направо от скалы" },
                }
            },
            [348] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 609, Text = "Время для него еще не настало" },
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
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Пусть сражаются",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            new Character
                            {
                                Name = "МОРЯК",
                                Skill = 10,
                                Strength = 12,
                            },
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ ПИРАТ",
                                Skill = 9,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ТРЕТИЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Все враги мертвы" },
                    new Option { Destination = 583, Text = "Ваш союзник погиб" },
                }
            },
            [351] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 51, Text = "Заплатить требуемые деньги" },
                    new Option { Destination = 228, Text = "Уйти" },
                }
            },
            [352] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 401, Text = "Отправиться на исследование острова" },
                    new Option { Destination = 407, Text = "Покинуть его" },
                }
            },
            [353] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 163, Text = "В затонувший фрегат" },
                    new Option { Destination = 8, Text = "Плыть дальше" },
                }
            },
            [354] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 232, Text = "Далее" },
                }
            },
            [355] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 402, Text = "К непонятному строению" },
                    new Option { Destination = 527, Text = "К любопытному участку песчаного дна" },
                }
            },
            [356] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 99, Text = "Далее" },
                }
            },
            [357] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 245, Text = "Далее" },
                }
            },
            [358] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 248, Text = "Первый" },
                    new Option { Destination = 16, Text = "Второй" },
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
            [359] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 416, Text = "Открыть ее" },
                    new Option { Destination = 372, Text = "Продолжить подъем" },
                }
            },
            [360] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 526, Text = "Далее" },
                }
            },
            [361] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 417, Text = "Первую" },
                    new Option { Destination = 25, Text = "Вторую" },
                    new Option { Destination = 261, Text = "Третью" },
                    new Option { Destination = 545, Text = "Четвертую" },
                    new Option { Destination = 199, Text = "Пятую" },
                    new Option { Destination = 294, Text = "Шестую" },
                    new Option { Destination = 56, Text = "Седьмую" },
                }
            },
            [362] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 582, Text = "Защитить Дельфина" },
                    new Option { Destination = 232, Text = "Плыть дальше" },
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
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Break",
                        ButtonName = "Попытаться взломать",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 283, Text = "Удалось" },
                    new Option { Destination = 8, Text = "Плыть дальше" },
                }
            },
            [365] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 235, Text = "В какой-то странный провал" },
                    new Option { Destination = 353, Text = "К непонятным развалинам" },
                    new Option { Destination = 430, Text = "В открытое море" },
                }
            },
            [366] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 26, Text = "Далее" },
                }
            },
            [367] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 232, Text = "Далее" },
                }
            },
            [368] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 622, Text = "Далее" },
                }
            },
            [369] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 104, Text = "Позвать их обоих" },
                    new Option { Destination = 262, Text = "Прибегнуть к помощи Крылатого льва" },
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
                    new Option { Destination = 431, Text = "Вскрыть бутылку и прочитать послание" },
                    new Option { Destination = 200, Text = "Взять ее с собой" },
                    new Option { Destination = 87, Text = "Плыть дальше" },
                }
            },
            [372] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 27, Text = "Посмотреть в подзорную трубу наверх" },
                    new Option { Destination = 546, Text = "Посмотреть в подзорную трубу вниз" },
                    new Option { Destination = 537, Text = "Покинуть башню" },
                }
            },
            [373] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 263, Text = "Направить шхуну к месту катастрофу" },
                    new Option { Destination = 57, Text = "Повернуть обратно в порт" },
                }
            },
            [374] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 31, Text = "Направо" },
                    new Option { Destination = 105, Text = "Прямо" },
                    new Option { Destination = 443, Text = "Налево" },
                }
            },
            [375] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 295, Text = "Последовать за ней" },
                    new Option { Destination = 204, Text = "Прямо" },
                    new Option { Destination = 514, Text = "Налево" },
                }
            },
            [376] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 68, Text = "Обратно в порт" },
                    new Option { Destination = 432, Text = "Продолжить плавание самостоятельно" },
                }
            },
            [377] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 168, Text = "Далее" },
                }
            },
            [378] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 163, Text = "Отправится внутрь" },
                    new Option { Destination = 265, Text = "Отправить Джона" },
                }
            },
            [379] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 622, Text = "Далее" },
                }
            },
            [380] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 526, Text = "Далее" },
                }
            },
            [381] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 547, Text = "Через него" },
                    new Option { Destination = 86, Text = "К небольшой уютной впадине" },
                }
            },
            [382] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 296, Text = "Заговорить" },
                    new Option { Destination = 558, Text = "Покормить" },
                    new Option { Destination = 462, Text = "Плыть дальше, минуя риф" },
                    new Option { Destination = 516, Text = "Плыть дальше, огибая его" },
                }
            },
            [383] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 244, Text = "Далее" },
                }
            },
            [384] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 568, Text = "Есть бутылка с посланием" },
                    new Option { Destination = 32, Text = "Отправться дальше" },
                }
            },
            [385] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 549, Text = "Далее" },
                }
            },
            [386] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 106, Text = "Плыть вместе" },
                    new Option { Destination = 444, Text = "Разделиться" },
                }
            },
            [387] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 111, Text = "Удачливы" },
                    new Option { Destination = 298, Text = "Нет" },
                }
            },
            [388] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЯТНИСТАЯ АКУЛА",
                                Skill = 10,
                                Strength = 10,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 369, Text = "Далее" },
                }
            },
            [389] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "РЫБА-СОБАКА",
                                Skill = 8,
                                Strength = 7,
                            },
                        },

                        Aftertext = "Если вы убили ее, то можете либо поторопиться и покинуть негостеприимный лес, либо направиться дальше: к группе деревьев неподалеку или к подводному лугу справа от вас?"
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 360, Text = "Покинуть лес" },
                    new Option { Destination = 152, Text = "К группе деревьев неподалеку" },
                    new Option { Destination = 550, Text = "К подводному лугу" },
                }
            },
            [390] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 18, Text = "Далее" },
                }
            },
            [391] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 34, Text = "Далее" },
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
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 108, Text = "Удачливы" },
                    new Option { Destination = 445, Text = "Нет" },
                }
            },
            [394] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 8, Text = "Плыть дальше" },
                    new Option { Destination = 585, Text = "В каюту капитана" },
                    new Option { Destination = 364, Text = "В каюту штурмана" },
                    new Option { Destination = 411, Text = "В трюм" },
                }
            },
            [395] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 299, Text = "Подождать неизвестно чего" },
                    new Option { Destination = 465, Text = "Покинуть остров" },
                }
            },
            [396] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 86, Text = "К небольшой уютной впадине" },
                    new Option { Destination = 575, Text = "К невысокой скале" },
                }
            },
            [397] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "МОРСКОЙ ПАУК",
                                Skill = 9,
                                Strength = 9,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 36, Text = "Далее" },
                }
            },
            [398] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "РЫБА-ЕДИНОРОГ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 119, Text = "Попытаться бежать" },
                    new Option { Destination = 551, Text = "Удалось ее победить" },
                }
            },
            [399] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 526, Text = "Удачливы" },
                    new Option { Destination = 109, Text = "Нет" },
                }
            },
            [400] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 529, Text = "Расскажете ему" },
                    new Option { Destination = 246, Text = "Прельстить деньгами" },
                }
            },
            [401] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 175, Text = "Обогнете озеро" },
                    new Option { Destination = 407, Text = "Покинете остров" },
                }
            },
            [402] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 239, Text = "Исследовать остатки цитадели" },
                    new Option { Destination = 15, Text = "Направиться прочь" },
                }
            },
            [403] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 243, Text = "Далее" },
                }
            },
            [404] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 69, Text = "Приблизитесь" },
                    new Option { Destination = 396, Text = "Предпочтете не терять времени" },
                }
            },
            [405] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 174, Text = "Остаться с Шелтоном" },
                    new Option { Destination = 373, Text = "Продолжить путешествие" },
                }
            },
            [406] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 582, Text = "Защитить Дельфина" },
                    new Option { Destination = 232, Text = "Плыть дальше" },
                }
            },
            [407] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 248, Text = "Первый" },
                    new Option { Destination = 16, Text = "Второй" },
                    new Option { Destination = 233, Text = "Третий" },
                    new Option { Destination = 165, Text = "Четвертый" },
                    new Option { Destination = 418, Text = "Пятый" },
                    new Option { Destination = 395, Text = "Шестой" },
                    new Option { Destination = 535, Text = "Седьмой" },
                    new Option { Destination = 100, Text = "Восьмой" },
                    new Option { Destination = 84, Text = "Девятый" },
                    new Option { Destination = 282, Text = "Одиннадцатый" },
                    new Option { Destination = 456, Text = "Двенадцатый" },
                    new Option { Destination = 500, Text = "Тринадцатый" },
                }
            },
            [408] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 234, Text = "Далее" },
                }
            },
            [409] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ДВОРЯНИН",
                                Skill = 10,
                                Strength = 12,
                            },
                        },

                        Aftertext = "Если он убит, то можете приблизиться к кораблю. Но, быть может, лучше уплыть от греха подальше?"
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 70, Text = "Приблизиться к кораблю" },
                    new Option { Destination = 380, Text = "Уплыть от греха подальше" },
                }
            },
            [410] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 370, Text = "Вернуться в порт" },
                    new Option { Destination = 102, Text = "Плыть дальше" },
                }
            },
            [411] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 172, Text = "Уйти из трюма" },
                    new Option { Destination = 619, Text = "Пойти дальше" },
                }
            },
            [412] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 25, Text = "Далее" },
                }
            },
            [413] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 241, Text = "В тот тоннель, из которого появились" },
                    new Option { Destination = 345, Text = "В ближайший от вас выход из зала" },
                }
            },
            [414] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 179, Text = "Оглядеться" },
                    new Option { Destination = 71, Text = "Уплыть в выход впереди" },
                    new Option { Destination = 345, Text = "Уплыть в выход  рядом" },
                }
            },
            [415] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 247, Text = "Денег нет" },
                    new Option { Destination = 3, Text = "Далее" },
                }
            },
            [416] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 173, Text = "Возьмете топор" },
                    new Option { Destination = 300, Text = "Возьмете арбалет" },
                    new Option { Destination = 59, Text = "Наденете доспехи" },
                }
            },
            [417] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 375, Text = "Далее" },
                }
            },
            [418] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 477, Text = "Уже встречались" },
                    new Option { Destination = 37, Text = "Нет" },
                }
            },
            [419] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 397, Text = "Рискнуть" },
                    new Option { Destination = 189, Text = "Плыть дальше" },
                }
            },
            [420] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 15, Text = "Далее" },
                }
            },
            [421] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 90, Text = "Исследовать пещеру" },
                    new Option { Destination = 523, Text = "Плыть куда глаза глядят" },
                }
            },
            [422] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 398, Text = "Направо" },
                    new Option { Destination = 285, Text = "Налево" },
                }
            },
            [423] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 276, Text = "Скрыться от Кита" },
                    new Option { Destination = 93, Text = "Ему навстречу" },
                }
            },
            [424] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 254, Text = "Надеть его" },
                    new Option { Destination = 345, Text = "Пыть по коридору справа" },
                    new Option { Destination = 71, Text = "Пыть по коридору, который перед вами" },
                }
            },
            [425] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Пусть сражаются",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            new Character
                            {
                                Name = "РЫБА-МОЛОТ",
                                Skill = 9,
                                Strength = 8,
                            },
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЯТНИСТАЯ АКУЛА",
                                Skill = 10,
                                Strength = 10,
                            },
                        },

                        Aftertext = "Хорошо, если Рыба-молот победила. Если же она мертва, придется вступить в бой вам.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 110, Text = "Рыба-молот победила" },
                    new Option { Destination = 268, Text = "Вступить в бой" },
                }
            },
            [426] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 94, Text = "Далее" },
                }
            },
            [427] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "РЫЦАРЬ-ВОДЯНОЙ",
                                Skill = 8,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 38, Text = "Далее" },
                }
            },
            [428] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 549, Text = "Далее" },
                }
            },
            [429] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 284, Text = "Далее" },
                }
            },
            [430] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ЭЛЕКТРИЧЕСКИЙ УГОРЬ",
                                Skill = 8,
                                Strength = 8,
                                ExtendedDamage = 2,
                            },
                        },

                        Aftertext = "Если победили его, то дальше путь лежит к странным развалинам, виднеющимся неподалеку.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 353, Text = "Далее" },
                }
            },
            [431] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 87, Text = "Далее" },
                }
            },
            [432] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 111, Text = "Удачливы" },
                    new Option { Destination = 298, Text = "Нет" },
                }
            },
            [433] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 402, Text = "К развалинам" },
                    new Option { Destination = 103, Text = "В открытое море" },
                }
            },
            [434] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 15, Text = "Далее" },
                }
            },
            [435] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 301, Text = "Приблизитесь к ним" },
                    new Option { Destination = 188, Text = "Поплывете обратно" },
                    new Option { Destination = 422, Text = "Пересечете пещеру" },
                }
            },
            [436] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 118, Text = "Есть" },
                    new Option { Destination = 318, Text = "Нет" },
                }
            },
            [437] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 117, Text = "Направо" },
                    new Option { Destination = 302, Text = "Налево" },
                }
            },
            [438] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 502, Text = "Воспользоваться опознавательным знаком" },
                    new Option { Destination = 518, Text = "Принять участие в состязании" },
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
                    new Option { Destination = 117, Text = "К подводному поселению" },
                    new Option { Destination = 302, Text = "К нелепому сооружению" },
                }
            },
            [442] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 93, Text = "Далее" },
                }
            },
            [443] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться с первым",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ РЫЦАРЬ-ВОДЯНОЙ",
                                Skill = 9,
                                Strength = 10,
                            },
                        },

                        Aftertext = "Если вам удалось убить противника за пять раундов атаки, то вы можете либо снова попытаться бежать, либо сразиться с двумя оставшимися (теперь уже одновременно). Если же через пять раундов атаки ваш враг еще жив, два других просто присоединяются к нему.",
                    },

                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться с остальными",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВТОРОЙ РЫЦАРЬ-ВОДЯНОЙ",
                                Skill = 8,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ТРЕТИЙ РЫЦАРЬ-ВОДЯНОЙ",
                                Skill = 7,
                                Strength = 9,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 201, Text = "Попытаться бежать" },
                    new Option { Destination = 414, Text = "Если вы победили их" },
                }
            },
            [444] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 361, Text = "Далее" },
                }
            },
            [445] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 181, Text = "Далее" },
                }
            },
            [446] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 519, Text = "Поплывете к нему" },
                    new Option { Destination = 250, Text = "К огромному подводному лесу" },
                }
            },
            [447] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 2, Text = "Прямо" },
                    new Option { Destination = 146, Text = "Направо" },
                }
            },
            [448] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 369, Text = "Далее" },
                }
            },
            [449] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ТРЕТИЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },
                    },

                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Пусть сражаются",

                        Allies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВОИН",
                                Skill = 11,
                                Strength = 14,
                            },
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВТОРОЙ ПИРАТ",
                                Skill = 9,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Далее" },
                }
            },
            [450] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 116, Text = "Согласитесь" },
                    new Option { Destination = 303, Text = "Откажетесь" },
                    new Option { Destination = 501, Text = "Отправитесь дальше" },
                }
            },
            [451] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "МОРСКОЙ ПАУК",
                                Skill = 9,
                                Strength = 9,
                            },
                        },

                        Aftertext = "Чудовище во много раз больше обычного паука и поэтому будет нелегким противником."
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 319, Text = "Далее" },
                }
            },
            [452] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 202, Text = "Поговорить с Карликом" },
                    new Option { Destination = 501, Text = "Поблагодарить его и отправиться дальше" },
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
                    new Option { Destination = 105, Text = "Далее" },
                }
            },
            [455] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 353, Text = "Далее" },
                }
            },
            [456] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 569, Text = "Покинуть остров" },
                    new Option { Destination = 207, Text = "Подняться на ближайший холм" },
                }
            },
            [457] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 629, Text = "Далее" },
                }
            },
            [458] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 320, Text = "Взять его с собой" },
                    new Option { Destination = 396, Text = "Плыть прочь из леса" },
                }
            },
            [459] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 212, Text = "Осмотреть ее" },
                    new Option { Destination = 509, Text = "Покинуть остров" },
                }
            },
            [460] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 530, Text = "Далее" },
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
                    new Option { Destination = 552, Text = "Спуститься и посмотреть" },
                    new Option { Destination = 204, Text = "Плыть дальше" },
                }
            },
            [463] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 15, Text = "Далее" },
                }
            },
            [464] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 471, Text = "Есть сушеный краб" },
                    new Option { Destination = 321, Text = "Распрощаться с хозяйкой и покинуть комнату" },
                }
            },
            [465] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 248, Text = "Первый" },
                    new Option { Destination = 16, Text = "Второй" },
                    new Option { Destination = 233, Text = "Третий" },
                    new Option { Destination = 165, Text = "Четвертый" },
                    new Option { Destination = 418, Text = "Пятый" },
                    new Option { Destination = 535, Text = "Седьмой" },
                    new Option { Destination = 100, Text = "Восьмой" },
                    new Option { Destination = 84, Text = "Девятый" },
                    new Option { Destination = 352, Text = "Десятый" },
                    new Option { Destination = 282, Text = "Одиннадцатый" },
                    new Option { Destination = 456, Text = "Двенадцатый" },
                    new Option { Destination = 500, Text = "Тринадцатый" },
                }
            },
            [466] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 539, Text = "Далее" },
                }
            },
            [467] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 553, Text = "Вернете перстень" },
                    new Option { Destination = 286, Text = "Будете драться" },
                }
            },
            [468] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 516, Text = "Далее" },
                }
            },
            [469] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 199, Text = "Далее" },
                }
            },
            [470] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Пусть сражаются",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ФИЛИН",
                                Skill = 9,
                                Strength = 9,
                            },
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "КОНДОР",
                                Skill = 9,
                                Strength = 9,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 429, Text = "Победил Филин" },
                    new Option { Destination = 570, Text = "Вступать в бой" },
                }
            },
            [471] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 79, Text = "Скажете, что да" },
                    new Option { Destination = 349, Text = "Ответите, что нет" },
                }
            },
            [472] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 586, Text = "Посмотреть, что это такое" },
                    new Option { Destination = 423, Text = "Поплывете в другую сторону" },
                }
            },
            [473] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 159, Text = "Направиться туда и выяснить, кто это был" },
                    new Option { Destination = 250, Text = "Поплывете в другую сторону" },
                }
            },
            [474] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                        Aftertext = "Если вам не повезло — можете либо перейти к правому сундуку (если вы этого еще не делали), либо уплыть прочь"
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 139, Text = "Если вам повезло" },
                    new Option { Destination = 43, Text = "Перейти к правому сундуку" },
                    new Option { Destination = 508, Text = "Уплыть прочь" },
                }
            },
            [475] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 305, Text = "Далее" },
                }
            },
            [476] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 329, Text = "Далее" },
                }
            },
            [477] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 92, Text = "Есть кольцо с трезубцем" },
                    new Option { Destination = 571, Text = "Нет" },
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
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 65, Text = "Удачливы" },
                    new Option { Destination = 580, Text = "Нет" },
                }
            },
            [480] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Пусть сражаются",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            new Character
                            {
                                Name = "РЫЦАРЬ-ВОДЯНОЙ",
                                Skill = 10,
                                Strength = 9,
                            },
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ ПИРАТ",
                                Skill = 9,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ТРЕТИЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },

                        Aftertext = "Водяному придется сражаться с тремя врагами одновременно. Если он победит их, может с чистой совестью уплыть домой, а если нет, — в бой вступать вам.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Если он победил их" },
                    new Option { Destination = 583, Text = "Вступить в бой" },
                }
            },
            [481] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 422, Text = "Далее" },
                }
            },
            [482] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 593, Text = "Поверите ему и отдадите изумруды" },
                    new Option { Destination = 322, Text = "Притворитесь, что пошутили" },
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
                    new Option { Destination = 461, Text = "Далее" },
                }
            },
            [485] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 195, Text = "Далее" },
                }
            },
            [486] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Пусть сражаются",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            new Character
                            {
                                Name = "КАПИТАН",
                                Skill = 10,
                                Strength = 14,
                            },
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ ПИРАТ",
                                Skill = 9,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ТРЕТИЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },

                        Aftertext = "В случае победы капитана над пиратами, если же они его убили, то дальше сражаться придется вам.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 488, Text = "Капитан победил" },
                    new Option { Destination = 583, Text = "Сражаться" },
                }
            },
            [487] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 80, Text = "Помочь ему" },
                    new Option { Destination = 350, Text = "Пусть сражается один" },
                }
            },
            [488] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Далее" },
                }
            },
            [489] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 605, Text = "Надеть его" },
                    new Option { Destination = 126, Text = "Плыть дальше" },
                }
            },
            [490] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВАЯ ПИРАНЬЯ",
                                Skill = 10,
                                Strength = 10,
                            },
                            new Character
                            {
                                Name = "ВТОРАЯ ПИРАНЬЯ",
                                Skill = 10,
                                Strength = 10,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 591, Text = "Есть амулет" },
                    new Option { Destination = 592, Text = "Обе рыбины мертвы2" },
                }
            },
            [491] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Далее" },
                }
            },
            [492] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 67, Text = "Исследовать пещеру" },
                    new Option { Destination = 302, Text = "Плыть дальше" },
                }
            },
            [493] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 234, Text = "К красивым морским цветам впереди" },
                    new Option { Destination = 48, Text = "В открытое море" },
                }
            },
            [494] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Пусть сражаются",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ГРИФ",
                                Skill = 8,
                                Strength = 10,
                            },
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "КОНДОР",
                                Skill = 9,
                                Strength = 9,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 429, Text = "Гриф победил Кондора" },
                    new Option { Destination = 570, Text = "Вступить в бой" },
                }
            },
            [495] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 105, Text = "Извинившись, вернитесь обратно" },
                    new Option { Destination = 31, Text = "Проплывёте прямо" },
                }
            },
            [496] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 67, Text = "Исследовать пещеру" },
                    new Option { Destination = 302, Text = "К какому-то строению" },
                }
            },
            [497] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 304, Text = "Далее" },
                }
            },
            [498] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 577, Text = "Бой продолжаете вы" },
                    new Option { Destination = 198, Text = "Если победа за вами" },
                }
            },
            [499] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 594, Text = "Бой продолжаете вы"  },
                    new Option { Destination = 198, Text = "Если победа за вами" },
                }
            },
            [500] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 183, Text = "Далее" },
                }
            },
            [501] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 279, Text = "Приблизитесь к нему" },
                    new Option { Destination = 396, Text = "Направитесь к выходу из леса" },
                }
            },
            [502] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 312, Text = "Далее" },
                }
            },
            [503] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 202, Text = "Поговорить с Карликом" },
                    new Option { Destination = 501, Text = "Поблагодарите его и отправитесь дальше" },
                }
            },
            [504] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 236, Text = "Прямо" },
                    new Option { Destination = 422, Text = "Направо" },
                }
            },
            [505] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 422, Text = "Далее" },
                }
            },
            [506] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 443, Text = "Далее" },
                }
            },
            [507] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "РЫБА-МОЛОТ",
                                Skill = 9,
                                Strength = 8,
                            },
                        },

                        Aftertext = "Если удалось победить ее, можете продолжать путь."
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 32, Text = "Далее" },
                }
            },
            [508] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 231, Text = "Далее" },
                }
            },
            [509] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 248, Text = "Первый" },
                    new Option { Destination = 16, Text = "Второй" },
                    new Option { Destination = 233, Text = "Третий" },
                    new Option { Destination = 165, Text = "Четвертый" },
                    new Option { Destination = 418, Text = "Пятый" },
                    new Option { Destination = 395, Text = "Шестой" },
                    new Option { Destination = 535, Text = "Седьмой" },
                    new Option { Destination = 100, Text = "Восьмой" },
                    new Option { Destination = 84, Text = "Девятый" },
                    new Option { Destination = 352, Text = "Десятый" },
                    new Option { Destination = 456, Text = "Двенадцатый" },
                    new Option { Destination = 500, Text = "Тринадцатый" },
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
                    new Option { Destination = 322, Text = "Далее" },
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
                    new Option { Destination = 208, Text = "Далее" },
                }
            },
            [514] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 126, Text = "К нему" },
                    new Option { Destination = 462, Text = "От него" },
                    new Option { Destination = 206, Text = "Не менять направление" },
                }
            },
            [515] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Далее" },
                }
            },
            [516] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 311, Text = "Уговорить ее" },
                    new Option { Destination = 211, Text = "Убить ее" },
                }
            },
            [517] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 60, Text = "Помочь ему" },
                    new Option { Destination = 486, Text = "Предоставите сражаться с пиратами в одиночку" },
                }
            },
            [518] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 323, Text = "Если выпавшее число меньше" },
                    new Option { Destination = 128, Text = "Если больше или равно" },
                }
            },
            [519] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ЭЛЕКТРИЧЕСКИЙ СКАТ",
                                Skill = 8,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 153, Text = "Уплыть от него" },
                    new Option { Destination = 64, Text = "Вы его убили" },
                }
            },
            [520] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 397, Text = "Приблизиться к ней и пообедать " },
                    new Option { Destination = 269, Text = "Плыть дальше" },
                }
            },
            [521] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 481, Text = "Удачливы" },
                    new Option { Destination = 130, Text = "Нет" },
                }
            },
            [522] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 243, Text = "Далее" },
                }
            },
            [523] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 17, Text = "Поговорить со стариком" },
                    new Option { Destination = 581, Text = "Узнать, куда он может отвезти" },
                    new Option { Destination = 247, Text = "Назовете дорогу сами" },
                    new Option { Destination = 88, Text = "Убьете его" },
                }
            },
            [524] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 376, Text = "Нагнать его и поговорить с капитаном" },
                    new Option { Destination = 95, Text = "Продолжать плыть" },
                }
            },
            [525] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 358, Text = "Далее" },
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
                    new Option { Destination = 281, Text = "Выполните просьбу погибших моряков" },
                    new Option { Destination = 433, Text = "Проплывете мимо" },
                }
            },
            [528] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 353, Text = "Далее" },
                }
            },
            [529] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 18, Text = "Далее" },
                }
            },
            [530] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 248, Text = "Первый" },
                    new Option { Destination = 16, Text = "Второй" },
                    new Option { Destination = 233, Text = "Третий" },
                    new Option { Destination = 165, Text = "Четвертый" },
                    new Option { Destination = 418, Text = "Пятый" },
                    new Option { Destination = 395, Text = "Шестой" },
                    new Option { Destination = 535, Text = "Седьмой" },
                    new Option { Destination = 100, Text = "Восьмой" },
                    new Option { Destination = 352, Text = "Десятый" },
                    new Option { Destination = 282, Text = "Одиннадцатый" },
                    new Option { Destination = 456, Text = "Двенадцатый" },
                    new Option { Destination = 500, Text = "Тринадцатый" },
                }
            },
            [531] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 280, Text = "Отдадите ему деньги" },
                    new Option { Destination = 365, Text = "Поплывете дальше" },
                }
            },
            [532] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 182, Text = "Далее" },
                }
            },
            [533] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ РЫЦАРЬ-ВОДЯНОЙ",
                                Skill = 8,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ РЫЦАРЬ-ВОДЯНОЙ",
                                Skill = 7,
                                Strength = 9,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 414, Text = "Далее" },
                }
            },
            [534] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Пусть сражаются",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            new Character
                            {
                                Name = "МОРЯК",
                                Skill = 10,
                                Strength = 12,
                            },
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ВОИН",
                                Skill = 10,
                                Strength = 10,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ ВОИН",
                                Skill = 10,
                                Strength = 10,
                            },
                        },

                        Aftertext = "В случае гибели моряка сражение придется продолжить самому, если же он перебил врагов, то исчезнет, кивнув на прощание и не дожидаясь вашей благодарности."
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 63, Text = "Продолжить самому" },
                    new Option { Destination = 217, Text = "Если он перебил врагов" },
                }
            },
            [535] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 191, Text = "Если рыбки нет" },
                    new Option { Destination = 324, Text = "Убраться с острова подобру-поздорову" },
                }
            },
            [536] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 279, Text = "Приблизитесь к нему" },
                    new Option { Destination = 396, Text = "К выходу из леса" },
                }
            },
            [537] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 397, Text = "Рискнуть" },
                    new Option { Destination = 189, Text = "Плыть дальше" },
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
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "МОРСКАЯ ИГЛА",
                                Skill = 8,
                                Strength = 7,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 214, Text = "Далее" },
                }
            },
            [540] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 248, Text = "Первый" },
                    new Option { Destination = 16, Text = "Второй" },
                    new Option { Destination = 233, Text = "Третий" },
                    new Option { Destination = 165, Text = "Четвертый" },
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
            [541] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 447, Text = "Далее" },
                }
            },
            [542] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 304, Text = "Далее" },
                }
            },
            [543] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 34, Text = "Далее" },
                }
            },
            [544] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 436, Text = "Совет" },
                    new Option { Destination = 620, Text = "Удачу" },
                }
            },
            [545] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВАЯ БАРРАКУДА",
                                Skill = 11,
                                Strength = 15,
                            },
                            new Character
                            {
                                Name = "ВТОРАЯ БАРРАКУДА",
                                Skill = 11,
                                Strength = 15,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 277, Text = "Далее" },
                }
            },
            [546] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 537, Text = "Далее" },
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
                    new Option { Destination = 365, Text = "Отправитесь дальше" },
                    new Option { Destination = 523, Text = "Поступите по-своему" },
                }
            },
            [549] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 275, Text = "Посмотреть, что это такое" },
                    new Option { Destination = 15, Text = "Вперед к руинам" },
                }
            },
            [550] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 69, Text = "Посмотреть на цветы и послушать музыку" },
                    new Option { Destination = 396, Text = "К выходу из леса" },
                    new Option { Destination = 101, Text = "В глубь его" },
                }
            },
            [551] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 382, Text = "К симпатичному коралловому рифу" },
                    new Option { Destination = 234, Text = "К скоплению прекрасных подводных цветов" },
                }
            },
            [552] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 129, Text = "Надеть обруч на голову" },
                    new Option { Destination = 617, Text = "Оставите лежать где лежал" },
                }
            },
            [553] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 501, Text = "Далее" },
                }
            },
            [554] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 236, Text = "Далее" },
                }
            },
            [555] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 248, Text = "Первый" },
                    new Option { Destination = 16, Text = "Второй" },
                    new Option { Destination = 233, Text = "Третий" },
                    new Option { Destination = 165, Text = "Четвертый" },
                    new Option { Destination = 418, Text = "Пятый" },
                    new Option { Destination = 395, Text = "Шестой" },
                    new Option { Destination = 535, Text = "Седьмой" },
                    new Option { Destination = 84, Text = "Девятый" },
                    new Option { Destination = 352, Text = "Десятый" },
                    new Option { Destination = 282, Text = "Одиннадцатый" },
                    new Option { Destination = 456, Text = "Двенадцатый" },
                    new Option { Destination = 500, Text = "Тринадцатый" },
                }
            },
            [556] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 261, Text = "Далее" },
                }
            },
            [557] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 474, Text = "Заняться вторым сундуком" },
                    new Option { Destination = 508, Text = "Плыть дальше" },
                }
            },
            [558] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 462, Text = "Дальше минуя риф" },
                    new Option { Destination = 516, Text = "Дальше огибая его" },
                }
            },
            [559] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 236, Text = "Далее" },
                }
            },
            [560] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 504, Text = "Далее" },
                }
            },
            [561] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 612, Text = "Далее" },
                }
            },
            [562] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 41, Text = "Далее" },
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
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Пусть сражаются",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            new Character
                            {
                                Name = "КАПИТАН",
                                Skill = 10,
                                Strength = 14,
                            },
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ВОИН",
                                Skill = 10,
                                Strength = 10,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ ВОИН",
                                Skill = 10,
                                Strength = 10,
                            },
                        },

                        Aftertext = "В случае победы капитана над пиратами, если же они его убили, то дальше сражаться придется вам.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 478, Text = "Капитан победил" },
                    new Option { Destination = 476, Text = "Призвать другого союзника" },
                    new Option { Destination = 329, Text = "Принять удар на себя" },
                }
            },
            [565] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВАЯ БАРРАКУДА",
                                Skill = 11,
                                Strength = 15,
                            },
                            new Character
                            {
                                Name = "ВТОРАЯ БАРРАКУДА",
                                Skill = 11,
                                Strength = 15,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 62, Text = "Ваша сила уменьшилась до четырёх" },
                    new Option { Destination = 277, Text = "Вы победили морских хищниц" },
                }
            },
            [566] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "АКУЛА-ЛЮДОЕД",
                                Skill = 9,
                                Strength = 11,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 134, Text = "Скрыться от нее" },
                    new Option { Destination = 82, Text = "Гигантская акула повержена" },
                }
            },
            [567] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 472, Text = "Далее" },
                }
            },
            [568] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 333, Text = "Вы уже открывали бутылку" },
                    new Option { Destination = 47, Text = "Нет" },
                }
            },
            [569] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 248, Text = "Первый" },
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
                    new Option { Destination = 500, Text = "Тринадцатый" },
                }
            },
            [570] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "КОНДОР",
                                Skill = 9,
                                Strength = 9,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 429, Text = "Далее" },
                }
            },
            [571] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 540, Text = "Далее" },
                }
            },
            [572] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 56, Text = "Далее" },
                }
            },
            [573] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 559, Text = "Далее" },
                }
            },
            [574] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "КРЫЛАТЫЙ ЛЕВ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },

                        Aftertext = "Если удалось выйти из схватки победителем, можете либо покинуть остров, проведя на нем 40 минут, либо, соблюдая все меры предосторожности, вернуться в логово Льва и осмотреть его."
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 324, Text = "Покинуть остров" },
                    new Option { Destination = 61, Text = "Вернуться в логово Льва и осмотреть его" },
                }
            },
            [575] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 475, Text = "Посмотреть, что там находится" },
                    new Option { Destination = 135, Text = "Не станете рисковать" },
                }
            },
            [576] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 377, Text = "Удачливы" },
                    new Option { Destination = 112, Text = "Нет" },
                }
            },
            [577] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Пусть сражаются",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            new Character
                            {
                                Name = "РЫЦАРЬ-ВОДЯНОЙ",
                                Skill = 10,
                                Strength = 9,
                            },
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },

                        Aftertext = "И вторая группа:",
                    },

                   new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВТОРОЙ ПИРАТ",
                                Skill = 9,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ТРЕТИЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },
                   },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 137, Text = "Рыцарь победил, а ваши противники еще живы" },
                    new Option { Destination = 498, Text = "Остался в живых только один" },
                    new Option { Destination = 198, Text = "Все враги мертвы" },
                }
            },
            [578] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 32, Text = "Далее" },
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
                    new Option { Destination = 219, Text = "Есть трезубец" },
                    new Option { Destination = 189, Text = "Плыть дальше" },
                }
            },
            [581] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 365, Text = "Далее" },
                }
            },
            [582] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "АКУЛА",
                                Skill = 10,
                                Strength = 16,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 367, Text = "Далее" },
                }
            },
            [583] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ ПИРАТ",
                                Skill = 9,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ТРЕТИЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Далее" },
                }
            },
            [584] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 338, Text = "Взять золотой пояс" },
                    new Option { Destination = 74, Text = "Взять браслет" },
                    new Option { Destination = 508, Text = "Плыть дальше" },
                }
            },
            [585] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 14, Text = "Быстро захлопнуть дверь" },
                    new Option { Destination = 170, Text = "Принять вызов" },
                }
            },
            [586] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 144, Text = "Приблизиться" },
                    new Option { Destination = 336, Text = "Миновать его стороной" },
                }
            },
            [587] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 217, Text = "Далее" },
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
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ТРЕТИЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },

                        Aftertext = "А также:",
                    },

                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Пусть сражаются",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            new Character
                            {
                                Name = "МОРЯК",
                                Skill = 10,
                                Strength = 12,
                            },
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВТОРОЙ ПИРАТ",
                                Skill = 9,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Далее" },
                }
            },
            [590] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 343, Text = "Далее" },
                }
            },
            [591] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 490, Text = "Далее" },
                }
            },
            [592] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 225, Text = "Есть раковина" },
                    new Option { Destination = 302, Text = "К чему-то очень похожему на мельницу" },
                }
            },
            [593] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 465, Text = "Далее" },
                }
            },
            [594] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВТОРОЙ ПИРАТ",
                                Skill = 9,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ТРЕТИЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },

                        Aftertext = "А во второй:",
                    },

                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Пусть сражаются",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            new Character
                            {
                                Name = "МОРСКОЙ РЫЦАРЬ",
                                Skill = 11,
                                Strength = 12,
                            },
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Все враги мертвы" },
                    new Option { Destination = 226, Text = "Ваш союзник убил пирата,в оба ваши врага живы" },
                    new Option { Destination = 499, Text = "Если живи только один враг" },
                }
            },
            [595] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 214, Text = "Далее" },
                }
            },
            [596] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВЕЛИКАН",
                                Skill = 12,
                                Strength = 12,
                            },
                        },

                        Aftertext = "Когда ВЫНОСЛИВОСТЬ вашего противника уменьшена до 2, удар грома потрясает остров..."
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 99, Text = "Далее" },
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
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {

                            new Character
                            {
                                Name = "ТРЕТИЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },

                        Aftertext = "А также:",
                    },

                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Пусть сражаются",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            new Character
                            {
                                Name = "НОСОРОГ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВТОРОЙ ПИРАТ",
                                Skill = 9,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Далее" },
                }
            },
            [599] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 123, Text = "Далее" },
                }
            },
            [600] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Пусть сражаются",
                        GroupFight = true,

                        Allies = new List<Character>
                        {
                            new Character
                            {
                                Name = "КРЫЛАТЫЙ ЛЕВ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },

                        Aftertext = "А во второй:",
                    },

                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Allies = new List<Character>
                        {
                            Character.Protagonist
                        },

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВТОРОЙ ПИРАТ",
                                Skill = 9,
                                Strength = 8,
                            },
                            new Character
                            {
                                Name = "ТРЕТИЙ ПИРАТ",
                                Skill = 10,
                                Strength = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 227, Text = "Лев победил, а у вас остался один враг" },
                    new Option { Destination = 238, Text = "Оба врага целы" },
                    new Option { Destination = 198, Text = "Все враги будут уничтожены" },
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
                    new Option { Destination = 105, Text = "Прямо" },
                    new Option { Destination = 31, Text = "Направо" },
                }
            },
            [603] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 208, Text = "Далее" },
                }
            },
            [604] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 336, Text = "Направо" },
                    new Option { Destination = 566, Text = "Налево" },
                }
            },
            [605] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 126, Text = "Далее" },
                }
            },
            [606] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 592, Text = "Далее" },
                }
            },
            [607] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Далее" },
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
                    new Option { Destination = 305, Text = "Далее" },
                }
            },
            [611] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 426, Text = "Удачливы" },
                    new Option { Destination = 588, Text = "Нет" },
                }
            },
            [612] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 520, Text = "Плыть дальше" },
                    new Option { Destination = 243, Text = "Повернуть в противоположную сторону" },
                }
            },
            [613] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 437, Text = "Далее" },
                }
            },
            [614] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 471, Text = "Отдать старухе" },
                    new Option { Destination = 321, Text = "Попрощаться и уплыть из комнаты" },
                }
            },
            [615] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 168, Text = "Далее" },
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
                    new Option { Destination = 204, Text = "Далее" },
                }
            },
            [618] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 441, Text = "Спрыгнете с него" },
                    new Option { Destination = 215, Text = "Посмотрите, куда он вас привезет" },
                }
            },
            [619] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 8, Text = "Далее" },
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
                    new Option { Destination = 417, Text = "Далее" },
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
                    new Option { Destination = 71, Text = "Вперёд" },
                    new Option { Destination = 345, Text = "Направо" },
                }
            },
            [624] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 258, Text = "Далее" },
                }
            },
            [625] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Далее" },
                }
            },
            [626] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 422, Text = "Далее" },
                }
            },
            [627] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 501, Text = "Далее" },
                }
            },
            [628] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 328, Text = "Далее" },
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
