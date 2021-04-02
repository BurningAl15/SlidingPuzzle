using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUtils : MonoBehaviour
{
    public static void ToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public static void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}