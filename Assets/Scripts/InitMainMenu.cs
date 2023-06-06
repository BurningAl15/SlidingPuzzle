using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitMainMenu : MonoBehaviour
{
    void Start()
    {
        CharacterSelected._instance.Initialize();
    }
}
