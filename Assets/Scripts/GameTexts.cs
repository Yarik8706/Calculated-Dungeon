using UnityEngine;

public static class GameTexts
{
    public static readonly MultiText[] DeathTexts =
    { 
        new("Даже математики \n умирают на работе... \n ...хмм, весело", "Even mathematicians \n die at work... \n ...hmm, fun"),
        new("Вы все равно не смогли бы решить \n этот пример", "You couldn’t solve \n this example anyway"),
        new("Ауч", "auch"),
        new("Евклид, Гаусс, Коши, Эйлер...\n жаль, что\n о тебе никто не узнает в таком виде", 
            "Euclid, Gauss, Cauchy, Euler... \n it's a pity that no one \n will know about you like that")
    };
    
    public static readonly MultiText[] TimeEndTexts =
    { 
        new("Время летит незаметно, когда\n тебе весело", "Time flies when \n you're having fun"), 
        new("Еще не время",   "It's not time yet"), 
        new("Ты опоздал",   "You are late"), 
        new("Время деньги \n и ты банкрот",   "Time is money \n and you are bankrupt")
    };
    
    public static readonly MultiText[] WinTexts =
    {   
        new("Круто! Ты чудесен! \n Умник", "Cool! You are wonderful! \n Nerd"),
        new("Что ж, на следующем уроке \n мы научимся \n брать тройные интегралы", "Well, in the next lesson \n we will learn how \n to take triple integrals"),
        new("Не могли бы вы решить это быстрее?", "Couldn't you solve it faster?"),
        new("Хороший стиль пальцев", "Nice fingerstyle"),
        new("Чувак, ты делаешь домашнее задание моей \n восьмилетней сестры", "Man, you do my eight year old \n sister's homework")
    };
    
    public static MultiText[] errorMathTexts =
    { 
        new("Не математический", "Not mathematical"),
        new("e^(i*PI) + 1 = 0 \n хотя бы запомни это", "e^(i*PI) + 1 = 0 \n at least remember this"),
        new("F", "F"),
        new("Бесконечное количество \n математиков заходит в бар... \n ...но без тебя", "An infinite number of \n mathematicians walk into a bar... \n ...but without you")
    };

    public static MultiText lvlText = new("Уровень", "Level");
    public static MultiText easyLvlText = new("Сложность легкая", "Difficulty is easy");
    public static MultiText middleLvlText = new("Сложность терпимая", "Difficulty is tolerable");
    public static MultiText hardLvlText = new("Уровень сложноват!", "The level is difficult!");
    public static MultiText winText = new(" Победа!", "WIN");
    public static MultiText defeatText = new(" Поражение!", "LOSE");
    public static MultiText recordTextStart = new("Время прохождения уровней на скорость:", "Time to complete levels at speed:");
    public static MultiText recordTextNotRecord = new("Еще не пройден", "Not passed yet");
    public static MultiText recordTextResult = new("Итого: ", "Total: ");
    public static MultiText recordTextPlace = new("Место в рейтинге: ", "Place in the ranking: ");

    public static readonly IMultiText[] TextsOnLvls =
    {
        new CrossplatformMultiText(
            new MultiText("Найди в примере ошибку и стрельни в нее, используй джойстик для этого!", "Find the mistake in the example and shoot it, use the joystick to do this!"),
            new MultiText("A D – движение, ПРОБЕЛ – прыжок, стреляй мышью. \nНайди в примере ошибку и стрельни в нее!", "A D - Movement, SPACE - jump, Use mouse for shoot \nFind the mistake in the example and shoot it")),
        new CrossplatformMultiText(
            new MultiText("С помощью рывка '<'\nпроскочи через пилы!", "Use the '<' dash to \njump through the saws!"),
            new MultiText("С помощью рывка \nна Shift проскочи пилы!", "Dash through the saws \nwith a Shift dash!")
        ),
        new MultiText("Используй эффективней дабл прыжок\n:)", "Use your double jump more effectively\n:)"),
        new MultiText("Мостик рухнул...", "The bridge collapsed..."),
        new MultiText("Используй двойной прыжок чтобы быстрее проходить препятствия!", "Space+Space - Doublejump\n \n Do you know what a doublejump is?"),
        new MultiText("БЕГИ!\n \nБЕГИ!!\n \nБЕГИ!!!", "RUN!\n \nRUN!!\n \nRUN!!!"),
        new MultiText("ШИПЫ ШИПЫ \nШИПЫ ШИПЫ", "SPIKES SPIKES\nSPIKES SPIKES"),
        new MultiText("ЛОЛ", "LOL"),
        new MultiText("Полегче на поворотах", "Take it easy on turns"),
        new MultiText("Вжжжжж", "Vzhzhzhzh"),
        new MultiText("ПРЫГАЙ!\nПРЫГАЙ!\nПРЫГАЙ!", "JUMP!\nJUMP!\nJUMP!"),
        new MultiText("Покажи на что \n ты способен!", "Show what you are capable of!"),
        new MultiText("Это что? Снежный уровень?", "What's this? Snow level?"),
        new MultiText("Ну как это\n проходить???", "Well, how to go \nthrough this???"),
        new MultiText("Ну и ветер!\nПрям сносит!", "What a wind!\nIt's blowing away!"),
    };
}