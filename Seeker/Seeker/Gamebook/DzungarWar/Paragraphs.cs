using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Seeker.Game;

namespace Seeker.Gamebook.DzungarWar
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
            paragraph.LateTrigger = source.LateTrigger;

            return paragraph;
        }

        private static Dictionary<int, Paragraph> Paragraph = new Dictionary<int, Paragraph>
        {
            [0] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 658, Text = "В путь!" },
                }
            },
            [1] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 115, Text = "Далее" },
                }
            },
            [2] = new Paragraph
            {
                Trigger = "Боль",

                Options = new List<Option>
                {
                    new Option { Destination = 373, Text = "На юг в Кульджу" },
                    new Option { Destination = 649, Text = "На север в Семиречье" },
                }
            },
            [3] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 9,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 12, Text = "В случае успеха" },
                    new Option { Destination = 22, Text = "В случае провала" },
                }
            },
            [4] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 12,
                    },
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить силу",
                        Stat = "Strength",
                        Level = 12,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 350, Text = "Если обе проверки успешны" },
                    new Option { Destination = 316, Text = "В любом другом случае" },

                    new Option {
                        Destination = 368,
                        Text = "Достать 100 ТАНЬГА из кошеля",
                        OnlyIf = "ТАНЬГА >= 100",

                        Do = new Modification
                        {
                            Name = "Tanga",
                            Value = -100,
                        },
                    },
                }
            },
            [5] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 465, Text = "С полковником Риддером" },
                    new Option { Destination = 429, Text = "С отцом Ионой" },
                    new Option { Destination = 514, Text = "Посетить уйгурское поселение и кульджинский базар" },
                    new Option { Destination = 536, Text = "Попроситься на приём к китайскому наместнику" },
                    new Option { Destination = 545, Text = "Покинуть Кульджу и отправиться дальше" },
                }
            },
            [6] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 101, Text = "Подождать главу кочевья Алтынбая в юрте" },
                    new Option { Destination = 85, Text = "Посмотреть на объездку лошадей" },
                    new Option { Destination = 93, Text = "Отправиться на поединок акынов" },
                }
            },
            [7] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 507, Text = "Покрутить молитвенную мельницу" },
                    new Option { Destination = 399, Text = "Пройти внутрь, не трогая её" },
                }
            },
            [8] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 53, Text = "Напасть на джунгаров" },
                    new Option { Destination = 63, Text = "Попытаться запугать их" },
                    new Option { Destination = 91, Text = "Заплатить 100 ТАНЬГА", OnlyIf = "ТАНЬГА >= 100" },
                    new Option { Destination = 42, Text = "Развернуться и уехать" },
                }
            },
            [9] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 3,
                    },
                },

                Trigger = "Взрыв",

                Options = new List<Option>
                {
                    new Option { Destination = 453, Text = "Далее" },
                }
            },
            [10] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 230, Text = "Рассказать Адильжану о задании хана" },
                    new Option { Destination = 258, Text = "Согласиться помочь ему, не раскрывая цели своего путешествия" },
                    new Option { Destination = 293, Text = "Вежливо отказать Адильжану, ссылаясь на важные и срочные дела, и расспросить его об обитателях Аягуза" },
                }
            },
            [11] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 575, Text = "Далее" },
                }
            },
            [12] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 87, Text = "Далее" },
                }
            },
            [13] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 262, Text = "Если отмечены оба слова: «Пастух» и «Дружба»", OnlyIf = "Дружба" },
                    new Option { Destination = 241, Text = "Если не отмечено слово «Пастух», но отмечено слово «Дружба»", OnlyIf = "Дружба" },
                    new Option { Destination = 218, Text = "В случае успеха" },
                    new Option { Destination = 233, Text = "В случае провала" },
                }
            },
            [14] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 12,
                        TriggerTestPenalty = "Арбалет, -4",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 50, Text = "В случае успеха" },
                    new Option { Destination = 285, Text = "В случае провала" },
                }
            },
            [15] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 409, Text = "Далее" },
                }
            },
            [16] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 629, Text = "Далее" },
                }
            },
            [17] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 183, Text = "Если отмечено ключевое слово «Рассвет»" },
                    new Option { Destination = 207, Text = "Если же это слово не отмечено" },
                }
            },
            [18] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 11,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 43, Text = "В случае успеха" },
                    new Option { Destination = 61, Text = "В случае провала" },
                }
            },
            [19] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 362, Text = "Вызвать главаря на поединок" },
                    new Option { Destination = 380, Text = "Заставить джунгаров покинуть эти места каким-нибудь другим способом" },
                    new Option { Destination = 30, Text = "Не ввязываться в перепалку и продолжить исследование этих мест" },
                    new Option { Destination = 42, Text = "Развернуться и покинуть озеро" },
                }
            },
            [20] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 31, Text = "В случае успеха" },
                    new Option { Destination = 51, Text = "В случае провала" },
                }
            },
            [21] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 458, Text = "Если отмечено ключевое слово «Сведения»" },
                    new Option { Destination = 470, Text = "Если отмечено ключевое слово «Оружейник»", OnlyIf = "Оружейник" },
                    new Option { Destination = 483, Text = "Если отмечено ключевое слово «Порох»" },
                    new Option { Destination = 511, Text = "Кроме того, можно наведаться в харчевню, чтобы там разузнать что-нибудь" },
                    new Option { Destination = 497, Text = "Если же Алдар Косе считает, что узнал достаточно" },
                }
            },
            [22] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 87, Text = "Далее" },
                }
            },
            [23] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 5,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 132, Text = "В случае успеха" },
                    new Option { Destination = 177, Text = "В случае провала" },
                }
            },
            [24] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 12,
                    },
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 12,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 382, Text = "Если обе проверки успешны" },
                    new Option { Destination = 316, Text = "В любом другом случае" },

                    new Option {
                        Destination = 368,
                        Text = "Достать 50 ТАНЬГА из кошеля",
                        OnlyIf = "ТАНЬГА >= 50",

                        Do = new Modification
                        {
                            Name = "Tanga",
                            Value = -50,
                        },
                    },
                }
            },
            [25] = new Paragraph
            {
                Trigger = "Крепость",

                Options = new List<Option>
                {
                    new Option { Destination = 5, Text = "Далее" },
                }
            },
            [26] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 56, Text = "Вскрыть сундук ножом" },
                    new Option { Destination = 72, Text = "Поднять его и бросить на пол, в надежде, что сундук расколется" },
                    new Option { Destination = 94, Text = "Обследовать сундук в поисках другого способа" },
                }
            },
            [27] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить силу",
                        Stat = "Strength",
                        Level = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 519, Text = "В случае успеха" },
                    new Option { Destination = 526, Text = "В случае провала" },
                }
            },
            [28] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 287, Text = "Далее" },
                }
            },
            [29] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 384, Text = "Если отмечено ключевое слово «Логово»", OnlyIf = "Логово" },
                    new Option { Destination = 395, Text = "Иначе" },
                }
            },
            [30] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 332, Text = "Воспользоваться арбалетом", OnlyIf = "Арбалет" },
                    new Option { Destination = 321, Text = "Выстрелить из пистолета" },
                    new Option { Destination = 308, Text = "Достать нож и спрыгнуть с коня, прикрывая его от нападения" },
                    new Option { Destination = 295, Text = "Попробовать задобрить камышового кота" },
                }
            },
            [31] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 325, Text = "Притворяться и дальше джунгарским воином" },
                    new Option { Destination = 335, Text = "Рассказать правду" },
                }
            },
            [32] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 13,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 167, Text = "В случае успеха" },
                    new Option { Destination = 285, Text = "В случае провала" },
                }
            },
            [33] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 15, Text = "Если отмечено одно из ключевых слов: «Уйгур» или «Цветок»" },
                    new Option { Destination = 24, Text = "Если ни одно из этих слов не отмечено" },
                }
            },
            [34] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 201, Text = "Далее" },
                }
            },
            [35] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 90, Text = "Если отмечено ключевое слово «Казах»", OnlyIf = "Казах" },
                    new Option { Destination = 84, Text = "В противном случае" },
                }
            },
            [36] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 575, Text = "Далее" },
                }
            },
            [37] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 87, Text = "Далее" },
                }
            },
            [38] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [39] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 20, Text = "Далее" },
                }
            },
            [40] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 62, Text = "Попытаться убедить конокрадов разумными доводами" },
                    new Option { Destination = 95, Text = "Сыграть на честолюбии цыган" },
                    new Option { Destination = 126, Text = "Запугать разбойников" },
                }
            },
            [41] = new Paragraph
            {
                Trigger = "Коварство",

                Options = new List<Option>
                {
                    new Option { Destination = 588, Text = "Если отмечено ключевое слово «Перевал»" },
                    new Option { Destination = 578, Text = "Иначе" },
                }
            },
            [42] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 100, Text = "Отправиться на юго-запад, в ставку хунтайши" },
                    new Option { Destination = 60, Text = "На запад, в сторону предполагаемой стоянки" },
                    new Option { Destination = 147, Text = "На юг, к озеру Алаколь" },
                }
            },
            [43] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 133, Text = "Далее" },
                }
            },
            [44] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 201, Text = "Проследовать к поселению Аягуз, принимая меры предосторожности" },
                    new Option { Destination = 279, Text = "Переправиться на другой берег и продолжить основное задание" },
                }
            },
            [45] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 497, Text = "Далее" },
                }
            },
            [46] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 33, Text = "Если отмечено ключевое слово «Хищник»" },
                    new Option { Destination = 4, Text = "Если это слово не отмечено" },
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
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 12,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 268, Text = "В случае успеха" },
                    new Option { Destination = 278, Text = "В случае провала" },
                }
            },
            [49] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 67, Text = "Если отмечено ключевое слово «(Сведения»" },
                    new Option { Destination = 79, Text = "Если это слово не отмечено" },
                }
            },
            [50] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 74, Text = "Воспользоваться арбалетом", OnlyIf = "Арбалет" },
                    new Option { Destination = 59, Text = "Если отмечено хотя бы одно из ключевых слов: «Пленник», «Переписка» или «Святыня» - то Алдар знает, о чём он будет докладывать хунтайши" },
                    new Option { Destination = 66, Text = "Иначе придётся сделать донесение о «казахском лазутчике»" },
                    new Option { Destination = 453, Text = "И конечно, ничто не мешает отступиться от замысла и прямо сейчас покинуть ставку" },
                }
            },
            [51] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 325, Text = "Притворяться джунгарским воином и дальше" },
                    new Option { Destination = 335, Text = "Рассказать правду" },
                }
            },
            [52] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 12,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 82, Text = "В случае успеха" },
                    new Option { Destination = 41, Text = "В случае провала джигит вынужден покинуть караван. Контрабандисты уже начинают проявлять нетерпение" },
                }
            },
            [53] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 75, Text = "В случае успеха" },
                    new Option { Destination = 83, Text = "В случае провала" },
                }
            },
            [54] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [55] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 251, Text = "Обезвредить пастуха с помощью ножа" },
                    new Option { Destination = 320, Text = "Вступить с ним в разговор, в надежде привлечь его на свою сторону" },
                }
            },
            [56] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 11,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 117, Text = "В случае успеха" },
                    new Option { Destination = 107, Text = "В случае провала" },
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
                    new Option { Destination = 496, Text = "Отправиться на юг, к ущелью Джунгарские ворота" },
                    new Option { Destination = 100, Text = "На запад, к ставке хунтайши" },
                }
            },
            [59] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 80, Text = "Далее" },
                }
            },
            [60] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 6, Text = "Наведаться в кочевье, представившись казахским путником" },
                    new Option { Destination = 39, Text = "Поехать туда, притворившись джунгаром" },
                    new Option { Destination = 77, Text = "Отказаться от посещения кочевья и продолжить свой путь" },
                }
            },
            [61] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 654, Text = "Далее" },
                }
            },
            [62] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 12,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 141, Text = "В случае успеха" },
                    new Option { Destination = 149, Text = "В случае провала" },
                }
            },
            [63] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 103, Text = "В случае успеха" },
                    new Option { Destination = 112, Text = "В случае провала" },
                }
            },
            [64] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 104, Text = "Если у Алдара достаточно денег" },
                    new Option { Destination = 41, Text = "Иначе придётся продолжать путь в одиночку" },
                }
            },
            [65] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 201, Text = "Проследовать к поселению Аягуз, принимая меры предосторожности" },
                    new Option { Destination = 279, Text = "Переправиться на другой берег и продолжить основное задание" },
                }
            },
            [66] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 14,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 88, Text = "В случае успеха" },
                    new Option { Destination = 98, Text = "В случае провала" },
                }
            },
            [67] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 330, Text = "В случае успеха" },
                    new Option { Destination = 345, Text = "В случае провала" },
                }
            },
            [68] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 90, Text = "Если отмечено ключевое слово «Казах»", OnlyIf = "Казах" },
                    new Option { Destination = 84, Text = "В противном случае" },
                }
            },
            [69] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 297, Text = "Далее" },
                }
            },
            [70] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = 50,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 373, Text = "На юг в Кульджу" },
                    new Option { Destination = 649, Text = "На север в Семиречье" },
                }
            },
            [71] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 46, Text = "Остаться в пещере отшельника" },
                    new Option { Destination = 58, Text = "Выбраться из пещеры, забрать коня на стоянке и отправиться дальше" },
                }
            },
            [72] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить силу",
                        Stat = "Strength",
                        Level = 11,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 136, Text = "В случае успеха" },
                    new Option { Destination = 107, Text = "В случае провала" },
                }
            },
            [73] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = -10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 279, Text = "Далее" },
                }
            },
            [74] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [75] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 236, Text = "Далее" },
                }
            },
            [76] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 527, Text = "Десять или выше" },
                    new Option { Destination = 512, Text = "Восемь или девять" },
                    new Option { Destination = 566, Text = "Шесть или семь" },
                    new Option { Destination = 476, Text = "От нуля до пяти" },
                    new Option { Destination = 583, Text = "Ниже нуля" },
                }
            },
            [77] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 100, Text = "Отправиться на юг, в ставку хунтайши" },
                    new Option { Destination = 113, Text = "На запад, к джунгарским кочевьям" },
                    new Option { Destination = 138, Text = "На восток, к озеру Сассык-Коль" },
                    new Option { Destination = 147, Text = "На юго-восток, к озеру Алаколь" },
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
                    new Option { Destination = 318, Text = "Ответить, что наш герой - джунгарский воин, прибывший в эти места по поручению" },
                    new Option { Destination = 309, Text = "Притвориться простым путником" },
                }
            },
            [80] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 106, Text = "Воспользоваться арбалетом", OnlyIf = "Арбалет" },
                    new Option { Destination = 114, Text = "Если это слово не отмечено" },
                }
            },
            [81] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 143, Text = "Попытаться подстрелить всадника из лука" },
                    new Option { Destination = 97, Text = "Выстрелить в коня" },
                    new Option { Destination = 23, Text = "Обхитрить джунгара" },
                }
            },
            [82] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 118, Text = "Положиться на Зейналду и поискать себе укромное место для засады" },
                    new Option { Destination = 131, Text = "Придумать и предложить что-то своё" },
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
                    new Option { Destination = 257, Text = "Завершить путешествие и направиться вместе с офицерами к казахскому ополчению" },

                    new Option {
                        Destination = 428,
                        Text = "Попрощаться с ними и продолжить свои поиски в Семиречье (в этом случае +2 ОПАСНОСТИ)",
                        Do = new Modification
                        {
                            Name = "Danger",
                            Value = 2,
                        },
                    },
                }
            },
            [85] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 119, Text = "Попробовать объездить жеребца" },
                    new Option { Destination = 128, Text = "Вернуться обратно в кочевье и подождать прибытия Алтынбая" },
                }
            },
            [86] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 71, Text = "Навестить отшельника" },
                    new Option { Destination = 58, Text = "Покинуть озеро Алаколь и продолжить путешествие по Семиречью" },
                }
            },
            [87] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 60, Text = "Направиться на юго-восток, в сторону предполагаемой стоянки" },
                    new Option { Destination = 113, Text = "Повернуть на запад, к джунгарским кочевьям" },
                }
            },
            [88] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = 200,
                    },
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 80, Text = "Далее" },
                }
            },
            [89] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 237, Text = "Далее" },
                }
            },
            [90] = new Paragraph
            {
                Trigger = "Барымта",

                Options = new List<Option>
                {
                    new Option { Destination = 257, Text = "Завершить путешествие и направиться вместе с Азимханом и офицерами к казахскому ополчению" },

                    new Option {
                        Destination = 428,
                        Text = "Попрощаться со всеми и продолжить свои поиски в Семиречье (в этом случае +4 ОПАСНОСТИ)",
                        Do = new Modification
                        {
                            Name = "Danger",
                            Value = 4,
                        },
                    },
                }
            },
            [91] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = -100,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 236, Text = "Далее" },
                }
            },
            [92] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 373, Text = "На юг, в Кульджу" },
                    new Option { Destination = 649, Text = "На север, в Семиречье" },
                }
            },
            [93] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 187, Text = "Вызваться на участие в айтысе" },
                    new Option { Destination = 198, Text = "Отказаться от участия и просто посмотреть и послушать акынов" },
                }
            },
            [94] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 11,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 145, Text = "В случае успеха" },
                    new Option { Destination = 107, Text = "В случае провала" },
                }
            },
            [95] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 8,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 180, Text = "В случае успеха" },
                    new Option { Destination = 149, Text = "В случае провала" },
                }
            },
            [96] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 5, Text = "Далее" },
                }
            },
            [97] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить силу",
                        Stat = "Strength",
                        Level = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 164, Text = "В случае успеха" },
                    new Option { Destination = 214, Text = "В случае провала" },
                }
            },
            [98] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
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
                RemoveTrigger = "Боль",

                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [101] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 276, Text = "Согласиться помочь бию" },
                    new Option { Destination = 128, Text = "Вежливо отказаться и остаться в юрте, ожидая приезда Алтынбая" },
                }
            },
            [102] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 90, Text = "Если отмечено ключевое слово «Казах»", OnlyIf = "Казах" },
                    new Option { Destination = 84, Text = "В противном случае" },
                }
            },
            [103] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 236, Text = "Далее" },
                }
            },
            [104] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 118, Text = "Положиться на Зейналду и поискать себе укромное место для засады" },
                    new Option { Destination = 131, Text = "Придумать и предложить что-то своё" },
                }
            },
            [105] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 170, Text = "В случае успеха" },
                    new Option { Destination = 237, Text = "В случае провала" },
                }
            },
            [106] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 151, Text = "Если уровень ОПАСНОСТИ стал равен 12 или выше" },
                    new Option { Destination = 453, Text = "Если же уровень ОПАСНОСТИ всё ещё меньше 12" },
                }
            },
            [107] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 151, Text = "Выстрелить из лука" },
                    new Option { Destination = 172, Text = "Наброситься на воина и ударить его рукоятью пистолета" },
                }
            },
            [108] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 154, Text = "Устроить пожар, как предлагает Медведев" },
                    new Option { Destination = 135, Text = "Последовать совету Дергалюка и учинить драку" },
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
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "StatBonuses",
                        Value = 1,
                    },
                },

                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить силы",
                        Text = "СИЛА",
                        Stat = "Strength",
                        StatStep = 3,
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить ловкости",
                        Text = "ЛОВКОСТЬ",
                        Stat = "Skill",
                        StatStep = 3,
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить мудрости",
                        Text = "МУДРОСТЬ",
                        Stat = "Wisdom",
                        StatStep = 3,
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить хитрости",
                        Text = "ХИТРОСТЬ",
                        Stat = "Cunning",
                        StatStep = 3,
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить красноречия",
                        Text = "КРАСНОРЕЧИЕ",
                        Stat = "Oratory",
                        StatStep = 3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 498, Text = "Далее" },
                }
            },
            [111] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 13,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 306, Text = "В случае успеха" },
                    new Option { Destination = 273, Text = "В случае провала" },
                }
            },
            [112] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [113] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 490, Text = "Далее" },
                }
            },
            [114] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 13,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 129, Text = "В случае успеха" },
                    new Option { Destination = 139, Text = "В случае провала" },
                }
            },
            [115] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 81, Text = "Помочь молодому джигиту" },
                    new Option { Destination = 44, Text = "Затаиться и пропустить погоню мимо" },
                }
            },
            [116] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = 200,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 403, Text = "Если отмечено ключевое слово «Переговорщик»" },
                    new Option { Destination = 503, Text = "В противном случае" },
                }
            },
            [117] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 130, Text = "Далее" },
                }
            },
            [118] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 282, Text = "Вскарабкаться на крутой правый склон и взорвать скалу" },
                    new Option { Destination = 298, Text = "Взобраться на пологий левый склон и сбросить валун на тропу" },
                    new Option { Destination = 223, Text = "Вернуться обратно и затаиться за одним из камней" },
                }
            },
            [119] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 13,
                    },
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить силу",
                        Stat = "Strength",
                        Level = 13,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 160, Text = "Если обе проверки успешны" },
                    new Option { Destination = 168, Text = "Если только одна из проверок успешна (всё равно какая)" },
                    new Option { Destination = 176, Text = "Если обе проверки провалены" },
                }
            },
            [120] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 11,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 102, Text = "В случае успеха" },
                    new Option { Destination = 108, Text = "В случае провала" },
                }
            },
            [121] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Strength",
                        Value = 4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 475, Text = "Далее" },
                }
            },
            [122] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [123] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 105, Text = "Поторговаться с пастухом, полагаясь на своё красноречие" },
                    new Option { Destination = 267, Text = "Попытаться хитростью заполучить коня" },
                }
            },
            [124] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [125] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "StatBonuses",
                        Value = 1,
                    },
                },

                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить силы",
                        Text = "СИЛА",
                        Stat = "Strength",
                        StatStep = 3,
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить ловкости",
                        Text = "ЛОВКОСТЬ",
                        Stat = "Skill",
                        StatStep = 3,
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить мудрости",
                        Text = "МУДРОСТЬ",
                        Stat = "Wisdom",
                        StatStep = 3,
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить хитрости",
                        Text = "ХИТРОСТЬ",
                        Stat = "Cunning",
                        StatStep = 3,
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить красноречия",
                        Text = "КРАСНОРЕЧИЕ",
                        Stat = "Oratory",
                        StatStep = 3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 498, Text = "Далее" },
                }
            },
            [126] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 11,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 189, Text = "В случае успеха" },
                    new Option { Destination = 149, Text = "В случае провала" },
                }
            },
            [127] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Wisdom",
                        Value = 2,
                    },
                    new Modification
                    {
                        Name = "Cunning",
                        Value = 2,
                    },
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 654, Text = "Далее" },
                }
            },
            [128] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 13, Text = "Рассказать правду о себе и о задании хана" },
                    new Option { Destination = 69, Text = "Вести беседу дальше, не раскрывая себя" },
                }
            },
            [129] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 148, Text = "Завершить начатое и произвести выстрел" },
                    new Option { Destination = 158, Text = "Отказаться от задуманного и опустить лук" },
                }
            },
            [130] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 10,
                    },
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить силу",
                        Stat = "Strength",
                        Level = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 240, Text = "Если обе проверки успешны" },
                    new Option { Destination = 263, Text = "В любом другом случае" },
                }
            },
            [131] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 13,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 196, Text = "В случае успеха" },
                    new Option { Destination = 209, Text = "В случае провала" },
                }
            },
            [132] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 10, Text = "Далее" },
                }
            },
            [133] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 13,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 280, Text = "В случае успеха" },
                    new Option { Destination = 294, Text = "В случае провала" },
                }
            },
            [134] = new Paragraph
            {
                Trigger = "Мост",

                Options = new List<Option>
                {
                    new Option { Destination = 86, Text = "Далее" },
                }
            },
            [135] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 11,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 35, Text = "В случае успеха" },
                    new Option { Destination = 54, Text = "В случае провала" },
                }
            },
            [136] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 130, Text = "Далее" },
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
                    new Option { Destination = 8, Text = "Направиться к рыбацкому посёлку, выдавая себя за обычного путника" },
                    new Option { Destination = 19, Text = "Поехать к рыбацкому посёлку, переодевшись джунгарским воином" },
                    new Option { Destination = 42, Text = "Развернуть коня и направиться в какое-нибудь другое место" },
                }
            },
            [139] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 453, Text = "Далее" },
                }
            },
            [140] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 256, Text = "Далее" },
                }
            },
            [141] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = 500,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 159, Text = "Далее" },
                }
            },
            [142] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 76, Text = "Далее" },
                }
            },
            [143] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 8,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 190, Text = "В случае успеха" },
                    new Option { Destination = 65, Text = "В случае провала" },
                }
            },
            [144] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 14,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 403, Text = "Если отмечено ключевое слово «(Переговорщик»" },
                    new Option { Destination = 503, Text = "В случае успеха" },
                    new Option { Destination = 326, Text = "В случае провала" },
                }
            },
            [145] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 130, Text = "Далее" },
                }
            },
            [146] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 14,
                        TriggerTestPenalty = "Боль, -4",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 165, Text = "В случае успеха" },
                    new Option { Destination = 178, Text = "В случае провала" },
                }
            },
            [147] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 452, Text = "Спасти тонущего человека" },
                    new Option { Destination = 463, Text = "Помочь вознице остановить повозку, пока та не разбилась о камни" },
                    new Option { Destination = 475, Text = "Не ввязываться в неприятности и продолжить свой путь" },
                }
            },
            [148] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 453, Text = "Далее" },
                }
            },
            [149] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [150] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 622, Text = "Если отмечено ключевое слово «Дружба»", OnlyIf = "Дружба" },
                    new Option { Destination = 588, Text = "Если не отмечено слово «Дружба», но отмечено слово «Перевал»" },
                    new Option { Destination = 578, Text = "Иначе" },
                }
            },
            [151] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 8,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 197, Text = "В случае успеха" },
                    new Option { Destination = 210, Text = "В случае провала" },
                }
            },
            [152] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Strength",
                        Value = 2,
                    },
                    new Modification
                    {
                        Name = "Skill",
                        Value = 2,
                    },
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 654, Text = "Далее" },
                }
            },
            [153] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 42, Text = "Далее" },
                }
            },
            [154] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 11,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 68, Text = "В случае успеха" },
                    new Option { Destination = 78, Text = "В случае провала" },
                }
            },
            [155] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 279, Text = "Переправиться на другой берег и продолжить путешествие" },
                    new Option { Destination = 222, Text = "Понаблюдать из укрытия за спящим поселением" },
                }
            },
            [156] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 12,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 11, Text = "В случае успеха" },
                    new Option { Destination = 36, Text = "В случае провала" },
                }
            },
            [157] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 87, Text = "Далее" },
                }
            },
            [158] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "MaxBonus",
                        Value = 1,
                    },
                },

                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить силы",
                        Text = "СИЛА",
                        Stat = "Strength",
                        StatToMax = true,
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить ловкости",
                        Text = "ЛОВКОСТЬ",
                        Stat = "Skill",
                        StatToMax = true,
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить мудрости",
                        Text = "МУДРОСТЬ",
                        Stat = "Wisdom",
                        StatToMax = true,
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить хитрости",
                        Text = "ХИТРОСТЬ",
                        Stat = "Cunning",
                        StatToMax = true,
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить красноречия",
                        Text = "КРАСНОРЕЧИЕ",
                        Stat = "Oratory",
                        StatToMax = true,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 453, Text = "Далее" },
                }
            },
            [159] = new Paragraph
            {
                Trigger = "Мугати",

                Options = new List<Option>
                {
                    new Option { Destination = 455, Text = "Далее" },
                }
            },
            [160] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = 100,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 128, Text = "Далее" },
                }
            },
            [161] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 142, Text = "Сделать денежное подношение в 150 ТАНЬГА", OnlyIf = "ТАНЬГА >= 150" },
                    new Option { Destination = 76, Text = "Если же денег меньше, или Алдар не хочет их тратить" },
                }
            },
            [162] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 475, Text = "Далее" },
                }
            },
            [163] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [164] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [165] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 247, Text = "Если отмечено ключевое слово «Русский»" },
                    new Option { Destination = 255, Text = "Если отмечено ключевое слово «Мугати»", OnlyIf = "Мугати" },
                    new Option { Destination = 264, Text = "Иначе" },
                }
            },
            [166] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [167] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 13,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 184, Text = "В случае успеха" },
                    new Option { Destination = 206, Text = "В случае провала" },
                }
            },
            [168] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 128, Text = "Далее" },
                }
            },
            [169] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 11,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 182, Text = "В случае успеха" },
                    new Option { Destination = 153, Text = "В случае провала" },
                }
            },
            [170] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = -80,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 279, Text = "Переправиться на другой берег и продолжить путешествие" },
                    new Option { Destination = 17, Text = "Посетить Арамбека, следуя указаниям Абулхаир-хана" },
                }
            },
            [171] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [172] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить силу",
                        Stat = "Strength",
                        Level = 8,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 217, Text = "В случае успеха" },
                    new Option { Destination = 210, Text = "В случае провала" },
                }
            },
            [173] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 57, Text = "Заплатить 300 ТАНЬГА", OnlyIf = "ТАНЬГА >= 300" },
                    new Option { Destination = 575, Text = "Извиниться перед Ганжууром, сказав, что таких денег при себе нет" },
                }
            },
            [174] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 475, Text = "Далее" },
                }
            },
            [175] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 194, Text = "Далее" },
                }
            },
            [176] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [177] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 201, Text = "Проследовать к поселению Аягуз, принимая меры предосторожности" },
                    new Option { Destination = 279, Text = "Переправиться на другой берег и продолжить основное задание" },
                }
            },
            [178] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [179] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = 100,
                    },
                    new Modification
                    {
                        Name = "Danger",
                        Value = 2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 87, Text = "Далее" },
                }
            },
            [180] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 159, Text = "Далее" },
                }
            },
            [181] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 553, Text = "Попытаться узнать что-нибудь об этих землях" },
                    new Option { Destination = 530, Text = "Попросить у русских охрану для возвращения в ханскую ставку" },
                }
            },
            [182] = new Paragraph
            {
                Trigger = "Буддист",

                Options = new List<Option>
                {
                    new Option { Destination = 42, Text = "Далее" },
                }
            },
            [183] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 279, Text = "Далее" },
                }
            },
            [184] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 453, Text = "Далее" },
                }
            },
            [185] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 26, Text = "Попытаться открыть сундук" },
                    new Option { Destination = 37, Text = "Не обращать на него внимания и подняться наверх" },
                }
            },
            [186] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 146, Text = "Далее" },
                }
            },
            [187] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 13,
                    },
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 13,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 211, Text = "Если обе проверки успешны" },
                    new Option { Destination = 226, Text = "Если только одна из них успешна (не важно какая)" },
                    new Option { Destination = 248, Text = "В любом другом случае" },
                }
            },
            [188] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 410, Text = "Далее" },
                }
            },
            [189] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 159, Text = "Далее" },
                }
            },
            [190] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 10, Text = "Далее" },
                }
            },
            [191] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 161, Text = "Далее" },
                }
            },
            [192] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 12,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 179, Text = "В случае успеха" },
                    new Option { Destination = 124, Text = "В случае провала" },
                }
            },
            [193] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 375, Text = "Далее" },
                }
            },
            [194] = new Paragraph
            {
                Trigger = "Барымта",

                Options = new List<Option>
                {
                    new Option { Destination = 257, Text = "Завершить путешествие и направиться вместе с Шандором к казахскому ополчению" },

                    new Option {
                        Destination = 428,
                        Text = "Попрощаться с ним и продолжить свои поиски в Семиречье (в этом случае +3 ОПАСНОСТИ)",
                        Do = new Modification
                        {
                            Name = "Danger",
                            Value = 3,
                        },
                    },
                }
            },
            [195] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 297, Text = "Далее" },
                }
            },
            [196] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 29, Text = "Далее" },
                }
            },
            [197] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 37, Text = "Далее" },
                }
            },
            [198] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 128, Text = "Далее" },
                }
            },
            [199] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [200] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 194, Text = "Далее" },
                }
            },
            [201] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 244, Text = "Алдар знает о Аягузе", OnlyIf = "Аягуз" },
                    new Option { Destination = 123, Text = "Если отмечено ключевое слово «Табун»" },
                    new Option { Destination = 155, Text = "Если не отмечено ни одно их этих слов" },
                }
            },
            [202] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 375, Text = "Далее" },
                }
            },
            [203] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 146, Text = "Далее" },
                }
            },
            [204] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 157, Text = "В случае успеха" },
                    new Option { Destination = 166, Text = "В случае провала" },
                }
            },
            [205] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 12,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 237, Text = "В случае успеха" },
                    new Option { Destination = 243, Text = "В случае провала" },
                }
            },
            [206] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 453, Text = "Далее" },
                }
            },
            [207] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 301, Text = "В случае успеха" },
                    new Option { Destination = 312, Text = "В случае провала" },
                }
            },
            [208] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 308, Text = "Далее" },
                }
            },
            [209] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 223, Text = "Далее" },
                }
            },
            [210] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 8,
                    },
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить силу",
                        Stat = "Strength",
                        Level = 8,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 240, Text = "Если обе проверки успешны" },
                    new Option { Destination = 263, Text = "В любом другом случае" },
                }
            },
            [211] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = 200,
                    },
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Wisdom",
                        Value = 2,
                    },
                    new Modification
                    {
                        Name = "Oratory",
                        Value = 2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 728, Text = "Далее" },
                }
            },
            [212] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = 100,
                    },
                },

                Trigger = "Алдан",

                Options = new List<Option>
                {
                    new Option { Destination = 428, Text = "Далее" },
                }
            },
            [213] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 410, Text = "Далее" },
                }
            },
            [214] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 34, Text = "Далее" },
                }
            },
            [215] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Trigger = "Лучник",

                Options = new List<Option>
                {
                    new Option { Destination = 575, Text = "Покинуть эти места" },
                    new Option { Destination = 557, Text = "Продолжить сбор сведений" },
                }
            },
            [216] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 485, Text = "Далее" },
                }
            },
            [217] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 37, Text = "Далее" },
                }
            },
            [218] = new Paragraph
            {
                Trigger = "Отряд",

                Options = new List<Option>
                {
                    new Option { Destination = 297, Text = "Далее" },
                }
            },
            [219] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [220] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 284, Text = "Далее" },
                }
            },
            [221] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 9, Text = "Далее" },
                }
            },
            [222] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 279, Text = "Переправиться на другой берег и продолжить путешествие" },
                    new Option { Destination = 77, Text = "Пробраться в юрту хромого человека, чтобы поговорить с ним" },
                }
            },
            [223] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "TestAll",
                        ButtonName = "Проверить силу, ловкость, хитрость и мудрость",
                        Stat = "Strength, Skill, Cunning, Wisdom",
                        Level = 42,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 357, Text = "Если все четыре проверки успешны" },
                    new Option { Destination = 364, Text = "В любом другом случае" },
                }
            },
            [224] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 204, Text = "Попытаться угадать, о каких же делах идёт речь" },
                    new Option { Destination = 192, Text = "Попробовать обмануть Басаана, чтобы он сам всё рассказал" },
                }
            },
            [225] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [226] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 128, Text = "Далее" },
                }
            },
            [227] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 406, Text = "Далее" },
                }
            },
            [228] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 284, Text = "Далее" },
                }
            },
            [229] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Wisdom",
                        Value = 2,
                    },
                    new Modification
                    {
                        Name = "Cunning",
                        Value = 2,
                    },
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 127, Text = "Остаться и продолжить изучение тибетских трактатов" },
                    new Option { Destination = 654, Text = "Покинуть монастырь и продолжить свой путь" },
                }
            },
            [230] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Trigger = "Аягуз, Дружба",

                Options = new List<Option>
                {
                    new Option { Destination = 258, Text = "Помочь Адильжану" },
                    new Option { Destination = 201, Text = "Попрощаться с ним и проследовать в Аягуз" },
                    new Option { Destination = 279, Text = "Переправиться на другой берег реки" },
                }
            },
            [231] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 212, Text = "Далее" },
                }
            },
            [232] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [233] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 297, Text = "Далее" },
                }
            },
            [234] = new Paragraph
            {
                LateTrigger = "Боль",

                Options = new List<Option>
                {
                    new Option { Destination = 573, Text = "Конь уже был ранен раньше", OnlyIf = "Боль" },
                    new Option { Destination = 146, Text = "Впереди брод" },
                }
            },
            [235] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 3, Text = "Далее" },
                }
            },
            [236] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Исследовать озеро, двигаясь вдоль берегов" },
                    new Option { Destination = 42, Text = "Покинуть эти места и отправиться дальше" },
                }
            },
            [237] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = -150,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 279, Text = "Переправиться на другой берег и продолжить путешествие" },
                    new Option { Destination = 17, Text = "Посетить Арамбека, следуя указаниям Абулхаир-хана" },
                }
            },
            [238] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 343, Text = "Если отмечены оба ключевых слова: «Казах» и «Русский»", OnlyIf = "Казах" },
                    new Option { Destination = 349, Text = "Если отмечено только ключевое слово «Русский»" },
                    new Option { Destination = 355, Text = "Если отмечено только ключевое слово «Казах»", OnlyIf = "Казах" },
                    new Option { Destination = 331, Text = "Если отмечено ключевое слово «Мугати»", OnlyIf = "Мугати" },
                }
            },
            [239] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 375, Text = "Далее" },
                }
            },
            [240] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 87, Text = "Далее" },
                }
            },
            [241] = new Paragraph
            {
                Trigger = "Отряд",

                Options = new List<Option>
                {
                    new Option { Destination = 297, Text = "Далее" },
                }
            },
            [242] = new Paragraph
            {
                Trigger = "Боль",

                Options = new List<Option>
                {
                    new Option { Destination = 575, Text = "Далее" },
                }
            },
            [243] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 216, Text = "Далее" },
                }
            },
            [244] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 17, Text = "Направиться к юрте Арамбека, ханского лазутчика" },
                    new Option { Destination = 111, Text = "Проникнуть в юрту Карабая, чтобы поживиться его богатствами" },
                }
            },
            [245] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [246] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 354, Text = "Далее" },
                }
            },
            [247] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 257, Text = "Далее" },
                }
            },
            [248] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 77, Text = "Далее" },
                }
            },
            [249] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 308, Text = "Далее" },
                }
            },
            [250] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Strength",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Skill",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 152, Text = "Остаться и продолжить совершенствование своего тела" },
                    new Option { Destination = 654, Text = "Покинуть монастырь и продолжить свой путь" },
                }
            },
            [251] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 287, Text = "Далее" },
                }
            },
            [252] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 375, Text = "Далее" },
                }
            },
            [253] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 200, Text = "В случае успеха" },
                    new Option { Destination = 219, Text = "В случае провала" },
                }
            },
            [254] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 224, Text = "Ответить, что Алдар прибыл сюда из-за «других дел»" },
                    new Option { Destination = 235, Text = "Сказать, что нашему герою важно только происходящее на руднике" },
                }
            },
            [255] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 257, Text = "Далее" },
                }
            },
            [256] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 475, Text = "Далее" },
                }
            },
            [257] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 625, Text = "Если отмечено оба ключевых слова «Сведения» и «Стрелок»" },
                    new Option { Destination = 640, Text = "Если не отмечено слово «Сведения», но отмечено слово «Стрелок»" },
                    new Option { Destination = 648, Text = "В любом другом случае" },
                }
            },
            [258] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 55, Text = "Далее" },
                }
            },
            [259] = new Paragraph
            {
                Trigger = "Крепость",

                Options = new List<Option>
                {
                    new Option { Destination = 322, Text = "Далее" },
                }
            },
            [260] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 284, Text = "Далее" },
                }
            },
            [261] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 175, Text = "В случае успеха" },
                    new Option { Destination = 163, Text = "В случае провала" },
                }
            },
            [262] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Trigger = "Отряд",

                Options = new List<Option>
                {
                    new Option { Destination = 297, Text = "Далее" },
                }
            },
            [263] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [264] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 257, Text = "Далее" },
                }
            },
            [265] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 9,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 193, Text = "В случае успеха" },
                    new Option { Destination = 202, Text = "В случае провала" },
                }
            },
            [266] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 549, Text = "Попробовать выкрасть буддийские святыни" },
                    new Option { Destination = 654, Text = "Отказаться от этой затеи и покинуть монастырь, чтобы продолжить свой путь" },
                }
            },
            [267] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 73, Text = "В случае успеха" },
                    new Option { Destination = 89, Text = "В случае провала" },
                }
            },
            [268] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 272, Text = "Далее" },
                }
            },
            [269] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить силу",
                        Stat = "Strength",
                        Level = 11,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 227, Text = "В случае успеха" },
                    new Option { Destination = 245, Text = "В случае провала" },
                }
            },
            [270] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 246, Text = "Далее" },
                }
            },
            [271] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 182, Text = "Если Алдар купил здесь что-нибудь" },
                    new Option { Destination = 169, Text = "Если он ничего не покупал" },
                }
            },
            [272] = new Paragraph
            {
                Trigger = "Оборона",

                Options = new List<Option>
                {
                    new Option { Destination = 373, Text = "На юг, в Кульджу" },
                    new Option { Destination = 649, Text = "На север, в Семиречье" },
                }
            },
            [273] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [274] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 8,
                    },
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 11,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 259, Text = "Если обе проверки успешны" },
                    new Option { Destination = 322, Text = "В любом другом случае" },
                }
            },
            [275] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 9, Text = "Далее" },
                }
            },
            [276] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 13,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 289, Text = "В случае успеха" },
                    new Option { Destination = 305, Text = "В случае провала" },
                }
            },
            [277] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 161, Text = "Далее" },
                }
            },
            [278] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 216, Text = "Далее" },
                }
            },
            [279] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 60, Text = "Направиться на юг, в сторону предполагаемого кочевья" },
                    new Option { Destination = 49, Text = "Повернуть на запад, в сторону озера Балхаш и устья реки Аягуз" },
                    new Option { Destination = 138, Text = "Поехать на восток, к озеру Сассык-Коль" },
                }
            },
            [280] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Trigger = "Гелугпа",

                Options = new List<Option>
                {
                    new Option { Destination = 654, Text = "Далее" },
                }
            },
            [281] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 9,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 239, Text = "В случае успеха" },
                    new Option { Destination = 252, Text = "В случае провала" },
                }
            },
            [282] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 13,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 307, Text = "В случае успеха" },
                    new Option { Destination = 223, Text = "В случае провала Алдар оставляет всякие попытки взобраться наверх и бежит обратно к ущелью" },
                }
            },
            [283] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 270, Text = "Если отмечено ключевое слово «Пленник»" },
                    new Option { Destination = 254, Text = "Если это слово не отмечено" },
                }
            },
            [284] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 271, Text = "Направиться к посёлку" },
                    new Option { Destination = 42, Text = "Покинуть озеро Сассык-Коль и отправиться дальше" },
                }
            },
            [285] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [286] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 322, Text = "Далее" },
                }
            },
            [287] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "StatBonuses",
                        Value = 1,
                    },
                },

                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить силы",
                        Text = "СИЛА",
                        Stat = "Strength",
                        StatStep = 3,
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить ловкости",
                        Text = "ЛОВКОСТЬ",
                        Stat = "Skill",
                        StatStep = 3,
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить мудрости",
                        Text = "МУДРОСТЬ",
                        Stat = "Wisdom",
                        StatStep = 3,
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить хитрости",
                        Text = "ХИТРОСТЬ",
                        Stat = "Cunning",
                        StatStep = 3,
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить красноречия",
                        Text = "КРАСНОРЕЧИЕ",
                        Stat = "Oratory",
                        StatStep = 3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 279, Text = "Далее" },
                }
            },
            [288] = new Paragraph
            {
                Trigger = "Барымта",

                Options = new List<Option>
                {
                    new Option { Destination = 257, Text = "Завершить путешествие и направиться вместе с Азимханом к казахскому ополчению" },

                    new Option {
                        Destination = 428,
                        Text = "Попрощаться с Азимханом и продолжить свои поиски в Семиречье (в этом случае +3 ОПАСНОСТИ)",
                        Do = new Modification
                        {
                            Name = "Danger",
                            Value = 3,
                        },
                    },
                }
            },
            [289] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 315, Text = "Далее" },
                }
            },
            [290] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 373, Text = "На юг в Кульджу" },
                    new Option { Destination = 649, Text = "На север в Семиречье" },
                }
            },
            [291] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 475, Text = "Далее" },
                }
            },
            [292] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [293] = new Paragraph
            {
                Trigger = "Аягуз",

                Options = new List<Option>
                {
                    new Option { Destination = 207, Text = "Несмотря ни на что проследовать в Аягуз" },
                    new Option { Destination = 279, Text = "Переправиться на другой берег реки" },
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
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 220, Text = "В случае успеха" },
                    new Option { Destination = 208, Text = "В случае провала" },
                }
            },
            [296] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 343, Text = "Если отмечены оба ключевых слова: «Казах» и «Русский»", OnlyIf = "Казах" },
                    new Option { Destination = 349, Text = "Если отмечено только ключевое слово «Русский»" },
                    new Option { Destination = 355, Text = "Если отмечено только ключевое слово «Казах»", OnlyIf = "Казах" },
                    new Option { Destination = 331, Text = "Если отмечено ключевое слово «Мугати»", OnlyIf = "Мугати" },
                }
            },
            [297] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Купить книгу",
                        Text = "КНИГА ПУТЕШЕСТВЕННИКА",
                        Trigger = "Мост",
                        Price = 120,
                        Aftertext = "Книга путешественника-географа про озёра Сассык-Коль и Алаколь. Торговец говорит, что один арабский учёный исследовал те места, чтобы создать точную карту. К сожалению, он погиб на охоте. А эту книгу торговец купил вместе с другими вещами путешественника. Алдар бегло листает рукопись. Озёра и прилежащая местность действительно описаны очень подробно: с зарисовками, картами и указанием скал, холмов и оврагов.",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Купить арбалет",
                        Text = "МАЛЕНЬКИЙ АРБАЛЕТ, 100 таньга",
                        Trigger = "Арбалет",
                        Price = 100,
                        Aftertext = "Маленький арбалет, который можно спрятать под складками одежды. На расстоянии в несколько шагов он легко пробивает кожаный доспех. По словам торговца, именно такими в давние времена пользовались ассасины Хасана ибн-Саббаха, когда нельзя было приблизиться к жертве вплотную. А ещё русский купец, продавший ему вещицу, сказал, что арбалетная стрела называется болт.",
                        
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Купить настойку",
                        Text = "ТРАВЯНАЯ НАСТОЙКА",
                        Price = 50,
                        Aftertext = "Эта настойка подобна той, которую дали Алдару ханские лекари. К сожалению, у торговца нет больших запасов, и он может продать только две склянки, вмещающие по одному глотку (используется по тем же правилам, что и настойка Алдара Косе).\n\nКак и на любом базаре, Алдар Косе может поторговаться. Для этого надо пройти проверку красноречия. В случае успеха Алдар приобретает соответствующий предмет за полцены. В случае провала он может купить товар по изначальной стоимости.",
                    },
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 12,
                        Aftertext = "",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 77, Text = "Далее" },
                }
            },
            [298] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 329, Text = "В случае успеха" },
                    new Option { Destination = 223, Text = "В случае провала, Алдар бежит обратно к ущелью, пока джунгары его не увидели" },
                }
            },
            [299] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [300] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 622, Text = "Если отмечено ключевое слово «Дружба»", OnlyIf = "Дружба" },
                    new Option { Destination = 655, Text = "Если слово «Дружба» не отмечено, может быть у нашего героя есть карты? Если отмечено ключевое слово «Тропа», то Алдар Косе может пересечь Джунгарские ворота по тайным тропам, не боясь ветра Эби", OnlyIf = "!Дружба" },
                    new Option { Destination = 588, Text = "Если отмечено ключевое слово «Перевал», то карта Джунгарского Алатау поможет найти удобный проход на север" },
                    new Option { Destination = 578, Text = "Если ничего этого нет, придётся уходить от погони, полагаясь на удачу" },
                }
            },
            [301] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 279, Text = "Далее" },
                }
            },
            [302] = new Paragraph
            {
                Trigger = "Оборона",

                Options = new List<Option>
                {
                    new Option { Destination = 453, Text = "Далее" },
                }
            },
            [303] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 297, Text = "Далее" },
                }
            },
            [304] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 367, Text = "Далее" },
                }
            },
            [305] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 128, Text = "Далее" },
                }
            },
            [306] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Tanga",
                        Value = 600,
                    },
                },

                Trigger = "Арбалет",

                Options = new List<Option>
                {
                    new Option { Destination = 207, Text = "Далее" },
                }
            },
            [307] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 29, Text = "Далее" },
                }
            },
            [308] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить силу",
                        Stat = "Strength",
                        Level = 12,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 228, Text = "В случае успеха" },
                    new Option { Destination = 199, Text = "В случае провала" },
                }
            },
            [309] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 283, Text = "Далее" },
                }
            },
            [310] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить силу",
                        Stat = "Strength",
                        Level = 11,
                    },
                },

                Trigger = "Воин",

                Options = new List<Option>
                {
                    new Option { Destination = 656, Text = "В случае успеха" },
                    new Option { Destination = 322, Text = "В случае провала: если это была первая проверка способностей в мире теней" },
                    new Option { Destination = 474, Text = "Иначе" },
                    new Option { Destination = 322, Text = "Вернуться в пещеру отшельника" },
                }
            },
            [311] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 453, Text = "Далее" },
                }
            },
            [312] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 279, Text = "Далее" },
                }
            },
            [313] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 561, Text = "Заплатить 150 ТАНЬГА за изготовление лёгкой упряжи", OnlyIf = "ТАНЬГА >= 150" },
                    new Option { Destination = 337, Text = "Иначе придётся либо попытать счастья в качестве новобранца на стрельбище" },
                    new Option { Destination = 575, Text = "Либо покинуть эти места и продолжить путь" },
                }
            },
            [314] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 343, Text = "Если отмечены оба ключевых слова: «Казах» и «Русский»", OnlyIf = "Казах" },
                    new Option { Destination = 349, Text = "Если отмечено только ключевое слово «Русский»" },
                    new Option { Destination = 355, Text = "Если отмечено только ключевое слово «Казах»", OnlyIf = "Казах" },
                    new Option { Destination = 331, Text = "Если отмечено ключевое слово «Мугати»", OnlyIf = "Мугати" },
                }
            },
            [315] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Tanga",
                        Value = 50,
                    },
                },

                Trigger = "Оружейник",

                Options = new List<Option>
                {
                    new Option { Destination = 128, Text = "Далее" },
                }
            },
            [316] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [317] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 12,
                        TriggerTestPenalty = "Арбалет, -3",
                    },
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить силу",
                        Stat = "Strength",
                        Level = 12,
                        TriggerTestPenalty = "Арбалет, -3",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 275, Text = "Если обе проверки успешны" },
                    new Option { Destination = 292, Text = "В любом другом случае" },
                }
            },
            [318] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 10,
                    },
                     new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 12,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 330, Text = "Если обе проверки успешны" },
                    new Option { Destination = 299, Text = "В любом другом случае" },
                }
            },
            [319] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить силу",
                        Stat = "Strength",
                        Level = 14,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 186, Text = "Если отмечено ключевое слово «Тулпар» (при неотмеченном ключевом слове «Скакун»)" },
                    new Option { Destination = 203, Text = "В случае успеха" },
                    new Option { Destination = 234, Text = "В случае провала" },
                }
            },
            [320] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 8,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 28, Text = "В случае успеха" },
                    new Option { Destination = 251, Text = "В случае провала" },
                }
            },
            [321] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 11,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 260, Text = "В случае успеха" },
                    new Option { Destination = 249, Text = "В случае провала" },
                }
            },
            [322] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 396, Text = "Далее" },
                }
            },
            [323] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 361, Text = "Далее" },
                }
            },
            [324] = new Paragraph
            {
                Trigger = "Воин",

                Options = new List<Option>
                {
                    new Option { Destination = 575, Text = "Если наш герой считает, что узнал достаточно, то можно покинуть эти места" },
                    new Option { Destination = 557, Text = "Иначе придётся продолжить сбор сведений" },
                }
            },
            [325] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 297, Text = "Далее" },
                }
            },
            [326] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 356, Text = "Далее" },
                }
            },
            [327] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 256, Text = "Далее" },
                }
            },
            [328] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 14,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 388, Text = "В случае успеха" },
                    new Option { Destination = 370, Text = "В случае провала" },
                }
            },
            [329] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 29, Text = "Далее" },
                }
            },
            [330] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 283, Text = "Далее" },
                }
            },
            [331] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 253, Text = "Придерживаться первоначальной задумки о разбойниках, которые подожгли стойбище" },
                    new Option { Destination = 261, Text = "Сослаться на Хуяга и сказать предводителю, что наш герой здесь за главного" },
                }
            },
            [332] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 284, Text = "Далее" },
                }
            },
            [333] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 11,
                    },
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 11,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 221, Text = "Если обе проверки успешны" },
                    new Option { Destination = 232, Text = "В любом другом случае" },
                }
            },
            [334] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 654, Text = "Далее" },
                }
            },
            [335] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 14,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 262, Text = "Если отмечены оба слова «Пастух» и «Дружба»", OnlyIf = "Дружба" },
                    new Option { Destination = 241, Text = "Если не отмечено слово «Пастух», но отмечено слово «Дружба»", OnlyIf = "Дружба" },
                    new Option { Destination = 218, Text = "В случае успеха" },
                    new Option { Destination = 195, Text = "В случае провала" },
                }
            },
            [336] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 436, Text = "Далее" },
                }
            },
            [337] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 12,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 215, Text = "В случае успеха" },
                    new Option { Destination = 242, Text = "В случае провала" },
                }
            },
            [338] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 357, Text = "Передать Азимхану, чтобы тот отказался от своих намерений, и продолжить изучение слабых мест стойбища. В этом случае Алдар Косе должен будет полагаться только на свои силы" },
                    new Option { Destination = 405, Text = "Начать приготовления к налёту и сообщить Азимхану, чтобы тот держал свой отряд наготове" },
                }
            },
            [339] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 574, Text = "Далее" },
                }
            },
            [340] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [341] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 14,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 45, Text = "В случае успеха" },
                    new Option { Destination = 497, Text = "Покинуть харчевню и поискать место для ночлега" },
                }
            },
            [342] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 13,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 272, Text = "В случае успеха" },
                    new Option { Destination = 290, Text = "В случае провала" },
                }
            },
            [343] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 154, Text = "Устроить пожар, как предлагает Медведев" },
                    new Option { Destination = 135, Text = "Последовать совету Дергалюка и учинить драку" },
                    new Option { Destination = 120, Text = "Предложить что-то своё" },
                }
            },
            [344] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить силу",
                        Stat = "Strength",
                        Level = 7,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 310, Text = "В случае успеха" },
                    new Option { Destination = 322, Text = "В случае провала" },
                }
            },
            [345] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 283, Text = "Далее" },
                }
            },
            [346] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 13,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 302, Text = "В случае успеха" },
                    new Option { Destination = 311, Text = "В случае провала" },
                }
            },
            [347] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 575, Text = "Далее" },
                }
            },
            [348] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Исследовать озеро, двигаясь вдоль его берегов" },
                    new Option { Destination = 42, Text = "Покинуть эти места и отправиться дальше" },
                }
            },
            [349] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 154, Text = "Устроить пожар, как предлагает Медведев" },
                    new Option { Destination = 135, Text = "Последовать совету Дергалюка и учинить драку" },
                    new Option { Destination = 120, Text = "Предложить что-то своё" },
                }
            },
            [350] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 15, Text = "Если отмечено одно из ключевых слов: «Уйгур» или «Цветок»" },
                    new Option { Destination = 24, Text = "Если ни одно из этих слов не отмечено" },
                }
            },
            [351] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 29, Text = "Далее" },
                }
            },
            [352] = new Paragraph
            {
                Trigger = "Доспех",

                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [353] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 654, Text = "Далее" },
                }
            },
            [354] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 185, Text = "Далее" },
                }
            },
            [355] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 288, Text = "Далее" },
                }
            },
            [356] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 514, Text = "Посетить уйгурское поселение и кульджинский базар" },
                    new Option { Destination = 536, Text = "Попроситься на приём к китайскому наместнику" },
                    new Option { Destination = 545, Text = "Покинуть Кульджу и отправиться дальше" },
                    new Option { Destination = 447, Text = "С купцом Штейном" },
                    new Option { Destination = 429, Text = "С отцом Ионой" },
                }
            },
            [357] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 417, Text = "Если отмечено ключевое слово «Казах»", OnlyIf = "Казах" },
                    new Option { Destination = 442, Text = "Поговорить с русскими и помочь им бежать" },
                    new Option { Destination = 455, Text = "Отказаться от этой затеи и продолжить наблюдение за стойбищем" },
                }
            },
            [358] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 406, Text = "Далее" },
                }
            },
            [359] = new Paragraph
            {
                Trigger = "Логово",

                Options = new List<Option>
                {
                    new Option { Destination = 373, Text = "На юг в Кульджу" },
                    new Option { Destination = 649, Text = "На север в Семиречье" },
                }
            },
            [360] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 375, Text = "Если Алдар Косе уже потратил все свои деньги, то он спокойно продолжает путь, недоумевая, зачем птице понадобился кошель" },
                    new Option { Destination = 281, Text = "Иначе придётся либо попробовать подстрелить ворона из лука" },
                    new Option { Destination = 265, Text = "Либо придумать что-нибудь другое" },
                }
            },
            [361] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "TestAll",
                        ButtonName = "Проверить силу, ловкость, хитрость и мудрость",
                        Stat = "Strength, Skill, Cunning, Wisdom",
                        Level = 42,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 238, Text = "Если все четыре проверки успешны" },
                    new Option { Destination = 225, Text = "В любом другом случае" },
                }
            },
            [362] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить силу",
                        Stat = "Strength",
                        Level = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 397, Text = "В случае успеха" },
                    new Option { Destination = 475, Text = "В случае провала" },
                }
            },
            [363] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 12,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 467, Text = "В случае успеха" },
                    new Option { Destination = 488, Text = "В случае провала" },
                }
            },
            [364] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [365] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 21, Text = "Далее" },
                }
            },
            [366] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 14,
                    },
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 14,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 188, Text = "Алдар помнит разговор с торговцем на озере Сассык-Коль", OnlyIf = "Буддист" },
                    new Option { Destination = 213, Text = "Если обе проверки успешны" },
                    new Option { Destination = 334, Text = "В любом другом случае" },
                }
            },
            [367] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 367, Text = "Далее" },
                }
            },
            [368] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 409, Text = "Далее" },
                }
            },
            [369] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 514, Text = "Далее" },
                }
            },
            [370] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 497, Text = "Далее" },
                }
            },
            [371] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 14,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 277, Text = "В случае успеха" },
                    new Option { Destination = 191, Text = "В случае провала" },
                }
            },
            [372] = new Paragraph
            {
                Trigger = "Воин",

                Options = new List<Option>
                {
                    new Option { Destination = 466, Text = "Предупредить разбойников и привлечь их на свою сторону" },
                    new Option { Destination = 478, Text = "Остаться с джунгарами и присоединиться к карательному походу на стоянку" },
                    new Option { Destination = 485, Text = "Незаметно покинуть эти места" },
                }
            },
            [373] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 514, Text = "Посетить уйгурское поселение и кульджинский базар" },
                    new Option { Destination = 525, Text = "Навестить русский острог" },
                    new Option { Destination = 536, Text = "Попроситься на приём к китайскому наместнику" },
                    new Option { Destination = 545, Text = "Если Алдар считает, что у него нет здесь никаких дел, и он не хочет ввязываться в китайско-джунгарские отношения, то можно покинуть Кульджу и отправиться дальше" },
                }
            },
            [374] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [375] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 11,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 99, Text = "Если отмечено ключевое слово «Хищник», то" },
                    new Option { Destination = 134, Text = "В случае успеха" },
                    new Option { Destination = 109, Text = "В случае провала" },
                }
            },
            [376] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 13,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 418, Text = "В случае успеха" },
                    new Option { Destination = 427, Text = "В случае провала" },
                }
            },
            [377] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 317, Text = "Внезапно напасть на стражей" },
                    new Option { Destination = 333, Text = "Попытаться обхитрить их" },
                }
            },
            [378] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 343, Text = "Если отмечены оба ключевых слова: «Казах» и «Русский»", OnlyIf = "Казах" },
                    new Option { Destination = 349, Text = "Если отмечено только ключевое слово «Русский»" },
                    new Option { Destination = 355, Text = "Если отмечено только ключевое слово «Казах»", OnlyIf = "Казах" },
                    new Option { Destination = 331, Text = "Если отмечено ключевое слово «Мугати»", OnlyIf = "Мугати" },
                }
            },
            [379] = new Paragraph
            {
                Trigger = "Воин",

                Options = new List<Option>
                {
                    new Option { Destination = 575, Text = "Покинуть эти места" },
                    new Option { Destination = 557, Text = "Продолжить сбор сведений" },
                }
            },
            [380] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 426, Text = "В случае успеха" },
                    new Option { Destination = 438, Text = "В случае провала" },
                }
            },
            [381] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [382] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 409, Text = "Далее" },
                }
            },
            [383] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 406, Text = "Далее" },
                }
            },
            [384] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 408, Text = "Далее" },
                }
            },
            [385] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 12,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 431, Text = "В случае успеха" },
                    new Option { Destination = 420, Text = "В случае провала" },
                }
            },
            [386] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 338, Text = "Если отмечено ключевое слово «Отряд»", OnlyIf = "Отряд" },
                    new Option { Destination = 357, Text = "Остаться на охране кочевья" },
                    new Option { Destination = 372, Text = "Вызваться на поиски разбойников" },
                }
            },
            [387] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 266, Text = "Медитации" },
                    new Option { Destination = 250, Text = "Истязания" },
                    new Option { Destination = 229, Text = "Изучение истин" },
                }
            },
            [388] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 497, Text = "Далее" },
                }
            },
            [389] = new Paragraph
            {
                Trigger = "Воин",

                Options = new List<Option>
                {
                    new Option { Destination = 575, Text = "Далее" },
                }
            },
            [390] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 375, Text = "Далее" },
                }
            },
            [391] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = 200,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 356, Text = "Далее" },
                }
            },
            [392] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 296, Text = "В случае успеха" },
                    new Option { Destination = 304, Text = "В случае провала" },
                }
            },
            [393] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 12,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 534, Text = "В случае успеха" },
                    new Option { Destination = 522, Text = "В случае провала" },
                }
            },
            [394] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 13,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 27, Text = "В случае успеха" },
                    new Option { Destination = 38, Text = "В случае провала" },
                }
            },
            [395] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 424, Text = "Спрятаться между верблюдами, как поступили несколько человек" },
                    new Option { Destination = 439, Text = "Бежать вперёд к скалам вместе с Зейналдой" },
                    new Option { Destination = 450, Text = "Броситься назад, в ту сторону, откуда раздался свист" },
                }
            },
            [396] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 58, Text = "Далее" },
                }
            },
            [397] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 449, Text = "Далее" },
                }
            },
            [398] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 314, Text = "В случае успеха" },
                    new Option { Destination = 323, Text = "В случае провала" },
                }
            },
            [399] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 14,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 387, Text = "В случае успеха" },
                    new Option { Destination = 353, Text = "В случае провала" },
                }
            },
            [400] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [401] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить силу",
                        Stat = "Strength",
                        Level = 12,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 379, Text = "Если отмечено ключевое слово «Табун», но не отмечены слова «Скакун» или «Тулпар»" },
                    new Option { Destination = 324, Text = "В случае успеха" },
                    new Option { Destination = 347, Text = "В случае провала" },
                }
            },
            [402] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 475, Text = "Далее" },
                }
            },
            [403] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = 300,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 356, Text = "Далее" },
                }
            },
            [404] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 12,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 510, Text = "В случае успеха" },
                    new Option { Destination = 529, Text = "В случае провала" },
                }
            },
            [405] = new Paragraph
            {
                Trigger = "Казах",

                Options = new List<Option>
                {
                    new Option { Destination = 357, Text = "Далее" },
                }
            },
            [406] = new Paragraph
            {
                Trigger = "Долина",

                Options = new List<Option>
                {
                    new Option { Destination = 453, Text = "Далее" },
                }
            },
            [407] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 12,
                    },
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 12,
                        TriggerTestPenalty = "Арбалет, -4",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 532, Text = "Если обе проверки успешны" },
                    new Option { Destination = 538, Text = "В любом другом случае" },
                }
            },
            [408] = new Paragraph
            {
                Trigger = "Мост, Оборона",

                Options = new List<Option>
                {
                    new Option { Destination = 562, Text = "Далее" },
                }
            },
            [409] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 469, Text = "Избежать опасности" },
                    new Option { Destination = 481, Text = "Познать врага" },
                    new Option { Destination = 493, Text = "Улучшить одну из своих способностей" },
                }
            },
            [410] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 461, Text = "Если отмечено ключевое слово «Прозрение»" },
                    new Option { Destination = 471, Text = "Иначе можно покинуть комнату через проход" },
                    new Option { Destination = 494, Text = "Либо задержаться здесь, чтобы немного отдохнуть" },
                }
            },
            [411] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить силу",
                        Stat = "Strength",
                        Level = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 378, Text = "В случае успеха" },
                    new Option { Destination = 367, Text = "В случае провала" },
                }
            },
            [412] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 356, Text = "Далее" },
                }
            },
            [413] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 406, Text = "Далее" },
                }
            },
            [414] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [415] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Trigger = "Крепость",

                Options = new List<Option>
                {
                    new Option { Destination = 42, Text = "Далее" },
                }
            },
            [416] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 373, Text = "На юг, в Кульджу" },
                    new Option { Destination = 649, Text = "На север, в Семиречье" },
                }
            },
            [417] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 442, Text = "Поговорить с русскими и помочь им бежать" },
                    new Option { Destination = 455, Text = "Отказаться от этой затеи и продолжить наблюдение за стойбищем" },
                }
            },
            [418] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 11,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 454, Text = "В случае успеха" },
                    new Option { Destination = 486, Text = "В случае провала" },
                }
            },
            [419] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 11,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 430, Text = "В случае успеха" },
                    new Option { Destination = 402, Text = "В случае провала" },
                }
            },
            [420] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 404, Text = "Далее" },
                }
            },
            [421] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 356, Text = "Далее" },
                }
            },
            [422] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [423] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 377, Text = "Вызваться на разгрузку бочек с порохом, чтобы спрятаться в подземелье до темноты" },
                    new Option { Destination = 394, Text = "Поехать навстречу каравану и притвориться сопровождающим" },
                    new Option { Destination = 407, Text = "Дождаться ночи и напасть на стражу подземелья" },
                }
            },
            [424] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 13,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 480, Text = "В случае успеха" },
                    new Option { Destination = 501, Text = "В случае провала" },
                }
            },
            [425] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "MaxBonus",
                        Value = 1,
                    },
                },

                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить силы",
                        Text = "СИЛА",
                        Stat = "Strength",
                        StatToMax = true,
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить ловкости",
                        Text = "ЛОВКОСТЬ",
                        Stat = "Skill",
                        StatToMax = true,
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить мудрости",
                        Text = "МУДРОСТЬ",
                        Stat = "Wisdom",
                        StatToMax = true,
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить хитрости",
                        Text = "ХИТРОСТЬ",
                        Stat = "Cunning",
                        StatToMax = true,
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить красноречия",
                        Text = "КРАСНОРЕЧИЕ",
                        Stat = "Oratory",
                        StatToMax = true,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 396, Text = "Далее" },
                }
            },
            [426] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = 50,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 449, Text = "Далее" },
                }
            },
            [427] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 654, Text = "Далее" },
                }
            },
            [428] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 100, Text = "Отправиться на восток, в ставку хунтайши" },
                    new Option { Destination = 502, Text = "На юг, к джунгарским войскам" },
                    new Option { Destination = 517, Text = "На юго-запад, чтобы кратчайшим путём достичь реки Или и переправиться через неё" },
                }
            },
            [429] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 598, Text = "Если отмечено ключевое слово «Святыня»" },
                    new Option { Destination = 580, Text = "В противном случае придётся либо притвориться последователем христианской веры" },
                    new Option { Destination = 563, Text = "Либо рассказать о себе правду и попросить о помощи" },
                }
            },
            [430] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 291, Text = "Далее" },
                }
            },
            [431] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 436, Text = "Далее" },
                }
            },
            [432] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 13,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 416, Text = "В случае успеха" },
                    new Option { Destination = 374, Text = "В случае провала" },
                }
            },
            [433] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 453, Text = "Отказаться от замысла и покинуть ставку" },
                    new Option { Destination = 544, Text = "Проникнуть в юрту Аюк-багатура ночью" },
                    new Option { Destination = 477, Text = "Придумать что-то другое" },
                }
            },
            [434] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = 1000,
                    },
                },

                Trigger = "Коварство",

                Options = new List<Option>
                {
                    new Option { Destination = 212, Text = "Далее" },
                }
            },
            [435] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 396, Text = "Далее" },
                }
            },
            [436] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 7, Text = "Поехать налево, к монастырю Салхи-Зуун" },
                    new Option { Destination = 373, Text = "Направо, в Кульджу" },
                }
            },
            [437] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [438] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 42, Text = "Далее" },
                }
            },
            [439] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 13,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 584, Text = "В случае успеха" },
                    new Option { Destination = 501, Text = "В случае провала" },
                }
            },
            [440] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить силу",
                        Stat = "Strength",
                        Level = 8,
                    },
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 8,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 303, Text = "Если обе проверки успешны" },
                    new Option { Destination = 174, Text = "Если только одна из проверок успешна (неважно какая)" },
                    new Option { Destination = 121, Text = "Если обе проверки провалены" },
                }
            },
            [441] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 371, Text = "Далее" },
                }
            },
            [442] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 455, Text = "Далее" },
                }
            },
            [443] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 14, Text = "Предпочесть лук" },
                    new Option { Destination = 14, Text = "Воспользоваться арбалетом", OnlyIf = "Арбалет" },
                    new Option { Destination = 32, Text = "Решиться на использование пистолета" },
                }
            },
            [444] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option {
                        Destination = 389,
                        Text = "Заплатить 50 ТАНЬГА бухарцу",
                        OnlyIf = "ТАНЬГА >= 50",

                        Do = new Modification
                        {
                            Name = "Tanga",
                            Value = -50,
                        },
                    },

                    new Option { Destination = 401, Text = "Иначе можно попытать счастья в качестве новобранца" },
                    new Option { Destination = 575, Text = "Или покинуть эти места" },
                }
            },
            [445] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить силу",
                        Stat = "Strength",
                        Level = 14,
                    },
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 14,
                    },
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 14,
                    },
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 14,
                    },
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 14,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 425, Text = "В случае успеха" },
                    new Option { Destination = 414, Text = "В случае провала" },
                }
            },
            [446] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 366, Text = "Сделать вид, что Алдар Косе и есть хубилган" },
                    new Option { Destination = 399, Text = "Извиниться за недоразумение и остаться в храме как обычный паломник" },
                }
            },
            [447] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 560, Text = "Рассказать про угон табуна", OnlyIf = "Барымта" },
                    new Option { Destination = 568, Text = "Отмечено ключевое слово «Святыня»" },
                    new Option { Destination = 576, Text = "Рассказать про взрыв", OnlyIf = "Взрыв" },
                    new Option { Destination = 585, Text = "Далее" },
                }
            },
            [448] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 13,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 434, Text = "В случае успеха" },
                    new Option { Destination = 422, Text = "В случае провала" },
                }
            },
            [449] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 236, Text = "В случае успеха" },
                    new Option { Destination = 348, Text = "В случае провала" },
                }
            },
            [450] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить силу",
                        Stat = "Strength",
                        Level = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 595, Text = "В случае успеха" },
                    new Option { Destination = 501, Text = "В случае провала" },
                }
            },
            [451] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 436, Text = "Далее" },
                }
            },
            [452] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 487, Text = "Прыгнуть в воду" },
                    new Option { Destination = 499, Text = "Использовать аркан" },
                }
            },
            [453] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 113, Text = "Отправиться на запад, к джунгарским кочевьям, через пустыню Сары-Есик" },
                    new Option { Destination = 502, Text = "На юго-запад, к расположению джунгарских войск" },
                    new Option { Destination = 147, Text = "На северо-восток, к озеру Алаколь" },
                    new Option { Destination = 496, Text = "На восток, к ущелью Джунгарские ворота" },
                }
            },
            [454] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 3,
                    },
                },

                Trigger = "Гелугпа",

                Options = new List<Option>
                {
                    new Option { Destination = 654, Text = "Далее" },
                }
            },
            [455] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 505, Text = "Если отмечено хотя бы одно из ключевых слов: «Казах», «Русский» или «Мугати»", OnlyIf = "Казах|Мугати" },
                    new Option { Destination = 513, Text = "Если отмечено ключевое слово «Туркмен»" },
                    new Option { Destination = 521, Text = "Остаться здесь и назавтра принять участие в карательном походе" },
                    new Option { Destination = 485, Text = "Не ввязываться в это опасное мероприятие и незаметно покинуть кочевье" },
                }
            },
            [456] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 342, Text = "Попытаться повернуть разговор в нужное русло, чтобы выведать полезные сведения" },
                    new Option { Destination = 359, Text = "Продолжать беседу как ни в чём не бывало в надежде, что тысячник и сам проговорится" },
                }
            },
            [457] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 396, Text = "Далее" },
                }
            },
            [458] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 21, Text = "Далее" },
                }
            },
            [459] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 14,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 441, Text = "В случае успеха" },
                    new Option { Destination = 492, Text = "В случае провала" },
                }
            },
            [460] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 216, Text = "Далее" },
                }
            },
            [461] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 516, Text = "Далее" },
                }
            },
            [462] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 408, Text = "Далее" },
                }
            },
            [463] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 440, Text = "Попытаться остановить лошадей, ухватившись за поводья" },
                    new Option { Destination = 419, Text = "Придумать что-нибудь ещё" },
                }
            },
            [464] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [465] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 116, Text = "Если отмечено слово «Пушкарь», и Алдар хочет воспользоваться советом Алексея Медведева (но тогда придётся рассказать всё о себе)" },
                    new Option { Destination = 144, Text = "Иначе либо раскрыть себя и попросить о помощи" },
                    new Option { Destination = 181, Text = "Либо, сохраняя купеческое обличье, попытаться выведать у полковника что-нибудь интересное" },
                }
            },
            [466] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 40, Text = "Далее" },
                }
            },
            [467] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 436, Text = "Далее" },
                }
            },
            [468] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 575, Text = "Покинуть расположение джунгарских войск, опасаясь мстительного ойрата" },
                    new Option { Destination = 557, Text = "Иначе у Алдара Косе есть выбор: продолжить сбор сведений о джунгарских войсках в расположении" },
                    new Option { Destination = 575, Text = "Либо отправиться дальше" },
                }
            },
            [469] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 435, Text = "Если отмечено ключевое слово «Сведения»" },
                    new Option { Destination = 457, Text = "Если же это слово не отмечено" },
                }
            },
            [470] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 352, Text = "Попросить Асылбека изготовить доспехи" },
                    new Option { Destination = 365, Text = "Притвориться торговцем, чтобы разузнать у оружейника побольше о жизни ставки" },
                }
            },
            [471] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 14,
                    },
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 14,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 528, Text = "Если обе проверки успешны" },
                    new Option { Destination = 541, Text = "Если успешна только проверка ЛОВКОСТИ" },
                    new Option { Destination = 437, Text = "В любом другом случае" },
                }
            },
            [472] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 13,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 448, Text = "В случае успеха" },
                    new Option { Destination = 460, Text = "В случае провала" },
                }
            },
            [473] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 517, Text = "Отправиться на запад, к реке Или, чтобы вернуться к Абулхаир-хану" },
                    new Option { Destination = 624, Text = "На юг, к урочищу Аныракай" },
                    new Option { Destination = 113, Text = "На север, к джунгарским кочевьям в Прибалхашье" },
                    new Option { Destination = 100, Text = "На восток, в ставку Галдана Церена" },
                }
            },
            [474] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = 150,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 436, Text = "Далее" },
                }
            },
            [475] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 390, Text = "Если Алдар Косе спас утопающего, прыгнув в озеро" },
                    new Option { Destination = 360, Text = "Если же нет, то можно искупаться" },
                    new Option { Destination = 375, Text = "Либо отказаться от этой мысли и продолжить путь" },
                }
            },
            [476] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 545, Text = "Далее" },
                }
            },
            [477] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 13,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 269, Text = "В случае успеха" },
                    new Option { Destination = 285, Text = "В случае провала" },
                }
            },
            [478] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 455, Text = "Далее" },
                }
            },
            [479] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 399, Text = "Далее" },
                }
            },
            [480] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 515, Text = "Далее" },
                }
            },
            [481] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 274, Text = "Если отмечены все три ключевых слова: «Воин», «Лучник» и «Река»", OnlyIf = "Воин, Лучник" },
                    new Option { Destination = 300, Text = "Если отмечены оба ключевых слова: «Воин» и Лучник»", OnlyIf = "Воин, Лучник" },
                    new Option { Destination = 310, Text = "Если отмечено только ключевое слово «Воин»", OnlyIf = "Воин" },
                    new Option { Destination = 344, Text = "Если не отмечено ни одно из этих ключевых слов" },
                }
            },
            [482] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 514, Text = "Согласиться на предложение уйгура насчёт товара. В этом случае вернуться на базар и выбрать что-нибудь себе по вкусу: товар или услугу проводника (если речь идёт о настойке или отваре женьшеня, то Алдар получит все порции выбранного напитка бесплатно)" },
                    new Option { Destination = 339, Text = "Расспросить собеседника о происходящем в Кульдже" },
                    new Option { Destination = 369, Text = "Попросить защиты от джунгарских лазутчиков" },
                }
            },
            [483] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 21, Text = "Далее" },
                }
            },
            [484] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 575, Text = "То наш герой вынужден покинуть расположение джунгарских войск, опасаясь мстительного ойрата" },
                    new Option { Destination = 557, Text = "Иначе у Алдара Косе есть выбор: продолжить сбор сведений о джунгарских войсках в расположении" },
                    new Option { Destination = 575, Text = "Либо отправиться дальше" },
                }
            },
            [485] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 428, Text = "Далее" },
                }
            },
            [486] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [487] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить силу",
                        Stat = "Strength",
                        Level = 9,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 327, Text = "В случае успеха" },
                    new Option { Destination = 340, Text = "В случае провала" },
                }
            },
            [488] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 404, Text = "Далее" },
                }
            },
            [489] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 13,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 92, Text = "В случае успеха" },
                    new Option { Destination = 137, Text = "В случае провала" },
                }
            },
            [490] = new Paragraph
            {
                Trigger = "Ойрат",

                Options = new List<Option>
                {
                    new Option { Destination = 386, Text = "Далее" },
                }
            },
            [491] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 383, Text = "Имя Алдана знакомо Алдару", OnlyIf ="Алдан" },
                    new Option { Destination = 358, Text = "Если Алдар Косе хочет рассказать о казахском лазутчике" },
                    new Option { Destination = 346, Text = "Если наш герой больше полагается на своё красноречие" },
                }
            },
            [492] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 371, Text = "Далее" },
                }
            },
            [493] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 445, Text = "Далее" },
                }
            },
            [494] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 654, Text = "Далее" },
                }
            },
            [495] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 156, Text = "Попытаться как-то обмануть писаря" },
                    new Option { Destination = 173, Text = "Положиться на силу денег (если кошелёк ещё полон: тут одной сотней не отделаешься)" },
                }
            },
            [496] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 523, Text = "Если отмечено ключевое слово «Тропа»" },
                    new Option { Destination = 535, Text = "Если слово «Тропа» не отмечено, но отмечено слово «Уйгур»" },
                    new Option { Destination = 547, Text = "В любом другом случае" },
                }
            },
            [497] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 423, Text = "Если отмечено ключевое слово «Порох», и Алдар Косе хочет взорвать склад в подземельях ставки" },
                    new Option { Destination = 433, Text = "Если отмечено ключевое слово «Сундук», и джигит намерен выкрасть бумаги из юрты Аюк-багатура" },
                    new Option { Destination = 443, Text = "Иначе можно попытаться убить джунгарского хунтайши Галдана Церена" },
                    new Option { Destination = 453, Text = "Либо покинуть ставку, ни во что не ввязываясь" },
                }
            },
            [498] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 465, Text = "С полковником Риддером" },
                    new Option { Destination = 447, Text = "С купцом Штейном" },
                    new Option { Destination = 514, Text = "Покинуть это место, чтобы не подвергать себя ещё большей опасности: Посетить уйгурское поселение и кульджинский базар" },
                    new Option { Destination = 536, Text = "Попроситься на приём к китайскому наместнику" },
                }
            },
            [499] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 13,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 140, Text = "В случае успеха" },
                    new Option { Destination = 162, Text = "В случае провала" },
                }
            },
            [500] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 3,
                    },
                },

                Trigger = "Гелугпа",

                Options = new List<Option>
                {
                    new Option { Destination = 654, Text = "Далее" },
                }
            },
            [501] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [502] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 628, Text = "Отмечено ключевое слово «Ойрат»", OnlyIf = "Ойрат" },
                    new Option { Destination = 647, Text = "Далее" },
                }
            },
            [503] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 391, Text = "Деньги" },
                    new Option { Destination = 412, Text = "Сведения о джунгарах" },
                    new Option { Destination = 421, Text = "Советы для переговоров с китайским наместником" },
                }
            },
            [504] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 13,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 413, Text = "В случае успеха" },
                    new Option { Destination = 400, Text = "В случае провала" },
                }
            },
            [505] = new Paragraph
            {
                Trigger = "Долина",

                Options = new List<Option>
                {
                    new Option { Destination = 651, Text = "Воспользоваться арбалетом", OnlyIf = "Арбалет" },
                    new Option { Destination = 411, Text = "Ударить Хуяга по голове" },
                    new Option { Destination = 398, Text = "Пырнуть его ножом" },
                    new Option { Destination = 392, Text = "Выстрелить в ойрата из лука" },
                }
            },
            [506] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 575, Text = "Покинуть расположение джунгарских войск, опасаясь мстительного ойрата" },
                    new Option { Destination = 557, Text = "Иначе у Алдара Косе есть выбор: продолжить сбор сведений о джунгарских войсках в расположении" },
                    new Option { Destination = 575, Text = "Либо отправиться дальше" },
                }
            },
            [507] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 14,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 446, Text = "В случае успеха" },
                    new Option { Destination = 479, Text = "В случае провала" },
                }
            },
            [508] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 5, Text = "Далее" },
                }
            },
            [509] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 575, Text = "Далее" },
                }
            },
            [510] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 436, Text = "Далее" },
                }
            },
            [511] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 328, Text = "Присесть к батракам и ремесленникам" },
                    new Option { Destination = 341, Text = "Присоединиться к воинам" },
                }
            },
            [512] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 545, Text = "Далее" },
                }
            },
            [513] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 48, Text = "Сделать выбор в пользу пешего отряда, чтобы принять участие в ложном нападении" },
                    new Option { Destination = 205, Text = "Предпочесть конницу, которая будет стеречь бандитов у оврага" },
                }
            },
            [514] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 14,
                        Aftertext = "Если проверка будет успешной, то товар достанется нашему герою по меньшей цене.",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Купить настойку",
                        Text = "ТРАВЯНАЯ НАСТОЙКА",
                        Price = 70,
                        Aftertext = "У торговцев травами и благовониями Алдар находит настойку, по составу и действию схожую с настойкой ханских лекарей. Он используется по тем же правилам, что и настойка Алдара. У торговцев есть только 3 глотка этого напитка.",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Купить настойку женьшеня",
                        Text = "НАСТОЙКА ЖЕНЬШЕНЯ",
                        Price = 170,
                        Aftertext = "Он гораздо сильнее травяной настойки. Один глоток отвара добавит сразу 8 пунктов к уровню способности при её проверке. В остальном он используется по тем же правилам. Торговцы могут продать только 2 глотка отвара.",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Купить доспех",
                        Text = "КОЖАНЫЙ ДОСПЕХ",
                        Price = 80,
                        Aftertext = "Старик-уйгур, сидящий чуть поодаль от рядов, показывает нашему герою очень лёгкий доспех конного воина. Он состоит из куртки для всадника и накидки для коня. Старик утверждает, что доспех защитит от случайных стрел и пушечной дроби. А кроме того, если понадобится удирать от погони, накидку можно легко срезать ножом, не вылезая из седла. Уйгур рассказывает, что доспех был изготовлен мастером-оружейником для его сына, который недавно погиб в стычке с бухарскими разбойниками. И вот старик продаёт доспех за ненадобностью. Алдар не хочет торговаться с отцом, потерявшим сына. Тем более, что старик берёт не так уж и дорого.",
                        Trigger = "Доспех",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Купить жеребца",
                        Text = "ЖЕРЕБЕЦ-АХАЛТЕКИНЕЦ",
                        Price = 250,
                        Aftertext = "Джигит заглядывается на породистых жеребцов-ахалтекинцев. Лошадник-туркмен, видя интерес нашего героя, зазывает его купить себе хорошего коня. Но Алдар помимо денег должен отдать ему своего коня в обмен.",
                        RemoveTrigger = "Боль",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Нанять проводника через горы",
                        Text = "УСЛУГИ ПРОВОДНИКА",
                        Price = 50,
                        Aftertext = "Кроме того, местные жители предлагают услуги проводника, чтобы кратчайшим путём переправиться через горы. Дело в том, что в урочище Аныракай джунгары построили крепость и проверяют все караваны, беря мзду за проезд. Поэтому неудивительно, что контрабанда здесь процветает.",
                        Trigger = "Запад",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Нанять проводника в казахские земли",
                        Text = "УСЛУГИ ПРОВОДНИКА",
                        Price = 180,
                        Aftertext = "Также наш герой желает сразу попасть в казахские земли в обход войск неприятеля. Проводники жалуются, что бухарские горцы промышляют разбоями в тех местах, поэтому и за проход берут дороже, ведя по горам не путников-одиночек, а хорошо вооружённые караваны. Деньги Алдар должен заплатить вперёд. А когда он закончит свои дела в Кульдже, то встретится с проводником либо присоединится к каравану, чтобы совершить путешествие через горы.\n\nНа шумном базаре день проходит незаметно, и над Кульджой сгущаются сумерки. Благодаря гостеприимству уйгуров Алдар Косе ночует в одном из саманных домиков. А утром, встав пораньше, он раздумывает, с чего же начать этот день.\n\nПомните, что Алдар Косе может посещать базар и русский острог неоднократно.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 482, Text = "Если отмечено ключевое слово «Уйгур», и Алдар Косе впервые посещает кульджинский базар" },
                    new Option { Destination = 525, Text = "Навестить русский острог" },
                    new Option { Destination = 536, Text = "Попроситься на приём к китайскому наместнику" },
                    new Option { Destination = 545, Text = "Если Алдар закончил свои дела в Кульдже, можно отправиться дальше" },
                }
            },
            [515] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 562, Text = "Попрощаться и продолжить свой путь к Абулхаир-хану" },
                    new Option { Destination = 574, Text = "Присоединиться к охоте за оставшимися разбойниками" },
                }
            },
            [516] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Strength",
                        Value = 2,
                    },
                    new Modification
                    {
                        Name = "Skill",
                        Value = 2,
                    },
                },

                Trigger = "Оборона",

                Options = new List<Option>
                {
                    new Option { Destination = 654, Text = "Далее" },
                }
            },
            [517] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 650, Text = "Если отмечено ключевое слово «Дружба»", OnlyIf = "Дружба" },
                    new Option { Destination = 599, Text = "Если отмечены оба ключевых слова: «Сведения» и «Коварство»", OnlyIf = "Коварство" },
                    new Option { Destination = 626, Text = "Если отмечено только слово «Сведения»" },
                    new Option { Destination = 645, Text = "В любом другом случае" },
                }
            },
            [518] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 498, Text = "Далее" },
                }
            },
            [519] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 9, Text = "Далее" },
                }
            },
            [520] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 12,
                    },
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 12,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 540, Text = "Если обе проверки успешны" },
                    new Option { Destination = 508, Text = "В любом другом случае" },
                }
            },
            [521] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 472, Text = "Подговорить воинов на пешую атаку через овраг" },
                    new Option { Destination = 460, Text = "Отказаться от этого намерения" },
                }
            },
            [522] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 654, Text = "Далее" },
                }
            },
            [523] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 436, Text = "Далее" },
                }
            },
            [524] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 373, Text = "На юг, в Кульджу" },
                    new Option { Destination = 649, Text = "На север, в Семиречье" },
                }
            },
            [525] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 465, Text = "К полковнику Риддеру" },
                    new Option { Destination = 447, Text = "К купцу Штейну" },
                    new Option { Destination = 429, Text = "К отцу Ионе" },
                    new Option { Destination = 514, Text = "Посетить уйгурское поселение и кульджинский базар" },
                    new Option { Destination = 536, Text = "Попроситься на приём к китайскому наместнику" },
                }
            },
            [526] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [527] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 545, Text = "Далее" },
                }
            },
            [528] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 516, Text = "Далее" },
                }
            },
            [529] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [530] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 356, Text = "Далее" },
                }
            },
            [531] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 319, Text = "Далее" },
                }
            },
            [532] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 9, Text = "Далее" },
                }
            },
            [533] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 498, Text = "Далее" },
                }
            },
            [534] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 12,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 500, Text = "В случае успеха" },
                    new Option { Destination = 381, Text = "В случае провала" },
                }
            },
            [535] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level =  12,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 474, Text = "В случае успеха" },
                    new Option { Destination = 451, Text = "В случае провала" },
                }
            },
            [536] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 556, Text = "Если отмечено ключевое слово «Цветок»" },
                    new Option { Destination = 572, Text = "Если это слово не отмечено" },
                }
            },
            [537] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 432, Text = "Решиться на тайный осмотр укреплений" },
                    new Option { Destination = 456, Text = "Поговорить с тысячником" },
                    new Option { Destination = 373, Text = "На юг, в Кульджу" },
                    new Option { Destination = 649, Text = "На север, в Семиречье" },
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
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 14,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 506, Text = "Если отмечено ключевое слово «Ерик»", OnlyIf = "Ерик" },
                    new Option { Destination = 484, Text = "В случае успеха" },
                    new Option { Destination = 468, Text = "В случае провала" },
                }
            },
            [540] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 25, Text = "Спросить о джунгарах" },
                    new Option { Destination = 47, Text = "Спросить про китайского наместника" },
                    new Option { Destination = 96, Text = "Спросить о местных торговцах и как сбить цены на базаре" },
                }
            },
            [541] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 494, Text = "Далее" },
                }
            },
            [542] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 518, Text = "Далее" },
                }
            },
            [543] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [544] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 11,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 504, Text = "В случае успеха" },
                    new Option { Destination = 464, Text = "В случае провала" },
                }
            },
            [545] = new Paragraph
            {
                Trigger = "Кульджа",

                Options = new List<Option>
                {
                    new Option { Destination = 624, Text = "Если наш герой ещё не побывал в этих местах, то можно отправиться в урочище Аныракай" },
                    new Option { Destination = 653, Text = "Либо в буддийский монастырь Салхи-Зуун" },
                    new Option { Destination = 612, Text = "Если отмечено ключевое слово «Север», то проводник через Джунгарский Алатау ожидает нашего героя" },
                    new Option { Destination = 605, Text = "Если отмечено ключевое слово «Запад», то караван через Заилийский Алатау готов двинуться в путь", OnlyIf = "Запад" },
                    new Option { Destination = 596, Text = "Если отмечено ключевое слово «Северо-запад», то полковник Риддер снарядил отряд для сопровождения Алдара Косе обратно к казахскому ополчению" },
                    new Option { Destination = 588, Text = "Если отмечено ключевое слово «Перевал», то наш герой благодаря карте и сам пройдёт через Джунгарский Алатау" },
                    new Option { Destination = 578, Text = "Если же Алдар Косе уже побывал в урочище Аныракай (либо не хочет туда ехать), и не отмечено ни одного из указанных слов, остаётся только самостоятельно искать путь через горы" },
                }
            },
            [546] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 459, Text = "Далее" },
                }
            },
            [547] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 171, Text = "Если отмечены оба ключевых слова: «Рассвет» и «Пленник»" },
                    new Option { Destination = 336, Text = "Заплатить 100 ТАНЬГА, как того требуют воины", OnlyIf = "ТАНЬГА >= 100" },
                    new Option { Destination = 363, Text = "Обмануть их, выдав себя за посланника хунтайши" },
                    new Option { Destination = 385, Text = "Поторговаться с джунгарами, притворившись бедняком", OnlyIf = "ТАНЬГА >= 10" },
                    new Option { Destination = 404, Text = "Прорваться силой, если в кошельке Алдара не найти даже и 10 ТАНЬГА, или если джигит не хочет их искать" },
                }
            },
            [548] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Brother",
                        ButtonName = "Серик",
                        Text = "ВЫБРАТЬ СЕРИКА",
                        Trigger = "Серик",
                    },
                    new Actions
                    {
                        ActionName = "Brother",
                        ButtonName = "Берик",
                        Text = "ВЫБРАТЬ БЕРИКА",
                        Trigger = "Берик",
                    },
                    new Actions
                    {
                        ActionName = "Brother",
                        ButtonName = "Ерик",
                        Text = "ВЫБРАТЬ ЕРИКА",
                        Trigger = "Ерик",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 634, Text = "Далее" },
                }
            },
            [549] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 18, Text = "Сдружиться со стражами, чтобы остаться в зале на ночь" },
                    new Option { Destination = 376, Text = "Устроить переполох" },
                    new Option { Destination = 393, Text = "Найти способ задуть свечи и подменить реликвии" },
                }
            },
            [550] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 518, Text = "Далее" },
                }
            },
            [551] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 539, Text = "Далее" },
                }
            },
            [552] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 537, Text = "Остаться в крепости на ночлег" },
                    new Option { Destination = 373, Text = "На юг, в Кульджу" },
                    new Option { Destination = 649, Text = "На север, в Семиречье" },
                }
            },
            [553] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 356, Text = "Далее" },
                }
            },
            [554] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 657, Text = "Если отмечено четыре или более указанных слова" },
                    new Option { Destination = 543, Text = "Если же отмечено менее четырёх слов" },
                }
            },
            [555] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 13,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 70, Text = "В случае успеха" },
                    new Option { Destination = 2, Text = "В случае провала" },
                }
            },
            [556] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 572, Text = "Далее" },
                }
            },
            [557] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 575, Text = "Покинуть расположение джунгарских войск, опасаясь мстительного ойрата" },
                    new Option { Destination = 509, Text = "Если отмечены все три ключевых слова: «Воин», «Лучник» и «Река»", OnlyIf = "Воин, Лучник" },
                    new Option { Destination = 495, Text = "Если отмечены оба слова: «Воин» и «Лучник»", OnlyIf = "Воин, Лучник" },
                    new Option { Destination = 606, Text = "Если отмечено только слово «Воин»", OnlyIf = "Воин" },
                    new Option { Destination = 638, Text = "Если же ни одно из этих слов не отмечено, то придётся собирать сведения с чистого листа" },
                }
            },
            [558] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 379, Text = "Далее" },
                }
            },
            [559] = new Paragraph
            {
                Trigger = "Крепость, Логово",

                Options = new List<Option>
                {
                    new Option { Destination = 582, Text = "Далее" },
                }
            },
            [560] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = 50,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 447, Text = "Далее" },
                }
            },
            [561] = new Paragraph
            {
                Trigger = "Лучник",

                Options = new List<Option>
                {
                    new Option { Destination = 575, Text = "Покинуть эти места" },
                    new Option { Destination = 557, Text = "Продолжить сбор сведений" },
                }
            },
            [562] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 257, Text = "Далее" },
                }
            },
            [563] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 12,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 550, Text = "В случае успеха" },
                    new Option { Destination = 533, Text = "В случае провала" },
                }
            },
            [564] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 537, Text = "Остаться в крепости ночлег" },
                    new Option { Destination = 373, Text = "На юг, в Кульджу" },
                    new Option { Destination = 649, Text = "На север, в Семиречье" },
                }
            },
            [565] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 548, Text = "Дать Серику согласие на подлог письма" },
                    new Option { Destination = 557, Text = "Не вмешиваться и продолжить сбор сведений о войске противника" },
                }
            },
            [566] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 545, Text = "Далее" },
                }
            },
            [567] = new Paragraph
            {
                LateTrigger = "Боль",

                Options = new List<Option>
                {
                    new Option { Destination = 573, Text = "Конь уже был ранен раньше", OnlyIf = "Боль" },
                    new Option { Destination = 319, Text = "В противном случае отметить ключевое слово «Боль» на листе персонажа. Погоня продолжается" },
                }
            },
            [568] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = 150,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 447, Text = "Далее" },
                }
            },
            [569] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [570] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 539, Text = "Далее" },
                }
            },
            [571] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [572] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 590, Text = "Если отмечено ключевое слово «Хубилган»" },
                    new Option { Destination = 603, Text = "Если отмечено ключевое слово «Фарфор»" },
                    new Option { Destination = 609, Text = "Воспользоваться арбалетом", OnlyIf = "Арбалет" },
                    new Option { Destination = 615, Text = "При наличии нескольких слов можно посмотреть соответствующие параграфы по очереди. После этого, а также в случае, если ни одно из указанных слов не отмечено" },
                }
            },
            [573] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [574] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "TestAll",
                        ButtonName = "Проверить силу, ловкость, хитрость и мудрость",
                        Stat = "Strength, Skill, Cunning, Wisdom",
                        Level = 42,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 462, Text = "Если все четыре проверки успешны" },
                    new Option { Destination = 636, Text = "В любом другом случае" },
                }
            },
            [575] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 631, Text = "Если отмечено ключевое слово «Ущелье»" },
                    new Option { Destination = 473, Text = "Если это слово не отмечено" },
                }
            },
            [576] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = 100,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 447, Text = "Далее" },
                }
            },
            [577] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 646, Text = "Далее" },
                }
            },
            [578] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "TestAll",
                        ButtonName = "Проверить силу, ловкость, хитрость и мудрость",
                        Stat = "Strength, Skill, Cunning, Wisdom",
                        Level = 42,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 635, Text = "Если все четыре проверки успешны" },
                    new Option { Destination = 642, Text = "В любом другом случае" },
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
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 12,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 542, Text = "В случае успеха" },
                    new Option { Destination = 533, Text = "В случае провала" },
                }
            },
            [581] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 539, Text = "Далее" },
                }
            },
            [582] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 637, Text = "Направиться в крепость в обличье джунгарского воина" },
                    new Option { Destination = 644, Text = "Или купца" },
                    new Option { Destination = 649, Text = "Обратно на север, в Семиречье" },
                    new Option { Destination = 588, Text = "Если отмечено ключевое слово «Перевал»" },
                    new Option { Destination = 578, Text = "Если это слово не отмечено" },
                }
            },
            [583] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [584] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 515, Text = "Далее" },
                }
            },
            [585] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 594, Text = "Рассказано было много полезного", OnlyIf = "Барымта, Взрыв" },
                    new Option { Destination = 540, Text = "Рассказано было нечто полезное", OnlyIf = "Барымта|Взрыв" },
                    new Option { Destination = 520, Text = "Алдар Косе сообщил Штейну мало полезного" },
                }
            },
            [586] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 14,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 581, Text = "Если отмечено ключевое слово «Серик»" },
                    new Option { Destination = 551, Text = "В случае успеха" },
                    new Option { Destination = 570, Text = "В случае провала" },
                }
            },
            [587] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 14,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 531, Text = "Если отмечено ключевое слово «Солончак»" },
                    new Option { Destination = 558, Text = "В случае успеха" },
                    new Option { Destination = 567, Text = "В случае провала" },
                }
            },
            [588] = new Paragraph
            {
                Trigger = "Боль",

                Options = new List<Option>
                {
                    new Option { Destination = 502, Text = "Если Алдар ещё не был в расположении джунгарских войск на берегу Или, то на обратном пути можно направиться туда и добыть последние сведения о противнике" },
                    new Option { Destination = 517, Text = "Если же наш герой уже побывал там (либо считает, что собранных сведений достаточно), то следует объехать войска с востока, повернуть к реке и переправиться через неё" },
                }
            },
            [589] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 599, Text = "Если отмечены оба ключевых слова: «Сведения» и «Коварство»", OnlyIf = "Коварство" },
                    new Option { Destination = 645, Text = "В любом другом случае" },
                }
            },
            [590] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [591] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 13,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 559, Text = "В случае успеха" },
                    new Option { Destination = 571, Text = "В случае провала" },
                }
            },
            [592] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 565, Text = "Далее" },
                }
            },
            [593] = new Paragraph
            {
                LateTrigger = "Боль, Крепость",

                Options = new List<Option>
                {
                    new Option { Destination = 573, Text = "Если ключевое слово «Боль» уже отмечено", OnlyIf = "Боль" },
                    new Option { Destination = 587, Text = "В противном случае отметить ключевое слово «Боль» на листе персонажа. Погоня продолжается" },
                }
            },
            [594] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 5, Text = "Далее" },
                }
            },
            [595] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 515, Text = "Далее" },
                }
            },
            [596] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 627, Text = "Далее" },
                }
            },
            [597] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Danger",
                        Value = 2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 586, Text = "Далее" },
                }
            },
            [598] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 550, Text = "Далее" },
                }
            },
            [599] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 645, Text = "Далее" },
                }
            },
            [600] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 554, Text = "Если отмечено три или более указанных слова" },
                    new Option { Destination = 569, Text = "Если же отмечено менее трёх слов" },
                }
            },
            [601] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 591, Text = "Взобраться ещё выше" },
                    new Option { Destination = 582, Text = "Посчитать, что полученных сведений достаточно, спуститься вниз и продолжить путь" },
                }
            },
            [602] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 575, Text = "Если уровень ОПАСНОСТИ равен 9 или выше, придётся сразу же покинуть эти места" },
                    new Option { Destination = 592, Text = "В противном случае наш герой проникает в расположение войск и осматривается, оставив коня возле харчевни" },
                }
            },
            [603] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 572, Text = "Далее" },
                }
            },
            [604] = new Paragraph
            {
                Trigger = "Коварство",

                Options = new List<Option>
                {
                    new Option { Destination = 589, Text = "Далее" },
                }
            },
            [605] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 16, Text = "Если отмечено ключевое слово «Логово»", OnlyIf = "Логово" },
                    new Option { Destination = 629, Text = "В противном случае" },
                }
            },
            [606] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 337, Text = "Отправиться на стрельбище в качестве новобранца" },
                    new Option { Destination = 313, Text = "Потолковать с кузнецами" },
                }
            },
            [607] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 524, Text = "Притворяться и дальше мирным купцом" },
                    new Option { Destination = 555, Text = "Сообщить про всадника, виденного по дороге" },
                    new Option { Destination = 489, Text = "Рассказать о деяниях «казахского лазутчика»" },
                }
            },
            [608] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 600, Text = "Если отмечено четыре или более из указанных слов" },
                    new Option { Destination = 579, Text = "Если же отмечено меньше четырёх слов" },
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
                    new Option { Destination = 592, Text = "Привязать коня и осмотреться" },
                    new Option { Destination = 575, Text = "Сразу же покинуть эти места" },
                }
            },
            [611] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [612] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 502, Text = "Если Алдар ещё не был в расположении джунгарских войск на берегу Или, то на обратном пути можно направиться туда и добыть последние сведения о противнике" },
                    new Option { Destination = 517, Text = "Если же наш герой уже побывал там (либо считает, что собранных сведений достаточно), то следует объехать войска с востока, чтобы затем повернуть к реке и переправиться через неё" },
                }
            },
            [613] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 589, Text = "Далее" },
                }
            },
            [614] = new Paragraph
            {
                Trigger = "Боль",

                Options = new List<Option>
                {
                    new Option { Destination = 582, Text = "Далее" },
                }
            },
            [615] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 14,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 546, Text = "В случае успеха" },
                    new Option { Destination = 621, Text = "В случае провала" },
                }
            },
            [616] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 586, Text = "Далее" },
                }
            },
            [617] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 41, Text = "Покинуть контрабандистов" },
                    new Option { Destination = 52, Text = "Попытаться убедить их помочь Алдару" },
                    new Option { Destination = 64, Text = "Посулить им деньги (если они остались)" },
                }
            },
            [618] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 607, Text = "Далее" },
                }
            },
            [619] = new Paragraph
            {
                Trigger = "Воин, Лучник",

                Options = new List<Option>
                {
                    new Option { Destination = 575, Text = "Далее" },
                }
            },
            [620] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 587, Text = "Далее" },
                }
            },
            [621] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 459, Text = "Далее" },
                }
            },
            [622] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 604, Text = "Если отмечено ключевое слово «Сведения»" },
                    new Option { Destination = 613, Text = "Если это слово не отмечено" },
                }
            },
            [623] = new Paragraph
            {
                Trigger = "Доспех",
                RemoveTrigger = "Боль",

                Options = new List<Option>
                {
                    new Option { Destination = 645, Text = "Далее" },
                }
            },
            [624] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 630, Text = "Вскарабкаться по скалам, чтобы осмотреть крепость" },
                    new Option { Destination = 637, Text = "Направиться туда в обличье джунгарского воина" },
                    new Option { Destination = 644, Text = "Или купца" },
                    new Option { Destination = 649, Text = "Обратно на север, в Семиречье" },
                    new Option { Destination = 588, Text = "Если отмечено ключевое слово «Перевал»" },
                    new Option { Destination = 578, Text = "Если это слово не отмечено" },
                }
            },
            [625] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 608, Text = "Далее" },
                }
            },
            [626] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [627] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 577, Text = "Если отмечены оба ключевых слова «Сведения» и «Коварство»", OnlyIf = "Коварство" },
                    new Option { Destination = 633, Text = "Если отмечено только слово «Сведения»" },
                    new Option { Destination = 646, Text = "В любом другом случае" },
                }
            },
            [628] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 619, Text = "Имя Алдана знакомо Алдару", OnlyIf ="Алдан" },
                    new Option { Destination = 610, Text = "Если отмечено ключевое слово «Хуяг»" },
                    new Option { Destination = 602, Text = "Если не отмечено ни одно из этих слов" },
                }
            },
            [629] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 643, Text = "Если отмечены оба ключевых слова: «Сведения» и «Коварство»", OnlyIf = "Коварство" },
                    new Option { Destination = 617, Text = "Если отмечено только слово «Сведения»" },
                    new Option { Destination = 29, Text = "В любом другом случае" },
                }
            },
            [630] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить силу",
                        Stat = "Strength",
                        Level = 13,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 601, Text = "В случае успеха" },
                    new Option { Destination = 614, Text = "В случае провала" },
                }
            },
            [631] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 517, Text = "Далее" },
                }
            },
            [632] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 587, Text = "Далее" },
                }
            },
            [633] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 623, Text = "Далее" },
                }
            },
            [634] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить силу",
                        Stat = "Strength",
                        Level = 14,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 616, Text = "В случае успеха" },
                    new Option { Destination = 597, Text = "В случае провала" },
                    new Option { Destination = 641, Text = "Имя Берика знакомо Алдару", OnlyIf = "Берик", },
                }
            },
            [635] = new Paragraph
            {
                Trigger = "Боль",

                Options = new List<Option>
                {
                    new Option { Destination = 517, Text = "Далее" },
                }
            },
            [636] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [637] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 552, Text = "Если отмечено ключевое слово «Подлог»" },
                    new Option { Destination = 564, Text = "Иначе придётся сослаться на тайное поручение" },
                }
            },
            [638] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 401, Text = "Записаться в новобранцы" },
                    new Option { Destination = 444, Text = "Поговорить с купцами" },
                }
            },
            [639] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 587, Text = "Далее" },
                }
            },
            [640] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [641] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 586, Text = "Далее" },
                }
            },
            [642] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [643] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 29, Text = "Далее" },
                }
            },
            [644] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 618, Text = "Заплатить 50 ТАНЬГА, если есть желание это сделать", OnlyIf = "ТАНЬГА >= 50" },
                    new Option { Destination = 649, Text = "Обратно на север, в Семиречье" },
                    new Option { Destination = 588, Text = "Если отмечено ключевое слово «Перевал»" },
                    new Option { Destination = 578, Text = "Если это слово не отмечено" },
                }
            },
            [645] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 652, Text = "Если ОПАСНОСТЬ равна 9 или больше" },
                    new Option { Destination = 587, Text = "Если ОПАСНОСТЬ равна 6, 7 или 8" },
                    new Option { Destination = 319, Text = "Если ОПАСНОСТЬ равна 5 или меньше" },
                }
            },
            [646] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 257, Text = "Далее" },
                }
            },
            [647] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 592, Text = "Далее" },
                }
            },
            [648] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 608, Text = "Далее" },
                }
            },
            [649] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 502, Text = "Если Алдар ещё не был в расположении джунгарских войск на берегу Или, то можно направиться туда и добыть последние сведения о противнике" },
                    new Option { Destination = 517, Text = "Если же наш герой уже побывал там (либо считает, что собранных сведений достаточно), то следует объехать войска с востока, чтобы затем повернуть к реке и переправиться через неё" },
                }
            },
            [650] = new Paragraph
            {
                Trigger = "Коварство",

                Options = new List<Option>
                {
                    new Option { Destination = 517, Text = "Продолжить путешествие в сторону реки" },
                }
            },
            [651] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 343, Text = "Если отмечены оба ключевых слова «Казах» и «Русский»", OnlyIf = "Казах" },
                    new Option { Destination = 349, Text = "Если отмечено только ключевое слово «Русский»" },
                    new Option { Destination = 355, Text = "Если отмечено только ключевое слово «Казах»", OnlyIf = "Казах" },
                    new Option { Destination = 331, Text = "Если отмечено ключевое слово «Мугати»", OnlyIf = "Мугати" },
                }
            },
            [652] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 14,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 620, Text = "В случае успеха" },
                    new Option { Destination = 611, Text = "В случае провала" },
                    new Option { Destination = 632, Text = "У Алдара есть доспехи", OnlyIf = "Доспех" },
                    new Option { Destination = 639, Text = "Если нет, но отмечено хотя бы одно из ключевых слов: «Тулпар» или «Скакун»" },
                    new Option { Destination = 593, Text = "Иначе можно просто подстегнуть коня, чтобы побыстрее проскочить мимо зембуреков" },
                }
            },
            [653] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 7, Text = "Далее" },
                }
            },
            [654] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 300, Text = "Если отмечено слово «Кульджа»", OnlyIf = "Кульджа" },
                    new Option { Destination = 373, Text = "Иначе" },
                }
            },
            [655] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 517, Text = "Далее" },
                }
            },
            [656] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить силу",
                        Stat = "Strength",
                        Level = 14,
                    },
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 12,
                    },
                },

                Trigger = "Лучник",

                Options = new List<Option>
                {
                    new Option { Destination = 322, Text = "Вернуться в пещеру отшельника" },
                    new Option { Destination = 286, Text = "В случае успеха" },
                    new Option { Destination = 322, Text = "В случае провала: если это была первая проверка способностей в мире теней" },
                    new Option { Destination = 414, Text = "Иначе" },
                }
            },
            [657] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
	        [658] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Выбрать силу",
                        Text = "СИЛА",
                        Stat = "Strength",
                        StatToMax = true,
                        Aftertext = "Способность персонажа поднимать и переносить тяжести, бороться, а также общая выносливость.",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Выбрать ловкость",
                        Text = "ЛОВКОСТЬ",
                        Stat = "Skill",
                        StatToMax = true,
                        Aftertext = "Скорость реакции, умение держать равновесие, уворачиваться, прыгать, а также ловкость рук (точные движения кистей и пальцев, включая игру на музыкальных инструментах).",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Выбрать мудрость",
                        Text = "МУДРОСТЬ",
                        Stat = "Wisdom",
                        StatToMax = true,
                        Aftertext = "Общие знания, жизненный опыт, способность к анализу и логическим рассуждениям, умение замечать детали и делать правильные выводы.",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Выбрать хитрость",
                        Text = "ХИТРОСТЬ",
                        Stat = "Cunning",
                        StatToMax = true,
                        Aftertext = "Способность обманывать, жульничать, воровские навыки, а также находчивость и умение быстро находить решения в сложных ситуациях.",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Выбрать красноречие",
                        Text = "КРАСНОРЕЧИЕ",
                        Stat = "Oratory",
                        StatToMax = true,
                        Aftertext = "Способность разговорить собеседника, дар убеждения, умение слагать стихи и песни.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 659, Text = "Далее" },
                }
            },
	    [659] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить силы",
                        Text = "СИЛА",
                        Stat = "Strength",
                        Aftertext = "Способность персонажа поднимать и переносить тяжести, бороться, а также общая выносливость.",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить ловкости",
                        Text = "ЛОВКОСТЬ",
                        Stat = "Skill",
                        Aftertext = "Скорость реакции, умение держать равновесие, уворачиваться, прыгать, а также ловкость рук (точные движения кистей и пальцев, включая игру на музыкальных инструментах).",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить мудрости",
                        Text = "МУДРОСТЬ",
                        Stat = "Wisdom",
                        Aftertext = "Общие знания, жизненный опыт, способность к анализу и логическим рассуждениям, умение замечать детали и делать правильные выводы.",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить хитрости",
                        Text = "ХИТРОСТЬ",
                        Stat = "Cunning",
                        Aftertext = "Способность обманывать, жульничать, воровские навыки, а также находчивость и умение быстро находить решения в сложных ситуациях.",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить красноречия",
                        Text = "КРАСНОРЕЧИЕ",
                        Stat = "Oratory",
                        Aftertext = "Способность разговорить собеседника, дар убеждения, умение слагать стихи и песни.\n\nАвтор советует не распылять эти пункты, а определить те качества, которые вы считаете важными. Ведь каждый человек уникален. Каким вы видите народного героя? Кроме того, книга-игра тем и хороша, что приключение можно повторить, выбрав нового персонажа с другими навыками.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 1, Text = "Далее" },
                }
            },
        };
    }
}
