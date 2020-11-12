using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Seeker.Game;

namespace Seeker.Gamebook.LegendsAlwaysLie
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
            paragraph.RemoveTrigger = source.RemoveTrigger;

            return paragraph;
        }

        private static Dictionary<int, Paragraph> Paragraph = new Dictionary<int, Paragraph>
        {
            [0] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 701, Text = "В путь!" },
                }
            },
            [1] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 10, Text = "Далее" },
                }
            },
            [2] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 18, Text = "Ответите «да»" },
                    new Option { Destination = 43, Text = "Попросите Коннери первым продемонстрировать свои атлетические навыки" },
                }
            },
            [3] = new Paragraph
            {
                Trigger = "Higher",

                Options = new List<Option>
                {
                    new Option { Destination = 35, Text = "Далее" },
                }
            },
            [4] = new Paragraph
            {
                Trigger = "Study",

                Options = new List<Option>
                {
                    new Option { Destination = 70, Text = "Далее" },
                }
            },
            [5] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 20, Text = "Вы поздороваетесь в ответ" },
                    new Option { Destination = 47, Text = "Пошлете подальше говорящую гориллу" },
                    new Option { Destination = 75, Text = "Проигнорируете" },
                }
            },
            [6] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 90, Text = "Далее" },
                }
            },
            [7] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 35, Text = "Далее" },
                }
            },
            [8] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 34, Text = "Вы будете пересекать рой, прикрывшись одеялом" },
                    new Option { Destination = 17, Text = "Предпочтете непокрытую голову и хорошую видимость" },
                }
            },
            [9] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 35, Text = "Далее" },
                }
            },
            [10] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 72, Text = "Зачем мы едем в Тирион" },
                    new Option { Destination = 94, Text = "Как он планирует добраться до горы Рантагенет" },
                    new Option { Destination = 83, Text = "Почему его зовут «Коннери из Таннендока» и где этот чертов Таннендок вообще находится" },
                    new Option { Destination = 51, Text = "Правду ли говорят о резне, которую он устроил на Соборной площади" },
                    new Option { Destination = 30, Text = "Доесть в тишине" },
                }
            },
            [11] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -3,
                    },
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 44, Text = "Далее" },
                }
            },
            [12] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 53, Text = "Вы будете настаивать на своем" },
                    new Option { Destination = 92, Text = "Послушаетесь" },
                }
            },
            [13] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryTrust",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 84, Text = "Бросить джинну любую вещь из своего заплечного мешка" },
                    new Option { Destination = 104, Text = "Метнуться к Коннери" },
                }
            },
            [14] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 31, Text = "Далее" },
                }
            },
            [15] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 26, Text = "Далее" },
                }
            },
            [16] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 33, Text = "Успели" },
                    new Option { Destination = 11, Text = "Нет" },
                }
            },
            [17] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 115, Text = "Вы будете ставить ЗАСЛОН, чтобы быть прикрытым остаток пути хоть с одной стороны" },
                    new Option { Destination = 65, Text = "Прорываться без помощи магии" },
                }
            },
            [18] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -2,
                    },
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 96, Text = "Далее" },
                }
            },
            [19] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 37, Text = "Вы предпочтете синицу в руках" },
                    new Option { Destination = 109, Text = "или продолжите поиски более удобного места в сгущающихся сумерках" },
                }
            },
            [20] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryTrust",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 61, Text = "Далее" },
                }
            },
            [21] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 63, Text = "Далее" },
                }
            },
            [22] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 48, Text = "Далее" },
                }
            },
            [23] = new Paragraph
            {
                Trigger = "Scream",

                Options = new List<Option>
                {
                    new Option { Destination = 112, Text = "Далее" },
                }
            },
            [24] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 114, Text = "Далее" },
                }
            },
            [25] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [26] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 40, Text = "Ты ж вроде на горшочке с золотом должен сидеть" },
                    new Option { Destination = 60, Text = "И что, давно последние гости были?" },
                    new Option { Destination = 82, Text = "А почему никто не охранял тот круг менгиров, через который мы вошли?" },
                }
            },
            [27] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [28] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 101, Text = "Далее" },
                }
            },
            [29] = new Paragraph
            {
                Trigger = "Tincture",

                Options = new List<Option>
                {
                    new Option { Destination = 90, Text = "Далее" },
                }
            },
            [30] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 69, Text = "Вежливо попросить его отойти" },
                    new Option { Destination = 120, Text = "Без лишних разговоров заехать ему с правой в челюсть" },
                }
            },
            [31] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 62, Text = "Ответите «Я хочу загадать желание»" },
                    new Option { Destination = 13, Text = "Сорвете кошель с пояса и бросите джинну" },
                    new Option { Destination = 104, Text = "Метнетесь в дверь, которую Коннери еще держит для вас открытой" },
                }
            },
            [32] = new Paragraph
            {
                Trigger = "RustyBlade",

                Options = new List<Option>
                {
                    new Option { Destination = 70, Text = "Далее" },
                }
            },
            [33] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 44, Text = "Далее" },
                }
            },
            [34] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте",
                    },
                },

                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 67, Text = "Успели" },
                    new Option { Destination = 58, Text = "Нет" },
                }
            },
            [35] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 9, Text = "«Мифология великанов», профессор фон Остерман" },
                    new Option { Destination = 76, Text = "«География полуострова Штарберген» перевод с гномьего, аббат Линия" },
                    new Option { Destination = 54, Text = "«Реликты», магистр Аберман Энзанский" },
                    new Option { Destination = 7, Text = "«Над пропастью во ржи: крестьянский фольклор о проделках маленького народца», издано на средства Лиасского университета" },
                    new Option { Destination = 100, Text = "«Легенды подгорных кузнецов», авторов аж трое и все вам неизвестны" },
                    new Option { Destination = 95, Text = "Когда время выходит, вы благополучно засыпаете, убаюканный рядами букв" },
                }
            },
            [36] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 55, Text = "Может, будет благоразумнее предложить Коннери вернуться на развилку" },
                    new Option { Destination = 27, Text = "Или же вы решите продолжить путь вперед" },
                }
            },
            [37] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = 4,
                    },
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = 4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 52, Text = "Далее" },
                }
            },
            [38] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -5,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 81, Text = "Далее" },
                }
            },
            [39] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [40] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 79, Text = "А язык у тебя подвешен, низкосракий прощелыга" },
                    new Option { Destination = 28, Text = "Ну ладно, 1-1. Нет горшочка и не надо" },
                }
            },
            [41] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 5, Text = "Далее" },
                }
            },
            [42] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 90, Text = "Далее" },
                }
            },
            [43] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 71, Text = "Будете прыгать на другой берег без разбега" },
                    new Option { Destination = 24, Text = "Метнете нож (если умеете)" },
                    new Option { Destination = 87, Text = "ЗАСЛОН" },
                }
            },
            [44] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 31, Text = "Далее" },
                }
            },
            [45] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
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
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryTrust",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 61, Text = "Далее" },
                }
            },
            [48] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 92, Text = "Далее" },
                }
            },
            [49] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",
                        ConneryAttacks = "3, 3",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРНАТЫЙ ДЕМОН",
                                Strength = 11,
                                Hitpoints = 12,
                            },
                        },

                        Aftertext = "Первые три раунда вам придется провести самостоятельно, потом к вам присоединяется Коннери. Начиная с четвертого раунда отнимайте у демона автоматически по три ЖИЗНИ за раунд – ваш напарник мастер наносить неотразимые удары.",
                    },
                },

                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 23, Text = "Далее" },
                }
            },
            [50] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 97, Text = "Далее" },
                }
            },
            [51] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryTrust",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Далее" },
                }
            },
            [52] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 70, Text = "Вы подскочите как угорелый и броситесь на поиски" },
                    new Option { Destination = 32, Text = "Не спеша соберетесь и лишь потом начнете розыск" },
                }
            },
            [53] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryTrust",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 92, Text = "Далее" },
                }
            },
            [54] = new Paragraph
            {
                Trigger = "Verse",

                Options = new List<Option>
                {
                    new Option { Destination = 35, Text = "Далее" },
                }
            },
            [55] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 38, Text = "Прикрой!" },
                    new Option { Destination = 99, Text = "Руби, я прикрою!" },
                }
            },
            [56] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryTrust",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 41, Text = "Далее" },
                }
            },
            [57] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 70, Text = "Далее" },
                }
            },
            [58] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 110, Text = "Продолжать двигаться вперед, надеясь, что ничего страшного с ним не случится" },
                    new Option { Destination = 85, Text = "Сбросить одеяло и спасать напарника (тогда сразу вычеркните одеяло из своих вещей, оно останется на дороге)" },
                }
            },
            [59] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 2, Text = "Далее" },
                }
            },
            [60] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 101, Text = "Далее" },
                }
            },
            [61] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 81, Text = "Идем прямо" },
                    new Option { Destination = 36, Text = "Идем направо" },
                    new Option { Destination = 12, Text = "Разделяемся" },
                }
            },
            [62] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [63] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "DiceWounds",
                        ButtonName = "Кинуть кубик",
                        Dices = 1,
                        Aftertext = "Коннери протягивает руку, но вы гордо мотаете головой и пружинисто поднимаетесь сами. Легенда ты или нет, дядя, но я еще и сам могу встать.\n\nА потом через тропу вдруг стремглав проносится белый пушистый кролик. От неожиданности вы оба хватаетесь за мечи, и не напрасно. Следом за ним огромными легкими скачками бежит длинный и худой птицеподобный демон. Пересекая Тропу, он шипит от боли – находиться на ней ему явно дискомфортно. Но тут он замечает вас, и с удивлением замирает на месте. Блестящие перья топорщатся, глазные яблоки бешено вращаются, клюв удивленно нацеливается на вас:\n\n– Ссссмертные? На Тропе? Я не видел смертных уже очень, очень давно.\n\nКоннери открывает рот, чтобы ответить.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 21, Text = "Выпала шестёрка" },
                    new Option { Destination = 49, Text = "Вы дадите Коннери сказать" },
                    new Option { Destination = 119, Text = "Скажете «А я давненько не шинковал в капусту уродливых демонов»" },
                    new Option { Destination = 89, Text = "Скажете «Следуй за белым кроликом, птица»" },
                }
            },
            [64] = new Paragraph
            {
                Trigger = "Ogadir",

                Options = new List<Option>
                {
                    new Option { Destination = 90, Text = "Далее" },
                }
            },
            [65] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -12,
                    },
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -6,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 91, Text = "Далее" },
                }
            },
            [66] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 3, Text = "Расскажите поподробнее, как вы познакомились?" },
                    new Option { Destination = 80, Text = "Какого рода опасность может таиться в телепортации через круг камней?" },
                }
            },
            [67] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 91, Text = "Далее" },
                }
            },
            [68] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 50, Text = "Далее" },
                }
            },
            [69] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 88, Text = "Далее" },
                }
            },
            [70] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 97, Text = "Далее" },
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
                    new Option { Destination = 30, Text = "Далее" },
                }
            },
            [73] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 22, Text = "Вы согласитесь заплатить" },
                    new Option { Destination = 48, Text = "Откажетесь и уйдете" },
                }
            },
            [74] = new Paragraph
            {
                Trigger = "Tears",

                Options = new List<Option>
                {
                    new Option { Destination = 15, Text = "Далее" },
                }
            },
            [75] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 61, Text = "Далее" },
                }
            },
            [76] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 35, Text = "Далее" },
                }
            },
            [77] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 105, Text = "Будете настаивать на своем" },
                    new Option { Destination = 92, Text = "Согласитесь, что продолжить путь – это лучшее решение" },
                }
            },
            [78] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 112, Text = "Далее" },
                }
            },
            [79] = new Paragraph
            {
                Trigger = "Babble",

                Options = new List<Option>
                {
                    new Option { Destination = 101, Text = "Далее" },
                }
            },
            [80] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 35, Text = "Далее" },
                }
            },
            [81] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 59, Text = "Вы схватите его за плечо, чтобы остановить" },
                    new Option { Destination = 102, Text = "Треснете гардой меча по затылку с теми же целями" },
                }
            },
            [82] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 28, Text = "Далее" },
                }
            },
            [83] = new Paragraph
            {
                Trigger = "Time",

                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Далее" },
                }
            },
            [84] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryTrust",
                        Value = -1,
                    },
                },

                Trigger = "Money",

                Options = new List<Option>
                {
                    new Option { Destination = 15, Text = "Далее" },
                }
            },
            [85] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 115, Text = "Вы будете ставить ЗАСЛОН, чтобы быть прикрытым остаток пути хоть с одной стороны" },
                    new Option { Destination = 65, Text = "Прорываться без помощи магии" },
                }
            },
            [86] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -6,
                    },
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -2,
                    },
                    new Modification
                    {
                        Name = "ConneryTrust",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 112, Text = "Далее" },
                }
            },
            [87] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 114, Text = "Далее" },
                }
            },
            [88] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 66, Text = "Вы можете еще посидеть с попутчиками за столом" },
                    new Option { Destination = 35, Text = "Подняться в комнату" },
                }
            },
            [89] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 49, Text = "Вы можете предоставить напарнику слово" },
                    new Option { Destination = 103, Text = "«Перестану…»" },
                    new Option { Destination = 98, Text = "«Буду…»" },
                }
            },
            [90] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 117, Text = "«Закат эпохи», перевод с эльфийского, магистр Франц" },
                    new Option { Destination = 29, Text = "«Хождение за три моря, в диковинную страну мороза и великанов, писаное купцом Афаннаджей»" },
                    new Option { Destination = 42, Text = "«Памятники дочеловеческих рас: беседы с эльфийскими мудрецами», в авторах числится добрый десяток ученых и переводчиков" },
                    new Option { Destination = 64, Text = "Этот вариант доступен лишь при условии, что прошлой ночью вы уже начали читать эту книгу" },
                    new Option { Destination = 106, Text = "Когда вы скоротаете время перед сном, откладывайте книгу и придвигайте к себе подушку" },
                }
            },
            [91] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 16, Text = "Далее" },
                }
            },
            [92] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 63, Text = "Направо" },
                    new Option { Destination = 8, Text = "Налево" },
                }
            },
            [93] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = 4,
                    },
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = 4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 4, Text = "Если у вас записано слово крик", OnlyIf = "Scream" },
                    new Option { Destination = 57, Text = "Надо вставать" },
                }
            },
            [94] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Далее" },
                }
            },
            [95] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 56, Text = "Если у вас записано слово время", OnlyIf = "Time" },
                    new Option { Destination = 41, Text = "А пока остается лишь ждать да провожать взглядом пушистые донельзя облака" },
                }
            },
            [96] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 77, Text = "«Я сверну, прикрой»" },
                    new Option { Destination = 92, Text = "«Идем дальше»" },
                }
            },
            [97] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 121, Text = "Далее" },
                }
            },
            [98] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 119, Text = "Далее" },
                }
            },
            [99] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -4,
                    },
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 81, Text = "Далее" },
                }
            },
            [100] = new Paragraph
            {
                Trigger = "Tact",

                Options = new List<Option>
                {
                    new Option { Destination = 35, Text = "Далее" },
                }
            },
            [101] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 118, Text = "Если у вас записано слово деньги", OnlyIf = "Money" },
                    new Option { Destination = 19, Text = "Вы прислушаетесь к своему внутреннему голосу и откажетесь" },
                    new Option { Destination = 90, Text = "Доверитесь выбору Коннери и примете предложение лепрекона (сразу отнимите у себя три золотых)" },
                }
            },
            [102] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 2, Text = "Далее" },
                }
            },
            [103] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 78, Text = "Вы подойдете к демону" },
                    new Option { Destination = 49, Text = "Дадите, наконец, Коннери высказаться" },
                }
            },
            [104] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 74, Text = "Успели" },
                    new Option { Destination = 62, Text = "Нет" },
                }
            },
            [105] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryTrust",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 25, Text = "Пешкой на h7" },
                    new Option { Destination = 46, Text = "Королем на d3" },
                    new Option { Destination = 73, Text = "Слоном на d4" },
                }
            },
            [106] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = 5,
                    },
                },

                Trigger = "Panic",

                Options = new List<Option>
                {
                    new Option { Destination = 68, Text = "Немедленно броситься на поиски" },
                    new Option { Destination = 113, Text = "Аккуратно одеться, почистить зубы нитью и не спеша выйти из пещеры" },
                }
            },
            [107] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 170, Text = "Далее" },
                }
            },
            [108] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 172, Text = "К мосту" },
                    new Option { Destination = 128, Text = "К тропинке" },
                }
            },
            [109] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 93, Text = "Продолжать поиски удобного места" },
                    new Option { Destination = 37, Text = "Вернетесь к сухому дереву и заночуете там" },
                }
            },
            [110] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 91, Text = "Далее" },
                }
            },
            [111] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -6,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 170, Text = "Далее" },
                }
            },
            [112] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 14, Text = "Вы решитесь подойти" },
                    new Option { Destination = 31, Text = "Пройдете мимо" },
                }
            },
            [113] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 50, Text = "Далее" },
                }
            },
            [114] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 96, Text = "Далее" },
                }
            },
            [115] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 91, Text = "Далее" },
                }
            },
            [116] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 170, Text = "Далее" },
                }
            },
            [117] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 90, Text = "Далее" },
                }
            },
            [118] = new Paragraph
            {
                Trigger = "Nuances",

                Options = new List<Option>
                {
                    new Option { Destination = 6, Text = "Попросите, чтобы напарник заплатил за вас" },
                    new Option { Destination = 19, Text = "Откажетесь от предложения лепрекона" },
                }
            },
            [119] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 86, Text = "Успешно" },
                    new Option { Destination = 45, Text = "Нет" },
                }
            },
            [120] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryTrust",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 88, Text = "Далее" },
                }
            },
            [121] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 160, Text = "Прямо" },
                    new Option { Destination = 192, Text = "Обходим справа" },
                    new Option { Destination = 218, Text = "Обходим слева" },
                }
            },
            [122] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 253, Text = "Далее" },
                }
            },
            [123] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",
                        RoundsToWin = 5,

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВЕЛИКАН-КОПЬЕНОСЕЦ",
                                Strength = 11,
                                Hitpoints = 10,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 306, Text = "Если вы управились за 5 раундов" },
                    new Option { Destination = 176, Text = "Если вы провозились дольше, то погоня успевает вас настигнуть" },
                }
            },
            [124] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -5,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 259, Text = "Далее" },
                }
            },
            [125] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 205, Text = "Далее" },
                }
            },
            [126] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = 2,
                    },
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = 2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 156, Text = "Двинуться прямо на север, в пологое ущелье, на дне которого струится туман" },
                    new Option { Destination = 368, Text = "В лесистое ущелье к северо-востоку от вас" },
                    new Option { Destination = 339, Text = "В уходящее зигзагами в неизвестность узкое ущелье слева" },
                    new Option { Destination = 287, Text = "Правда, Коннери утверждает, что слышит, как с той стороны доносится странный стук" },
                }
            },
            [127] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 296, Text = "Далее" },
                }
            },
            [128] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 193, Text = "Успели" },
                    new Option { Destination = 152, Text = "Нет" },
                }
            },
            [129] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 368, Text = "То, что прямо перед вами, покрыто буйной зеленой растительностью" },
                    new Option { Destination = 156, Text = "Левее, на северо-западе, ущелье скрыто плотным белым туманом" },
                }
            },
            [130] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 197, Text = "Далее" },
                }
            },
            [131] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 212, Text = "Вдоль правой" },
                    new Option { Destination = 179, Text = "Вдоль левой" },
                }
            },
            [132] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 203, Text = "Далее" },
                }
            },
            [133] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",
                        ConneryAttacks = "2",
                        AttackWounds = 3,

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВЕЛИКАН",
                                Strength = 13,
                                Hitpoints = 16,
                            },
                        },

                        Aftertext = "Несмотря на то, что вас двое, это непростой бой. Уж очень здоровый бугай вам попался. После победы вы осматриваете его, находите 2 золотых, и моток медной проволоки.\n\nБерите все, что захотите, и двигайтесь дальше, пока не появились его сородичи.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 148, Text = "Далее" },
                }
            },
            [134] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 174, Text = "Далее" },
                }
            },
            [135] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 171, Text = "Далее" },
                }
            },
            [136] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 173, Text = "«До середины леса»" },
                    new Option { Destination = 145, Text = "«До конца леса»" },
                    new Option { Destination = 208, Text = "«И волк, и лес – все это суть иллюзия»" },
                }
            },
            [137] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 217, Text = "Спрятаться за стеной, надеясь что все обойдется" },
                    new Option { Destination = 341, Text = "Войти в крипту" },
                }
            },
            [138] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 155, Text = "Далее" },
                }
            },
            [139] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 182, Text = "Вы сделаете это" },
                    new Option { Destination = 224, Text = "Оставите ему жизнь" },
                }
            },
            [140] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 151, Text = "Кто автор" },
                    new Option { Destination = 127, Text = "О чем сложена эта баллада" },
                }
            },
            [141] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -1,
                    },
                },

                Trigger = "Cache",

                Options = new List<Option>
                {
                    new Option { Destination = 338, Text = "Далее" },
                }
            },
            [142] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте в первый раз",

                        Damage = new Modification
                        {
                            Name = "Hitpoints",
                            Empty = true,
                        },
                    },
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте во второй раз",

                        Damage = new Modification
                        {
                            Name = "Hitpoints",
                            Empty = true,
                        },
                    },
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте в третий раз",

                        Damage = new Modification
                        {
                            Name = "Hitpoints",
                            Empty = true,
                        },

                        Aftertext = "Если хоть раз не удалось, то линия вашей судьбы оборвется здесь, в грязи богом забытого ущелья…",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 125, Text = "Далее" },
                }
            },
            [143] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 297, Text = "Далее" },
                }
            },
            [144] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 129, Text = "Далее" },
                }
            },
            [145] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 371, Text = "Далее" },
                }
            },
            [146] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryTrust",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 231, Text = "Далее" },
                }
            },
            [147] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 324, Text = "Далее" },
                }
            },
            [148] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 156, Text = "Низина, что правее вашего курса, вся укрыта плотным белым туманом" },
                    new Option { Destination = 339, Text = "Прямо расположено зигзагообразное глубокое ущелье, что скрывается за его поворотом – неизвестно" },
                }
            },
            [149] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 124, Text = "Ударите в воду СГУСТКОМ" },
                    new Option { Destination = 178, Text = "Потянетесь к мечу за спиной" },
                    new Option { Destination = 159, Text = "Попытаетесь разжать хватку" },
                }
            },
            [150] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 225, Text = "Пробиваться с боем" },
                    new Option { Destination = 194, Text = "Вернуться по тропе и выбрать другой путь" },
                }
            },
            [151] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 296, Text = "Далее" },
                }
            },
            [152] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 351, Text = "Далее" },
                }
            },
            [153] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 108, Text = "Далее" },
                }
            },
            [154] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 384, Text = "Оттолкнуть стареющую легенду и принять удар на себя" },
                    new Option { Destination = 228, Text = "Отступать вместе с напарником, подначив его словами: «Да что с тобой, совсем реакция пропала?»" },
                }
            },
            [155] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 176, Text = "Броситесь к Коннери, чтобы вместе сразиться с конунгом" },
                    new Option { Destination = 219, Text = "Метнетесь за своими вещами" },
                }
            },
            [156] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 395, Text = "Прямо, к ручью, рядом с которым периодически бьют в воздух все те же гейзеры? В этот раз видимость хорошая, и они, возможно, уже не будут представлять такой опасности" },
                    new Option { Destination = 221, Text = "Правее, забирая на северо-восток, к одинокой башне" },
                    new Option { Destination = 373, Text = "Левее, поднимаясь на поросшую кустарником возвышенность" },
                }
            },
            [157] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 108, Text = "Далее" },
                }
            },
            [158] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",
                        AttackWounds = 3,

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВЕЛИКАН",
                                Strength = 13,
                                Hitpoints = 14,
                            },
                        },

                        Aftertext = "Он очень опасный противник и вы можете предпринять попытку убежать. Если же вы смогли его одолеть, то можете позаимствовать из карманов противника 2 золотых и бронзовый ключ. После уходите.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 174, Text = "Предпринять попытку убежать" },
                    new Option { Destination = 229, Text = "Вы победили" },
                }
            },
            [159] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 223, Text = "Да" },
                    new Option { Destination = 201, Text = "Нет" },
                }
            },
            [160] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 192, Text = "Подняться обратно, обойдя ущелье справа" },
                    new Option { Destination = 218, Text = "Подняться обратно, обойдя ущелье слева" },
                    new Option { Destination = 131, Text = "Продолжить спуск" },
                }
            },
            [161] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryTrust",
                        Value = 1,
                    },
                },

                Trigger = "Note",

                Options = new List<Option>
                {
                    new Option { Destination = 207, Text = "Далее" },
                }
            },
            [162] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 321, Text = "Если у вас записано слово сфера", OnlyIf = "Sphere" },
                    new Option { Destination = 230, Text = "Если у вас есть ржавый кинжал", OnlyIf = "RustyBlade" },
                    new Option { Destination = 215, Text = "Предложить ему заточить ваш меч" },
                }
            },
            [163] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 327, Text = "Далее" },
                }
            },
            [164] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 131, Text = "Далее" },
                }
            },
            [165] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 207, Text = "Далее" },
                }
            },
            [166] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 143, Text = "Если у вас записано слово посох", OnlyIf = "Staff" },
                    new Option { Destination = 0, Text = "Начать с начала", OnlyIf = "!Staff"  },
                }
            },
            [167] = new Paragraph
            {
                Trigger = "Amulet",

                Options = new List<Option>
                {
                    new Option { Destination = 337, Text = "Далее" },
                }
            },
            [168] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 204, Text = "Исследовать пещеру" },
                    new Option { Destination = 216, Text = "Не искать приключений на свою пятую точку и продолжить свой путь" },
                }
            },
            [169] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 136, Text = "Север" },
                    new Option { Destination = 285, Text = "Северо-восток" },
                }
            },
            [170] = new Paragraph
            {
                Trigger = "Debt",

                Options = new List<Option>
                {
                    new Option { Destination = 388, Text = "Далее" },
                }
            },
            [171] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 274, Text = "Левее, на северо-запад" },
                    new Option { Destination = 188, Text = "Прямо по курсу" },
                }
            },
            [172] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВЕЛИКАН",
                                Strength = 12,
                                Hitpoints = 12,
                            },
                        },

                        Aftertext = "Победа откроет вам путь.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 244, Text = "Далее" },
                }
            },
            [173] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 371, Text = "Далее" },
                }
            },
            [174] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 251, Text = "Далее" },
                }
            },
            [175] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 148, Text = "Далее" },
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
                Options = new List<Option>
                {
                    new Option { Destination = 132, Text = "Вонзить тигру меч между ребер" },
                    new Option { Destination = 198, Text = "Ударить плашмя и заорать, просто отвлекая его внимание" },
                }
            },
            [178] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 201, Text = "Далее" },
                }
            },
            [179] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [180] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 207, Text = "Далее" },
                }
            },
            [181] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 144, Text = "Если ваша специализация метатель" },
                    new Option { Destination = 292, Text = "Принять бой" },
                }
            },
            [182] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryTrust",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 289, Text = "Далее" },
                }
            },
            [183] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [184] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 297, Text = "Далее" },
                }
            },
            [185] = new Paragraph
            {
                Trigger = "Turf",

                Options = new List<Option>
                {
                    new Option { Destination = 233, Text = "Далее" },
                }
            },
            [186] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 350, Text = "Далее" },
                }
            },
            [187] = new Paragraph
            {
                Trigger = "EvilEye",

                Options = new List<Option>
                {
                    new Option { Destination = 157, Text = "Далее" },
                }
            },
            [188] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 381, Text = "Вы все же поможете великану" },
                    new Option { Destination = 388, Text = "Тихо скроетесь меж камней и продолжите свой путь" },
                }
            },
            [189] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 304, Text = "Если у вас записано слово высший", OnlyIf = "Higher" },
                    new Option { Destination = 377, Text = "Сражаться" },
                    new Option { Destination = 247, Text = "Попытаться выбраться из пещеры" },
                }
            },
            [190] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 220, Text = "Удерживать брыкающегося напарника" },
                    new Option { Destination = 258, Text = "Ослабите захват, позволив ему сделать то, что он собирался" },
                }
            },
            [191] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 333, Text = "«Как обстановка в клане?»" },
                    new Option { Destination = 140, Text = "«Не прочтет ли он вам какое-нибудь стихотворение?»" },
                    new Option { Destination = 210, Text = "«Не видел ли он поблизости других людей?»" },
                }
            },
            [192] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 238, Text = "Вы изготовитесь к бою" },
                    new Option { Destination = 211, Text = "Метнетесь влево, чтобы посмотреть как там Коннери" },
                }
            },
            [193] = new Paragraph
            {
                Trigger = "Missing",

                Options = new List<Option>
                {
                    new Option { Destination = 214, Text = "Попытаетесь рассмотреть с обрыва, не появится ли Коннери в воде" },
                    new Option { Destination = 351, Text = "Разбежитесь и прыгнете в реку" },
                    new Option { Destination = 234, Text = "Рванете дальше, чтобы быстрее проскользнуть за поворот" },
                }
            },
            [194] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 291, Text = "Далее" },
                }
            },
            [195] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 161, Text = "Если у вас записано слово настой", OnlyIf = "Tincture" },
                    new Option { Destination = 207, Text = "Прихватить пергамент с собой и выбрать направление движения" },
                }
            },
            [196] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 709, Text = "Шагаете вперед и вонзаете меч в жреца горного клана" },
                    new Option { Destination = 397, Text = "Шагаете назад, выталкивая напарника обратно в коридор" },
                }
            },
            [197] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте",
                        Aftertext = "Если же проверка реакции была неудачной, потеряйте 6 ЖИЗНЕЙ, от страшного удара сзади. Когда вы поднимитесь обратно на ноги, все уже будет кончено. Если же вы были быстры, словно ветер, то вы наверняка успеете… Сделать что?",

                        Damage = new Modification
                        {
                            Name = "Hitpoints",
                            Value = -6,
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 116, Text = "Подпрыгнуть" },
                    new Option { Destination = 111, Text = "Присесть" },
                    new Option { Destination = 170, Text = "Если проверка была неудачной" },
                }
            },
            [198] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 122, Text = "Промолчать" },
                    new Option { Destination = 235, Text = "Ответить ему обычным нерифмованным языком" },
                    new Option { Destination = 163, Text = "Рискнуть и все же попытаться сложить стихотворный ответ" },
                }
            },
            [199] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 150, Text = "Прямо на север, к нагромождению скал" },
                    new Option { Destination = 291, Text = "Левее на северо-запад, где ориентиром служат три одиноко стоящих чахлых деревца" },
                }
            },
            [200] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
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
                    new Option { Destination = 213, Text = "Далее" },
                }
            },
            [203] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 184, Text = "Вправо" },
                    new Option { Destination = 166, Text = "Влево" },
                }
            },
            [204] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 189, Text = "Атакуете" },
                    new Option { Destination = 247, Text = "Будете отступать" },
                    new Option { Destination = 279, Text = "ВЗОР" },
                }
            },
            [205] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 150, Text = "Правее, на северо-западе, обычное для этого пейзажа нагромождение скал" },
                    new Option { Destination = 291, Text = "Прямо по курсу все усеяно камнями поменьше, и, как ориентир для движения, стоят три одиноких деревца" },
                    new Option { Destination = 310, Text = "Левее возвышается скала тускло-рыжеватого оттенка" },
                }
            },
            [206] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 183, Text = "Вы рванетесь к гному, чтобы его обезоружить" },
                    new Option { Destination = 236, Text = "Останетесь на месте, полагая, что он блефует" },
                    new Option { Destination = 312, Text = "Разойдетесь с Коннери в стороны и двинетесь к коротышке по широкой дуге: один справа, другой слева, рассчитывая сбить ему прицел" },
                }
            },
            [207] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 287, Text = "Прямо по курсу" },
                    new Option { Destination = 252, Text = "Правее, к небольшому прозрачному озеру" },
                }
            },
            [208] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 145, Text = "Далее" },
                }
            },
            [209] = new Paragraph
            {
                Trigger = "Legs",

                Options = new List<Option>
                {
                    new Option { Destination = 108, Text = "Далее" },
                }
            },
            [210] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 296, Text = "Далее" },
                }
            },
            [211] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [212] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 268, Text = "Сбросите сапоги" },
                    new Option { Destination = 142, Text = "Останетесь в обуви" },
                }
            },
            [213] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 245, Text = "Если вы выбрали второй вариант" },
                    new Option { Destination = 335, Text = "Ваш выбор неверен" },
                }
            },
            [214] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 351, Text = "Прыгать в реку" },
                    new Option { Destination = 234, Text = "Бежать по тропе к повороту" },
                }
            },
            [215] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 315, Text = "Если ваша специализация маг" },
                    new Option { Destination = 383, Text = "Иначе" },
                }
            },
            [216] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 168, Text = "Если у вас записано слово дерн", OnlyIf = "Turf" },
                    new Option { Destination = 274, Text = "Далее" },
                }
            },
            [217] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 277, Text = "Если у вас записано слово пружинка", OnlyIf = "Spring" },
                    new Option { Destination = 174, Text = "Далее" },
                }
            },
            [218] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",
                        ReactionWounds = "1-3",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВЕЛИКАН",
                                Strength = 11,
                                Hitpoints = 10,
                            },
                        },

                        Aftertext = "Удары у великана страшные, но совсем уж медленные. Каждый раз, когда вы проигрываете раунд, реагируйте. Если успеваете, то вы теряете 1 ЖИЗНЬ. Не успеваете – теряете 3 ЖИЗНИ.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 139, Text = "Если бой затянется дольше 10 раундов" },
                    new Option { Destination = 248, Text = "Если вы справились раньше" },
                }
            },
            [219] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 239, Text = "Будете натягивать сапоги" },
                    new Option { Destination = 176, Text = "Броситесь босиком на выручку Коннери (меч уже в руке, и это главное)" },
                    new Option { Destination = 284, Text = "Окликните напарника" },
                }
            },
            [220] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [221] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 363, Text = "Зайдете внутрь" },
                    new Option { Destination = 254, Text = "Двинетесь дальше" },
                }
            },
            [222] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 187, Text = "Далее" },
                }
            },
            [223] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 259, Text = "Далее" },
                }
            },
            [224] = new Paragraph
            {
                Trigger = "Mercy",

                Options = new List<Option>
                {
                    new Option { Destination = 289, Text = "Далее" },
                }
            },
            [225] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",
                        GolemFight = true,

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "САБЛЕРУКИЙ ГОЛЕМ",
                                Strength = 11,
                                Hitpoints = 2,
                            },
                        },

                        Aftertext = "Вам надо выстоять 4 раунда, потом Коннери внезапно кричит:\n\n– Прикрой! – и вам надо успеть СРЕАГИРОВАТЬ, чтобы защитить своего компаньона. Если все проходит удачно, Коннери наносит голему повреждение. Если нет – отнимите у напарника 2 ЖИЗНИ. После продержитесь еще 4 раунда и повторите те же действия. Как только у Коннери получится дважды удачно повредить голема, бой заканчивается.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 288, Text = "Далее" },
                }
            },
            [226] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 270, Text = "Далее" },
                }
            },
            [227] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 323, Text = "Далее" },
                }
            },
            [228] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 308, Text = "Далее" },
                }
            },
            [229] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 386, Text = "К одинокой скале из слюды" },
                    new Option { Destination = 216, Text = "К скале, изогнутая аркой наподобие буквы Л" },
                    new Option { Destination = 375, Text = "К чахлой рощице из причудливо изогнутых деревьев" },
                }
            },
            [230] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Sell",
                        ButtonName = "Продать кинжал",
                        Price = 5,
                        Aftertext = "Согласитесь вы или откажетесь – решайте сами. Но в любом случае, ледок между вами сломан, и возобновления драки можно не опасаться. Попрощавшись, вы спускаетесь от кузни к дороге.\n\n...Позади слышны звуки смачных подзатыльников – кузнец наказывает своих подмастерьев, возомнивших себя крутыми боевиками.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 148, Text = "Далее" },
                }
            },
            [231] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 395, Text = "Свернете вслед за ручьем и направитесь к гейзерам" },
                    new Option { Destination = 373, Text = "Пойдете прямо на север, поднимаясь на поросшую кустарником возвышенность" },
                }
            },
            [232] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 320, Text = "Если у вас записано слово Огадар", OnlyIf = "Ogadir" },
                    new Option { Destination = 174, Text = "Будете блокировать удар" },
                    new Option { Destination = 134, Text = "Попытаетесь откатиться в сторону" },
                    new Option { Destination = 396, Text = "ЗАСЛОН" },
                }
            },
            [233] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 378, Text = "Прямо по курсу возвышаются высокие холмы" },
                    new Option { Destination = 252, Text = "Левее, в низине на северо-западе, виднеется небольшое прозрачное озеро" },
                }
            },
            [234] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 351, Text = "Будете возвращаться и прыгать" },
                    new Option { Destination = 329, Text = "Попробуете спуститься по берегу вдоль реки" },
                }
            },
            [235] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 253, Text = "Далее" },
                }
            },
            [236] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте",
                        Aftertext = "Замешкались – теряете 4 ЖИЗНИ.\n\nКоннери играючи отбил предназначенный ему болт. Гном мрачнеет, но поднимает с земли новые боеприпасы и деловито заряжает арбалет. Надо что-то предпринимать, не стоять же, как мишень.",

                        Damage = new Modification
                        {
                            Name = "Hitpoints",
                            Value = -4,
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 183, Text = "Двинетесь на гнома" },
                    new Option { Destination = 312, Text = "Разделитесь с напарником, чтобы зайти с разных сторон" },
                }
            },
            [237] = new Paragraph
            {
                Trigger = "Water",

                Options = new List<Option>
                {
                    new Option { Destination = 126, Text = "Далее" },
                }
            },
            [238] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 276, Text = "Уйти влево и рубануть по его крылу" },
                    new Option { Destination = 199, Text = "Уйти вправо, пропустить его мимо и помочь напарнику, пока кровожадная птица заходит на следующий вираж" },
                }
            },
            [239] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 176, Text = "Далее" },
                }
            },
            [240] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 362, Text = "Далее" },
                }
            },
            [241] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 180, Text = "Далее" },
                }
            },
            [242] = new Paragraph
            {
                Trigger = "EvilEye",

                Options = new List<Option>
                {
                    new Option { Destination = 307, Text = "Далее" },
                }
            },
            [243] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 191, Text = "Далее" },
                }
            },
            [244] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 275, Text = "Вытащить мост" },
                    new Option { Destination = 390, Text = "Броситься в реку" },
                    new Option { Destination = 374, Text = "Пойти дальше" },
                }
            },
            [245] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 307, Text = "Далее" },
                }
            },
            [246] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 388, Text = "Далее" },
                }
            },
            [247] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте в первый раз",

                        Damage = new Modification
                        {
                            Name = "Hitpoints",
                            Empty = true,
                        },
                    },
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте во второй раз",

                        Damage = new Modification
                        {
                            Name = "Hitpoints",
                            Empty = true,
                        },
                    },
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте в третий раз",

                        Damage = new Modification
                        {
                            Name = "Hitpoints",
                            Empty = true,
                        },

                        Aftertext = "Но даже при хорошем раскладе его холодные когти оставят на вас свои следы. Если ваша реакция оказалась в полном порядке, бросьте два кубика и отнимите выпавшее от своей ЖИЗНИ.",
                    },
                    new Actions
                    {
                        ActionName = "DiceWounds",
                        ButtonName = "Кинуть 2 кубика",
                        Dices = 2,
                        Aftertext = "Если вы еще живы, то вам удалось выбраться. На свет за вами вампир не идет, вы в безопасности.\n\nОтдышавшись, вернитесь на и выберите дальнейший маршрут.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 216, Text = "Далее" },
                }
            },
            [248] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 289, Text = "Далее" },
                }
            },
            [249] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 206, Text = "Вы вступите с ним в беседу" },
                    new Option { Destination = 331, Text = "Просканируете окрестности заклинанием ВЗОР" },
                }
            },
            [250] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 267, Text = "Если у вас записано слово паника", OnlyIf = "Panic" },
                    new Option { Destination = 299, Text = "Прямо по курсу равнина, покрытая длинной, выше колена, травой" },
                    new Option { Destination = 137, Text = "Правее, на северо-востоке, виднеются какие-то руины, истертые временем до абсолютной неузнаваемости" },
                    new Option { Destination = 398, Text = "К северо-западу же от вас стандартные скалы, правда, увитые разнообразной зеленью (все-таки здесь ощутимо теплее, чем на промозглом краю плато)" },
                }
            },
            [251] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 294, Text = "Если у вас записано слово милосердие", OnlyIf = "Mercy" },
                    new Option { Destination = 348, Text = "Далее" },
                }
            },
            [252] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 149, Text = "Если у вас записано слово слезы", OnlyIf = "Tears" },
                    new Option { Destination = 126, Text = "Далее" },
                }
            },
            [253] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 285, Text = "Левее видна одинокая возвышенность, густо поросшая кустарником" },
                    new Option { Destination = 350, Text = "Прямо по курсу же виднеется нагромождение застывших лавовых потоков – неуютное место, лишенное всяческой растительности" },
                }
            },
            [254] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",
                        ConneryAttacks = "2, 10",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВЕЛИКАН НА НОСОРОГЕ",
                                Strength = 13,
                                Hitpoints = 14,
                            },
                        },

                        Aftertext = "Это будет сложная схватка. Коннери одолеет своего противника за 10 раундов, и если ваш бой затянется дольше – придет вам на помощь – после 10 раунда ваш противник будет терять по 2 ЖИЗНИ за раунд. Победив, вы находите в переметных сумках поверженного великана 2 золотых и кусок малахита, после чего уходите вперед. Вскоре вы видите справа от себя ручей.",
                    },
                },

                Trigger = "PieceOfMalachite",

                Options = new List<Option>
                {
                    new Option { Destination = 108, Text = "Далее" },
                }
            },
            [255] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 424, Text = "Принести жертву второму идолу и послушать, что он скажет (если у вас конечно есть еще 10 золотых)" },
                    new Option { Destination = 388, Text = "Уйти" },
                }
            },
            [256] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -6,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 169, Text = "Далее" },
                }
            },
            [257] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryTrust",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 129, Text = "Далее" },
                }
            },
            [258] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте",

                        Damage = new Modification
                        {
                            Name = "Hitpoints",
                            Value = -4,
                        },

                        Aftertext = "Если ваши рефлексы подведут – теряете 4 ЖИЗНИ. Если все в порядке, то вам удастся увернуться.\n\nПроследив свой бросок, конунг делает движение бедрами, и ковер забирает влево. Едва не чиркнув по склону в крутом вираже, он начинает огибать гору. Маневренности этого колдовского транспортного средства позавидовал бы даже ястреб.\n\nВы поднимаете Коннери. Легендарный ведьмак совсем плох – лицо восковое, лоб усеивают крупные капли пота. Кажется, от падения он опять потерял сознание. До храма не более полусотни шагов, но вы уже различаете топот за своей спиной. Носороги спустились на равнину и набирают разбег. Насколько они близко, вы предпочитаете не смотреть.\n\n– Все будет пучком, старикан, – приговариваете вы, неся Коннери на спине. Ноги его бессильно волочатся по земле. – Ты только не вздумай умирать. Сегодня хреновый день для смерти, знаешь ли.\n\nВойна и орки, какой же он тяжелый! Раздобрел на тюремных харчах, старый пень. До храма остается полтора десятка шагов, и тут из его пасти, из самой ее сумеречной тени вам навстречу шагает великан. На нем накидка из шкур снежных барсов, в руке зажато длинное копье. Даже если бы сейчас случился Армагеддон, он и то был бы менее некстати, чем этот страж-копьеносец.\n\n– Да чтоб тебя, – вы бессильно останавливаетесь в тупом отчаянии. Потом, сбросив оцепенение, опускаете напарника на землю и беретесь за меч. Если дорога в храм лежит через твой труп, приятель – что ж, так тому и быть.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 298, Text = "Если у вас записано слово должок", OnlyIf = "Debt" },
                    new Option { Destination = 354, Text = "Если у вас записано слово учеба", OnlyIf = "Study" },
                    new Option { Destination = 123, Text = "Принять бой" },
                }
            },
            [259] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 237, Text = "Будете ли вы рассказывать о происшествии" },
                    new Option { Destination = 332, Text = "Что-нибудь наврете, чтобы не выглядеть дураком-фантазером" },
                }
            },
            [260] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "КОНДОР",
                                Strength = 11,
                                Hitpoints = 7,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 164, Text = "Далее" },
                }
            },
            [261] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 301, Text = "Броситесь наперерез, чтобы остановить носорога" },
                    new Option { Destination = 241, Text = "Прыгнете к Коннери, чтобы убрать его с дороги" },
                }
            },
            [262] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 195, Text = "Быстро осмотреть кости" },
                    new Option { Destination = 281, Text = "Обогнуть скалу, чтобы посмотреть, что же там оставляют магнитке великаны" },
                }
            },
            [263] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 229, Text = "Далее" },
                }
            },
            [264] = new Paragraph
            {
                Trigger = "FairyTale",

                Options = new List<Option>
                {
                    new Option { Destination = 229, Text = "Далее" },
                }
            },
            [265] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "СУККУБ",
                                Strength = 10,
                                Hitpoints = 12,
                            },
                        },

                        Aftertext = "После победы вы помогаете Коннери подняться. Тот морщится, ощупывая огромную шишку на голове (Коннери –4 ЖИЗНИ).\n\n– Что-то ты расслабился, – угрюмо говорите вы. – Как ты до своих преклонных лет дожил, если в такие примитивные засады попадаешься?\n\n– Мне показалось…\n\n– Что тебе показалось?\n\nКажется, что Коннери колеблется, хотя этот бегающий взгляд может быть просто следствием удара головой.",
                    },
                },

                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 322, Text = "Если ДОВЕРИЕ напарника равно 7 или больше", OnlyIf = "ДОВЕРИЕ >= 7" },
                    new Option { Destination = 231, Text = "Если ДОВЕРИЕ напарника меньше 7", OnlyIf = "ДОВЕРИЕ < 7" },
                }
            },
            [266] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 400, Text = "Далее" },
                }
            },
            [267] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 250, Text = "Далее" },
                }
            },
            [268] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -2,
                    },
                },

                Trigger = "Legs",

                Options = new List<Option>
                {
                    new Option { Destination = 205, Text = "Далее" },
                }
            },
            [269] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте",
                        Aftertext = "(не успеете – он заденет вас, потеряете 2 ЖИЗНИ) и оседает на землю.\n\nВы оборачиваетесь – за вашей спиной поднимается с земли великан. Вам повезло, что у него нет оружия, но и с голыми руками он опасный противник.",

                        Damage = new Modification
                        {
                            Name = "Hitpoints",
                            Value = -2,
                        },
                    },
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",
                        ConneryAttacks = "2, 5",
                        AttackWounds = 3,

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВЕЛИКАН",
                                Strength = 11,
                                Hitpoints = 12,
                            },
                        },

                        Aftertext = "Через 5 раундов распутавшийся Коннери приходит вам на помощь. После этого каждый раунд отнимайте у противника 2 ЖИЗНИ. После победы.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 180, Text = "Далее" },
                }
            },
            [270] = new Paragraph
            {
                Trigger = "Staff",

                Options = new List<Option>
                {
                    new Option { Destination = 226, Text = "Если у вас записано слово сглаз", OnlyIf = "EvilEye" },
                    new Option { Destination = 345, Text = "Вы все же двинетесь по ней" },
                    new Option { Destination = 387, Text = "Воспользовавшись тем, что у вас теперь есть посох, чтобы проверять дорогу, двинетесь по зыбкому торфу, забирая левее от источника жуткого рева" },
                }
            },
            [271] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 138, Text = "Попробуете поставить ему подножку, когда он уходит" },
                    new Option { Destination = 155, Text = "Пользуясь случаем, попытаетесь ногой подтянуть к себе один из его инструментов" },
                }
            },
            [272] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 370, Text = "Далее" },
                }
            },
            [273] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 383, Text = "«Просто проходили мимо»" },
                    new Option { Destination = 162, Text = "«Нам надо кое-что починить»" },
                }
            },
            [274] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 392, Text = "Поинтересуетесь, что именно им нужно" },
                    new Option { Destination = 364, Text = "Бегом рванетесь вперед" },
                }
            },
            [275] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 411, Text = "Далее" },
                }
            },
            [276] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 293, Text = "Успели" },
                    new Option { Destination = 260, Text = "Нет" },
                }
            },
            [277] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 394, Text = "Покинете руины" },
                    new Option { Destination = 341, Text = "Рискнете исследовать крипту" },
                }
            },
            [278] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 148, Text = "Далее" },
                }
            },
            [279] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 304, Text = "Если у вас записано слово высший", OnlyIf = "Higher" },
                    new Option { Destination = 377, Text = "Принять свой, скорее всего – последний, бой" },
                    new Option { Destination = 247, Text = "Попытаться выбежать из пещеры на свет" },
                }
            },
            [280] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 202, Text = "Вы можете попробовать решить ведьмин ребус самостоятельно" },
                    new Option { Destination = 335, Text = "Подождать, пока это сделает Коннери" },
                    new Option { Destination = 242, Text = "Бросить прутик, отказываясь играть в непонятные игры" },
                }
            },
            [281] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 207, Text = "Далее" },
                }
            },
            [282] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 243, Text = "«Ты верховный шаман горного клана?»" },
                    new Option { Destination = 303, Text = "«Почему ты сражался с тигром без оружия?»" },
                }
            },
            [283] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 185, Text = "Далее" },
                }
            },
            [284] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 190, Text = "Далее" },
                }
            },
            [285] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 385, Text = "Поднимитесь на холм" },
                    new Option { Destination = 158, Text = "Предпочтете его обогнуть" },
                }
            },
            [286] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 141, Text = "Не побоитесь замочить ноги и исследуете водопад" },
                    new Option { Destination = 370, Text = "Займетесь более насущными вещами" },
                }
            },
            [287] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 369, Text = "Попробуете настоять на визите в кузню" },
                    new Option { Destination = 133, Text = "Продолжите путь по дороге" },
                }
            },
            [288] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 249, Text = "Далее" },
                }
            },
            [289] = new Paragraph
            {
                Trigger = "PieceOfMeat",

                Options = new List<Option>
                {
                    new Option { Destination = 310, Text = "Прямо, к узкой вертикальной скале тускло-рыжеватого цвета" },
                    new Option { Destination = 291, Text = "Правее, на северо-восток, где посреди каменистой равнины сиротливо стоят три невысоких дерева" },
                }
            },
            [290] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [291] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 311, Text = "Подойти поближе, чтобы разглядеть, кто это" },
                    new Option { Destination = 349, Text = "Предпочтете дать крюк, чтобы не вступать в контакт с неведомым аборигеном" },
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
                Options = new List<Option>
                {
                    new Option { Destination = 164, Text = "Далее" },
                }
            },
            [294] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 348, Text = "Далее" },
                }
            },
            [295] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 261, Text = "Далее" },
                }
            },
            [296] = new Paragraph
            {
                Trigger = "Meeting",

                Options = new List<Option>
                {
                    new Option { Destination = 253, Text = "Далее" },
                }
            },
            [297] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 316, Text = "Рискнете и разведаете" },
                    new Option { Destination = 387, Text = "Предпочтете обойти холмы, сделав крюк по зыбкому болотистому торфянику" },
                }
            },
            [298] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 306, Text = "Далее" },
                }
            },
            [299] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 345, Text = "Двинетесь по тропе, решив, что местным животным виднее, куда идти" },
                    new Option { Destination = 270, Text = "Свернете к дереву, чтобы сначала выломать себе подходящий посох – незаменимую вещь во время променада по зыбкой местности" },
                }
            },
            [300] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 271, Text = "Далее" },
                }
            },
            [301] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 180, Text = "Далее" },
                }
            },
            [302] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -2,
                    },
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 207, Text = "Далее" },
                }
            },
            [303] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 191, Text = "Далее" },
                }
            },
            [304] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 216, Text = "Далее" },
                }
            },
            [305] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 250, Text = "Далее" },
                }
            },
            [306] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 176, Text = "Встретить смерть с мечом в руках, и примете бой над телом напарника" },
                    new Option { Destination = 266, Text = "Наплюете на арифметику и будете тащить Коннери к разверзнутой пасти, невзирая ни на что" },
                }
            },
            [307] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 252, Text = "Прямо, к небольшому прозрачному озерцу, расположенному в низине" },
                    new Option { Destination = 378, Text = "Правее, на северо-восток, к высоким холмам" },
                    new Option { Destination = 287, Text = "Коннери утверждает, что слышит доносящееся оттуда ритмичное постукивание" },
                }
            },
            [308] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 395, Text = "На северо-запад, к гейзерам" },
                    new Option { Destination = 221, Text = "Прямо на север, к башне, одиноко возвышающейся на пригорке посреди сочного зеленого луга" },
                }
            },
            [309] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 282, Text = "Если у вас записано слово стихи", OnlyIf = "Verse" },
                    new Option { Destination = 253, Text = "Если же нет, остается лишь развести руками – ничего подходящего вам на ходу не сочинить" },
                }
            },
            [310] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 346, Text = "«Прячемся за скалу!»" },
                    new Option { Destination = 330, Text = "«Идем ему навстречу!»" },
                }
            },
            [311] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 242, Text = "Предложите побыстрее уйти" },
                    new Option { Destination = 280, Text = "Подойти ближе" },
                }
            },
            [312] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте",
                        Aftertext = "Если промедлите, теряете 4 ЖИЗНИ.\n\nРуки у рыжебородого уже трясутся. Еще бы, с обеих сторон к нему неумолимо надвигаются двое людей в черном. Он судорожно развязывает свой мешок, путаясь в тесемках, и достает небольшой металлический шар.\n\n– Фас! – командует он шарику.\n\nТот внезапно раскладывается в маленького стального паучка. Ведьмачьим острым зрением вы отмечаете поблескивающие маслянистым коготки. Наверняка яд. Паучок очень прытко бросается к вам, но на полпути вдруг с шипением и треском останавливается. Внутри него что-то еле слышно жужжит. Затем он опять окукливается, трансформируясь в металлическую сферу.\n\n– Сломалась твоя игрушка, – ехидно замечает Коннери, подходя к гному. Тот пытается что-то возразить, но получает мощный подзатыльник и кубарем скатывается с ящиков на землю.\n\n– В другой раз, перед расстрелом мирных путников из арбалета, советую хорошенько подумать.\n\nВы проходите мимо свернувшегося в шар паучка. Неясно, опасна эта гадость еще или нет. Если хотите, можете взять его с собой. В таком случае поместите его в мешок и запишите слово сфера.\n\nКоннери в это время допрашивает гнома.\n\n– Расскажи нам, милый друг, что же ты забыл в столь суровом месте, и что у тебя в этих проклятых ящиках?\n\nКоротышка упрямо зыркает глазами поверх бороды и молчит.",

                        Damage = new Modification
                        {
                            Name = "Hitpoints",
                            Value = -4,
                        },
                    },
                },

                Trigger = "Sphere",

                Options = new List<Option>
                {
                    new Option { Destination = 283, Text = "Добавить гному затрещину от себя" },
                    new Option { Destination = 347, Text = "Самостоятельно открыть один из ящиков" },
                }
            },
            [313] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 261, Text = "Полоснуть его мечом по шее" },
                    new Option { Destination = 295, Text = "Вырубить, ударив ногой в основание черепа" },
                }
            },
            [314] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [315] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 278, Text = "Отдадите клинок" },
                    new Option { Destination = 383, Text = "Уйдёте" },
                }
            },
            [316] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 253, Text = "Тихо ретируетесь, пока им не до вас" },
                    new Option { Destination = 336, Text = "Рискнете посмотреть, что будет дальше" },
                }
            },
            [317] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 328, Text = "Сказать «Пожалуйста»" },
                    new Option { Destination = 262, Text = "Сказать «Два весла тебе в рот, гнида мелкопакостная»" },
                }
            },
            [318] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 297, Text = "Далее" },
                }
            },
            [319] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 342, Text = "Побежите вперед, надеясь оторваться" },
                    new Option { Destination = 181, Text = "Будете бросать в них камнями" },
                    new Option { Destination = 292, Text = "Примете бой" },
                }
            },
            [320] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 334, Text = "Далее" },
                }
            },
            [321] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 383, Text = "Если у вас нет таких денег" },
                    new Option { Destination = 367, Text = "Если же есть, то почему-то вы чувствуете, что лучше заплатить" },
                }
            },
            [322] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 231, Text = "Далее" },
                }
            },
            [323] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "МЕДВЕДЬ",
                                Strength = 11,
                                Hitpoints = 12,
                            },
                        },

                        Aftertext = "После победы вы осматриваете берлогу зверя, и в груде костей находите 2 золотых и статуэтку нимфы с обломанной головой. Теперь пора уходить.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 171, Text = "Далее" },
                }
            },
            [324] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 174, Text = "Далее" },
                }
            },
            [325] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 352, Text = "Далее" },
                }
            },
            [326] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 272, Text = "БРОНЯ" },
                    new Option { Destination = 344, Text = "будете падать как есть, сгруппировавшись и положившись на удачу" },
                }
            },
            [327] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 309, Text = "Если вы подобрали именно эти слова" },
                    new Option { Destination = 235, Text = "Если нет, то стих у вас получится откровенно кособокий" },
                }
            },
            [328] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 346, Text = "Далее" },
                }
            },
            [329] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 141, Text = "Если у вас записано слово записка", OnlyIf = "Note" },
                    new Option { Destination = 393, Text = "Решите понырять, чтобы проверить, не ушло ли тело Коннери на глубину (мало ли, вдруг треснулся головой, пока его несло течением)" },
                    new Option { Destination = 379, Text = "Решите форсировать реку, и проверить, нет ли следов напарника на другой стороне" },
                }
            },
            [330] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 313, Text = "Бросаетесь на выбитого из седла гиганта" },
                    new Option { Destination = 269, Text = "Бросаетесь на носорога" },
                }
            },
            [331] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 206, Text = "Далее" },
                }
            },
            [332] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryTrust",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 237, Text = "Далее" },
                }
            },
            [333] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 296, Text = "Далее" },
                }
            },
            [334] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 285, Text = "Пойдете прямо" },
                    new Option { Destination = 350, Text = "Свернете направо" },
                    new Option { Destination = 136, Text = "Направитесь левее" },
                }
            },
            [335] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -1,
                    },
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 242, Text = "Далее" },
                }
            },
            [336] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 177, Text = "Вмешаться в бой и помочь великану" },
                    new Option { Destination = 290, Text = "Вмешаться в бой и напасть на великана" },
                    new Option { Destination = 253, Text = "Тихо ретироваться, пока вас не заметили" },
                }
            },
            [337] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 300, Text = "Далее" },
                }
            },
            [338] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -1,
                    },
                },

                Trigger = "Legs",

                Options = new List<Option>
                {
                    new Option { Destination = 250, Text = "Далее" },
                }
            },
            [339] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 146, Text = "Будете рубить тварь, пока она не воспользовалась доверчивостью напарника" },
                    new Option { Destination = 265, Text = "Послушаете Коннери и удержитесь от агрессии" },
                }
            },
            [340] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 553, Text = "Если у вас есть кусок малахита, и вы не прочь рискнуть", OnlyIf = "PieceOfMalachite" },
                    new Option { Destination = 397, Text = "Если же нет, то остается лишь покинуть комнату" },
                }
            },
            [341] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 394, Text = "Далее" },
                }
            },
            [342] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте в первый раз",

                        Damage = new Modification
                        {
                            Name = "Hitpoints",
                            Value = -2,
                        },
                    },
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте во второй раз",

                        Damage = new Modification
                        {
                            Name = "Hitpoints",
                            Value = -2,
                        },
                    },
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте в третий раз",

                        Damage = new Modification
                        {
                            Name = "Hitpoints",
                            Value = -2,
                        },
                    },
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте в четвёртый раз",
                        Aftertext = "За каждый раз, когда вы промедлите, отнимите у себя 2 ЖИЗНИ.",

                        Damage = new Modification
                        {
                            Name = "Hitpoints",
                            Value = -2,
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 292, Text = "Если вы пропустите 3 и более укола, то вынуждены будете остановиться и принять бой" },
                    new Option { Destination = 129, Text = "Если же вам удалось оторваться" },
                }
            },
            [343] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",
                        OnlyRounds = 4,

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ТРОЛЛЬ",
                                Strength = 9,
                                Hitpoints = 10,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ ТРОЛЛЬ",
                                Strength = 9,
                                Hitpoints = 10,
                            },
                        },

                        Aftertext = "Через 4 раунда на шум во двор выходит великан-кузнец. Оценив обстановку, он поднимает руку и что-то гортанно кричит. В его руке тяжелый молот, и вообще, под его руководством тролли будут куда опаснее.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 175, Text = "Вы проскочите меж неуклюжих троллей и атакуете великана" },
                    new Option { Destination = 273, Text = "Будете этого делать, решив сначала разобраться со своими соперниками" },
                }
            },
            [344] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 370, Text = "Далее" },
                }
            },
            [345] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 203, Text = "Успели" },
                    new Option { Destination = 318, Text = "Нет" },
                }
            },
            [346] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 317, Text = "Если у вас записано слово болтовня", OnlyIf = "Babble" },
                    new Option { Destination = 302, Text = "Если ваша специализация – воин" },
                    new Option { Destination = 165, Text = "Если же все эти варианты вам не подходят" },
                }
            },
            [347] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 185, Text = "Далее" },
                }
            },
            [348] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 167, Text = "Если у вас записано слово встреча", OnlyIf = "Meeting" },
                    new Option { Destination = 337, Text = "Далее" },
                }
            },
            [349] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 307, Text = "Далее" },
                }
            },
            [350] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 186, Text = "Потратите четверть часа на это занятие" },
                    new Option { Destination = 375, Text = "Предпочтете двигаться на север, где виднеется чахлая рощица из слабых, причудливо изогнутых деревьев" },
                    new Option { Destination = 386, Text = "Заберете левее, к ярко блестящей на солнце скале" },
                }
            },
            [351] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "DiceWounds",
                        ButtonName = "Кинуть кубик",
                        Dices = 1,
                        DiceBonus = -2,
                        Aftertext = "Коннери нигде не видно, сверху пестрым горохом сыплются десятки леммингов. Вы отчаянно пытаетесь затормозить, но тщетно – за гладкие камни не ухватиться, а течение слишком стремительно.\n\nЗапишите слово пропажа. Если уже записано – оставьте все, как есть.\n\nНа северном берегу, чуть ниже по реке, виден просвет в утесах. Длинная узкая ложбина прорезает скалы ножом, доходя до самой воды. Там, откуда вы упали, вообще один сплошной обрыв, так что вариантов у вас немного.",
                    },
                },

                Trigger = "Missing",

                Options = new List<Option>
                {
                    new Option { Destination = 389, Text = "Попробуете славировать и выплыть к расселине на противоположном берегу (задачка сложная, но может выгореть)" },
                    new Option { Destination = 326, Text = "Отдадитесь на волю течения, прикроете руками голову и будете молиться, что река не размажет вас по какому-нибудь валуну, а вынесет в безопасное место" },
                }
            },
            [352] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 274, Text = "Прямо, на равнину" },
                    new Option { Destination = 188, Text = "Правее, на северо-восток" },
                }
            },
            [353] = new Paragraph
            {
                Trigger = "Basket",

                Options = new List<Option>
                {
                    new Option { Destination = 397, Text = "Далее" },
                }
            },
            [354] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 306, Text = "Далее" },
                }
            },
            [355] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте",
                        Aftertext = "При хорошем раскладе вам удается удержаться и переправиться на другой берег. При плохом — вы падаете вниз.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 393, Text = "Ушли под воду" },
                    new Option { Destination = 305, Text = "Очутились на другом берегу" },
                }
            },
            [356] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 257, Text = "Будете упорствовать, настаивая на своем" },
                    new Option { Destination = 319, Text = "Дадите ему возможность разогнать медуз громким криком" },
                }
            },
            [357] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 169, Text = "Далее" },
                }
            },
            [358] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 391, Text = "Вонзаете меч в широкую спину рептилии" },
                    new Option { Destination = 130, Text = "Продолжаете путь наверх, выбрав своей мишенью голову" },
                }
            },
            [359] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ГОРНЫЙ ВОЛК",
                                Strength = 12,
                                Hitpoints = 16,
                            },
                        },

                        Aftertext = "Победа достается вам нелегко. Коннери вообще забрызган кровью с головы до ног, к счастью, большая часть – не его (отнимите у напарника 4 ЖИЗНИ).\n\n– По преданиям великанов, конец света начнется с того, что гигантский волк проглотит луну, – тяжело дыша, говорит Коннери. – Глядя на этих тварей, я даже как-то начинаю в это верить.\n\nОн вкладывает меч в ножны и косится на вас:\n\n– Ты бы слушался меня хоть иногда, что ли. Глядишь, и ненужных драк можно было бы избежать.\n\nС тяжелым сердцем вы движетесь к выходу из ущелья, которое теперь напоминает бойню.",
                    },
                },

                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 308, Text = "Далее" },
                }
            },
            [360] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте",
                    },
                },

                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -3,
                    },
                },

                Trigger = "Fury",

                Options = new List<Option>
                {
                    new Option { Destination = 325, Text = "Все в порядке" },
                    new Option { Destination = 380, Text = "Нет, но еще есть крохотный шанс поставить ЗАСЛОН" },
                    new Option { Destination = 314, Text = "Заклинаний в запасе у вас нет, или вы из экономии не хотите этого делать" },
                }
            },
            [361] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 264, Text = "Сказка" },
                    new Option { Destination = 382, Text = "Совет" },
                }
            },
            [362] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 174, Text = "Поднять руки, сдаться и попытаться поговорить" },
                    new Option { Destination = 324, Text = "Обнажить клинок и продать свою жизнь как можно дороже" },
                }
            },
            [363] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 399, Text = "Попробовать подняться наверх и исследовать верхний ярус башни" },
                    new Option { Destination = 157, Text = "Предпочтете остаться на месте, развлекаясь разглядыванием трещин в стене" },
                }
            },
            [364] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -5,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 388, Text = "Далее" },
                }
            },
            [365] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",
                        ReactionRound = "1, Детки",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "КАМЫШОВЫЙ КОТ",
                                Strength = 10,
                                Hitpoints = 10,
                            },
                            new Character
                            {
                                Name = "КАМЫШОВАЯ КОШКА",
                                Strength = 10,
                                Hitpoints = 10,
                            },
                        },

                        Aftertext = "После победы мелочь быстро разбегается, вы же осторожно выглядываете, чтобы оценить обстановку. Великана и след простыл. То ли не заметил вас, то ли не рискнул разъезжать на носороге по болотистой почве. Выждав еще с пяток минут, вы выходите из укрытия и прикидываете, куда бы направиться дальше.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 334, Text = "Далее" },
                }
            },
            [366] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 147, Text = "Подползете ближе и попытаетесь ворваться в ворота вместе с группой из трех великанов" },
                    new Option { Destination = 362, Text = "Прогуляетесь вдоль стены вправо" },
                    new Option { Destination = 240, Text = "Прогуляетесь вдоль стены влево" },
                    new Option { Destination = 39, Text = "Будете ждать еще" },
                }
            },
            [367] = new Paragraph
            {
                Trigger = "Spring",

                Options = new List<Option>
                {
                    new Option { Destination = 148, Text = "Далее" },
                }
            },
            [368] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 154, Text = "Попытаетесь уйти в сторону, чтобы дать напарнику показать себя" },
                    new Option { Destination = 384, Text = "Не будете рисковать и встретите волка ударом меча" },
                }
            },
            [369] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 343, Text = "Нанесете визит кузнецу" },
                    new Option { Destination = 133, Text = "Пойдете дальше по дороге" },
                }
            },
            [370] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 286, Text = "Если у вас записано слово записка", OnlyIf = "Note" },
                    new Option { Destination = 393, Text = "Нырнуть, чтобы рассмотреть, нет ли на дне тела Коннери" },
                    new Option { Destination = 338, Text = "Выбраться на берег" },
                }
            },
            [371] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 386, Text = "На северо-восток по направлению к ярко блестящей на солнце скале" },
                    new Option { Destination = 216, Text = "На север, избрав ориентиром чудную скалу, изгибающуюся похожей на букву Л аркой" },
                }
            },
            [372] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 141, Text = "Сделаете это" },
                    new Option { Destination = 338, Text = "Решите, что вашим заледеневшим конечностям уже довольно испытаний и устремитесь на берег" },
                }
            },
            [373] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте",
                    },
                },

                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 193, Text = "Успели" },
                    new Option { Destination = 152, Text = "Нет" },
                }
            },
            [374] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = 2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 250, Text = "Далее" },
                }
            },
            [375] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 135, Text = "Если у вас есть кусок мяса, можете попробовать бросить его медведю", OnlyIf = "PieceOfMeat" },
                    new Option { Destination = 227, Text = "Также можно попробовать поднять руки, показывая что вы безоружны, и медленно отходить назад" },
                    new Option { Destination = 323, Text = "Вступить в схватку" },
                }
            },
            [376] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [377] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [378] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 356, Text = "Попробуете ему помешать" },
                    new Option { Destination = 319, Text = "Пусть кричит" },
                }
            },
            [379] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 338, Text = "Вплавь" },
                    new Option { Destination = 355, Text = "Рискнете пропрыгать по камням" },
                }
            },
            [380] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 352, Text = "Далее" },
                }
            },
            [381] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 358, Text = "Не сбавляя темп бега, пробежитесь по хвосту и запрыгнете ящеру на спину" },
                    new Option { Destination = 197, Text = "Рубанете его сзади по ноге (выше не достать)" },
                }
            },
            [382] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 229, Text = "Далее" },
                }
            },
            [383] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 148, Text = "Далее" },
                }
            },
            [384] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",
                        ConneryAttacks = "2",
                        RoundsToWin = 5,

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВОЛЧИЦА",
                                Strength = 11,
                                Hitpoints = 17,
                            },
                        },

                        Aftertext = "Если вы уложились за пять раундов, то успеваете убежать с поля боя до подхода подкрепления.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 308, Text = "Уложились за пять раундов" },
                    new Option { Destination = 359, Text = "Не уложились" },
                }
            },
            [385] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте",
                        Aftertext = "Атака была настолько внезапной, что полностью уйти от поражения у вас не получится при любом раскладе. Если ваши рефлексы были на высоте, то метательный нож лишь оцарапает вам плечо (-2 ЖИЗНИ). Если нет, то нож воткнется вам в руку (–5 ЖИЗНЕЙ).\n\nБуквально через секунду в вашу сторону устремляется еще один, точно такой же снаряд, но теперь вы наготове.",

                        Benefit = new Modification
                        {
                            Name = "Hitpoints",
                            Value = -2,
                        },
                        Damage = new Modification
                        {
                            Name = "Hitpoints",
                            Value = -5,
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 361, Text = "Если же ваша специализация – метатель ножей" },
                    new Option { Destination = 263, Text = "В противном случае вы просто отбиваете нож мечом и идете вперед, чтобы разобраться с нападающим" },
                }
            },
            [386] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 360, Text = "Если у вас записано слово слезы или водоем", OnlyIf = "Tears|Water" },
                    new Option { Destination = 352, Text = "Вы спокойно проходите мимо, скорчив отражению забавную рожицу" },
                }
            },
            [387] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 232, Text = "Продолжите движение вперед, понимая, что в таком случае бой неизбежен" },
                    new Option { Destination = 365, Text = "Броситесь в заросли высокой травы неподалеку, чтобы укрыться там" },
                }
            },
            [388] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 174, Text = "Двинетесь к воротам, решив испробовать старую как мир тактику – вперед, напролом, а там разберемся" },
                    new Option { Destination = 362, Text = "Пройдете вдоль стены вправо, чтобы поискать слабое место" },
                    new Option { Destination = 240, Text = "Пройдете влево с той же целью" },
                    new Option { Destination = 366, Text = "Присядете в теньке и понаблюдаете некоторое время" },
                }
            },
            [389] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -2,
                    },
                },

                Trigger = "Missing, Legs",

                Options = new List<Option>
                {
                    new Option { Destination = 250, Text = "Далее" },
                }
            },
            [390] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "DiceWounds",
                        ButtonName = "Кинуть кубик",
                        Dices = 1,
                        DiceBonus = -2,
                        Aftertext = "Коннери нигде не видно. Вы отчаянно пытаетесь затормозить, но тщетно – за гладкие камни не ухватиться, а течение слишком стремительно.\n\nНа северном берегу, чуть ниже по реке, виден просвет в утесах. Длинная узкая ложбина прорезает скалы ножом, доходя до самой воды. На южном, откуда вы шли, вообще один сплошной обрыв, так что вариантов у вас немного.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 389, Text = "Попробуете славировать и выплыть к расселине на противоположном берегу (задачка сложная, но может выгореть)" },
                    new Option { Destination = 326, Text = "Отдадитесь на волю течения, прикроете руками голову и будете молиться, что река не размажет вас по какому-нибудь валуну, а вынесет в безопасное место" },
                }
            },
            [391] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 107, Text = "Если ваша специализация воин, то ваша сила поможет вам справиться с этой непростой задачей" },
                    new Option { Destination = 197, Text = "Если же нет, то удержаться не выйдет, и при падении вы сильно повреждаете колено (–3 ЖИЗНИ)" },
                }
            },
            [392] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 255, Text = "Деньги правому идолу" },
                    new Option { Destination = 246, Text = "Деньги левому идолу" },
                    new Option { Destination = 364, Text = "Броситесь бежать" },
                }
            },
            [393] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 338, Text = "Сделать ли вам несколько гребков и побыстрее очутиться на противоположном берегу" },
                    new Option { Destination = 372, Text = "Нырнуть еще раз, чтобы рассмотреть блеснувшую вещь" },
                }
            },
            [394] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 345, Text = "Тропа должна означать надежный путь" },
                    new Option { Destination = 387, Text = "Прогуляться по равнине, выбрав ориентиром одинокий холм, возвышающийся левее по курсу" },
                }
            },
            [395] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 209, Text = "Перепрыгнете через ручей" },
                    new Option { Destination = 153, Text = "Заберетесь на камень" },
                }
            },
            [396] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 334, Text = "Далее" },
                }
            },
            [397] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 504, Text = "Далее" },
                }
            },
            [398] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 357, Text = "Если у вас есть принт" },
                    new Option { Destination = 376, Text = "Будете щипать себя" },
                    new Option { Destination = 256, Text = "Шарахнете себе же в ногу СГУСТКОМ" },
                }
            },
            [399] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 187, Text = "Успели" },
                    new Option { Destination = 222, Text = "Нет" },
                }
            },
            [400] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 549, Text = "Далее" },
                }
            },
            [401] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 429, Text = "ВЗОР" },
                    new Option { Destination = 444, Text = "СГУСТОК (актуально, если вы воин, а хочется, хоть на секунду, разогнать темноту)" },
                    new Option { Destination = 462, Text = "Рискнете двигаться наобум" },
                }
            },
            [402] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 432, Text = "Согласиться с «теорией радуги»" },
                    new Option { Destination = 454, Text = "Предложить свое решение" },
                    new Option { Destination = 536, Text = "ВЗОР" },
                }
            },
            [403] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 463, Text = "Далее" },
                }
            },
            [404] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 505, Text = "«Смуглая, невысокая, фигуристая, черноволосая»" },
                    new Option { Destination = 430, Text = "«Белое платье, среднего роста, бледно-голубые глаза, непослушная челка»" },
                    new Option { Destination = 494, Text = "«Ярко-синие глаза, короткое белое платье, черные волосы, серьга в левом ухе»" },
                }
            },
            [405] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте",
                        Aftertext = "Если реакция подведет вас, то атака джинна будет смертельной, и ваше приключение окончено…\n\nЕсли же ваши рефлексы на высоте, то вы несомненно успеете – но что именно?",

                        Damage = new Modification
                        {
                            Name = "Hitpoints",
                            Value = -30,
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 558, Text = "Встретить противника ударом меча" },
                    new Option { Destination = 594, Text = "Ударить его СГУСТКОМ" },
                    new Option { Destination = 415, Text = "Присесть на месте" },
                }
            },
            [406] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 448, Text = "Далее" },
                }
            },
            [407] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 476, Text = "Далее" },
                }
            },
            [408] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 550, Text = "Далее" },
                }
            },
            [409] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "MushroomsForConnery",
                        ButtonName = "Предложить гриб Коннери",
                        Aftertext = "Если ДОВЕРИЕ Коннери 6 или больше, то он хмыкнув, возьмет гриб – добавьте вам обоим по 3 ЖИЗНИ.\n\nЕсли нет, то он вежливо откажется его есть, и 3 ЖИЗНИ получите только вы.",
                    },
                },

                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = 3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 457, Text = "Далее" },
                }
            },
            [410] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 462, Text = "Далее" },
                }
            },
            [411] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 374, Text = "Далее" },
                }
            },
            [412] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [413] = new Paragraph
            {
                Trigger = "Sand",

                Options = new List<Option>
                {
                    new Option { Destination = 441, Text = "Далее" },
                }
            },
            [414] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 418, Text = "Далее" },
                }
            },
            [415] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 486, Text = "Далее" },
                }
            },
            [416] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте",
                        Aftertext = "В случае, если ваша скорость окажется недостаточной, теряете 4 ЖИЗНИ. Если же ваша реакция на высоте, то вы успеваете отпрянуть к левой стене как раз вовремя, чтобы остаться целым и невредимым.\n\nОднако, напарник ваш чуточку замешкался, и это стоит ему аж 5 ЖИЗНЕЙ. Рана на его плече выглядит крайне скверно. Дела идут все хуже и хуже, но вы находите в себе силы подбодрить ведьмака:\n\n– Держись, половина пройдена.",

                        Damage = new Modification
                        {
                            Name = "Hitpoints",
                            Value = -4,
                        },
                    },
                },

                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -5,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 472, Text = "Далее" },
                }
            },
            [417] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 469, Text = "Красная и оранжевая" },
                    new Option { Destination = 516, Text = "Оранжевая и синяя" },
                    new Option { Destination = 445, Text = "Красная и синяя" },
                }
            },
            [418] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте",
                        Aftertext = "Если с реакцией у вас окажется туго, то получи те удар по спине и потеряйте 2 ЖИЗНИ. Если же все хорошо, то вы молодец, успеваете отпрыгнуть.\n\nОтбросило вас или вы сами отпрыгнули, но повернувшись, вы понимаете одно – Коннери остался где-то по ту сторону. Стены в зале разгораются все ярче, все сильнее. Плиты заканчивают движение, свет пронизывает их насквозь, и вы с изумлением понимаете, что стоите в зеркальном лабиринте.\n\nСлева от вас плиты еще не разогрелись, не пропитались светом, они все еще матово-полупрозрачны. А вот справа и позади – самые натуральные зеркала. Десятки ваших отражений сбивают с толку, но где-то за ними слышна забористая ругань Коннери, и вы немного успокаиваетесь. Ничего c ним не случилось.\n\n– Эй, – окликаете вы напарника. – Я тут.",

                        Damage = new Modification
                        {
                            Name = "Hitpoints",
                            Value = -2,
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 529, Text = "Если у вас записано слово слезы, или водоем, или ярость", OnlyIf = "Tears|Water|Fury" },
                    new Option { Destination = 539, Text = "Если у вас записано слово нюансы", OnlyIf = "Nuances" },
                }
            },
            [419] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 457, Text = "Левое ответвление поблескивает антрацитовой чернотой стен, сквозь пол пробиваются бледные грибы, намекающие, что этим путем очень редко ходят" },
                    new Option { Destination = 524, Text = "В правом проходе базальтовые стены разрисованы примитивными фигурками, похоже на сцены из охоты" },
                }
            },
            [420] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 502, Text = "Вы позволите ему сделать это" },
                    new Option { Destination = 710, Text = "Быстро пройдете вперед, крича напарнику: «Стой! Смотри, я иду, и со мной ничего не происходит!»" },
                }
            },
            [421] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 472, Text = "Далее" },
                }
            },
            [422] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryTrust",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 401, Text = "Далее" },
                }
            },
            [423] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 534, Text = "Далее" },
                }
            },
            [424] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = 4,
                    },
                },

                Trigger = "Idols",

                Options = new List<Option>
                {
                    new Option { Destination = 388, Text = "Далее" },
                }
            },
            [425] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [426] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 440, Text = "Далее" },
                }
            },
            [427] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 706, Text = "Пойдете по коридору вправо, по направлению к изящной кирпичной арке" },
                    new Option { Destination = 504, Text = "Пойдете по коридору влево" },
                    new Option { Destination = 196, Text = "В дверь из обожженной глины" },
                    new Option { Destination = 458, Text = "В дверь из дерева, изящно украшенного резьбой" },
                    new Option { Destination = 340, Text = "В дверь из черного камня с малахитовыми вкраплениями" },
                }
            },
            [428] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 443, Text = "Рискнете пройти сквозь барьер, раздевшись и вывалявшись в песке" },
                    new Option { Destination = 599, Text = "Перейдете к плану с муравейником" },
                }
            },
            [429] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 451, Text = "Если ваша специализация – магия" },
                    new Option { Destination = 410, Text = "Далее" },
                }
            },
            [430] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryTrust",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 527, Text = "Далее" },
                }
            },
            [431] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 419, Text = "Броситься бегом к спасительной двери, благо она уже неподалеку" },
                    new Option { Destination = 475, Text = "Преодолеть оставшееся расстояние двумя гигантскими прыжками" },
                }
            },
            [432] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 459, Text = "Ваш выбор пал на самого себя" },
                    new Option { Destination = 440, Text = "Ваш выбор пал на напарника" },
                }
            },
            [433] = new Paragraph
            {
                Trigger = "Gift",

                Options = new List<Option>
                {
                    new Option { Destination = 467, Text = "Далее" },
                }
            },
            [434] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 484, Text = "Далее" },
                }
            },
            [435] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = 3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 470, Text = "Далее" },
                }
            },
            [436] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 570, Text = "Продолжать бежать на жреца, надеясь опередить его и нанести удар до того, как он совершит задуманное" },
                    new Option { Destination = 541, Text = "Резко отпрыгнуть в сторону" },
                    new Option { Destination = 593, Text = "СГУСТОК" },
                    new Option { Destination = 557, Text = "Если ваша специализация метатель" },
                }
            },
            [437] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 425, Text = "Преодолеть оставшийся отрезок тем же способом" },
                    new Option { Destination = 490, Text = "Выпрямиться в полный рост" },
                }
            },
            [438] = new Paragraph
            {
                RemoveTrigger = "Basket",

                Options = new List<Option>
                {
                    new Option { Destination = 477, Text = "Далее" },
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
                    new Option { Destination = 406, Text = "Прыгнуть на красную плиту, чтобы попытаться ее уравновесить" },
                    new Option { Destination = 417, Text = "Прыгнуть на желтую плиту" },
                    new Option { Destination = 469, Text = "Прыгнуть на оранжевую плиту" },
                }
            },
            [441] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 590, Text = "Потрогать барьер рукой" },
                    new Option { Destination = 413, Text = "Бросить сквозь него горсть песка" },
                    new Option { Destination = 464, Text = "Бросить какую-нибудь вещь из вашего рюкзака, если у вас есть подозрения, что именно этот предмет может помочь вам миновать барьер" },
                    new Option { Destination = 713, Text = "Бросить золотую монету" },
                    new Option { Destination = 493, Text = "Попробуйте предпринять что-то еще" },
                }
            },
            [442] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "ShareFood",
                        ButtonName = "Нужнее мне",
                        FoodSharing = Actions.FoodSharingType.KeepMyself,
                    },
                    new Actions
                    {
                        ActionName = "ShareFood",
                        ButtonName = "Нужнее Коннери",
                        FoodSharing = Actions.FoodSharingType.ToHim,
                    },
                    new Actions
                    {
                        ActionName = "ShareFood",
                        ButtonName = "Поделить поровну",
                        FoodSharing = Actions.FoodSharingType.FiftyFifty,

                        Aftertext = "Всю провизию вы приканчиваете буквально за пару минут.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 477, Text = "Далее" },
                }
            },
            [443] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 544, Text = "Далее" },
                }
            },
            [444] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 410, Text = "Далее" },
                }
            },
            [445] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 471, Text = "Желтая и красная" },
                    new Option { Destination = 516, Text = "Красная и синяя" },
                    new Option { Destination = 469, Text = "Желтая и синяя" },
                }
            },
            [446] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 527, Text = "Далее" },
                }
            },
            [447] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 439, Text = "Продолжать движение по правой стене" },
                    new Option { Destination = 421, Text = "Перейдете к левой стене" },
                    new Option { Destination = 416, Text = "Двинетесь по центру коридора, повернувшись боком, чтобы стать как можно более плоским" },
                }
            },
            [448] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [449] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 688, Text = "Далее" },
                }
            },
            [450] = new Paragraph
            {
                Trigger = "Delay",

                Options = new List<Option>
                {
                    new Option { Destination = 535, Text = "Далее" },
                }
            },
            [451] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 410, Text = "Далее" },
                }
            },
            [452] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 408, Text = "Успели" },
                    new Option { Destination = 517, Text = "Нет" },
                }
            },
            [453] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 537, Text = "Далее" },
                }
            },
            [454] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 489, Text = "По отдельности и каждый наступает на свою плиту" },
                    new Option { Destination = 426, Text = "Оба наступаете на одну" },
                }
            },
            [455] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 703, Text = "Если у вас записано слово такт", OnlyIf = "Tact" },
                    new Option { Destination = 513, Text = "Испытать скорость своей реакции и попытаться пройти зал манекенов" },
                }
            },
            [456] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 407, Text = "Далее" },
                }
            },
            [457] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 409, Text = "Если ваша специализация маг" },
                    new Option { Destination = 524, Text = "Вы будете настаивать на своем – в этом случае вы возвращаетесь и следуете другим путем" },
                    new Option { Destination = 506, Text = "Согласитесь идти дальше" },
                }
            },
            [458] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 353, Text = "У вас есть медный ключ" },
                    new Option { Destination = 427, Text = "Возвращайтесь" },
                }
            },
            [459] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [460] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 509, Text = "Далее" },
                }
            },
            [461] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте в первый раз",
                    },
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте во второй раз",
                    },
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте в третий раз",
                        Aftertext = "Если все прошло успешно, бросьте кубик и отнимите выпавшее от своей ЖИЗНИ. Если нет – то бросить придется три кубика и после произвести точно такое же вычитание.",
                    },
                    new Actions
                    {
                        ActionName = "DiceWounds",
                        ButtonName = "Кинуть 1 кубик",
                        Dices = 1,
                        Aftertext = "или",
                    },
                    new Actions
                    {
                        ActionName = "DiceWounds",
                        ButtonName = "Кинуть 3 кубика",
                        Dices = 3,
                        Aftertext = "Коннери также не сумел избежать града сталактитов – отнимите у него 4 ЖИЗНИ.\n\nЕсли вы еще живы, скорее и как можно бесшумнее покиньте зал-ловушку.",
                    },
                },

                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 583, Text = "Далее" },
                }
            },
            [462] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 487, Text = "Вдоль правой стены" },
                    new Option { Destination = 439, Text = "Вдоль левой стены" },
                    new Option { Destination = 496, Text = "По центру коридора" },
                }
            },
            [463] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 528, Text = "Успели" },
                    new Option { Destination = 431, Text = "Нет" },
                }
            },
            [464] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 441, Text = "Далее" },
                }
            },
            [465] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 575, Text = "Вы не будете сбавлять скорость и рискнете пробежать по пустому месту" },
                    new Option { Destination = 589, Text = "Затормозите и подождете Коннери" },
                }
            },
            [466] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 414, Text = "Далее" },
                }
            },
            [467] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 486, Text = "Далее" },
                }
            },
            [468] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -5,
                    },
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 447, Text = "Далее" },
                }
            },
            [469] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 448, Text = "Далее" },
                }
            },
            [470] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 435, Text = "Если у вас записано слово схрон", OnlyIf = "Cache" },
                    new Option { Destination = 712, Text = "Попробуете взобраться по стене к потолку и проверить, как обстоят дела там" },
                    new Option { Destination = 493, Text = "Попробуете что-то еще" },
                }
            },
            [471] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 422, Text = "синюю и желтую" },
                    new Option { Destination = 469, Text = "синюю и фиолетовую" },
                    new Option { Destination = 516, Text = "желтую и фиолетовую" },
                }
            },
            [472] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 485, Text = "«Идем вдоль правой стены»" },
                    new Option { Destination = 439, Text = "«Идем вдоль левой стены»" },
                    new Option { Destination = 403, Text = "«Идем по центру, повернувшись боком»" },
                    new Option { Destination = 437, Text = "«Садимся и идем на корточках, ни в коем случае не поднимая высоко голову»" },
                }
            },
            [473] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 535, Text = "Далее" },
                }
            },
            [474] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 434, Text = "Сделаете это" },
                    new Option { Destination = 484, Text = "С сожалением повесите его обратно на стену" },
                }
            },
            [475] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [476] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 404, Text = "Рассказываете все, как было" },
                    new Option { Destination = 446, Text = "Говорите, что вам очень жаль, оставляя историю о призраке при себе" },
                }
            },
            [477] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 442, Text = "Если у вас записано слово корзинка", OnlyIf = "Basket" },
                    new Option { Destination = 600, Text = "Далее" },
                }
            },
            [478] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = 0,
                        WizardWoundsPenalty = -2,
                    },
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 481, Text = "Далее" },
                }
            },
            [479] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 447, Text = "Далее" },
                }
            },
            [480] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",
                        ZombieFight = true,

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "КОРОЛЬ-ЗОМБИ",
                                Strength = 12,
                                Hitpoints = 8,
                            },
                        },

                        Aftertext = "После вашей победы Коннери, внимательно следивший за боем, замечает:\n\n– Знаешь, если бы у него было нормальное оружие, а не этот дурацкий скипетр… – напарник не договаривает начатую фразу, потому что груда костей и полусгнивших сухожилий начинает шевелиться.\n\n– Ходу, – бросаете вы, устремляясь к двери. Если есть желание, можете прихватить с черепа неумирающего стража белую корону .\n\nУ самого порога Коннери вдруг оборачивается и говорит с непонятной интонацией:\n\n– Что же такого надо было сотворить в далеком прошлом, чтобы тебя вот так вот прокляли?\n\n– Потом поразмыслишь, – вы чуть ли не силой выталкиваете напарника в дверь. За вашими спинами, шурша истлевшими одеяниями, восстает с пола проклятый когда-то король, чтобы вновь занять свой пост.",
                    },
                },


                Options = new List<Option>
                {
                    new Option { Destination = 418, Text = "Далее" },
                }
            },
            [481] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 418, Text = "Далее" },
                }
            },
            [482] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 503, Text = "Если у вас записано слово песок", OnlyIf = "Sand" },
                    new Option { Destination = 493, Text = "Решите, чем бы еще вам заняться" },
                }
            },
            [483] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 461, Text = "Вы ответите напарнику: «Хорошо»" },
                    new Option { Destination = 576, Text = "Молча двинетесь за ним" },
                    new Option { Destination = 507, Text = "ВЗОР" },
                }
            },
            [484] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 535, Text = "Бросаетесь вперед на троллей" },
                    new Option { Destination = 450, Text = "Оборачиваетесь, чтобы увидеть грозящую сзади опасность" },
                    new Option { Destination = 711, Text = "Делаете шаг вправо к стойкам с оружием" },
                    new Option { Destination = 473, Text = "Делаете шаг влево к стойкам с оружием" },
                }
            },
            [485] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 439, Text = "Далее" },
                }
            },
            [486] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 402, Text = "Далее" },
                }
            },
            [487] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 447, Text = "Далее" },
                }
            },
            [488] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -1,
                        WizardWoundsPenalty = -2,
                    },
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 418, Text = "Далее" },
                }
            },
            [489] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 498, Text = "Желтая и красная" },
                    new Option { Destination = 516, Text = "Желтая и оранжевая" },
                    new Option { Destination = 469, Text = "Оранжевая и красная" },
                }
            },
            [490] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 463, Text = "Далее" },
                }
            },
            [491] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 549, Text = "Далее" },
                }
            },
            [492] = new Paragraph
            {
                Trigger = "Gift",

                Options = new List<Option>
                {
                    new Option { Destination = 467, Text = "Далее" },
                }
            },
            [493] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 547, Text = "«Попробуем сделать подкоп»" },
                    new Option { Destination = 514, Text = "«Изучим песчаные замки, что стоят по углам»" },
                    new Option { Destination = 470, Text = "«Изучим стены комнаты, особенно в месте примыкания барьера»" },
                    new Option { Destination = 441, Text = "«Еще поэкспериментируем с проницаемостью барьера»" },
                }
            },
            [494] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 527, Text = "Далее" },
                }
            },
            [495] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 500, Text = "Далее" },
                }
            },
            [496] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 479, Text = "Успели" },
                    new Option { Destination = 468, Text = "Нет" },
                }
            },
            [497] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 488, Text = "Будем крутить ворот" },
                    new Option { Destination = 530, Text = "Перейдем ручей вброд" },
                }
            },
            [498] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 516, Text = "Красная и оранжевая" },
                    new Option { Destination = 469, Text = "Оранжевая и синяя" },
                    new Option { Destination = 445, Text = "Красная и синяя" },
                }
            },
            [499] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 564, Text = "Ответите ему" },
                    new Option { Destination = 515, Text = "Свернете за поворот и будете бежать, пока хватит сил" },
                    new Option { Destination = 405, Text = "Останетесь в своем коридоре и примете бой" },
                    new Option { Destination = 586, Text = "Будете разбивать зеркала" },
                }
            },
            [500] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 456, Text = "Если у вас есть шахтерская рукавица и вода во фляге" },
                    new Option { Destination = 548, Text = "Далее" },
                }
            },
            [501] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 466, Text = "Вы сделаете это" },
                    new Option { Destination = 414, Text = "Будете, скрепя зубы, наблюдать за дальнейшим ходом поединка" },
                }
            },
            [502] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 538, Text = "Если ваша специализация метатель" },
                    new Option { Destination = 532, Text = "СГУСТОК" },
                    new Option { Destination = 478, Text = "Броситься обратно, стараясь ступать по своим следам" },
                }
            },
            [503] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -1,
                    },
                },

                Trigger = "Guess",

                Options = new List<Option>
                {
                    new Option { Destination = 599, Text = "Хотите рискнуть и побарахтаться в муравейнике" },
                    new Option { Destination = 493, Text = "Поразмыслите, что еще можно предпринять." },
                }
            },
            [504] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 477, Text = "Будете настаивать на своем" },
                    new Option { Destination = 438, Text = "Послушаете напарника и двинетесь к двери" },
                }
            },
            [505] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryTrust",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 527, Text = "Далее" },
                }
            },
            [506] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 704, Text = "Правый, из которого слышен шум воды" },
                    new Option { Destination = 455, Text = "Ничем не примечательный левый" },
                }
            },
            [507] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 545, Text = "Зажмуритесь" },
                    new Option { Destination = 576, Text = "Шагнете к Коннери, чтобы придержать его" },
                }
            },
            [508] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [509] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 565, Text = "Попробуете протиснуться наружу" },
                    new Option { Destination = 591, Text = "Последуете за Коннери" },
                }
            },
            [510] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 577, Text = "Если вы сегодня убили в бою хоть кого-нибудь, любое существо" },
                    new Option { Destination = 537, Text = "Если нет – то ничего не произошло" },
                }
            },
            [511] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 560, Text = "Обогнете его" },
                    new Option { Destination = 580, Text = "Накроете его одеялом (если оно у вас есть)" },
                    new Option { Destination = 522, Text = "Пробегая мимо, хорошенько наподдадите ему пяткой, чтобы он улетел назад" },
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
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте в первый раз",

                        Damage = new Modification
                        {
                            Name = "Hitpoints",
                            Value = -3,
                        },
                    },
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте во второй раз",

                        Damage = new Modification
                        {
                            Name = "Hitpoints",
                            Value = -3,
                        },
                    },
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте в третий раз",

                        Damage = new Modification
                        {
                            Name = "Hitpoints",
                            Value = -3,
                        },

                        Aftertext = "Коннери проходит сквозь град ударов достаточно уверенно, лишь в самом конце чувствительно получает по крестцу (Коннери теряет 2 ЖИЗНИ). Потирая ушибленное место, напарник говорит:\n\n– Что-то я не понимаю причину твоего веселья.\n\n– Ты чего, всегда смешно, когда ближнему прилетает по заднице.\n\n– Знаешь что, умник… Попробуй-ка повторить за мной… – и он скороговоркой выпаливает, – В недрах тундры выдры в гетрах тырят в ведрах ядра кедра.\n\nВы честно пытаетесь. Два раза. После смеетесь уже вдвоем.\n\n-Тыдры… ярда… – Коннери от смеха даже опирается на стену. Тысяча орков, ваш напарник нравится вам все больше и больше.\n\nОтсмеявшись свое, вы идете дальше.",
                    },
                },

                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 537, Text = "Далее" },
                }
            },
            [514] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 533, Text = "Вы согласитесь, и разворошите замок острием меча" },
                    new Option { Destination = 482, Text = "Будете копать руками" },
                    new Option { Destination = 493, Text = "Предпочтете сделать другой выбор" },
                }
            },
            [515] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -6,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 564, Text = "Вы откликнетесь на зов Коннери" },
                    new Option { Destination = 405, Text = "Отступите в «свой» коридор, чтобы хотя бы спина пока была прикрыта" },
                    new Option { Destination = 586, Text = "Будете разбивать зеркала" },
                }
            },
            [516] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 448, Text = "Далее" },
                }
            },
            [517] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -8,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 550, Text = "Далее" },
                }
            },
            [518] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 551, Text = "Рискнете спрыгнуть на пол и быстро пробежаться назад" },
                    new Option { Destination = 574, Text = "Не будете рисковать и преодолеете весь путь, раскорячившись между стен" },
                }
            },
            [519] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 460, Text = "Целиком вложиться в один встречный удар" },
                    new Option { Destination = 705, Text = "Попытаться уйти в сторону" },
                }
            },
            [520] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 523, Text = "Далее" },
                }
            },
            [521] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте",

                        Benefit = new Modification
                        {
                            Name = "Hitpoints",
                            Value = -2,
                        },
                        Damage = new Modification
                        {
                            Name = "Hitpoints",
                            Value = -30,
                        },

                        Aftertext = "Если проверка неудачна, то вы останетесь под решеткой навсегда, пришпиленный к полу, будто жук булавками…\n\nЕсли все прошло хорошо, значит, вы успели прокатиться под падающей решеткой. Оцарапанная рука (–2 ЖИЗНИ) не в счет.\n\nКоннери помогает вам подняться, и вы вместе уходите вперед по коридору, который приводит вас к золотистой двери.",
                    },
                },

                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 493, Text = "Далее" },
                }
            },
            [522] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 465, Text = "Далее" },
                }
            },
            [523] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 474, Text = "Подойдете к отделу с оружием гномов" },
                    new Option { Destination = 542, Text = "Полюбуетесь на эльфийские клинки" },
                    new Option { Destination = 484, Text = "Не будет ничего не трогать и побыстрее покинуть комнату" },
                }
            },
            [524] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 708, Text = "Справа от колонны" },
                    new Option { Destination = 596, Text = "Слева от колонны" },
                }
            },
            [525] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -1,
                    },
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 428, Text = "Если у вас записано слово догадка", OnlyIf = "Guess" },
                    new Option { Destination = 493, Text = "Попробовать что-то другое" },
                }
            },
            [526] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 480, Text = "Предложите себя на роль поединщика" },
                    new Option { Destination = 501, Text = "Уступите напарнику честь убрать преграду с вашего пути" },
                    new Option { Destination = 414, Text = "Решите, что проще всего подгнившего короля будет победить вдвоем" },
                }
            },
            [527] = new Paragraph
            {
                Trigger = "PieceOfMalachite",

                Options = new List<Option>
                {
                    new Option { Destination = 540, Text = "Далее" },
                }
            },
            [528] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 419, Text = "Сделать стремительный бросок, чтобы пробежать оставшееся до спасительной двери расстояние" },
                    new Option { Destination = 475, Text = "Преодолеть его двумя гигантскими прыжками, стараясь как можно меньше касаться пола" },
                }
            },
            [529] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте в первый раз",

                        Damage = new Modification
                        {
                            Name = "Hitpoints",
                            Value = -3,
                        },
                    },
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте в второй раз",

                        Damage = new Modification
                        {
                            Name = "Hitpoints",
                            Value = -3,
                        },

                        Aftertext = "За каждую неудачу теряете 3 ЖИЗНИ.",
                    },
                },

                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 499, Text = "Далее" },
                }
            },
            [530] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [531] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [532] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 481, Text = "Далее" },
                }
            },
            [533] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 493, Text = "Далее" },
                }
            },
            [534] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 495, Text = "Быммуным – меч, Берилионидин – цепь, Браккус – череп" },
                    new Option { Destination = 508, Text = "Быммуным – череп, Берилионидин – меч, Браккус – цепь" },
                    new Option { Destination = 572, Text = "Как-то иначе" },
                }
            },
            [535] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 452, Text = "Вы справились за 6 раундов" },
                    new Option { Destination = 512, Text = "Не успели" },
                }
            },
            [536] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 402, Text = "Далее" },
                }
            },
            [537] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 561, Text = "Если у вас записано слово сказка", OnlyIf = "FairyTale" },
                    new Option { Destination = 566, Text = "Из правого коридора доносится звук какого-то работающего механизма" },
                    new Option { Destination = 483, Text = "Левый же подозрительно тих и абсолютно не освещен" },
                }
            },
            [538] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 481, Text = "Далее" },
                }
            },
            [539] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 433, Text = "Подойдете ближе" },
                    new Option { Destination = 492, Text = "Начнете медленно, осторожно пятиться назад" },
                }
            },
            [540] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 523, Text = "Левую дверь" },
                    new Option { Destination = 555, Text = "Правую дверь" },
                    new Option { Destination = 588, Text = "Коридор" },
                }
            },
            [541] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -3,
                    },
                },

                Trigger = "Five",

                Options = new List<Option>
                {
                    new Option { Destination = 595, Text = "Далее" },
                }
            },
            [542] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 484, Text = "Далее" },
                }
            },
            [543] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = 3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 569, Text = "Далее" },
                }
            },
            [544] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 423, Text = "Далее" },
                }
            },
            [545] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 493, Text = "Далее" },
                }
            },
            [546] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 506, Text = "Далее" },
                }
            },
            [547] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 525, Text = "Будете продолжать" },
                    new Option { Destination = 493, Text = "Бросите свою затею" },
                }
            },
            [548] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 407, Text = "Далее" },
                }
            },
            [549] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 491, Text = "Если у вас записано слово пропажа", OnlyIf = "Missing" },
                    new Option { Destination = 497, Text = "Прямо" },
                    new Option { Destination = 526, Text = "Направо" },
                    new Option { Destination = 420, Text = "Налево" },
                }
            },
            [550] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 511, Text = "Далее" },
                }
            },
            [551] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 574, Text = "Если его уровень его ДОВЕРИЯ меньше 6", OnlyIf = "ДОВЕРИЕ < 6" },

                    new Option {
                        Destination = 540,
                        Text = "Войдите в понравившуюся дверь",
                        OnlyIf = "ДОВЕРИЕ >= 6",

                        Do = new Modification
                        {
                            Name = "ConneryTrust",
                            Value = 1,
                        },
                    },
                }
            },
            [552] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 581, Text = "Если у вас записано слово шарик", OnlyIf = "Ball" },
                    new Option { Destination = 598, Text = "Рванете как ветер, чтобы быстрее миновать опасное место" },
                    new Option { Destination = 559, Text = "Пройдете вдоль левой стены, прижавшись к ней спиной, чтобы уменьшить зону поражения" },
                }
            },
            [553] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 397, Text = "Далее" },
                }
            },
            [554] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [555] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 592, Text = "Если у вас записано слово оберег", OnlyIf = "Amulet" },
                    new Option { Destination = 569, Text = "Далее" },
                }
            },
            [556] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 493, Text = "Далее" },
                }
            },
            [557] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 595, Text = "Далее" },
                }
            },
            [558] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 597, Text = "Далее" },
                }
            },
            [559] = new Paragraph
            {
                Trigger = "Three",

                Options = new List<Option>
                {
                    new Option { Destination = 573, Text = "Далее" },
                }
            },
            [560] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -2,
                    },
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 465, Text = "Далее" },
                }
            },
            [561] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 510, Text = "Если у вас есть 8 золотых, можете попробовать проверить, так ли это" },
                    new Option { Destination = 537, Text = "Если нет, то выбирайте дальнейший маршрут" },
                }
            },
            [562] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [563] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 436, Text = "Далее" },
                }
            },
            [564] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 515, Text = "Свернете за поворот и будете бежать, пока хватит сил" },
                    new Option { Destination = 405, Text = "Останетесь в своем коридоре и примете бой" },
                    new Option { Destination = 586, Text = "Будете разбивать зеркала" },
                }
            },
            [565] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [566] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 556, Text = "Вы согласитесь" },
                    new Option { Destination = 521, Text = "Будете настаивать на том, что на весах должны стоять вы, как более молодой и резвый" },
                    new Option { Destination = 585, Text = "Попробуете отяготить весы чем-то еще" },
                }
            },
            [567] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [568] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Init = true,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 607, Text = "Далее" },
                }
            },
            [569] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 584, Text = "Если ваша специализация – маг, то ваш ответ: «Да»" },
                    new Option { Destination = 520, Text = "Иначе вы вынуждены ответить «Нет»" },
                }
            },
            [570] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -6,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 595, Text = "Далее" },
                }
            },
            [571] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 397, Text = "Далее" },
                }
            },
            [572] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",
                        IncrementWounds = true,

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ЛАНТАН",
                                Strength = 13,
                                Hitpoints = 16,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ ЛАНТАН",
                                Strength = 13,
                                Hitpoints = 16,
                            },
                        },

                        Aftertext = "Покончив с ними, вы смотрите на Коннери. Это была очень трудная схватка для вас обоих. Напарник стоит над тремя мертвыми тварями и покачивается от усталости и ран (Коннери теряет 6 ЖИЗНЕЙ).",
                    },
                },

                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -6,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 500, Text = "Далее" },
                }
            },
            [573] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 587, Text = "Вы сбавите ход и дадите ей упасть прямо перед собой" },
                    new Option { Destination = 563, Text = "Не сбавляя скорости, прыгнете вперед, сделаете кувырок и прокатитесь под падающей балкой" },
                }
            },
            [574] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 540, Text = "Далее" },
                }
            },
            [575] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте",
                        Aftertext = "Если у вас вышла заминка, то она окажется смертельной, и вы упадете в бездонный провал…\n\nЕсли все в порядке, то вам чудом удастся сохранить равновесие.\n\n– Вниз не смотри! – кричит сзади Коннери. Вы слушаетесь и без труда преодолеваете провал.",

                        Damage = new Modification
                        {
                            Name = "Hitpoints",
                            Value = -30,
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 552, Text = "Далее" },
                }
            },
            [576] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 707, Text = "Останетесь на месте" },
                    new Option { Destination = 461, Text = "Спросите напарника – что будем делать?" },
                }
            },
            [577] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = 5,
                    },
                },

                RemoveTrigger = "EvilEye",

                Options = new List<Option>
                {
                    new Option { Destination = 537, Text = "Далее" },
                }
            },
            [578] = new Paragraph
            {
                Trigger = "Brothers",

                Options = new List<Option>
                {
                    new Option { Destination = 543, Text = "Далее" },
                }
            },
            [579] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 506, Text = "Далее" },
                }
            },
            [580] = new Paragraph
            {
                Trigger = "One",

                Options = new List<Option>
                {
                    new Option { Destination = 465, Text = "Далее" },
                }
            },
            [581] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 573, Text = "Далее" },
                }
            },
            [582] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 477, Text = "Будете настаивать на том, чтобы бежать дальше по коридору" },
                    new Option { Destination = 427, Text = "Согласитесь с напарником и подниметесь по лестнице" },
                }
            },
            [583] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 493, Text = "Далее" },
                }
            },
            [584] = new Paragraph
            {
                Trigger = "Ball",

                Options = new List<Option>
                {
                    new Option { Destination = 520, Text = "Далее" },
                }
            },
            [585] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 493, Text = "Вы сумели набрать необходимый вес" },
                    new Option { Destination = 556, Text = "Рискнёт Коннери" },
                    new Option { Destination = 521, Text = "Вы сами рискнёте" },
                }
            },
            [586] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 564, Text = "Попытаетесь докричаться до Коннери" },
                    new Option { Destination = 515, Text = "Свернете за поворот и будете бежать, пока хватит сил" },
                    new Option { Destination = 405, Text = "Останетесь в своем коридоре и примете бой" },
                }
            },
            [587] = new Paragraph
            {
                Trigger = "Four",

                Options = new List<Option>
                {
                    new Option { Destination = 436, Text = "Далее" },
                }
            },
            [588] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 518, Text = "Успели" },
                    new Option { Destination = 562, Text = "Нет" },
                }
            },
            [589] = new Paragraph
            {
                Trigger = "Two",

                Options = new List<Option>
                {
                    new Option { Destination = 552, Text = "Далее" },
                }
            },
            [590] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -15,
                    },
                    new Modification
                    {
                        Name = "Strength",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 441, Text = "Далее" },
                }
            },
            [591] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 582, Text = "Далее" },
                }
            },
            [592] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 578, Text = "Если у вас в вещах есть старый бронзовый наруч" },
                    new Option { Destination = 543, Text = "Далее" },
                }
            },
            [593] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 595, Text = "Далее" },
                }
            },
            [594] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [595] = new Paragraph
            {
                Trigger = "one, two, four, five",

                Options = new List<Option>
                {
                    new Option { Destination = 554, Text = "Если у вас записаны все эти слова: один, два, три, четыре, пять", OnlyIf = "one, two, three, four, five" },
                    new Option { Destination = 519, Text = "Если у вас записаны четыре из этих слов, а какого-то одного не хватает", OnlyIf = "4; one; two; three; four; five" },
                    new Option { Destination = 509, Text = "Если ни одни из вариантов, приведенных выше, не про вас, то ваш спринт был безупречен" },
                }
            },
            [596] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 579, Text = "Успели" },
                    new Option { Destination = 567, Text = "Нет" },
                    new Option { Destination = 546, Text = "ЗАСЛОН" },
                }
            },
            [597] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 486, Text = "Далее" },
                }
            },
            [598] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте в первый раз",

                        Damage = new Modification
                        {
                            Name = "Hitpoints",
                            Value = -3,
                        },
                    },
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте во второй раз",

                        Damage = new Modification
                        {
                            Name = "Hitpoints",
                            Value = -3,
                        },
                    },
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте в третий раз",

                        Damage = new Modification
                        {
                            Name = "Hitpoints",
                            Value = -3,
                        },
                    },
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте в четвёртый раз",

                        Damage = new Modification
                        {
                            Name = "Hitpoints",
                            Value = -3,
                        },

                        Aftertext = "Каждый неудача обойдется вам в 3 ЖИЗНИ.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 573, Text = "Далее" },
                }
            },
            [599] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -8,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 544, Text = "Далее" },
                }
            },
            [600] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 625, Text = "Далее" },
                }
            },
            [601] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 635, Text = "Далее" },
                }
            },
            [602] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = 0,
                        WizardWoundsPenalty = -3,
                        ThrowerWoundsPenalty = -3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 683, Text = "Далее" },
                }
            },
            [603] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 619, Text = "Если у вас записано слово дыхание", OnlyIf = "Breath" },
                    new Option { Destination = 628, Text = "Далее" },
                }
            },
            [604] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 629, Text = "Далее" },
                }
            },
            [605] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 614, Text = "Вы попятитесь дальше, забираясь под леса" },
                    new Option { Destination = 639, Text = "или подпрыгнете и подтянетесь, забираясь на нижний ярус" },
                }
            },
            [606] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте",
                        Aftertext = "Если ваши рефлексы подведут вас, то снаряд достигнет своей цели\n\nЕсли же вы быстры как ветер, то вы успеете отбить летящий шар. В этом случае:",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 638, Text = "Если у вас в руках орочий яхан" },
                    new Option { Destination = 652, Text = "Если же обычный ведьмачий меч" },
                    new Option { Destination = 661, Text = "Снаряд достигнет своей цели" },
                }
            },
            [607] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 629, Text = "Далее" },
                }
            },
            [608] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 634, Text = "Далее" },
                }
            },
            [609] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 682, Text = "Далее" },
                }
            },
            [610] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 623, Text = "Далее" },
                }
            },
            [611] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 630, Text = "Остановитесь перед расселиной и будете ждать атаки приближающегося дракона" },
                    new Option { Destination = 663, Text = "Начнете огибать ее по дуге, надеясь, что Каратана попытается срезать путь через зеленое облако, клубящееся над расселиной" },
                }
            },
            [612] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 661, Text = "«Это не моя война» – и останетесь стоять на месте" },
                    new Option { Destination = 654, Text = "«Тысяча орков, что я делаю?» – и окликните инеистого великана, дав понять, что вы еще живы" },
                }
            },
            [613] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 637, Text = "Если у вас записано слово идолы", OnlyIf = "Idols" },
                    new Option { Destination = 620, Text = "Далее" },
                }
            },
            [614] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 670, Text = "Далее" },
                }
            },
            [615] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 665, Text = "Если его ДОВЕРИЕ равно 10 или больше", OnlyIf = "ДОВЕРИЕ >= 10" },
                    new Option { Destination = 632, Text = "Если его ДОВЕРИЕ меньше 10", OnlyIf = "ДОВЕРИЕ < 10" },
                }
            },
            [616] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 651, Text = "Далее" },
                }
            },
            [617] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 629, Text = "Далее" },
                }
            },
            [618] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 684, Text = "Далее" },
                }
            },
            [619] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 618, Text = "Предпримете еще одну попытку пробить доспехи" },
                    new Option { Destination = 684, Text = "Решите, что тут дело нечисто, и надо действовать как-то по-другому" },
                }
            },
            [620] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 661, Text = "Далее" },
                }
            },
            [621] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 648, Text = "Попробуете немного сдвинуть близлежащий валун, чтобы преградить им траекторию шипастого шара (но камень выглядит невероятно тяжелым)" },
                    new Option { Destination = 606, Text = "Попытаетесь сбить его в полете ударом меча (но даже если выйдет – разлет осколков будет представлять серьезную угрозу вашему здоровью)" },
                    new Option { Destination = 449, Text = "ЗАСЛОН" },
                }
            },
            [622] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 636, Text = "Далее" },
                }
            },
            [623] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 631, Text = "Вы бросите камень в пасть дракона" },
                    new Option { Destination = 659, Text = "Закричите и замашете руками, привлекая его внимание" },
                    new Option { Destination = 602, Text = "Поднимете булыжник потяжелее и обрушите его на лапу дракона" },
                }
            },
            [624] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 629, Text = "Далее" },
                }
            },
            [625] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 650, Text = "Далее" },
                }
            },
            [626] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 695, Text = "Далее" },
                }
            },
            [627] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 603, Text = "Далее" },
                }
            },
            [628] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -8,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 619, Text = "Далее" },
                }
            },
            [629] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 605, Text = "деревянные леса" },
                    new Option { Destination = 611, Text = "зеленое облако над расселиной" },
                    new Option { Destination = 653, Text = "Решите применить СГУСТОК" },
                    new Option { Destination = 679, Text = "Если все варианты испробованы, то больше бегать от ледяной смерти у вас уже не получается – вы слишком устали" },
                }
            },
            [630] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте",
                    },
                },

                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 622, Text = "Вы были быстрее дракона" },
                    new Option { Destination = 679, Text = "Нет" },
                }
            },
            [631] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 677, Text = "Ваша специализация – метатель" },
                    new Option { Destination = 659, Text = "Иначе" },
                }
            },
            [632] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 681, Text = "Далее" },
                }
            },
            [633] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 629, Text = "Далее" },
                }
            },
            [634] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 615, Text = "Далее" },
                }
            },
            [635] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 601, Text = "Если у вас записано слово учеба", OnlyIf = "Study" },
                    new Option { Destination = 678, Text = "Стоять на месте" },
                    new Option { Destination = 686, Text = "Двигаться вокруг карды по часовой стрелке, согласно оси ее вращения" },
                    new Option { Destination = 662, Text = "Двигаться против часовой стрелки, и, соответственно, против оси ее вращения" },
                    new Option { Destination = 644, Text = "Двигаться мелкими шажками взад-вперед, изображая из себя маятник" },
                }
            },
            [636] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -5,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 629, Text = "Далее" },
                }
            },
            [637] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 603, Text = "Далее" },
                }
            },
            [638] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 688, Text = "Далее" },
                }
            },
            [639] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 655, Text = "Отбегаете по настилу в сторону как можно дальше" },
                    new Option { Destination = 674, Text = "Разбегаетесь навстречу Каратане и прыгаете, рассчитывая пролететь над его пастью и приземлиться ему на шею" },
                }
            },
            [640] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 698, Text = "Далее" },
                }
            },
            [641] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 633, Text = "Далее" },
                }
            },
            [642] = new Paragraph
            {
                Trigger = "Breath",

                Options = new List<Option>
                {
                    new Option { Destination = 694, Text = "Далее" },
                }
            },
            [643] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 697, Text = "Если его ДОВЕРИЕ 7 или больше", OnlyIf = "ДОВЕРИЕ >= 7" },
                    new Option { Destination = 647, Text = "Если его ДОВЕРИЕ меньше 7", OnlyIf = "ДОВЕРИЕ < 7" },
                }
            },
            [644] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "КАРДА",
                                Strength = 12,
                                Hitpoints = 14,
                            },
                        },

                        Aftertext = "Победа откроет вам путь.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 612, Text = "Далее" },
                }
            },
            [645] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 669, Text = "Далее" },
                }
            },
            [646] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 603, Text = "Далее" },
                }
            },
            [647] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -15,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 680, Text = "Если он все еще жив, вы оттаскиваете его тело к месту, где оставили раненного Браннибора, раздумывая, что же делать дальше" },
                    new Option { Destination = 661, Text = "Если нет" },
                }
            },
            [648] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 667, Text = "Если ваша специализация – воин" },
                    new Option { Destination = 661, Text = "Если нет" },
                }
            },
            [649] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 656, Text = "Далее" },
                }
            },
            [650] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 675, Text = "Далее" },
                }
            },
            [651] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 693, Text = "Закусив губу, будете идти вперед" },
                    new Option { Destination = 685, Text = "Отчаянно крикнете что-нибудь ободряющее Браннибору" },
                }
            },
            [652] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -8,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 688, Text = "Далее" },
                }
            },
            [653] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 687, Text = "Подождете еще секунд 5, чтобы уж наверняка" },
                    new Option { Destination = 658, Text = "Рискнете прямо сейчас" },
                }
            },
            [654] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 646, Text = "«Жалкое отродье эпической потаскухи, межвидовая накипь, ты просто струсил»" },
                    new Option { Destination = 613, Text = "«Каратана из рода инеистых великанов, я вызываю тебя на поединок, и пусть услышат меня духи ветра и камня! Да будет так!»" },
                    new Option { Destination = 620, Text = "Будете немногословны и молча, без изысков, поманите Тану мечом — жест, понятный всем и каждому" },
                    new Option { Destination = 664, Text = "Если у вас есть белая корона" },
                }
            },
            [655] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 604, Text = "Будете прыгать на землю" },
                    new Option { Destination = 641, Text = "Досчитаете до пяти, и прыгнете только потом, молясь при этом, чтобы леса не рухнули раньше" },
                }
            },
            [656] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 692, Text = "Если у вас записано слово братья", OnlyIf = "Brothers" },
                    new Option { Destination = 643, Text = "Далее" },
                }
            },
            [657] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 682, Text = "Далее" },
                }
            },
            [658] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "DiceCheck",
                        ButtonName = "Кинуть кубик",
                        Aftertext = "Если ваша специализация маг, смотрите вариант «чет». Иначе все решает не умение, а чистое везение.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 607, Text = "Выпал чет" },
                    new Option { Destination = 624, Text = "Выпал нечет" },
                }
            },
            [659] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 669, Text = "Далее" },
                }
            },
            [660] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -3,
                    },
                },

                Trigger = "Breath",

                Options = new List<Option>
                {
                    new Option { Destination = 694, Text = "Далее" },
                }
            },
            [661] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [662] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 635, Text = "Далее" },
                }
            },
            [663] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 689, Text = "Если у вас есть рог марала или изюбря" },
                    new Option { Destination = 617, Text = "Далее" },
                }
            },
            [664] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 627, Text = "Достанете из мешка корону" },
                    new Option { Destination = 654, Text = "Не будете этого делать" },
                }
            },
            [665] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 681, Text = "Далее" },
                }
            },
            [666] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 649, Text = "Далее" },
                }
            },
            [667] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 688, Text = "Далее" },
                }
            },
            [668] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 616, Text = "Попытаетесь скрестить с противником клинки, войдя с ним в клинч" },
                    new Option { Destination = 691, Text = "Сымитируете, будто вы споткнулись (быть может, даже придется подставиться под удар)" },
                }
            },
            [669] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 629, Text = "Далее" },
                }
            },
            [670] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 633, Text = "Далее" },
                }
            },
            [671] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 680, Text = "Далее" },
                }
            },
            [672] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 695, Text = "Далее" },
                }
            },
            [673] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 657, Text = "Успели" },
                    new Option { Destination = 609, Text = "Нет" },
                }
            },
            [674] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [675] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 698, Text = "Попытаетесь отговорить напарника" },
                    new Option { Destination = 640, Text = "Без лишних церемоний треснете его по голове, чтобы оглушить" },
                }
            },
            [676] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 694, Text = "Далее" },
                }
            },
            [677] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 645, Text = "Далее" },
                }
            },
            [678] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "КАРДА",
                                Strength = 15,
                                Hitpoints = 14,
                            },
                        },

                        Aftertext = "Победа над ней откроет вам путь.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 612, Text = "Далее" },
                }
            },
            [679] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [680] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 696, Text = "Далее" },
                }
            },
            [681] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 626, Text = "Поинтересуетесь, что ей нужно" },
                    new Option { Destination = 672, Text = "Проигнорируете ее и все-таки оглянетесь на жреца" },
                }
            },
            [682] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 621, Text = "Попробуете остановить снаряд во что бы то ни стало" },
                    new Option { Destination = 661, Text = "Предпочтете укрыться за валуном" },
                }
            },
            [683] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 645, Text = "Далее" },
                }
            },
            [684] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 668, Text = "Далее" },
                }
            },
            [685] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 608, Text = "Далее" },
                }
            },
            [686] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "КАРДА",
                                Strength = 14,
                                Hitpoints = 14,
                            },
                        },

                        Aftertext = "Победа над ней откроет вам путь.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 612, Text = "Далее" },
                }
            },
            [687] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Empty = true,
                    },
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 568, Text = "У вас еще есть ведьмачий Эликсир" },
                    new Option { Destination = 699, Text = "Его нет" },
                }
            },
            [688] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 676, Text = "«Я бился вместе с ними»" },
                    new Option { Destination = 661, Text = "«Война и орки, ты прав»" },
                    new Option { Destination = 642, Text = "«Это мои друзья»" },
                    new Option { Destination = 660, Text = "Угрюмое молчание" },
                }
            },
            [689] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 636, Text = "Далее" },
                }
            },
            [690] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 666, Text = "Далее" },
                }
            },
            [691] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте",

                        Damage = new Modification
                        {
                            Name = "Hitpoints",
                            Value = -3,
                        },

                        Aftertext = "Если ваша реакция запаздывает, придется потерять 3 ЖИЗНИ.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 651, Text = "Далее" },
                }
            },
            [692] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 680, Text = "Далее" },
                }
            },
            [693] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 661, Text = "Далее" },
                }
            },
            [694] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 635, Text = "Далее" },
                }
            },
            [695] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 700, Text = "Далее" },
                }
            },
            [696] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 673, Text = "Далее" },
                }
            },
            [697] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Reaction",
                        ButtonName = "Реагируйте",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 671, Text = "Успели" },
                    new Option { Destination = 647, Text = "Нет" },
                }
            },
            [698] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 661, Text = "«Я сделал все что мог. Ты тоже был отличным попутчиком, Коннери из Таннендока. Жаль, что эта история так нелепо заканчивается» – и вы остаетесь на месте." },
                    new Option { Destination = 610, Text = "«Какого черта, я же не собираюсь жить вечно» – и вы, сбросив куртку, догоняете напарника" },
                }
            },
            [699] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [700] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [701] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Стать воином",
                        Specialization = Character.SpecializationType.Warrior,
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Стать магом",
                        Specialization = Character.SpecializationType.Wizard,
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Стать метателем",
                        Specialization = Character.SpecializationType.Thrower,
                        Aftertext = "Учтите, что помимо всего перечисленного, специализация может вам пригодится в самый неожиданный момент. Воин, например, самый сильный из всей троицы, а маг может почувствовать заранее какую-то магическую ловушку.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 702, Text = "Далее" },
                }
            },
            [702] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Обучиться лечению",
                        Text = "Заклинание 'ЛЕЧЕНИЕ'",
                        Spell = true,
                        Aftertext = "Заклинание самоисцеления (уменьшительно именуемое всеми ведьмаками ЛЕЧИЛКА), добавляет 8 ЖИЗНЕЙ (кроме как во время боя).",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Обучиться заслону",
                        Text = "Заклинание 'ЗАСЛОН'",
                        Spell = true,
                        Aftertext = "На несколько секунд создает между вами и противником невидимую магическую преграду.",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Обучиться сгустку",
                        Text = "Заклинание 'СГУСТОК'",
                        Spell = true,
                        Aftertext = "Удар по противнику небольшим сгустком пламени, отнимающий у него 6 ЖИЗНЕЙ.",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Обучиться взору",
                        Text = "Заклинание 'ВЗОР'",
                        Spell = true,
                        Aftertext = "Включает т. н. «истинное зрение». Улучшает ориентировку в темноте и дает способность увидеть скрытую магию. Применениев условиях хорошей освещенности может быть болезненным. Недоступно, если вы выберете специализацию «воин».",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Обучиться броне",
                        Text = "Заклинание 'БРОНЯ'",
                        Spell = true,
                        Aftertext = "Ваши мышцы каменеют, сосуды сужаются. На время одного боя вы теряете в два раза меньше ЖИЗНИ от ударов противника.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 1, Text = "Далее" },
                }
            },
            [703] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "ConneryHitpoints",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 537, Text = "Далее" },
                }
            },
            [704] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 453, Text = "Вы подойдете и попробуете оказать ему помощь" },
                    new Option { Destination = 537, Text = "Тихо перейдете мост, пока ловушка обезврежена и проследуете дальше" },
                }
            },
            [705] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",
                        ReactionHit = "2-1",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПУМА",
                                Strength = 12,
                                Hitpoints = 6,
                            },
                        },

                        Aftertext = "К счастью, живучесть вашего противника оказывается не такой уж большой, и нескольких точных попаданий достаточно, чтобы превратить черную пантеру в холодный, пригодный лишь для набивки чучела, труп.\n\nСтряхнув с клинка кровь и шерсть, вы оглядываетесь на Коннери. Тот бросился было вам на выручку, но, увидев, что вы и сами быстро справились, остановился на полпути.\n\nЧто ж, между вами и выходом больше, кажется, не осталось никаких преград.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 509, Text = "Далее" },
                }
            },
            [706] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 504, Text = "Далее" },
                }
            },
            [707] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 583, Text = "Далее" },
                }
            },
            [708] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 506, Text = "Далее" },
                }
            },
            [709] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 571, Text = "Записано" },
                    new Option { Destination = 412, Text = "Нет" },
                }
            },
            [710] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [711] = new Paragraph
            {
                Trigger = "Delay",

                Options = new List<Option>
                {
                    new Option { Destination = 535, Text = "Далее" },
                }
            },
            [712] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Hitpoints",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 493, Text = "Далее" },
                }
            },
            [713] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 441, Text = "Далее" },
                }
            },
        };
    }
}
