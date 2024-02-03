using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Inputs;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using YG;

public class IntroDialogControl : MonoBehaviour
{
    private int _nowPage;
    public TMP_Text text;
    public static bool isIntro = true;

    private void Awake()
    {
        YandexGame.InitEnvirData();
#if !UNITY_EDITOR
        Constants.isPC = YandexGame.EnvironmentData.isDesktop;
#else
        YandexGame.savesData.isIntroWas = false;
#endif
        YandexGame.InitLang();
        Constants.isRu = YandexGame.lang == "ru";
        YandexGame.SwitchLangEvent += PlayerData.ChangeLanguageEvent;
    }

    private readonly IMultiText[] _manualDialogText =
    {
        new MultiText("Эта история произошла давным-давно в далёком-далёком местe...", "This story happened a long time ago in a far, distant place..."),
        new MultiText("Однажды маленькая девочка вернулась домой из магической академии. Она принесла домой плохие новости - ей поставили низкую оценку за экзамен...", "One day a little girl returned home from a magic academy. She brought home bad news - she got a low grade on the exam..."),
        new MultiText("...экзамен по математике.", "...math exam."),
        new MultiText("Да, девочка имела плохие знания в математике...", "Yes, the girl had poor knowledge of mathematics..."),
        new MultiText("...и в остальных предметах тоже...", "...and in other subjects too..."),
        new MultiText("...она вообще была не самым умным ребенком.", "... she wasn't the smartest kid ever."),
        new MultiText("Ее отец, верховный маг Капитолия, был в ярости! Невежество дочери было оскорблением его имени.", "Her father, the capitol archmage, was furious! His daughter's ignorance was an insult to his name."),
        new MultiText("Чтобы проучить дочь, а также улучшить её знания в математике, маг отправил её в заброшенный город дровосеков, который находился у подножья вулкана.", "To teach his daughter a lesson, as well as improve her knowledge in mathematics, the magician sent her to the abandoned city of woodcutters, which was located at the foot of the volcano."),
        new MultiText("...он был не самым лучшим отцом...", "...he was not the best father..."),
        new MultiText("Однако для защиты маг дал ей светлячка-огнеплюя.", "However, for protection, the magician gave her a firefly firespitter."),
        new MultiText("...сама девочка колдовать не умела, на уроках магии она спала...", "... the girl herself did not know how to conjure, she slept in magic lessons ..."),
        new MultiText("Отправляя дочь в опасное приключение, отец дал ей важное наставление...", "Sending his daughter on a dangerous adventure, the father gave her important advice ..."),
        new CrossplatformMultiText(new("Запомни две главные истины: Дабл прыжок работает и джойстик чтобы стрелять", "Remember two main truths: Double jump works and the joystick is used to shoot."),
            new MultiText("Запомни главную истину: ESC чтобы выйти в меню", "Remember the main truth: ESC to exit to the menu")),
        new CrossplatformMultiText(new("Если ничего не поняла, могу всё повторить - дабл прыжок работает и джойстик потяни и отпусти чтобы стрельнуть. Если всё ясно, ступай вперед.", "If I don’t understand anything, I can repeat everything - the double jump works and you pull the joystick and release it to shoot. If everything is clear, go ahead."),
            new("Если ничего не поняла, могу всё повторить - после решения примеров дверь откроется. Если всё ясно, ступай вперед.", "If I don’t understand anything, I can repeat everything - after solving the examples, the door will open. If everything is clear, go ahead.")
        )
    };

    private readonly IMultiText[] _finalDialogText =
    {
        new MultiText("Спустя множество попыток, девочка смогла пройти все испытания.", "After many attempts, the girl was able to pass all the tests."),
        new MultiText("Смогла ли она научиться математике?", "Was she able to learn math?"),
        new MultiText("Возможно...", "Perhaps..."),
        new MultiText("...но она точно научилась уворачиваться от летящих в неё пил и не падать в лаву.", "...but she definitely learned to dodge the saws flying at her and not fall into the lava."),
        new MultiText("И спасибо тебе, что играл в мою игру!", "And thank you for playing my game!"),
        new MultiText("Гамовер", "GAME OVER"),
        new MultiText("намек на сиквел", "GAME OVER?")
    };

    private IMultiText[] _activeDialogText;
    
    private void Start()
    {
        if (YandexGame.savesData.isIntroWas)
        {
            SceneManager.LoadScene(Constants.MenuSceneId);
            return;
        }
        if (isIntro)
        {
            _activeDialogText = _manualDialogText;
        }
        else
        {
            _activeDialogText = _finalDialogText;
        }

        NextPage(); 
    }

    public void NextPage()
    {
        if(_nowPage > _activeDialogText.Length) return;
        if (_nowPage == _activeDialogText.Length)
        {
            _nowPage++;
            BlackoutControl.Instance.StartBlackount().OnComplete(() =>
            {
                SceneManager.LoadScene(Constants.MenuSceneId);
                isIntro = false;
                YandexGame.savesData.isIntroWas = true;
                PlayerData.SaveData();
            });
            return;
        }

        text.text = _activeDialogText[_nowPage].GetText();
        _nowPage++;
    }
}
