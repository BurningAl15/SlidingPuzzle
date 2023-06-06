using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterSelected : MonoBehaviour
{
    public static CharacterSelected _instance;
    [SerializeField] private Character currentCharacter;

    private Coroutine currentCoroutine = null;

    [SerializeField] int difficulty=2;
    
    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if(_instance!=null)
            DestroyImmediate(this);
        DontDestroyOnLoad(gameObject);
    }

    public Character CurrentCharacter => currentCharacter;
    public int Difficulty => difficulty;

    public void Initialize(){
      SetDifficulty(3);
    }

    public void SetDifficulty(int _difficulty)
    {
        SelectDifficultyManager._instance.SetCurrentSelected(_difficulty-2);
        difficulty = _difficulty;
    }
    
    public void SetCharacter(int _char)
    {
        if (currentCoroutine == null)
            currentCoroutine = StartCoroutine(Set_Character(_char));
    }

    IEnumerator Set_Character(int _char)
    {
        currentCharacter = (Character)_char;
        print(currentCharacter + " - "+ _char);
        yield return new WaitForSeconds(1f);
        yield return TransitionManager._instance.TransitionEffect_FadeIn();
        SceneUtils.ToNextScene();
        currentCoroutine = null;
    }

    public void ReturnToMainMenu()
    {
        if (currentCoroutine == null)
            currentCoroutine = StartCoroutine(Clear_Character());
    }
    
    IEnumerator Clear_Character()
    {
        currentCharacter = Character.None;
        yield return new WaitForSeconds(1f);
        yield return TransitionManager._instance.TransitionEffect_FadeIn();
        SceneUtils.ToMainMenu();
        currentCoroutine = null;
    }
}
