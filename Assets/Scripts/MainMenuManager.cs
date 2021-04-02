using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    private Coroutine currentCoroutine = null;
    void Start()
    {
        if (currentCoroutine == null)
            currentCoroutine = StartCoroutine(InitializeMainMenu());
    }

    IEnumerator InitializeMainMenu()
    {
        yield return TransitionManager._instance.TransitionEffect_FadeOut();
        currentCoroutine = null;
    }

    public void ToNextScene()
    {
        if (currentCoroutine == null)
            currentCoroutine = StartCoroutine(To_NextScene());
    }
    IEnumerator To_NextScene()
    {
        yield return TransitionManager._instance.TransitionEffect_FadeIn();
        SceneUtils.ToMainMenu();
        currentCoroutine = null;
    }

    public void ToGameplay(int _char)
    {
        CharacterSelected._instance.SetCharacter(_char);
    }

    public void SetDifficulty(int difficulty)
    {
        CharacterSelected._instance.SetDifficulty(difficulty);
    }
}
